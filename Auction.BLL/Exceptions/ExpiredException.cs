using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Exceptions
{
    public class ExpiredException : Exception
    {
        public ExpiredException() : base() { }

        public ExpiredException(string message) : base(message) { }

        public ExpiredException(string message, Exception innerException) : base(message, innerException) { }
    }
}
