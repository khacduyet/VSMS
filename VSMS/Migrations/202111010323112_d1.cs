namespace VSMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admin", t => t.AdminId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.AdminId);
            
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
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Summary = c.String(nullable: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Image = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admin", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Post_Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        TagId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Single(nullable: false),
                        MemberId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 200),
                        UserName = c.String(nullable: false, maxLength: 200),
                        PassWord = c.String(nullable: false, maxLength: 200),
                        BirthDay = c.String(maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                        Address = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                        Status = c.Byte(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DriveTest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCar = c.Int(nullable: false),
                        IdMember = c.Int(nullable: false),
                        Note = c.String(),
                        Status = c.Byte(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Car", t => t.IdCar, cascadeDelete: true)
                .ForeignKey("dbo.Member", t => t.IdMember, cascadeDelete: true)
                .Index(t => t.IdCar)
                .Index(t => t.IdMember);
            
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarName = c.String(nullable: false, maxLength: 300),
                        ModeId = c.Int(nullable: false),
                        CatId = c.Int(nullable: false),
                        Engine = c.String(maxLength: 200),
                        FuelType = c.String(maxLength: 200),
                        Transmission = c.String(maxLength: 200),
                        Descriptions = c.String(),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CatId, cascadeDelete: true)
                .ForeignKey("dbo.Mode", t => t.ModeId, cascadeDelete: true)
                .Index(t => t.ModeId)
                .Index(t => t.CatId);
            
            CreateTable(
                "dbo.CarDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCar = c.Int(nullable: false),
                        IdFeature = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Car", t => t.IdCar, cascadeDelete: true)
                .ForeignKey("dbo.Feature", t => t.IdFeature, cascadeDelete: true)
                .Index(t => t.IdCar)
                .Index(t => t.IdFeature);
            
            CreateTable(
                "dbo.Feature",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Descriptions = c.String(),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CateName = c.String(nullable: false, maxLength: 300),
                        Note = c.String(maxLength: 500),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Mode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModeName = c.String(nullable: false, maxLength: 300),
                        Year = c.Int(nullable: false),
                        Note = c.String(maxLength: 500),
                        ManafatureId = c.Int(nullable: false),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manuafature", t => t.ManafatureId, cascadeDelete: true)
                .Index(t => t.ManafatureId);
            
            CreateTable(
                "dbo.Manuafature",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Address = c.String(maxLength: 500),
                        Note = c.String(maxLength: 500),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Status = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Car", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Slug = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminOrder", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderDetails", "CarId", "dbo.Car");
            DropForeignKey("dbo.Order", "MemberId", "dbo.Member");
            DropForeignKey("dbo.DriveTest", "IdMember", "dbo.Member");
            DropForeignKey("dbo.DriveTest", "IdCar", "dbo.Car");
            DropForeignKey("dbo.Car", "ModeId", "dbo.Mode");
            DropForeignKey("dbo.Mode", "ManafatureId", "dbo.Manuafature");
            DropForeignKey("dbo.ImageProductDetails", "IdImageProduct", "dbo.ImageProduct");
            DropForeignKey("dbo.ImageProductDetails", "IdProduct", "dbo.Car");
            DropForeignKey("dbo.Car", "CatId", "dbo.Category");
            DropForeignKey("dbo.CarDetails", "IdFeature", "dbo.Feature");
            DropForeignKey("dbo.CarDetails", "IdCar", "dbo.Car");
            DropForeignKey("dbo.AdminOrder", "AdminId", "dbo.Admin");
            DropForeignKey("dbo.Post_Tags", "PostId", "dbo.Post");
            DropForeignKey("dbo.Post", "AuthorId", "dbo.Admin");
            DropForeignKey("dbo.Per_relationship", "Id_per", "dbo.Permission");
            DropForeignKey("dbo.Per_relationship", "Id_admin", "dbo.Admin");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "CarId" });
            DropIndex("dbo.Mode", new[] { "ManafatureId" });
            DropIndex("dbo.ImageProductDetails", new[] { "IdProduct" });
            DropIndex("dbo.ImageProductDetails", new[] { "IdImageProduct" });
            DropIndex("dbo.CarDetails", new[] { "IdFeature" });
            DropIndex("dbo.CarDetails", new[] { "IdCar" });
            DropIndex("dbo.Car", new[] { "CatId" });
            DropIndex("dbo.Car", new[] { "ModeId" });
            DropIndex("dbo.DriveTest", new[] { "IdMember" });
            DropIndex("dbo.DriveTest", new[] { "IdCar" });
            DropIndex("dbo.Order", new[] { "MemberId" });
            DropIndex("dbo.Post_Tags", new[] { "PostId" });
            DropIndex("dbo.Post", new[] { "AuthorId" });
            DropIndex("dbo.Per_relationship", new[] { "Id_per" });
            DropIndex("dbo.Per_relationship", new[] { "Id_admin" });
            DropIndex("dbo.AdminOrder", new[] { "AdminId" });
            DropIndex("dbo.AdminOrder", new[] { "OrderId" });
            DropTable("dbo.Tags");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Manuafature");
            DropTable("dbo.Mode");
            DropTable("dbo.ImageProduct");
            DropTable("dbo.ImageProductDetails");
            DropTable("dbo.Category");
            DropTable("dbo.Feature");
            DropTable("dbo.CarDetails");
            DropTable("dbo.Car");
            DropTable("dbo.DriveTest");
            DropTable("dbo.Member");
            DropTable("dbo.Order");
            DropTable("dbo.Post_Tags");
            DropTable("dbo.Post");
            DropTable("dbo.Permission");
            DropTable("dbo.Per_relationship");
            DropTable("dbo.Admin");
            DropTable("dbo.AdminOrder");
        }
    }
}
