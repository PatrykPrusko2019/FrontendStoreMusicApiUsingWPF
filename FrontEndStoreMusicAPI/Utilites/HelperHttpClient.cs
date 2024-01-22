using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace FrontEndStoreMusicAPI.Utilites
{
     static class HelperHttpClient
    {
        private const string uri = @"https://localhost:7195";

        private static string GetTokenJWT()
        {
            string tokenJWT = "";
            if (MusicStoreWindow.DetailsUser != null)
            {
                tokenJWT = $@"{MusicStoreWindow.DetailsUser.TokenJWT}";
                tokenJWT = tokenJWT.Substring(1, tokenJWT.Count() - 2);
                return tokenJWT;
            }
            return null;
        }

        public static HttpResponseMessage DeleteHttp(HttpClient client, int artistId, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            var response = client.DeleteAsync(@$"{requestUri}/{artistId}").Result;
            return response;
        }

        public static HttpResponseMessage DeleteAllHttp(HttpClient client, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            var response = client.DeleteAsync(requestUri).Result;
            return response;
        }

        public static HttpResponseMessage PutHttp<T>(HttpClient client, T modelDto, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            HttpResponseMessage response = client.PutAsJsonAsync(requestUri, modelDto).Result;
            return response;
        }

        public static HttpResponseMessage PostHttp<T>(HttpClient client, T modelDto, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            HttpResponseMessage response = client.PostAsJsonAsync(requestUri, modelDto).Result;
            return response;
        }

        public async static Task<HttpResponseMessage> GetHttp<T>(HttpClient client, T value, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            var response = await client.GetAsync(@$"{requestUri}/{value}");
            return response;
        }

        public async static Task<HttpResponseMessage> SecondGetHttp(HttpClient client, ArtistQuery value, string requestUri)
        {
            requestUri +=  @$"?SearchWord={value.SearchWord}&PageSize={value.PageSize}&PageNumber={value.PageNumber}&SortDirection={value.SortDirection}&SortBy={value.SortBy}";
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string tokenJWT = GetTokenJWT();
            if (tokenJWT != null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");

            var  response = await client.GetAsync(requestUri);
           
            return response;
        }

        public async static void GetResponseBodyError(HttpResponseMessage response)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            int startIndex = responseBody.IndexOf("\"errors\"", StringComparison.OrdinalIgnoreCase);
            if (startIndex != -1) responseBody = responseBody.Substring(startIndex);
            MessageBox.Show("Status Code: " + (int)response.StatusCode + " -> " + response.StatusCode + "\nErrors: " + responseBody);
        }

        public async static void GetResponseBodyOk(HttpResponseMessage response, string extendText)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            MessageBox.Show($"{extendText}\nStatus Code: " + (int)response.StatusCode + " -> " + response.StatusCode + (responseBody = responseBody != null ? "\nResponse Body: " + responseBody : ""));
        }

        public static List<ArtistDto> GenerateAlbumsSongsForArtists(List<ArtistDto> artists)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < artists.Count; i++)
            {
                var artist = artists[i];
                artist = GenerateAlbumsSongsForArtist(artist);
            }
            return artists;
        }

        public static ArtistDto GenerateAlbumsSongsForArtist(ArtistDto artist)
        {
            StringBuilder sb = new StringBuilder();
            
                for (var i = 0; i < artist.Albums.Count; i++)
                {
                    sb.Append("\nAlbum:\n");
                    var album = artist.Albums[i];
                    sb.Append($"{i + 1}. Id: {album.Id}, Title: {album.Title}, Length: {album.Length}, NumbersOfSongs: {album.NumberOfSongs}, Price: {album.Price}\n");
                    if (artist.Albums != null || artist.Albums.Count != 0) sb = artist.Albums.Count == 1 ? sb.Append("Song:\n") : sb.Append("Songs:\n");
                    for (int j = 0; j < album.Songs.Count; j++)
                    {
                        var song = album.Songs[j];
                        sb.Append($"{j + 1}. Id: {song.Id}, Name: {song.Name}, albumId: {song.AlbumId}\n");
                    }

                }
                artist.AlbumsSongs = sb.ToString();
               
            return artist;
        }

        internal static List<AlbumDto> GenerateAlbums(List<AlbumDto> albums)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < albums.Count; i++)
            {
                var album = albums[i];

                for (int j = 0; j < album.Songs.Count; j++)
                {
                    var song = album.Songs[j];
                    sb.Append($"{j + 1}. Id: {song.Id}, Name: {song.Name}, albumId: {song.AlbumId}\n");
                }
                album.ListOfSongs = sb.ToString();
                sb.Clear();
            }
            return albums;
        }

        internal static DetailsAlbumDto GenerateSongsForAlbum(DetailsAlbumDto detailsAlbumDto)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < detailsAlbumDto.Songs.Count; j++)
            {
                var song = detailsAlbumDto.Songs[j];
                sb.Append($"{j + 1}. Id: {song.Id}, Name: {song.Name}, albumId: {song.AlbumId}\n");
            }
            detailsAlbumDto.ListOfSongs = sb.ToString();
            return detailsAlbumDto;
        }
    }
}
