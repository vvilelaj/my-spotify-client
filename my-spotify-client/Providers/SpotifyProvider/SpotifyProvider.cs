using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using my_spotify_client.Common.AppSettingsManager;
using my_spotify_client.Common.SessionManager;
using my_spotify_client.Models;
using my_spotify_client.Providers.SpotifyProvider.Entities;

namespace my_spotify_client.Providers.SpotifyProvider
{
    public class SpotifyProvider
    {
        private static SpotifyProvider _instance;

        public static SpotifyProvider Instance()
        {
            if (_instance == null) _instance = new SpotifyProvider();

            return _instance;
        }

        public AppSettingsManager AppSettingsManager
        {
            get
            {
                return AppSettingsManager.Instance();
            }
        }

        private SessionManager SessionManager
        {
            get
            {
                return SessionManager.Instance();
            }
        }

        public static class SpotifyEndPoints
        {
            public static readonly string Authorize = "/authorize";
            public static readonly string token = "/api/token";
        }

        public string GetAuthorizationUrl(string state)
        {
            var authorizeUrl = AppSettingsManager.SpotifyAccountsBaseAddressUrl + SpotifyEndPoints.Authorize + "?";

            authorizeUrl += "client_id=" + AppSettingsManager.ClientId + "&";
            authorizeUrl += "response_type=code&";
            authorizeUrl += "redirect_uri=" + AppSettingsManager.RedirectUri + "&";
            authorizeUrl += "scope=user-read-private user-read-email&";
            authorizeUrl += "state=" + state;

            return authorizeUrl;
        }

        public async Task LoadAuthorizationTokenAsync(string code)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppSettingsManager.SpotifyAccountsBaseAddressUrl)
            };
            var request = new HttpRequestMessage(HttpMethod.Post, SpotifyEndPoints.token);
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", AppSettingsManager.RedirectUri),
            };
            request.Content = new FormUrlEncodedContent(keyValues);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", GetAuthenticationHeaderInBase64());

            SessionManager.SpotifyToken = await httpClient.SendAsync(request).Result.Content.ReadAsAsync<SpotifyToken>();
        }

        public async Task<SpotifyToken> GetTokenAsync()
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
            var request = new HttpRequestMessage(HttpMethod.Post, SpotifyEndPoints.token);
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", SessionManager.SpotifyToken.Refresh_Token),
            };
            request.Content = new FormUrlEncodedContent(keyValues);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", GetAuthenticationHeaderInBase64());
            return await httpClient.SendAsync(request).Result.Content.ReadAsAsync<SpotifyToken>();
        }

        private string GetAuthenticationHeaderInBase64()
        {
            var authValue = AppSettingsManager.ClientId + ":" + AppSettingsManager.ClientSecret;
            var bytes = Encoding.UTF8.GetBytes(authValue);
            var authValueBase64 = Convert.ToBase64String(bytes);
            return authValueBase64;
        }

        public async Task<UserProfile> GetUserProfileAsync()
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(AppSettingsManager.SpotifyBaseUrl)
            };
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", GetTokenAsync().Result.Access_Token);

            var userProfile = await httpClient.GetAsync(AppSettingsManager.SpotifyBaseUrl + "/v1/me").Result.Content.ReadAsAsync<UserProfile>();

            return userProfile;
        }

    }
}