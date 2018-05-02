using System;
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

        internal RateMusicalProject GetByUserAndProjectId(int musicalProjectId, int userId)
        {
            string sql = "SELECT * FROM RateMusicalProject WHERE musical_project_id = @musicalProjectId AND musician_id = @userId";

            RateMusicalProject retorno = QueryFirst(sql, new
            {
                musicalProjectId,
                userId
            });

            return retorno;
        }

        internal RateMusicalProject Update(RateMusicalProject rateMusicalProject)
        {
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                mySqlConnection.Execute("UPDATE RateMusicalProject SET rate_value = @rate_value WHERE id = @id", new
                {
                    rateMusicalProject.rate_value,
                    rateMusicalProject.id,
                });

                rateMusicalProject = Get(rateMusicalProject.id);
            }

            return rateMusicalProject;
        }
    }
}