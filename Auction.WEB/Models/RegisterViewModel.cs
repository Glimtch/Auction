using Auction.WEB.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.WEB.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email adress")]
        [Display(Name = "Email (login)*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Input passwords does not match")]
        [Display(Name = "Confirm password*")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Nickname is required")]
        [Display(Name = "Nickname*")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Credit card number is required")]
        [Display(Name = "Credit card number*")]
        [CreditCardNumber(ErrorMessage = "Invalid card number")]
        public string CreditCardNumber { get; set; }
    }
}