using System.Collections.Generic;
using System.Net.Http;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public class GenreServiceGateway : AbstractServiceGateway, IServiceGateway<Genre, int>
    {

        public GenreServiceGateway() : base()
        {

        }

        public Genre Create(Genre t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/genres", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Genre>().Result;
            }
            return null;
        }

        public Genre Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/genres/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Genre>().Result;
            }
            return null;
        }

        public List<Genre> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/genres/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Genre>>().Result;
            }
            return null;
        }

        public Genre Update(Genre t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/genres/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Genre>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/genres/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Genre>().Result != null;
            }
            return false;
        }


    }
}
