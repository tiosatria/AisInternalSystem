using System;
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
using System.Threading;
using AisInternalSystem.Module;
using Data = AisInternalSystem.Controller.Data;

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
                        DoSearch(SearchBy.Name);
                        Utilities.SetDoubleBuffer(FlowClassList, true);
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
            ReviseClass();
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
                    LoadUnassignedClass();
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

        private void ControlIsVisible(bool b)
        {
            btnRevise.Visible = b;
            dropTc.Visible = b;
            txtClassCapacity.Visible = b;
            txtClassName.Visible = b;
        }

        private void dropAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();

            }
            catch (Exception)
            {
                PopUp.Alert("We're a little bit busy, please try again later!", frmAlert.AlertType.Warning);
            }

        }

        private void GetClassInfo(int id)
        {
            DataTable dt = Query.GetDataTable("fetch_class_info", new string[1] { "@classid" }, new MySqlDbType[1] { MySqlDbType.Int32 }, new string[1] { id.ToString() });
            if (dt.Rows.Count >=1)
            {
                txtClassName.Text = dt.Rows[0][1].ToString();
                txtClassCapacity.Text = dt.Rows[0][8].ToString();
                Teacher teacher = new Teacher();
                teacher.TeacherID = Convert.ToInt32(dt.Rows[0][3].ToString());
                teacher.TeacherName = dt.Rows[0][12].ToString();
                Data.AssignTeacherList.Add(teacher);
                if (dt.Rows[0][4].ToString() != null && dt.Rows[0][4].ToString() != "" && dt.Rows[0][4].ToString() != "0")
                {
                    Teacher asstc = new Teacher();
                    asstc.TeacherID = Convert.ToInt32(dt.Rows[0][4].ToString());
                    asstc.TeacherName = dt.Rows[0][17].ToString();
                    Data.AssignAssTcList.Add(asstc);
                    CurrentAssCareTeacher = Convert.ToInt32(dt.Rows[0][4].ToString());
                }
                string grade = dt.Rows[0][2].ToString();
                if (grade.ToLower().Contains("kindergarten") || grade.ToLower().Contains("nursery"))
                {
                    dropAssTc.Visible = true;
                    guna2Button1.Visible = true;
                }
                else
                {
                    guna2Button1.Visible = false;
                    dropAssTc.Visible = false;
                }
                DropTC();
                CurrentCareTeacher = teacher.TeacherID;
                dropTc.SelectedIndex = Data.AssignTeacherList.FindIndex(o => o.TeacherID == teacher.TeacherID);
                dropAssTc.SelectedIndex = Data.AssignAssTcList.FindIndex(o => o.TeacherID == CurrentAssCareTeacher);
            }
        }
        private int CurrentCareTeacher = 0;
        private int CurrentAssCareTeacher = 0;
        private void DropTC()
        {
            dropTc.Items.Clear(); 
            foreach (Teacher teacher in Data.AssignTeacherList)
            {
                dropTc.Items.Add(teacher.TeacherName);
            }
            dropAssTc.Items.Clear();
            foreach (Teacher asstc in Data.AssignAssTcList)
            {
                dropAssTc.Items.Add(asstc.TeacherName);
            }
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
        public void ClassChoosedEvent(UCClassModel classModel)
        {
            if (CurrentCareTeacher != 0)
            {
                Data.AssignTeacherList.RemoveAt(Data.AssignTeacherList.FindIndex(o => o.TeacherID == CurrentCareTeacher));
                CurrentCareTeacher = 0;
            }
            if (CurrentAssCareTeacher != 0)
            {
                Data.AssignAssTcList.RemoveAt(Data.AssignAssTcList.FindIndex(o => o.TeacherID == CurrentAssCareTeacher));
                CurrentAssCareTeacher = 0;
            }
            foreach (UCClassModel classmod in FlowClassList.Controls)
            {
                if (classmod.ClassIdentifier == classModel.ClassIdentifier)
                {
                    foreach (Guna2ShadowPanel panel in classmod.Controls)
                    {
                        panel.FillColor = Color.Coral;
                        LoadClassMember(classModel.ClassIdentifier);
                        GetClassInfo(classModel.ClassIdentifier);
                    }
                }
                else
                {
                    foreach (Guna2ShadowPanel item in classmod.Controls)
                    {
                        item.FillColor = Color.White;
                    }
                }
            }
            CurrentSelectedClassID = classModel.ClassIdentifier;
            BtnRemoveFromClass.Text = "Remove selected student from " + "( " + classModel.ClassName +" )";
            btnAssignTo.Text = "Assign selected student to " + "( " + classModel.ClassName + " )";
        }
        public enum SearchBy
        {
            Name, AISID, Grade, Gender
        }
        private SearchBy _search;
        
        private void DoSearch(SearchBy search)
        {
            _search = search;
            switch (_search)
            {
                case SearchBy.Name:
                    txtSearch.Clear();
                    txtSearch.Visible = true;
                    dropSearch.Visible = false;
                    break;
                case SearchBy.AISID:
                    txtSearch.Clear();
                    txtSearch.Visible = true;
                    dropSearch.Visible = false;
                    break;
                case SearchBy.Grade:
                    dropSearch.Items.Clear();
                    foreach (Grade item in Data.grades)
                    {
                        dropSearch.Items.Add(item.GradeName);
                    }
                    dropSearch.Visible = true;
                    dropSearch.SelectedIndex = 0;
                    txtSearch.Visible = false;
                    break;
                case SearchBy.Gender:
                    dropSearch.Items.Clear();
                    dropSearch.Items.Add("MALE");
                    dropSearch.Items.Add("FEMALE");
                    dropSearch.SelectedIndex = 0;
                    dropSearch.Visible = true;
                    txtSearch.Visible = false;
                    break;
            }
        }

        private void RemoveStudentFromClass()
        {
            if (Query.Delete("DeleteStudentFromClass", new string[3] { "@_aisid", "@_currclass", "@_assignedby" }, new MySqlDbType[3] { MySqlDbType.Int32, MySqlDbType.Int32, MySqlDbType.Int32 }, new string[3] {CurrentSelectedStudent.ToString(), CurrentSelectedClassID.ToString(), Data.user.OwnerID.ToString() }))
            {
                PopUp.Alert("Selected student has been removed from class!", frmAlert.AlertType.Success);
                LoadClassMember(CurrentSelectedClassID);
                LoadUnassignedClass();
            }
            else
            {
                PopUp.Alert("Failed to delete student", frmAlert.AlertType.Warning);
            }
        }

        private void AssignStudentTOClass()
        {
            if (Query.Delete("AssignStudentToClass", new string[3] { "@_aisid", "@_currclass", "@_assignedby" }, new MySqlDbType[3] { MySqlDbType.Int32, MySqlDbType.Int32, MySqlDbType.Int32 }, new string[3] { CurrentSelectedUnassignedStudent.ToString(), CurrentSelectedClassID.ToString(), Data.user.OwnerID.ToString() }))
            {
                PopUp.Alert("Selected student has been removed from class!", frmAlert.AlertType.Success);
                LoadClassMember(CurrentSelectedClassID);
                LoadUnassignedClass();
            }
            else
            {
                PopUp.Alert("Failed to assign student", frmAlert.AlertType.Warning);

            }

        }

        private void ReviseClass()
        {
            SelectedCareTeacher = Data.AssignTeacherList[Data.AssignTeacherList.FindIndex(o => o.TeacherName == dropTc.SelectedItem.ToString())].TeacherID;
            if (dropAssTc.SelectedIndex == -1)
            {
                SelectedAssCareTeacher = null;
            }
            if (SelectedAssCareTeacher == null)
            {
                if (Query.Insert("ReviseClassNoCareTeacher", new string[4] { "@_classname", "_careteacher", "_class_capacity", "_classid" }, new MySqlDbType[4] { MySqlDbType.VarChar, MySqlDbType.Int32, MySqlDbType.Int32, MySqlDbType.Int32 }, new string[4] { txtClassName.Text, SelectedCareTeacher.ToString(), txtClassCapacity.Text, CurrentSelectedClassID.ToString() }))
                {
                    PopUp.Alert("Class has been revised succesfully!", frmAlert.AlertType.Success);
                }
                else
                {
                    PopUp.Alert("Failed to revised class", frmAlert.AlertType.Error);
                }
            }
            else
            {
                if (Query.Insert("ReviseClass", new string[5] { "@_classname", "_careteacher", "_assistant", "_class_capacity", "_classid" }, new MySqlDbType[5] { MySqlDbType.VarChar, MySqlDbType.Int32, MySqlDbType.Int32, MySqlDbType.Int32, MySqlDbType.Int32 }, new string[5] { txtClassName.Text, SelectedCareTeacher.ToString(), SelectedAssCareTeacher.ToString(), txtClassCapacity.Text, CurrentSelectedClassID.ToString() }))
                {
                    PopUp.Alert("Class has been revised succesfully!", frmAlert.AlertType.Success);
                }
                else
                {
                    PopUp.Alert("Failed to revised class", frmAlert.AlertType.Error);
                }
            }

        }

        private int CurrentSelectedClassID = 0;
        private void LoadClassMember(int classcurr)
        {
            DataTable dt = Query.GetDataTable("fetchmemberlist", new string[1] { "@classid" }, new MySqlDbType[1] { MySqlDbType.Int32 }, new string[1] { classcurr.ToString() });
            if (dt.Rows.Count >= 1)
            {
                dgStudList.Visible = true;
                panelEmpty.Visible = false;
                dgStudList.DataSource = dt;
                dgStudList.Columns[0].Width = 30;
                dgStudList.Columns[2].Visible = false;
                dgStudList.Columns[3].Visible = false;
                dgStudList.Columns[4].Visible = false;
                dgStudList.Columns[6].Visible = false;
                dgStudList.Columns[7].Visible = false;
                dgStudList.Columns[5].Visible = false;
            }
            else
            {
                panelEmpty.Visible = true;
                dgStudList.Visible = false;
            }
            lbltotalmember.Text = "Total Member: " + dt.Rows.Count.ToString(); 
        }
        private void LoadUnassignedClass()
        {
            DataTable dt = Query.GetDataTable("GetUnassignedStudent", new string[1] { "@noparam" }, new MySqlDbType[1] { MySqlDbType.VarChar }, new string[1] { "" });
            if (dt.Rows.Count >= 1)
            {
                dgMemberUnassigned.Visible = true;
                PanelUnassignedEmpty.Visible = false;
                dgMemberUnassigned.Visible = true;
                dgMemberUnassigned.DataSource = dt;
                dgMemberUnassigned.Columns[0].Width = 30;
                dgMemberUnassigned.Columns[2].Visible = false;
                dgMemberUnassigned.Columns[3].Visible = false;
                dgMemberUnassigned.Columns[4].Visible = false;
                dgMemberUnassigned.Columns[6].Visible = false;
                dgMemberUnassigned.Columns[7].Visible = false;
                dgMemberUnassigned.Columns[5].Visible = false;
            }
            else
            {
                PanelUnassignedEmpty.Visible = true;
                dgMemberUnassigned.Visible = false;
            }
        }
        private void GetClassList()
        {
            string AY = "";
            if (dropAcademicYear.InvokeRequired)
            {
                dropAcademicYear.Invoke(new MethodInvoker(delegate { AY = dropAcademicYear.SelectedItem.ToString(); }));
            }
            Invoke(new MethodInvoker(delegate () { FlowClassList.Controls.Clear(); }));
            DataTable dt = Query.Load(Query.Process.GetClassListByYear, new string[1] { AY });
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
                    Invoke(new MethodInvoker(delegate () { lbltotalClass.Text = "Loaded Class: " + i.ToString(); }));
                    Invoke(new MethodInvoker(delegate () { FlowClassList.Controls.Add(classList[i]); }));
                }
                Invoke(new MethodInvoker(delegate () { lbltotalClass.Text = "Total Class: " + dt.Rows.Count.ToString(); }));
            }
            else
            {
                Invoke(new MethodInvoker(delegate () { lbltotalClass.Text = "No class available"; }));

            }
        }
        #endregion

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SwitchMenu(CaSwitcher.AssignStudent);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            GetClassList();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            DoSearch(SearchBy.Name);

        }

        private void lblgrade_Click(object sender, EventArgs e)
        {
            DoSearch(SearchBy.Grade);

        }

        private void lblgender_Click(object sender, EventArgs e)
        {
            DoSearch(SearchBy.Gender);

        }

        private void lblaisid_Click(object sender, EventArgs e)
        {
            DoSearch(SearchBy.AISID);

        }

        private void radName_CheckedChanged(object sender, EventArgs e)
        {
            DoSearch(SearchBy.Name);
        }

        private void radGrade_CheckedChanged(object sender, EventArgs e)
        {
            DoSearch(SearchBy.Grade);

        }

        private void r_CheckedChanged(object sender, EventArgs e)
        {
            DoSearch(SearchBy.Gender);

        }

        private void radAisid_CheckedChanged(object sender, EventArgs e)
        {
            DoSearch(SearchBy.AISID);

        }

        private int CurrentSelectedUnassignedStudent = 0;
        private int CurrentSelectedStudent = 0;
        
        private void dgMemberUnassigned_SelectionChanged(object sender, EventArgs e)
        {
            if (dgMemberUnassigned.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgMemberUnassigned.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgMemberUnassigned.Rows[selectedrowindex];
                string a = Convert.ToString(selectedRow.Cells["AIS ID"].Value);
                CurrentSelectedUnassignedStudent = Convert.ToInt32(a);
            }
        }

        private void dgStudList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentSelectedStudent = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgStudList, "AIS ID"));

            }
            catch (Exception)
            {

            }
        }

        private void BtnRemoveFromClass_Click(object sender, EventArgs e)
        {
            RemoveStudentFromClass();
        }

        private void btnAssignTo_Click(object sender, EventArgs e)
        {
            AssignStudentTOClass();
        }

        private void dropTc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCareTeacher = Data.AssignTeacherList[Data.AssignTeacherList.FindIndex(o => o.TeacherName == dropTc.SelectedItem.ToString())].TeacherID;
        }

        private int? SelectedCareTeacher = 0;
        private int? SelectedAssCareTeacher = 0;

        private void dropAssTc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedAssCareTeacher = Data.AssignAssTcList[Data.AssignAssTcList.FindIndex(o => o.TeacherName == dropAssTc.SelectedItem.ToString())].TeacherID;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            PopUp.Alert("Assistant teacher has been revoked", frmAlert.AlertType.Success);
            SelectedAssCareTeacher = null;
            dropAssTc.Items.Clear();
        }

        private void UCClassDirectoryService_Load(object sender, EventArgs e)
        {

        }
    }
}
