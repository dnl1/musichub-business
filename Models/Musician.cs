using System;

namespace MusicHubBusiness.Models
{
    public class Musician
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime? birth_date { get; set; }

        public bool ShouldSerializepassword()
        {
            return false;
        }
    }
}