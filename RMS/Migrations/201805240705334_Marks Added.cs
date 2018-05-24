namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarksAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "MarksheetFileName", c => c.String());
            AddColumn("dbo.Enrollments", "CEMark", c => c.Double(nullable: false));
            AddColumn("dbo.Enrollments", "FinalMark", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enrollments", "FinalMark");
            DropColumn("dbo.Enrollments", "CEMark");
            DropColumn("dbo.Assignments", "MarksheetFileName");
        }
    }
}
