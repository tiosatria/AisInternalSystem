using MediaFoundation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AisInternalSystem.Controller;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Windows;

namespace AisInternalSystem.Module
{
    public class QueryOld
    {
        //variables


        //Properties
        public enum Process{ 
            Master, 
            LoadStudent, LoadStudentList, 
            Employee, EmployeeList,
            Class, ClassList,
            Subject, Auth, LogLoginHistory}
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
                        cmd = new MySqlCommand("CheckSchoolYear", Db.GetConnection());
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@_status", MySqlDbType.VarChar).Value = str[0];
                        dataAdapter = new MySqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                    }
                    catch (MySqlException ex)
                    {
                        PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    return table;
                case Process.LoadStudent:
                        
                    return table;
                case Process.Employee:

                    return table;
                case Process.Class:

                    return table;

                case Process.ClassList:
                    cmd = new MySqlCommand("LoadClassLListFilter", Db.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("");

                    return table;
                case Process.Subject:

                    return table;
                default:
                    return table;
                case Process.Auth:
                    cmd = new MySqlCommand("Auth", Db.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@_usr", MySqlDbType.VarChar).Value = str[0];
                    cmd.Parameters.Add("@_pwd", MySqlDbType.VarChar).Value = str[1];
                    dataAdapter = new MySqlDataAdapter(cmd);
                    dataAdapter.Fill(table);
                    return table;
            }
        }

        

        public static void Insert(Process proc)
        {
            switch (proc)
            {

            }
        }
    }
}
