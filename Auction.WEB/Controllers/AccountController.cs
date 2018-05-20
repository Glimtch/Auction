using Auction.BLL.DTOs;
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
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.AuthenticateAsync(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
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
                catch (Exception e)
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
            var user = await UserService.GetUserByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            return View(new ProfileViewModel() { Id = user.Id, Nickname = user.Nickname, Email = user.Email, CreditCardNumber = user.CreditCardNumber });
        }
    }
}