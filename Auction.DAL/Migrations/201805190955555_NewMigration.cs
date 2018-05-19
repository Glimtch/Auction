namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BidderId = c.String(nullable: false, maxLength: 128),
                        BidPrice = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuctionUsers", t => t.BidderId, cascadeDelete: true)
                .ForeignKey("dbo.Lots", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.BidderId);
            
            CreateTable(
                "dbo.AuctionUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nickname = c.String(nullable: false),
                        CreditCardNumber = c.String(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Image = c.Binary(maxLength: 8000),
                        StartPrice = c.Decimal(nullable: false, storeType: "money"),
                        SellerId = c.String(nullable: false, maxLength: 128),
                        ExpireDate = c.DateTime(nullable: false),
                        WasBidOn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuctionUsers", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AuctionUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuctionUsers", t => t.AuctionUser_Id)
                .Index(t => t.AuctionUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        AuctionUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AuctionUsers", t => t.AuctionUser_Id)
                .Index(t => t.AuctionUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        AuctionUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AuctionUsers", t => t.AuctionUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.AuctionUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bids", "Id", "dbo.Lots");
            DropForeignKey("dbo.AspNetUserRoles", "AuctionUser_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.AspNetUserLogins", "AuctionUser_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.AspNetUserClaims", "AuctionUser_Id", "dbo.AuctionUsers");
            DropForeignKey("dbo.Lots", "SellerId", "dbo.AuctionUsers");
            DropForeignKey("dbo.Bids", "BidderId", "dbo.AuctionUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "AuctionUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "AuctionUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "AuctionUser_Id" });
            DropIndex("dbo.Lots", new[] { "SellerId" });
            DropIndex("dbo.Bids", new[] { "BidderId" });
            DropIndex("dbo.Bids", new[] { "Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Lots");
            DropTable("dbo.AuctionUsers");
            DropTable("dbo.Bids");
        }
    }
}
