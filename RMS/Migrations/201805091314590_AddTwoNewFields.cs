namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTwoNewFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "FirstName", c => c.String());
            AlterColumn("dbo.UserInfoes", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "FirstName", c => c.String(nullable: false));
        }
    }
}
