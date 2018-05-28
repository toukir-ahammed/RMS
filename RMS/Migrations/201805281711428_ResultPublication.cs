namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultPublication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResultPublications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepartmentID = c.Int(nullable: false),
                        Semester = c.Int(nullable: false),
                        CalenderYear = c.Int(nullable: false),
                        PublicationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            AddColumn("dbo.Enrollments", "DepartmentID", c => c.Int());
            AddColumn("dbo.Enrollments", "Submitted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Enrollments", "DepartmentID");
            AddForeignKey("dbo.Enrollments", "DepartmentID", "dbo.Departments", "DepartmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultPublications", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Enrollments", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.ResultPublications", new[] { "DepartmentID" });
            DropIndex("dbo.Enrollments", new[] { "DepartmentID" });
            DropColumn("dbo.Enrollments", "Submitted");
            DropColumn("dbo.Enrollments", "DepartmentID");
            DropTable("dbo.ResultPublications");
        }
    }
}
