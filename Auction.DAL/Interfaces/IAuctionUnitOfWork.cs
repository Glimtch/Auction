using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Interfaces
{
    public interface IAuctionUnitOfWork : IDisposable
    {
        IRepository<Bid> Bids { get; }
        IRepository<Lot> Lots { get; }
        Task SaveAsync();
    }
}
