namespace XChess.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        Result = c.String(),
                        WhitePlayerId = c.Int(nullable: false),
                        BlackPlayerId = c.Int(nullable: false),
                        IdWhitePlayer = c.Int(nullable: false),
                        IdBlackPlayer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Moves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        MoveNumber = c.Int(nullable: false),
                        Notation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        IdGame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Moves", "GameId", "dbo.Games");
            DropIndex("dbo.Moves", new[] { "GameId" });
            DropTable("dbo.Users");
            DropTable("dbo.Moves");
            DropTable("dbo.Games");
        }
    }
}
