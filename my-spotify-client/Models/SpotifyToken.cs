using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_spotify_client.Models
{
    public class SpotifyToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public string Scope { get; set; }
        public int Expires_In { get; set; }
        public string Refresh_Token { get; set; }

        //public override string ToString()
        //{
        //    var str = "Access_Token:" + Access_Token + " | ";
        //    str += "Token_Type:" + Token_Type + " | ";
        //    str += "Scope:" + Scope + " | ";
        //    str += "Expires_In:" + Expires_In + " | ";
        //    str += "Refresh_Token:" + Refresh_Token;
        //    return str;
        //}
    }
}