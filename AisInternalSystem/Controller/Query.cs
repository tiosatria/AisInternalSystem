using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AisInternalSystem.Module;
using MySql.Data.MySqlClient;

namespace AisInternalSystem.Controller
{
    public class Query
    {
        #region Enumeration
        public enum Process
        { 
            Auth, LogLoginHistory, Master, LoadStudent, LoadStudentList
        }
        #endregion

        #region Properties
        public Query()
        {

        }
        #endregion

        #region Variable
        private static MySqlCommand cmd;
        private static MySqlDataAdapter da;
        Process _process;
        #endregion

        #region Function
        public static DataTable Load(Process proc, string[] str)
        {
            DataTable dt = new DataTable();
            switch (proc)
            {
                case Process.Auth:
                    cmd = new MySqlCommand("Auth", Db.GetConnection());
                    cmd.Parameters.Add("@_usr", MySqlDbType.VarChar).Value = str[0];
                    cmd.Parameters.Add("@_pwd", MySqlDbType.VarChar).Value = str[1];
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                    break;
                case Process.LoadStudent:
                    cmd = new MySqlCommand("LoadStudentData", Db.GetConnection());
                    cmd.Parameters.Add("@_aisid", MySqlDbType.Int32).Value = str[0];
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                    break;
                default:
                    return null;
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
                    cmd = new MySqlCommand("GetRandomNumberAY", Db.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UniqueNumber = reader.GetInt32("random_num");
                    }
                    reader.Close();
                    return UniqueNumber;
                    break;
                default:
                    return null;
                    break;
            }
        }
        #endregion

    }
}
