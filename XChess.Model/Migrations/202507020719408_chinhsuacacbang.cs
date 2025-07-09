namespace XChess.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chinhsuacacbang : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Moves", "GameId", "dbo.Games");
            DropIndex("dbo.Moves", new[] { "GameId" });
            DropPrimaryKey("dbo.Moves");
            DropPrimaryKey("dbo.Users");
            CreateTable(
                "dbo.MatchPlayers",
                c => new
                    {
                        MatchId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        PlayerColor = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedID = c.Long(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedID = c.Long(),
                    })
                .PrimaryKey(t => new { t.MatchId, t.UserId });
            
            CreateTable(
                "dbo.MatchResults",
                c => new
                    {
                        MatchId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        GameResult = c.Int(nullable: false),
                        Note = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedID = c.Long(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedID = c.Long(),
                    })
                .PrimaryKey(t => new { t.MatchId, t.UserId });
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StartedAt = c.DateTime(nullable: false),
                        FinishedAt = c.DateTime(),
                        GameMode = c.Int(nullable: false),
                        InitialTime = c.Time(nullable: false, precision: 7),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedID = c.Long(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedID = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Moves", "MatchId", c => c.Long(nullable: false));
            AddColumn("dbo.Moves", "userId", c => c.Long());
            AddColumn("dbo.Moves", "From", c => c.String());
            AddColumn("dbo.Moves", "To", c => c.String());
            AddColumn("dbo.Moves", "Piece", c => c.String());
            AddColumn("dbo.Moves", "MoveTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Moves", "FenAfter", c => c.String());
            AddColumn("dbo.Moves", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Moves", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Moves", "CreatedID", c => c.Long());
            AddColumn("dbo.Moves", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Moves", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Moves", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Moves", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Moves", "UpdatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Moves", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Moves", "UpdatedID", c => c.Long());
            AddColumn("dbo.Users", "Role", c => c.String());
            AddColumn("dbo.Users", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Users", "CreatedID", c => c.Long());
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Users", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "UpdatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Users", "UpdatedID", c => c.Long());
            AlterColumn("dbo.Moves", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Moves", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            DropColumn("dbo.Moves", "GameId");
            DropColumn("dbo.Moves", "Notation");
            DropColumn("dbo.Users", "FirtName");
            DropColumn("dbo.Users", "LastName");
            DropTable("dbo.Games");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "FirtName", c => c.String());
            AddColumn("dbo.Moves", "Notation", c => c.String());
            AddColumn("dbo.Moves", "GameId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Moves");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Moves", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Users", "UpdatedID");
            DropColumn("dbo.Users", "UpdatedBy");
            DropColumn("dbo.Users", "UpdatedDate");
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Users", "DeletedAt");
            DropColumn("dbo.Users", "UpdatedAt");
            DropColumn("dbo.Users", "CreatedID");
            DropColumn("dbo.Users", "CreatedBy");
            DropColumn("dbo.Users", "CreatedDate");
            DropColumn("dbo.Users", "Role");
            DropColumn("dbo.Moves", "UpdatedID");
            DropColumn("dbo.Moves", "UpdatedBy");
            DropColumn("dbo.Moves", "UpdatedDate");
            DropColumn("dbo.Moves", "IsDeleted");
            DropColumn("dbo.Moves", "DeletedAt");
            DropColumn("dbo.Moves", "UpdatedAt");
            DropColumn("dbo.Moves", "CreatedAt");
            DropColumn("dbo.Moves", "CreatedID");
            DropColumn("dbo.Moves", "CreatedBy");
            DropColumn("dbo.Moves", "CreatedDate");
            DropColumn("dbo.Moves", "FenAfter");
            DropColumn("dbo.Moves", "MoveTime");
            DropColumn("dbo.Moves", "Piece");
            DropColumn("dbo.Moves", "To");
            DropColumn("dbo.Moves", "From");
            DropColumn("dbo.Moves", "userId");
            DropColumn("dbo.Moves", "MatchId");
            DropTable("dbo.Matches");
            DropTable("dbo.MatchResults");
            DropTable("dbo.MatchPlayers");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Moves", "Id");
            CreateIndex("dbo.Moves", "GameId");
            AddForeignKey("dbo.Moves", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
    }
}
