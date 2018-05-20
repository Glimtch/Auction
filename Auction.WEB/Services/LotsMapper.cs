using Auction.BLL.DTOs;
using Auction.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Auction.WEB.Services
{
    /// <summary>
    /// Provides functionality for mapping between Auction.BLL.DTOs.LotDTO and Auction.WEB.Models.DetailedLotViewModel.
    /// </summary>
    internal static class LotViewModelsMapper
    {
        /// <summary>
        /// Creates a new LotDTO object from provided DetailedLotViewModel object.
        /// </summary>
        internal static LotDTO LotDTOFromDetailedLot(DetailedLotViewModel lot)
        {
            return new LotDTO()
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                Image = lot.Image,
                StartPrice = lot.StartPrice,
                SellerId = lot.SellerId,
                ExpireDate = lot.ExpireDate,
                BidderId = lot.BidderId,
                CurrentPrice = lot.CurrentPrice
            };
        }

        /// <summary>
        /// Creates a new DetailedLotViewModel object from provided LotDTO object.
        /// </summary>
        internal static DetailedLotViewModel DetailedLotFromLotDTO(LotDTO lotDto)
        {
            return new DetailedLotViewModel()
            {
                Id = lotDto.Id,
                Name = lotDto.Name,
                Description = lotDto.Description,
                Image = lotDto.Image,
                StartPrice = lotDto.StartPrice,
                SellerId = lotDto.SellerId,
                SellerNickname = lotDto.SellerNickname,
                CurrentPrice = lotDto.CurrentPrice,
                ExpireDate = lotDto.ExpireDate,
                BidderId = lotDto.BidderId,
                BidderNickname = lotDto.BidderNickname
            };
        }
    }
}