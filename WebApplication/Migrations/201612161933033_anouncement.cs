namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anouncement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnouncementModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        publishTime = c.DateTime(nullable: false),
                        LectureNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnouncementModels");
        }
    }
}
