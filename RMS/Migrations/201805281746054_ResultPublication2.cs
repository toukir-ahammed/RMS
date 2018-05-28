namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultPublication2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "Submitted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Enrollments", "Submitted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrollments", "Submitted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Assignments", "Submitted");
        }
    }
}
