using Dapper;
using MusicHubBusiness.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace MusicHubBusiness.Repository
{
    public class MusicianRepository : BaseRepository<Musician>, IRepository<Musician>
    {
        public MusicianRepository() : base("Musician")
        {
        }

        public Musician Create(Musician musician)
        {
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                mySqlConnection.Execute("INSERT INTO Musician(name, email, password, birth_date) VALUES (@name,@email,@password,@birth_date)", new
                {
                    musician.name,
                    musician.email,
                    musician.password,
                    musician.birth_date
                });

                musician = GetLatest();
            }

            return musician;
        }

        public Musician Login(string email, string password)
        {
            Musician retorno = null;
            using (MySqlConnection mySqlConnection = GetConnection())
            {
                retorno = mySqlConnection.Query<Musician>("SELECT * FROM Musician WHERE email = @email AND password = @password", new
                {
                    email,
                    password,
                }).FirstOrDefault();
            }

            return retorno;
        }

        internal Musician Update(Musician musician)
        {
            Execute("UPDATE Musician SET name = @name, email = @email, password = @password, birth_date = @birth_date", new
            {
                musician.name,
                musician.email,
                musician.password,
                musician.birth_date
            });

            return Get(musician.id);
        }

        internal IEnumerable<Musician> GetMusicians(IEnumerable<int> musician_ids)
        {
            IEnumerable<Musician> retorno = null;
            Query("SELECT * FROM Musician WHERE id IN (@musician_id)", new { musician_id = string.Join(",", musician_ids) });

            return retorno;
        }

        internal IEnumerable<Musician> SearchByName(string name)
        {
            IEnumerable<Musician> retorno = null;

            retorno = Query("SELECT * FROM Musician WHERE name LIKE @name", new { name = string.Format("%{0}%", name) });

            return retorno;
        }

        internal Musician GetByEmail(string email)
        {
            Musician retorno = null;

            retorno = QueryFirst("SELECT * FROM Musician WHERE email = @email", new
            {
                email
            });

            return retorno;
        }
    }
}