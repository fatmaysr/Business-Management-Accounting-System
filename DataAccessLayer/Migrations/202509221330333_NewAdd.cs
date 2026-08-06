namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAdd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CariKodu", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customers", "CariAdi", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "VergiDairesi", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "VergiDairesi", c => c.String());
            AlterColumn("dbo.Customers", "CariAdi", c => c.String());
            AlterColumn("dbo.Customers", "CariKodu", c => c.String(maxLength: 50));
        }
    }
}
