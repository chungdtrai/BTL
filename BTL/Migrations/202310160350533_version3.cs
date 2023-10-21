namespace BTL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCategories", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategories", "Description", c => c.String());
        }
    }
}
