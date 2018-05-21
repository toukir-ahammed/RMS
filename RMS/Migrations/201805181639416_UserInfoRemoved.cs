namespace RMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInfoRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "MyUserInfo_Id", "dbo.MyUserInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "MyUserInfo_Id" });
            DropColumn("dbo.AspNetUsers", "MyUserInfo_Id");
            DropTable("dbo.MyUserInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MyUserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "MyUserInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "MyUserInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "MyUserInfo_Id", "dbo.MyUserInfoes", "Id");
        }
    }
}
