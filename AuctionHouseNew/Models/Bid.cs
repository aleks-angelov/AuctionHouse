using System;

namespace AuctionHouseNew.Models
{
    public class Bid
    {
        public int BidID { get; set; }
        public int BidderID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PlaceTime { get; set; }
        public virtual Auction Auction { get; set; }
    }
}