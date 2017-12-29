using Dapper;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class MusicalGenreRepository : BaseRepository<MusicalGenre>
    {
        public MusicalGenreRepository() : base("MusicalGenre")
        {
        }
    }
}