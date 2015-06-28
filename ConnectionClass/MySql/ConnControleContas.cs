using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionClass.MySql
{
    public class ConnControleContas
    {
        private static MySqlConnection conn = new MySqlConnection();
        private static MySqlCommand cmd = new MySqlCommand();
        private static MySqlDataReader dr;

        public static string StrConn = "Server=localhost" +
                                       ";Port=3306" +
                                       ";Database=controlecontasdb" +
                                       ";Uid=root" +
                                       ";Pwd=";

        public static bool Connect()
        {

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.ConnectionString = StrConn;
                conn.Open();
            }

            return true;
        }

        public static bool Close()
        {
            conn.Close();
            return true;
        }

        public static bool CommandPersist(MySqlCommand pCmd)
        {
            Connect();

            if (dr != null && !dr.IsClosed)
                dr.Close();

            pCmd.Connection = conn;
            pCmd.ExecuteNonQuery();
            return true;
        }

        public static MySqlDataReader Get(string pSql)
        {
            Connect();
            if (dr != null && !dr.IsClosed)
                dr.Close();

            cmd = new MySqlCommand(pSql, conn);
            dr = cmd.ExecuteReader();

            return dr;
        }

    }
}
