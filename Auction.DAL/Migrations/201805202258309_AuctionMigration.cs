namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bids", "Id", "dbo.Lots");
            AddForeignKey("dbo.Bids", "Id", "dbo.Lots", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Id", "dbo.Lots");
            AddForeignKey("dbo.Bids", "Id", "dbo.Lots", "Id", cascadeDelete: true);
        }
    }
}
