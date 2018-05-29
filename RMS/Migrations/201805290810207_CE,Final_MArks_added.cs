namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CEFinal_MArks_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "CETotal", c => c.Double(nullable: false));
            AddColumn("dbo.Assignments", "FinalExamTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignments", "FinalExamTotal");
            DropColumn("dbo.Assignments", "CETotal");
        }
    }
}
