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
        bool Create(int artistId, CreateAlbumDto createAlbumDto);
        Task<List<AlbumDto>> GetAll(int artistId, AlbumQuery searchQuery);
        Task<AlbumDto> GetById(int artistId, int albumId);
        bool Update(int artistId, int albumId, UpdateAlbumDto updateAlbumDto);
        public void DeleteById(int artistId, int albumId);
        void DeleteAll(int artistId);
        Task<DetailsAlbumDto> GetDetails(int artistId, int albumId);
    }
    class AlbumService : IAlbumService
    {
        public bool Create(int artistId, CreateAlbumDto createAlbumDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album";

                var response = HelperHttpClient.PostHttp(client, createAlbumDto, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, $"Created and Added new Album to id Artist: {artistId}");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<List<AlbumDto>> GetAll(int artistId, AlbumQuery searchQuery)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album";

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

        public async Task<AlbumDto> GetById(int artistId, int albumId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album";

                var response = await HelperHttpClient.GetHttp(client, albumId, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var albumDto = await response.Content.ReadFromJsonAsync<AlbumDto>();
                    if (albumDto != null ) return albumDto;
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }

        public async Task<DetailsAlbumDto> GetDetails(int artistId, int albumId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/details";

                var response = await HelperHttpClient.GetHttp(client, albumId, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var detailsArtistDto = await response.Content.ReadFromJsonAsync<DetailsAlbumDto>();
                    if (detailsArtistDto != null) return detailsArtistDto;
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }

        public bool Update(int artistId, int albumId, UpdateAlbumDto updateAlbumDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/{albumId}";

                var response = HelperHttpClient.PutHttp(client, updateAlbumDto, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, $"Updated Album");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return response.IsSuccessStatusCode;
            }
        }
        public void DeleteById(int artistId, int albumId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album";

                var response = HelperHttpClient.DeleteHttp(client, albumId, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "Deleted Album");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
            }
        }

        public void DeleteAll(int artistId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album";

                var response = HelperHttpClient.DeleteAllHttp(client, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "Deleted Album");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
            }
        }

    }
}
