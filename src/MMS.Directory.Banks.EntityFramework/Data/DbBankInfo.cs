using System;
using System.Collections.Generic;

namespace MMS.Directory.Banks.EntityFramework.Data
{
    public class DbBankInfo
    {
        public DbBankInfo()
        {
            CreditArticles = new HashSet<DbCreditKindArticle>();
        }

        public string Oid { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime? UpdatedDateUtc { get; set; }

        public virtual HashSet<DbCreditKindArticle> CreditArticles { get; set; }
    }
}
