using Auction.BLL.DTOs;
using Auction.BLL.Exceptions;
using Auction.BLL.Infrastructure;
using Auction.BLL.Interfaces;
using Auction.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Auction.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUsersService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUsersService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (model == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                try
                {
                    ClaimsIdentity claim = await UserService.AuthenticateAsync(userDto);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
                catch(UsersManagementException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (model == null)
                return HttpNotFound();
            
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Nickname = model.Nickname,
                    Email = model.Email,
                    Password = model.Password,
                    CreditCardNumber = model.CreditCardNumber,
                    Roles = new List<string>() { "user" }
                };
                try
                {
                    await UserService.CreateAsync(userDto);
                    return RedirectToAction("Login");
                }
                catch (UsersManagementException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> UserProfile(string id)
        {
            if (id == null)
                return HttpNotFound();
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
            try
            {
                var user = await UserService.GetUserByIdAsync(id);
                return View(new ProfileViewModel() { Id = user.Id, Nickname = user.Nickname, Email = user.Email, CreditCardNumber = user.CreditCardNumber });
            }
            catch(UsersManagementException)
            {
                return HttpNotFound();
            }
<<<<<<< HEAD
=======
=======
            var user = await UserService.GetUserByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            return View(new ProfileViewModel() { Id = user.Id, Nickname = user.Nickname, Email = user.Email, CreditCardNumber = user.CreditCardNumber });
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
        }
    }
}