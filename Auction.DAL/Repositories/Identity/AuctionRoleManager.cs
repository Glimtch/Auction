using Auction.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Identity
{
    public class AuctionRoleManager : RoleManager<AuctionRole>
    {
        public AuctionRoleManager(RoleStore<AuctionRole> store)
                    : base(store) { }
    }
}
