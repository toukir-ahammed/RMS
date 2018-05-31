namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogAdded2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "Instructor_ID", "dbo.Instructors");
            DropIndex("dbo.Logs", new[] { "Instructor_ID" });
            AddColumn("dbo.Logs", "AssignmentID", c => c.Int());
            CreateIndex("dbo.Logs", "AssignmentID");
            AddForeignKey("dbo.Logs", "AssignmentID", "dbo.Assignments", "AssignmentID");
            DropColumn("dbo.Logs", "InstrucorID");
            DropColumn("dbo.Logs", "Instructor_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "Instructor_ID", c => c.Int());
            AddColumn("dbo.Logs", "InstrucorID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Logs", "AssignmentID", "dbo.Assignments");
            DropIndex("dbo.Logs", new[] { "AssignmentID" });
            DropColumn("dbo.Logs", "AssignmentID");
            CreateIndex("dbo.Logs", "Instructor_ID");
            AddForeignKey("dbo.Logs", "Instructor_ID", "dbo.Instructors", "ID");
        }
    }
}
