using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using my_spotify_client.Models;

namespace my_spotify_client.Providers.SpotifyProvider.Providers
{
    public class AuthProvider : SpotifyBaseProvider
    {
        private static AuthProvider _instance;

        public static AuthProvider Instance()
        {
            if (_instance == null) _instance = new AuthProvider();

            return _instance;
        }

        public string GetAuthorizationUrl(string state)
        {
            var authorizeUrl = AppSettingsManager.SpotifyAccountsBaseAddressUrl + AuthEndPoints.Authorize + "?";

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
            var request = new HttpRequestMessage(HttpMethod.Post, AuthEndPoints.Token);
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
    }
}