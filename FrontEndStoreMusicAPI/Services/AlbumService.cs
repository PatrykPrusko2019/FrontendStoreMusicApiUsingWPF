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
    interface IAlbumService
    {
        Task<List<AlbumDto>> GetAll(int artistId);
    }
    class AlbumService : IAlbumService
    {
        public async Task<List<AlbumDto>> GetAll(int artistId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist";

                var response = await HelperHttpClient.GetHttp(client, @$"{artistId}/album", requestUri);

                var albums = await response.Content.ReadFromJsonAsync<List<AlbumDto>>();

                if (response.IsSuccessStatusCode)
                {
                    albums = HelperHttpClient.GenerateAlbums(albums);
                    return albums;
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
