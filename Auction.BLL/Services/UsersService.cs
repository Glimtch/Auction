using Auction.BLL.DTOs;
using Auction.BLL.Exceptions;
using Auction.BLL.Infrastructure;
using Auction.BLL.Interfaces;
using Auction.DAL.Entities;
using Auction.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
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
<<<<<<< HEAD
                throw new ArgumentNullException("User provided was null value");;
=======
<<<<<<< HEAD
                throw new ArgumentNullException("User provided was null value");;
=======
<<<<<<< HEAD
                throw new ArgumentNullException("User provided was null value");;
=======
                throw new ArgumentNullException("User provided was null value");
            ClaimsIdentity claim = null;
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
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
            var soldLots = new List<LotDTO>();
            var wonBids = new List<LotDTO>();
            var user = await db.UserManager.FindByIdAsync(id);
            if (user == null)
<<<<<<< HEAD
                throw new UsersManagementException("A user with provided id does not exist");
            await Task.Run(() =>
            {
                user = db.UserManager.Users.Where(u => u.Id == id)
                    .Include(u => u.Lots).Include(u => u.Lots.Select(l => l.Seller))
                    .Include(u => u.Lots.Select(l => l.CurrentBid))
                    .Include(u => u.Lots.Select(l => l.CurrentBid.Bidder))
                    .Include(u => u.Bids).Include(u => u.Bids.Select(b => b.Bidder))
                    .Include(u => u.Bids.Select(b => b.Lot))
                    .Include(u => u.Bids.Select(b => b.Lot.CurrentBid))
                    .FirstOrDefault();
            });

            foreach (var lot in user.Lots)
            {
                if (lot.State == LotState.Sold)
                    soldLots.Add(LotDTOsMapper.LotDtoFromLot(lot));
            }
            foreach (var bid in user.Bids)
            {
                if (bid.Lot.State == LotState.Sold)
                    wonBids.Add(LotDTOsMapper.LotDtoFromLot(bid.Lot));
            }
            return new UserDTO()
            {
=======
                throw new UsersManagementException("A user with current id does not exist");
<<<<<<< HEAD
            var sold = new List<LotDTO>();
            var won = new List<LotDTO>();
            foreach(var lot in user.Lots)
            {
                if (lot.State == LotState.Sold)
                    sold.Add(LotDTOsMapper.LotDtoFromLot(lot));
            }
            foreach(var bid in user.Bids)
            {
                if(bid.Lot.State == LotState.Sold)
                    won.Add(LotDTOsMapper.LotDtoFromLot(bid.Lot));
            }
            return new UserDTO()
            {
=======
            return new UserDTO() {
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
                Id = user.Id,
                Nickname = user.Nickname,
                CreditCardNumber = user.CreditCardNumber,
                Email = user.Email,
<<<<<<< HEAD
                Roles = db.UserManager.GetRoles(user.Id),
                SoldLots = soldLots,
                WonBids = wonBids
=======
<<<<<<< HEAD
                Roles = db.UserManager.GetRoles(user.Id),
                SoldLots = sold,
                WonBids = won
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
            };
        }

        /// <summary>
        /// Sets new role to a user and remove and old one, if all of them exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UsersManagementException"></exception>
<<<<<<< HEAD
        public async Task ChangeUserRoleAsync(string id, string oldRole, string newRole)
        {
            if (id == null)
                throw new ArgumentNullException("Id provided was null value");
            if (string.IsNullOrEmpty(oldRole) || string.IsNullOrEmpty(newRole))
                throw new ArgumentNullException("One or more roles provided was null value");
=======
        public async Task ChangeUserRole(string id, string oldRole, string newRole)
        {
            if (id == null)
                throw new ArgumentNullException("Id provided was null value");
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
            var user = await db.UserManager.FindByIdAsync(id);
            if (user == null)
                throw new UsersManagementException("A user with current id does not exist");
            if (!(await db.RoleManager.RoleExistsAsync(oldRole)) ||
                !(await db.RoleManager.RoleExistsAsync(newRole)))
            {
                throw new UsersManagementException("One or more roles provided does not exist");
            }
            await db.UserManager.AddToRoleAsync(id, newRole);
            await db.UserManager.RemoveFromRoleAsync(id, oldRole);
            await db.SaveAsync();
<<<<<<< HEAD
=======
=======
                Roles = db.UserManager.GetRoles(user.Id) };
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
