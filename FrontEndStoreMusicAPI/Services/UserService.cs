using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Utilites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FrontEndStoreMusicAPI.Services
{
    interface IUserService
    {
        Task<UserDto> GetUserByEmail(string email);
    }
    class UserService : IUserService
    {
        public async Task<UserDto> GetUserByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                const string requestUri = @"api/login/user";
                client.BaseAddress = new Uri("https://localhost:7195");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(@$"api/login/user/{email}");
                var user = await response.Content.ReadFromJsonAsync<UserDto>();
                if (user.DateOfBirth == null) user.DateOfBirth = new DateTime();

                if (response.IsSuccessStatusCode)
                {
                    return user;
                }
                return null;
            }
        }
    }


}
