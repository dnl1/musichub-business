namespace MusicHubBusiness.Models
{
    public class MusicalProjectInstrument
    {
        public int id { get; set; }
        public int musical_project_id { get; set; }
        public int instrument_id { get; set; }
        public bool is_base_instrument { get; set; }
    }
}