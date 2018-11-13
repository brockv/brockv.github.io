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
        public string Buyer { get; set; }

        public decimal Price { get; set; }

        public DateTime BidTimestamp { get; set; }

        public virtual Item Item { get; set; }

        public virtual Buyer Buyer1 { get; set; }
    }
}
