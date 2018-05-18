namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AuctionProfiles", "Id", "dbo.AuctionUsers");
            DropIndex("dbo.AuctionProfiles", new[] { "Id" });
            AddColumn("dbo.AuctionUsers", "CreditCardNumber", c => c.String(nullable: false));
            DropColumn("dbo.AuctionUsers", "AuctionProfileId");
            DropTable("dbo.AuctionProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AuctionProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nickname = c.String(nullable: false),
                        CreditCardNumber = c.String(nullable: false),
                        AuctionUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AuctionUsers", "AuctionProfileId", c => c.String());
            DropColumn("dbo.AuctionUsers", "CreditCardNumber");
            CreateIndex("dbo.AuctionProfiles", "Id");
            AddForeignKey("dbo.AuctionProfiles", "Id", "dbo.AuctionUsers", "Id");
        }
    }
}
