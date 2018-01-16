using Dapper;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class RateContributionRepository : BaseRepository<RateContribution>, IRepository<RateContribution>
    {
        public RateContributionRepository() : base("RateContribution")
        {
        }

        public RateContribution Create(RateContribution contribution)
        {
            using (mySqlConnection)
            {
                mySqlConnection.Execute("INSERT INTO RateContribution(musician_id, contribution_id, rate_value) " +
                    "VALUES (@musician_id, @contribution_id, @rate_value)", new
                {
                    contribution.musician_id,
                    contribution.contribution_id,
                    contribution.rate_value
                });

                contribution = GetLatest();
            }

            return contribution;
        }
    }
}