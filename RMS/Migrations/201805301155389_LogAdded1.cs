namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogAdded1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Instructor_ID", c => c.Int());
            CreateIndex("dbo.Logs", "Instructor_ID");
            AddForeignKey("dbo.Logs", "Instructor_ID", "dbo.Instructors", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "Instructor_ID", "dbo.Instructors");
            DropIndex("dbo.Logs", new[] { "Instructor_ID" });
            DropColumn("dbo.Logs", "Instructor_ID");
        }
    }
}
