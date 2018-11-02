using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework6.Models.ViewModels
{
    public class MyViewModel
    {
        public Person MyViewModelPerson { get; set; }
        public Customer MyViewModelCustomer { get; set; }
        public List<InvoiceLine> MyViewModelInvoice { get; set; }
    }
}