
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Utilites;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace FrontEndStoreMusicAPI.Services
{
    interface ILoginService
    {
        Task<string> LoginUser(LoginDto loginDto); 
    }
    class LoginService : ILoginService
    {
        public async Task<string> LoginUser(LoginDto loginDto)
        {
            using (HttpClient client = new HttpClient()) 
            {
                const string requestUri = @"api/account/login";
                HttpResponseMessage response = HelperHttpClient.GetHttpPost(client, loginDto, requestUri);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode) 
                {
                    return responseBody;
                }
                else
                {
                    int startIndex = responseBody.IndexOf("\"errors\"", StringComparison.OrdinalIgnoreCase);
                    if (startIndex != -1) responseBody = responseBody.Substring(startIndex);
                    MessageBox.Show("Status Code: " + (int)response.StatusCode + " -> " + response.StatusCode + "\nErrors: " + responseBody);
                }
                return "";
            }
            
        }
    }
}
