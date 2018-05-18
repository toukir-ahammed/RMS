namespace RMS.DAL.RMSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            AddColumn("dbo.Student", "DepartmentId", c => c.Int());
            AddColumn("dbo.Instructor", "DepartmentId", c => c.Int());
            AlterColumn("dbo.Course", "DepartmentID", c => c.Int());
            CreateIndex("dbo.Course", "DepartmentID");
            CreateIndex("dbo.Student", "DepartmentId");
            CreateIndex("dbo.Instructor", "DepartmentId");
            AddForeignKey("dbo.Student", "DepartmentId", "dbo.Department", "DepartmentID");
            AddForeignKey("dbo.Instructor", "DepartmentId", "dbo.Department", "DepartmentID");
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Instructor", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Student", "DepartmentId", "dbo.Department");
            DropIndex("dbo.Instructor", new[] { "DepartmentId" });
            DropIndex("dbo.Student", new[] { "DepartmentId" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            AlterColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false));
            DropColumn("dbo.Instructor", "DepartmentId");
            DropColumn("dbo.Student", "DepartmentId");
            CreateIndex("dbo.Course", "DepartmentID");
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
        }
    }
}
