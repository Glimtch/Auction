namespace Auction.DAL.Migrations
{
    using Auction.DAL.EF;
    using Auction.DAL.Entities;
    using Auction.DAL.Repositories.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AuctionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Auction.DAL.EF.AuctionContext";
        }

        protected override void Seed(AuctionContext context)
        {
            AuctionRoleManager roleManager = new AuctionRoleManager(new RoleStore<AuctionRole>(context));

            roleManager.Create(new AuctionRole() { Name = "ceo" });
            roleManager.Create(new AuctionRole() { Name = "admin" });
            roleManager.Create(new AuctionRole() { Name = "user" });

            AuctionUserManager userManager = new AuctionUserManager(new UserStore<AuctionUser>(context));

            var user1 = new AuctionUser() { Email = "dragon34@gmail.com", UserName = "dragon34@gmail.com", Nickname = "Dragon34", CreditCardNumber = "5168 8090 0000 0000" };
            userManager.Create(user1, "Boss(0)");
            userManager.AddToRole(user1.Id, "ceo");
            userManager.AddToRole(user1.Id, "admin");

            var user2 = new AuctionUser() { Email = "killer@gmail.com", UserName = "killer@gmail.com", Nickname = "Killer Sanya", CreditCardNumber = "5168 1254 1234 4321" };
            userManager.Create(user2, "Abc=123");
            userManager.AddToRole(user2.Id, "admin");

            var user3 = new AuctionUser() { Email = "naberlin@gmail.com", UserName = "naberlin@gmail.com", Nickname = "Thomas the Train", CreditCardNumber = "4790 7000 1488 1221" };
            userManager.Create(user3, "Choo-400");
            userManager.AddToRole(user3.Id, "user");

            context.SaveChanges();
        }
    }
}
