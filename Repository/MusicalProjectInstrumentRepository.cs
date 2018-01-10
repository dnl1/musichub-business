using Dapper;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class MusicalProjectInstrumentRepository : BaseRepository<MusicalProjectInstrument>
    {
        public MusicalProjectInstrumentRepository() : base("MusicalProjectInstrument")
        {
        }

        public MusicalProjectInstrument Create(MusicalProjectInstrument musicalProjectInstrument)
        {
            using (mySqlConnection)
            {
                mySqlConnection.Execute("INSERT INTO MusicalProjectInstrument(musical_project_id, instrument_id, is_base_instrument) VALUES (@musical_project_id ,@instrument_id, @is_base_instrument)", new
                {
                    musicalProjectInstrument.musical_project_id,
                    musicalProjectInstrument.instrument_id,
                    musicalProjectInstrument.is_base_instrument
                });

                musicalProjectInstrument = GetLatest();
            }

            return musicalProjectInstrument;
        }
    }
}