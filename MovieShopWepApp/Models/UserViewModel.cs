using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceGateway.Entities;

namespace MovieShopWepApp.Models
{
    public class UserViewModel
    {
        public List<Genre> Genres { get; set; }
        public List<Movie> Movies { get; set; }
        public Genre Genre { get; set; }
    }
}