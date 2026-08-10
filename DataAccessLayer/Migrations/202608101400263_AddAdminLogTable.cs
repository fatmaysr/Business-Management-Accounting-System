namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminLogs",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        Process = c.String(),
                        ProcessDate = c.DateTime(nullable: false),
                        Info = c.String(),
                    })
                .PrimaryKey(t => t.LogID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminLogs");
        }
    }
}
