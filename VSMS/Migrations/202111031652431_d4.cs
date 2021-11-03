namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "CarId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contact", "CarId");
        }
    }
}
