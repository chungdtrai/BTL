namespace BTL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderv4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Code", c => c.String(nullable: false));
        }
    }
}
