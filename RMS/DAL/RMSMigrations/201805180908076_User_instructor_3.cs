namespace RMS.DAL.RMSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_instructor_3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CourseInstructor", newName: "InstructorCourse");
            RenameColumn(table: "dbo.InstructorCourse", name: "CourseID", newName: "Course_CourseID");
            RenameColumn(table: "dbo.InstructorCourse", name: "InstructorID", newName: "Instructor_ID");
            RenameIndex(table: "dbo.InstructorCourse", name: "IX_InstructorID", newName: "IX_Instructor_ID");
            RenameIndex(table: "dbo.InstructorCourse", name: "IX_CourseID", newName: "IX_Course_CourseID");
            DropPrimaryKey("dbo.InstructorCourse");
            AddPrimaryKey("dbo.InstructorCourse", new[] { "Instructor_ID", "Course_CourseID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.InstructorCourse");
            AddPrimaryKey("dbo.InstructorCourse", new[] { "CourseID", "InstructorID" });
            RenameIndex(table: "dbo.InstructorCourse", name: "IX_Course_CourseID", newName: "IX_CourseID");
            RenameIndex(table: "dbo.InstructorCourse", name: "IX_Instructor_ID", newName: "IX_InstructorID");
            RenameColumn(table: "dbo.InstructorCourse", name: "Instructor_ID", newName: "InstructorID");
            RenameColumn(table: "dbo.InstructorCourse", name: "Course_CourseID", newName: "CourseID");
            RenameTable(name: "dbo.InstructorCourse", newName: "CourseInstructor");
        }
    }
}
