namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fluentvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MCQModels", "Title", c => c.String());
            AlterColumn("dbo.MCQModels", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MCQModels", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.MCQModels", "Title", c => c.String(nullable: false));
        }
    }
}
