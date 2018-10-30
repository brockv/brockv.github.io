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
        /// <summary>
        /// Constructor which links the database to the web application
        /// </summary>
        public ServiceRequestFormContext() : base("name=RequestsDatabase") { }

        /// <summary>
        /// Used to let us both query the database and save instances of it.
        /// </summary>
        public virtual DbSet<ServiceRequestForm> RequestForms { get; set; }
    }
}