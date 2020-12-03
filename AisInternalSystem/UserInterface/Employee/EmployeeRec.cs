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
        private bool isSaved = false;
        private OpenFileDialog EmployePicOPF = null;
        private string EmployeePicStr;
        private Entities.Employee employee = null;
        OpenFileDialog DocumentPath = null;
        private string employeePhotoLocation = null;
        #endregion

        #region Construct
        public EmployeeRec()
        {

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
            if (Module.Document.Insert(new string[5] { dbString, Utilities.GetCurrentUserID().ToString(), dropDocsType.SelectedItem.ToString(), txtDocsDesc.Text, employee.EmployeeIdentifier.ToString() }))
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
            #region Document
            DocumentInit();
            #endregion
            #region Personal
            txtFullname.Text = e.Fullname;
            dropGender.Text = e.Gender;
            dropReligion.Text = e.Religion;
            txtPlaceOfBirth.Text = e.PlaceOfBirth;
            txtNationality.Text = e.Nationality;
            dropDob.Value = e.DateOfBirth;
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
            #endregion
            #region Education

            #endregion
        }
        private void LoadFirst()
        {
            dropDocsType.DataSource = Data.DocumentTypeEmployee;
        }
        private void ModeSwitcher(EditMode mode)
        {
            _mode = mode;
            switch (mode)
            {
                case EditMode.Create:
                    //InitControl();
                    Utilities.ClearInputOnPanel(personal1);
                    Utilities.ClearInputOnPanel(personal2);
                    break;
                case EditMode.Update:
                    //InitEmployee();

                    break;
            }
        }
        private void SaveEmployeeData()
        {
            Entities.Employee e = employee;
            if (e==null)
            {
                e = new Entities.Employee();
            }
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
                if (Entities.Employee.UpdatePhoto(e.EmployeeIdentifier, employeePhotoLocation))
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
            PopUp.Alert("Photo Uploaded!", frmAlert.AlertType.Success);
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
            if (isSaved)
            {

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

        #endregion

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (isSaved)
            {
                SaveEmployeeData();
                this.SendToBack();
            }
            else
            {
                PopUp.Alert("Please save the employee data before Finalizing the data!", frmAlert.AlertType.Error);
            }
        }
    }
}
