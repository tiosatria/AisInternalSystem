using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Resources;
using AisInternalSystem.Module;
using MySql.Data.MySqlClient;
using Telerik.WinControls.UI;

namespace AisInternalSystem.Controller
{
    public class Query
    {
        #region Enumeration
        public enum Process
        { 
            Auth, LogLoginHistory, Master, LoadStudent, LoadStudentList, GetAcademicYearList, GetClassListByYear, GetClassMember, GetClassInfo, GetAvailableTeacherToAssign, GetUnassignedStudent
        }
        #endregion

        #region Properties
        private static BackgroundWorker worker;

        private static void initWorker()
        {
            worker = new BackgroundWorker();
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        public enum Do
        {
        GetDataTable, GetList, Delete, Insert
        }

        private static Do @do;

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (@do)
            {
                case Do.GetDataTable:
                    break;
                case Do.GetList:

                    break;
                case Do.Delete:
                    break;
                case Do.Insert:
                    break;
                default:
                    break;
            }
        }

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

        public static List<string> GetList(string query)
        {
            List<string> l = new List<string>();
            DataTable dt = new DataTable();
            MySqlCommand cmd = Command(query);
            Db.DataAdapter(cmd, dt);
            foreach (string item in dt.Rows)
            {
                l.Add(item);
            }
            return l;
        }

        public static AutoCompleteStringCollection GetAutoCompleteCollection(string query)
        {
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            MySqlCommand cmd = Command(query);
            Db.DataAdapter(cmd, dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str = dt.Rows[i][0].ToString();
                ac.Add(str);
            }
            return ac;
        }

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
                case Process.GetUnassignedStudent:
                    cmd = Command("GetUnassignedStudent");
                    Db.DataAdapter(cmd, dt);
                    return dt;
                default:
                    return null;
                    break;
            }
        }

        public static bool Delete(string Query, string[] param, MySqlDbType[] type, string[] value)
        {
            MySqlCommand cmd = Command(Query);
            if (param[0] != "@noparam")
            {
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i], type[i]).Value = value[i];
                }
                try
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public static DataTable GetDataTable(string Query,string[] param,MySqlDbType[] type, string[] value)
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd;
            cmd = Command(Query);
            if (param[0] != "@noparam")
            {
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i], type[i]).Value = value[i];
                }
                Db.DataAdapter(cmd, dt);
            }
            else
            {
                Db.DataAdapter(cmd, dt);
            }
            return dt;
        }
            

        public static bool Insert(string query, string[] param, MySqlDbType[] type, string[] value)
        {
            MySqlCommand cmd = Command(query);
            if (param[0]  != "@noparam")
            {
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i], type[i]).Value = value[i];
                }
                try
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        PopUp.Alert("Operation completed succesfully", frmAlert.AlertType.Success);
                    }
                    return true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    PopUp.Alert($"Oops something wrong\n({ex.Message})", frmAlert.AlertType.Error);
                    return false;
                }
            }
            else
            {
                try
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        PopUp.Alert("Operation completed succesfully", frmAlert.AlertType.Success);
                    }
                    return true;
                }
                catch (MySqlException ex)
                {
                    PopUp.Alert($"Something is wrong, possibly duplicate\nTech. Detail({ex.Message})", frmAlert.AlertType.Error);
                    return false;
                }
            }
        }

        public static MySqlCommand Command(string str)
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
