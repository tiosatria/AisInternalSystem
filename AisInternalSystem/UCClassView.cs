using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Module;
using MySql.Data.MySqlClient;
using AisInternalSystem.Properties;
using Guna.UI2.WinForms;
using System.Numerics;

namespace AisInternalSystem
{
    public partial class UCClassView : UserControl
    {
        int ClassID = 0 ;
        Int64 empId = 0;
        Dialog msg = new Dialog();
        ModuleInProgress indevelopment = new ModuleInProgress();
        UCEmployeeDetailed openemployee = new UCEmployeeDetailed();
        MySqlCommand command;

        public UCClassView()
        {
            InitializeComponent();
        }

        #region EventListener
        private void btnBackto_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void btnOngoing_Click(object sender, EventArgs e)
        {
            Cswitcher(ClassViewEnum.Grading);
        }

        private void btnPastSchool_Click(object sender, EventArgs e)
        {
            Cswitcher(ClassViewEnum.Attendance);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Cswitcher(ClassViewEnum.SubjectTeacher);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Cswitcher(ClassViewEnum.Media);
        }
        #endregion

        #region Function
        void FocusButton(Guna2Button btn)
        {
            btnGrading.FillColor = Color.Silver;
            btnGrading.ForeColor = Color.Black;
            btnAttendance.FillColor = Color.Silver;
            btnAttendance.ForeColor = Color.Black;
            btnSubjectTeacher.FillColor = Color.Silver;
            btnSubjectTeacher.ForeColor = Color.Black;
            btnMedia.FillColor = Color.Silver;
            btnMedia.ForeColor = Color.Black;
            btn.FillColor = Color.Black;
            btn.ForeColor = Color.White;
        }
        public void Cswitcher(ClassViewEnum u)
        {
            _enumclass = u;
            switch (_enumclass)
            {
                case ClassViewEnum.Grading:
                    FocusButton(btnGrading);
                    PanelGrading.BringToFront();
                    GradingInit();
                    break;
                case ClassViewEnum.Attendance:
                    FocusButton(btnGrading);
                    PanelClassAttendance.BringToFront();
                    AttendanceInit();
                    break;
                case ClassViewEnum.SubjectTeacher:
                    FocusButton(btnSubjectTeacher);
                    PanelSubjectTeacher.BringToFront();
                    SubjectTeacherInit();
                    break;
                case ClassViewEnum.Media:
                    FocusButton(btnMedia);
                    PanelClassMedia.BringToFront();
                    ClassMediaInit();
                    break;
            }
        }
        void ClassMediaInit()
        {
            PanelClassMedia.Controls.Add(indevelopment);
            indevelopment.Location = new Point(174, 73);
        }
        void SubjectTeacherInit()
        {
            PanelSubjectTeacher.Controls.Add(indevelopment);
            indevelopment.Location = new Point(174, 73);
        }
        void AttendanceInit()
        {
            PanelClassAttendance.Controls.Add(indevelopment);
            indevelopment.Location = new Point(174, 73);
        }
        void GradingInit()
        {
            PanelGrading.Controls.Add(indevelopment);
            indevelopment.Location = new Point(174, 73);
        }
        public void loadStudentList(int u)
        {
            ClassID = u;
            //memberlist
            Db.open_connection();
            command = new MySqlCommand("fetchmemberlist", Db.get_connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@classid", MySqlDbType.Int32).Value = u;
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                flowStudentList.Controls.Clear();
                UCClassMember[] students = new UCClassMember[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    students[i] = new UCClassMember();
                    students[i].RowNumber = i + 1;
                    students[i].Name = dt.Rows[i][0].ToString();
                    students[i].Religion = dt.Rows[i][1].ToString();
                    students[i].Birthdate = dt.Rows[i][2].ToString();
                    students[i].Mobile = dt.Rows[i][3].ToString();
                    students[i].Aisid = Convert.ToInt32(dt.Rows[i][7].ToString());
                    if(students[i].Mobile == "" | students[i].Mobile == null)
                    {
                        students[i].Mobile = dt.Rows[i][4].ToString();
                        if(students[i].Mobile == "" | students[i].Mobile == null)
                        {
                            students[i].Mobile = "No info";
                        }
                    }
                    students[i].EnglishProficiency = dt.Rows[i][5].ToString();
                    try
                    {
                        students[i].StudentPicture = Image.FromFile(dt.Rows[i][6].ToString());

                    }
                    catch (Exception)
                    {
                        students[i].StudentPicture = Resources.icons8_student_male_60px;
                    }
                    flowStudentList.Controls.Add(students[i]);
                    lbltotalstud.Text = "Total: " + dt.Rows.Count.ToString();
                }
            }
            else
            {
                flowStudentList.Controls.Clear();
            }
            command = new MySqlCommand("fetch_class_info", Db.get_connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@classid", MySqlDbType.Int32).Value = ClassID;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lblCareTeacher.Text = reader.GetString("care_teacher_name");
                lblBirthdate.Text = "Birthdate: " + reader.GetDateTime("ct_birthdate").ToString("d");
                lblContact.Text = "Contact: " + reader.GetString("ct_contact");
                try
                {
                    picCareTeacher.Image = Image.FromFile(reader.GetString("ct_pic"));

                }
                catch (Exception)
                {
                    picCareTeacher.Image = Resources.icons8_student_male_60px;
                }
                lblClassName.Text = reader.GetString("classname");
                lblAcademicYear.Text = "ACADEMIC YEAR: " + reader.GetString("academicyear");
                lblTerms.Text = "Terms: " + reader.GetString("termsacademicyear");
                lblAYCode.Text = "Academic Year Code: " + reader.GetString("aycode");
                empId = reader.GetInt64("emp_idtc");
            }
            reader.Close();
            Db.close_connection();
        }
        public void Init(int i)
        {

        }
        #endregion

        #region Properties
        public enum ClassViewEnum
        {
            Grading, 
            Attendance,
            SubjectTeacher,
            Media
        };
        private ClassViewEnum _enumclass;

        #endregion

        private void guna2ShadowPanel3_Click(object sender, EventArgs e)
        {
            Dashboard mainform;
            mainform = (Dashboard)this.FindForm();
            mainform.Controls.Add(openemployee);
            mainform.Controls[mainform.Controls.IndexOf(openemployee)].BringToFront();
            mainform.Controls[mainform.Controls.IndexOf(openemployee)].Dock = DockStyle.Fill;
            openemployee.empID = empId;
            openemployee.LoadEmployeeData();
        }
    }
}
