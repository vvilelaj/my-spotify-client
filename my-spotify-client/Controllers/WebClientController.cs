using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using my_spotify_client.Common;

namespace my_spotify_client.Controllers
{
    public partial class WebClientController : Controller
    {
        // GET: WebClient
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RequestAuthorizationToSpotify()
        {
            var authorizeUrl = "https://accounts.spotify.com/authorize?";
            //
            var clientId = ConfigurationManager.AppSettings[AppSettingsKey.CLIENT_ID];
            authorizeUrl += "client_id=" + clientId + "&";
            //
            authorizeUrl += "response_type=code&";
            //
            var redirectUri = ConfigurationManager.AppSettings[AppSettingsKey.REDIRECT_URI];    
            authorizeUrl += "redirect_uri=" + redirectUri + "&";
            //
            authorizeUrl += "scope=user-read-private user-read-email";

            return Redirect(authorizeUrl);
        }

        public ActionResult ProccessSpotifyResponse(string code="", string error = "")
        {
            if (!string.IsNullOrWhiteSpace(error))
                return View();

            return null;
        }
    }
}