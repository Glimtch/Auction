using Auction.DAL.EF;
using Auction.DAL.Entities;
using Auction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class AuctionUnitOfWork : IAuctionUnitOfWork
    {
        AuctionContext db;
        BidRepository bidRepository;
        LotRepository lotRepository;

        public AuctionUnitOfWork(string connectionString)
        {
            db = new AuctionContext(connectionString);
        }

        public IRepository<Bid> Bids
        {
            get
            {
                if (bidRepository == null)
                    bidRepository = new BidRepository(db);
                return bidRepository;
            }
        }

        public IRepository<Lot> Lots
        {
            get
            {
                if (lotRepository == null)
                    lotRepository = new LotRepository(db);
                return lotRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
