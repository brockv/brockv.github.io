using Homework5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Homework5.DAL
{
    public class ServiceRequestFormContext : DbContext
    {
        public ServiceRequestFormContext() { }

        public virtual DbSet<ServiceRequestForm> RequestForms { get; set; }
    }
}