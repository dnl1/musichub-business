namespace MusicHubBusiness.Models
{
    public class RateMusician
    {
        public int id { get; set; }
        public int musician_owner_id { get; set; }
        public int musician_target_id { get; set; }
        public int rate_value { get; set; }
    }
}