using Auction.DAL.EF;
using Auction.DAL.Entities;
using Auction.DAL.Repositories.Identity;
using Auction.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        AuctionContext db;
        AuctionUserManager userManager;
        AuctionRoleManager roleManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new AuctionContext(connectionString);
        }

        public AuctionUserManager UserManager
        {
            get
            {
                if(userManager == null)
                    userManager = new AuctionUserManager(new UserStore<AuctionUser>(db));
                return userManager;
            }
        }

        public AuctionRoleManager RoleManager
        {
            get
            {
                if(roleManager == null)
                    roleManager = new AuctionRoleManager(new RoleStore<AuctionRole>(db));
                return roleManager;
            }
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
                    if (userManager != null)
                        userManager.Dispose();
                    if (roleManager != null)
                        roleManager.Dispose();
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
