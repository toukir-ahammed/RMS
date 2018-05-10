namespace RMS.DAL.RMSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeDataValidationsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Course", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.Department", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "RegistrationNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "Session", c => c.String(maxLength: 50));
            AlterColumn("dbo.Instructor", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Instructor", "Designation", c => c.String(maxLength: 50));
            DropColumn("dbo.Course", "CourseTitle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Course", "CourseTitle", c => c.String());
            AlterColumn("dbo.Instructor", "Designation", c => c.String());
            AlterColumn("dbo.Instructor", "Name", c => c.String());
            AlterColumn("dbo.Student", "Session", c => c.String());
            AlterColumn("dbo.Student", "RegistrationNumber", c => c.String());
            AlterColumn("dbo.Student", "Name", c => c.String());
            AlterColumn("dbo.Department", "Name", c => c.String());
            DropColumn("dbo.Course", "Title");
        }
    }
}
