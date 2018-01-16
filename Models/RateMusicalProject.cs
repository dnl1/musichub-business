using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Models
{
    public class RateMusicalProject
    {
        public int id { get; set; }
        public int musician_id { get; set; }
        public int musical_project_id { get; set; }
        public int rate_value { get; set; }
    }
}
