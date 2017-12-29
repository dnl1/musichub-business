using Dapper;
using MusicHubBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Repository
{
    public class MusicianRepository:BaseRepository<Musician>
    {
        public MusicianRepository() : base("Musician")
        {
            
        }

        public Musician Create(Musician musician)
        {
            using (mySqlConnection)
            {
                 mySqlConnection.Execute("INSERT INTO Musician(name, email, password, birth_date) VALUES (@name,@email,@password,@birth_date)", new {
                    name = musician.name,
                    email = musician.email,
                    password = musician.password,
                    birth_date = musician.birth_date.ToSQLDateString()
                });

                musician = GetLatest();
            }

            return musician;
        }

        public Musician Login(string email, string password)
        {
            Musician retorno = null;
            using (mySqlConnection)
            {
                retorno = mySqlConnection.Query<Musician>("SELECT * FROM Musician WHERE email = @email AND password = @password", new
                {
                    email = email,
                    password = password,
                }).FirstOrDefault();

            }

            return retorno;
        }

        internal object GetByEmail(string email)
        {
            Musician retorno = null;
            using (mySqlConnection)
            {
                retorno = mySqlConnection.Query<Musician>("SELECT * FROM Musician WHERE email = @email", new
                {
                    email = email
                }).FirstOrDefault();

            }

            return retorno;
        }
    }
}
