using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_spotify_client.Models
{
    public class ProccessSpotifyResponseModel
    {
        public String Error { get; set; }
        public bool HuboError { get; set; }

        public ProccessSpotifyResponseModel(bool huboError, string error)
        {
            this.HuboError = huboError;
            this.Error = error;
        }
    }
}