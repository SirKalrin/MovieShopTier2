using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using MovieShopWepApp.Models;
using Newtonsoft.Json.Linq;

namespace ServiceGateway.ServiceGateways
{
    public class AuthorizationServiceGateway
    {
        HttpClient client = new HttpClient();

        public AuthorizationServiceGateway()
        {
            string baseAddress = WebConfigurationManager.AppSettings["MovieShopRestBaseAddress"];
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        public HttpResponseMessage Register(RegisterViewModel model)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("api/account/register", model).Result;
            return response;
        }

        public HttpResponseMessage Login(string userName, string password)
        {
            //setup login data
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password)
            });

            //Request token
            HttpResponseMessage response = client.PostAsync("/token", formContent).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(responseJson);
                string token = jObject.GetValue("access_token").ToString();
                HttpContext.Current.Session["token"] = token;
            }

            return response;
        }

        //private void AddAuthorizationHeader()
        //{
        //    if (HttpContext.Current.Session["token"] != null)
        //    {
        //        string token = HttpContext.Current.Session["token"].ToString();
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        //    }
        //}

    }
}
