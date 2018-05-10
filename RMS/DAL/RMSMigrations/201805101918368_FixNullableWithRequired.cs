namespace RMS.DAL.RMSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNullableWithRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Course", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Department", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "RegistrationNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "Session", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Instructor", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Instructor", "Designation", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructor", "Designation", c => c.String(maxLength: 50));
            AlterColumn("dbo.Instructor", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "Session", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "RegistrationNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.Department", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 50));
        }
    }
}
