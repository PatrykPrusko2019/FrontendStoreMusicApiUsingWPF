
using FrontEndStoreMusicAPI.Models;
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
                client.BaseAddress = new Uri("https://localhost:7195");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/account/login", loginDto).Result;
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode) 
                {
                    MessageBox.Show("Generated Token JWT: " + responseBody + "\nStatus Code: " + (int)response.StatusCode + " -> " + response.StatusCode);
                }
                else
                {
                    MessageBox.Show("Status Code: " + (int)response.StatusCode + " -> " + response.StatusCode + "\nErrors: " + responseBody);
                }
            }
            
        }
    }
}
