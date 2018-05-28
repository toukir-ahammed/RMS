namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Semester_Year : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "Semester", c => c.Int(nullable: false));
            AddColumn("dbo.Assignments", "CalenderYear", c => c.Int(nullable: false));
            AddColumn("dbo.Enrollments", "Semester", c => c.Int(nullable: false));
            AddColumn("dbo.Enrollments", "CalenderYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enrollments", "CalenderYear");
            DropColumn("dbo.Enrollments", "Semester");
            DropColumn("dbo.Assignments", "CalenderYear");
            DropColumn("dbo.Assignments", "Semester");
        }
    }
}
