using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Homework8.Models
{
    public class Bid
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        [ForeignKey("Item")]
        public int ItemID { get; set; }

        /// <summary>
        /// Property to hold the item's name.
        /// </summary>
        [Required(ErrorMessage = "Please enter the buyer's name"), StringLength(30)]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        /// <summary>
        /// Property to hold the item's description.
        /// </summary>
        [Required(ErrorMessage = "Please enter your bid amount"), StringLength(30)]
        [Display(Name = "Bid Amount")]
        public decimal Price { get; set; }

        /// <summary>
        /// Property to hold the time the bid was submitted.
        /// </summary>
        [Display(Name = "Date Submitted")]
        public DateTime RequestTimestamp { get; set; }
    }
}