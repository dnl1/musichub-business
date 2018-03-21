using Dapper;
using MusicHubBusiness.Models;
using MySql.Data.MySqlClient;

namespace MusicHubBusiness.Repository
{
    public class RateMusicalProjectRepository : BaseRepository<RateMusicalProject>, IRepository<RateMusicalProject>
    {
        public RateMusicalProjectRepository() : base("RateMusicalProject")
        {
        }

        public RateMusicalProject Create(RateMusicalProject contribution)
        {
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                mySqlConnection.Execute("INSERT INTO RateMusicalProject(musician_id, musical_project_id, rate_value) " +
                    "VALUES (@musician_id, @musical_project_id, @rate_value)", new
                {
                    contribution.musical_project_id,
                    contribution.musician_id,
                    contribution.rate_value
                });

                contribution = GetLatest();
            }

            return contribution;
        }
    }
}