namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SemesterAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Semester", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Semester");
        }
    }
}
