using Auction.BLL.Interfaces;
using Auction.BLL.Services;
using Auction.DAL;
using Auction.DAL.Interfaces;
using Auction.DAL.Repositories;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Infrastructure
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIdentityUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument("DefaultConnection");
            Bind<IUsersService>().To<UsersService>().InSingletonScope();
            Bind<IAuctionUnitOfWork>().To<AuctionUnitOfWork>().WithConstructorArgument("DefaultConnection");
            Bind<ILotsService>().To<LotsService>().InSingletonScope();
        }
    }
}
