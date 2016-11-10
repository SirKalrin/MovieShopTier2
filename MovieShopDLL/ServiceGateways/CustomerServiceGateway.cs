using System.Collections.Generic;
using System.Net.Http;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public class CustomerServiceGateway : AbstractServiceGateway<Customer, int>
    {

        public CustomerServiceGateway() : base()
        {
            
        }

        public override Customer Create(Customer t)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync("api/customers/", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Customer>().Result;
            }
            return null;
        }

        public override Customer Read(int id)
        {
            HttpResponseMessage response = Client.GetAsync($"api/customers/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Customer>().Result;
            }
            return null;
        }

        public override List<Customer> ReadAll()
        {
            HttpResponseMessage response = Client.GetAsync("api/customers/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Customer>>().Result;
            }
            return null;
        }

        public override Customer Update(Customer t)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/customers/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Customer>().Result;
            }
            return null;
        }

        public override bool Delete(int id)
        {
            HttpResponseMessage response = Client.DeleteAsync($"api/customers/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Customer>().Result != null;
            }
            return false;
        }
    }
}
