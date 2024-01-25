
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Utilites;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace FrontEndStoreMusicAPI.Services
{
    interface IRegisterService
    {
        Task<bool> Register(RegisterUserDto registerUserDto);
    }

    class RegisterService : IRegisterService
    {
        public async Task<bool> Register(RegisterUserDto registerUserDto)
        {

            using (HttpClient client = new HttpClient())
            {
                const string requestUri = @"api/account/register";
                HttpResponseMessage response = HelperHttpClient.PostHttp(client, registerUserDto, requestUri);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Added new user" + "\nStatus code: " +(int)response.StatusCode + " -> " + response.StatusCode);
                    return true;
                }
                else
                {
                    int startIndex = responseBody.IndexOf("\"errors\"", StringComparison.OrdinalIgnoreCase);
                    if (startIndex != -1) responseBody = responseBody.Substring(startIndex);
                    MessageBox.Show("Invalid one of values, Status Code: " + (int)response.StatusCode + " -> " + response.StatusCode + "\nDETAILS OF WHAT TO CORRECT: " + responseBody);
                    return false;
                }
            }

        }

    }
}
