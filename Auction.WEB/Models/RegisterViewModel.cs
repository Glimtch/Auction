using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.WEB.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email (login)*")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm password*")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nickname*")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Credit card number*")]
        [DataType(DataType.CreditCard)]
        public string CreditCardNumber { get; set; }
    }
}