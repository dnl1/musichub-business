using System;
using System.Collections.Generic;
using Dapper;
using MusicHubBusiness.Models;

namespace MusicHubBusiness.Repository
{
    public class MusicalGenreRepository : BaseRepository<MusicalGenre>, IRepository<MusicalGenre>
    {
        public MusicalGenreRepository() : base("MusicalGenre")
        {
        }

        public MusicalGenre Create(MusicalGenre entity)
        {
            throw new System.NotImplementedException();
        }
    }
}