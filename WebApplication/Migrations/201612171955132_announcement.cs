namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class announcement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnnouncementModels", "LecturerNo", c => c.Int(nullable: false));
            AddColumn("dbo.AnnouncementModels", "LecturerName", c => c.String());
            DropColumn("dbo.AnnouncementModels", "LectureNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnnouncementModels", "LectureNo", c => c.Int(nullable: false));
            DropColumn("dbo.AnnouncementModels", "LecturerName");
            DropColumn("dbo.AnnouncementModels", "LecturerNo");
        }
    }
}
