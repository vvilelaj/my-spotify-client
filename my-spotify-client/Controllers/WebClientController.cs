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
using my_spotify_client.Providers;
using my_spotify_client.Providers.SpotifyProvider;

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

        public AppSettingsManager AppSettingsManager
        {
            get
            {
                return AppSettingsManager.Instance();
            }
        }

        private SpotifyProvider SpotifyProvider
        {
            get
            {
                return SpotifyProvider.Instance();
            }
        }

        public ActionResult Index()
        {
            if (SessionManager.SpotifyToken == null) return View();

            //var 
            return null;
        }

        public ActionResult RequestAuthorizationToSpotify()
        {
            SessionManager.State = DateTime.Now.Ticks.ToString();
            return Redirect(SpotifyProvider.GetSpotifyAuthorizationUrl(SessionManager.State));
        }

        public async Task<ActionResult> ProccessSpotifyResponse(string code = "", string error = "", string state = "")
        {
            if (!string.IsNullOrWhiteSpace(error) || 
                !state.Equals(SessionManager.State)) return View(new ProccessSpotifyResponseModel(true, error));

            SessionManager.SpotifyToken = await SpotifyProvider.GetTokenAsync(code);
            return View(new ProccessSpotifyResponseModel(false, string.Empty));
        }
    }
}