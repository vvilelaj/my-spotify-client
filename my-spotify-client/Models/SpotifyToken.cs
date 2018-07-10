using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_spotify_client.Models
{
    public class SpotifyToken
    {
        public SpotifyToken()
        {
            CreatedDate = DateTime.Now;
        }
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public string Scope { get; set; }
        public int Expires_In { get; set; }
        public string Refresh_Token { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}