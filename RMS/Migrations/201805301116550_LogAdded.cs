namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        InstrucorID = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        LogMessage = c.String(),
                    })
                .PrimaryKey(t => t.LogID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
