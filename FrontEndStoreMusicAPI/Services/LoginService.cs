
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Utilites;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows;

namespace FrontEndStoreMusicAPI.Services
{
    interface ILoginService
    {
        void LoginUser(LoginDto loginDto); 
    }
    class LoginService : ILoginService
    {
        public async void LoginUser(LoginDto loginDto)
        {
            using (HttpClient client = new HttpClient()) 
            {
                const string requestUri = @"api/account/login";
                HttpResponseMessage response = HelperHttpClient.GetHttpClient(client, loginDto, requestUri);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode) 
                {
                    MessageBox.Show("Generated Token JWT: " + responseBody + "\nStatus Code: " + (int)response.StatusCode + " -> " + response.StatusCode);
                }
                else
                {
                    int startIndex = responseBody.IndexOf("\"errors\"", StringComparison.OrdinalIgnoreCase);
                    if (startIndex != -1) responseBody = responseBody.Substring(startIndex);
                    MessageBox.Show("Status Code: " + (int)response.StatusCode + " -> " + response.StatusCode + "\nErrors: " + responseBody);
                }
            }
            
        }
    }
}
