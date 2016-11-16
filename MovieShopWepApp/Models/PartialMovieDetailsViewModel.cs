using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExchangeRate;
using ServiceGateway.Entities;

namespace MovieShopWepApp.Models
{
    public class PartialMovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public Iso4217 CurrentCurrency { get; set; }
    }
}