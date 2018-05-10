namespace RMS.DAL.RMSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "Name", c => c.String(maxLength: 50));
        }
    }
}
