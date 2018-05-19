using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Auction.WEB.Infrastructure
{
    public class CreditCardNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var number = value as string;
            if (!string.IsNullOrEmpty(number))
            {
                string pattern = @"^\d{4}[\s-]?\d{4}[\s-]?\d{4}[\s-]?\d{4}$";
                Regex regex = new Regex(pattern);
                return regex.IsMatch(number);
            }
            return false;
        }
    }
}