using System.Collections.Generic;
using System.Net.Http;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public class GenreServiceGateway : AbstractServiceGateway<Genre, int>
    {

        public GenreServiceGateway() : base()
        {

        }

        public override Genre Create(Genre t)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync("api/genres", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Genre>().Result;
            }
            return null;
        }

        public override Genre Read(int id)
        {
            HttpResponseMessage response = Client.GetAsync($"api/genres/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Genre>().Result;
            }
            return null;
        }

        public override List<Genre> ReadAll()
        {
            HttpResponseMessage response = Client.GetAsync("api/genres/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Genre>>().Result;
            }
            return null;
        }

        public override Genre Update(Genre t)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/genres/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Genre>().Result;
            }
            return null;
        }

        public override bool Delete(int id)
        {
            HttpResponseMessage response = Client.DeleteAsync($"api/genres/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Genre>().Result != null;
            }
            return false;
        }


    }
}
