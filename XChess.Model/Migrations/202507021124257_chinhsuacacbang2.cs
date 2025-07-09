namespace XChess.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chinhsuacacbang2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Matches", newName: "ChessMatches");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ChessMatches", newName: "Matches");
        }
    }
}
