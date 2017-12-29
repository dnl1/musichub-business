using Dapper;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class MusicalProjectRepository : BaseRepository<MusicalProject>
    {
        public MusicalProjectRepository() : base("MusicalProject")
        {
        }

        public MusicalProject Create(MusicalProject musicalProject)
        {
            using (mySqlConnection)
            {
                mySqlConnection.Execute("INSERT INTO MusicalProject(name, owner_id, gender_id, created_at, updated_at) VALUES (@name ,@owner_id, @gender_id ,@created_at, @updated_at)", new
                {
                    name = musicalProject.name,
                    owner_id = musicalProject.owner_id,
                    gender_id = musicalProject.gender_id,
                    created_at = musicalProject.created_at.ToSQLDateTimeString(),
                    updated_at = musicalProject.updated_at.ToSQLDateTimeString()
                });

                musicalProject = GetLatest();
            }

            return musicalProject;
        }
    }
}