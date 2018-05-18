using Auction.DAL.Repositories.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        AuctionUserManager UserManager { get; }
        AuctionRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}