using Auction.BLL.Infrastructure;
using Auction.BLL.Interfaces;
using Auction.BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Web.Mvc;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(Auction.WEB.App_Start.DependenciesConfig))]

namespace Auction.WEB.App_Start
{
    public class DependenciesConfig
    {
        public void Configuration(IAppBuilder app)
        {
            IKernel kernel = new StandardKernel(new ServicesModule());
            app.CreatePerOwinContext<IUsersService>(() => kernel.Get<IUsersService>());
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}