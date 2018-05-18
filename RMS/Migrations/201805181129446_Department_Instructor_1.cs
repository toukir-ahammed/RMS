namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Department_Instructor_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Instructor_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Instructor_ID");
            AddForeignKey("dbo.AspNetUsers", "Instructor_ID", "dbo.Instructors", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Instructor_ID", "dbo.Instructors");
            DropIndex("dbo.AspNetUsers", new[] { "Instructor_ID" });
            DropColumn("dbo.AspNetUsers", "Instructor_ID");
        }
    }
}
