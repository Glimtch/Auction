using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Exceptions
{
    public class LotsManagementException : Exception
    {
        public LotsManagementException() : base() { }

        public LotsManagementException(string message) : base(message) { }

        public LotsManagementException(string message, Exception innerException) : base(message, innerException) { }
    }
}
