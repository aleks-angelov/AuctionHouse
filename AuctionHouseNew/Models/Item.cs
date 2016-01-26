using System.ComponentModel.DataAnnotations;

namespace AuctionHouseNew.Models
{
    public class Item
    {
        public int ItemID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Valuation { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}