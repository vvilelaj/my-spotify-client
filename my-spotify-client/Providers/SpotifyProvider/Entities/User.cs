using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_spotify_client.Providers.SpotifyProvider.Entities
{
    public class User
    {
        public User()
        {
            Images= new List<Image>();
            CreatedDate = DateTime.Now;
        }
        public string Id { get; set; }
        public string Display_Name { get; set; }
        public string Href { get; set; }
        public List<Image> Images { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}