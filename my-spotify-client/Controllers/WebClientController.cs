using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using my_spotify_client.Common;
using my_spotify_client.Common.AppSettingsManager;
using my_spotify_client.Common.SessionManager;
using my_spotify_client.Models;

namespace my_spotify_client.Controllers
{
    public partial class WebClientController : Controller
    {
        private SessionManager SessionManager
        {
            get
            {
                return Common.SessionManager.SessionManager.Instance();
            }
        }

        private AppSettingsManager AppSettingsManager
        {
            get
            {
                return AppSettingsManager.Instance();
            }
        }

        public ActionResult Index()
        {
            if (SessionManager.SpotifyToken == null) return View();//View(null);
            return null;
        }

        public ActionResult RequestAuthorizationToSpotify()
        {
            SessionManager.State = DateTime.Now.Ticks.ToString();
            //
            var authorizeUrl = AppSettingsManager.SpotifyAccountsUrl + "/authorize?";
            authorizeUrl += "client_id=" + AppSettingsManager.ClientId + "&";
            authorizeUrl += "response_type=code&";
            authorizeUrl += "redirect_uri=" + AppSettingsManager.RedirectUri + "&";
            authorizeUrl += "scope=user-read-private user-read-email&";
            authorizeUrl += "state=" + SessionManager.State;

            return Redirect(authorizeUrl);
        }

        public async Task<ActionResult> ProccessSpotifyResponse(string code = "", string error = "", string state = "")
        {
            if (!string.IsNullOrWhiteSpace(error) || !state.Equals(SessionManager.State)) return View(new ProccessSpotifyResponseModel(true, error));

            SessionManager.SpotifyToken = await GetSpotifyTokenAsync(code);

            return View(new ProccessSpotifyResponseModel(false, string.Empty));
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
                new KeyValuePair<string, string>("redirect_uri",AppSettingsManager.RedirectUri),
                new KeyValuePair<string, string>("client_id",AppSettingsManager.ClientId),
                new KeyValuePair<string, string>("client_secret",AppSettingsManager.ClientSecret)
            };
            request.Content = new FormUrlEncodedContent(keyValues);

            return await httpClient.SendAsync(request).Result.Content.ReadAsAsync<SpotifyToken>();
        }
    }
}