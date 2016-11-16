using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using ExchangeRate;

namespace ServiceGateway.ServiceGateways
{
    public static class CurrencyConverterService
    {
        private static Iso4217 from = Iso4217.DKK;
 

        public static double GetCurrency(Iso4217 to)
        {
            if (to != from)
                return Provider.Google.Rate(from, to)/10000;
            return 1;
        }
    }
}
