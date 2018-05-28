namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PromotionAndTranscript : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "LeastCGPAToPass", c => c.Double(nullable: false));
            AddColumn("dbo.Departments", "LeastGPToPass", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "LeastGPToPass");
            DropColumn("dbo.Departments", "LeastCGPAToPass");
        }
    }
}
