using MMS.Directory.Banks.Model;
using System;
using System.Linq;

namespace MMS.Directory.Banks.EntityFramework.Data
{
    public static class Mapper
    {
        public static BankInfo ToModel(this DbBankInfo from)
        {
            return from == null ? null : new BankInfo
            {
                Name = from.Name,
                Oid = from.Oid,
                IsActive = from.IsActive,
                CreditArticles = from.CreditArticles.Select(ToModel).ToList()
            };
        }

        public static CreditKindArticle ToModel(this DbCreditKindArticle from)
        {
            return from == null ? null : new CreditKindArticle
            {
                ArticleNo = from.ArticleNo,
                CreditKind = from.CreditKind.ToEnum<CreditKind>()
            };
        }
    }
}
