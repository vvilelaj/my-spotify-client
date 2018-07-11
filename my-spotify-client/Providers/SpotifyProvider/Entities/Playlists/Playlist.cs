using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_spotify_client.Providers.SpotifyProvider.Entities.Playlists
{
    public class Playlist : BaseEntity
    {
        public Playlist()
        {
            Images = new List<Image>();
        }
        public bool Collaborative { get; set; }
        public string Id { get; set; }
        public List<Image> Images { get; set; }
        public string Name{ get; set; }
        public bool Public { get; set; }
        public string Uti { get; set; }
    }
}