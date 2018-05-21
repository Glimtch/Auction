using Auction.BLL.DTOs;
using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
    /// <summary>
    /// Provides functionality for mapping between Auction.DAL.Entities.Lot and Auction.BLL.DTOs.LotDTO.
    /// </summary>
    internal static class LotsMapper
    {
        /// <summary>
        /// Creates a new LotDTO object from provided Lot object.
        /// </summary>
<<<<<<< HEAD
=======
=======
    public static class LotsMapper
    {
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
        internal static LotDTO LotDtoFromLot(Lot lot)
        {
            if (lot == null)
                return null;
<<<<<<< HEAD
            var lotDto = new LotDTO() { Id = lot.Id, Name = lot.Name, Description = lot.Description, Image = lot.Image, ExpireDate = lot.ExpireDate, SellerId = lot.SellerId, SellerNickname = lot.Seller.Nickname, StartPrice = lot.StartPrice };
=======
<<<<<<< HEAD
            var lotDto = new LotDTO() { Id = lot.Id, Name = lot.Name, Description = lot.Description, Image = lot.Image, ExpireDate = lot.ExpireDate, SellerId = lot.SellerId, SellerNickname = lot.Seller.Nickname, StartPrice = lot.StartPrice };
=======
            var lotDto = new LotDTO() { Id = lot.Id, Name = lot.Name, Description = lot.Description, Image = lot.Image, ExpireDate = lot.ExpireDate, SellerId = lot.SellerId, StartPrice = lot.StartPrice };
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
            if (lot.WasBidOn)
            {
                lotDto.CurrentPrice = lot.CurrentBid.BidPrice;
                lotDto.BidderId = lot.CurrentBid.BidderId;
<<<<<<< HEAD
                lotDto.BidderNickname = lot.CurrentBid.Bidder.Nickname;
=======
<<<<<<< HEAD
                lotDto.BidderNickname = lot.CurrentBid.Bidder.Nickname;
=======
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
            }
            else
            {
                lotDto.CurrentPrice = lot.StartPrice;
            }
            return lotDto;
        }

<<<<<<< HEAD
        /// <summary>
        /// Creates a new Lot object and a Bid if exists from provided LotDTO object.
        /// </summary>
=======
<<<<<<< HEAD
        /// <summary>
        /// Creates a new Lot object and a Bid if exists from provided LotDTO object.
        /// </summary>
=======
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
        internal static (Lot lot, Bid bid) LotAndBidFromLotDto(LotDTO lotDto)
        {
            if (lotDto == null)
                return (null, null);
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
