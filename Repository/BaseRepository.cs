using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;

namespace MusicHubBusiness.Repository
{
    public abstract class BaseRepository<T>
    {
        private string TableName;
        private readonly string ConnectionString;

        public BaseRepository(string tableName)
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["MusicHubConnectionString"].ConnectionString;

            this.TableName = tableName;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
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
            using (MySqlConnection connection = GetConnection())
            {
                connection.Execute(sql, parameters);
            }
        }

        public IEnumerable<T> Query(string sql, object parameters = null)
        {
            IEnumerable<T> retorno = default(IEnumerable<T>);

            using (MySqlConnection connection = GetConnection())
            {
                retorno = connection.Query<T>(sql, parameters);
            }

            return retorno;
        }

        public T QueryFirst(string sql, object parameters = null)
        {
            T retorno = default(T);

            using (MySqlConnection connection = GetConnection())
            {
                retorno = connection.QueryFirstOrDefault<T>(sql, parameters);
            }

            return retorno;
        }
    }
}