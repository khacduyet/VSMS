namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarDetails", "Car_Id", "dbo.Car");
            DropForeignKey("dbo.CarDetails", "Feature_Id", "dbo.Feature");
            DropIndex("dbo.CarDetails", new[] { "Car_Id" });
            DropIndex("dbo.CarDetails", new[] { "Feature_Id" });
            DropColumn("dbo.CarDetails", "IdCar");
            DropColumn("dbo.CarDetails", "IdFeature");
            RenameColumn(table: "dbo.CarDetails", name: "Car_Id", newName: "IdCar");
            RenameColumn(table: "dbo.CarDetails", name: "Feature_Id", newName: "IdFeature");
            CreateTable(
                "dbo.ImageProductDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdImageProduct = c.Int(nullable: false),
                        IdProduct = c.Int(nullable: false),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Car", t => t.IdProduct, cascadeDelete: true)
                .ForeignKey("dbo.ImageProduct", t => t.IdImageProduct, cascadeDelete: true)
                .Index(t => t.IdImageProduct)
                .Index(t => t.IdProduct);
            
            CreateTable(
                "dbo.ImageProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.CarDetails", "IdCar", c => c.Int(nullable: false));
            AlterColumn("dbo.CarDetails", "IdFeature", c => c.Int(nullable: false));
            CreateIndex("dbo.CarDetails", "IdCar");
            CreateIndex("dbo.CarDetails", "IdFeature");
            AddForeignKey("dbo.CarDetails", "IdCar", "dbo.Car", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CarDetails", "IdFeature", "dbo.Feature", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarDetails", "IdFeature", "dbo.Feature");
            DropForeignKey("dbo.CarDetails", "IdCar", "dbo.Car");
            DropForeignKey("dbo.ImageProductDetails", "IdImageProduct", "dbo.ImageProduct");
            DropForeignKey("dbo.ImageProductDetails", "IdProduct", "dbo.Car");
            DropIndex("dbo.ImageProductDetails", new[] { "IdProduct" });
            DropIndex("dbo.ImageProductDetails", new[] { "IdImageProduct" });
            DropIndex("dbo.CarDetails", new[] { "IdFeature" });
            DropIndex("dbo.CarDetails", new[] { "IdCar" });
            AlterColumn("dbo.CarDetails", "IdFeature", c => c.Int());
            AlterColumn("dbo.CarDetails", "IdCar", c => c.Int());
            DropTable("dbo.ImageProduct");
            DropTable("dbo.ImageProductDetails");
            RenameColumn(table: "dbo.CarDetails", name: "IdFeature", newName: "Feature_Id");
            RenameColumn(table: "dbo.CarDetails", name: "IdCar", newName: "Car_Id");
            AddColumn("dbo.CarDetails", "IdFeature", c => c.Int(nullable: false));
            AddColumn("dbo.CarDetails", "IdCar", c => c.Int(nullable: false));
            CreateIndex("dbo.CarDetails", "Feature_Id");
            CreateIndex("dbo.CarDetails", "Car_Id");
            AddForeignKey("dbo.CarDetails", "Feature_Id", "dbo.Feature", "Id");
            AddForeignKey("dbo.CarDetails", "Car_Id", "dbo.Car", "Id");
        }
    }
}
