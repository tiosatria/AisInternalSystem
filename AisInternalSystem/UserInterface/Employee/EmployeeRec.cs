using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AisInternalSystem.Controller;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Employee
{
    public partial class EmployeeRec : UserControl
    {
        #region Properties
        private bool isLoaded = false;
        private bool isBusy = false;
        private bool isSaved = false;
        private bool EduEdit = false;
        private Entities.Education education = null;
        private List<Entities.Education> edulist = null;
        private OpenFileDialog EmployePicOPF = null;
        private string EmployeePicStr;
        private Entities.Employee employee = null;
        OpenFileDialog DocumentPath = null;
        private string employeePhotoLocation = null;
        #endregion

        #region Construct
        public EmployeeRec()
        {
            Confirmation.CancelEmployee += Confirmation_CancelEmployee;
        }

        private void Confirmation_CancelEmployee(object sender, EventArgs e)
        {
            InitClearFormEmployee();
            UIController.NavigateUI(UIController.Controls.EmployeeDirectoryService);
        }

        private void InitClearFormEmployee()
        {
            isBusy = false;
            picEmployee.Image = Properties.Resources.icons8_male_user_200px;
            EmployePicOPF = null;
            education = null;
            Utilities.ClearInputOnPanel(personal1);
            Utilities.ClearInputOnPanel(personal2);
        }

        public void InitObject(EditMode mode)
        {
            if (!isLoaded)
            {
                InitializeComponent();
                LoadFirst();
            }
            else
            {

            }
            isLoaded = true;
            _mode = mode;
            ModeSwitcher(mode);
        }
        #endregion

        #region Enumeration
        public enum LeftNavigationSwitch
        {
            Personal, Relationship, Education
        }
        public enum RightNavigationSwitch
        {
            Document, Medical
        }
        private LeftNavigationSwitch _leftswitch;
        private RightNavigationSwitch _rightswitch;
        
        public enum EditMode
        {
            Create, Update
        }
        private EditMode _mode;
        #endregion

        #region Function
        #region Documents
        private void OpenDocument()
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
            string dbString = Utilities.GetFileDbLocationString(Utilities.LocationType.EmployeeDocument, $"{employee.EmployeeIdentifier}_{dropDocsType.SelectedItem}", DocumentPath);
            Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { DocumentPath.FileName, dbString });
            Utilities.workerparam.ProgressChanged += Workerparam_ProgressChanged;
            if (Module.Document.Insert(Module.Document.DocumentFor.Employee, new string[5] { dbString, Utilities.GetCurrentUserID().ToString(), dropDocsType.SelectedItem.ToString(), txtDocsDesc.Text, employee.EmployeeIdentifier.ToString() }))
            {
                PopUp.Alert("Data is valid\nnow we're writing data to the server", frmAlert.AlertType.Info);
            }
            else
            {
                PopUp.Alert("Failed to upload document :(", frmAlert.AlertType.Error);
            }
        }
        private void DeleteDocument()
        {
            string idstring = Utilities.GetSelectedDatagridValue(dgDocs, "id_docs");
            if (idstring == "" || idstring == null)
            {
                PopUp.Alert("There is nothing to delete!", frmAlert.AlertType.Warning);
            }
            else
            {
                if(Module.Document.Delete( Module.Document.DocumentFor.Employee, idstring))
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
        private void UploadClear()
        {
            DocumentPath = null;
            txtDocsPath.Text = "";
            dropDocsType.Enabled = true;
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
        private void DocumentInit()
        {
            DataTable dt = Module.Document.GetDocumentbyID(employee.EmployeeIdentifier, "employee");
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

        #endregion

        #region Operation
        private void LoadExistingData(Entities.Employee e)
        {
            employee = e;
            #region Document
            DocumentInit();
            #endregion
            #region Personal
            txtEmployeeID.Text = e.EmployeeID.ToString();
            txtFullname.Text = e.Fullname;
            dropGender.Text = e.Gender;
            dropReligion.Text = e.Religion;
            txtPlaceOfBirth.Text = e.PlaceOfBirth;
            txtNationality.Text = e.Nationality;
            try
            {
                dropDob.Value = e.DateOfBirth;

            }
            catch (Exception)
            {
                dropDob.Value = DateTime.Now;
            }
            txtFullAddress.Text = e.Address;
            txtHomePhone.Text = e.HomePhone;
            txtHomeTown.Text = e.HometownNumber;
            txtHometownAddress.Text = e.HometownAddress;
            txtMobileNumber.Text = e.Mobile;
            txtWhatsappNumber.Text = e.Whatsapp;
            txtPersonalEmail.Text = e.PersonalEmail;
            txtNpwpNumber.Text = e.NPWP;
            txtNikKtp.Text = e.NIK;
            txtBPJSTK.Text = e.BPJSTK;
            txtBPJSKES.Text = e.BPJSKES;
            dropRole.Text = e.Role;
            dropDepartment.Text = e.Department;
            dropJoinDate.Value = e.JoinDate;
            txtPassport.Text = e.PassportNumber;
            dropEmployeeSTatus.Text = e.EmployeeStatus;
            employeePhotoLocation = e.PhotoLocation;
            dropMaritalStatus.Text = e.MaritalStatus;
            txtSpouseName.Text = e.SpouseName;
            txtSpousePhone.Text = e.SpousePhone;
            e.NoOfDependant = Convert.ToInt32(numDependant.Value);
            txtContactName.Text = e.EmergencyContactName;
            txtContactRelationship.Text = e.EmergencyContactRelationship;
            txtEmergencyConcatAddress.Text = e.EmergencyContactAddress;
            txtEmergencyContactPhone.Text = e.EmergencyContactPhone;
            picEmployee.Image = Utilities.GetImage(e.PhotoLocation);
            #endregion
            #region Education
            LoadEducation(e);
            #endregion
        }
        private void SpouseVisible(bool o)
        {
            lblspname.Visible = o;
            lblspphone.Visible = o;
            txtSpouseName.Visible = o;
            txtSpousePhone.Visible = o;
        }
        private void LoadEducation(Entities.Employee e)
        {
            edulist = Entities.Education.GetEducation(e.EmployeeIdentifier);
            if (edulist != null && edulist.Count >= 1)
            {
                dgEducationDetail.DataSource = edulist;
                dgEducationDetail.Visible = true;
                label34.Visible = false;
            }
            else
            {
                label34.Visible = true;
                dgEducationDetail.Visible = false;
            }
        }
        private void LoadFirst()
        {

        }
        private void ModeSwitcher(EditMode mode)
        {
            if (isBusy)
            {
                PopUp.Alert("You have ongoing record/editing session, please click cancel or finish to finish the current editing session!", frmAlert.AlertType.Error);
            }
            else
            {
                _mode = mode;
                switch (mode)
                {
                    case EditMode.Create:
                        //InitControl();
                        isSaved = false;
                        isBusy = true;
                        EmployeePicStr = null;
                        EmployePicOPF = null;
                        Utilities.ClearInputOnPanel(personal1);
                        Utilities.ClearInputOnPanel(personal2);
                        Utilities.ClearInputOnPanel(Education1);
                        Utilities.ClearInputOnPanel(Relationship1);
                        Utilities.ClearInputOnPanel(PanelUploadDocsUp);
                        dgDocs.DataSource = null;
                        dgEducationDetail.DataSource = null;
                        NavSwitchLeft(LeftNavigationSwitch.Personal);
                        NavSwitchRight(RightNavigationSwitch.Document);
                        break;
                    case EditMode.Update:
                        isSaved = true;
                        isBusy = true;
                        EmployeePicStr = null;
                        EmployePicOPF = null;
                        Utilities.ClearInputOnPanel(personal1);
                        Utilities.ClearInputOnPanel(personal2);
                        NavSwitchLeft(LeftNavigationSwitch.Personal);
                        NavSwitchRight(RightNavigationSwitch.Document);
                        LoadExistingData(EmployeeDirectory.employee);
                        break;
                }
            }
        }
        private void SaveEmployeeData()
        {
            Entities.Employee e = employee;
            if (e==null)
            {
                e = new Entities.Employee();
            }
            e.EmployeeID = Convert.ToInt64(txtEmployeeID.Text);
            e.Fullname = txtFullname.Text;
            e.Gender = dropGender.Text;
            e.Religion = dropReligion.Text;
            e.PlaceOfBirth = txtPlaceOfBirth.Text;
            e.Nationality = txtNationality.Text;
            e.DateOfBirth = dropDob.Value;
            e.Address = txtFullAddress.Text;
            e.HomePhone = txtHomePhone.Text;
            e.HometownNumber = txtHomeTown.Text;
            e.HometownAddress = txtHometownAddress.Text;
            e.Mobile = txtMobileNumber.Text;
            e.Whatsapp = txtWhatsappNumber.Text;
            e.PersonalEmail = txtPersonalEmail.Text;
            e.NPWP = txtNpwpNumber.Text;
            e.NIK = txtNikKtp.Text;
            e.BPJSTK = txtBPJSTK.Text;
            e.BPJSKES = txtBPJSKES.Text;
            e.Role = dropRole.Text;
            e.Department = dropDepartment.Text;
            e.JoinDate = dropJoinDate.Value;
            e.PassportNumber = txtPassport.Text;
            e.EmployeeStatus = dropEmployeeSTatus.Text;
            e.PhotoLocation = employeePhotoLocation;
            e.MaritalStatus = dropMaritalStatus.Text;
            e.SpouseName = txtSpouseName.Text;
            e.SpousePhone = txtSpousePhone.Text;
            e.Maker = Data.user.OwnerID;
            e.NoOfDependant = Convert.ToInt32(numDependant.Value);
            e.EmergencyContactName = txtContactName.Text;
            e.EmergencyContactRelationship = txtContactRelationship.Text;
            e.EmergencyContactAddress = txtEmergencyConcatAddress.Text;
            e.EmergencyContactPhone = txtEmergencyContactPhone.Text;
            if (isSaved)
            {
                //revise
                if (Entities.Employee.SaveEmployee(true, e))
                {
                    PopUp.Alert("Employee data updated succesfully!", frmAlert.AlertType.Success);
                }
                else
                {
                    PopUp.Alert("Failed to update employee data :( \nPlease try again!", frmAlert.AlertType.Error);
                }
            }
            else
            {

                //insert
                if (Entities.Employee.SaveEmployee(false, e))
                {
                    PopUp.Alert("Employee data has been saved succesfully!", frmAlert.AlertType.Success);
                    isSaved = true;
                }
                else
                {
                    PopUp.Alert("Failed to save employee data :(\nPlease try again!", frmAlert.AlertType.Error);
                }
            }
            WritePhoto(e);
        }

        private void WritePhoto(Entities.Employee e)
        {
            Utilities.WorkerFinished += Utilities_WorkerFinished;
            btnFinalize.Enabled = false;
            if (EmployePicOPF!=null)
            {
                string employeePhotoLocation = Utilities.GetFileDbLocationString(Utilities.LocationType.EmployeePhoto, e.EmployeeID.ToString(), EmployePicOPF);
                if (Entities.Employee.UpdatePhoto(e.EmployeeID, employeePhotoLocation))
                {
                    Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { EmployePicOPF.FileName, employeePhotoLocation });
                }
                else
                {
                    PopUp.Alert("Failed to update photo, please try again!", frmAlert.AlertType.Error);
                }
            }
            else
            {
                btnFinalize.Enabled = true;
            }
        }
        private void Utilities_WorkerFinished(object sender, EventArgs e)
        {
            btnFinalize.Enabled = true;
        }
        #endregion

        #region NavigationSwitch
        private void NavSwitchLeft(LeftNavigationSwitch _left)
        {
            switch (_left)
            {
                case LeftNavigationSwitch.Personal:
                    ContainerEmployee.BringToFront();
                    break;
                case LeftNavigationSwitch.Relationship:
                    ContainerRelationship.BringToFront();
                    break;
                case LeftNavigationSwitch.Education:
                    ContainerEducation.BringToFront();
                    break;
            }
        }
        private void NavSwitchRight(RightNavigationSwitch _right)
        {
            switch (_right)
            {
                case RightNavigationSwitch.Document:
                    ContainerDocuments.BringToFront();
                    break;
                case RightNavigationSwitch.Medical:
                    //to be added
                    break;
            }
        }
        #endregion

        #endregion

        #region Event

        private void btnOpenDocs_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void btnDeleteDocs_Click(object sender, EventArgs e)
        {
            DeleteDocument();
        }
        private void btnStud1Next_Click(object sender, EventArgs e)
        {
            personal1.SendToBack();
        }
        private void picEmployee_Click(object sender, EventArgs e)
        {
            EmployePicOPF = Controller.Utilities.OpenImage(picEmployee);
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (isSaved)
            {
                DocumentPath = Utilities.OpenFile("Document Files(*.JPG; *.PNG; *.JPEG; *.PDF; *.DOCX; *.DOC; *.XLSX;) | *.JPG; *.PNG; *.JPEG; *.PDF; *.DOCX; *.DOC; *.XLSX; | All Files (*.*) | *.*");
                txtDocsPath.Text = DocumentPath.FileName;
            }
            else
            {
                PopUp.Alert("Please save the employee data to upload the document!", frmAlert.AlertType.Warning);
            }
        }
        private void btnUploadRecord_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                PopUp.Alert("Please save the employee data to upload the document!", frmAlert.AlertType.Warning);
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
        private void btnNavMeds_Click(object sender, EventArgs e)
        {
            PopUp.Alert("This module is still planned to be released in the future!", frmAlert.AlertType.Info);
        }

        private void btnStudBack_Click(object sender, EventArgs e)
        {
            personal2.SendToBack();
        }

        private void HighlightLeftPanel(Guna.UI2.WinForms.Guna2Button btn)
        {
            UIController.HighlightButton(new List<Guna.UI2.WinForms.Guna2Button> { btnNavPersonal, btnNavRelat, btnNavEdu }, btn);
        }
        private void HighlightRightPanel(Guna.UI2.WinForms.Guna2Button btn)
        {
            UIController.HighlightButton(new List<Guna.UI2.WinForms.Guna2Button> { btnNavDocs, btnNavMeds }, btn);
        }

        private void BtnStud__Click(object sender, EventArgs e)
        {
            HighlightLeftPanel(btnNavPersonal);
            NavSwitchLeft(LeftNavigationSwitch.Personal);
        }

        private void btnNavDocs_Click(object sender, EventArgs e)
        {
            HighlightRightPanel(btnNavDocs);
            NavSwitchRight(RightNavigationSwitch.Document);
        }

        private void btnSibling_Click(object sender, EventArgs e)
        {
            HighlightLeftPanel(btnNavEdu);
            NavSwitchLeft(LeftNavigationSwitch.Education);
        }

        private void btnRelat1_Click(object sender, EventArgs e)
        {
            HighlightLeftPanel(btnNavRelat);
            NavSwitchLeft(LeftNavigationSwitch.Relationship);
        }
        private void btnStudOk_Click(object sender, EventArgs e)
        {
            if (txtFullname.Text != "" && txtPlaceOfBirth.Text != "" && txtNationality.Text != "" && txtFullAddress.Text != "" && txtEmployeeID.Text != "")
            {
                SaveEmployeeData();
            }
            else
            {
                PopUp.Alert("Please fill the required info and generate Employee ID!", frmAlert.AlertType.Warning);
            }
        }
        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (isSaved)
            {
                picEmployee.Image = Properties.Resources.icons8_male_user_100;
                SaveEmployeeData();
                employee = null;
                education = null;
                edulist = null;
                isSaved = false;
                EduEdit = false;
                isBusy = false;   
                this.SendToBack();
            }
            else
            {
                PopUp.Alert("Please save the employee data before Finalizing the data!", frmAlert.AlertType.Error);
            }
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtContactRelationship.Text != "" && txtContactName.Text != "" && txtEmergencyConcatAddress.Text != "" && txtEmergencyContactPhone.Text != "")
            {
                SaveEmployeeData();
            }
            else
            {
                PopUp.Alert("Please fill the required info!", frmAlert.AlertType.Warning);
            }
        }
        private void dropMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropMaritalStatus.Text == "SINGLE")
            {
                SpouseVisible(false);
            }
            else
            {
                SpouseVisible(true);
            }
        }

        #endregion
        private void btnAddEducation_Click(object sender, EventArgs es)
        {
            Entities.Education e = education;
            if (e!=null)
            {
                e.EduDesignation = txtEduDesignation.Text;
                e.EduLevel = dropEduLevel.Text;
                e.EmployeeID = employee.EmployeeIdentifier;
                e.GraduatedYear = txtYearGraduated.Text;
                e.StartYear = txtYearStart.Text;
                e.Institution = txtInstitution.Text;
                SaveEducation(e);
            }
            else
            {
                if (employee!=null)
                {
                    e = new Entities.Education();
                    e.EduDesignation = txtEduDesignation.Text;
                    e.EduLevel = dropEduLevel.Text;
                    e.EmployeeID = employee.EmployeeIdentifier;
                    e.GraduatedYear = txtYearGraduated.Text;
                    e.StartYear = txtYearStart.Text;
                    e.Institution = txtInstitution.Text;
                    SaveEducation(e);
                }
                else
                {
                    PopUp.Alert("Please save the employee data before recording education information!", frmAlert.AlertType.Error);
                }
            }
        }

        private void GetIndividualEducationDetails()
        {
            int o = 0;
            try
            {
                o = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgEducationDetail, "ID"));
            }
            catch (Exception)
            {
                o = 0;
            }
            if (o != 0)
            {
                education = Entities.Education.GetIndividiualEducation(edulist, o);
            }
            else
            {
                education = null; 
            }
        }

        private void dgEducationDetail_SelectionChanged(object sender, EventArgs e)
        {
            GetIndividualEducationDetails();
        }
        private void btnEditEdu_Click(object sender, EventArgs e)
        {
            if (education!=null)
            {
                dropEduLevel.SelectedIndex = dropEduLevel.Items.IndexOf(education.EduLevel);
                txtEduDesignation.Text = education.EduDesignation;
                txtInstitution.Text = education.Institution;
                txtYearStart.Text = education.StartYear;
                txtYearGraduated.Text = education.GraduatedYear;
                if (txtYearGraduated.Text == "" || txtYearGraduated.Text == null)
                {
                    chkIsPursuingDegree.Checked = true;
                }
                else
                {
                    chkIsPursuingDegree.Checked = false;
                }
                btnAddEducation.Text = "Revise Education";
                EduEdit = true;
            }
            else
            {
                PopUp.Alert("Please select the education information that you wish to edit!", frmAlert.AlertType.Warning);
            }
        }
        private void SaveEducation(Entities.Education e)
        {
            if (employee != null || employee.EmployeeIdentifier != null)
            {
                if (txtInstitution.Text != "" && txtYearStart.Text != "")
                {
                    if (EduEdit)
                    {
                        ReviseEdu(e);
                        EduEdit = false;    
                    }
                    else
                    {
                        SaveEdu(e);
                    }
                }
                else
                {
                    PopUp.Alert("Please specify the institution name and education start year!", frmAlert.AlertType.Warning);
                }
            }
            else
            {
                PopUp.Alert("Please save employee data before saving the education information", frmAlert.AlertType.Warning);
            }
        }
        private void ReviseEdu(Entities.Education e)
        {
            if (Entities.Education.Update(e))
            {
                PopUp.Alert("Education information updated succesfully!", frmAlert.AlertType.Success);
                ClearEditEdu();
                LoadEducation(employee);
            }
            else
            {
                PopUp.Alert("Failed to revise education information", frmAlert.AlertType.Error);
            }
        }
        private void SaveEdu(Entities.Education e)
        {
            if (Entities.Education.Insert(e))
            {
                PopUp.Alert("Education information recorded succesfully!", frmAlert.AlertType.Success);
                ClearEditEdu();
                Utilities.ClearInputOnPanel(Education1);
            }
            else
            {
                PopUp.Alert("Failed to record education information, please try again!", frmAlert.AlertType.Error);
            }
        }
        private void IsPursuingDegree(bool o)
        {
            if (o == true)
            {
                txtYearGraduated.Clear();
                txtYearGraduated.Enabled = false;
            }
            else
            {
                txtYearGraduated.Clear();
                txtYearGraduated.Enabled = true;
            }
        }
        private void ClearEditEdu()
        {
            dropEduLevel.SelectedIndex = 0;
            txtEduDesignation.Clear();
            txtInstitution.Clear();
            txtYearStart.Clear();
            txtYearGraduated.Clear();
            IsPursuingDegree(false);
            btnAddEducation.Text = "Add Education Details";
        }
        private void DeleteEducationDetails(Entities.Education e)
        {
            if (Entities.Education.Delete(e))
            {
                PopUp.Alert("Education information deleted succesfully!", frmAlert.AlertType.Success);
                LoadEducation(employee);
            }
            else
            {
                PopUp.Alert("Failed to delete education information", frmAlert.AlertType.Error);
            }
        }
        private void btnDeleteEdu_Click(object sender, EventArgs e)
        {
            if (education!=null)
            {
                DeleteEducationDetails(education);
            }
            else
            {
                PopUp.Alert("Please click on the desired education information to delete", frmAlert.AlertType.Warning);
            }
        }

        private void Education1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dropEduLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropEduLevel.Text == "Vocational" || dropEduLevel.Text == "College/HigherSecondary (SMU/SMK)")
            {
                txtEduDesignation.Clear();
                txtEduDesignation.Visible = false;
                label23.Visible = false;
                chkIsPursuingDegree.Visible = false;
            }
            else
            {
                txtEduDesignation.Clear();
                txtEduDesignation.Visible = true;
                label23.Visible = true;
                chkIsPursuingDegree.Visible = true;
            }
        }

        private void chkIsPursuingDegree_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsPursuingDegree.CheckState == CheckState.Checked)
            {
                IsPursuingDegree(true);
            }
            else
            {
                IsPursuingDegree(false);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (isSaved)
            {
                PopUp.Alert("Can only generate employeeID first time!", frmAlert.AlertType.Warning);
            }
            else
            {
                txtEmployeeID.Text = Entities.Employee.GenerateEmployeeID(dropJoinDate.Value, dropDepartment.SelectedIndex + 1, dropRole.SelectedIndex + 1, dropRole.Text).ToString();

            }
        }

        private void ContainerDocuments_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Confirmation.Fire(Confirmation.onConfirmEnum.CancelEmployee);
        }
    }
}
