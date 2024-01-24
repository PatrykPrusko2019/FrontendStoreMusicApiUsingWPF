using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Models.Create;
using FrontEndStoreMusicAPI.Models.Details;
using FrontEndStoreMusicAPI.Models.Update;
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
        bool Create(CreateArtistDto createArtistDto);
        void Delete(int artistId);
        Task<PageResult<ArtistDto>> GetAll(ArtistQuery searchQuery);
        Task<DetailsArtistDto> GetDetails(int id);
        bool Update(int id, UpdateArtistDto updateArtistDto);
    }

    class ArtistService : IArtistService
    {
        public bool Create(CreateArtistDto createArtistDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = @"api/artist";

                var response = HelperHttpClient.PostHttp(client, createArtistDto, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "Created and Added new Artist");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return response.IsSuccessStatusCode;
            }
        }

        public void Delete(int artistId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = @"api/artist";

                var response = HelperHttpClient.DeleteHttp(client, artistId, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "Deleted Artist");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
            }
        }

        public async Task<PageResult<ArtistDto>> GetAll(ArtistQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist";

                var response = await HelperHttpClient.SecondGetHttp(client, searchQuery, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var artists = await response.Content.ReadFromJsonAsync<PageResult<ArtistDto>>();
                    if (artists != null && artists.Items != null && artists.Items.Count > 0)
                    {
                        artists.Items = HelperHttpClient.GenerateAlbumsSongsForArtists(artists.Items);
                        return artists;
                    }
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

                if (response.IsSuccessStatusCode)
                {
                    var artist = await response.Content.ReadFromJsonAsync<DetailsArtistDto>();
                    if (artist != null) return artist;
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }


        public bool Update(int id, UpdateArtistDto updateArtistDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{id}";

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
