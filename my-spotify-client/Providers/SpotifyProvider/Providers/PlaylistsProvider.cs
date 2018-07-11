using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using my_spotify_client.Providers.SpotifyProvider.Entities;
using my_spotify_client.Providers.SpotifyProvider.Entities.Playlists;
using my_spotify_client.Providers.SpotifyProvider.Entities.UserProfile;

namespace my_spotify_client.Providers.SpotifyProvider.Providers
{
    public class PlaylistsProvider : SpotifyBaseProvider
    {
        private static PlaylistsProvider _instance;

        public static PlaylistsProvider Instance()
        {
            if (_instance == null) _instance = new PlaylistsProvider();

            return _instance;
        }

        public async Task<Paging<Playlist>> GetPlaylistsAsync()
        {
            var userProfile = await GetHttpClient().GetAsync(AppSettingsManager.SpotifyBaseUrl + "/v1/me/playlists").Result.Content.ReadAsAsync<Paging<Playlist>>();

            return userProfile;
        }
    }
}