namespace RMS.DAL.RMSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        CourseTitle = c.String(),
                        Credits = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentId = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollmentId)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegistrationNumber = c.String(),
                        Session = c.String(),
                        ClassRoll = c.Int(nullable: false),
                        ExamRoll = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Designation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        InstructorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.InstructorID })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Instructor", t => t.InstructorID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.InstructorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Enrollment", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropIndex("dbo.CourseInstructor", new[] { "InstructorID" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseID" });
            DropIndex("dbo.Enrollment", new[] { "StudentId" });
            DropIndex("dbo.Enrollment", new[] { "CourseID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.Instructor");
            DropTable("dbo.Student");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
        }
    }
}
