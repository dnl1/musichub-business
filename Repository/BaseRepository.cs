using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;

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

        public T GetLatest()
        {
            T retorno = default(T);

            retorno = QueryFirst($"SELECT * FROM {this.TableName} ORDER BY ID DESC LIMIT 1");

            return retorno;
        }

        public T Get(int id)
        {
            T retorno = default(T);

            retorno = QueryFirst($"SELECT * FROM {this.TableName} WHERE ID = @Id", new
            {
                ID = id
            });

            return retorno;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> retorno = default(IEnumerable<T>);

            retorno = Query($"SELECT * FROM {this.TableName}");

            return retorno;
        }

        public void Delete(int id)
        {
            Execute($"DELETE {this.TableName} WHERE ID = @Id", new
            {
                ID = id
            });
        }

        public void Execute(string sql, object parameters = null)
        {
            using (mySqlConnection)
            {
                mySqlConnection.Execute(sql, parameters);
            }
        }

        public IEnumerable<T> Query(string sql, object parameters = null)
        {
            IEnumerable<T> retorno = default(IEnumerable<T>);

            using (mySqlConnection)
            {
                retorno = mySqlConnection.Query<T>(sql, parameters);
            }

            return retorno;
        }

        public T QueryFirst(string sql, object parameters = null)
        {
            T retorno = default(T);

            using (mySqlConnection)
            {
                retorno = mySqlConnection.QueryFirstOrDefault<T>(sql, parameters);
            }

            return retorno;
        }
    }
}