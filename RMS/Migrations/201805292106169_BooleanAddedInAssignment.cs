namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BooleanAddedInAssignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "CESubmitted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Assignments", "FinalSubmitted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Assignments", "Submitted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "Submitted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Assignments", "FinalSubmitted");
            DropColumn("dbo.Assignments", "CESubmitted");
        }
    }
}
