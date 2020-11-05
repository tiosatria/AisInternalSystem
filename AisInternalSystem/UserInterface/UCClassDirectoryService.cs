﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using AisInternalSystem.Controller;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AisInternalSystem.Entities;
using Telerik.WinControls.UI;
using Telerik.Pivot.Queryable.Filtering;
using Guna.UI2.WinForms;

namespace AisInternalSystem
{

    public partial class UCClassDirectoryService : UserControl
    {
        bool isLoaded;
        public UCClassDirectoryService()
        {

        }

        public void InitObject(UIController.ControlState state)
        {
            switch (state)
            {
                case UIController.ControlState.Load:
                    if (!isLoaded)
                    {
                        InitializeComponent();
                        GetAcademicYear();
                        SwitchMenu(CaSwitcher.ClassMember);
                    }
                    break;
                case UIController.ControlState.Dispose:

                    break;
                default:
                    break;
            }
            isLoaded = true;
        }

        #region Event

        private void btnClassMember_Click(object sender, EventArgs e)
        {
            SwitchMenu(CaSwitcher.ClassMember);
        }

        private void btnEditClassInfo_Click(object sender, EventArgs e)
        {
            SwitchMenu(CaSwitcher.EditClassinfo);
        }

        private void btnRevise_Click(object sender, EventArgs e)
        {

        }

        enum CaSwitcher
        {
            ClassMember,
            EditClassinfo,
            AssignStudent
        }
        
        private void SwitchMenu(CaSwitcher switcher)
        {
            switch (switcher)
            {
                case CaSwitcher.ClassMember:
                    PanelClassMember.BringToFront();
                    btnClassMember.FillColor = Color.Black;
                    btnClassMember.ForeColor = Color.White;
                    btnEditClassInfo.FillColor = Color.White;
                    btnEditClassInfo.ForeColor = Color.Black;
                    btnClassAssignment.FillColor = Color.White;
                    btnClassAssignment.ForeColor = Color.Black;
                    break;
                case CaSwitcher.EditClassinfo:
                    PanelClassInfo.BringToFront();
                    btnClassMember.FillColor = Color.White;
                    btnClassMember.ForeColor = Color.Black;
                    btnEditClassInfo.FillColor = Color.Black;
                    btnEditClassInfo.ForeColor = Color.White;
                    btnClassAssignment.FillColor = Color.White;
                    btnClassAssignment.ForeColor = Color.Black;
                    break;
                case CaSwitcher.AssignStudent:
                    GetUnassignedMember();
                    PanelAssignStudent.BringToFront();
                    btnClassMember.FillColor = Color.White;
                    btnClassMember.ForeColor = Color.Black;
                    btnEditClassInfo.FillColor = Color.White;
                    btnEditClassInfo.ForeColor = Color.Black;
                    btnClassAssignment.FillColor = Color.Black;
                    btnClassAssignment.ForeColor = Color.White;
                    break;
                default:
                    PanelClassMember.BringToFront();
                    btnClassMember.FillColor = Color.Black;
                    btnClassMember.ForeColor = Color.White;
                    btnEditClassInfo.FillColor = Color.White;
                    btnEditClassInfo.ForeColor = Color.Black;
                    break;
            }
        }

        private bool UnassignedLoad;
        private void GetUnassignedMember()
        {
            if (!UnassignedLoad)
            {
                 dgMemberUnassigned.DataSource = Query.Load(Query.Process.GetUnassignedStudent, new string[1] { "0" });
            }
            UnassignedLoad = true;
        }


