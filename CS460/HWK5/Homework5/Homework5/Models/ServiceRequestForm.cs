using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework5.Models
{
    public class ServiceRequestForm
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter your first name"), StringLength(20)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name"), StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number"), StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your apartment name"), StringLength(20)]
        [Display(Name = "Apartments")]
        public string ApartmentName { get; set; }

        [Required(ErrorMessage = "Please enter your unit number")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Unit Number must be positive")]
        [Display(Name = "Unit Number")]
        public int UnitNumber { get; set; }

        [Required(ErrorMessage = "Please describe your request"), StringLength(250)]
        [Display(Name = "Request Description")]
        public string RequestDescription { get; set; }

        [Display(Name = "Entry Allowed?")]
        public bool AllowEntry { get; set; }

        [Display(Name = "Date Submitted")]
        public DateTime RequestTimestamp { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}: {FirstName} {LastName} {PhoneNumber} {ApartmentName} {UnitNumber} {RequestDescription} {AllowEntry} {RequestTimestamp}";
        }
    }
}