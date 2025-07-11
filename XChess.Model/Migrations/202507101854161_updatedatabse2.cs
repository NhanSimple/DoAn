namespace XChess.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabse2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 255));
            CreateIndex("dbo.Users", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            AlterColumn("dbo.Users", "Email", c => c.String());
        }
    }
}
