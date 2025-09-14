namespace XChess.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class capnhatDatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChessMatches", "StartedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ChessMatches", "FinishedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ChessMatches", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ChessMatches", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ChessMatches", "UpdatedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ChessMatches", "DeletedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ChessMatches", "UpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchPlayers", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchPlayers", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchPlayers", "UpdatedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchPlayers", "DeletedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchPlayers", "UpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "UpdatedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "DeletedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "UpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchResults", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchResults", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchResults", "UpdatedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchResults", "DeletedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MatchResults", "UpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Moves", "MoveTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Moves", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Moves", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Moves", "UpdatedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Moves", "DeletedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Moves", "UpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roles", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roles", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roles", "UpdatedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roles", "DeletedAt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roles", "UpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Roles", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.Roles", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Roles", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Roles", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Moves", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Moves", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.Moves", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Moves", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Moves", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Moves", "MoveTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MatchResults", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MatchResults", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.MatchResults", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.MatchResults", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MatchResults", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.Users", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MatchPlayers", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MatchPlayers", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.MatchPlayers", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.MatchPlayers", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MatchPlayers", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ChessMatches", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ChessMatches", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.ChessMatches", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.ChessMatches", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ChessMatches", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ChessMatches", "FinishedAt", c => c.DateTime());
            AlterColumn("dbo.ChessMatches", "StartedAt", c => c.DateTime(nullable: false));
        }
    }
}
