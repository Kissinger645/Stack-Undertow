namespace Stack_Undertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "QuestionerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Questions", "QuestionerId");
            AddForeignKey("dbo.Questions", "QuestionerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuestionerId", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "QuestionerId" });
            DropColumn("dbo.Questions", "QuestionerId");
        }
    }
}
