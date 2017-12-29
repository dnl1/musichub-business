using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Repository
{
    public abstract class BaseRepository<T>
    {
        private string TableName;

        protected MySqlConnection mySqlConnection;

        public BaseRepository(string tableName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MusicHubConnectionString"].ConnectionString;
            this.mySqlConnection = new MySqlConnection(connectionString);

            this.TableName = tableName;
        }

        internal T GetLatest()
        {
            T retorno = default(T);

            using (mySqlConnection)
            {
                retorno = mySqlConnection.QueryFirstOrDefault<T>($"SELECT * FROM {this.TableName} ORDER BY ID DESC LIMIT 1");
            }

            return retorno;
        }

        internal T Get(int id)
        {
            T retorno = default(T);

            using (mySqlConnection)
            {
                retorno = mySqlConnection.QueryFirstOrDefault<T>($"SELECT * FROM {this.TableName} WHERE ID = @Id", new {
                    ID = id
                });
            }

            return retorno;
        }

    }
}
