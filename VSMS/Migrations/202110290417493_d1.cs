namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Permission_details", "PerId", "dbo.Permission");
            DropIndex("dbo.Permission_details", new[] { "PerId" });
            DropTable("dbo.Permission_details");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Permission_details",
                c => new
                    {
                        Detail_Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        PerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Detail_Id);
            
            CreateIndex("dbo.Permission_details", "PerId");
            AddForeignKey("dbo.Permission_details", "PerId", "dbo.Permission", "PerId", cascadeDelete: true);
        }
    }
}
