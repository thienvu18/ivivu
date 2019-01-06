using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ivivuApp
{
    class Database
    {
        public static SqlConnection connection;

        ~Database()
        {
            connection.Close();
        }
        public static void init()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString,   // update me
                UserID = ConfigurationManager.ConnectionStrings["UserID"].ConnectionString,              // update me
                Password = ConfigurationManager.ConnectionStrings["Password"].ConnectionString,      // update me
                InitialCatalog = ConfigurationManager.ConnectionStrings["InitialCatalog"].ConnectionString
            };

            // Connect to SQL
            Console.Write("Connecting to SQL Server ... ");
            connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
        }
    }
}
