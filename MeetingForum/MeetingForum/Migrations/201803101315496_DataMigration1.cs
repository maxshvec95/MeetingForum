namespace MeetingForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Articles", "Titile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Titile", c => c.String(nullable: false));
            DropColumn("dbo.Articles", "Title");
        }
    }
}
