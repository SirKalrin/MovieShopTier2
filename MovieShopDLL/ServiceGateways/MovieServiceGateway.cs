using System.Collections.Generic;
using System.Net.Http;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public class MovieServiceGateway : AbstractServiceGateway, IServiceGateway<Movie, int>
    {
        public MovieServiceGateway() : base()
        {

        }

        public Movie Create(Movie t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/movies", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Movie>().Result;
            }
            return null;
        }

        public Movie Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/movies/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var movie = response.Content.ReadAsAsync<Movie>().Result;
                return movie;
            }
            return null;
        }

        public List<Movie> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/movies/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Movie>>().Result;
            }
            return null;
        }

        public Movie Update(Movie t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/movies/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Movie>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/movies/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Movie>().Result != null;
            }
            return false;
        }
    }
}
