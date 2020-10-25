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
        Db db = new Db();
        MySqlDataReader reader;
        Dialog msg = new Dialog();
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
        public void CSwitcher()
        {

        }

        public void InitClassList()
        {
            dropAcademicYear.Items.Clear();
            //academicyearDropdown
            try
            {
                db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select academicyear from academic_year", db.get_connection());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dropAcademicYear.Items.Add(reader.GetString("academicyear"));
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        public void InitClass()
        {
            try
            {
                db.open_connection();
                command = new MySqlCommand("LoadClassLListFilter", db.get_connection());
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
                db.close_connection();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
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

    }
}
