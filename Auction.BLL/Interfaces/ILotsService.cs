﻿using Auction.BLL.DTOs;
using Auction.BLL.Infrastructure;
using Auction.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction.BLL.Exceptions;

namespace Auction.BLL.Interfaces
{
    public interface ILotsService : IDisposable
    {
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
        Task<LotDTO> GetLotByIdAsync(int id);

        /// <summary>
        /// Creates a lot, if it does not exist.
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
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
        Task CreateLotAsync(LotDTO lotDto);

        /// <summary>
        /// Updates a lot, if it exists.
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
        Task UpdateLotAsync(LotDTO lotDto);

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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

        /// <summary>
        /// Invokes when a lot's time expires.
        /// </summary>
        event LotDateExpiringDelegate LotDateExpiring;
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
    }
}
