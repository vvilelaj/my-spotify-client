using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using my_spotify_client.Providers.SpotifyProvider.Entities;
using my_spotify_client.Providers.SpotifyProvider.Entities.UserProfile;

namespace my_spotify_client.Providers.SpotifyProvider.Providers
{
    public class UserProfileProvider : SpotifyBaseProvider
    {
        private static UserProfileProvider _instance;

        public static UserProfileProvider Instance()
        {
            if (_instance == null) _instance = new UserProfileProvider();

            return _instance;
        }

        public async Task<User> GetUserProfileAsync()
        {
            var userProfile = await GetHttpClient().GetAsync(AppSettingsManager.SpotifyBaseUrl + "/v1/me").Result.Content.ReadAsAsync<User>();

            return userProfile;
        }
    }
}