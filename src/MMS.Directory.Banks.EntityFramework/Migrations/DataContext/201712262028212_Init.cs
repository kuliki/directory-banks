namespace MMS.Directory.Banks.EntityFramework.Migrations.DataContext
{
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankInfo",
                c => new
                    {
                        Oid = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDateUtc = c.DateTime(nullable: false),
                        UpdatedDateUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Oid);
            
            CreateTable(
                "dbo.CreditKindArticle",
                c => new
                    {
                        BankOid = c.String(nullable: false, maxLength: 50),
                        CreditKind = c.String(nullable: false, maxLength: 50),
                        ArticleNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BankOid, t.CreditKind })
                .ForeignKey("dbo.BankInfo", t => t.BankOid, cascadeDelete: true)
                .Index(t => t.BankOid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditKindArticle", "BankOid", "dbo.BankInfo");
            DropIndex("dbo.CreditKindArticle", new[] { "BankOid" });
            DropTable("dbo.CreditKindArticle");
            DropTable("dbo.BankInfo");
        }
    }
}
