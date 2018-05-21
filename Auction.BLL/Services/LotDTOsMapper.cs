using Auction.BLL.DTOs;
using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
    /// <summary>
    /// Provides functionality for mapping between Auction.DAL.Entities.Lot and Auction.BLL.DTOs.LotDTO.
    /// </summary>
    internal static class LotDTOsMapper
    {
        /// <summary>
        /// Creates a new LotDTO object from provided Lot object.
        /// </summary>
        internal static LotDTO LotDtoFromLot(Lot lot)
        {
            if (lot == null)
                return null;
            var lotDto = new LotDTO()
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                Image = lot.Image,
                ExpireDate = lot.ExpireDate,
                SellerId = lot.SellerId,
                SellerNickname = lot.Seller.Nickname,
                StartPrice = lot.StartPrice
            };
            if (lot.WasBidOn)
            {
                lotDto.CurrentPrice = lot.CurrentBid.BidPrice;
                lotDto.BidderId = lot.CurrentBid.BidderId;
                lotDto.BidderNickname = lot.CurrentBid.Bidder.Nickname;
            }
            else
            {
                lotDto.CurrentPrice = lot.StartPrice;
            }
            return lotDto;
        }

        /// <summary>
        /// Creates a new Lot object and a Bid if exists from provided LotDTO object.
        /// </summary>
        internal static (Lot lot, Bid bid) LotAndBidFromLotDto(LotDTO lotDto)
        {
            if (lotDto == null)
                return (null, null);
            Lot lot = new Lot()
            {
                Id = lotDto.Id,
                Name = lotDto.Name,
                Description = lotDto.Description,
                Image = lotDto.Image,
                ExpireDate = lotDto.ExpireDate,
                StartPrice = lotDto.StartPrice,
                SellerId = lotDto.SellerId
            };
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
