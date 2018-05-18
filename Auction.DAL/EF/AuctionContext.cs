using Auction.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.EF
{
    public class AuctionContext : IdentityDbContext<AuctionUser>
    {
        public AuctionContext() : base("DefaultConnection") { }

        public AuctionContext(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AuctionUserConfig());
            modelBuilder.Configurations.Add(new LotConfig());
            modelBuilder.Configurations.Add(new BidConfig());
        }
        
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}
