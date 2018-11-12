namespace Homework8
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bid
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public int ItemID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Buyer")]
        public string Buyer { get; set; }

        [Display(Name = "Item Price")]
        public decimal Price { get; set; }

        [Display(Name = "Time of Bid")]
        public DateTime BidTimestamp { get; set; }

        [Display(Name = "Item Name")]
        public virtual Item Item { get; set; }
    }
}
