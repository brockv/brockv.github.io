using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework8.Models
{
    public class Item
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Property to hold the item's name.
        /// </summary>
        [Required(ErrorMessage = "Please enter the item's name"), StringLength(30)]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        /// <summary>
        /// Property to hold the item's description.
        /// </summary>
        [Required(ErrorMessage = "Please enter the item's description"), StringLength(30)]
        [Display(Name = "Item Description")]
        public string Description { get; set; }

        /// <summary>
        /// Property to hold the item's seller.
        /// </summary>
        [Required(ErrorMessage = "Please enter the seller's name"), StringLength(30)]
        [Display(Name = "Seller")]
        public string Seller { get; set; }
    }
}