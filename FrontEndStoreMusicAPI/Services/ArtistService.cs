
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Utilites;
using MusicStoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Services
{
    interface IArtistService
    {
        Task<PageResult<Models.ArtistDto>> GetAll(ArtistQuery searchQuery);
    }

    class ArtistService : IArtistService
    {
        public async Task<PageResult<Models.ArtistDto>> GetAll(ArtistQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist";
                var response = await HelperHttpClient.GetHttp(client, searchQuery, requestUri);
                var user = await response.Content.ReadFromJsonAsync<PageResult<Models.ArtistDto>>();

                if (response.IsSuccessStatusCode)
                {
                    return user;
                }
                return null;
            }
        }
    }
}
