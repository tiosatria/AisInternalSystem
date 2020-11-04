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
            Auth, LogLoginHistory, Master, LoadStudent, LoadStudentList, GetAcademicYearList, GetClassListByYear, GetClassMember, GetClassInfo, GetAvailableTeacherToAssign
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
                    Db.DataAdapter(cmd, dt);
                    return dt;
                    break;
                case Process.LoadStudent:
                    cmd = new MySqlCommand("LoadStudentData", Db.GetConnection());
                    cmd.Parameters.Add("@_aisid", MySqlDbType.Int32).Value = str[0];
                    cmd.CommandType = CommandType.StoredProcedure;
                    Db.DataAdapter(cmd, dt);
                    return dt;
                    break;
                case Process.GetAcademicYearList:
                    cmd = new MySqlCommand("GetAcademicYearList", Db.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    Db.DataAdapter(cmd, dt);
                    return dt;
                case Process.GetClassListByYear:
                    cmd = new MySqlCommand("LoadClassLListFilter", Db.GetConnection());
                    cmd.Parameters.Add("@ay", MySqlDbType.VarChar).Value = str[0];
                    cmd.CommandType = CommandType.StoredProcedure;
                    Db.DataAdapter(cmd, dt);
                    return dt;
                    break;
                case Process.GetClassMember:
                    cmd = new MySqlCommand("fetchmemberlist", Db.GetConnection());
                    cmd.Parameters.Add("@classid", MySqlDbType.Int32).Value = str[0];
                    cmd.CommandType = CommandType.StoredProcedure;
                    Db.DataAdapter(cmd, dt);
                    return dt;
                case Process.GetClassInfo:
                    cmd = Command("fetch_class_info");
                    cmd.Parameters.Add("@classid", MySqlDbType.Int32).Value = str[0];
                    Db.DataAdapter(cmd, dt);
                    return dt;
                case Process.GetAvailableTeacherToAssign:
                    cmd = Command("fetch_teacher_assign_class");
                    cmd.Parameters.Add("@teacher", MySqlDbType.VarChar).Value = str[0];
                    Db.DataAdapter(cmd, dt);
                    return dt;
                default:
                    return null;
                    break;
            }
        }

        private static MySqlCommand Command(string str)
        {
            MySqlCommand cmd = new MySqlCommand(str, Db.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
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
