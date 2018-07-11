using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using my_spotify_client.Common.AppSettingsManager;
using my_spotify_client.Common.SessionManager;
using my_spotify_client.Models;

namespace my_spotify_client.Providers.SpotifyProvider
{
    public class SpotifyBaseProvider
    {
        protected AppSettingsManager AppSettingsManager
        {
            get
            {
                return AppSettingsManager.Instance();
            }
        }

        protected SessionManager SessionManager
        {
            get
            {
                return SessionManager.Instance();
            }
        }

        protected static class AuthEndPoints
        {
            public static readonly string Authorize = "/authorize";
            public static readonly string Token = "/api/token";
        }

        private async Task<SpotifyToken> GetTokenAsync()
        {
            var token = SessionManager.SpotifyToken;
            var now = DateTime.Now;
            var seconds = (now - token.CreatedDate).Seconds;
            if (token.Expires_In - seconds < 10)
            {
                var newToken = await GetRefreshedToken();
                token.Access_Token = newToken.Access_Token;
                token.CreatedDate = newToken.CreatedDate;
                token.Expires_In = newToken.Expires_In;
                token.Scope = newToken.Scope;
                SessionManager.SpotifyToken = token;
            }
            return token;
        }

        private async Task<SpotifyToken> GetRefreshedToken()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppSettingsManager.SpotifyAccountsBaseAddressUrl)
            };
            var request = new HttpRequestMessage(HttpMethod.Post, AuthEndPoints.Token);
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", SessionManager.SpotifyToken.Refresh_Token),
            };
            request.Content = new FormUrlEncodedContent(keyValues);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", GetAuthenticationHeaderInBase64());
            return await httpClient.SendAsync(request).Result.Content.ReadAsAsync<SpotifyToken>();
        }

        protected string GetAuthenticationHeaderInBase64()
        {
            var authValue = AppSettingsManager.ClientId + ":" + AppSettingsManager.ClientSecret;
            var bytes = Encoding.UTF8.GetBytes(authValue);
            var authValueBase64 = Convert.ToBase64String(bytes);
            return authValueBase64;
        }

        protected HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(AppSettingsManager.SpotifyBaseUrl)
            };
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetTokenAsync().Result.Access_Token);

            return httpClient;
        }
    }
}