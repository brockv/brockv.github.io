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
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name"), StringLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number"), StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your apartment name"), StringLength(20)]
        public string ApartmentName { get; set; }

        [Required(ErrorMessage = "Please enter your unit number")]
        public int UnitNumber { get; set; }

        [Required(ErrorMessage = "Please describe your request"), StringLength(250)]
        public string RequestDescription { get; set; }

        /* 
         * This is the value for the checkbox. This, paired with IntValue,
         * will convert the standard bool value of the checkbox to an int
         * so that I can store it in the database. (1 is true, 0 is false)
         */
        public bool AllowEntry
        {
            get { return IntValue == 1; }
            set { IntValue = value ? 1 : 0; }
        }

        /* Returns 0 or 1, based on the bool value of the checkbox */
        public int IntValue { get; set; }

        public DateTime RequestTimestamp { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}: {FirstName} {LastName} {PhoneNumber} {ApartmentName} {UnitNumber} {RequestDescription} {AllowEntry} {RequestTimestamp}";
        }
    }
}