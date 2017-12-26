using MMS.Directory.Banks.Core;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System;

namespace MMS.Directory.Banks.EntityFramework.Data
{
    public class DataContext : DbContext
    {
        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DataContext()
            : this("Banks")
        {
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DataException ex)
            {
                throw new StorageException(ExplainDataException(ex), ex);
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DataException ex)
            {
                throw new StorageException(ExplainDataException(ex), ex);
            }
        }

        public IQueryable<DbBankInfo> QueryBankInfo(bool withDeleted = false)
        {
            var query = BankInfo.Include(x => x.CreditArticles);

            if (!withDeleted)
                query = query.Where(x => !x.IsDeleted);

            return query;
        }

        public void LockTable<T>(bool exclusive)
            where T : class
        {
            if (Database.CurrentTransaction == null)
                throw new InvalidOperationException("Database.CurrentTransaction required");

            var tableName = this.GetTableName<T>();
            var lockType = exclusive ? "X" : string.Empty;

            Database.ExecuteSqlCommand($"SELECT TOP 0 NULL FROM {tableName} WITH (TABLOCK{lockType})");
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
        }

        private static string ExplainDataException(DataException exception)
        {
            if (exception == null)
                return null;

            if (exception is DbEntityValidationException validationEx)
            {
                var errorMessages = validationEx.EntityValidationErrors
                     .SelectMany(x => x.ValidationErrors)
                     .Select(x => x.ErrorMessage);

                return string.Join("\r\n", errorMessages);
            }

            var sqlException = FindSqlException(exception);
            if (sqlException != null)
                return sqlException.Message;

            return exception.Message;
        }

        private static SqlException FindSqlException(Exception exception)
        {
            while (exception != null)
            {
                if (exception is SqlException)
                    return (SqlException)exception;

                exception = exception.InnerException;
            }

            return null;
        }

        public DbSet<DbBankInfo> BankInfo { get; set; }

        public DbSet<DbCreditKindArticle> CreditKindArticle { get; set; }
    }
}