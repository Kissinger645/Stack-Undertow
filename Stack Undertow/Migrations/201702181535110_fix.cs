namespace Stack_Undertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Points", name: "PointName", newName: "PointId");
            RenameIndex(table: "dbo.Points", name: "IX_PointName", newName: "IX_PointId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Points", name: "IX_PointId", newName: "IX_PointName");
            RenameColumn(table: "dbo.Points", name: "PointId", newName: "PointName");
        }
    }
}
