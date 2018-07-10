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
using my_spotify_client.Providers.SpotifyProvider.Providers;

namespace my_spotify_client.Providers.SpotifyProvider
{
    public class SpotifyProvider : SpotifyBaseProvider
    {
        private static SpotifyProvider _instance;

        public static SpotifyProvider Instance()
        {
            if (_instance == null) _instance = new SpotifyProvider();

            return _instance;
        }

        public AuthProvider Auth
        {
            get
            {
                return AuthProvider.Instance();
            }
        }

        public UserProfileProvider UserProfile
        {
            get
            {
                return UserProfileProvider.Instance();
            }
        }
    }
}