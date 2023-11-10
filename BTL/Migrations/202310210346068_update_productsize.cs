namespace BTL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_productsize : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductSizes", "Size_Id", "dbo.Sizes");
            DropIndex("dbo.ProductSizes", new[] { "Size_Id" });
            RenameColumn(table: "dbo.ProductSizes", name: "Size_Id", newName: "SizeId");
            AlterColumn("dbo.ProductSizes", "SizeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductSizes", "SizeId");
            AddForeignKey("dbo.ProductSizes", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
            DropColumn("dbo.ProductSizes", "SideId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductSizes", "SideId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductSizes", "SizeId", "dbo.Sizes");
            DropIndex("dbo.ProductSizes", new[] { "SizeId" });
            AlterColumn("dbo.ProductSizes", "SizeId", c => c.Int());
            RenameColumn(table: "dbo.ProductSizes", name: "SizeId", newName: "Size_Id");
            CreateIndex("dbo.ProductSizes", "Size_Id");
            AddForeignKey("dbo.ProductSizes", "Size_Id", "dbo.Sizes", "Id");
        }
    }
}
