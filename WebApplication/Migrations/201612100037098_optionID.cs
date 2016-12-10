namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class optionID : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ResponseModels", name: "Option", newName: "OptionID");
            RenameIndex(table: "dbo.ResponseModels", name: "IX_Option", newName: "IX_OptionID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ResponseModels", name: "IX_OptionID", newName: "IX_Option");
            RenameColumn(table: "dbo.ResponseModels", name: "OptionID", newName: "Option");
        }
    }
}
