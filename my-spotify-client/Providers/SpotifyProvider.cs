﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using my_spotify_client.Common.AppSettingsManager;
using my_spotify_client.Controllers;
using my_spotify_client.Models;

namespace my_spotify_client.Providers
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

        public string GetSpotifyAuthorizationUrl(string state)
        {
            var authorizeUrl = AppSettingsManager.SpotifyAccountsUrl + "/authorize?";

            authorizeUrl += "client_id=" + AppSettingsManager.ClientId + "&";
            authorizeUrl += "response_type=code&";
            authorizeUrl += "redirect_uri=" + AppSettingsManager.RedirectUri + "&";
            authorizeUrl += "scope=user-read-private user-read-email&";
            authorizeUrl += "state=" + state;

            return authorizeUrl;
        }

        public async Task<SpotifyToken> GetSpotifyTokenAsync(string code)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppSettingsManager.SpotifyAccountsUrl)
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/token");
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", AppSettingsManager.RedirectUri),
                new KeyValuePair<string, string>("client_id", AppSettingsManager.ClientId),
                new KeyValuePair<string, string>("client_secret", AppSettingsManager.ClientSecret)
            };
            request.Content = new FormUrlEncodedContent(keyValues);

            return await httpClient.SendAsync(request).Result.Content.ReadAsAsync<SpotifyToken>();
        }
    }
}