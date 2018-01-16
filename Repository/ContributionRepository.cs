using System;
using System.Collections.Generic;
using Dapper;
using MusicHubBusiness.Enum;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class ContributionRepository : BaseRepository<Contribution>, IRepository<Contribution>
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

        internal IEnumerable<Contribution> GetByMusicalProjectId(int musical_project_id)
        {
            IEnumerable<Contribution> retorno = null;
            using (mySqlConnection)
            {
                retorno = mySqlConnection.Query<Contribution>("SELECT * FROM Contribution WHERE musical_project_id = @musical_project_id", new { musical_project_id });

            }

            return retorno;
        }

        internal IEnumerable<Contribution> GetFreeContributions(int id)
        {
            IEnumerable<Contribution> retorno = null;
            using (mySqlConnection)
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
    }
}