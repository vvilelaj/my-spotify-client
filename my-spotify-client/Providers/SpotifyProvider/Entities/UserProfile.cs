using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_spotify_client.Providers.SpotifyProvider.Entities
{
    public class UserProfile
    {
        public UserProfile()
        {
            images= new List<Image>();
            CreatedDate = DateTime.Now;
        }
        public string id { get; set; }
        public string display_name { get; set; }
        public string href { get; set; }
        public List<Image> images { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}