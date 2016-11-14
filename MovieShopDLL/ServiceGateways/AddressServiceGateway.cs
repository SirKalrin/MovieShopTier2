using System.Collections.Generic;
using System.Net.Http;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public class AddressServiceGateway : AbstractServiceGateway, IServiceGateway<Address, int>
    {
        public AddressServiceGateway() : base()
        {

        }

        public Address Create(Address t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/addresses", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Address>().Result;
            }
            return null;
        }

        public Address Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/addresses/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Address>().Result;
            }
            return null;
        }

        public List<Address> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/addresses/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Address>>().Result;
            }
            return null;
        }

        public Address Update(Address t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/addresses/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Address>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/addresses/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Address>().Result != null;
            }
            return false;
        }
    }
}
