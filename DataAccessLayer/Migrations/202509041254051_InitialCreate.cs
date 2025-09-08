namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        CariID = c.Int(nullable: false),
                        BankId = c.Int(nullable: false),
                        ClassName = c.String(),
                        ClassNumber = c.String(),
                        AccountNumber = c.String(),
                        Iban = c.String(),
                        AccountName = c.String(),
                        PosStatus = c.Int(nullable: false),
                        CcStatus = c.Int(nullable: false),
                        SecretToken = c.String(),
                        Status = c.Int(nullable: false),
                        Customer_CariID = c.Int(),
                    })
                .PrimaryKey(t => t.BankAccountId)
                .ForeignKey("dbo.Customers", t => t.Customer_CariID)
                .ForeignKey("dbo.Banks", t => t.BankId)
                .ForeignKey("dbo.Customers", t => t.CariID)
                .Index(t => t.CariID)
                .Index(t => t.BankId)
                .Index(t => t.Customer_CariID);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        CariID = c.Int(nullable: false),
                        BankName = c.String(),
                        Branch = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        SecretToken = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BankId)
                .ForeignKey("dbo.Customers", t => t.CariID)
                .Index(t => t.CariID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CariID = c.Int(nullable: false, identity: true),
                        CariKodu = c.String(maxLength: 50),
                        CariAdi = c.String(),
                        CariTipi = c.String(),
                        VergiDairesi = c.String(),
                        VergiNo = c.String(),
                        TCKimlikNo = c.String(),
                        Adres = c.String(),
                        Telefon = c.String(),
                        Email = c.String(),
                        WebSitesi = c.String(),
                    })
                .PrimaryKey(t => t.CariID);
            
            CreateTable(
                "dbo.BankTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        BankId = c.Int(nullable: false),
                        CurrentToken = c.String(),
                        CustomerId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        Cuurency = c.Int(nullable: false),
                        Price = c.String(),
                        Type = c.Int(nullable: false),
                        Content = c.String(),
                        Method = c.Int(nullable: false),
                        SecretToken = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banks", t => t.BankId, cascadeDelete: true)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .Index(t => t.BankId)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.CaseTransactions",
                c => new
                    {
                        CaseTransactionId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CariID = c.Int(nullable: false),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaseTransactionId)
                .ForeignKey("dbo.CashCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CariID)
                .Index(t => t.CaseId)
                .Index(t => t.CariID);
            
            CreateTable(
                "dbo.CashCases",
                c => new
                    {
                        CaseId = c.Int(nullable: false, identity: true),
                        CariID = c.Int(nullable: false),
                        CaseName = c.String(),
                        CaseCode = c.String(),
                        Currency = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaseId)
                .ForeignKey("dbo.Customers", t => t.CariID)
                .Index(t => t.CariID);
            
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
                .PrimaryKey(t => t.CurrencyId)
                .ForeignKey("dbo.Customers", t => t.CariID)
                .Index(t => t.CariID);
            
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
                .PrimaryKey(t => t.CurrecyDetailId)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Customers", t => t.CariID)
                .Index(t => t.CurrencyId)
                .Index(t => t.CariID);
            
            CreateTable(
                "dbo.FastSaleCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        CategoryName = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.FastSaleProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Category = c.String(),
                        Value = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.FastSales",
                c => new
                    {
                        FastSaleId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CariID = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FastToken = c.String(),
                        Type = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FastSaleId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        StockName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StockId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrencyDetails", "CariID", "dbo.Customers");
            DropForeignKey("dbo.CurrencyDetails", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Currencies", "CariID", "dbo.Customers");
            DropForeignKey("dbo.CaseTransactions", "CariID", "dbo.Customers");
            DropForeignKey("dbo.CaseTransactions", "CaseId", "dbo.CashCases");
            DropForeignKey("dbo.CashCases", "CariID", "dbo.Customers");
            DropForeignKey("dbo.BankTransactions", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.BankTransactions", "BankId", "dbo.Banks");
            DropForeignKey("dbo.BankAccounts", "CariID", "dbo.Customers");
            DropForeignKey("dbo.BankAccounts", "BankId", "dbo.Banks");
            DropForeignKey("dbo.Banks", "CariID", "dbo.Customers");
            DropForeignKey("dbo.BankAccounts", "Customer_CariID", "dbo.Customers");
            DropIndex("dbo.CurrencyDetails", new[] { "CariID" });
            DropIndex("dbo.CurrencyDetails", new[] { "CurrencyId" });
            DropIndex("dbo.Currencies", new[] { "CariID" });
            DropIndex("dbo.CashCases", new[] { "CariID" });
            DropIndex("dbo.CaseTransactions", new[] { "CariID" });
            DropIndex("dbo.CaseTransactions", new[] { "CaseId" });
            DropIndex("dbo.BankTransactions", new[] { "BankAccountId" });
            DropIndex("dbo.BankTransactions", new[] { "BankId" });
            DropIndex("dbo.Banks", new[] { "CariID" });
            DropIndex("dbo.BankAccounts", new[] { "Customer_CariID" });
            DropIndex("dbo.BankAccounts", new[] { "BankId" });
            DropIndex("dbo.BankAccounts", new[] { "CariID" });
            DropTable("dbo.Users");
            DropTable("dbo.Stocks");
            DropTable("dbo.FastSales");
            DropTable("dbo.FastSaleProducts");
            DropTable("dbo.FastSaleCategories");
            DropTable("dbo.CurrencyDetails");
            DropTable("dbo.Currencies");
            DropTable("dbo.CashCases");
            DropTable("dbo.CaseTransactions");
            DropTable("dbo.BankTransactions");
            DropTable("dbo.Customers");
            DropTable("dbo.Banks");
            DropTable("dbo.BankAccounts");
        }
    }
}
