using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Exceptions
{
    public class UsersManagementException : Exception
    {
        public UsersManagementException() : base() { }

        public UsersManagementException(string message) : base(message) { }

        public UsersManagementException(string message, Exception innerException) : base(message, innerException) { }
    }
}
