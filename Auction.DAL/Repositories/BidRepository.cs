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

        public Task<Bid> GetByIdAsync(int id)
        {
            return db.Bids.FindAsync(id);
        }

        public IQueryable<Bid> GetAll()
        {
            return db.Bids;
        }

        public void Update(Bid entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
