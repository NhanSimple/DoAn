namespace XChess.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirtName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Users", "IdGame");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IdGame", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirtName");
        }
    }
}
