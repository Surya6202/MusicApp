namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicUserApp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Musics",
                c => new
                    {
                        MusicId = c.Int(nullable: false, identity: true),
                        MusicName = c.String(),
                        PublishedYear = c.Int(nullable: false),
                        ReleasedDate = c.DateTime(),
                        IsPublished = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MusicId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Musics");
        }
    }
}
