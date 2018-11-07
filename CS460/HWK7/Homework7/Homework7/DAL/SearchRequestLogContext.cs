using Homework7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Homework7.DAL
{
    public class SearchRequestLogContext : DbContext
    {
        /// <summary>
        /// Constructor which links the database to the web application
        /// </summary>
        public SearchRequestLogContext() : base("name=RequestsDatabase") { }

        /// <summary>
        /// Used to let us both query the database and save instances of it.
        /// </summary>
        public virtual DbSet<SearchRequestLog> SearchRequests { get; set; }
    }
}