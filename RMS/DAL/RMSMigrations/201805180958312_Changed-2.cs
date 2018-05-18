namespace RMS.DAL.RMSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Student", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Enrollment", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Instructor", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructor");
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            DropIndex("dbo.Enrollment", new[] { "CourseID" });
            DropIndex("dbo.Enrollment", new[] { "StudentId" });
            DropIndex("dbo.Student", new[] { "DepartmentId" });
            DropIndex("dbo.Instructor", new[] { "DepartmentId" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseID" });
            DropIndex("dbo.CourseInstructor", new[] { "InstructorID" });
            DropTable("dbo.Course");
            DropTable("dbo.Department");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Student");
            DropTable("dbo.Instructor");
            DropTable("dbo.CourseInstructor");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        InstructorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.InstructorID });
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Designation = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RegistrationNumber = c.String(nullable: false, maxLength: 50),
                        Session = c.String(nullable: false, maxLength: 50),
                        ClassRoll = c.Int(nullable: false),
                        ExamRoll = c.Int(nullable: false),
                        DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentId = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollmentId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Credits = c.Int(nullable: false),
                        DepartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateIndex("dbo.CourseInstructor", "InstructorID");
            CreateIndex("dbo.CourseInstructor", "CourseID");
            CreateIndex("dbo.Instructor", "DepartmentId");
            CreateIndex("dbo.Student", "DepartmentId");
            CreateIndex("dbo.Enrollment", "StudentId");
            CreateIndex("dbo.Enrollment", "CourseID");
            CreateIndex("dbo.Course", "DepartmentID");
            AddForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructor", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
            AddForeignKey("dbo.Instructor", "DepartmentId", "dbo.Department", "DepartmentID");
            AddForeignKey("dbo.Enrollment", "StudentId", "dbo.Student", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Student", "DepartmentId", "dbo.Department", "DepartmentID");
            AddForeignKey("dbo.Enrollment", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID");
        }
    }
}
