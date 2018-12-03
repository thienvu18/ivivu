using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;

namespace App
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string constConnection = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(constConnection);
            SqlCommand cmd;

            conn.Open();
            string sql = "SELECT * FROM DatPhong";

            cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(dr);

            using (dr)
            {
                while (dr.Read())
                {
                    Console.WriteLine(dr["donGia"]);
                }

                conn.Close();
                conn.Dispose();
            }
        }
    }
}
