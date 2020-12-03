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
using AisInternalSystem.Controller; 
using System.Numerics;
using MediaFoundation.Alt;

namespace AisInternalSystem
{
    public partial class UCClassView : UserControl
    {
        private bool isLoaded = false;
        public UCClassView()
        {

        }
        private void ClassRoom_CurrentClassChangedEvent(object sender, Entities.ClassRoom e)
        {
            Init();
        }

        public void InitObject()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                Entities.ClassRoom.CurrentClassChangedEvent += ClassRoom_CurrentClassChangedEvent;
                Init();
            }
            else
            {

            }
            isLoaded = true;
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
                    break;
                case ClassViewEnum.Attendance:
                    FocusButton(btnAttendance);
                    PanelClassAttendance.BringToFront();
                    break;
                case ClassViewEnum.SubjectTeacher:
                    FocusButton(btnSubjectTeacher);
                    PanelSubjectTeacher.BringToFront();
                    break;
                case ClassViewEnum.Media:
                    FocusButton(btnMedia);
                    PanelClassMedia.BringToFront();
                    break;
            }
        }
        BackgroundWorker worker = null;

        private void Init()
        {
            btnBackto.Enabled = false;
            Utilities.Clear(flowStudentList.Controls, true);
            flowStudentList.Controls.Clear();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            PopUp.Alert("Please wait while we're fetching class information...", frmAlert.AlertType.Info);
            this.Cswitcher(UCClassView.ClassViewEnum.Grading);
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (worker != null)
            {
                PopUp.Alert("Class info loaded succesfully!", frmAlert.AlertType.Success);
                btnBackto.Enabled = true;
                worker.Dispose();
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadClassInfo();
            LoadClassMember();
        }


        private void LoadClassMember()
        {
            Entities.ClassRoom currentClass = Entities.ClassRoom.CurrentClass;
            if (currentClass != null)
            {
                if (currentClass.ListOfStudent.Count >= 1)
                {
                    List<Entities.Student> studentList = currentClass.ListOfStudent;
                    UCClassMember[] classMembers = new UCClassMember[currentClass.ListOfStudent.Count];
                    for (int i = 0; i < currentClass.ListOfStudent.Count; i++)
                    {
                        classMembers[i] = new UCClassMember();
                        classMembers[i].StudentName = studentList[i].CertificateName;
                        try
                        {
                            classMembers[i].StudentPicture = Image.FromFile(studentList[i].PhotoLocation);

                        }
                        catch (Exception)
                        {
                            classMembers[i].StudentPicture = Resources.icons8_male_user_100;
                        }
                        classMembers[i].Religion = studentList[i].Religion;
                        classMembers[i].Birthdate = studentList[i].DateofBirth.ToString("d");
                        classMembers[i].Mobile = studentList[i].MobileNumber;
                        classMembers[i].EnglishProficiency = studentList[i].EnglishProficiency;
                        classMembers[i].RowNumber = studentList.IndexOf(studentList[i]);
                        Invoke(new MethodInvoker(delegate { flowStudentList.Controls.Add(classMembers[i]);
                                                            lbltotalstud.Text = $"Total: {flowStudentList.Controls.Count}";
                        }));
                    }
                }
                else
                {
                    PopUp.Alert("No student is assigned to this class!", frmAlert.AlertType.Warning);
                }
            }
            else
            {
                MessageBox.Show("ANJING");
            }
        }

        private void LoadClassInfo()
        {
            Entities.ClassRoom currentClass = Entities.ClassRoom.CurrentClass;
            if (currentClass!=null)
            {
                Invoke(new MethodInvoker(delegate {
                    lblClassName.Text = currentClass.ClassName;
                    lblCareTeacher.Text = currentClass.CareTeacher.TeacherName;
                    lblBirthdate.Text = $"Birthdate: {currentClass.CareTeacher.BirthDate.ToString("d")}";
                    lblContact.Text = $"Contact: {currentClass.CareTeacher.Contact}";
                    lblAcademicYear.Text = $"ACADEMIC YEAR {currentClass.AcademicYear}";
                    lblAYCode.Text = $"Academic Year Code: {currentClass.AYCode}";
                    lblTerms.Text = $"Term: { currentClass.Term}";
                    try
                    {
                        picCareTeacher.Image = Image.FromFile(currentClass.CareTeacher.ImageLocation);
                    }
                    catch (Exception)
                    {
                        picCareTeacher.Image = Resources.icons8_male_user_100;
                    }
                }));
            }
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
    }
}
