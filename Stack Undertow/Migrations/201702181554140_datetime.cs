namespace Stack_Undertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Points", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Points", "Created");
        }
    }
}
