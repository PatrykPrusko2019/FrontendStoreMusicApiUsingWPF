﻿using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Models.Details;
using FrontEndStoreMusicAPI.Utilites;
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
        Task<List<DetailsArtistDto>> GetDetailsArtistsByUserId(int userId);
    }
    class UserService : IUserService
    {
        public async Task<UserDto> GetUserByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/login/user";
                var response = await HelperHttpClient.GetHttp(client, email, requestUri);
                var user = await response.Content.ReadFromJsonAsync<UserDto>();
                
                if (response.IsSuccessStatusCode)
                {
                    return user;
                }
                return null;
            }
        }

        public async Task<List<DetailsArtistDto>> GetDetailsArtistsByUserId(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/login/user";
                var response = await HelperHttpClient.GetHttp(client, @$"{userId}/artist" ,requestUri);
                var detailsArtists = await response.Content.ReadFromJsonAsync<List<DetailsArtistDto>>();

                if (response.IsSuccessStatusCode)
                {
                    return detailsArtists;
                }
                return null;
            }
        }
    }


}
