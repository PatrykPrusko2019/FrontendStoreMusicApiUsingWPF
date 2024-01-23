using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Services
{
    interface ISongService
    {
        Task<List<SongDto>> GetAll(int artistId, int albumId, SongQuery searchQuery);
    }

    class SongService : ISongService
    {
        public async Task<List<SongDto>> GetAll(int artistId, int albumId, SongQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/{albumId}/song";

                var response = await HelperHttpClient.SecondGetHttp(client, searchQuery, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var songs = await response.Content.ReadFromJsonAsync<List<SongDto>>();
                    if (songs != null && songs.Count > 0)
                    {
                        return songs;
                    }
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }
    }
}
