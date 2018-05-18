using Auction.BLL.DTOs;
using Auction.BLL.Infrastructure;
using Auction.BLL.Interfaces;
using Auction.DAL.Entities;
using Auction.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
    public class UsersService : IUsersService
    {
        IIdentityUnitOfWork db;

        public UsersService(IIdentityUnitOfWork uow)
        {
            db = uow;
        }

        public async Task CreateAsync(UserDTO userDto)
        {
            AuctionUser user = await db.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new AuctionUser { Email = userDto.Email, UserName = userDto.Email, Nickname = userDto.Nickname, CreditCardNumber = userDto.CreditCardNumber };
                var result = await db.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    throw new Exception(result.Errors.FirstOrDefault());
                foreach (var role in userDto.Roles)
                {
                    await db.UserManager.AddToRoleAsync(user.Id, role);
                }
                await db.SaveAsync();
            }
            else
            {
                throw new ArgumentException("User with provided login already exists");
            }
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            AuctionUser user = await db.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await db.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Id provided was null value");
            var user = await db.UserManager.FindByIdAsync(id);
            if (user == null)
                return null;
            return new UserDTO() { Id = user.Id, Nickname = user.Nickname, CreditCardNumber = user.CreditCardNumber, Email = user.Email, Roles = db.UserManager.GetRoles(user.Id) };
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
