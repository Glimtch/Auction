using Auction.DAL.EF;
using Auction.DAL.Entities;
using Auction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Auction.DAL.Repositories
{
    public class LotRepository : IRepository<Lot>
    {
        private AuctionContext db;

        public LotRepository(AuctionContext context)
        {
            db = context;
        }

        public void Add(Lot entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Lot provided was null value");
            if (db.Lots.Find(entity.Id) != null)
                throw new InvalidOperationException("A lot with current id already exists");
            db.Lots.Add(entity);
        }

        public void Delete(int id)
        {
            Lot lot = db.Lots.Find(id);
            if (lot != null)
                db.Lots.Remove(lot);
        }

        public async Task<Lot> GetByIdAsync(int id)
        {
            return await db.Lots.AsNoTracking().Where(l => l.Id == id).Include(l => l.CurrentBid).Include(l => l.CurrentBid.Bidder).Include(l => l.Seller).FirstOrDefaultAsync();
        }

        public IQueryable<Lot> GetAll()
        {
            return db.Lots;
        }

        public void Update(Lot entity)
        {
            db.Lots.AddOrUpdate(entity);
        }
    }
}
