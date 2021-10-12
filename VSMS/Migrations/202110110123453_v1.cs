namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Avatar = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Per_relationship",
                c => new
                    {
                        Id_rel = c.Int(nullable: false, identity: true),
                        Id_admin = c.Int(nullable: false),
                        Id_per = c.Int(nullable: false),
                        Date_created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id_rel)
                .ForeignKey("dbo.Admin", t => t.Id_admin, cascadeDelete: true)
                .ForeignKey("dbo.Permission", t => t.Id_per, cascadeDelete: true)
                .Index(t => t.Id_admin)
                .Index(t => t.Id_per);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        PerId = c.Int(nullable: false, identity: true),
                        PerName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PerId);
            
            CreateTable(
                "dbo.Permission_details",
                c => new
                    {
                        Detail_Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        PerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Detail_Id)
                .ForeignKey("dbo.Permission", t => t.PerId, cascadeDelete: true)
                .Index(t => t.PerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Per_relationship", "Id_per", "dbo.Permission");
            DropForeignKey("dbo.Permission_details", "PerId", "dbo.Permission");
            DropForeignKey("dbo.Per_relationship", "Id_admin", "dbo.Admin");
            DropIndex("dbo.Permission_details", new[] { "PerId" });
            DropIndex("dbo.Per_relationship", new[] { "Id_per" });
            DropIndex("dbo.Per_relationship", new[] { "Id_admin" });
            DropTable("dbo.Permission_details");
            DropTable("dbo.Permission");
            DropTable("dbo.Per_relationship");
            DropTable("dbo.Admin");
        }
    }
}
