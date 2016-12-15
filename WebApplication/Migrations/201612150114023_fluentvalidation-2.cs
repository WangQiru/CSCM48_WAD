namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fluentvalidation2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MCQModels", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.MCQModels", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MCQModels", "Description", c => c.String());
            AlterColumn("dbo.MCQModels", "Title", c => c.String());
        }
    }
}
