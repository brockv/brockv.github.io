using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework5.Models
{
    public class ServiceRequestForm
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Property to hold the tennant's first name.
        /// </summary>
        [Required(ErrorMessage = "Please enter your first name"), StringLength(20)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Property to hold the tennant's last name.
        /// </summary>
        [Required(ErrorMessage = "Please enter your last name"), StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Property to hold the tennant's phone number.
        /// </summary>
        [Required(ErrorMessage = "Please enter your phone number"), StringLength(20)]
        [RegularExpression("^\\d{3}-\\d{3}-\\d{4}$", ErrorMessage = "Please use the format: ###-###-####")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Property to hold the tennant's apartment name.
        /// </summary>
        [Required(ErrorMessage = "Please enter your apartment name"), StringLength(20)]
        [Display(Name = "Apartments")]
        public string ApartmentName { get; set; }

        /// <summary>
        /// Property to hold the tennant's unit number.
        /// </summary>
        [Required(ErrorMessage = "Please enter your unit number")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Unit Number must be positive")]
        [Display(Name = "Unit Number")]
        public int UnitNumber { get; set; }

        /// <summary>
        /// Property to hold the tennant's reason for submitting the request.
        /// </summary>
        [Required(ErrorMessage = "Please describe your request"), StringLength(250)]
        [Display(Name = "Request Description")]
        public string RequestDescription { get; set; }

        /// <summary>
        /// Property to hold the tennant's consent for unsupervised entry.
        /// </summary>
        [Display(Name = "Entry Allowed?")]
        public bool AllowEntry { get; set; }

        /// <summary>
        /// Property to hold the time the tennant's form was submitted.
        /// </summary>
        [Display(Name = "Date Submitted")]
        public DateTime RequestTimestamp { get; set; }

        /// <summary>
        /// Overloaded ToString to print table entries.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()}: {FirstName} {LastName} {PhoneNumber} {ApartmentName} {UnitNumber} {RequestDescription} {AllowEntry} {RequestTimestamp}";
        }
    }
}