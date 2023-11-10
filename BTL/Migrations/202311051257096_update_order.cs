namespace BTL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TypePayment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TypePayment");
        }
    }
}
