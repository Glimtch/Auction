﻿using Auction.BLL.DTOs;
using Auction.BLL.Exceptions;
using Auction.BLL.Infrastructure;
using Auction.BLL.Interfaces;
using Auction.DAL.Entities;
using Auction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
<<<<<<< HEAD
        /// Is triggered when a lot's time expires.
=======
<<<<<<< HEAD
        /// Is triggered when a lot's time expires.
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
        /// Is invoked when a lot's time expires.
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
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
<<<<<<< HEAD
            var (newLot, bid) = LotDTOsMapper.LotAndBidFromLotDto(lotDto);
            db.Lots.Add(newLot);
=======
<<<<<<< HEAD
            var (newLot, bid) = LotDTOsMapper.LotAndBidFromLotDto(lotDto);
            db.Lots.Add(newLot);
=======
            var (newLot, bid) = LotsMapper.LotAndBidFromLotDto(lotDto);
            db.Lots.Add(newLot);
<<<<<<< HEAD
=======
=======
        /// Invokes when a lot's time expires.
        /// </summary>
        public event LotDateExpiringDelegate LotDateExpiring = null;
        
        /// <summary>
        /// Creates a lot, if it does not exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task CreateLotAsync(LotDTO lotDto)
        {
            if (lotDto == null)
                throw new ArgumentNullException("Lot provided was null value");
            if (lotDto.Name == null || lotDto.SellerId == null || lotDto.StartPrice < 1 || lotDto.StartPrice > 500000 || lotDto.ExpireDate < DateTime.Now)
                throw new ArgumentException("Invalid lot info provided");
            var (lot, bid) = LotsMapper.LotAndBidFromLotDto(lotDto);
            db.Lots.Add(lot);
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
            await db.SaveAsync();
        }

        /// <summary>
        /// Updates a lot, if it exists.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ExpiredException"></exception>
        public async Task UpdateLotAsync(LotDTO lotDto)
        {
            if (lotDto == null)
                throw new ArgumentNullException ("Lot provided was null value");
            if (lotDto.Name == null || lotDto.SellerId == null)
                throw new ArgumentException("Invalid lot provided");
            var lot = await db.Lots.GetByIdAsync(lotDto.Id);
            if (lot == null)
                throw new NotFoundException("No lot with current id exists in database");
            if (lot.State != LotState.Active)
            {
                LotDateExpiring?.Invoke(this, new ExpiredLotEventArgs(lot));
                throw new ExpiredException("Lot's time has expired. It cannot be changed anymore");
            }
            var (newLot, bid) = LotsMapper.LotAndBidFromLotDto(lotDto);
            db.Lots.Update(newLot);
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
            await db.SaveAsync();
        }

        /// <summary>
<<<<<<< HEAD
        /// Updates a lot, if it exists.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
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
<<<<<<< HEAD
            var (newLot, bid) = LotDTOsMapper.LotAndBidFromLotDto(lotDto);
=======
<<<<<<< HEAD
            var (newLot, bid) = LotDTOsMapper.LotAndBidFromLotDto(lotDto);
=======
            var (newLot, bid) = LotsMapper.LotAndBidFromLotDto(lotDto);
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
            db.Lots.Update(newLot);
            await db.SaveAsync();
        }

        /// <summary>
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
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
<<<<<<< HEAD
                var lots = db.Lots.GetAll().Where(l => l.Name.Contains(pattern)).Include(l => l.CurrentBid).Include(l => l.CurrentBid.Bidder).Include(l => l.Seller);
=======
                var lots = db.Lots.GetAll().Where(l => l.Name.Contains(pattern));
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
                foreach (var lot in lots)
                {
                    if (lot.State != LotState.Active)
                        LotDateExpiring?.Invoke(this, new ExpiredLotEventArgs(lot));
                    else
<<<<<<< HEAD
                        lotDtos.Add(LotDTOsMapper.LotDtoFromLot(lot));
=======
<<<<<<< HEAD
                        lotDtos.Add(LotDTOsMapper.LotDtoFromLot(lot));
=======
                        lotDtos.Add(LotsMapper.LotDtoFromLot(lot));
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
                }
            });
            return lotDtos;
        }

        /// <summary>
        /// Return a lot by id if it exists.
        /// </summary>
<<<<<<< HEAD
        /// <exception cref="LotsManagementException"></exception>
=======
<<<<<<< HEAD
        /// <exception cref="LotsManagementException"></exception>
=======
<<<<<<< HEAD
        /// <exception cref="LotsManagementException"></exception>
=======
<<<<<<< HEAD
        /// <exception cref="LotsManagementException"></exception>
=======
        /// <exception cref="NotFoundException"></exception>
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
        public async Task<LotDTO> GetLotByIdAsync(int id)
        {
            var lot = await db.Lots.GetByIdAsync(id);
            if(lot == null)
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
                throw new LotsManagementException("A lot with current id does not exist");
            if (lot.State != LotState.Active)
                LotDateExpiring?.Invoke(this, new ExpiredLotEventArgs(lot));
            return LotDTOsMapper.LotDtoFromLot(lot);
<<<<<<< HEAD
=======
=======
<<<<<<< HEAD
                throw new LotsManagementException("A lot with current id does not exist");
=======
<<<<<<< HEAD
                throw new LotsManagementException("A lot with current id does not exist");
=======
                throw new NotFoundException("No lot with current id exists in database");
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
            if (lot.State != LotState.Active)
                LotDateExpiring?.Invoke(this, new ExpiredLotEventArgs(lot));
            return LotsMapper.LotDtoFromLot(lot);
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
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
<<<<<<< HEAD
        /// <exception cref="LotsManagementException"></exception>
=======
<<<<<<< HEAD
        /// <exception cref="LotsManagementException"></exception>
=======
<<<<<<< HEAD
        /// <exception cref="LotsManagementException"></exception>
=======
<<<<<<< HEAD
        /// <exception cref="LotsManagementException"></exception>
=======
        /// <exception cref="NotFoundException"></exception>
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
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
<<<<<<< HEAD
                throw new LotsManagementException("A lot with current id does not exist");
=======
<<<<<<< HEAD
                throw new LotsManagementException("A lot with current id does not exist");
=======
<<<<<<< HEAD
                throw new LotsManagementException("A lot with current id does not exist");
=======
<<<<<<< HEAD
                throw new LotsManagementException("A lot with current id does not exist");
=======
                throw new NotFoundException("A lot with current id does not exist");
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
        
        private void LotDateExpireHandler(object sender, ExpiredLotEventArgs e)
        {
            throw new NotImplementedException();
        }
=======
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
