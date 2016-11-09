﻿using System.Collections.Generic;
using System.Net.Http;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public class OrderServiceGateway : AbstractServiceGateway<Order, int>
    {
        public OrderServiceGateway() : base()
        {

        }

        public override Order Create(Order t)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync("api/orders", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Order>().Result;
            }
            return null;
        }

        public override Order Read(int id)
        {
            HttpResponseMessage response = Client.GetAsync($"api/orders/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Order>().Result;
            }
            return null;
        }

        public override List<Order> ReadAll()
        {
            HttpResponseMessage response = Client.GetAsync("api/orders/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Order>>().Result;
            }
            return null;
        }

        public override Order Update(Order t)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync("api/orders", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Order>().Result;
            }
            return null;
        }

        public override bool Delete(int id)
        {
            HttpResponseMessage response = Client.DeleteAsync($"api/orders/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Order>().Result != null;
            }
            return false;
        }
    }
}
