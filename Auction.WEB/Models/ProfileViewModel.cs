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
<<<<<<< HEAD

=======
<<<<<<< HEAD

=======
>>>>>>> 330f767868bac6b464127496d52c68aeb23560ed
>>>>>>> 8d169e5693621b36e5ac124313ffe2c4964d930b
        public string Nickname { get; set; }
        public string Email { get; set; }
        [Display(Name = "Credit card")]
        public string CreditCardNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IList<DetailedLotViewModel> SoldLots { get; set; }
        public IList<DetailedLotViewModel> WonBids { get; set; }
    }
}