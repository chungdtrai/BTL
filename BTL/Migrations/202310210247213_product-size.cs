namespace BTL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productsize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SideId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Size_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.Size_Id)
                .Index(t => t.ProductId)
                .Index(t => t.Size_Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SizeName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSizes", "Size_Id", "dbo.Sizes");
            DropForeignKey("dbo.ProductSizes", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductSizes", new[] { "Size_Id" });
            DropIndex("dbo.ProductSizes", new[] { "ProductId" });
            DropTable("dbo.Sizes");
            DropTable("dbo.ProductSizes");
        }
    }
}
