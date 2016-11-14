using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public abstract class AbstractServiceGateway<T, K> : IServiceGateway<T, K> where T : AbstractEntity
    {

        protected HttpClient Client = new HttpClient();

        protected AbstractServiceGateway()
        {
            Client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["MovieShopRestBaseAddress"]);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        protected void AddAuthorizationHeader()
        {
            if (HttpContext.Current.Session["token"] != null)
            {
                string token = HttpContext.Current.Session["token"].ToString();
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }

        public abstract T Create(T t);
        public abstract T Read(K id);
        public abstract List<T> ReadAll();
        public abstract T Update(T t);
        public abstract bool Delete(K id);
    }
}
