using FrontEndStoreMusicAPI.Utilites;
using MusicStoreApi.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows;

namespace FrontEndStoreMusicAPI.Services
{
    interface IRegisterService
    {
        void Register(RegisterUserDto registerUserDto);
        void LoginUser(LoginDto loginDto);
    }

    class RegisterService : IRegisterService
    {
        
        public void LoginUser(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async void Register(RegisterUserDto registerUserDto)
        {

            using (HttpClient client = new HttpClient())
            { 
                client.BaseAddress = new Uri("https://localhost:7195");
                client.DefaultRequestHeaders.Add("appkey", "myapp_key");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/account/register", registerUserDto).Result;
                string responseBody = await response.Content.ReadAsStringAsync();
                responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Added new user" + "\nStatus code: " +((int)response.StatusCode) + " -> " + response.StatusCode);
                }
                else
                {
                    int startIndex = responseBody.IndexOf("\"errors\"", StringComparison.OrdinalIgnoreCase);
                    if (startIndex != -1) responseBody = responseBody.Substring(startIndex);
                    MessageBox.Show("Invalid one of values, Status Code: " + ((int)response.StatusCode) + " -> " + response.StatusCode + "\nDETAILS OF WHAT TO CORRECT: " + responseBody);
                }
            }

        }

    }
}
