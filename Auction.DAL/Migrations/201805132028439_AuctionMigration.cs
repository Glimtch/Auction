namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuctionMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "AuctionUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AuctionProfiles", new[] { "Id" });
            DropIndex("dbo.AuctionUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        LotId = c.Int(nullable: false),
                        BidderId = c.String(nullable: false, maxLength: 128),
                        BidPrice = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lots", t => t.Id)
                .ForeignKey("dbo.AuctionUsers", t => t.BidderId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.BidderId);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        StartPrice = c.Decimal(nullable: false, storeType: "money"),
                        SellerId = c.String(nullable: false, maxLength: 128),
                        ExpireDate = c.DateTime(nullable: false),
                        BidId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuctionUsers", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.SellerId);
            
            AddColumn("dbo.AuctionProfiles", "Nickname", c => c.String(nullable: false));
            AddColumn("dbo.AuctionProfiles", "AuctionUserId", c => c.String());
            AddColumn("dbo.AuctionUsers", "AuctionProfileId", c => c.String());
            AddColumn("dbo.AspNetUserClaims", "AuctionUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserLogins", "AuctionUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserRoles", "AuctionUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.AuctionProfiles", "CreditCardNumber", c => c.String(nullable: false));
            AlterColumn("dbo.AuctionUsers", "Email", c => c.String());
            AlterColumn("dbo.AuctionUsers", "UserName", c => c.String());
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String());
            CreateIndex("dbo.AuctionUsers", "Id");
            CreateIndex("dbo.AspNetUserClaims", "AuctionUser_Id");
            CreateIndex("dbo.AspNetUserLogins", "AuctionUser_Id");
            CreateIndex("dbo.AspNetUserRoles", "AuctionUser_Id");
            AddForeignKey("dbo.AspNetUserClaims", "AuctionUser_Id", "dbo.AuctionUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "AuctionUser_Id", "dbo.AuctionUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "AuctionUser_Id", "dbo.AuctionUsers", "Id");
            DropColumn("dbo.AuctionProfiles", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuctionProfiles", "Name", c => c.String());
            DropForeignKey("dbo.AspNetUserRoles", "AuctionUser_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.AspNetUserLogins", "AuctionUser_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.AspNetUserClaims", "AuctionUser_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.Lots", "SellerId", "dbo.AuctionUsers");
            DropForeignKey("dbo.Bids", "BidderId", "dbo.AuctionUsers");
            DropForeignKey("dbo.Bids", "Id", "dbo.Lots");
            DropIndex("dbo.AspNetUserRoles", new[] { "AuctionUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "AuctionUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "AuctionUser_Id" });
            DropIndex("dbo.Lots", new[] { "SellerId" });
            DropIndex("dbo.Bids", new[] { "BidderId" });
            DropIndex("dbo.Bids", new[] { "Id" });
            DropIndex("dbo.AuctionUsers", new[] { "Id" });
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AuctionUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.AuctionUsers", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.AuctionProfiles", "CreditCardNumber", c => c.String());
            DropColumn("dbo.AspNetUserRoles", "AuctionUser_Id");
            DropColumn("dbo.AspNetUserLogins", "AuctionUser_Id");
            DropColumn("dbo.AspNetUserClaims", "AuctionUser_Id");
            DropColumn("dbo.AuctionUsers", "AuctionProfileId");
            DropColumn("dbo.AuctionProfiles", "AuctionUserId");
            DropColumn("dbo.AuctionProfiles", "Nickname");
            DropTable("dbo.Lots");
            DropTable("dbo.Bids");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AuctionUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.AuctionProfiles", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.AuctionUsers", newName: "AspNetUsers");
        }
    }
}
