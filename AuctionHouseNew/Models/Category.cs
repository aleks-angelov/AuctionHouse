using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuctionHouseNew.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}