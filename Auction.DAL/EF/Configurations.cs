using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.EF
{
    class AuctionUserConfig : EntityTypeConfiguration<AuctionUser>
    {
        public AuctionUserConfig()
        {
            HasMany(u => u.ActiveLots).WithRequired(l => l.Seller);
            HasMany(u => u.ActiveBids).WithRequired(b => b.Bidder);
            Property(u => u.Nickname).IsRequired();
            Property(u => u.CreditCardNumber).IsRequired();
        }
    }

    class LotConfig : EntityTypeConfiguration<Lot>
    {
        public LotConfig()
        {
            Property(l => l.Name).IsRequired();
            Property(l => l.Image).HasColumnType("varbinary");
            Property(l => l.StartPrice).IsRequired().HasColumnType("money");
            Property(l => l.ExpireDate).IsRequired();
            Ignore(l => l.IsSold);
        }
    }

    class BidConfig : EntityTypeConfiguration<Bid>
    {
        public BidConfig()
        {
            Property(b => b.BidPrice).IsRequired().HasColumnType("money");
            HasRequired(b => b.Lot).WithOptional(l => l.CurrentBid);
        }
    }
}
