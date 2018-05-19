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
        Task<LotDTO> GetLotByIdAsync(int id);
        Task CreateOrUpdateLotAsync(LotDTO lotDto);
        Task<LotDTO> UpdateBidAsync(int lotId, decimal newPrice, string BidderId);
        Task<IEnumerable<LotDTO>> GetAllActiveLotsAsync();
        Task<IEnumerable<LotDTO>> SearchActiveLotsAsync(string pattern);
        Task<IEnumerable<LotDTO>> GetLotsBySellerAsync(UserDTO user);
        Task<IEnumerable<LotDTO>> GetLotsByBidderAsync(UserDTO user);
        Task<IEnumerable<LotDTO>> GetWonLotsByBidderAsync(UserDTO user);
        Task DeleteLotAsync(int id);
        event LotDateExpiringDelegate LotDateExpiring;
    }
}
