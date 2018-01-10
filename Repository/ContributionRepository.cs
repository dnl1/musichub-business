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
                mySqlConnection.Execute("INSERT INTO Contribution(name, email, password, birth_date) VALUES (@name,@email,@password,@birth_date)", new
                {
                    contribution.musician_id,
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