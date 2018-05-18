using Auction.BLL.DTOs;
using Auction.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Interfaces
{
    public interface IUsersService : IDisposable
    {
        Task CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto);
        Task<UserDTO> GetUserByIdAsync(string id);
    }
}
