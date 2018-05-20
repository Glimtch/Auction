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
        /// <summary>
        /// Return a lot by id if it exists.
        /// </summary>
        /// <exception cref="NotFoundException"></exception>
        Task<LotDTO> GetLotByIdAsync(int id);

        /// <summary>
        /// Creates a lot, if it does not exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        Task CreateLotAsync(LotDTO lotDto);

        /// <summary>
        /// Updates a lot, if it exists.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ExpiredException"></exception>
        Task UpdateLotAsync(LotDTO lotDto);

        /// <summary>
        /// Sets a new bid on a lot.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ExpiredException"></exception>
        /// <param name="lotId">Id of a lot a bid will be placed on.</param>
        /// <param name="newPrice">New price bid.</param>
        /// <param name="bidderId">Id of a user that will be set as a new high bidder.</param>
        Task UpdateBidAsync(int lotId, decimal newPrice, string BidderId);

        /// <summary>
        /// Returns all active lots.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        Task<IEnumerable<LotDTO>> GetAllActiveLotsAsync();

        /// <summary>
        /// Returns all active lots with provided pattern.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        Task<IEnumerable<LotDTO>> SearchActiveLotsAsync(string pattern);

        Task<IEnumerable<LotDTO>> GetLotsBySellerAsync(UserDTO user);
        Task<IEnumerable<LotDTO>> GetLotsByBidderAsync(UserDTO user);
        Task<IEnumerable<LotDTO>> GetWonLotsByBidderAsync(UserDTO user);

        /// <summary>
        /// Delets a lot if it exists.
        /// </summary>
        Task DeleteLotAsync(int id);

        /// <summary>
        /// Invokes when a lot's time expires.
        /// </summary>
        event LotDateExpiringDelegate LotDateExpiring;
    }
}
