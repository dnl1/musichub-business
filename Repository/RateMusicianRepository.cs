using Dapper;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class RateMusicianRepository : BaseRepository<RateMusician>, IRepository<RateMusician>
    {
        public RateMusicianRepository() : base("RateMusician")
        {
        }

        public RateMusician Create(RateMusician contribution)
        {
            using (mySqlConnection)
            {
                mySqlConnection.Execute("INSERT INTO RateMusician(musician_owner_id, musician_target_id, rate_value) " +
                    "VALUES (@musician_owner_id, @musician_target_id, @rate_value)", new
                {
                    contribution.musician_owner_id,
                    contribution.musician_target_id,
                    contribution.rate_value
                });

                contribution = GetLatest();
            }

            return contribution;
        }
    }
}