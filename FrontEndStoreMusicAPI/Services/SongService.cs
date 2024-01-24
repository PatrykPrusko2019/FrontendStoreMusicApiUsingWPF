using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Models.Create;
using FrontEndStoreMusicAPI.Models.Details;
using FrontEndStoreMusicAPI.Models.Update;
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
        bool Create(int artistId, int albumId, CreateSongDto createSongDto);
        bool DeleteAll(int artistId, int albumId);
        void DeleteById(int artistId, int albumId, int songId);
        Task<List<SongDto>> GetAll(int artistId, int albumId, SongQuery searchQuery);
        Task<SongDto> GetById(int artistId, int albumId, int songId);
        Task<DetailsSongDto> GetDetails(int artistId, int albumId, int songId);
        bool Update(int artistId, int albumId, int songId, UpdateSongDto updateSongDto);
    }

    class SongService : ISongService
    {
        public bool Create(int artistId, int albumId, CreateSongDto createSongDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/{albumId}/song";

                var response = HelperHttpClient.PostHttp(client, createSongDto, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, $"Created and Added new Song to id Album: {albumId}, id Artist: {artistId}");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return response.IsSuccessStatusCode;
            }
        }

        public bool Update(int artistId, int albumId, int songId, UpdateSongDto updateSongDto)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/{albumId}/song/{songId}";

                var response = HelperHttpClient.PutHttp(client, updateSongDto, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, $"Updated Song");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return response.IsSuccessStatusCode;
            }
        }

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

        public async Task<SongDto> GetById(int artistId, int albumId, int songId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/{albumId}/song";

                var response = await HelperHttpClient.GetHttp(client, songId, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var songDto = await response.Content.ReadFromJsonAsync<SongDto>();
                    if (songDto != null) return songDto;
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }

        public async Task<DetailsSongDto> GetDetails(int artistId, int albumId, int songId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/{albumId}/song/details";

                var response = await HelperHttpClient.GetHttp(client, songId, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var detailsSongDto = await response.Content.ReadFromJsonAsync<DetailsSongDto>();
                    if (detailsSongDto != null) return detailsSongDto;
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
                return null;
            }
        }

        public void DeleteById(int artistId, int albumId, int songId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/{albumId}/song";

                var response = HelperHttpClient.DeleteHttp(client, songId, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "Deleted Song");
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                }
            }
        }

        public bool DeleteAll(int artistId, int albumId)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $@"api/artist/{artistId}/album/{albumId}/song";

                var response = HelperHttpClient.DeleteAllHttp(client, requestUri);

                if (response.IsSuccessStatusCode)
                {
                    HelperHttpClient.GetResponseBodyOk(response, "Deleted Songs");
                    return true;
                }
                else
                {
                    HelperHttpClient.GetResponseBodyError(response);
                    return false;
                }
            }
        }

        
    }
}
