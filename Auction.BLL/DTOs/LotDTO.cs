using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.DTOs
{
    public class LotDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public decimal StartPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public string SellerId { get; set; }
        public string SellerNickname { get; set; }
        public DateTime ExpireDate { get; set; }
        public string BidderId { get; set; }
<<<<<<< HEAD
        public string BidderNickname { get; set; }
=======
<<<<<<< HEAD
        public string BidderNickname { get; set; }
=======
<<<<<<< HEAD
        public string BidderNickname { get; set; }
=======
<<<<<<< HEAD
        public string BidderNickname { get; set; }
=======
>>>>>>> 4fb9aa43f112ff5d2bc9808fd6c9d29d451dc7eb
>>>>>>> 4b36963e151b0fddd8b58bef31cd33b7709a6b58
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
    }
}
