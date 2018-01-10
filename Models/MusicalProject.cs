using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Models
{
    public class MusicalProject
    {
        public int id { get; set; }
        public string name { get; set; }
        public int owner_id { get; set; }
        public int musical_genre_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
