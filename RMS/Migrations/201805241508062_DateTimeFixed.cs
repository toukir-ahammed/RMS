namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeFixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "CEDeadline", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Assignments", "FinalDeadLine", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignments", "FinalDeadLine");
            DropColumn("dbo.Assignments", "CEDeadline");
        }
    }
}
