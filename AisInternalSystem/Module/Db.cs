using System.Data;
using MySql.Data.MySqlClient;

namespace AisInternalSystem.Module
{
    static class Db
    {
        //declare connection path
        //port was 4409, changed to 3306
        //ais connection
        static private MySqlConnection connection = new MySqlConnection("Uid=hermes;Pwd=fR9iMEnRxcaHjB;server=192.168.30.100;database=aisdb;port=4409;Allow User Variables=True");

        //open connection
        static public void open_connection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        //close connection

        static public void close_connection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        //return connection
        static public MySqlConnection get_connection()
        {
            return connection;
        }

    }
}
