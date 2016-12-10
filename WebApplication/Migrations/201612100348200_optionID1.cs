namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class optionID1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResponseModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ResponseModels", new[] { "UserId" });
            DropPrimaryKey("dbo.ResponseModels");
            AlterColumn("dbo.ResponseModels", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.ResponseModels", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ResponseModels", "ID");
            CreateIndex("dbo.ResponseModels", "UserId");
            AddForeignKey("dbo.ResponseModels", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResponseModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ResponseModels", new[] { "UserId" });
            DropPrimaryKey("dbo.ResponseModels");
            AlterColumn("dbo.ResponseModels", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ResponseModels", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ResponseModels", new[] { "QuestionID", "UserId" });
            CreateIndex("dbo.ResponseModels", "UserId");
            AddForeignKey("dbo.ResponseModels", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
