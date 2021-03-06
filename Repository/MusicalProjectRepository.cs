﻿using Dapper;
using MusicHubBusiness.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MusicHubBusiness.Repository
{
    public class MusicalProjectRepository : BaseRepository<MusicalProject>, IRepository<MusicalProject>
    {
        public MusicalProjectRepository() : base("MusicalProject")
        {
        }

        public MusicalProject Create(MusicalProject musicalProject)
        {
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                mySqlConnection.Execute("INSERT INTO MusicalProject(name, owner_id, musical_genre_id, created_at, updated_at) VALUES (@name ,@owner_id, @musical_genre_id,@created_at, @updated_at)", new
                {
                    musicalProject.name,
                    musicalProject.owner_id,
                    musicalProject.musical_genre_id,
                    created_at = musicalProject.created_at.ToSQLDateTimeString(),
                    updated_at = musicalProject.updated_at.ToSQLDateTimeString()
                });

                musicalProject = GetLatest();
            }

            return musicalProject;
        }

        internal IEnumerable<MusicalProject> SearchByMusicalGenre(int musical_genre_id, int owner_id)
        {
            IEnumerable<MusicalProject> retorno = default(IEnumerable<MusicalProject>);

            using (MySqlConnection mySqlConnection = GetConnection())
            {
                retorno = mySqlConnection.Query<MusicalProject>("SELECT * FROM MusicalProject WHERE musical_genre_id = @musical_genre_id AND owner_id <> @owner_id", new { musical_genre_id, owner_id });
            }

            return retorno;
        }

        internal IEnumerable<MusicalProject> GetByOwnerId(int owner_id)
        {
            IEnumerable<MusicalProject> retorno = default(IEnumerable<MusicalProject>);

            using (MySqlConnection mySqlConnection = GetConnection())
            {
                retorno = mySqlConnection.Query<MusicalProject>("SELECT * FROM MusicalProject WHERE owner_id = @owner_id ORDER BY created_at DESC", new { owner_id });
            }

            return retorno;
        }

        internal void Finish(int id)
        {
            Execute("UPDATE MusicalProject SET finish = 1 WHERE id = @id", new { id });
        }
    }
}