namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSaleTableAndWalkInCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.String(),
                        CaseId = c.Int(nullable: false),
                        CariID = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.CashCases", t => t.CaseId)
                .ForeignKey("dbo.Customers", t => t.CariID)
                .ForeignKey("dbo.FastSaleProducts", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.CaseId)
                .Index(t => t.CariID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "ProductId", "dbo.FastSaleProducts");
            DropForeignKey("dbo.Sales", "CariID", "dbo.Customers");
            DropForeignKey("dbo.Sales", "CaseId", "dbo.CashCases");
            DropIndex("dbo.Sales", new[] { "CariID" });
            DropIndex("dbo.Sales", new[] { "CaseId" });
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropTable("dbo.Sales");
        }
    }
}
