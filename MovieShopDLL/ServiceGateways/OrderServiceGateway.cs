using System.Collections.Generic;
using System.Net.Http;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public class OrderServiceGateway : AbstractServiceGateway, IServiceGateway<Order, int>
    {
        public OrderServiceGateway() : base()
        {

        }

        public Order Create(Order t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/orders", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Order>().Result;
            }
            return null;
        }

        public Order Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/orders/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Order>().Result;
            }
            return null;
        }

        public List<Order> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/orders/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Order>>().Result;
            }
            return null;
        }

        public Order Update(Order t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/orders/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Order>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/orders/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Order>().Result != null;
            }
            return false;
        }
    }
}
