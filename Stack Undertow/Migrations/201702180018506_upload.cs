namespace Stack_Undertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageUploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        File = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageUploads");
        }
    }
}
