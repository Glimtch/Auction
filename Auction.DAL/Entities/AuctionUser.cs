using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Auction.DAL.Entities
{
    public class AuctionUser : IdentityUser
    {
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }

        public string Nickname { get; set; }
        public string CreditCardNumber { get; set; }

        public AuctionUser()
        {
            Lots = new List<Lot>();
            Bids = new List<Bid>();
        }
    }
}
