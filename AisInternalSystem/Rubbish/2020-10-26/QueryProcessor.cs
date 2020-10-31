using MediaFoundation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AisInternalSystem.Module
{
    public class QueryProcessor
    {
        //variables


        //Properties
        public enum Process{ 
            Master, 
            Student, 
            Employee, 
            Class, 
            Subject}
        private Process _process;

        //Function
        public static DataTable Load(Process proc, string[] str)
        {
            MySqlCommand cmd;
            MySqlDataAdapter dataAdapter;
            DataTable table = new DataTable();
            MySqlDataReader reader;

            switch (proc)
            {
                case Process.Master:
                    try
                    {
                        cmd = new MySqlCommand("CheckSchoolYear", Db.get_connection());
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@_status", MySqlDbType.VarChar).Value = str[0];
                        dataAdapter = new MySqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                    }
                    catch (MySqlException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    return table;
                    break;
                case Process.Student:

                    return table;
                    break;
                case Process.Employee:

                    return table;
                    break;
                case Process.Class:

                    return table;
                    break;
                case Process.Subject:

                    return table;
                    break;
                default:

                    return table;
                    break;
            }
        }

        public static int? GetRandomNumber(Process proc)
        {
            MySqlCommand cmd;
            MySqlDataAdapter dataAdapter;
            DataTable table = new DataTable();
            MySqlDataReader reader;

            int? UniqueNumber = null;
            switch (proc)
            {
                case Process.Master:
                    cmd = new MySqlCommand("GetRandomNumberAY", Db.get_connection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UniqueNumber = reader.GetInt32("random_num");
                    }
                    reader.Close();
                    return UniqueNumber;
                    break;
                case Process.Student:

                    return UniqueNumber;
                    break;
                case Process.Employee:

                    return UniqueNumber;
                    break;
                case Process.Class:


                    return UniqueNumber;
                    break;
                case Process.Subject:

                    return UniqueNumber;
                    break;
                default:

                    return UniqueNumber;
                    break;
            }
        }

        public static void Insert(Process proc)
        {
            switch (proc)
            {
                case Process.Master:
                    break;
                case Process.Student:
                    break;
                case Process.Employee:
                    break;
                case Process.Class:
                    break;
                case Process.Subject:
                    break;
                default:
                    break;
            }
        }
    }
}
