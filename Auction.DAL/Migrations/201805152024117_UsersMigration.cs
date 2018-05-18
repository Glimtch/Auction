namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuctionUsers", "Nickname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuctionUsers", "Nickname");
        }
    }
}
