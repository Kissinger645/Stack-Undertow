namespace Stack_Undertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pointreason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Points", "Reason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Points", "Reason");
        }
    }
}
