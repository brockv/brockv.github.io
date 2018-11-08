using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework7.Models
{
    public class SearchRequestLog
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Property to hold the time of the request.
        /// </summary>
        public DateTime RequestTimestamp { get; set; }

        /// <summary>
        /// Property to hold the type of the request.
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// Property to hold the requestor's IP address.
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// Property to hold the client's browser agent information.
        /// </summary>
        public string BrowserAgent { get; set; }

        /// <summary>
        /// Overloaded ToString to print table entries.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()}: {RequestTimestamp} {RequestType} {ClientIP} {BrowserAgent}";
        }
    }
}
