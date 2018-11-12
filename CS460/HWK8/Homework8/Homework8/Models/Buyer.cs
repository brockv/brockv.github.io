using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework8.Models
{
    public class Buyer
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Property to hold the buyer's name.
        /// </summary>
        [Required(ErrorMessage = "Please enter the buyer's name"), StringLength(30)]
        [Display(Name = "Buyer Name")]
        public string Name { get; set; }
    }
}