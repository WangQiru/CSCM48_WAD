namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class optionstextnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OptionModels", "Text", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OptionModels", "Text", c => c.String(nullable: false));
        }
    }
}
