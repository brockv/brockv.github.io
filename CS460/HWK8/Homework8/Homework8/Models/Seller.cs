using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework8.Models
{
    public class Seller
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Property to hold the seller's name.
        /// </summary>
        [Required(ErrorMessage = "Please enter the seller's name"), StringLength(30)]
        [Display(Name = "Seller Name")]
        public string Name { get; set; }
    }
}