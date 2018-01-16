namespace MusicHubBusiness.Models
{
    public class RateContribution
    {
        public int id { get; set; }
        public int musician_id { get; set; }
        public int contribution_id { get; set; }
        public int rate_value { get; set; }
    }
}