using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using my_spotify_client.Providers.SpotifyProvider.Entities;

namespace my_spotify_client.Models.WebClient
{
    public class IndexModel
    {
        public IndexModel()
        {
            UserProfile=new User();
        }
        public User UserProfile { get; set; }
    }
}