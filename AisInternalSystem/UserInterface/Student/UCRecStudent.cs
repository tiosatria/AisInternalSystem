using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Entities;
using AisInternalSystem.Controller;
using Telerik.Charting;
using Guna.UI2.WinForms;
using Telerik.WinControls;
using Telerik.Pivot.Queryable.Filtering;
using System.Runtime.InteropServices;
using AisInternalSystem.Properties;
using AisInternalSystem.Module;
using System.Reflection;
using Guna.UI2.AnimatorNS;
using System.Net.Configuration;

namespace AisInternalSystem.UserInterface.Student
{
    public partial class UCRecStudent : UserControl
    {
        bool isLoaded = false;
        bool studIsSaved = false;
        public UCRecStudent()
        {

        }
        private void PrepObjectForEditing()
        {

        }
        private void PrepObjectForRecord()
        {

        }
        public void InitObject()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                SwitcherLeft(NavLeftEnum.Student);
                SwitcherRight(NavRightEnum.Document);
                AutoFillInit();
            }
            else
            {

            }
        }

        #region EventListener
        private void FillDgRelationshipList()
        {
            DataTable dt = Query.GetDataTable("FetchConnectedParents", new string[1] { "@_aisid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { SelectedIDStudent.ToString() });
            if (dt.Rows.Count < 1)
            {
                lblnoparents.Visible = true;
                dgParList.Visible = false;
                dgParList.DataSource = dt;
            }
            else
            {
                lblnoparents.Visible = false;
                dgParList.Visible = true;
                dgParList.DataSource = dt;
            }
        }
        private void InsertDataStudent()
        {
            string photo;
            if (studentImg == null)
            {
                photo = null;
            }
            else
            {
                photo = Utilities.GetFileDbLocationString(Utilities.LocationType.StudentPhoto, SelectedIDStudent.ToString(), studentImg);
            }
            if (
            Entities.Student.InsertStudent(new string[34]
                {
                    txtstudAisid.Text,
                    txtNisn.Text,
                    txtAsn.Text,
                    dropIntake.Value.ToString("yyyy-MM-dd"),
                    txtFamilyName.Text,
                    txtGivenName.Text,
                    txtMiddleName.Text,
                    txtCertificateName.Text,
                    dropDob.Value.ToString("yyyy-MM-dd"),
                    txtPlaceOfBirth.Text,
                    txtCountryOfBirth.Text,
                    gender,
                    dropReligion.SelectedItem.ToString(),
                    txtNationality.Text,
                    txtHomeAddress.Text,
                    txtHomeState.Text,
                    txtSuburb.Text,
                    txtPostCode.Text,
                    txtHomeCountry.Text,
                    txtPostalAddress.Text,
                    txtPostalState.Text,
                    txtPostalSuburb.Text,
                    txtPostalCode.Text,
                    txtPostalCountry.Text,
                    txtHomePhone.Text,
                    txtMobileNumber.Text,
                    txtFaxNumber.Text,
                    txtLangSpoken.Text,
                    dropSpokenEnglish.SelectedItem.ToString(),
                    dropStudStat.SelectedItem.ToString(),
                    photo,
                    Utilities.GetCurrentUserID().ToString(),
                    Data.grades[Data.grades.FindIndex(o => o.GradeName == dropCurrGrade.SelectedItem.ToString())].GradeLevel.ToString(),
                    dropPropGrade.SelectedItem.ToString()
                }
                ))
            {
                PopUp.Alert("Student record has been saved succesfully!", frmAlert.AlertType.Success);
                SelectedIDStudent = Convert.ToInt32(txtstudAisid.Text);
                studIsSaved = true;
                studentImg = null;
            }
            else
            {
                PopUp.Alert("Failed to record student data!", frmAlert.AlertType.Error);
            }
        }
        private void ReviseDataStudent()
        {
            string photo;
            if (studentImg == null)
            {
                photo = null;
            }
            else
            {
                photo = Utilities.GetFileDbLocationString(Utilities.LocationType.StudentPhoto, SelectedIDStudent.ToString(), studentImg);
            }
            if (
            Entities.Student.ReviseStudent(new string[34]
                {
                 txtstudAisid.Text,
                    txtNisn.Text,
                    txtAsn.Text,
                    dropIntake.Value.ToString("yyyy-MM-dd"),
                    txtFamilyName.Text,
                    txtGivenName.Text,
                    txtMiddleName.Text,
                    txtCertificateName.Text,
                    dropDob.Value.ToString("yyyy-MM-dd"),
                    txtPlaceOfBirth.Text,
                    txtCountryOfBirth.Text,
                    gender,
                    dropReligion.SelectedItem.ToString(),
                    txtNationality.Text,
                    txtHomeAddress.Text,
                    txtHomeState.Text,
                    txtSuburb.Text,
                    txtPostCode.Text,
                    txtHomeCountry.Text,
                    txtPostalAddress.Text,
                    txtPostalState.Text,
                    txtPostalSuburb.Text,
                    txtPostalCode.Text,
                    txtPostalCountry.Text,
                    txtHomePhone.Text,
                    txtMobileNumber.Text,
                    txtFaxNumber.Text,
                    txtLangSpoken.Text,
                    dropSpokenEnglish.SelectedItem.ToString(),
                    dropStudStat.SelectedItem.ToString(),
                    photo,
                    Utilities.GetCurrentUserID().ToString(),
                    Data.grades[Data.grades.FindIndex(o => o.GradeName == dropCurrGrade.SelectedItem.ToString())].GradeLevel.ToString(),
                    dropPropGrade.SelectedItem.ToString()
                }
                ))
            {
                PopUp.Alert("Student record has been Updated succesfully!", frmAlert.AlertType.Success);
                studentImg = null;
            }
            else
            {
                PopUp.Alert("Failed to update student data!", frmAlert.AlertType.Error);
            }
        }
        string gender = "MALE";
        private void SaveStudent()
        {
            if (!studIsSaved)
            {
                InsertDataStudent();
            }
            else
            {
                ReviseDataStudent();
            }
            
        }
        private void btnStudOk_Click(object sender, EventArgs e)
        {
            if (txtstudAisid.Text == "" || txtCertificateName.Text == "" || txtPlaceOfBirth.Text == "" || txtCountryOfBirth.Text == "" || txtNationality.Text == "" || txtHomeCountry.Text == "" || txtLangSpoken.Text == "")
            {
                PopUp.Alert("Cannot proceed, please complete the data\nbefore saving the record!", frmAlert.AlertType.Warning);
            }
            else
            {
                SaveStudent();    
            }
        }
        private void btnStud1Next_Click(object sender, EventArgs e)
        {
            if (txtstudAisid.Text == "" || txtCertificateName.Text == "" || txtPlaceOfBirth.Text == "" || txtCountryOfBirth.Text == "" || txtNationality.Text == "" || txtHomeCountry.Text == "") 
            {
                PopUp.Alert("Cannot proceed, please complete the data\nbefore go to the next page!", frmAlert.AlertType.Warning);
            }
            else
            {
                UIController.SendPanelBack(panelStud1);

            }
        }

        private void btnStudBack_Click(object sender, EventArgs e)
        {
            UIController.SendPanelBack(panelStud2);
        }

        OpenFileDialog studentImg;
        OpenFileDialog parentsImg;
        OpenFileDialog DocumentPath;
        private void picStud_Click(object sender, EventArgs e)
        {
            studentImg = Utilities.OpenImage(picStud);
        }
        #endregion

        #region Enumeration
        public enum StateEditing
        {
            Record, Update
        }
        private StateEditing _state;
        public enum NavLeftEnum
        {
            Student, Relationship, Sibling
        }
        private NavLeftEnum _navleft;
        public enum NavRightEnum
        {
            Document, School, Medical
        }
        private NavRightEnum _navright;
        #endregion

        #region Properties
        private Entities.Student student1;
        #endregion

        #region Function
        private void AutoFillInit()
        {
            foreach (var item in Data.grades)
            {
                dropPropGrade.Items.Add(item.GradeName);
            }
            dropPropGrade.SelectedIndex = 0;
            foreach (string item in Data.StudentStatus)
            {
                dropStudStat.Items.Add(item);
            }
            dropStudStat.SelectedIndex = 0;
            foreach (string item in Data.Religion)
            {
                dropReligion.Items.Add(item);
            }
            dropReligion.SelectedIndex = 0;
            foreach (string item in Data.EnglishProficiency)
            {
                dropSpokenEnglish.Items.Add(item);
            }
            dropSpokenEnglish.SelectedIndex = 0;
            foreach (string item in Data.DocumentTypeStudent)
            {
                dropDocsType.Items.Add(item);
            }
            dropDocsType.SelectedIndex = 0;
            foreach (var item in Data.grades)
            {
                dropCurrGrade.Items.Add(item.GradeName);
            }
            dropCurrGrade.SelectedIndex = 0;
            txtAutoComplete(txtLangSpoken, Data.LanguageSpoken);
            txtAutoComplete(txtHomeCountry, Data.Country);
            txtAutoComplete(txtFHomeCountry, Data.Country);
            txtAutoComplete(txtCountryOfBirth, Data.Country);
            txtAutoComplete(txtFPostalCountry, Data.Country);
            txtAutoComplete(txtSchoolCountry, Data.Country);
            txtAutoComplete(txtPostalCountry, Data.Country);
            txtAutoComplete(txtPostalCountry, Data.Country);
            txtAutoComplete(txtFHomeState, Data.State);
            txtAutoComplete(txtHomeState, Data.State);
            txtAutoComplete(txtFPostalState, Data.State);
            txtAutoComplete(txtPostalState, Data.State);
            txtAutoComplete(txtFNationality, Data.Nationality);
            txtAutoComplete(txtNationality, Data.Nationality);
            txtAutoComplete(txtPlaceOfBirth, Data.PlaceOfBirth);
            txtAutoComplete(txtFOccupation, Data.Occupation);
        }
        private void txtAutoComplete(Guna2TextBox textBox,  AutoCompleteStringCollection collection)
        {
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox.AutoCompleteCustomSource = collection;
            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        
        private void SwitcherLeft(NavLeftEnum nav)
        {
            _navleft = nav;
            switch (nav)
            {
                case NavLeftEnum.Student:
                    ContainerStudent.BringToFront();
                    ButtonFocusLeft(BtnStud_);
                    break;
                case NavLeftEnum.Relationship:
                    ContainerRelat.BringToFront();
                    ButtonFocusLeft(btnRelat1);
                    break;
                case NavLeftEnum.Sibling:
                    ContainerSibling.BringToFront();
                    ButtonFocusLeft(btnSibling);
                    break;
            }
        }
        private void ButtonFocusLeft(Guna2Button button)
        {
            UIController.NormalizeButton(BtnStud_, Color.White);
            UIController.NormalizeButton(btnRelat1, Color.White);
            UIController.NormalizeButton(btnSibling, Color.White);
            UIController.HighlightButton(button, Color.Black);
        }

        private void ButtonFocusRight(Guna2Button button)
        {
            UIController.NormalizeButton(btnNavDocs, Color.White);
            UIController.NormalizeButton(btnNavMedical, Color.White);
            UIController.NormalizeButton(btnNavSchool, Color.White);
            UIController.HighlightButton(button, Color.Black);
        }

        private void SwitcherRight(NavRightEnum nav)
        {
            _navright = nav;
            switch (nav)
            {
                case NavRightEnum.Document:
                    ContainerDocuments.BringToFront();
                    ButtonFocusRight(btnNavDocs);
                    break;
                case NavRightEnum.School:
                    ContainerSchool.BringToFront();
                    ButtonFocusRight(btnNavSchool);
                    break;
                case NavRightEnum.Medical:
                    ContainerMedical.BringToFront();
                    ButtonFocusRight(btnNavMedical);
                    break;
            }
        }
        #endregion

        private void BtnStud__Click(object sender, EventArgs e)
        {
            SwitcherLeft(NavLeftEnum.Student);
        }

        private void btnNavDocs_Click(object sender, EventArgs e)
        {
            SwitcherRight(NavRightEnum.Document);
        }

        private void btnNavSchool_Click(object sender, EventArgs e)
        {
            SwitcherRight(NavRightEnum.School);
        }

        private void btnNavMedical_Click(object sender, EventArgs e)
        {
            SwitcherRight(NavRightEnum.Medical);
        }
        
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                PopUp.Alert("To upload document, please save the student data!", frmAlert.AlertType.Warning);
            }
            else
            {
                DocumentPath = Utilities.OpenFile("Document Files(*.JPG; *.PNG; *.JPEG; *.PDF; *.DOCX; *.DOC; *.XLSX;) | *.JPG; *.PNG; *.JPEG; *.PDF; *.DOCX; *.DOC; *.XLSX; | All Files (*.*) | *.*");
                txtDocsPath.Text = DocumentPath.FileName;

            }

        }
        private int _progressint = 0;
        private void UploadDocuments()
        {
            btnUpload.Enabled = false;
            dropDocsType.Enabled = false;
            txtDocsDesc.Enabled = false;
            btnUploadRecord.Enabled = false;
            ProgressCopy.Visible = true;
            lblstat.Visible = true;
            lblpleasewait.Visible = true;
            picUploading.Visible = true;
            string dbString = Utilities.GetFileDbLocationString(Utilities.LocationType.StudentDocuments, $"{SelectedIDStudent}_{dropDocsType.SelectedItem}", DocumentPath);
            Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { DocumentPath.FileName, dbString });
            Utilities.workerparam.ProgressChanged += Workerparam_ProgressChanged;
            if (Document.Insert(new string[5] {dbString , Utilities.GetCurrentUserID().ToString(), dropDocsType.SelectedItem.ToString(), txtDocsDesc.Text, SelectedIDStudent.ToString() }))
            {
                PopUp.Alert("Data is valid\nnow we're writing data to the server", frmAlert.AlertType.Info);
            }
            else
            {
                PopUp.Alert("Failed to upload document :(", frmAlert.AlertType.Error);
            }
        }
        private void UploadClear()
        {
            DocumentPath = null;
            txtDocsPath.Text = "";
            dropDocsType.Enabled = true;
            dropDocsType.SelectedIndex += 1;
            txtDocsDesc.Text = "";
            btnUpload.Enabled = true;
            txtDocsDesc.Enabled = true;
            btnUploadRecord.Enabled = true;
            lblstat.Visible = false;
            ProgressCopy.Visible = false;
            ProgressCopy.Value = 0;
            lblpleasewait.Visible = false;
            picUploading.Visible = false;
        }
        private void Workerparam_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblstat.Text = $"{e.ProgressPercentage} %";
            ProgressCopy.Value = e.ProgressPercentage;
            if (ProgressCopy.Value == 100)
            {
                PopUp.Alert("We finished uploading your file\nNow you can upload some document again!", frmAlert.AlertType.Success);
                UploadClear();
                DocumentInit();
            }
        }

        private void DocumentInit()
        {
            DataTable dt = Document.GetDocumentbystudentid(SelectedIDStudent);
            if (dt.Rows.Count < 1)
            {
                lblnodocs.Visible = true;
                dgDocs.Visible = false;
            }
            else
            {
                lblnodocs.Visible = false;
                dgDocs.Visible = true;
                dgDocs.DataSource = dt;
                dgDocs.Columns[0].Visible = false;
                dgDocs.Columns[1].Visible = false;
            }
        }
        private void btnUploadRecord_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                
                PopUp.Alert("We don't know where to upload it\nPlease save student data first and retry again", frmAlert.AlertType.Warning);
            }
            else
            {
                if (DocumentPath == null)
                {
                    PopUp.Alert("Hey, you haven't upload anything, wakey wakey", frmAlert.AlertType.Warning);
                }
                else
                {
                    if (DocumentPath.FileName == "" || DocumentPath.FileName == null)
                    {
                        PopUp.Alert("Hey, you haven't upload anything, wakey wakey", frmAlert.AlertType.Warning);

                    }
                    else
                    {
                        UploadDocuments();
                        btnUploadRecord.Enabled = false;

                    }
                }
            }
        }
        
        private void dgDocs_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedID = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgDocs, "id_docs"));

            }
            catch (Exception)
            {

            }
        }

        private void radFemale_CheckedChanged(object sender, EventArgs e)
        {
            gender = "FEMALE";
        }

        private void RadMale_CheckedChanged(object sender, EventArgs e)
        {
            gender = "MALE";
        }

        private void btnFNext_Click(object sender, EventArgs e)
        {
            if (txtFName.Text == "" || txtFNationality.Text == "" || txtFHomeCountry.Text == "" || txtFOccupation.Text == "")
            {
                PopUp.Alert("You cannot proceed, please fill the required form!", frmAlert.AlertType.Error);
            }
            else
            {
                PanelRelat2.BringToFront();
            }
        }
        private void clearControlParent()
        {
            foreach (var item in PanelRelat1.Controls)
            {
                if (item is Guna2TextBox)
                {
                    Guna2TextBox textbox = item as Guna2TextBox;
                    textbox.Clear();
                }
                if (item is Guna2CirclePictureBox)
                {
                    Guna2CirclePictureBox picture = item as Guna2CirclePictureBox;
                    picture.Image = Resources.icons8_male_user_200px;
                }
                if (item is Guna2ComboBox)
                {
                    Guna2ComboBox combo = item as Guna2ComboBox;
                    combo.SelectedIndex = 0;
                }
            }
        }
        private void btnSearchParents_Click(object sender, EventArgs e)
        {

        }

        private int SelectedIDParents = 0;
        private int SelectedIDStudent = 0;

        private void AssignParenttoStudent()
        {
            if (SelectedIDParents == 0)
            {
                PopUp.Alert("Cannot assign parents, please select the parent and then click assign!", frmAlert.AlertType.Warning);
            }
            else if (SelectedIDStudent == 0)
            {
                PopUp.Alert("No student to assign parent to!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (Query.Insert("AssignParents", new string[2] {"@_aisid", "@_parrelatid" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[2] { SelectedIDStudent.ToString(), SelectedIDParents.ToString() }))
                {
                    PopUp.Alert("Parents assigned succesfully!", frmAlert.AlertType.Success);
                }
                else
                {
                    PopUp.Alert("Failed to assign parent!", frmAlert.AlertType.Error);
                }
            }
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AssignParenttoStudent();
        }

        private void FillDgFindParents()
        {
            if (dropParentstype.SelectedItem.ToString() == "GUARDIAN")
            {
                DataTable dt = Query.GetDataTable("GetGuardianList", new string[1] { "@_names" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { txtSearchPar.Text });
                if (dt.Rows.Count < 1)
                {
                    dgParList.Visible = false;
                    lblfindparents.Visible = true;
                    lblfindparents.Text = $"Can't find parents with name '{txtSearchPar.Text}'\nPlease try again with other name";
                    dgParList.Columns[0].Visible = false;
                }
                else
                {
                    dgParList.Visible = true;
                    dgParList.DataSource = dt;
                    lblfindparents.Visible = false;
                }
            }
            else
            {
                DataTable dt = Query.GetDataTable("GetParentList", new string[2] { "@_relationship", "@_names" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[2] { dropParentstype.SelectedItem.ToString(), txtSearchPar.Text });
                if (dt.Rows.Count < 1)
                {
                    dgParList.Visible = false;
                    lblfindparents.Visible = true;
                    lblfindparents.Text = $"Can't find parents with name '{txtSearchPar.Text}'\nPlease try again with other name";
                }
                else
                {
                    dgParList.Visible = true;
                    dgParList.DataSource = dt;
                    lblfindparents.Visible = false;
                    dgParList.Columns[0].Visible = false;
                }
            }
        }

        private void btnSearchPar_Click(object sender, EventArgs e)
        {
            FillDgFindParents();
        }
        private int SelectedDeassignParent = 0;
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (SelectedDeassignParent == 0)
            {
                PopUp.Alert("Please select one parent to De-Assign from this student!", frmAlert.AlertType.Warning);

            }
            else
            {
                if (Query.Insert("DeAssignParent", new string[2] { "@_aisid", "@_parrelatid" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[2] { SelectedIDStudent.ToString(), SelectedAssignParent.ToString() }))
                {
                    FillDgRelationshipList();
                    PopUp.Alert("Parent assigned succesfully!", frmAlert.AlertType.Success);
                    SelectedDeassignParent = 0;
                }
                else
                {
                    PopUp.Alert("Failed to de-assign parents!", frmAlert.AlertType.Success);

                }
            }
        }
        private int SelectedAssignParent = 0;
        private void dgParList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SelectedAssignParent = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgParList, "id"));

            }
            catch (Exception)
            {

            }
        }

        private void btnAssignParent_Click(object sender, EventArgs e)
        {
            if (SelectedAssignParent == 0)
            {
                PopUp.Alert("Please select one parent to Assign to this student!", frmAlert.AlertType.Warning);
            }
            else if (SelectedIDStudent == 0)
            {
                PopUp.Alert("We don't know where to assign it :(", frmAlert.AlertType.Warning);
            }
            else
            {
                if (Query.Insert("AssignParents", new string[2] { "@_aisid", "@_parrelatid" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[2] { SelectedIDStudent.ToString(), SelectedAssignParent.ToString() }))
                {
                    FillDgRelationshipList();
                    FillDgFindParents();
                    PopUp.Alert("Parent assigned succesfully!", frmAlert.AlertType.Success);
                    SelectedAssignParent = 0;
                }
                else
                {
                    PopUp.Alert("Failed to assign parents!", frmAlert.AlertType.Success);

                }

            }
        }

        private void BtnStud__Click_1(object sender, EventArgs e)
        {
            SwitcherLeft(NavLeftEnum.Student);
        }

        private void btnRelat1_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                PopUp.Alert("You have to save the student data before you can navigate here!", frmAlert.AlertType.Warning);
            }
            else
            {
                SwitcherLeft(NavLeftEnum.Relationship);
            }
        }

        private void btnSibling_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                PopUp.Alert("You have to save the student data before you can navigate here!", frmAlert.AlertType.Warning);
            }
            else
            {
                SwitcherLeft(NavLeftEnum.Sibling);
            }
        }

        private void dgRelationshipList_SelectionChanged(object sender, EventArgs e)
        {
            SelectedDeassignParent = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgRelationshipList, "ID"));
        }

        private void btnCreateNewParent_Click(object sender, EventArgs e)
        {
            ContainerRelationship.BringToFront();
        }
        private string RelationshipTypeString = "";
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropRelatWithChild.SelectedItem.ToString() == "OTHER, PLEASE SPECIFY")
            {
                txtRelatWithChild.Visible = true;
                RelationshipTypeString = txtRelatWithChild.Text;
            }
            else
            {
                txtRelatWithChild.Visible = false;
                RelationshipTypeString = dropRelatWithChild.SelectedItem.ToString();
            }
        }

        private void txtRelatWithChild_TextChanged(object sender, EventArgs e)
        {
            RelationshipTypeString = txtRelatWithChild.Text;
        }
        private void AddParent()
        {
            string image = null;
            if (parentsImg == null)
            {
                image = null;
            }
            else
            {
                image = Utilities.GetFileDbLocationString(Utilities.LocationType.ParentPhoto, txtFName.Text, parentsImg);
            }
            if (Query.Insert("InsertStudentRelationship", new string[27] { "@_relationship", "@_names", "@_nationality", "@_australianresidence", "@_auabortorres", "@_schooleducation", "@_nonschooleducation", "@_occupation", "@_homeaddress", "@_homestate", "@_homecountry", "@_suburb", "@_postcode", "@_postaladdress", "@_postalstate", "@_postalsuburb", "@_postalcode", "@_postalcountry", "@_homephoneno", "@_mobilenumb", "@_faxnumb", "@_emailaddress", "@_whatsapp", "@_mainlang", "@_otherthanenglish", "@_photo", "@_maker"}, new MySql.Data.MySqlClient.MySqlDbType[27] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[27] { RelationshipTypeString, txtFName.Text, txtFNationality.Text, txtFAuRes.Text, txtFAuAborigine.Text, txtFSchoolEdu.Text, txtFNonSchoolEdu.Text, txtFOccupation.Text, txtFHomeAddress.Text, txtFHomeState.Text, txtFHomeCountry.Text, txtFSuburb.Text, txtFPostCode.Text, txtFPostalAdd.Text, txtFPostalState.Text, txtFPostalSuburb.Text, txtFPostalCode.Text, txtFPostalCountry.Text, txtFHomePhone.Text, txtFMobileNumber.Text, txtFFaxNumber.Text, txtFEmailAddress.Text, txtFWhatsapp.Text, txtFMainLanguage.Text, txtFOtherThanEnglishSpoken.Text, image, Utilities.GetCurrentUserID().ToString() }))
            {
                PopUp.Alert("Parent added succesfully!", frmAlert.AlertType.Success);
                clearControlParent();
            }
            else
            {
                PopUp.Alert("Failed to add parent :(", frmAlert.AlertType.Error);
            }
        }
        private void btnSaveFather_Click(object sender, EventArgs e)
        {
            if (txtFName.Text == "" || txtFNationality.Text == "" || txtFHomeCountry.Text == "" || txtFOccupation.Text == "")
            {
                PopUp.Alert("You cannot proceed, please fill the required form!", frmAlert.AlertType.Error);
            }
            else
            {
                AddParent();
            }
        }

        private void btnFBack_Click(object sender, EventArgs e)
        {
            PanelRelat2.SendToBack();
        }

        private void picFather_Click(object sender, EventArgs e)
        {
            parentsImg = Utilities.OpenImage(picFather);
        }

        private void btnOpenDocs_Click(object sender, EventArgs e)
        {
            string path = Utilities.GetSelectedDatagridValue(dgDocs, "docspath");
            if (path == "" || path == null)
            {
                PopUp.Alert("There is nothing to open", frmAlert.AlertType.Warning);
            }
            else
            {
                Utilities.OpenFileDocs(path);

            }
        }
        private void DeleteDocs()
        {
            string idstring = Utilities.GetSelectedDatagridValue(dgDocs, "id_docs");
            if (idstring== "" || idstring == null)
            {
                PopUp.Alert("There is nothing to delete!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (Query.Delete("DeleteSelectedDocument", new string[1] { "@_id_docs" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { idstring }))
                {
                    PopUp.Alert("Document deleted succesfully!", frmAlert.AlertType.Success);
                    DocumentInit();
                }
                else
                {
                    PopUp.Alert("Failed to delete document!", frmAlert.AlertType.Error);
                }
            }
            
        }
        private void btnDeleteDocs_Click(object sender, EventArgs e)
        {
            DeleteDocs();
        }

        private void btnNavDocs_Click_1(object sender, EventArgs e)
        {
            SwitcherRight(NavRightEnum.Document);
        }

        private void btnNavSchool_Click_1(object sender, EventArgs e)
        {
            SwitcherRight(NavRightEnum.School);
        }

        private void btnNavMedical_Click_1(object sender, EventArgs e)
        {
            SwitcherRight(NavRightEnum.Medical);
        }
        private string ExtraSupport = "NO";
        private void AddSchoolInfo()
        {
            if (Query.Insert("InsertSchoolInfo", new string[8] { "@_of_student", "@_name_of_school", "@_country", "@_grade", "@_dateattended", "@_language_of_instruction", "@_extra_support", "@_curriculum" }, new MySql.Data.MySqlClient.MySqlDbType[8] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[8] { SelectedIDStudent.ToString(), txtNameOfSchool.Text, txtSchoolCountry.Text, dropGradeSchool.SelectedItem.ToString(), txtFromTo.Text, txtLangOfInstruction.Text, ExtraSupport, txtCurriculum.Text }))
            {
                PopUp.Alert("School information added!", frmAlert.AlertType.Success);
                InitSchoolInfo();
            }
            else
            {
                PopUp.Alert("Failed to add school information", frmAlert.AlertType.Success);
            }
        }
        private void InitSchoolInfo()
        {
            DataTable dt = Query.GetDataTable("FetchSchoolInfo", new string[1] { "@_of_student" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { SelectedIDStudent.ToString() });
            if (dt.Rows.Count < 1)
            {
                dgSchoolInfo.Visible = false;
                lblnoschoolinfo.Visible = true;
            }
            else
            {
                dgSchoolInfo.Columns[0].Visible = false;
                dgSchoolInfo.DataSource = dt;
                dgSchoolInfo.Visible = true;
                lblnoschoolinfo.Visible = false;
            }
        }
        private void btnAddInfoSchool_Click(object sender, EventArgs e)
        {
            if (SelectedIDStudent == 0)
            {
                PopUp.Alert("Please save the student data before making any change", frmAlert.AlertType.Warning);
            }
            else
            {
                AddSchoolInfo();

            }
        }

        private void chkExtraSupportNo_CheckedChanged(object sender, EventArgs e)
        {
            ExtraSupport = "NO";
            chkExtraSuppYes.CheckState = CheckState.Unchecked;
        }

        private void chkExtraSuppYes_CheckedChanged(object sender, EventArgs e)
        {
            chkExtraSupportNo.CheckState = CheckState.Unchecked;
            ExtraSupport = "YES";
        }

        private void btnPrevSchDel_Click(object sender, EventArgs e)
        {
            string sch = Utilities.GetSelectedDatagridValue(dgSchoolInfo, "id");

            if (sch == "" || sch == null)
            {
                PopUp.Alert("There is nothing to delete!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (Query.Delete("DeleteSchoolInfo", new string[1] { "@_id" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { sch }))
                {
                    PopUp.Alert("School info deleted!", frmAlert.AlertType.Success);
                    InitSchoolInfo();
                }
                else
                {
                    PopUp.Alert("Failed to delete school info", frmAlert.AlertType.Error);
                }
            }
        }
        bool MedicalSaved = false;
        bool bregularmed = false;
        private void InsertMedical()
        {
            string healthcondition = "";
            string allergies = "";
            string medicationforallergies = "";
            string regularmed = "NO";
            string regularmeddetails = "";

            if (txtMedsDetails.Text == "" || txtMedsDetails.Text == null)
            {
                healthcondition = "No serious Health History";
            }
            else
            {
                healthcondition = txtMedsDetails.Text;
            }
            if (txtMEdsAllergies.Text == "" || txtMEdsAllergies.Text == null)
            {
                allergies = "No allergies";
            }
            else
            {
                allergies = txtMEdsAllergies.Text;
            }
            if (txtMedsMedsAller.Text == "" | txtMedsMedsAller.Text == null)
            {
                medicationforallergies = "No meds needed";
            }
            else
            {
                medicationforallergies = txtMedsMedsAller.Text;
            }
            if (bregularmed)
            {
                regularmed = "YES";
                regularmeddetails = txtMedsDose.Text;
                if (txtMedsDose.Text == "" || txtMedsDose.Text == null)
                {
                    regularmeddetails = "No Details";
                }
                else
                {
                    regularmeddetails = txtMedsDose.Text;
                }
            }
            else
            {
                regularmeddetails = "No Details";
            }
            if (Query.Insert("InsertMedical", new string[7] { "@_medicalofstud", "@_healthcondition", "@_allergies", "@_medication_for_allergies", "@_regularmedication", "@_regularmedicationdetails", "@_maker" }, new MySql.Data.MySqlClient.MySqlDbType[7] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[7] { SelectedIDStudent.ToString(), healthcondition, allergies, medicationforallergies, regularmed, regularmeddetails, Utilities.GetCurrentUserID().ToString() }))
            {
                PopUp.Alert("Student Medical info added succesfully!", frmAlert.AlertType.Success);
                MedicalSaved = true;
            }
            else
            {
                PopUp.Alert("Can't update student medical info", frmAlert.AlertType.Error);
            }
        }
        private void InitMedical()
        {
            DataTable dt = Query.GetDataTable("FetchStudentMedicalInfo", new string[1] { "@_medicalofstud" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { SelectedIDStudent.ToString() });
            if (dt.Rows.Count >= 1)
            {
                MedicalSaved = true;
            }
            else
            {
                MedicalSaved = false;
            }
            txtMedsDetails.Text = dt.Rows[0][2].ToString();
            txtMEdsAllergies.Text = dt.Rows[0][3].ToString();
            txtMedsMedsAller.Text = dt.Rows[0][4].ToString();
            if (dt.Rows[0][5].ToString() == "No")
            {
                chkmedsNo.CheckState = CheckState.Checked;
                bregularmed = false;
            }
            else
            {
                chkMedsyes.CheckState = CheckState.Checked;
                bregularmed = true;
            }
            txtMedsDose.Text = dt.Rows[0][6].ToString();
        }
        private void UpdateMedical()
        {
            string healthcondition = "";
            string allergies = "";
            string medicationforallergies = "";
            string regularmed = "NO";
            string regularmeddetails = "";

            if (txtMedsDetails.Text == "" || txtMedsDetails.Text == null)
            {
                healthcondition = "No serious Health History";
            }
            else
            {
                healthcondition = txtMedsDetails.Text;
            }
            if (txtMEdsAllergies.Text == "" || txtMEdsAllergies.Text == null)
            {
                allergies = "No allergies";
            }
            else
            {
                allergies = txtMEdsAllergies.Text;
            }
            if (txtMedsMedsAller.Text == "" | txtMedsMedsAller.Text == null)
            {
                medicationforallergies = "No meds needed";
            }
            else
            {
                medicationforallergies = txtMedsMedsAller.Text;
            }
            if (bregularmed)
            {
                regularmed = "YES";
                regularmeddetails = txtMedsDose.Text;
                if (txtMedsDose.Text == "" || txtMedsDose.Text == null)
                {
                    regularmeddetails = "No Details";
                }
                else
                {
                    regularmeddetails = txtMedsDose.Text;
                }
            }
            else
            {
                regularmeddetails = "No Details";
            }
            if (Query.Insert("UpdateMedical", new string[7] { "@_medicalofstud", "@_healthcondition", "@_allergies", "@_medication_for_allergies", "@_regularmedication", "@_regularmedicationdetails", "@_revised" }, new MySql.Data.MySqlClient.MySqlDbType[7] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[7] { SelectedIDStudent.ToString(), healthcondition, allergies, medicationforallergies, regularmed, regularmeddetails, Utilities.GetCurrentUserID().ToString() }))
            {
                PopUp.Alert("Student Medical info added succesfully!", frmAlert.AlertType.Success);
                MedicalSaved = true;
            }
            else
            {
                PopUp.Alert("Can't update student medical info", frmAlert.AlertType.Error);
            }
        }
        private void SaveMedical()
        {
            if (!studIsSaved)
            {
                PopUp.Alert("Please save student data before making any change!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (MedicalSaved)
                {
                    UpdateMedical();
                }
                else
                {
                    InsertMedical();
                }
            }
        }
        private void btnAddInfo_Click(object sender, EventArgs e)
        {
            SaveMedical();
        }

        private void chkMedsyes_CheckedChanged(object sender, EventArgs e)
        {
            bregularmed = true;
            txtMedsDose.Enabled = true;
            chkmedsNo.CheckState = CheckState.Unchecked;
        }

        private void chkmedsNo_CheckedChanged(object sender, EventArgs e)
        {
            bregularmed = false;
            txtMedsDose.Enabled = false;
            txtMedsDose.Clear();
            chkMedsyes.CheckState = CheckState.Unchecked;
        }
        private void AddSiblingInfo()
        {
            if (Query.Insert("InsertNewSibling", new string[7] { "@_aisid", "@_siblingfname", "@_siblinglname", "@_currentschoolsibling", "@_dob", "@_gender", "@_maker" }, new MySql.Data.MySqlClient.MySqlDbType[7] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar,MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Date, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[7] { SelectedIDStudent.ToString(), txtSiblingFname.Text, txtSiblingLname.Text, txtSiblingSchName.Text, dropdobSibling.Value.ToString("yyyy-MM-dd"), siblingGender, Utilities.GetCurrentUserID().ToString() }))
            {
                PopUp.Alert("Sibling info added succesfully!", frmAlert.AlertType.Success);
                FetchLinkedSiblingInfo();
            }
            else
            {
                PopUp.Alert("Failed to add sibling info!", frmAlert.AlertType.Error);

            }
        }
        private void NormalizeSiblingInfo()
        {
            siblingIsEditing = true;
        }
        private void FetchLinkedSiblingInfo()
        {
            DataTable dt = Query.GetDataTable("FetchSiblingLinked", new string[1] { "@_aisid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { SelectedIDStudent.ToString() });
            if (dt.Rows.Count < 1)
            {
                lblnolinkedsibling.Visible = true;
                dgLinkSibling.Visible = false;
            }
            else
            {
                lblnolinkedsibling.Visible = false;
                dgLinkSibling.Visible = true;
                dgLinkSibling.DataSource = dt;
                dgLinkSibling.Columns[0].Visible = false;
            }
        }
        private void FillSearchSibling()
        {
            DataTable dt = Query.GetDataTable("SearchForSibling", new string[1] { "@_name" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { txtsiblingsearch.Text });
            if (dt.Rows.Count < 1)
            {
                lblsearchnosibling.Visible = true;
                lblsearchnosibling.Text = $"The sibling with name {txtsiblingsearch.Text} was not found, please try different name!";
                dgSearchSibling.Visible = false;
            }
            else
            {
                dgSearchSibling.Visible = true;
                lblsearchnosibling.Visible = false;
                dgSearchSibling.DataSource = dt;
                dgSearchSibling.Columns[0].Visible = false;
            }
        }
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                PopUp.Alert("Please save student data before making any change!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (txtSiblingFname.Text == "")
                {
                    PopUp.Alert("Please enter sibling firstname!", frmAlert.AlertType.Warning);
                }
                else
                {
                    AddSiblingInfo();
                }
            }
        }
        private string siblingGender = "MALE";
        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            siblingGender = "FEMALE";

        }

        private void radMaleSibling_CheckedChanged(object sender, EventArgs e)
        {
            siblingGender = "MALE";
        }

        private void btnSearchSibling_Click(object sender, EventArgs e)
        {
            FillSearchSibling();
        }
        private void LinkStudent()
        {
            if (Query.Insert("AssignSiblingToStudent", new string[2] { "@_aisid", "@_siblingid" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[2] { SelectedIDStudent.ToString(), Utilities.GetSelectedDatagridValue(dgSearchSibling, "id")}))
            {
                PopUp.Alert("Sibling linked succesfully!", frmAlert.AlertType.Success);
            }
            else
            {
                PopUp.Alert("Failed to link sibling!", frmAlert.AlertType.Error);
            }
        }
        private string msgNotsaved = "Please save student data before making any change";
        private void btnlinksibling_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                PopUp.Alert("Please save student data before making any change", frmAlert.AlertType.Warning);
            }
            else
            {
                if (dgSearchSibling.SelectedRows.Count < 1)
                {
                    PopUp.Alert("Please select atleast one sibling to link!", frmAlert.AlertType.Warning);
                }
                else
                {
                    LinkStudent();
                }
            }
        }
        private int CurrentSelectedSiblingID = 0;
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                PopUp.Alert(msgNotsaved, frmAlert.AlertType.Warning);
            }
            else
            {
                if (CurrentSelectedSiblingID != 0)
                {
                    NormalizeSiblingInfo();

                }
                else
                {
                    PopUp.Alert("Please select sibling to edit!", frmAlert.AlertType.Warning);
                }
            }

        }
        private bool siblingIsEditing = false;
        private void dgLinkSibling_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentSelectedSiblingID = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgLinkSibling, "id"));
            }
            catch (Exception)
            {

            }
        }

        private void dgSearchSibling_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentSelectedSiblingID = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgSearchSibling, "id"));
            }
            catch (Exception)
            {

            }
        }
        private void UnlinkSiblingFromStudent()
        { 
        
        }
        private void btndeassign_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                PopUp.Alert(msgNotsaved, frmAlert.AlertType.Warning);
            }
            else
            {
                UnlinkSiblingFromStudent();
            }
        }
    }
}
