using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using my_spotify_client.Providers.SpotifyProvider.Entities;
using my_spotify_client.Providers.SpotifyProvider.Entities.Playlists;
using my_spotify_client.Providers.SpotifyProvider.Entities.UserProfile;

namespace my_spotify_client.Models.WebClient
{
    public class IndexModel
    {
        public IndexModel()
        {
            UserProfile=new User();
            Playlists = new Paging<Playlist>();
        }
        public User UserProfile { get; set; }
        public Paging<Playlist> Playlists { get; set; }
    }
}