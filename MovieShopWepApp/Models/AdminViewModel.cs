using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceGateway.Entities;

namespace MovieShopWepApp.Models
{
    public class AdminViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Order> Orders { get; set; }
    }
}