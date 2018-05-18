using Auction.BLL.DTOs;
using Auction.BLL.Infrastructure;
using Auction.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Interfaces
{
    public interface ILotsService : IDisposable
    {
        Task CreateLotAsync(LotDTO lotDto);
        Task UpdateBidAsync(LotDTO lotDTO);
        Task<LotDTO> GetLotByIdAsync(int id);
        Task<IEnumerable<LotDTO>> GetAllActiveLotsAsync();
        Task<IEnumerable<LotDTO>> SearchActiveLotsAsync(string pattern);
        Task<IEnumerable<LotDTO>> GetLotsBySellerAsync(UserDTO user);
        Task<IEnumerable<LotDTO>> GetLotsByBidderAsync(UserDTO user);
        Task<IEnumerable<LotDTO>> GetWonLotsByBidderAsync(UserDTO user);
        event LotDateExpiringDelegate LotDateExpiring;
    }
}
