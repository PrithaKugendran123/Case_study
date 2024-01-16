using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Crime_Analysis_and_Reporting_System.Util
{
    static class DBConnection
    {
        private static SqlConnection connection = null;

        public static SqlConnection GetConnection()
        {
            if (connection == null)
            {
                
                string connectionString = ConfigurationManager.ConnectionStrings["carsConnection"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }

            return connection;
        }
    }
}
