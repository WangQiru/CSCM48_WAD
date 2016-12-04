namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LecturerNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LectureNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LectureNo");
        }
    }
}
