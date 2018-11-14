using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework8.Models.ViewModels
{
    public class AuctionHouseVM
    {
        /* Initialize the models we'll need */
        public Item VMItem { get; set; }
        public List<Bid> VMBids { get; set; }
    }
}