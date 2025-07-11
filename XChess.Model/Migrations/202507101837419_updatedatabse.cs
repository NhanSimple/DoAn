namespace XChess.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
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
            
            AddColumn("dbo.MatchPlayers", "User_Id", c => c.Long());
            AddColumn("dbo.MatchPlayers", "ChessMatch_Id", c => c.Long());
            AddColumn("dbo.MatchResults", "ChessMatch_Id", c => c.Long());
            CreateIndex("dbo.MatchPlayers", "MatchId");
            CreateIndex("dbo.MatchPlayers", "UserId");
            CreateIndex("dbo.MatchPlayers", "User_Id");
            CreateIndex("dbo.MatchPlayers", "ChessMatch_Id");
            CreateIndex("dbo.MatchResults", "UserId");
            CreateIndex("dbo.MatchResults", "ChessMatch_Id");
            CreateIndex("dbo.Moves", "MatchId");
            CreateIndex("dbo.Moves", "UserId");
            AddForeignKey("dbo.MatchPlayers", "MatchId", "dbo.ChessMatches", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MatchPlayers", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.MatchResults", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Moves", "MatchId", "dbo.ChessMatches", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Moves", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.MatchPlayers", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.MatchPlayers", "ChessMatch_Id", "dbo.ChessMatches", "Id");
            AddForeignKey("dbo.MatchResults", "ChessMatch_Id", "dbo.ChessMatches", "Id");
            DropColumn("dbo.Users", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role", c => c.String());
            DropForeignKey("dbo.MatchResults", "ChessMatch_Id", "dbo.ChessMatches");
            DropForeignKey("dbo.MatchPlayers", "ChessMatch_Id", "dbo.ChessMatches");
            DropForeignKey("dbo.MatchPlayers", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Moves", "UserId", "dbo.Users");
            DropForeignKey("dbo.Moves", "MatchId", "dbo.ChessMatches");
            DropForeignKey("dbo.MatchResults", "UserId", "dbo.Users");
            DropForeignKey("dbo.MatchPlayers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MatchPlayers", "MatchId", "dbo.ChessMatches");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Moves", new[] { "UserId" });
            DropIndex("dbo.Moves", new[] { "MatchId" });
            DropIndex("dbo.MatchResults", new[] { "ChessMatch_Id" });
            DropIndex("dbo.MatchResults", new[] { "UserId" });
            DropIndex("dbo.MatchPlayers", new[] { "ChessMatch_Id" });
            DropIndex("dbo.MatchPlayers", new[] { "User_Id" });
            DropIndex("dbo.MatchPlayers", new[] { "UserId" });
            DropIndex("dbo.MatchPlayers", new[] { "MatchId" });
            DropColumn("dbo.MatchResults", "ChessMatch_Id");
            DropColumn("dbo.MatchPlayers", "ChessMatch_Id");
            DropColumn("dbo.MatchPlayers", "User_Id");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
        }
    }
}
