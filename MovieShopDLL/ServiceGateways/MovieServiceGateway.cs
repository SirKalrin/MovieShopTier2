﻿using System.Collections.Generic;
using System.Net.Http;
using ServiceGateway.Entities;

namespace ServiceGateway.ServiceGateways
{
    public class MovieServiceGateway : AbstractServiceGateway<Movie, int>
    {
        public MovieServiceGateway() : base()
        {

        }

        public override Movie Create(Movie t)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync("api/movies", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Movie>().Result;
            }
            return null;
        }

        public override Movie Read(int id)
        {
            HttpResponseMessage response = Client.GetAsync($"api/movies/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Movie>().Result;
            }
            return null;
        }

        public override List<Movie> ReadAll()
        {
            HttpResponseMessage response = Client.GetAsync("api/movies/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Movie>>().Result;
            }
            return null;
        }

        public override Movie Update(Movie t)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync("api/movies", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Movie>().Result;
            }
            return null;
        }

        public override bool Delete(int id)
        {
            HttpResponseMessage response = Client.DeleteAsync($"api/movies/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Movie>().Result != null;
            }
            return false;
        }
    }
}