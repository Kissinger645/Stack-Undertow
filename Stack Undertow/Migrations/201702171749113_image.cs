namespace Stack_Undertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Poster", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Poster");
        }
    }
}
