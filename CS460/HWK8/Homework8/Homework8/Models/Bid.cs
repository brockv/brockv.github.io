namespace Homework8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bid
    {
        public int ID { get; set; }

        public int ItemID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Buyer")]
        public string Buyer { get; set; }

        [Display(Name = "Bid Amount")]
        public decimal Price { get; set; }

        [Display(Name = "Time of Bid")]
        public DateTime BidTimestamp { get; set; }

        [Display(Name = "Item")]
        public virtual Item Item { get; set; }

        public virtual Buyer Buyer1 { get; set; }
    }
}
