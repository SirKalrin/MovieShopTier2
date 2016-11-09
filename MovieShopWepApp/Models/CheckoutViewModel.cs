using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceGateway.Entities;

namespace MovieShopWepApp.Models
{
    public class CheckoutViewModel
    {
        public Customer customer { get; set; }
        public Movie movie { get; set; }
    }
}