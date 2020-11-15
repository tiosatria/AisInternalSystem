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
using AisInternalSystem.Controller;
using AisInternalSystem.Properties;
using Guna.UI2.WinForms;
using AisInternalSystem.Entities;
using System.Web.UI.Design;
using Telerik.WinControls;
using System.Runtime.CompilerServices;
using Telerik.WinControls.UI;
using Microsoft.VisualBasic.CompilerServices;
using System.Web.Services.Discovery;
using System.Threading;
using System.Net.Configuration;
using MySql.Data.MySqlClient;
using Data = AisInternalSystem.Controller.Data;
using System.Management;

namespace AisInternalSystem
{
    public partial class UCSubject : UserControl
    {
        private UserControl control = new UserControl();
        private bool isLoaded;
        public UCSubject()
        {
        }
        public void InitObject()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                InitDropDown();
                MenuSwitcher(MenuSubject.Create);
                backgroundWorker1.RunWorkerAsync();
            }
            isLoaded = true;
        }
        private void InitDropDown()
        {
            foreach (Grade grade in Data.grades)
            {
                if (grade.GradeLevel == 0)
                {
                    
                }
                else
                {
                    dropGrade.Items.Add(grade.GradeName);
                }
            }
            foreach (Teacher teacher in Data.teachersList)
            {
                dropTeacher.Items.Add(teacher.TeacherName);
            }
            dropGrade.SelectedIndex = 0;
            dropTeacher.SelectedIndex = 0;
        }
        private int _progressCopy;

        public int ProgressCopy
        {
            get { return _progressCopy; }
            set
            {
                _progressCopy = value; if (value == 100)
                {
                    isFinished = true;
                }
            }
        }
        private bool isFinished;

        public bool ProcessIsFinished
        {
            get { return isFinished; }
            set
            {
                isFinished = value; if (true)
                {
                    MessageBox.Show("Completed");
                } }
        }

        #region Properties
        OpenFileDialog imageLocation;
        #endregion
        enum MenuSubject
        {
            Create, Edit, Assign
        }
        private MenuSubject _menu;
        #region Function

        private void FocusButton(Guna2Button button)
        {
            UIController.HighlightButton(new List<Guna2Button> { btnCreateSubject, btnEditSubject, btnAssignsubject }, button);
        }

        private void MenuSwitcher(MenuSubject men_)
        {
            _menu = men_;
            switch (men_)
            {
                case MenuSubject.Create:
                    FocusButton(btnCreateSubject);
                    PanelCreateSubject.BringToFront();
                    break;
                case MenuSubject.Edit:
                    FocusButton(btnEditSubject);
                    PanelEditSubject.BringToFront();
                    break;
                case MenuSubject.Assign:
                    FocusButton(btnAssignsubject);
                    PanelAssign.BringToFront();
                    break;
            }   
        }

        private void InitAssign()
        {
            if (SelectedSubjectID == 0)
            {
                lblsubjnoselectassign.Visible = true;

            }
            else
            {
                lblsubjnoselectassign.Visible = false;
                guna2ShadowPanel3.Visible = true;
                guna2ShadowPanel8.Visible = true;

            }
        }

        private bool SubjectListLoaded;
        private bool onEditMode;
        private void GetSubjectList()
        {
            int controlCount = -1;
            int IndexControl = controlCount;

            if (flowSubjectList.InvokeRequired)
            {
                flowSubjectList.Invoke(new MethodInvoker(delegate { controlCount = flowSubjectList.Controls.Count; }));
            }
            List<Subject> subjectList = Subject.SubjectList();
            if (SubjectListLoaded)
            {
                if (subjectList.Count > controlCount)
                {
                    int lastIndexInsert = subjectList.FindIndex(o => o.SubjectName == subjName);
                    UCSubjectList subjects = new UCSubjectList();
                    subjects.SubjectName = subjectList[lastIndexInsert].SubjectName;
                    subjects.SubjectDescription = subjectList[lastIndexInsert].SubjectDescription;
                    subjects.SubjectID = subjectList[lastIndexInsert].SubjectID;
                    subjects.ImageLocation = subjectList[lastIndexInsert].SubjectImageLocation;
                    subjects.TaughtBy = subjectList[lastIndexInsert].IsTaughtBy;
                    subjects.TaughtIn = subjectList[lastIndexInsert].TaughtIn;
                    subjects.IndexOnControl = IndexControl++;
                    Invoke(new MethodInvoker(delegate { flowSubjectList.Controls.Add(subjects); }));
                }
                else
                {

                }
                if (onEditMode)
                {
                    Invoke(new MethodInvoker(delegate { flowSubjectList.Controls.Clear(); }));
                    if (subjectList.Count >= 1)
                    {
                        
                        UCSubjectList[] subjects = new UCSubjectList[subjectList.Count];
                        for (int i = 0; i < subjectList.Count; i++)
                        {
                            subjects[i] = new UCSubjectList();
                            subjects[i].SubjectName = subjectList[i].SubjectName;
                            subjects[i].SubjectDescription = subjectList[i].SubjectDescription;
                            subjects[i].SubjectID = subjectList[i].SubjectID;
                            subjects[i].ImageLocation = subjectList[i].SubjectImageLocation;
                            subjects[i].TaughtBy = subjectList[i].IsTaughtBy;
                            subjects[i].TaughtIn = subjectList[i].TaughtIn;
                            subjects[i].IndexOnControl = IndexControl++;
                            Invoke(new MethodInvoker(delegate { flowSubjectList.Controls.Add(subjects[i]); }));
                        }
                    }
                    onEditMode = false;
                }
            }
            else
            {
                if (subjectList.Count >= 1)
                {
                    UCSubjectList[] subjects = new UCSubjectList[subjectList.Count];
                    for (int i = 0; i < subjectList.Count; i++)
                    {
                        subjects[i] = new UCSubjectList();
                        subjects[i].SubjectName = subjectList[i].SubjectName;
                        subjects[i].SubjectDescription = subjectList[i].SubjectDescription;
                        subjects[i].SubjectID = subjectList[i].SubjectID;
                        subjects[i].ImageLocation = subjectList[i].SubjectImageLocation;
                        subjects[i].TaughtBy = subjectList[i].IsTaughtBy;
                        subjects[i].TaughtIn = subjectList[i].TaughtIn;
                        subjects[i].IndexOnControl = IndexControl++;
                        Invoke(new MethodInvoker(delegate { flowSubjectList.Controls.Add(subjects[i]); }));
                    }
                }
            }
            SubjectListLoaded = true;
            Invoke(new MethodInvoker(delegate { lbleditsubjecttotal.Text = "Total: " + flowSubjectList.Controls.Count.ToString(); }));
        }
        string selectedSubjectName = "";
        private int SelectedSubjectID = 0;
        public void TeacherRevoke(UCSubjectTeacher teacher)
        {
            if (Query.Delete("RevokeSubjectTeacher", new string[3] {"@_subject_taught", "@_in_grade", "@_teacher"}, new MySqlDbType[3] { MySqlDbType.Int32, MySqlDbType.VarChar, MySqlDbType.Int32 }, new string[3] {SelectedSubjectID.ToString(), teacher.Grade, teacher.TeacherID.ToString() }))
            {
                PopUp.Alert("Teacher " + teacher.TeacherName + " has been revoked!", frmAlert.AlertType.Success);
                foreach (UCSubjectTeacher item in FlowSubjectTeacher.Controls)
                {
                    if (item.TeacherID == teacher.TeacherID && item.Grade == teacher.Grade)
                    {
                        FlowSubjectTeacher.Controls.Remove(item);
                    }
                }
            }
            else
            {
                PopUp.Alert("Something is wrong, we couldn't revoke " + teacher.Name, frmAlert.AlertType.Error);
            }
        }
        public void SubjectChoosedEvent(UCSubjectList selectedSubject)
        {
            foreach (var item in flowSubjectList.Controls)
            {
                if (item is UCSubjectList)
                {
                    UCSubjectList subject = item as UCSubjectList;
                    foreach (Guna2ShadowPanel panelItem in subject.Controls)
                    {
                        panelItem.FillColor = Color.White;
                    }
                }
            }
            foreach (Guna2ShadowPanel item in selectedSubject.Controls)
            {
                item.FillColor = Color.Coral;
            }
            SelectedSubjectID = selectedSubject.SubjectID;
            txteditSubjectName.Text = selectedSubject.SubjectName;
            txtEditSubjectDesc.Text = selectedSubject.SubjectDescription;
            piceditSubjectPic.Image = selectedSubject.SubjectImage;
            selectedSubjectName = selectedSubject.SubjectName;
            InitAssign();
            picSelectedAssign.Image = selectedSubject.SubjectImage;
            lblassignistaughtby.Text = "Is taught by: " + selectedSubject.TaughtBy;
            lblassignistaughtin.Text = "Is Taught in: " + selectedSubject.TaughtIn;
            lblassignsubjectname.Text = "Subject Name: " + selectedSubject.SubjectName;
            WorkerAssignTeacher.RunWorkerAsync();
        }
        private int SelectedIndexControl;
        private void EditSubject()
        {
            onEditMode = true;
            if (Subject.EditSubject(new string[3] {SelectedSubjectID.ToString(), txteditSubjectName.Text, txtEditSubjectDesc.Text }))
            {
                PopUp.Alert("Subject Edited succesfully!", frmAlert.AlertType.Success);
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void AddSubject()
        {
            subjName = txtSubjectName.Text;
            if (txtSubjectName.Text == "" || txtSubjectDesc.Text == "")
            {
                PopUp.Alert("Please enter subject name and description", frmAlert.AlertType.Warning);
            }
            else
            {
                if (UseCustomImage)
                {
                    string dbString = Utilities.GetFileDbLocationString(Utilities.LocationType.SubjectPhoto, txtSubjectName.Text, imageLocation);
                    if (Subject.InsertSubject(new string[5]
            {
             txtSubjectName.Text,
                Utilities.GetCurrentUserID().ToString(),
                Utilities.GetTimeStamp(), dbString
                ,
                txtSubjectDesc.Text
            }))
                    {
                        Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { imageLocation.FileName, dbString });
                        backgroundWorker1.RunWorkerAsync();
                        ClearInsertSubjectForm();
                    }
                }
                else
                {
                    if (Subject.InsertSubject(new string[5]
            {
             txtSubjectName.Text,
                Utilities.GetCurrentUserID().ToString(),
                Utilities.GetTimeStamp(), drop_image_Preset.SelectedItem.ToString()
                ,
                txtSubjectDesc.Text
            }))
                    {
                        backgroundWorker1.RunWorkerAsync();
                        ClearInsertSubjectForm();
                    }
                }
            }
        }
        #endregion

        private void ClearInsertSubjectForm()
        {
            txtSubjectName.Text = "";
            txtSubjectDesc.Text = "";
            pic_subject.Image = Resources.subjectdefault;
            drop_image_Preset.SelectedIndex = 0;
        }

        #region EventListener
        private void drop_image_Preset_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        string subjName = string.Empty;
        private void btnAddSubject_Click_1(object sender, EventArgs e)
        {
            AddSubject();
        }
        #endregion

        private void btnCreateSubject_Click(object sender, EventArgs e)
        {
            MenuSwitcher(MenuSubject.Create);
        }
        private void LoadSubject()
        {

        }

        private void btnEditSubject_Click(object sender, EventArgs e)
        {
            MenuSwitcher(MenuSubject.Edit);
        }

        private void btnAssignsubject_Click(object sender, EventArgs e)
        {
            MenuSwitcher(MenuSubject.Assign);
        }

        private void PanelCreateSubject_Paint(object sender, PaintEventArgs e)
        {

        }
        private void DoSearch()
        {
            foreach (UCSubjectList item in flowSubjectList.Controls)
            {
                if (item.SubjectName.ToLower().Contains(txtSearch.Text))
                {
                    flowSubjectList.Controls[flowSubjectList.Controls.IndexOf(item)].Show();
                    item.Visible = true;
                }
                else if (item.SubjectDescription.ToLower().Contains(txtSearch.Text))
                {
                    flowSubjectList.Controls[flowSubjectList.Controls.IndexOf(item)].Show();
                    item.Visible = true;
                }
                else
                {
                    flowSubjectList.Controls[flowSubjectList.Controls.IndexOf(item)].Hide();
                    item.Visible = false;
                }
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DoSearch();
        }
        private bool UseCustomImage;
        private void pic_subject_Click(object sender, EventArgs e)
        {
            UseCustomImage = true;
            imageLocation = Utilities.OpenImage(pic_subject);
            if (imageLocation.SafeFileName == "" || imageLocation.SafeFileName == null)
            {
                UseCustomImage = false;
            }
        }
        private void SwitchImage()
        {
            UseCustomImage = false;
            if (drop_image_Preset.SelectedItem == "Default")
            {
                pic_subject.Image = Resources.subjectdefault;
            }
            else if (drop_image_Preset.SelectedItem == "Biology")
            {
                pic_subject.Image = Resources.subjectBiology;
            }
            else if (drop_image_Preset.SelectedItem == "English")
            {
                pic_subject.Image = Resources.subjectEnglish;
            }
            else if (drop_image_Preset.SelectedItem == "Geography")
            {
                pic_subject.Image = Resources.subjectGeography;
            }
            else if (drop_image_Preset.SelectedItem == "History")
            {
                pic_subject.Image = Resources.subjectHistory;
            }
            else if (drop_image_Preset.SelectedItem == "Math")
            {
                pic_subject.Image = Resources.subjectMath;
            }
            else if (drop_image_Preset.SelectedItem == "Science")
            {
                pic_subject.Image = Resources.subjectScience;
            }
            else if (drop_image_Preset.SelectedItem == "Social")
            {
                pic_subject.Image = Resources.subjectSocial;
            }
            else if (drop_image_Preset.SelectedItem == "Technology")
            {
                pic_subject.Image = Resources.subjectTechnology;
            }
            else
            {
                try
                {
                    pic_subject.Image = Image.FromFile(imageLocation.FileName);
                }
                catch (Exception)
                {
                    pic_subject.Image = Resources.subjectdefault;
                }
            }
        }
        private void drop_image_Preset_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SwitchImage();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            GetSubjectList();
        }
        private void GetSubjectTeacherList()
        {
            Invoke(new MethodInvoker(delegate { FlowSubjectTeacher.Controls.Clear(); }));
            if (SelectedSubjectID != 0)
            {
                List<UCSubjectTeacher> teachers = Subject.GetSubjectTeacher(SelectedSubjectID);
                foreach (UCSubjectTeacher item in teachers)
                {
                    Invoke(new MethodInvoker(delegate { FlowSubjectTeacher.Controls.Add(item); }));
                }
            }
            else
            {

            }
        }
        //feature update
        private void dropEditPicPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopUp.Alert("Cannot change image preset on edit mode\nIn This Version!", frmAlert.AlertType.Warning);
        }
        //feature update
        private void piceditSubjectPic_Click(object sender, EventArgs e)
        {
            PopUp.Alert("Cannot change image preset on edit mode\nIn This Version!", frmAlert.AlertType.Warning);

        }

        private void btnEditOk_Click(object sender, EventArgs e)
        {
            if (SelectedSubjectID == 0)
            {
                PopUp.Alert("To edit subject, please select subject\nOn the left panel", frmAlert.AlertType.Warning);
            }
            else
            {
                    EditSubject();
            }
            if (txteditSubjectName.Text == "" || txteditSubjectName.Text == null)
            {
                PopUp.Alert("Please specify the subject name", frmAlert.AlertType.Warning);
            }
        }

        private void AssignSubject(string[] str)
        {
            if (Subject.AssignSubject(new string[3] { str[0], str[1], str[2] }))
            {
                PopUp.Alert("Teacher has assigned succesfully!", frmAlert.AlertType.Success);
            }
            else
            {
                PopUp.Alert("Failed to assign teacher, possibly duplicates", frmAlert.AlertType.Error);
            }
        }
        private void btnAssignTo_Click(object sender, EventArgs e)
        {
            AssignSubject(new string[3] {SelectedSubjectID.ToString(), Data.grades[Data.grades.FindIndex(o=> o.GradeName == dropGrade.SelectedItem.ToString())].GradeName, Data.teachersList[Data.teachersList.FindIndex(o=> o.TeacherName == dropTeacher.SelectedItem.ToString())].TeacherID.ToString()});
        }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            onEditMode = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void DeleteSubject()
        {
            if (SelectedSubjectID == 0)
            {
                PopUp.Alert("Please select subject on left panel", frmAlert.AlertType.Warning);
            }
            else if (Query.Delete("DeleteSubject", new string[1] { "@_subjectid" }, new MySqlDbType[1] { MySqlDbType.Int32 }, new string[1] { SelectedSubjectID.ToString() }))
            {
                PopUp.Alert("Subject deleted succesfully!", frmAlert.AlertType.Success);
                foreach (UCSubjectList item in flowSubjectList.Controls)
                {
                    if (item.SubjectID == SelectedSubjectID)
                    {
                        flowSubjectList.Controls.Remove(item);
                    }
                }
                lbleditsubjecttotal.Text += " (Not Realtime)";
            }
            else
            {
                PopUp.Alert("Couldn't delete subject, ensure that\n the corresponding subject has no teacher", frmAlert.AlertType.Warning);
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DeleteSubject();
        }

        private void WorkerAssignTeacher_DoWork(object sender, DoWorkEventArgs e)
        {
            GetSubjectTeacherList();
        }
    }
}
