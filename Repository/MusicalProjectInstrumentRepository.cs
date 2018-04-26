using System;
using System.Collections.Generic;
using Dapper;
using MusicHubBusiness.Models;
using MySql.Data.MySqlClient;

namespace MusicHubBusiness.Repository
{
    public class MusicalProjectInstrumentRepository : BaseRepository<MusicalProjectInstrument>, IRepository<MusicalProjectInstrument>
    {
        public MusicalProjectInstrumentRepository() : base("MusicalProjectInstrument")
        {
        }

        public MusicalProjectInstrument Create(MusicalProjectInstrument musicalProjectInstrument)
        {
            using (MySqlConnection mySqlConnection = GetConnection())
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

        internal IEnumerable<MusicalProjectInstrument> GetByMusicalProject(int musicalProjectId)
        {
            string sql = "SELECT * FROM MusicalProjectInstrument WHERE musical_project_id = @musicalProjectId";
            IEnumerable<MusicalProjectInstrument> retorno = Query(sql, new { musicalProjectId });

            return retorno;
        }
    }
}