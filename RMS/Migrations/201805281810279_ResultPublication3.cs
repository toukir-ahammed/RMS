namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultPublication3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollments", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Enrollments", new[] { "DepartmentID" });
            AddColumn("dbo.Assignments", "DepartmentID", c => c.Int());
            CreateIndex("dbo.Assignments", "DepartmentID");
            AddForeignKey("dbo.Assignments", "DepartmentID", "dbo.Departments", "DepartmentID");
            DropColumn("dbo.Enrollments", "DepartmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrollments", "DepartmentID", c => c.Int());
            DropForeignKey("dbo.Assignments", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Assignments", new[] { "DepartmentID" });
            DropColumn("dbo.Assignments", "DepartmentID");
            CreateIndex("dbo.Enrollments", "DepartmentID");
            AddForeignKey("dbo.Enrollments", "DepartmentID", "dbo.Departments", "DepartmentID");
        }
    }
}
