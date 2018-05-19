using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.WEB.Models
{
    public class RegisterLotViewModel
    {
        [Required(ErrorMessage = "Lot name is required")]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }
        
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required(ErrorMessage = "Starting price is required")]
        [Display(Name = "Starting Price*")]
        [Range(typeof(decimal), "1", "500000", ErrorMessage = "Starting price must be between $1-500'000")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal StartPrice { get; set; }

        public string SellerId { get; set; }

        [Required(ErrorMessage = "Expire date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Expire date*")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpireDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Expire time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime ExpireTime { get; set; }



    }
}