        public void GetClassInfo(string id)
        {
            Data.teachersList.Clear();
            DataTable ClassInfo = Query.Load(Query.Process.GetClassInfo, new string[1] { id });
            DataTable dt = Query.Load(Query.Process.GetClassMember, new string[1] { id });
            DataTable teacher = Query.Load(Query.Process.GetAvailableTeacherToAssign, new string[1] { "Teacher" });
            DataTable AssistantTeacher = Query.Load(Query.Process.GetAvailableTeacherToAssign, new string[1] { "Assistant Assistant" });
            if (dt.Rows.Count >= 1)
            {
                dgStudList.Visible = true;
                dgStudList.DataSource = dt;
                dgStudList.Columns[4].Visible = false;
                dgStudList.Columns[5].Visible = false;
                dgStudList.Columns[7].Visible = false;
                lbltotalmember.Text = "Total Member: " + dt.Rows.Count.ToString();
            }
            else
            {
                panelEmpty.Visible = true;
                dgStudList.Visible = false;
            }
            txtClassName.Text = ClassInfo.Rows[0][1].ToString();
            txtClassCapacity.Text = ClassInfo.Rows[0][8].ToString();
            if (teacher.Rows.Count >= 1)
            {
                Module.Teacher[] teachers = new Module.Teacher[teacher.Rows.Count];
                for (int i = 0; i < teacher.Rows.Count; i++)
                {
                    teachers[i] = new Module.Teacher();
                    teachers[i].TeacherName = teacher.Rows[i][0].ToString();
                    teachers[i].TeacherID = Convert.ToInt32(teacher.Rows[i][1].ToString());
                        Data.teachersList.Add(teachers[i]);
                }
            }
            if (AssistantTeacher.Rows.Count >=1)
            {
                Module.Teacher[] AssTc = new Module.Teacher[AssistantTeacher.Rows.Count];
                for (int i = 0; i < AssistantTeacher.Rows.Count; i++)
                {
                    AssTc[i] = new Module.Teacher();
                    AssTc[i].TeacherName = AssistantTeacher.Rows[i][0].ToString();
                    AssTc[i].TeacherID = Convert.ToInt32(AssistantTeacher.Rows[i][1].ToString());
                        Data.assistantTeacherList.Add(AssTc[i]);
                }
            }
            dropAssTc.Items.Clear();
            dropTc.Items.Clear();
            if (ClassInfo.Rows[0][17].ToString() == null)
            {

            }
            else
            {
                dropAssTc.Items.Add(ClassInfo.Rows[0][17].ToString());
                dropAssTc.SelectedIndex = Data.assistantTeacherList.FindIndex(o => o.TeacherName == ClassInfo.Rows[0][17].ToString());
            }
            //for careteacher
            Module.Teacher careteacher = new Module.Teacher();
            careteacher.TeacherName = ClassInfo.Rows[0][12].ToString();
            try
            {
                careteacher.TeacherID = Convert.ToInt32(ClassInfo.Rows[0][3].ToString());
                Data.teachersList.Add(careteacher);

            }
            catch (Exception)
            {
                ControlIsVisible(false);
            }
            string grade = ClassInfo.Rows[0][2].ToString();
            ControlIsVisible(true);
            if (grade == "NURSERY" || grade == "KINDERGARTEN 1" || grade == "KINDERGARTEN 2" || grade == "KINDERGARTEN 3")
            {
                dropAssTc.Visible = true;
            }
            else
            {
                dropAssTc.Visible = false;
            }
            foreach (var item in Data.teachersList)
            {
                dropTc.Items.Add(item.TeacherName);
            }
            foreach (var item in Data.assistantTeacherList)
            {
                dropAssTc.Items.Add(item.TeacherName);
            }
            dropTc.SelectedIndex = Data.teachersList.FindIndex(o => o.TeacherName == careteacher.TeacherName);
       }

        private void ControlIsVisible(bool b)
        {
            btnRevise.Visible = b;
            dropTc.Visible = b;
            txtClassCapacity.Visible = b;
            txtClassName.Visible = b;
        }

        private void dropAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetClassList();
        }
        #endregion

        #region Function
        private void GetAcademicYear()
        {
            dropAcademicYear.Items.Clear(); 
            DataTable dt = Query.Load(Query.Process.GetAcademicYearList, new string[1] { "0" });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dropAcademicYear.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        private void GetClassList()
        {
            FlowClassList.Controls.Clear();
            DataTable dt = Query.Load(Query.Process.GetClassListByYear, new string[1] { dropAcademicYear.SelectedItem.ToString() });
            if (dt.Rows.Count >= 1)
            {
                UCClassModel[] classList = new UCClassModel[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    classList[i] = new UCClassModel();
                    classList[i].CareTeacher = $"Care Teacher: {dt.Rows[i][6].ToString()}";
                    classList[i].ClassName = dt.Rows[i][0].ToString();
                    classList[i].ClassMember = $"Class Member Count:  {dt.Rows[i][9].ToString()}";
                    classList[i].ClassDepartment = $"Department: {dt.Rows[i][8].ToString()}";
                    classList[i].ClassIdentifier = Convert.ToInt32(dt.Rows[i][7].ToString());
                    if (classList[i].ClassName == "NOT ASSIGNED")
                    {
                        classList[i].Visible = false;
                    }
                    FlowClassList.Controls.Add(classList[i]);
                }
            }
        }
        #endregion

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SwitchMenu(CaSwitcher.AssignStudent);
        }
    }
}
