using System.Data;
using System.Windows.Forms;
using AisInternalSystem.Controller;
using Microsoft.ReportingServices.Interfaces;
using MySql.Data.MySqlClient;

namespace AisInternalSystem.Module
{
    public static class Db
    {
        //declare connection path
        //port was 4409, changed to 3306
        //ais connection
        static private MySqlConnection Connection = new MySqlConnection("Uid=hermes;Pwd=fR9iMEnRxcaHjB;server=192.168.30.100;database=aisdb;port=4409;Allow User Variables=True");
        //home connection
        //static private MySqlConnection Connection = new MySqlConnection("Uid=root;Pwd=B14ngk3r0g523507!!%%;server=localhost;database=aisdb;port=3306;Allow User Variables=True");

        //open connection
        static public void OpenConnection()
        {
                if (Connection.State == ConnectionState.Closed)
                {
                try
                {
                    Connection.Open();

                }
                catch (MySqlException e)
                {
                    PopUp.Alert(e.Message, frmAlert.AlertType.Error);
                }
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

        public static void DataAdapter(MySqlCommand command, DataTable dataTable)
        {
            var da = new MySqlDataAdapter(command);
            try
            {
                da.Fill(dataTable);
            }
            catch (MySqlException ex)
            {
                PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        //return connection
        static public MySqlConnection GetConnection()
        {

            if (Connection.State == ConnectionState.Closed)
            {
                OpenConnection();
            }
            return Connection;
        }

    }
}
