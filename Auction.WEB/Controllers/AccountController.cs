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
using Auction.WEB.Services;

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
            try
            {
                var user = await UserService.GetUserByIdAsync(id);
                var sold = new List<DetailedLotViewModel>();
                var won = new List<DetailedLotViewModel>();

                foreach (var lotDto in user.SoldLots)
                {
                    sold.Add(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
                }
                foreach(var lotDto in user.WonBids)
                {
                    won.Add(LotViewModelsMapper.DetailedLotFromLotDTO(lotDto));
                }

                return View(new ProfileViewModel()
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    Email = user.Email,
                    CreditCardNumber = user.CreditCardNumber,
                    Roles = user.Roles,
                    SoldLots = sold,
                    WonBids = won
                });
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
            try
            {
                var user = await UserService.GetUserByIdAsync(id);
                return View(new ProfileViewModel() { Id = user.Id, Nickname = user.Nickname, Email = user.Email, CreditCardNumber = user.CreditCardNumber });
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
            }
            catch(UsersManagementException)
            {
                return HttpNotFound();
            }
<<<<<<< HEAD
        }

        [HttpPost]
        public async Task<ActionResult> SetToAdmin(string id)
        {
            if (id == null)
                return HttpNotFound();
            try
            {
                await UserService.ChangeUserRole(id, "user", "admin");
            }
            catch(UsersManagementException e)
            {
                ViewBag.Message = e;
            }
            return RedirectToAction("UserProfile", new { id = id });
        }

        [HttpPost]
        public async Task<ActionResult> SetToUser(string id)
        {
            if (id == null)
                return HttpNotFound();
            try
            {
                await UserService.ChangeUserRole(id, "admin", "user");
            }
            catch (UsersManagementException e)
            {
                ViewBag.Message = e;
            }
            return RedirectToAction("UserProfile", new { id = id });
=======
<<<<<<< HEAD
=======
=======
            var user = await UserService.GetUserByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            return View(new ProfileViewModel() { Id = user.Id, Nickname = user.Nickname, Email = user.Email, CreditCardNumber = user.CreditCardNumber });
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
        }
    }
}