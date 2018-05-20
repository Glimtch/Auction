using Auction.BLL.DTOs;
using Auction.BLL.Exceptions;
using Auction.BLL.Infrastructure;
using Auction.BLL.Interfaces;
using Auction.DAL.Entities;
using Auction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
    public delegate void LotDateExpiringDelegate(object sender, ExpiredLotEventArgs e);

    public class LotsService : ILotsService
    {
        IAuctionUnitOfWork db;

        /// <summary>
        /// Provides functionality for registering, updating, finding, deleting lots and bidding.
        /// </summary>
        public LotsService(IAuctionUnitOfWork uow)
        {
            db = uow;
        }

        /// <summary>
        /// Is invoked when a lot's time expires.
        /// </summary>
        internal event LotDateExpiringDelegate LotDateExpiring = null;

        /// <summary>
        /// Creates a lot, if it does not exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="LotsManagementException"></exception>
        public async Task CreateLotAsync(LotDTO lotDto)
        {
            if (lotDto == null)
                throw new ArgumentNullException("Lot provided was null value");
            if (lotDto.Name == null || lotDto.SellerId == null || lotDto.StartPrice < 1 || lotDto.StartPrice > 500000 || lotDto.ExpireDate < DateTime.Now)
                throw new ArgumentException("Invalid lot info provided");
            var lot = await db.Lots.GetByIdAsync(lotDto.Id);
            if (lot != null)
                throw new LotsManagementException("A lot with current id already exists");
            var (newLot, bid) = LotsMapper.LotAndBidFromLotDto(lotDto);
            db.Lots.Add(newLot);
            await db.SaveAsync();
        }

        /// <summary>
        /// Updates a lot, if it exists.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="LotsManagementException"></exception>
        /// <exception cref="ExpiredException"></exception>
        public async Task UpdateLotAsync(LotDTO lotDto)
        {
            if (lotDto == null)
                throw new ArgumentNullException ("Lot provided was null value");
            if (lotDto.Name == null || lotDto.SellerId == null)
                throw new ArgumentException("Invalid lot provided");
            var lot = await db.Lots.GetByIdAsync(lotDto.Id);
            if (lot == null)
                throw new LotsManagementException("A lot with current id does not exist");
            if (lot.State != LotState.Active)
            {
                LotDateExpiring?.Invoke(this, new ExpiredLotEventArgs(lot));
                throw new ExpiredException("Lot's time has expired. It cannot be changed anymore");
            }
            var (newLot, bid) = LotsMapper.LotAndBidFromLotDto(lotDto);
            db.Lots.Update(newLot);
            await db.SaveAsync();
        }

        /// <summary>
        /// Returns all active lots.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<LotDTO>> GetAllActiveLotsAsync()
        {
            return await SearchActiveLotsAsync("");
        }

        /// <summary>
        /// Returns all active lots with provided pattern.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IEnumerable<LotDTO>> SearchActiveLotsAsync(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("Pattern provided was null value");
            var lotDtos = new List<LotDTO>();
            await Task.Run(() =>
            {
                var lots = db.Lots.GetAll().Where(l => l.Name.Contains(pattern));
                foreach (var lot in lots)
                {
                    if (lot.State != LotState.Active)
                        LotDateExpiring?.Invoke(this, new ExpiredLotEventArgs(lot));
                    else
                        lotDtos.Add(LotsMapper.LotDtoFromLot(lot));
                }
            });
            return lotDtos;
        }

        /// <summary>
        /// Return a lot by id if it exists.
        /// </summary>
        /// <exception cref="LotsManagementException"></exception>
        public async Task<LotDTO> GetLotByIdAsync(int id)
        {
            var lot = await db.Lots.GetByIdAsync(id);
            if(lot == null)
                throw new LotsManagementException("A lot with current id does not exist");
            if (lot.State != LotState.Active)
                LotDateExpiring?.Invoke(this, new ExpiredLotEventArgs(lot));
            return LotsMapper.LotDtoFromLot(lot);
        }

        public async Task<IEnumerable<LotDTO>> GetLotsByBidderAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LotDTO>> GetLotsBySellerAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LotDTO>> GetWonLotsByBidderAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets a new bid on a lot.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="LotsManagementException"></exception>
        /// <exception cref="ExpiredException"></exception>
        /// <param name="lotId">Id of a lot a bid will be placed on.</param>
        /// <param name="newPrice">New price bid.</param>
        /// <param name="bidderId">Id of a user that will be set as a new high bidder.</param>
        public async Task UpdateBidAsync(int lotId, decimal newPrice, string bidderId)
        {
            if (string.IsNullOrEmpty(bidderId))
                throw new ArgumentNullException("Bidder id provided was null value");
            var lot = await db.Lots.GetByIdAsync(lotId);
            if (lot == null)
                throw new LotsManagementException("A lot with current id does not exist");
            if (lot.WasBidOn &&
                newPrice <= lot.CurrentBid.BidPrice)
                throw new ArgumentException("Cannot perform a bid with price lower than current");
            if (lot.State != LotState.Active)
            {
                LotDateExpiring?.Invoke(this, new ExpiredLotEventArgs(lot));
                throw new ExpiredException("Lot's time has expired. It cannot be bid on anymore");
            }
            var bid = new Bid() { Id = lotId, BidPrice = newPrice, BidderId = bidderId };
            lot.WasBidOn = true;
            db.Lots.Update(lot);
            db.Bids.Update(bid);
            await db.SaveAsync();
        }

        /// <summary>
        /// Delets a lot if it exists.
        /// </summary>
        public async Task DeleteLotAsync(int id)
        {
            db.Lots.Delete(id);
            await db.SaveAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
