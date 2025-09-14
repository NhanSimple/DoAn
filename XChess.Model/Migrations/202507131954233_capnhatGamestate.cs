namespace XChess.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class capnhatGamestate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChessMatches", "InitialTime", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChessMatches", "InitialTime", c => c.Time(nullable: false, precision: 7));
        }
    }
}
