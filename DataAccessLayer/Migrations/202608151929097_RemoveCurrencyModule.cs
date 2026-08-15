namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCurrencyModule : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Currencies", "CariID", "dbo.Customers");
            DropForeignKey("dbo.CurrencyDetails", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.CurrencyDetails", "CariID", "dbo.Customers");
            DropIndex("dbo.Currencies", new[] { "CariID" });
            DropIndex("dbo.CurrencyDetails", new[] { "CurrencyId" });
            DropIndex("dbo.CurrencyDetails", new[] { "CariID" });
            DropTable("dbo.Currencies");
            DropTable("dbo.CurrencyDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CurrencyDetails",
                c => new
                    {
                        CurrecyDetailId = c.Int(nullable: false, identity: true),
                        CurrencyId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CariID = c.Int(nullable: false),
                        Type = c.Int(),
                        Amount = c.Int(),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(),
                        Total = c.String(),
                        Description = c.String(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.CurrecyDetailId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CariID = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        TaxNumber = c.String(),
                        Iban = c.String(),
                        TaxAdmin = c.String(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateIndex("dbo.CurrencyDetails", "CariID");
            CreateIndex("dbo.CurrencyDetails", "CurrencyId");
            CreateIndex("dbo.Currencies", "CariID");
            AddForeignKey("dbo.CurrencyDetails", "CariID", "dbo.Customers", "CariID");
            AddForeignKey("dbo.CurrencyDetails", "CurrencyId", "dbo.Currencies", "CurrencyId");
            AddForeignKey("dbo.Currencies", "CariID", "dbo.Customers", "CariID");
        }
    }
}
