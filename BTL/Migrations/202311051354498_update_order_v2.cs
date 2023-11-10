namespace BTL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_order_v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CreateDate");
        }
    }
}
