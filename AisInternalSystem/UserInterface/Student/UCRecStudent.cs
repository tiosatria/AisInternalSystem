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
        public static event EventHandler<Image> PictureStudentChanged;
        private bool isLoaded = false;
        private bool studIsSaved = false;
        private bool isBusy = false;    

        public UCRecStudent()
        {
            Confirmation.CancelStudent += Confirmation_CancelStudent;
        }

        private void Confirmation_CancelStudent(object sender, EventArgs e)
        {
            InitClearFormStudent();
            isBusy = false;
            txtSearchPar.Clear(); dgParList.DataSource = null; txtsiblingsearch.Clear(); dgSearchSibling.DataSource = null;
            UIController.NavigateUI(UIController.Controls.StudentDirectoryService);
        }

        private void PrepObjectForEditing()
        {
            InitClearFormStudent();
            txtstudAisid.Enabled = false;
            studIsSaved = true;
            isBusy = true;
            SwitcherLeft(NavLeftEnum.Student);
            SwitcherRight(NavRightEnum.Document);
            LoadStudentData();
        }
        private void PrepObjectForRecord()
        {
            InitClearFormStudent();
            txtstudAisid.Enabled = true;
            isBusy = true;
            studIsSaved = false;
            parentSaved = false;
            SwitcherLeft(NavLeftEnum.Student);
            SwitcherRight(NavRightEnum.Document);
            DeLoadStudentData();
        }
        private void InitClearFormStudent()
        {
            isBusy = false;
            siblingIsEditing = false;
            studentImg = null;
            picStud.Image = Resources.icons8_student_male_80px;
            CurrentSelectedSiblingID = 0;
            SelectedAssignParent = 0;
            SelectedIDParents = 0;
            SelectedDeassignParent = 0;
            foreach (var item in panelStud1.Controls)
            {
                if (item is Guna2TextBox)
                {
                    Guna2TextBox textbox = item as Guna2TextBox;
                    textbox.Clear();
                }
            }
            foreach (var item in panelStud2.Controls)
            {
                if (item is Guna2TextBox)
                {
                    Guna2TextBox textbox = item as Guna2TextBox;
                    textbox.Clear();
                }
                if (item is Guna2ComboBox)
                {
                    Guna2ComboBox combo = item as Guna2ComboBox;
                    if (combo.Items.Count >=1)
                    {
                        combo.SelectedIndex = 0;
                    }
                }
            }
            dropIntake.Value = DateTime.Now;
            dropDob.Value = Convert.ToDateTime("1/1/2018");
            RadMale.Checked = true;
            radFemale.Checked = false;
            gender = "MALE";
        }
        private void InitClearFormParent()
        {
            foreach (var item in PanelRelat1.Controls)
            {
                if (item is Guna2TextBox)
                {
                    Guna2TextBox textbox = item as Guna2TextBox;
                    textbox.Clear();
                }
                if (item is Guna2ComboBox)
                {
                    Guna2ComboBox combo = item as Guna2ComboBox;
                    if (combo.Items.Count >= 1)
                    {
                        combo.SelectedIndex = 0;
                    }
                }
            }
            foreach (var item in PanelRelat2.Controls)
            {
                if (item is Guna2TextBox)
                {
                    Guna2TextBox textbox = item as Guna2TextBox;
                    textbox.Clear();
                }
                if (item is Guna2ComboBox)
                {
                    Guna2ComboBox combo = item as Guna2ComboBox;
                    if (combo.Items.Count >= 1)
                    {
                        combo.SelectedIndex = 0;
                    }
                }
            }
            RelatPhotoLocationStr = null;
            picFather.Image = Resources.icons8_male_user_100;
        }
        private void DeLoadStudentData()
        {
            dgRelationshipList.DataSource = null;
            dgDocs.DataSource = null;
            Utilities.ClearInputOnPanel(PanelMedicalUp);
            dgSchoolInfo.DataSource = null;
            dgLinkSibling.DataSource = null;
        }
        private void LoadStudentData()
        {
            StudentBasicInfo();
            StudentRelationship();
            StudentDocument();
            StudentMedical();
            StudentSchool();
            StudentSibling();
        }
        private void GenderF(string str)
        {
            if (str == "FEMALE")
            {
                radFemale.Checked = true;
                RadMale.Checked = false;
                gender = "FEMALE";
            }
            else
            {
                radFemale.Checked = false;
                RadMale.Checked = true;
                gender = "MALE";
            }
        }
        private void StudentBasicInfo()
        {
            Utilities.ClearInputOnPanel(panelStud1);
            Utilities.ClearInputOnPanel(panelStud2);
            student = Entities.Student.GetStudent(Entities.Student.CurrentStudent.AisID);
            SelectedIDStudent = student.AisID;
            txtstudAisid.Text = student.AisID.ToString();
            txtNisn.Text = student.NIS;
            txtAsn.Text = student.ASN;
            dropIntake.Value = student.Intake;
            txtFamilyName.Text = student.FamilyName;
            txtGivenName.Text = student.GivenName;
            txtMiddleName.Text = student.MiddleName;
            txtCertificateName.Text = student.CertificateName;
            dropDob.Value = student.DateofBirth;
            txtPlaceOfBirth.Text = student.PlaceOfBirth;
            txtCountryOfBirth.Text = student.CountryOfBirth;
            GenderF(student.Gender);
            dropReligion.SelectedIndex = dropReligion.Items.IndexOf(student.Religion);
            txtNationality.Text = student.Nationality;
            txtHomeAddress.Text = student.HomeAddress;
            txtHomeState.Text = student.HomeState;
            txtSuburb.Text = student.Suburb;
            txtPostCode.Text = student.PostCode;
            txtHomeCountry.Text = student.HomeCountry;
            txtPostalAddress.Text = student.PostalAddress;
            txtPostalState.Text = student.PostalState;
            txtPostalSuburb.Text = student.PostalSuburb;
            txtPostalCode.Text = student.PostalCode;
            txtPostalCountry.Text = student.PostalCountry;
            txtHomePhone.Text = student.HomePhone;
            txtMobileNumber.Text = student.HomePhone;
            txtFFaxNumber.Text = student.FaxNumber;
            txtLangSpoken.Text = student.LangSpoken;
            dropSpokenEnglish.SelectedIndex = dropSpokenEnglish.Items.IndexOf(student.EnglishProficiency);
            dropStudStat.SelectedIndex = dropStudStat.Items.IndexOf(student.StudentStatus);
            photo = student.PhotoLocation;
            try
            {
                using (Image img = Image.FromFile(photo))
                {
                    Bitmap b = new Bitmap(img);
                    picStud.Image = b;
                    img.Dispose();
                }
            }
            catch (Exception)
            {
                picStud.Image = Resources.icons8_student_male_80px;
            }
            dropCurrGrade.SelectedIndex = dropCurrGrade.Items.IndexOf(student.CurrentGradeString);
            dropPropGrade.SelectedIndex = dropPropGrade.Items.IndexOf(student.ProposedGrade);
        }
        private void StudentRelationship()
        {
            FillDgRelationshipList();
        }
        private void StudentDocument()
        {
            DocumentInit();
        }
        private void StudentMedical()
        {
            Utilities.ClearInputOnPanel(PanelMedicalUp);
            int j = 0;
            int i = 0; 
            DataTable dt = Query.GetDataTable("stud_medical_info", new string[1] { "@_medicalofstud" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { SelectedIDStudent.ToString() });
            if (dt.Rows.Count >= 1)
            {
                txtMedsDetails.Text = dt.Rows[i][j].ToString();
                txtMEdsAllergies.Text = dt.Rows[i][j++].ToString();
                txtMedsMedsAller.Text = dt.Rows[i][j++].ToString();
                onRegularmeds(dt.Rows[i][j++].ToString());
                txtMedsDose.Text = dt.Rows[i][j++].ToString();
            }
            else
            {
                Utilities.ClearInputOnPanel(PanelMedicalUp);
            }
        }
        private void onRegularmeds(string stat)
        {
            if (stat == "YES")
            {
                chkMedsyes.Checked = true;
                chkmedsNo.Checked = false;
                bregularmed = true;
            }
            else
            {
                chkMedsyes.Checked = false;
                chkmedsNo.Checked = true;
                bregularmed = false;
            }
        }
        private void StudentSchool()
        {
            InitSchoolInfo();
        }
        private void StudentSibling()
        {
            FetchLinkedSiblingInfo();
        }
        public void InitObject(EditMode edit)
        {
            _editMode = edit;
            if (!isLoaded)
            {
                InitializeComponent();
                PrepForm();
                AutoFillInit();
            }
            else
            {
                PrepForm();
            }
            isLoaded = true;
        }
        private void initDrop()
        {
            //dropgradesch
            if (!dropGradeSchLoaded)
            {
                dropGradeSchool.Items.Clear();
                foreach (Grade item in Grade.GradeList())
                {
                    dropGradeSchool.Items.Add(item.GradeName);
                }
                dropGradeSchLoaded = true;
            }
            else
            {

            }

        }
        private void PrepForm()
        {
            switch (_editMode)
            {
                case EditMode.Create:
                    if (!isBusy)
                    {
                        PrepObjectForRecord();
                        initDrop();
                    }
                    else
                    {
                        PopUp.Alert("You have ongoing recording session, please mark the session as finished to record or edit new data!", frmAlert.AlertType.Warning);
                    }
                    break;
                case EditMode.Edit:
                    if (!isBusy)
                    {
                        PrepObjectForEditing();
                        initDrop();
                    }
                    else
                    {
                        PopUp.Alert("You have ongoing recording session, please mark the session as finished to record or edit new data!", frmAlert.AlertType.Warning);

                    }
                    break;
            }
        }
        public enum EditMode
        {
            Create, Edit
        }
        private EditMode _editMode;
        #region EventListener
        private void FillDgRelationshipList()
        {
            DataTable dt = Query.GetDataTable("FetchConnectedParents", new string[1] { "@_aisid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { SelectedIDStudent.ToString() });
            if (dt.Rows.Count < 1)
            {
                lblnoparents.Visible = true;
                dgRelationshipList.Visible = false;
                dgRelationshipList.DataSource = dt;
            }
            else
            {
                lblnoparents.Visible = false;
                dgRelationshipList.Visible = true;
                dgRelationshipList.DataSource = dt;
            }
        }
        private Entities.Student student = null;

        private string photo = null;


        private void InsertDataStudent()
        {
            Utilities.WorkerFinished += Utilities_WorkerFinished;
            student = new Entities.Student();
            student.AisID = Convert.ToInt32(txtstudAisid.Text);
            student.NIS = txtNisn.Text;
            student.ASN = txtAsn.Text;
            student.Intake = Convert.ToDateTime(dropIntake.Value.ToString(PublicProperties.DateFormat));
            student.FamilyName = txtFamilyName.Text;
            student.GivenName = txtGivenName.Text;
            student.MiddleName = txtMiddleName.Text;
            student.CertificateName = txtCertificateName.Text;
            student.DateofBirth = Convert.ToDateTime(dropDob.Value.ToString("yyyy-MM-dd"));
            student.PlaceOfBirth = txtPlaceOfBirth.Text;
            student.CountryOfBirth = txtCountryOfBirth.Text;
            student.Gender = gender;
            student.Religion = dropReligion.SelectedItem.ToString();
            student.Nationality = txtNationality.Text;
            student.HomeAddress = txtHomeAddress.Text;
            student.HomeState = txtHomeState.Text;
            student.Suburb = txtSuburb.Text;
            student.PostCode = txtPostCode.Text;
            student.HomeCountry = txtHomeCountry.Text;
            student.PostalAddress = txtPostalAddress.Text;
            student.PostalState = txtPostalState.Text;
            student.PostalSuburb = txtPostalSuburb.Text;
            student.PostalCode = txtPostalCode.Text;
            student.PostalCountry = txtPostalCountry.Text;
            student.HomePhone = txtHomePhone.Text;
            student.MobileNumber = txtMobileNumber.Text;
            student.FaxNumber = txtFaxNumber.Text;
            student.LangSpoken = txtLangSpoken.Text;
            student.EnglishProficiency = dropSpokenEnglish.SelectedItem.ToString();
            student.StudentStatus = dropStudStat.SelectedItem.ToString();
            student.PhotoLocation = photo;
            student.Maker = Utilities.GetCurrentUserID();
            student.CurrentGrade = Data.grades[Data.grades.FindIndex(o => o.GradeName == dropCurrGrade.SelectedItem.ToString())].GradeLevel;
            student.ProposedGrade = dropPropGrade.SelectedItem.ToString();
            if (Entities.Student.InsertStudent(student))
            {
                PopUp.Alert("Student record has been saved succesfully!", frmAlert.AlertType.Success);
                if (studentImg != null && studentImg.FileName != "" && studentImg.FileName != null)
                {
                    picStud.Image.Dispose();
                    btnFinalize.Enabled = false;
                    Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { studentImg.FileName, photo });
                }
                SelectedIDStudent = Convert.ToInt32(txtstudAisid.Text);
                studIsSaved = true;
                studentImg = null;
                student = null;
            }
            else
            {
                PopUp.Alert("Failed to record student data!", frmAlert.AlertType.Error);
            }
        }

        private void Utilities_WorkerFinished(object sender, EventArgs e)
        {
            try
            {
                using (Image img = Image.FromFile(photo))
                {
                    Bitmap bitmap = new Bitmap(img);
                    picStud.Image = bitmap;
                    img.Dispose();
                    PictureStudentChanged?.Invoke(sender, bitmap);
                    btnFinalize.Enabled = true;
                }
            }
            catch (Exception)
            {
                picStud.Image = Resources.icons8_student_male_80px;
                btnFinalize.Enabled = true;
            }
        }

        private void ReviseDataStudent()
        {
            Utilities.WorkerFinished += Utilities_WorkerFinished1;
            student = new Entities.Student();
            student.AisID = Convert.ToInt32(txtstudAisid.Text);
            student.NIS = txtNisn.Text;
            student.ASN = txtAsn.Text;
            student.Intake = Convert.ToDateTime(dropIntake.Value.ToString(PublicProperties.DateFormat));
            student.FamilyName = txtFamilyName.Text;
            student.GivenName = txtGivenName.Text;
            student.MiddleName = txtMiddleName.Text;
            student.CertificateName = txtCertificateName.Text;
            student.DateofBirth = Convert.ToDateTime(dropDob.Value.ToString("yyyy-MM-dd"));
            student.PlaceOfBirth = txtPlaceOfBirth.Text;
            student.CountryOfBirth = txtCountryOfBirth.Text;
            student.Gender = gender;
            student.Religion = dropReligion.SelectedItem.ToString();
            student.Nationality = txtNationality.Text;
            student.HomeAddress = txtHomeAddress.Text;
            student.HomeState = txtHomeState.Text;
            student.Suburb = txtSuburb.Text;
            student.PostCode = txtPostCode.Text;
            student.HomeCountry = txtHomeCountry.Text;
            student.PostalAddress = txtPostalAddress.Text;
            student.PostalState = txtPostalState.Text;
            student.PostalSuburb = txtPostalSuburb.Text;
            student.PostalCode = txtPostalCode.Text;
            student.PostalCountry = txtPostalCountry.Text;
            student.HomePhone = txtHomePhone.Text;
            student.MobileNumber = txtMobileNumber.Text;
            student.FaxNumber = txtFaxNumber.Text;
            student.LangSpoken = txtLangSpoken.Text;
            student.EnglishProficiency = dropSpokenEnglish.SelectedItem.ToString();
            student.StudentStatus = dropStudStat.SelectedItem.ToString();
            student.PhotoLocation = photo;
            student.Revised = Utilities.GetCurrentUserID();
            student.CurrentGrade = Data.grades[Data.grades.FindIndex(o => o.GradeName == dropCurrGrade.SelectedItem.ToString())].GradeLevel;
            student.ProposedGrade = dropPropGrade.SelectedItem.ToString();
            if (Entities.Student.ReviseStudent(student))
            {
                PopUp.Alert("Student record has been saved succesfully!", frmAlert.AlertType.Success);
                if (studentImg != null && studentImg.FileName != "" && studentImg.FileName != null)
                {
                    btnFinalize.Enabled = false;
                    Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { studentImg.FileName, photo });
                }
                SelectedIDStudent = Convert.ToInt32(txtstudAisid.Text);
                studIsSaved = true;
                student = null;
            }
            else
            {
                PopUp.Alert("Failed to record student data!", frmAlert.AlertType.Error);
            }
        }

        private void Utilities_WorkerFinished1(object sender, EventArgs e)
        {
            try
            {
                using (Image img = Image.FromFile(photo))
                {
                    Bitmap bitmap = new Bitmap(img);
                    picStud.Image = bitmap;
                    img.Dispose();
                    PictureStudentChanged?.Invoke(sender, bitmap);
                    btnFinalize.Enabled = true;
                }
            }
            catch (Exception)
            {
                picStud.Image = Resources.icons8_student_male_80px;
                btnFinalize.Enabled = true;
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

        }
        private void btnStudBack_Click(object sender, EventArgs e)
        {
            UIController.SendPanelBack(panelStud2);
        }
        OpenFileDialog studentImg;
        OpenFileDialog parentsImg;
        OpenFileDialog DocumentPath;
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
        private bool autofillLoaded = false;
        #region Function
        private void AutoFillInit()
        {
            if (!autofillLoaded)
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
                //father
                txtAutoComplete(txtFNationality, Data.Nationality);
                txtAutoComplete(txtFAuRes, Data.AustralianResidence);
                txtAutoComplete(txtFAuAborigine, Data.AustralianAborigine);
                txtAutoComplete(txtFNonSchoolEdu, Data.NonSchoolEducation);
                txtAutoComplete(txtFOccupation, Data.Occupation);
                txtAutoComplete(txtFHomeAddress, Data.Country);
            }
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
            UIController.HighlightButton(new List<Guna2Button> { BtnStud_, btnRelat1, btnSibling }, button);
        }

        private void ButtonFocusRight(Guna2Button button)
        {
            UIController.HighlightButton(new List<Guna2Button> { btnNavDocs, btnNavMedical, btnNavSchool }, button);
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
            string dbString = Utilities.GetFileDbLocationString(Utilities.LocationType.StudentDocuments, $"{SelectedIDStudent}_{dropDocsType.SelectedItem}_{txtDocsDesc.Text}", DocumentPath);
            Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { DocumentPath.FileName, dbString });
            Utilities.workerparam.ProgressChanged += Workerparam_ProgressChanged;
            if (Document.Insert(Document.DocumentFor.Student, new string[5] {dbString , Utilities.GetCurrentUserID().ToString(), dropDocsType.SelectedItem.ToString(), txtDocsDesc.Text, SelectedIDStudent.ToString() }))
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
            try
            {
                dropDocsType.SelectedIndex += 1;

            }
            catch (Exception)
            {
                
            }
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
            DataTable dt = Document.GetDocumentbyID(SelectedIDStudent, "student");
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
                if (Query.Delete("DeAssignParent", new string[2] { "@_aisid", "@_parrelatid" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[2] { SelectedIDStudent.ToString(), SelectedDeassignParent.ToString() }))
                {
                    FillDgRelationshipList();
                    PopUp.Alert("Parent unlinked succesfully!", frmAlert.AlertType.Success);
                    SelectedDeassignParent = 0;
                }
                else
                {
                    PopUp.Alert("Failed to de-assign parents!", frmAlert.AlertType.Error);

                }
            }
        }
        private int SelectedAssignParent = 0;
        private void dgParList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SelectedAssignParent = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgParList, "id"));
                relationship = new Relationship();
                relationship.RelatinshipID = SelectedAssignParent;
                relatID = SelectedAssignParent;
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
            try
            {
                SelectedDeassignParent = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgRelationshipList, "ID"));
                relationship = new Relationship();
                relationship.RelatinshipID = SelectedDeassignParent;
                relatID = SelectedDeassignParent;
            }
            catch (Exception)
            {

            }
        }
        public enum RelationshipEditMode
        {
            Create,
            Edit

        }
        private RelationshipEditMode _relatmode;
        private void LoadRelationshipData()
        {
            relationship = Relationship.GetRelationship(relationship);
            txtFName.Text = relationship.RelationshipName;
            txtFNationality.Text = relationship.Nationality;
            txtFAuRes.Text = relationship.AustralianResidence;
            txtFAuAborigine.Text = relationship.AustralianAborigin;
            txtFSchoolEdu.Text = relationship.SchoolEducation;
            txtFNonSchoolEdu.Text = relationship.NonSchoolEducation;
            txtFOccupation.Text = relationship.Occupation;
            txtFHomeAddress.Text = relationship.Homeaddress;
            txtFHomeState.Text = relationship.Homestate;
            txtFHomeCountry.Text = relationship.HomeCountry;
            txtFSuburb.Text = relationship.Suburb;
            txtFPostCode.Text = relationship.PostCode;
            txtFPostalAdd.Text = relationship.PostalAddress;
            txtFPostalState.Text = relationship.PostalState;
            txtFPostalSuburb.Text = relationship.PostalSuburb;
            txtFPostalCode.Text = relationship.PostalCode;
            txtFPostalCountry.Text = relationship.PostalCountry;
            txtFHomePhone.Text = relationship.HomephoneNo;
            txtFMobileNumber.Text = relationship.MobileNumb;
            txtFFaxNumber.Text = relationship.FaxNumber;
            txtFEmailAddress.Text = relationship.EmailAddress;
            txtFWhatsapp.Text = relationship.Whatsapp;
            txtFMainLanguage.Text = relationship.MainLang;
            txtFOtherThanEnglishSpoken.Text = relationship.OtherThanEnglish;
            dropRelatWithChild.SelectedIndex = dropRelatWithChild.Items.IndexOf(relationship.RelationshipType);
            if (!dropRelatWithChild.Items.Contains(relationship.RelationshipType))
            {
                dropRelatWithChild.SelectedIndex = dropRelatWithChild.Items.IndexOf("OTHER, PLEASE SPECIFY");
                txtRelatWithChild.Text = relationship.RelationshipType;
                txtRelatWithChild.Visible = true;
                txtRelatWithChild.Enabled = true;
                RelationshipType = relationship.RelationshipType;
            }
            try
            {
                using (Image imag = Image.FromFile(relationship.Photolocation))
                {
                    Bitmap bitmap = new Bitmap(imag);
                    picFather.Image = bitmap;
                    imag.Dispose();
                }
            }
            catch (Exception)
            {
                picFather.Image = Resources.icons8_male_user_100; 
            }
            relatID = relationship.RelatinshipID;
            this.RelationshipType = relationship.RelationshipType;
            parentLoaded = true;
        }
        private void GoRelationship(RelationshipEditMode mode)
        {
            _relatmode = mode;
            switch (mode)
            {
                case RelationshipEditMode.Create:
                    RelatPhotoLocationStr = null;
                    parentSaved = false;
                    relationship = null;
                    relatPhoto = null;
                    picFather.Image = Properties.Resources.icons8_male_user_200px;
                    btnSaveRelationship.Text = "Add Parent";
                    clearControlParent();
                    txtstudAisid.Enabled = true;
                    break;
                case RelationshipEditMode.Edit:
                    RelatPhotoLocationStr = null;
                    relatPhoto = null;
                    clearControlParent();
                    parentSaved = true;
                    LoadRelationshipData();
                    btnSaveRelationship.Text = "Revise Parent";
                    txtstudAisid.Enabled = false;
                    break;
            }
            ContainerRelationship.BringToFront();
        }
        private void btnCreateNewParent_Click(object sender, EventArgs e)
        {
            GoRelationship(RelationshipEditMode.Create);
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
                PopUp.Alert("We're finding the document on the server...", frmAlert.AlertType.Info);
                Utilities.WorkerFire(Utilities.WorkerProcess.OpenFileNewThread, new string[1] { path });
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
                if (Module.Document.Delete( Document.DocumentFor.Student, idstring)) 
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
            if (dropGradeSchool.SelectedItem != null && dropprevschooldatefrom.SelectedItem != null && dropprevschooldateto.SelectedItem != null)
            {
                if (Query.Insert("InsertSchoolInfo", new string[8] { "@_of_student", "@_name_of_school", "@_country", "@_grade", "@_dateattended", "@_language_of_instruction", "@_extra_support", "@_curriculum" }, new MySql.Data.MySqlClient.MySqlDbType[8] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[8] { SelectedIDStudent.ToString(), txtNameOfSchool.Text, txtSchoolCountry.Text, dropGradeSchool.SelectedItem.ToString(), dropprevschooldatefrom.SelectedItem.ToString() + "/" + dropprevschooldateto.SelectedItem.ToString(), txtLangOfInstruction.Text, ExtraSupport, txtCurriculum.Text }))
                {
                    PopUp.Alert("School information added!", frmAlert.AlertType.Success);
                    InitSchoolInfo();
                    Utilities.ClearInputOnPanel(PanelPrevSchoolinfoUp);
                }
                else
                {
                    PopUp.Alert("Failed to add school information", frmAlert.AlertType.Success);
                }
            }
            else
            {
                PopUp.Alert("Failed to record school information, make sure your data is valid!", frmAlert.AlertType.Warning);
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
                dgSchoolInfo.DataSource = dt;
                dgSchoolInfo.Columns[0].Visible = false;
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
            if (siblingIsEditing)
            {
                if (Query.Insert("ReviseSibling", new string[7] { "@_id", "@_siblingfname", "@_siblinglname", "@_currentschoolsibling", "@_dob", "@_gender", "@_maker" }, 
                    new MySql.Data.MySqlClient.MySqlDbType[7] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Date, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32 }, 
                    new string[7] { CurrentSelectedSiblingID.ToString(), txtSiblingFname.Text, txtSiblingLname.Text, txtSiblingSchName.Text, dropdobSibling.Value.ToString(PublicProperties.DateFormat), siblingGender, Data.user.UserID.ToString()  }))
                {
                    PopUp.Alert("Sibling edited succesfully!", frmAlert.AlertType.Success);
                    siblingIsEditing = false;
                    btnAddSibling.Text = "Add";
                    siblingGender = "MALE";
                    radMaleSibling.Checked = true;
                    radFemaleSibling.Checked = false;
                    txtSiblingFname.Clear(); txtSiblingLname.Clear(); txtSiblingSchName.Clear(); dropdobSibling.Value = DateTime.Now;
                }
                else
                {
                    PopUp.Alert("Failed to edit sibling, please try again!", frmAlert.AlertType.Error);
                }
            }
            else
            {
                if (Query.Insert("InsertNewSibling", new string[7] { "@_aisid", "@_siblingfname", "@_siblinglname", "@_currentschoolsibling", "@_dob", "@_gender", "@_maker" }, new MySql.Data.MySqlClient.MySqlDbType[7] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Date, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[7] { SelectedIDStudent.ToString(), txtSiblingFname.Text, txtSiblingLname.Text, txtSiblingSchName.Text, dropdobSibling.Value.ToString("yyyy-MM-dd"), siblingGender, Utilities.GetCurrentUserID().ToString() }))
                {
                    PopUp.Alert("Sibling info added succesfully!", frmAlert.AlertType.Success);
                    FetchLinkedSiblingInfo();
                }
                else
                {
                    PopUp.Alert("Failed to add sibling info!", frmAlert.AlertType.Error);

                }
            }
        }
        private void FetchSiblingGender(string gender)
        {
            siblingGender = gender;

            if (gender == "MALE")
            {
                radMaleSibling.Checked = true;
                radFemaleSibling.Checked = false;
            }
            else
            {
                radFemaleSibling.Checked = true;
                radMaleSibling.Checked = true;
            }
        }
        private void FetchSiblingInfo()
        {
            btnAddSibling.Text = "Revise Sibling";
            DataTable dt = Query.GetDataTable("FetchSiblingInfo", new string[1] { "@_siblingid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { CurrentSelectedSiblingID.ToString() });
            if (dt.Rows.Count >=1)
            {
                txtSiblingFname.Text = dt.Rows[0][1].ToString();
                txtSiblingLname.Text = dt.Rows[0][2].ToString();
                txtSiblingSchName.Text = dt.Rows[0][3].ToString();
                FetchSiblingGender(dt.Rows[0][5].ToString());
                dropdobSibling.Value = Convert.ToDateTime(dt.Rows[0][4].ToString());
            }
        }
        private void NormalizeSiblingInfo()
        {
            siblingIsEditing = true;
            FetchSiblingInfo();
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
                FetchLinkedSiblingInfo();
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
                    if (siblingIsEditing)
                    {
                        PopUp.Alert("An ongoing sibling revise detected, please apply current record to edit new one!", frmAlert.AlertType.Warning);
                    }
                    else
                    {
                        NormalizeSiblingInfo();
                    }
                }
                else
                {
                    PopUp.Alert("Please select sibling to edit!", frmAlert.AlertType.Warning);
                }
            }

        }
        private bool siblingIsEditing = false;

        private void dgSearchSibling_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentSelectedSiblingID = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgSearchSibling, "id"));
                btndeassign.Enabled = false;
            }
            catch (Exception)
            {

            }
        }
        private void UnlinkSiblingFromStudent()
        {
            if (Query.Delete("DelinkSibling", new string[2] { "@_aisid", "@_siblingid" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[2] { SelectedIDStudent.ToString(), CurrentSelectedSiblingID.ToString() }))
            {
                PopUp.Alert("Selected sibling has been unlinked", frmAlert.AlertType.Success);
                FetchLinkedSiblingInfo();
                if (dgLinkSibling.Rows.Count<1)
                {
                    btndeassign.Enabled = false;
                }
                else
                {
                    btndeassign.Enabled = true;
                }
            }
            else
            {
                PopUp.Alert("Something error", frmAlert.AlertType.Error);
            }
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

        private void txtMedsDose_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnStudBack_Click_1(object sender, EventArgs e)
        {
            panelStud2.SendToBack();
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (!studIsSaved)
            {
                PopUp.Alert("Failed to finalize data, please save the student data to finalize the record!", frmAlert.AlertType.Warning);
            }
            else
            {
                FinalizeRecord();
            }
        }
        private void FinalizeRecord()
        {
            SaveStudent();
            isBusy = false;
            studIsSaved = false;
            txtSearchPar.Clear();dgParList.DataSource = null; txtsiblingsearch.Clear();dgSearchSibling.DataSource = null;
            //go back to previous screen
            UIController.NavigateUI(UIController.Controls.StudentDirectoryService); 
        }

        private void dropprevschooldatefrom_SelectedIndexChanged(object sender, EventArgs e)
        {

            dropprevschooldateto.DataSource = PublicProperties.DropYearPickFirst(Convert.ToInt32(dropprevschooldatefrom.SelectedValue.ToString()) + 1);
        }

        private void dropprevschooldatefrom_Click(object sender, EventArgs e)
        {
            int dateprevious = Convert.ToInt32(DateTime.Now.Year) - 12;
            dropprevschooldatefrom.DataSource = PublicProperties.DropYearPickFirst(dateprevious);
        }
        private bool dropGradeSchLoaded = false;
        private void dropGradeSchool_Click(object sender, EventArgs e)
        {
            
        }

        private void dropGradeSchool_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgLinkSibling_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentSelectedSiblingID = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgLinkSibling, "id"));
                btndeassign.Enabled = true;
            }
            catch (Exception)
            {

            }
        }

        private void btnFNext_Click_1(object sender, EventArgs e)
        {
            PanelRelat1.SendToBack();
        }

        private void btnFBack_Click_1(object sender, EventArgs e)
        {
            PanelRelat2.SendToBack();
        }
        private bool parentSaved = false;
        private Entities.Relationship relationship = null;
        private string RelationshipType = string.Empty;
        private OpenFileDialog RelatPhotoLocationStr = null;
        private int relatID = 0;
        private void SaveParent()
        {
            //data here
            if (relationship != null)
            {
                relationship.RelationshipType = this.RelationshipType;
                if (relationship.RelationshipType == string.Empty)
                {
                    relationship.RelationshipType = dropRelatWithChild.Text;
                }
                relationship.RelationshipName = txtFName.Text;
                relationship.Nationality = txtFNationality.Text;
                relationship.AustralianResidence = txtFAuRes.Text;
                relationship.AustralianAborigin = txtFAuAborigine.Text;
                relationship.SchoolEducation = txtFSchoolEdu.Text;
                relationship.NonSchoolEducation = txtFNonSchoolEdu.Text;
                relationship.Occupation = txtFOccupation.Text;
                relationship.Homeaddress = txtFHomeAddress.Text;
                relationship.Homestate = txtHomeState.Text;
                relationship.HomeCountry = txtFHomeCountry.Text;
                relationship.Suburb = txtFSuburb.Text;
                relationship.PostCode = txtFPostCode.Text;
                relationship.PostalAddress = txtFPostalAdd.Text;
                relationship.PostalState = txtFPostalState.Text;
                relationship.PostalSuburb = txtFPostalSuburb.Text;
                relationship.PostalCode = txtFPostalCode.Text;
                relationship.PostalCountry = txtFPostalCountry.Text;
                relationship.HomephoneNo = txtFHomePhone.Text;
                relationship.MobileNumb = txtFMobileNumber.Text;
                relationship.FaxNumber = txtFFaxNumber.Text;
                relationship.EmailAddress = txtFEmailAddress.Text;
                relationship.Whatsapp = txtFWhatsapp.Text;
                relationship.MainLang = txtFMainLanguage.Text;
                relationship.OtherThanEnglish = txtFOtherThanEnglishSpoken.Text;
                if (RelatPhotoLocationStr == null)
                {
                    relatPhoto = null;
                }
                else
                {
                    relatPhoto = Utilities.GetFileDbLocationString(Utilities.LocationType.ParentPhoto, SelectedIDStudent.ToString() + relationship.RelationshipType, RelatPhotoLocationStr);
                    relationship.Photolocation = relatPhoto;
                    Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { RelatPhotoLocationStr.FileName, relatPhoto });
                }
                relationship.Maker = Data.user.OwnerID;
                relationship.RelatinshipID = relatID;
            }
            else
            {
                relationship = new Relationship();
                relationship.RelationshipType = this.RelationshipType;
                if (relationship.RelationshipType == string.Empty)
                {
                    relationship.RelationshipType = dropRelatWithChild.Text;
                    RelationshipType = relationship.RelationshipType;
                }
                relationship.RelationshipName = txtFName.Text;
                relationship.Nationality = txtFNationality.Text;
                relationship.AustralianResidence = txtFAuRes.Text;
                relationship.AustralianAborigin = txtFAuAborigine.Text;
                relationship.SchoolEducation = txtFSchoolEdu.Text;
                relationship.NonSchoolEducation = txtFNonSchoolEdu.Text;
                relationship.Occupation = txtFOccupation.Text;
                relationship.Homeaddress = txtFHomeAddress.Text;
                relationship.Homestate = txtHomeState.Text;
                relationship.HomeCountry = txtFHomeCountry.Text;
                relationship.Suburb = txtFSuburb.Text;
                relationship.PostCode = txtFPostCode.Text;
                relationship.PostalAddress = txtFPostalAdd.Text;
                relationship.PostalState = txtFPostalState.Text;
                relationship.PostalSuburb = txtFPostalSuburb.Text;
                relationship.PostalCode = txtFPostalCode.Text;
                relationship.PostalCountry = txtFPostalCountry.Text;
                relationship.HomephoneNo = txtFHomePhone.Text;
                relationship.MobileNumb = txtFMobileNumber.Text;
                relationship.FaxNumber = txtFFaxNumber.Text;
                relationship.EmailAddress = txtFEmailAddress.Text;
                relationship.Whatsapp = txtFWhatsapp.Text;
                relationship.MainLang = txtFMainLanguage.Text;
                relationship.OtherThanEnglish = txtFOtherThanEnglishSpoken.Text;
                if (RelatPhotoLocationStr == null)
                {
                    relatPhoto = null;
                }
                else
                {
                    relatPhoto = Utilities.GetFileDbLocationString(Utilities.LocationType.ParentPhoto, SelectedIDStudent.ToString() + relationship.RelationshipType, RelatPhotoLocationStr);
                    relationship.Photolocation = relatPhoto;
                    Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { RelatPhotoLocationStr.FileName, relatPhoto });
                }
                relationship.Maker = Data.user.OwnerID;
            }
            if (RelationshipType == "" || RelationshipType == null)
            {
                PopUp.Alert("Please specify the relationship type!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (Entities.Relationship.SaveRelationship(parentSaved, relationship))
                {
                    PopUp.Alert("Parent record saved succesfully!", frmAlert.AlertType.Success);
                    relationship = null;
                    relatPhoto = null;
                    RelationshipType = null;
                    Utilities.ClearInputOnPanel(PanelRelat1);
                    Utilities.ClearInputOnPanel(PanelRelat2);
                    StudentRelationship();
                    ContainerRelationship.SendToBack();
                }
                else
                {
                    PopUp.Alert("Failed to save parent record :(", frmAlert.AlertType.Error);
                }
            }
        }
        private void btnSaveRelationship_Click(object sender, EventArgs e)
        {
            if (txtFName.Text == "")
            {
                PopUp.Alert("Name cannot be null!", frmAlert.AlertType.Warning);
            }
            else if (txtFNationality.Text == "")
            {
                PopUp.Alert("Nationality cannot be null!", frmAlert.AlertType.Warning);
            }
            else
            {
                SaveParent();
            }
        }
        private bool parentLoaded = false;
        private void dropRelatWithChild_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (parentLoaded)
            {
                try
                {
                    if (dropRelatWithChild.SelectedItem.ToString() == "OTHER, PLEASE SPECIFY")
                    {
                        txtRelatWithChild.Visible = true;
                        RelationshipType = txtRelatWithChild.Text;
                        txtRelatWithChild.Enabled = true;
                    }
                    else
                    {
                        RelationshipType = dropRelatWithChild.SelectedItem.ToString();
                        txtRelatWithChild.Enabled = false;
                        txtRelatWithChild.Visible = false;
                    }
                }
                catch (NullReferenceException)
                {

                }
            }
            else
            {

            }
            
        }

        private void txtRelatWithChild_TextChanged_1(object sender, EventArgs e)
        {
            RelationshipType = txtRelatWithChild.Text;
        }

        private void picFather_Click_1(object sender, EventArgs e)
        {
            RelatPhotoLocationStr = Utilities.OpenImage(picFather);

        }


        private void btnEditParent_Click(object sender, EventArgs e)
        {
            if (relationship!=null)
            {
                GoRelationship(RelationshipEditMode.Edit);
            }
            else
            {
                PopUp.Alert("No relationship to edit, please select the relationship to edit first!", frmAlert.AlertType.Warning);
            }
        }

        private void btnBacktoRelat_Click(object sender, EventArgs e)
        {
            relatPhoto = null;
            RelatPhotoLocationStr = null;
            RelationshipType = null;
            Utilities.ClearInputOnPanel(PanelRelat1);
            Utilities.ClearInputOnPanel(PanelRelat2);
            ContainerRelationship.SendToBack();
        }

        private void dropPropGrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtHomeAddress_TextChanged(object sender, EventArgs e)
        {

        }
        private string relatPhoto = string.Empty;
        private void picStud_Click_1(object sender, EventArgs e)
        {
            studentImg = Utilities.OpenImage(picStud);
            if (studentImg == null)
            {
                photo = null;
            }
            else
            {
                photo = Utilities.GetFileDbLocationString(Utilities.LocationType.StudentPhoto, SelectedIDStudent.ToString(), studentImg);
            }
        }

        private void btnStudOk_Click_1(object sender, EventArgs e)
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
            panelStud2.BringToFront();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Confirmation.Fire(Confirmation.onConfirmEnum.CancelStudentRecord);
        }

        private void dgRelationshipList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgParList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectedAssignParent = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgParList, "id"));
                relationship = new Relationship();
                relationship.RelatinshipID = SelectedAssignParent;
                relatID = SelectedAssignParent;
            }
            catch (Exception)
            {

            }
        }
    }
}
