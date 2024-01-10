using FrontEndStoreMusicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Utilites
{
     static class HelperHttpClient
    {
        private const string uri = @"https://localhost:7195";
        public static HttpResponseMessage GetHttpPost<T>(HttpClient client, T modelDto, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(requestUri, modelDto).Result;
            return response;
        }

        

    }
}
