namespace Stack_Undertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userpoints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Points = c.Int(nullable: false),
                        PointName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PointName)
                .Index(t => t.PointName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points", "PointName", "dbo.AspNetUsers");
            DropIndex("dbo.Points", new[] { "PointName" });
            DropTable("dbo.Points");
        }
    }
}
