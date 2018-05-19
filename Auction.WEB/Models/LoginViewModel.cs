using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auction.WEB.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        [EmailAddress(ErrorMessage = "Invalid email adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}