using Dapper;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class RateMusicalProjectRepository : BaseRepository<RateMusicalProject>, IRepository<RateMusicalProject>
    {
        public RateMusicalProjectRepository() : base("RateMusicalProject")
        {
        }

        public RateMusicalProject Create(RateMusicalProject contribution)
        {
            using (mySqlConnection)
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