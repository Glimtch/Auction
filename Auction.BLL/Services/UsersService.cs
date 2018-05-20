using Auction.BLL.DTOs;
using Auction.BLL.Exceptions;
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
        
        /// <summary>
        /// Provides functionality for registering, authenticating, and finding users.
        /// </summary>
        public UsersService(IIdentityUnitOfWork uow)
        {
            db = uow;
        }

        /// <summary>
        /// Creates a user, if it does not exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UsersManagementException"></exception>
        public async Task CreateAsync(UserDTO userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException("User provided was null value");
            AuctionUser user = await db.UserManager.FindByEmailAsync(userDto.Email);
            if (user != null)
                throw new UsersManagementException("User with provided login already exists");
            user = new AuctionUser { Email = userDto.Email, UserName = userDto.Email, Nickname = userDto.Nickname, CreditCardNumber = userDto.CreditCardNumber };
            var result = await db.UserManager.CreateAsync(user, userDto.Password);
            if (result.Errors.Count() > 0)
                throw new UsersManagementException(result.Errors.FirstOrDefault());
            foreach (var role in userDto.Roles)
            {
                await db.UserManager.AddToRoleAsync(user.Id, role);
            }
            await db.SaveAsync();

        }

        /// <summary>
        /// Authenticates a user, if exists.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UsersManagementException"></exception>
        /// <returns>
        /// Claims identity of the authenticated user.
        /// </returns>
        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto)
        {
            if(userDto == null)
<<<<<<< HEAD
                throw new ArgumentNullException("User provided was null value");;
=======
                throw new ArgumentNullException("User provided was null value");
            ClaimsIdentity claim = null;
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
            AuctionUser user = await db.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user == null)
                throw new UsersManagementException("A user with current login and password does not exist");
            return await db.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        /// <summary>
        /// Returns a user, if it exists.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UsersManagementException"></exception>
        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException("Id provided was null value");
            var user = await db.UserManager.FindByIdAsync(id);
            if (user == null)
                throw new UsersManagementException("A user with current id does not exist");
            return new UserDTO() {
                Id = user.Id,
                Nickname = user.Nickname,
                CreditCardNumber = user.CreditCardNumber,
                Email = user.Email,
                Roles = db.UserManager.GetRoles(user.Id) };
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
