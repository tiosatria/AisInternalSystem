using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AisInternalSystem.Module;

namespace AisInternalSystem
{

    public partial class UCClassDirectoryService : UserControl
    {
        MySqlCommand command;
        MySqlDataReader reader;
        Data collection = new Data();
        public UCClassDirectoryService()
        {
            InitializeComponent();
        }
        #region Properties
        public enum ClassEnum{
            ClassMember,
            EditClassInfo
        };
        private ClassEnum _cenum;
        #endregion

        #region Function
        public void CSwitcher(ClassEnum i)
        {
            _cenum = i;
            switch (_cenum)
            {
                case ClassEnum.ClassMember:
                    LoadMember();
                    PanelClassMember.BringToFront();
                    btnClassMember.FillColor = Color.Black;
                    btnClassMember.ForeColor = Color.White;
                    btnEditClassInfo.FillColor = Color.Silver;
                    btnEditClassInfo.ForeColor = Color.Black;
                    break;
                case ClassEnum.EditClassInfo:
                    LoadInfo();
                    PanelClassInfo.BringToFront();
                    btnClassMember.FillColor = Color.Silver;
                    btnClassMember.ForeColor = Color.Black;
                    btnEditClassInfo.FillColor = Color.Black;
                    btnEditClassInfo.ForeColor = Color.White;
                    break;
                default:
                    break;
            }
        }
        public void LoadMember()
        {
            try
            {
                Db.open_connection();
                command = new MySqlCommand("fetchmemberlist", Db.get_connection());
                command.Parameters.Add("@classid", MySqlDbType.Int32).Value = Convert.ToInt32(Dashboard.SelectedString);
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                if(dt.Rows.Count > 0)
                {
                    dgStudList.Visible = true;
                    panelEmpty.Visible = false;
                    dgStudList.DataSource = bs;
                    dgStudList.Columns[3].Visible = false;
                    dgStudList.Columns[4].Visible = false;
                    dgStudList.Columns[6].Visible = false;
                    lbltotalmember.Text = "Total Member: " + dt.Rows.Count.ToString();
                }
                else
                {
                    dgStudList.Visible = false;
                    panelEmpty.Visible = true;
                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }
        void Load_teacher()
        {
            try
            {
                collection.EmpTeacherName.Clear();
                collection.EmpidTeacherEmpid.Clear();
                dropTc.Items.Clear();
                Db.open_connection();
                command = new MySqlCommand("fetch_teacher_assign_class", Db.get_connection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@teacher", MySqlDbType.VarChar).Value = "Teacher";
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        collection.EmpidTeacherEmpid.Add(reader.GetString("employeeid"));
                        collection.EmpTeacherName.Add(reader.GetString("emp_fullname"));
                    }
                    for (int i = 0; i < collection.EmpTeacherName.Count; i++)
                    {
                        dropTc.Items.Add(collection.EmpTeacherName[i]);
                    }
                }
                reader.Close();
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }
        void Load_assistantTeacher()
        {
            try
            {
                collection.EmpAssistantTeacherName.Clear();
                collection.EmpidAssistantTeacherEmpid.Clear();
                dropAssTc.Items.Clear();
                Db.open_connection();
                command = new MySqlCommand("fetch_teacher_assign_class", Db.get_connection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@teacher", MySqlDbType.VarChar).Value = "Assistant Teacher";
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        collection.EmpidAssistantTeacherEmpid.Add(reader.GetString("employeeid"));
                        collection.EmpAssistantTeacherName.Add(reader.GetString("emp_fullname"));
                    }
                    for (int i = 0; i < collection.EmpAssistantTeacherName.Count; i++)
                    {
                        dropAssTc.Items.Add(collection.EmpAssistantTeacherName[i]);
                    }
                }
                else
                {

                }
                reader.Close();
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }
        public void LoadInfo()
        {
            int careteacher = 0, assistant=0;
            string grade = null;
            Load_teacher();
            Load_assistantTeacher();
            try
            {
                Db.open_connection();
                command = new MySqlCommand("fetch_class_info", Db.get_connection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@classid", MySqlDbType.Int32).Value = Dashboard.SelectedString;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        collection.EmpTeacherName.Add(reader.GetString("Care_Teacher_Name"));
                        collection.EmpidTeacherEmpid.Add(reader.GetString("careteacher"));
                        grade = reader.GetString("grade");
                        if(grade == "NURSERY" | grade == "KINDERGARTEN 1" | grade == "KINDERGARTEN 2" | grade == "KINDERGARTEN 3")
                        {
                            dropAssTc.Visible = true;
                            try
                            {
                                assistant = reader.GetInt32("assistant");

                            }
                            catch (Exception)
                            {
                                assistant = 0;
                            }
                            if(assistant == 0 | assistant == null)
                            {

                            }
                            else
                            {
                                collection.EmpAssistantTeacherName.Add(reader.GetString("Assistant_Care_Teacher"));
                                collection.EmpidAssistantTeacherEmpid.Add(reader.GetString("assistant"));
                                dropAssTc.Items.Add(reader.GetString("Assistant_Care_Teacher"));
                            }
                        }
                        else
                        {
                            dropAssTc.Visible = false; 
                        }
                        careteacher = reader.GetInt32("careteacher");
                        try
                        {
                            assistant = reader.GetInt32("assistant");

                        }
                        catch (Exception)
                        {
                            assistant = 0;
                        }
                        txtClassName.Text = reader.GetString("classname");
                        txtClassCapacity.Text = reader.GetString("class_capacity");
                        dropTc.Items.Add(reader.GetString("care_teacher_name"));
                    }
                    dropTc.SelectedIndex = collection.EmpidTeacherEmpid.IndexOf(careteacher.ToString());
                    if(assistant == 0 | assistant == null)
                    {

                    }
                    else
                    {
                        dropAssTc.SelectedIndex = collection.EmpidAssistantTeacherEmpid.IndexOf(assistant.ToString());
                    }
                }
                reader.Close();
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }
        public void InitClassList()
        {
            dropAcademicYear.Items.Clear();
            //academicyearDropdown
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select academicyear from academic_year", Db.get_connection());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dropAcademicYear.Items.Add(reader.GetString("academicyear"));
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        public void InitClass()
        {
            try
            {
                Db.open_connection();
                command = new MySqlCommand("LoadClassLListFilter", Db.get_connection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ay", MySqlDbType.VarChar).Value = dropAcademicYear.SelectedItem.ToString();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 1)
                {
                    FlowClassList.Controls.Clear();
                    UCClassModel[] classlist = new UCClassModel[dt.Rows.Count];  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        classlist[i] = new UCClassModel();
                        classlist[i].ClassName = dt.Rows[i][0].ToString();
                        classlist[i].CareTeacher = "Care Teacher: " + dt.Rows[i][6].ToString();
                        classlist[i].ClassIdentifier = Convert.ToInt32(dt.Rows[i][7].ToString());
                        classlist[i].ClassDepartment = "Class Department: " + dt.Rows[i][8].ToString();
                        classlist[i].ClassMember = "Class Member: " + dt.Rows[i][9].ToString();
                        FlowClassList.Controls.Add(classlist[i]);
                    }
                    lbltotalClass.Text = "Total Class: " + dt.Rows.Count.ToString();
                }
                else
                {

                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Revise_With_Ass()
        {
            try
            {
                Db.open_connection();
                command = new MySqlCommand("reviseClass_WithAssistant", Db.get_connection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@nameclass", MySqlDbType.VarChar).Value = txtClassName.Text;
                command.Parameters.Add("@upcareteacher", MySqlDbType.Int32).Value = collection.EmpidTeacherEmpid[collection.EmpidTeacherEmpid.IndexOf(dropTc.SelectedItem.ToString())];
                command.Parameters.Add("@upassistant", MySqlDbType.Int32).Value = collection.EmpidTeacherEmpid[collection.EmpidTeacherEmpid.IndexOf(dropTc.SelectedItem.ToString())];
                command.Parameters.Add("@upclasscapacity", MySqlDbType.Int32).Value = Convert.ToInt32(txtClassCapacity.Text);
                command.Parameters.Add("@classid", MySqlDbType.Int32).Value = Convert.ToInt32(Dashboard.SelectedString);
                if (command.ExecuteNonQuery() == 1)
                {
                    Msg.Alert("Class revised succesfully!", frmAlert.AlertType.Success);
                }
                else
                {
                    Msg.Alert("Error Occured", frmAlert.AlertType.Error);
                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }
        void Revise_Without_Ass()
        {
            try
            {
                Db.open_connection();
                command = new MySqlCommand("ReviseClass", Db.get_connection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@nameclass", MySqlDbType.VarChar).Value = txtClassName.Text;
                command.Parameters.Add("@upcareteacher", MySqlDbType.Int32).Value = collection.EmpidTeacherEmpid[collection.EmpTeacherName.IndexOf(dropTc.SelectedItem.ToString())];
                command.Parameters.Add("@upclasscapacity", MySqlDbType.Int32).Value = Convert.ToInt32(txtClassCapacity.Text);
                command.Parameters.Add("@classid", MySqlDbType.Int32).Value = Convert.ToInt32(Dashboard.SelectedString);
                if (command.ExecuteNonQuery() == 1)
                {
                    Msg.Alert("Class revised succesfully!", frmAlert.AlertType.Success);
                }
                else
                {
                    Msg.Alert("Error Occured", frmAlert.AlertType.Error);
                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }

        }
        #endregion

        #region EventListener
        private void BtnGoBack_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void dropAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitClass();
        }


        #endregion

        private void btnClassMember_Click(object sender, EventArgs e)
        {
            CSwitcher(ClassEnum.ClassMember);
        }

        private void btnEditClassInfo_Click(object sender, EventArgs e)
        {
            CSwitcher(ClassEnum.EditClassInfo);
        }
       
        private void btnRevise_Click(object sender, EventArgs e)
        {
            if(dropAssTc.SelectedIndex == -1)
            {
                Revise_Without_Ass();
            }
            else
            {
                Revise_With_Ass();
            }
        }
    }
}
