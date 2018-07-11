using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_spotify_client.Providers.SpotifyProvider.Entities
{
    public class Paging<T>: BaseEntity
        where T : BaseEntity
    {
        public Paging()
        {
            Items = new List<T>();
        }
        public int Limit { get; set; }
        public string Next { get; set; }
        public int Offset { get; set; }
        public List<T> Items { get; set; }
        public string Previous { get; set; }
        public int Total { get; set; }
    }
}