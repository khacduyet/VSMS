namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Member", "FullName", c => c.String(maxLength: 200));
            AlterColumn("dbo.Member", "BirthDay", c => c.String(maxLength: 200));
            AlterColumn("dbo.Member", "Address", c => c.String(maxLength: 200));
            AlterColumn("dbo.Member", "Phone", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Member", "Phone", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Member", "Address", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Member", "BirthDay", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Member", "FullName", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
