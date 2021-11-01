namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Car", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Car", "Price");
        }
    }
}
