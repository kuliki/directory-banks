using MMS.Directory.Banks.EntityFramework.Data;
using System.Data.Entity.Migrations;

namespace MMS.Directory.Banks.EntityFramework.Migrations
{
    internal sealed class DataContextConfiguration : DbMigrationsConfiguration<Data.DataContext>
    {
        public DataContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"Migrations\DataContext";
            MigrationsNamespace = "MMS.Directory.Banks.EntityFramework.Migrations.DataContext";
            ContextKey = "MMS.Directory.Banks.EntityFramework.Data.DataContext";
        }
    }
}
