namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AuctionUsers", new[] { "Id" });
            CreateIndex("dbo.AuctionProfiles", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AuctionProfiles", new[] { "Id" });
            CreateIndex("dbo.AuctionUsers", "Id");
        }
    }
}
