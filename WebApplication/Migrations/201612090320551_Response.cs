namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Response : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResponseModels", "QuestionID", c => c.Int(nullable: false));
            CreateIndex("dbo.ResponseModels", "QuestionID");
            AddForeignKey("dbo.ResponseModels", "QuestionID", "dbo.OptionModels", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResponseModels", "QuestionID", "dbo.OptionModels");
            DropIndex("dbo.ResponseModels", new[] { "QuestionID" });
            DropColumn("dbo.ResponseModels", "QuestionID");
        }
    }
}
