using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.WEB.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        [Display(Name = "Credit card")]
        public string CreditCardNumber { get; set; }
    }
}