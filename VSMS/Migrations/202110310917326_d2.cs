namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Post_Tags", "Tags_Id", "dbo.Tags");
            DropIndex("dbo.Post_Tags", new[] { "Tags_Id" });
            DropColumn("dbo.Post_Tags", "Tags_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post_Tags", "Tags_Id", c => c.Int());
            CreateIndex("dbo.Post_Tags", "Tags_Id");
            AddForeignKey("dbo.Post_Tags", "Tags_Id", "dbo.Tags", "Id");
        }
    }
}
