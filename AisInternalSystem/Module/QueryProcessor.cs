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
            Student, StudentList, 
            Employee, EmployeeList,
            Class, ClassList,
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

                case Process.ClassList:
                    cmd = new MySqlCommand("LoadClassLListFilter", Db.get_connection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("");

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

        /*
        public static void Update(Process proc, string[] str)
        {
            
            MySqlCommand cmd;
            switch (proc)
            {
                case Process.Master:

                    break;

                case Process.Student:
                    cmd = new MySqlCommand("UpdateStudent", Db.get_connection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < length; i++)
                    {

                    }
                    cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = str[0];
                    cmd.Parameters.Add("@nis", MySqlDbType.VarChar).Value = str[1];
                    cmd.Parameters.Add("@ausid", MySqlDbType.VarChar).Value = str[2];
                    cmd.Parameters.Add("@intake", MySqlDbType.Date).Value = str[3];
                    cmd.Parameters.Add("@familyname", MySqlDbType.VarChar).Value = str[4];
                    cmd.Parameters.Add("@givenname", MySqlDbType.VarChar).Value = str[5];
                    cmd.Parameters.Add("@middlename", MySqlDbType.VarChar).Value = str[6];
                    cmd.Parameters.Add("@certificatename", MySqlDbType.VarChar).Value = str[7];
                    cmd.Parameters.Add("@dob", MySqlDbType.Date).Value = str[8];
                    cmd.Parameters.Add("@pob", MySqlDbType.VarChar).Value = str[9];
                    cmd.Parameters.Add("@cob", MySqlDbType.VarChar).Value = str[10];
                    cmd.Parameters.Add("@gender", MySqlDbType.VarChar).Value = str[11];
                    cmd.Parameters.Add("@religion", MySqlDbType.VarChar).Value = str[12];
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = str[13];
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = str[14];
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = str[15];
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = str[16];
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = str[17];
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtHomeCountry.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtPostalCountry.Text;
                    cmd.Parameters.Add("@homephone", MySqlDbType.VarChar).Value = txtHomePhone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtMobileNumber.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtFaxNumber.Text;
                    cmd.Parameters.Add("@langspoken", MySqlDbType.VarChar).Value = txtLangSpoken.Text;
                    cmd.Parameters.Add("@englishproficiency", MySqlDbType.VarChar).Value = dropSpokenEnglish.SelectedValue.ToString();
                    cmd.Parameters.Add("@studstat", MySqlDbType.VarChar).Value = dropStudStat.SelectedValue.ToString();
                    cmd.Parameters.Add("@studentimg", MySqlDbType.VarChar).Value = studentPhoto;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@revised", MySqlDbType.Int32).Value = Dashboard.ownerId;
                    cmd.Parameters.Add("@current_grade", MySqlDbType.VarChar).Value = 0;
                    cmd.Parameters.Add("@proposedgrade", MySqlDbType.VarChar).Value = dropPropGrade.SelectedValue.ToString();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Student succesfully updated", frmAlert.AlertType.Success);
                    }
                    else
                    {

                    }
                    break;

                case Process.StudentList:
                    
                    break;
                case Process.Employee:
                    
                    break;
                case Process.EmployeeList:
                    
                    break;
                case Process.Class:
                    
                    break;
                case Process.ClassList:
                    
                    break;
                case Process.Subject:
                    
                    break;
                default:
                    break;
            
            } 

        } 
        */
    }
}
