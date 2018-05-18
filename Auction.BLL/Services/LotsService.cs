using Auction.BLL.DTOs;
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

        public async Task CreateLotAsync(LotDTO lotDto)
        {
            if (lotDto == null)
                throw new Exception ("Lot provided was null value");
            var (lot, bid) = LotAndBidFromLotDto(lotDto);
            db.Lots.Add(lot);
            if (bid != null)
                db.Bids.Add(bid);
            await db.SaveAsync();
        }
        
        public async Task<IEnumerable<LotDTO>> GetAllActiveLotsAsync()
        {
            return await SearchActiveLotsAsync("");
        }

        public async Task<IEnumerable<LotDTO>> SearchActiveLotsAsync(string pattern)
        {
            var lotDtos = new List<LotDTO>();
            await Task.Run(async () =>
            {
                var lots = db.Lots.GetAll().Where(l => l.Name.Contains(pattern));
                foreach (var lot in lots)
                {
                    lotDtos.Add(await LotDtoFromLotAsync(lot));
                }
            });
            return lotDtos;
        }

        public async Task<LotDTO> GetLotByIdAsync(int id)
        {
            return await LotDtoFromLotAsync(await db.Lots.GetByIdAsync(id));
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

        public async Task UpdateBidAsync(LotDTO lotDto)
        {
            if (lotDto == null)
                throw new ArgumentNullException("Provided lot was null value");
            var lot = await db.Lots.GetByIdAsync(lotDto.Id);
            if (lot == null)
                throw new ArgumentException("A lot with current id does not exist");
            if (lot.BidId != null && 
                lotDto.CurrentPrice <= (await db.Bids.GetByIdAsync((int)lot.BidId)).BidPrice)
                throw new Exception("Cannot perform a bid with price lower than current");
            if(string.IsNullOrEmpty(lotDto.BidderId))
                throw new ArgumentNullException("Bidder id provided was null value");
            Bid bid = new Bid() { LotId = lotDto.Id, BidderId = lotDto.BidderId, BidPrice = lotDto.CurrentPrice };
            db.Bids.Add(bid);
            await db.SaveAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private async Task<LotDTO> LotDtoFromLotAsync(Lot lot)
        {
            if (lot == null)
                return null;
            var lotDto = new LotDTO() { Id = lot.Id, Name = lot.Name, Description = lot.Description, Image = lot.Image, ExpireDate = lot.ExpireDate, SellerId = lot.SellerId, StartPrice = lot.StartPrice, IsSold = lot.IsSold };
            if (lot.BidId != null)
            {
                var bid = await db.Bids.GetByIdAsync((int)lot.BidId);
                lotDto.CurrentPrice = bid.BidPrice;
                lotDto.BidderId = bid.BidderId;
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
                bid = new Bid() { LotId = lotDto.Id, BidderId = lotDto.BidderId, BidPrice = lotDto.CurrentPrice };
            }
            return (lot, bid);
        }
    }
}
