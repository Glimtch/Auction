using Auction.BLL.DTOs;
using Auction.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auction.BLL.Exceptions;

namespace Auction.BLL.Interfaces
{
    public interface IUsersService : IDisposable
    {
        /// <summary>
        /// Creates a user, if it does not exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UsersManagementException"></exception>
        Task CreateAsync(UserDTO userDto);

        /// <summary>
        /// Authenticates a user, if exists.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UsersManagementException"></exception>
        /// <returns>
        /// Claims identity of the authenticated user.
        /// </returns>
        Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto);

        /// <summary>
        /// Returns a user, if it exists.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UsersManagementException"></exception>
        Task<UserDTO> GetUserByIdAsync(string id);

        /// <summary>
        /// Sets new role to a user and remove and old one, if all of them exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UsersManagementException"></exception>
        Task ChangeUserRoleAsync(string id, string oldRole, string newRole);
    }
}
