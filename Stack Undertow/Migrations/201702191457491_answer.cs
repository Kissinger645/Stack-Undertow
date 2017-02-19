namespace Stack_Undertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        AnswerText = c.String(),
                        Best = c.Boolean(nullable: false),
                        Answerer = c.String(maxLength: 128),
                        QId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Answerer)
                .ForeignKey("dbo.Questions", t => t.QId, cascadeDelete: true)
                .Index(t => t.Answerer)
                .Index(t => t.QId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "Answerer", "dbo.AspNetUsers");
            DropIndex("dbo.Answers", new[] { "QId" });
            DropIndex("dbo.Answers", new[] { "Answerer" });
            DropTable("dbo.Answers");
        }
    }
}
