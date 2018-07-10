using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using my_spotify_client.Models;

namespace my_spotify_client.Common.SessionManager
{
    public class SessionManager
    {
        private static SessionManager _instance;

        public  static SessionManager Instance()
        {
            if(_instance == null) _instance = new SessionManager();

            return _instance;
        }

        public SpotifyToken SpotifyToken
        {
            get
            {
                return (SpotifyToken)HttpContext.Current.Session[SessionKey.SPOTIFY_TOKEN];
            }
            set
            {
                HttpContext.Current.Session[SessionKey.SPOTIFY_TOKEN] = value;
            }
        }

        public string State
        {
            get
            {
                return HttpContext.Current.Session[SessionKey.STATE].ToString();
            }
            set
            {
                HttpContext.Current.Session[SessionKey.STATE] = value;
            }
        }
    }
}