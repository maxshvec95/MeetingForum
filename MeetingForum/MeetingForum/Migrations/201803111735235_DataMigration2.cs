namespace MeetingForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Accepted", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Accepted");
        }
    }
}
