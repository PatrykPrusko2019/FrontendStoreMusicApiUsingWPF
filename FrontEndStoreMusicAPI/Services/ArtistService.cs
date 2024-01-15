
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View;
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
        void Update(UpdateArtistDto updateArtistDto);
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
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);  
                }
                return null;
            }
        }

        
        public async void Update(UpdateArtistDto updateArtistDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{updateArtistDto.Id}";

                var response = await HelperHttpClient.PutHttp(client, updateArtistDto, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "Updated Artist");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
            }
        }
    }
}
