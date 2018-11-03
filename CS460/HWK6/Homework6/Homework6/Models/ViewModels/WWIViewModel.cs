using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework6.Models.ViewModels
{
    /// <summary>
    /// ViewModel constructed for use in the 'ViewDetails' view.
    /// </summary>
    public class WWIViewModel
    {
        /* Initialize the models we'll need */
        public Person VMPerson { get; set; }
        public Customer VMCustomer { get; set; }
        public List<InvoiceLine> VMInvoices { get; set; }

        /* Initialize the variables we'll need */
        public int VMCustomerID { get; set; } = 0;
        public bool VMIsPrimaryContactPerson { get; set; } = false;
        public decimal VMGrossSales { get; set; } = 0;
        public decimal VMGrossProfits { get; set; } = 0;
    }
}