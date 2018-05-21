using Auction.DAL.EF;
using Auction.DAL.Entities;
using Auction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class BidRepository : IRepository<Bid>
    {
        private AuctionContext db;

        public BidRepository(AuctionContext context)
        {
            db = context;
        }

        public void Add(Bid entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Bid provided was null value");
            if (db.Lots.Find(entity.Id) != null)
                throw new InvalidOperationException("A bid with current id already exists");
            db.Bids.Add(entity);
        }

        public void Delete(int id)
        {
            Bid bid = db.Bids.Find(id);
            if (bid != null)
                db.Bids.Remove(bid);
        }

        public async Task<Bid> GetByIdAsync(int id)
        {
            return await db.Bids.AsNoTracking().Where(b => b.Id == id).Include(b => b.Bidder).Include(b => b.Lot).Include(b => b.Lot.Seller).FirstOrDefaultAsync();
        }

        public IQueryable<Bid> GetAll()
        {
            return db.Bids;
        }

        public void Update(Bid entity)
        {
            db.Bids.AddOrUpdate(entity);
        }
    }
}
