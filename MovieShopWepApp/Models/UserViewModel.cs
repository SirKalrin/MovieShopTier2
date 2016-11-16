using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExchangeRate;
using ServiceGateway.Entities;

namespace MovieShopWepApp.Models
{
    public class UserViewModel
    {
        public List<Genre> Genres { get; set; }
        public List<Movie> Movies { get; set; }
        public Genre Genre { get; set; }
        public Iso4217 CurrentCurrency { get; set; }
        public int? GenreId { get; set; }
    }
}