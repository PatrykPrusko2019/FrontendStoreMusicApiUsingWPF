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

        public static HttpResponseMessage PutHttp<T>(HttpClient client, T modelDto, string requestUri)
        {
            string tokenJWT = "";
            if (MusicStoreWindow.detailsUser != null) tokenJWT = $@"{MusicStoreWindow.detailsUser.TokenJWT}";

            tokenJWT = tokenJWT.Substring(1, tokenJWT.Count() - 2);

            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $@"{tokenJWT}");
            HttpResponseMessage response = client.PutAsJsonAsync(requestUri, modelDto).Result;
            return response;
        }
        public static HttpResponseMessage PostHttp<T>(HttpClient client, T modelDto, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(requestUri, modelDto).Result;
            return response;
        }

        public async static Task<HttpResponseMessage> GetHttp<T>(HttpClient client, T value, string requestUri)
        {
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync(@$"{requestUri}/{value}");
            return response;
        }

        public async static Task<HttpResponseMessage> SecondGetHttp(HttpClient client, ArtistQuery value, string requestUri)
        {
            requestUri +=  @$"?SearchWord={value.SearchWord}&PageSize={value.PageSize}&PageNumber={value.PageNumber}&SortDirection={value.SortDirection}&SortBy={value.SortBy}";
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
            MessageBox.Show($"{extendText}Status Code: " + (int)response.StatusCode + " -> " + response.StatusCode + "\nResponse Body: " + responseBody);
        }

        internal static List<ArtistDto> GenerateAlbumsSongs(List<ArtistDto> artists)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var artist in artists) 
            {
                for (var i = 0; i < artist.Albums.Count; i++)
                {
                    sb.Append("\nAlbum:\n");
                    var album = artist.Albums[i];
                    sb.Append( $"{i+1}. Id: {album.Id}, Title: {album.Title}, Length: {album.Length}, NumbersOfSongs: {album.NumberOfSongs}, Price: {album.Price}\n");
                    if (artist.Albums != null || artist.Albums.Count != 0) sb = artist.Albums.Count == 1 ? sb.Append("Song:\n") : sb.Append("Songs:\n");
                    for (int j = 0; j < album.Songs.Count; j++)
                    {
                        var song = album.Songs[j];
                        sb.Append($"{j + 1}. Id: {song.Id}, Name: {song.Name}, AlbumId: {song.AlbumId}\n");
                    }

                }
                artist.AlbumsSongs = sb.ToString();
                sb.Clear();
            }

            return artists;
        }
    }
}
