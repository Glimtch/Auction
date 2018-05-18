using Auction.DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Identity
{
    public class AuctionUserManager : UserManager<AuctionUser>
    {
        public AuctionUserManager(IUserStore<AuctionUser> store)
                : base(store) { }
    }
}
