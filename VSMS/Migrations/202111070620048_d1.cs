namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdminOrder", "AdminId", "dbo.Admin");
            DropForeignKey("dbo.AdminOrder", "OrderId", "dbo.Order");
            DropIndex("dbo.AdminOrder", new[] { "OrderId" });
            DropIndex("dbo.AdminOrder", new[] { "AdminId" });
            AddColumn("dbo.Order", "AdminId", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "AdminId");
            AddForeignKey("dbo.Order", "AdminId", "dbo.Admin", "Id", cascadeDelete: true);
            DropColumn("dbo.Order", "Total");
            DropTable("dbo.AdminOrder");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AdminOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Order", "Total", c => c.Single(nullable: false));
            DropForeignKey("dbo.Order", "AdminId", "dbo.Admin");
            DropIndex("dbo.Order", new[] { "AdminId" });
            DropColumn("dbo.Order", "AdminId");
            CreateIndex("dbo.AdminOrder", "AdminId");
            CreateIndex("dbo.AdminOrder", "OrderId");
            AddForeignKey("dbo.AdminOrder", "OrderId", "dbo.Order", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AdminOrder", "AdminId", "dbo.Admin", "Id", cascadeDelete: true);
        }
    }
}
