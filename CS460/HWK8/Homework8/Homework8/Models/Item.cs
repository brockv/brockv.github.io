namespace Homework8
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Bids = new HashSet<Bid>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name="Item Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Item Description")]
        public string Description { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Seller")]
        public string Seller { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bids { get; set; }

        public virtual Seller Seller1 { get; set; }
    }
}
