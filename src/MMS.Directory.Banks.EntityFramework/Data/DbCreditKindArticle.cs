namespace MMS.Directory.Banks.EntityFramework.Data
{
    public class DbCreditKindArticle
    {
        public string BankOid { get; set; }

        public string CreditKind { get; set; }

        public int ArticleNo { get; set; }

        public virtual DbBankInfo Bank { get; set; }
    }
}