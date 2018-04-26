using Dapper;
using MusicHubBusiness.Enum;
using MusicHubBusiness.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MusicHubBusiness.Repository
{
    public class ContributionRepository : BaseRepository<Contribution>, IRepository<Contribution>
    {
        public ContributionRepository() : base("Contribution")
        {
        }

        public Contribution Create(Contribution contribution)
        {
            using (MySqlConnection mySqlConnection = GetConnection())
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

        internal IEnumerable<Contribution> GetByMusicalProjectId(int musical_project_id)
        {
            IEnumerable<Contribution> retorno = null;
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                retorno = mySqlConnection.Query<Contribution>("SELECT * FROM Contribution WHERE musical_project_id = @musical_project_id", new { musical_project_id });
            }

            return retorno;
        }

        internal IEnumerable<Contribution> GetFreeContributions(int id)
        {
            IEnumerable<Contribution> retorno = null;
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                retorno = mySqlConnection.Query<Contribution>
                    ("SELECT * FROM Contribution WHERE musician_id = @musician_id AND type_id = @type_id", new
                    {
                        musician_id = id,
                        type_id = (int)eContributionType.FreeContribution
                    });
            }

            return retorno;
        }

        internal IEnumerable<Contribution> GetByMusicalProjectAndIntrument(int musicalProjectId, int instrumentId)
        {
            IEnumerable<Contribution> retorno = null;
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                retorno = mySqlConnection.Query<Contribution>
                    (
                    @"SELECT DISTINCT C.* FROM Contribution C
                    INNER JOIN MusicalProjectInstrument MPI ON C.musical_project_id = MPI.musical_project_id
                    WHERE C.musical_project_id = @musicalProjectId AND MPI.instrument_id = @instrumentId", 
                    new
                    {
                        musicalProjectId,
                        instrumentId
                    });
            }

            return retorno;
        }

        internal void Approve(int contributionId)
        {
            RefuseApproveds(contributionId);

            string sql = @"UPDATE Contribution SET status_id = 2 WHERE id = @contributionId";

            Execute(sql, new { contributionId });
        }

        private void RefuseApproveds(int contributionId)
        {
            var contribution = Get(contributionId);
            string sql = @"UPDATE Contribution SET status_id = 3 WHERE 
            musical_project_instrument_id = @musicalProjectInstrumentId AND 
            id <> @contributionId";

            Execute(sql, new { musicalProjectInstrumentId = contribution.musical_project_instrument_id, contributionId });
        }
    }
}