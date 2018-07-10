using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace my_spotify_client.Common.AppSettingsManager
{
    public class AppSettingsManager
    {
        private static AppSettingsManager _instance;

        public static AppSettingsManager Instance()
        {
            if (_instance == null) _instance = new AppSettingsManager();

            return _instance;
        }

        public string SpotifyAccountsBaseAddressUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[AppSettingsKey.SPOTIFY_ACCOUNTS_BASE_ADDRESS];
            }
        }

        public string ClientId
        {
            get
            {
                return ConfigurationManager.AppSettings[AppSettingsKey.CLIENT_ID];
            }
        }

        public string ClientSecret
        {
            get
            {
                return ConfigurationManager.AppSettings[AppSettingsKey.CLIENT_SECRET];
            }
        }

        public string RedirectUri
        {
            get
            {
                return ConfigurationManager.AppSettings[AppSettingsKey.REDIRECT_URI];
            }
        }

        public string SpotifyBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[AppSettingsKey.SPOTIFY_BASE_ADDRESS];
            }
        }
    }
}