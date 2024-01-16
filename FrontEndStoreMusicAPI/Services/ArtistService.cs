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
        Task<DetailsArtistDto> GetDetails(int id);
        bool Update(UpdateArtistDto updateArtistDto);
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

        public async Task<DetailsArtistDto> GetDetails(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/details";

                var response = await HelperHttpClient.GetHttp(client, id, requestUri);

                var artist = await response.Content.ReadFromJsonAsync<DetailsArtistDto>();

                if (response.IsSuccessStatusCode)
                {
                    return artist;
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }


        public bool Update(UpdateArtistDto updateArtistDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{updateArtistDto.Id}";

                var response = HelperHttpClient.PutHttp(client, updateArtistDto, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "Updated Artist");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return response.IsSuccessStatusCode;
            }
        }
    }
}
