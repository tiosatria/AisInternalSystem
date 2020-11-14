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
using Guna.UI2.WinForms;
using AisInternalSystem.Entities;
using System.Web.UI.Design;
using Telerik.WinControls;
using System.Runtime.CompilerServices;
using Telerik.WinControls.UI;
using Microsoft.VisualBasic.CompilerServices;
using System.Web.Services.Discovery;

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
                MenuSwitcher(MenuSubject.Create);
                GetSubjectList();
            }
            isLoaded = true;
        }

        #region Properties
        string imageLocation = string.Empty;
        #endregion
        enum MenuSubject
        {
            Create, Edit, Assign
        }
        private MenuSubject _menu;
        #region Function
        private void MenuSwitcher(MenuSubject men_)
        {
            _menu = men_;
            switch (men_)
            {
                case MenuSubject.Create:
                    UIController.FocusButtonNoPanel(btnCreateSubject, this);
                    PanelCreateSubject.BringToFront();
                    break;
                case MenuSubject.Edit:
                    UIController.FocusButtonNoPanel(btnEditSubject, this);
                    PanelEditSubject.BringToFront();
                    break;
                case MenuSubject.Assign:
                    UIController.FocusButtonNoPanel(btnAssignsubject, this);
                    PanelAssign.BringToFront();
                    break;
            }   
        }
        private bool SubjectListLoaded;
        private void GetSubjectList()
        {
            List<Subject> subjectList = Subject.SubjectList();
            if (SubjectListLoaded)
            {
                if (subjectList.Count > flowSubjectList.Controls.Count)
                {
                    int lastIndexInsert = subjectList.FindIndex(o => o.SubjectName == txtSubjectName.Text);
                    UCSubjectList subjects = new UCSubjectList();
                    subjects.SubjectName = subjectList[lastIndexInsert].SubjectName;
                    subjects.SubjectDescription = subjectList[lastIndexInsert].SubjectDescription;
                    subjects.SubjectID = subjectList[lastIndexInsert].SubjectID;
                    subjects.ImageLocation = subjectList[lastIndexInsert].SubjectImageLocation;
                    subjects.TaughtBy = subjectList[lastIndexInsert].IsTaughtBy;
                    subjects.TaughtIn = subjectList[lastIndexInsert].TaughtIn;
                    flowSubjectList.Controls.Add(subjects);
                }
                else
                {

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
                        flowSubjectList.Controls.Add(subjects[i]);
                    }
                }
            }
            SubjectListLoaded = true;
            lbleditsubjecttotal.Text = "Total: " + flowSubjectList.Controls.Count.ToString();
        }
        private void AddSubject()
        {
            if (txtSubjectName.Text == "" || txtSubjectDesc.Text == "")
            {
                PopUp.Alert("Please enter subject name and description", frmAlert.AlertType.Warning);
            }
            else
            {
                if(Subject.InsertSubject(new string[5]
            {
             txtSubjectName.Text,
                Utilities.GetCurrentUserID().ToString(),
                Utilities.GetTimeStamp(),
                imageLocation,
                txtSubjectDesc.Text
            }))
                {
                    GetSubjectList();
                }
            }
        }
        #endregion

        #region EventListener
        private void drop_image_Preset_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
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
    }
}
