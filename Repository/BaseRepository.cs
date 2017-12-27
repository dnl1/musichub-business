using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Repository
{
    public abstract class BaseRepository
    {
        protected MySqlConnection mySqlConnection;

        public BaseRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MusicHubConnectionString"].ConnectionString;
            this.mySqlConnection = new MySqlConnection(connectionString);
        }

    }
}
