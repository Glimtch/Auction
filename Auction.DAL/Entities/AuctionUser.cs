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
        public virtual ICollection<Lot> ActiveLots { get; set; }
        public virtual ICollection<Bid> ActiveBids { get; set; }

        public string Nickname { get; set; }
        public string CreditCardNumber { get; set; }

        public AuctionUser()
        {
            ActiveLots = new List<Lot>();
            ActiveBids = new List<Bid>();
        }
    }
}
