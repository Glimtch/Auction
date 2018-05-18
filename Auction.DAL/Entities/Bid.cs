using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Entities
{
    public class Bid
    {
        public int Id { get; set; }
        
        public int LotId { get; set; }
        public virtual Lot Lot { get; set; }
        public string BidderId { get; set; }
        public virtual AuctionUser Bidder { get; set; }
        public decimal BidPrice { get; set; }
    }
}
