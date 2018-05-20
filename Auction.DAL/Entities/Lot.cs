using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Entities
{
    public class Lot
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public decimal StartPrice { get; set; }
        public string SellerId { get; set; }
        public virtual AuctionUser Seller { get; set; } 
        public DateTime ExpireDate { get; set; }
        public bool WasBidOn { get; set; }
        public virtual Bid CurrentBid { get; set; }
        public LotState State
        {
            get
            {
                if (DateTime.Now < ExpireDate)
                    return LotState.Active;
                if (WasBidOn)
                    return LotState.Sold;
                return LotState.Expired;
            }
        }
    }

    public enum LotState
    {
        Active,
        Expired,
        Sold
    }
}
