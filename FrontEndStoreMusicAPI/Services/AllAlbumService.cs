using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Utilites;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Services
{

    interface IAllAlbumService
    {
        Task<List<AlbumDto>> GetAll(AlbumQuery searchQuery);
    }
    class AllAlbumService : IAllAlbumService
    {
        public async Task<List<AlbumDto>> GetAll(AlbumQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/album";

                var response = await HelperHttpClient.SecondGetHttp(client, searchQuery, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var albums = await response.Content.ReadFromJsonAsync<List<AlbumDto>>();
                    if (albums != null && albums.Count > 0)
                    {
                        albums = HelperHttpClient.GenerateAlbums(albums);
                        return albums;
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
