using System;
using System.Collections.Generic;

namespace my_spotify_client.Providers.SpotifyProvider.Entities.UserProfile
{
    public class User : BaseEntity
    {
        public User()
        {
            Images= new List<Image>();
            CreatedDate = DateTime.Now;
        }
        public string Id { get; set; }
        public string Display_Name { get; set; }
        public List<Image> Images { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}