using MusicHubBusiness.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Models
{
    public class Contribution
    {
        public int id { get; set; }
        public int musician_id { get; set; }
        public int musical_project_instrument_id { get; set; }
        public eContributionStatus status_id { get; set; }
        public eContributionType type_id { get; set; }
        public int musical_genre_id { get; set; }
        public string timing { get; set; }
        public DateTime created_at { get; set; }
    }
}
