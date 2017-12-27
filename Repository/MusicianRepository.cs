using Dapper;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Repository
{
    public class MusicianRepository:BaseRepository
    {
        public Musician Create(Musician musician)
        {
            using (mySqlConnection)
            {
                 mySqlConnection.Execute("INSERT INTO Musician VALUES (0,@name,@email,@password,@birth_date)", new {
                    name = musician.name,
                    email = musician.email,
                    password = musician.password,
                    birth_date = musician.birth_date.ToSQLString()
                });

                musician = GetLatest();
            }

            return musician;
        }

        private Musician GetLatest()
        {
            Musician retorno = null;

            using (mySqlConnection)
            {
                retorno = mySqlConnection.QueryFirst<Musician>("SELECT* FROM Musician ORDER BY ID DESC LIMIT 1");
            }

            return retorno;
        }
    }
}
