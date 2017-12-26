using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MMS.Directory.Banks.Model;

namespace MMS.Directory.Banks.EntityFramework.Tests
{
    [TestClass]
    public class BanksStorageTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var service = new Storage.BankInfoStorage();

            var newBankInfo = new BankInfo
            {
                Oid = Guid.NewGuid().ToString(),
                Name = "test",
                IsActive = true,
                CreditArticles = new System.Collections.Generic.List<CreditKindArticle>
                    {
                        new CreditKindArticle
                        {
                            CreditKind = CreditKind.None,
                            ArticleNo = 1,
                        },
                        new CreditKindArticle
                        {
                            CreditKind = CreditKind.Promo,
                            ArticleNo = 2
                        }
                    }
            };

            service.Sync(new[] { newBankInfo });

            //service.UpdateBank(newBankInfo);

            var bank = service.GetBank(newBankInfo.Oid);

            Assert.IsNotNull(bank);
        }
    }
}
