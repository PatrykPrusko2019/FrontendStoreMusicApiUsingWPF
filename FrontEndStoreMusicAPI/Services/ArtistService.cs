
using FrontEndStoreMusicAPI.Models;
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
    interface IArtistService
    {
        Task<PageResult<ArtistDto>> GetAll(ArtistQuery searchQuery);
    }

    class ArtistService : IArtistService
    {
        public async Task<PageResult<ArtistDto>> GetAll(ArtistQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist";

                var response = await HelperHttpClient.SecondGetHttp(client, searchQuery, requestUri);

                var artists = await response.Content.ReadFromJsonAsync<PageResult<ArtistDto>>();

                if (response.IsSuccessStatusCode)
                {   
                    artists.Items = HelperHttpClient.GenerateAlbumsSongs(artists.Items);
                    return artists;
                }
                return null;
            }
        }

        
        public async Task<ArtistDto> Update(UpdateArtistDto updateArtistDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist";

                var response = HelperHttpClient.PutHttp(client, updateArtistDto, requestUri);

                var artists = await response.Content.ReadFromJsonAsync<PageResult<ArtistDto>>();

                if (response.IsSuccessStatusCode)
                {
                    artists.Items = HelperHttpClient.GenerateAlbumsSongs(artists.Items);
                    return null;
                }
                return null;
            }
        }
    }
}
