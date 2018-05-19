﻿using Auction.BLL.DTOs;
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
    public delegate void LotDateExpiringDelegate(object sender, EventArgs e);

    public class LotsService : ILotsService
    {
        IAuctionUnitOfWork db;

        public LotsService(IAuctionUnitOfWork uow)
        {
            db = uow;
        }

        public event LotDateExpiringDelegate LotDateExpiring;

        public async Task CreateOrUpdateLotAsync(LotDTO lotDto)
        {
            if (lotDto == null)
                throw new Exception ("Lot provided was null value");
            var (lot, bid) = LotAndBidFromLotDto(lotDto);
<<<<<<< HEAD
            db.Lots.Update(lot);
=======
            db.Lots.Add(lot);
            if (bid != null)
                db.Bids.Update(bid);
>>>>>>> 22b2c0b5690e3f3d5887113ec81c9842a533e78d
            await db.SaveAsync();
        }
        
        public async Task<IEnumerable<LotDTO>> GetAllActiveLotsAsync()
        {
            return await SearchActiveLotsAsync("");
        }

        public async Task<IEnumerable<LotDTO>> SearchActiveLotsAsync(string pattern)
        {
            var lotDtos = new List<LotDTO>();
            await Task.Run(() =>
            {
                var lots = db.Lots.GetAll().Where(l => l.Name.Contains(pattern) && l.ExpireDate > DateTime.Now);
                foreach (var lot in lots)
                {
<<<<<<< HEAD
                    lotDtos.Add(LotDtoFromLot(lot));
=======
                    if (!lot.IsSold)
                        lotDtos.Add(await LotDtoFromLotAsync(lot));
>>>>>>> 22b2c0b5690e3f3d5887113ec81c9842a533e78d
                }
            });
            return lotDtos;
        }

        public async Task<LotDTO> GetLotByIdAsync(int id)
        {
            return LotDtoFromLot(await db.Lots.GetByIdAsync(id));
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

        public async Task<LotDTO> UpdateBidAsync(int lotId, decimal newPrice, string bidderId)
        {
            var lot = await db.Lots.GetByIdAsync(lotId);
            if (lot == null)
                throw new ArgumentException("A lot with current id does not exist");
<<<<<<< HEAD
            if (lot.WasBidOn &&
                newPrice <= (await db.Bids.GetByIdAsync(lot.Id)).BidPrice)
=======
            if (lot.WasBidOn && 
                lotDto.CurrentPrice <= (await db.Bids.GetByIdAsync(lot.Id)).BidPrice)
>>>>>>> 22b2c0b5690e3f3d5887113ec81c9842a533e78d
                throw new Exception("Cannot perform a bid with price lower than current");
            if (string.IsNullOrEmpty(bidderId))
                throw new ArgumentNullException("Bidder id provided was null value");
<<<<<<< HEAD
            var bid = new Bid() { Id = lotId, BidPrice = newPrice, BidderId = bidderId };
            lot.WasBidOn = true;
            db.Lots.Update(lot);
            db.Bids.Update(bid);
            await db.SaveAsync();
            return LotDtoFromLot(lot);
        }

        public async Task DeleteLotAsync(int id)
        {
            db.Lots.Delete(id);
=======
            var (newLot, bid) = LotAndBidFromLotDto(lotDto);
            db.Lots.Update(newLot);
            db.Bids.Update(bid);
>>>>>>> 22b2c0b5690e3f3d5887113ec81c9842a533e78d
            await db.SaveAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private LotDTO LotDtoFromLot(Lot lot)
        {
            if (lot == null)
                return null;
            var lotDto = new LotDTO() { Id = lot.Id, Name = lot.Name, Description = lot.Description, Image = lot.Image, ExpireDate = lot.ExpireDate, SellerId = lot.SellerId, StartPrice = lot.StartPrice, IsSold = lot.IsSold };
            if (lot.WasBidOn)
            {
<<<<<<< HEAD
                lotDto.CurrentPrice = lot.CurrentBid.BidPrice;
                lotDto.BidderId = lot.CurrentBid.BidderId;
=======
                var bid = await db.Bids.GetByIdAsync(lot.Id);
                lotDto.CurrentPrice = bid.BidPrice;
                lotDto.BidderId = bid.BidderId;
>>>>>>> 22b2c0b5690e3f3d5887113ec81c9842a533e78d
            }
            else
            {
                lotDto.CurrentPrice = lot.StartPrice;
            }
            return lotDto;
        }

        private (Lot lot, Bid bid) LotAndBidFromLotDto(LotDTO lotDto)
        {
            Lot lot = new Lot() { Id = lotDto.Id, Name = lotDto.Name, Description = lotDto.Description, Image = lotDto.Image, ExpireDate = lotDto.ExpireDate, StartPrice = lotDto.StartPrice, SellerId = lotDto.SellerId };
            Bid bid = null;
            if (lotDto.BidderId != null)
            {
                bid = new Bid() { Id = lotDto.Id, BidderId = lotDto.BidderId, BidPrice = lotDto.CurrentPrice };
                lot.WasBidOn = true;
            }
            return (lot, bid);
        }
    }
}
