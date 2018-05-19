using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.WEB.Models
{
    public class DetailedLotViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

<<<<<<< HEAD
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }

=======
>>>>>>> 22b2c0b5690e3f3d5887113ec81c9842a533e78d
        [Required]
        [Display(Name = "Starting Price")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal StartPrice { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal CurrentPrice { get; set; }

<<<<<<< HEAD
=======
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal NewPrice { get; set; }

>>>>>>> 22b2c0b5690e3f3d5887113ec81c9842a533e78d
        public string SellerId { get; set; }

        public string SellerNickname { get; set; }

        [Required]
        [Display(Name = "Expire date")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpireDate { get; set; }

        [Display(Name = "Time left")]
        public TimeSpan TimeLeft
        {
            get
            {
                return ExpireDate - DateTime.Now;
            }
        }

        public string BidderId { get; set; }
    }
}