using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL
{
    public class ExpiredLotEventArgs : EventArgs
    {
        public Lot Lot { get; }

        public ExpiredLotEventArgs(Lot lot)
        {
            this.Lot = lot;
        }
    }
}
