using System.Data;
using MySql.Data.MySqlClient;

namespace AisInternalSystem.Module
{
    public static class Db
    {
        //declare connection path
        //port was 4409, changed to 3306
        //ais connection
        //static private MySqlConnection connection = new MySqlConnection("Uid=hermes;Pwd=fR9iMEnRxcaHjB;server=192.168.30.100;database=aisdb;port=4409;Allow User Variables=True");
        //home connection
        static private MySqlConnection Connection = new MySqlConnection("Uid=root;Pwd=B14ngk3r0g523507!!%%;server=localhost;database=aisdb;port=3306;Allow User Variables=True");

        //open connection
        static public void OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        //close connection

        static public void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        //return connection
        static public MySqlConnection GetConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            return Connection;
        }

    }
}
