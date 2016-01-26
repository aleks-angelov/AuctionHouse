using System;
using System.Collections.Generic;

namespace AuctionHouseNew.Models
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int WinnerID { get; set; }
        public virtual Item Item { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}