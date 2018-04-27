using Dapper;
using MusicHubBusiness.Models;
using MySql.Data.MySqlClient;

namespace MusicHubBusiness.Repository
{
    public class RateMusicianRepository : BaseRepository<RateMusician>, IRepository<RateMusician>
    {
        public RateMusicianRepository() : base("RateMusician")
        {
        }

        public RateMusician Create(RateMusician rateMusician)
        {
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                mySqlConnection.Execute("INSERT INTO RateMusician(musician_owner_id, musician_target_id, rate_value) " +
                    "VALUES (@musician_owner_id, @musician_target_id, @rate_value)", new
                    {
                        rateMusician.musician_owner_id,
                        rateMusician.musician_target_id,
                        rateMusician.rate_value
                    });

                rateMusician = GetLatest();
            }

            return rateMusician;
        }

        internal RateMusician Update(RateMusician rateMusician)
        {
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                mySqlConnection.Execute("UPDATE RateMusician SET rate_value = @rate_value WHERE id = @id", new
                {
                    rateMusician.rate_value,
                    rateMusician.id,
                });

                rateMusician = Get(rateMusician.id);
            }

            return rateMusician;
        }

        internal RateMusician GetByOwnerId(int musician_target_id, int musician_owner_id)
        {
            RateMusician retorno = null;

            using (MySqlConnection mySqlConnection = GetConnection())
            {
                retorno = mySqlConnection.QueryFirstOrDefault<RateMusician>("SELECT * FROM RateMusician WHERE musician_owner_id = @musician_owner_id AND musician_target_id = @musician_target_id", new
                {
                    musician_owner_id,
                    musician_target_id
                });
            }

            return retorno;
        }
    }
}