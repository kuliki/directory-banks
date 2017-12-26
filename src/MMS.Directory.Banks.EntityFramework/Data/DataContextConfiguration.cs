using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace MMS.Directory.Banks.EntityFramework.Data
{
    public static class DataContextConfiguration
    {
        public static void Configure(this DbModelBuilder modelBuilder)
        {
            ConfigureBankInfo(modelBuilder);
            ConfigureCreditKindArticle(modelBuilder);
        }

        private static void ConfigureBankInfo(this DbModelBuilder modelBuilder)
        {
            var config = ConfigureEntity<DbBankInfo>(modelBuilder, "dbo.BankInfo");

            config.Property(x => x.Name).HasMaxLength(BaseStringLength);
            config.Property(x => x.Oid).HasMaxLength(OidStringLength);
            config.Property(x => x.IsActive);
            config.Property(x => x.IsDeleted);
            config.Property(x => x.CreatedDateUtc);
            config.Property(x => x.UpdatedDateUtc);

            config.HasMany(x => x.CreditArticles).WithRequired(x => x.Bank);
            config.HasKey(x => x.Oid);
        }

        private static void ConfigureCreditKindArticle(this DbModelBuilder modelBuilder)
        {
            var config = ConfigureEntity<DbCreditKindArticle>(modelBuilder, "dbo.CreditKindArticle");

            config.Property(x => x.BankOid).IsRequired();
            config.Property(x => x.CreditKind).HasMaxLength(BaseStringLength);
            config.Property(x => x.ArticleNo);

            config.HasKey(x => new { x.BankOid, x.CreditKind });
        }

        private static EntityTypeConfiguration<T> ConfigureEntity<T>(DbModelBuilder modelBuilder, string tableName)
            where T : class
        {
            var config = modelBuilder.Entity<T>();
            config.ToTable(tableName);

            return config;
        }

        private const int BaseStringLength = 50;
        private const int OidStringLength = 50;
    }
}