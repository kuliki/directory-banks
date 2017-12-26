using MMS.Directory.Banks.EntityFramework.Data;
using MMS.Directory.Banks.Model;
using MMS.Directory.Banks.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace MMS.Directory.Banks.EntityFramework.Storage
{
    public class BankInfoStorage : IBankInfoStorage
    {
        public IReadOnlyCollection<BankInfo> GetBanks(bool onlyActive)
        {
            using (var context = CreateDataContext())
            {
                var query = context
                    .QueryBankInfo(false)
                    .AsNoTracking();

                if (onlyActive)
                    query = query.Where(x => x.IsActive);

                return query.AsEnumerable().Select(x => x.ToModel()).ToArray();
            }
        }

        public BankInfo GetBank(string bankOid)
        {
            using (var context = CreateDataContext())
            {
                var bankData = context
                    .QueryBankInfo(false)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Oid == bankOid);

                return bankData.ToModel();
            }
        }

        public void UpdateBank(BankInfo bank)
        {
            bank.AssertNotNull(nameof(bank));

            using (var context = CreateDataContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                context.LockTable<DbBankInfo>(true);

                var bankData = context
                    .QueryBankInfo(false)
                    .FirstOrDefault(x => x.Oid == bank.Oid);

                if (bankData == null)
                    throw new Exception($"Bank {bank.Oid} not found");

                UpdateBankData(bankData, bank);

                context.SaveChanges();
                transaction.Commit();
            }
        }

        public void Sync(IReadOnlyCollection<BankHeader> masterList)
        {
            var banks = masterList.ToDictionary(x => x.Oid);
            var now = DateTime.UtcNow;

            using (var context = CreateDataContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                context.LockTable<DbBankInfo>(true);

                var entities = context.BankInfo.ToDictionary(x => x.Oid);

                //новые банки
                foreach (var bank in masterList.Where(x => !entities.ContainsKey(x.Oid)))
                {
                    var bankData = context.BankInfo.Add(context.BankInfo.Create());

                    bankData.Oid = bank.Oid;
                    bankData.Name = bank.Name;
                    bankData.CreatedDateUtc = now;
                }

                //общие банки, в т.ч. те что были удалены и восстановленны в master-data
                foreach (var bank in masterList.Where(x => entities.ContainsKey(x.Oid)))
                {
                    var bankData = entities[bank.Oid];

                    if (bankData.IsDeleted)
                    {
                        bankData.IsDeleted = false;
                        bankData.UpdatedDateUtc = now;
                    }
                }

                //удаленные банки
                foreach (var bank in entities.Values.Where(x => !banks.ContainsKey(x.Oid)))
                {
                    var bankData = entities[bank.Oid];

                    bankData.IsDeleted = true;
                    bankData.UpdatedDateUtc = now;
                }

                context.SaveChanges();
                transaction.Commit();
            }
        }

        private void UpdateBankData(DbBankInfo bankData, BankInfo bank)
        {
            bankData.UpdatedDateUtc = DateTime.UtcNow;
            bankData.Name = bank.Name;
            bankData.IsActive = bank.IsActive;

            var currentArticles = bankData.CreditArticles.ToArray();
            bankData.CreditArticles.Clear();

            if (bank.CreditArticles != null)
            {
                foreach (var article in bank.CreditArticles)
                {
                    var articleData = currentArticles.FirstOrDefault(x => x.CreditKind == article.CreditKind.ToString());
                    if (articleData == null)
                    {
                        articleData = new DbCreditKindArticle
                        {
                            BankOid = bankData.Oid,
                            Bank = bankData,
                            CreditKind = article.CreditKind.ToString()
                        };
                    }

                    UpdateCreditKindArticleData(articleData, article);

                    bankData.CreditArticles.Add(articleData);
                }
            }
        }

        private void UpdateCreditKindArticleData(DbCreditKindArticle articleData, CreditKindArticle article)
        {
            articleData.ArticleNo = article.ArticleNo;
        }

        private DataContext CreateDataContext()
        {
            return new DataContext();
        }
    }
}
