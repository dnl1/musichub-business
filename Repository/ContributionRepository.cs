using Dapper;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class ContributionRepository : BaseRepository<Contribution>
    {
        public ContributionRepository() : base("Contribution")
        {
        }

        public Contribution Create(Contribution contribution)
        {
            using (mySqlConnection)
            {
                mySqlConnection.Execute("INSERT INTO Contribution(musician_id, musical_project_id, musical_project_instrument_id, musical_genre_id, status_id, timing, type_id, created_at) " +
                    "VALUES (@musician_id, @musical_project_id, @musical_project_instrument_id, @musical_genre_id, @status_id, @timing, @type_id, @created_at)", new
                {
                    contribution.musician_id,
                    contribution.musical_project_id,
                    contribution.musical_genre_id,
                    contribution.musical_project_instrument_id,
                    contribution.status_id,
                    contribution.timing,
                    contribution.type_id,
                    created_at = contribution.created_at.ToSQLDateTimeString()
                });

                contribution = GetLatest();
            }

            return contribution;
        }
    }
}