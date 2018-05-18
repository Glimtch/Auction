using Auction.DAL.Interfaces;
using Auction.DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL
{
    public class UnitsOfWorkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IIdentityUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument("DefaultConnection");
        }
    }
}
