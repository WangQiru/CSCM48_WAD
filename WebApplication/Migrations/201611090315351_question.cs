namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class question : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionModels", "MCQID", "dbo.MCQModels");
            DropIndex("dbo.QuestionModels", new[] { "MCQID" });
            AlterColumn("dbo.QuestionModels", "MCQID", c => c.Int());
            CreateIndex("dbo.QuestionModels", "MCQID");
            AddForeignKey("dbo.QuestionModels", "MCQID", "dbo.MCQModels", "ID");
        }
    }
}
