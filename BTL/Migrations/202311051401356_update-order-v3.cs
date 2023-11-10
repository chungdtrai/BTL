namespace BTL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderv3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
