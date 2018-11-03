using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework6.Models.ViewModels
{
    public class WWIViewModel
    {
        public Person VMPerson { get; set; }
        public Customer VMCustomer { get; set; }
        public List<InvoiceLine> VMInvoices { get; set; }

        public bool IsPrimaryContactPerson { get; set; } = false;

        public decimal VMGrossSales { get; set; }
        public decimal VMGrossProfits { get; set; }
    }
}