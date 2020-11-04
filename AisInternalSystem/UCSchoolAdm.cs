using AisInternalSystem.Module;
using AisInternalSystem.Properties;
using Guna.UI2.WinForms;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows.Forms;

namespace AisInternalSystem
{
    public partial class UCSchoolAdm : UserControl
    {
        OpenFileDialog opf = new OpenFileDialog();
        int RelationshipCount = 0, aisid = 0, siblingid = 0, schoolId = 0, docsSelected = 0, careTeacher = 0;
        int? AssTeacher, StudCAID, ClassId, GradeID, aisidCA;
        Data collection = new Data();
        StudSummary summarystud = new StudSummary();
        bool StudIsSaved = false, addRelatIsClicked = false, fatherIsSaved = false, motheriIsSaved = false, guardianIsSaved = false, stepFatherIsSaved = false, stepMotherIsSaved = false,
            medsIsSaved = false;
        string lblExpktp = "No explanation", lblExpKitas = "", lblExpform = "", lblexpParentsPhoto = "", lblExpPassport = "", gender = "Male", siblingGender = "Male", studentPhoto = null,
            fatherPhoto = null, motherPhoto = null, stepFatherPhoto = null, stepMotherPhoto = null,
            careTeacherName, AssistantTeacherName,
            guardianPhoto = null, timeStamping = "yyyy-MM-dd HH:mm:ss", docspath = null, docspathDb = null, docstype = null, docsname = null, extension = null, medsdose = "No Details";
        Point defaultPanelLocation = new Point(16, 33);
        Size defaultPanelSize = new Size(674, 572);
        Size defaultUcSize = new Size(1280, 611);
        Size PanelAddRelationshipSize = new Size(228, 266);
        Point PanelAddRelationshipLoc = new Point(472, 33);
        Size RightSidePanelSize = new Size(549, 401);
        Point RightSidePanelLoc = new Point(721, 33);
        Point DownRightSidePanelLoc = new Point(721, 445);
        Size DownRightSidePanelSize = new Size(549, 160);
        BackgroundWorker worker = new BackgroundWorker();
        UCStudDetailed PanelStudDetailed = new UCStudDetailed();
        UCClassDirectoryService ClassDirectory = new UCClassDirectoryService();
        //MainMenu Size And Location
        Size panelMainMenu = new Size(640, 305);
        Point Panel1 = new Point(0, 0);
        Point Panel2 = new Point(640, 0);
        Point Panel3 = new Point(0, 305);
        Point Panel4 = new Point(640, 305);

        public UCSchoolAdm()
        {
            InitializeComponent();
            init();
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            UIState(UIStateEnum.MainMenu);
            dropReligion.DataSource = collection.Religion;
            dropSpokenEnglish.DataSource = collection.Proficiency;
            dropPropGrade.DataSource = collection.Grade;
            dropChooseGrade.DataSource = collection.ChooseGrade;
            dropClassGradeGrade.DataSource = collection.ClassGrade;
            dropDocsType.DataSource = collection.DocumentType;
            dropStudStat.DataSource = collection.StudentStatus;
            dropGradeSchool.DataSource = collection.Grade;
        }

        public enum Searchby
        {
            Name,
            ID,
            Gender,
            Origin,
            Revised
        }
        private Searchby _searcby;
        void CopyFile(string source, string des)
        {
            FileStream fsOut = new FileStream(des, FileMode.Create);
            FileStream fsIn = new FileStream(source, FileMode.Open);
            byte[] bt = new byte[10000];
            int reaDbyte;

            while ((reaDbyte = fsIn.Read(bt, 0, bt.Length)) > 0)
            {
                fsOut.Write(bt, 0, reaDbyte);
                worker.ReportProgress((int)(fsIn.Position * 100 / fsIn.Length));
            }
            fsIn.Close();
            fsOut.Close();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            CopyFile(docspath, docspathDb);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressCopy.Value = e.ProgressPercentage;
            label33.Text = ProgressCopy.Value.ToString();
            if (ProgressCopy.Value == 100)
            {
                Msg.Alert("File uploaded succesfully", frmAlert.AlertType.Info);
                btnUploadRecord.Enabled = true;
                label8.Visible = false;
                ProgressCopy.Visible = false;
                label33.Visible = false;
                txtDocsName.Clear();
                txtDocsPath.Clear();
                txtDocsDesc.Clear();
                dropDocsType.SelectedIndex = 0;
                loadListDocs();
            }
            worker.Dispose();
        }

        private MouseEvent pointer;

        public enum MainMenu
        {
            RecordStudent,
            StudentDirectory,
            FutureFunction,
            Enquiry,
            def,

        }

        public enum UIStateEnum
        {
            MainMenu,
            RecordStudent,
            Enquiry,
            EditStudentData,
            StudentDirectory,
            ClassAssignment,
            DetailedStudent,
            ClassDirectory
            //add more here
        }

        void normalizePanelLeft(Panel p)
        {
            p.Show();
            p.Size = defaultPanelSize;
            p.Location = defaultPanelLocation;
        }

        void normalizePanelRight(Panel p)
        {
            p.Show();
            p.Size = RightSidePanelSize;
            p.Location = RightSidePanelLoc;

        }

        void normalizePanelRightDown(Panel p)
        {
            p.Show();
            p.Size = DownRightSidePanelSize;
            p.Location = DownRightSidePanelLoc;
        }

        void normalizePanelSingleRight(Panel p)
        {
            p.Show();
            p.Size = new Size(549, 561);
            p.Location = RightSidePanelLoc;
        }
        //Section UI State
        private UIStateEnum _State;
        public void UIState(UIStateEnum stateEnum)
        {
            _State = stateEnum;
            switch (_State)
            {
                case UIStateEnum.MainMenu:
                    defaultMainMenuPosition();
                    break;
                case UIStateEnum.RecordStudent:
                    hideMenu();
                    panelRecStud.Show();
                    panelRecStud.Dock = DockStyle.Fill;
                    panelRecStud.BringToFront();
                    PanelClassAssignment.Dock = DockStyle.None;
                    PanelClassAssignment.SendToBack();
                    normalizePanelLeft(panelStud1);
                    normalizePanelRight(PanelUploadDocsUp);
                    normalizePanelRightDown(PanelStudDocsDown);
                    BtnNavigate(BtnStud_);
                    ShowPanel(panelStud1);
                    collection.AutoCompleteLoad();
                    AutoComplete();
                    break;
                case UIStateEnum.EditStudentData:
                    hideMenu();
                    LoadStudentData();
                    panelStudDirectory.Hide();
                    panelRecStud.Show();
                    panelRecStud.Dock = DockStyle.Fill;
                    normalizePanelLeft(panelStud1);
                    normalizePanelRight(PanelUploadDocsUp);
                    normalizePanelRightDown(PanelStudDocsDown);
                    collection.AutoCompleteLoad();
                    AutoComplete();
                    break;
                case UIStateEnum.StudentDirectory:
                    hideMenu();
                    panelStudDirectory.Show();
                    panelStudDirectory.BringToFront();
                    panelStudDirectory.Dock = DockStyle.Fill;
                    break;
                case UIStateEnum.ClassAssignment:
                    hideMenu();
                    ClassAssignmentInit();
                    PanelClassAssignment.Show();
                    PanelClassAssignment.BringToFront();
                    PanelClassAssignment.Dock = DockStyle.Fill;
                    break;
                case UIStateEnum.ClassDirectory:
                    this.Controls.Add(ClassDirectory);
                    ClassDirectory.BringToFront();
                    ClassDirectory.InitClassList();
                    ClassDirectory.Dock = DockStyle.Fill;
                    break;
                case UIStateEnum.DetailedStudent:
                    hideMenu();
                    this.Controls.Add(PanelStudDetailed);
                    PanelStudDetailed.LoadStudentData(aisid);
                    PanelStudDetailed.BringToFront();
                    PanelStudDetailed.Dock = DockStyle.Fill;
                    break;
                default:
                    defaultMainMenuPosition();
                    break;
            }
        }



        void LoadStudentData()
        {
            StudIsSaved = true;
            //student
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select * from student_data where aisid = @aisid", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txtNisn.Text = reader.GetString("nis").ToString();
                    txtAsn.Text = reader.GetString("ausid").ToString();
                    txtais.Text = reader.GetString("aisid");
                    dropIntake.Value = reader.GetDateTime("intake");
                    txtFamilyName.Text = reader.GetString("familyname");
                    txtGivenName.Text = reader.GetString("givenname");
                    txtMiddleName.Text = reader.GetString("middlename");
                    txtCertificateName.Text = reader.GetString("certificatename");
                    dropDob.Value = reader.GetDateTime("dob");
                    txtPlaceOfBirth.Text = reader.GetString("pob");
                    txtHomeCountry.Text = reader.GetString("homecountry");
                    txtCountryOfBirth.Text = reader.GetString("cob");
                    gender = reader.GetString("gender");
                    if (gender == "MALE")
                    {
                        RadMale.Checked = true;
                    }
                    else
                    {
                        radFemale.Checked = true;
                    }
                    dropReligion.SelectedIndex = dropReligion.Items.IndexOf(reader.GetString("religion"));
                    txtNationality.Text = reader.GetString("nationality");
                    txtHomeAddress.Text = reader.GetString("homeaddress");
                    txtHomeState.Text = reader.GetString("homestate");
                    txtSuburb.Text = reader.GetString("suburb");
                    txtPostCode.Text = reader.GetString("postcode");
                    txtPostalCountry.Text = reader.GetString("postalcountry");
                    txtHomePhone.Text = reader.GetString("homephone");
                    if(txtHomePhone.Text == "" | txtHomePhone.Text == null)
                    {
                        reader.GetString("mobilenumb");
                    }
                    txtMobileNumber.Text = reader.GetString("mobilenumb");
                    txtFaxNumber.Text = reader.GetString("faxnumb");
                    txtLangSpoken.Text = reader.GetString("langspoken");
                    dropSpokenEnglish.SelectedIndex = dropSpokenEnglish.Items.IndexOf(reader.GetString("englishproficiency"));
                    dropStudStat.SelectedIndex = dropStudStat.Items.IndexOf(reader.GetString("studstat"));
                    try
                    {
                        picStud.Image = Image.FromFile(reader.GetString("studentimg"));
                        studentPhoto = reader.GetString("studentimg");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException ex)
                    {
                        picStud.Image = Resources.icons8_student_male_80px;
                    }
                    dropPropGrade.SelectedIndex = dropPropGrade.Items.IndexOf(reader.GetString("proposedgrade"));
                }
                reader.Close();
                //medical
                cmd = new MySqlCommand("select * from stud_medical_info where medicalofstud = @medicalofstud", Db.get_connection());
                cmd.Parameters.Add("@medicalofstud", MySqlDbType.Int32).Value = aisid;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txtMedsDetails.Text = reader.GetString("healthcondition");
                    txtMEdsAllergies.Text = reader.GetString("allergies");
                    txtMedsMedsAller.Text = reader.GetString("medication_for_allergies");
                    string requirespecial = reader.GetString("regularmedication");
                    if (requirespecial == "YES")
                    {
                        chkMedsyes.Checked = true;
                        chkmedsNo.Checked = false;
                    }
                    else
                    {
                        chkmedsNo.Checked = true;
                        chkMedsyes.Checked = false;
                    }
                    txtMedsDose.Text = reader.GetString("regularmedicationdetails");
                }
                reader.Close();

                //load docs
                loadListDocs();

                //prevschool
                PrevSchoolHandler();

                //sibling
                siblingHandler();

                //medical


                //check student relationship
                //father
                Db.open_connection();
                cmd = new MySqlCommand("select * from stud_relationship where relationshiptostud = @aisid and relationship = @relationship", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Father";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    fatherIsSaved = true;
                    RelationshipCount++;
                    btnAddFather.Enabled = false;
                    switch (RelationshipCount)
                    {
                        case 1:
                            entity1 = _entity.Father;
                            break;
                        case 2:
                            entity2 = _entity.Father;
                            break;
                        case 3:
                            entity3 = _entity.Father;
                            break;
                        case 4:
                            entity4 = _entity.Father;
                            break;
                    }
                    TopNavEntity(RelationshipCount, _entity.Father);
                    while (reader.Read())
                    {
                        txtFName.Text = reader.GetString("name");
                        txtFNationality.Text = reader.GetString("nationality");
                        txtFAuRes.Text = reader.GetString("australianresidence");
                        txtFAuAborigine.Text = reader.GetString("auabortorres");
                        txtFSchoolEdu.Text = reader.GetString("schooleducation");
                        txtFNonSchoolEdu.Text = reader.GetString("nonschooleducation");
                        txtFOccupation.Text = reader.GetString("occupation");
                        txtFHomeAddress.Text = reader.GetString("homeaddress");
                        txtFHomeState.Text = reader.GetString("homestate");
                        txtFHomeCountry.Text = reader.GetString("homecountry");
                        txtFSuburb.Text = reader.GetString("suburb");
                        txtFPostCode.Text = reader.GetString("postcode");
                        txtFPostalAdd.Text = reader.GetString("postaladdress");
                        txtFPostalState.Text = reader.GetString("postalstate");
                        txtFPostalSuburb.Text = reader.GetString("postalsuburb");
                        txtFPostalCode.Text = reader.GetString("postalcode");
                        txtFPostalCountry.Text = reader.GetString("postalcountry");
                        txtFHomePhone.Text = reader.GetString("homephoneno");
                        txtFMobileNumber.Text = reader.GetString("mobilenumb");
                        txtFaxNumber.Text = reader.GetString("faxnumb");
                        txtFEmailAddress.Text = reader.GetString("emailaddress");
                        txtFWhatsapp.Text = reader.GetString("whatsapp");
                        txtFMainLanguage.Text = reader.GetString("mainlang");
                        txtFOtherThanEnglishSpoken.Text = reader.GetString("otherthanenglish");
                        try
                        {
                            picFather.Image = Image.FromFile(reader.GetString("photo"));
                            fatherPhoto = reader.GetString("photo");

                        }
                        catch (Exception e)
                        {
                            picFather.Image = Resources.icons8_male_user_100;
                        }
                    }
                }
                reader.Close();
                //stepfather
                cmd = new MySqlCommand("select * from stud_relationship where relationshiptostud = @aisid and relationship = @relationship", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Step Father";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    stepFatherIsSaved = true;
                    RelationshipCount++;
                    BtnAddStepFather.Enabled = false;
                    switch (RelationshipCount)
                    {
                        case 1:
                            entity1 = _entity.StepFather;
                            break;
                        case 2:
                            entity2 = _entity.StepFather;
                            break;
                        case 3:
                            entity3 = _entity.StepFather;
                            break;
                        case 4:
                            entity4 = _entity.StepFather;
                            break;
                    }
                    TopNavEntity(RelationshipCount, _entity.StepFather);
                    while (reader.Read())
                    {
                        txtSFName.Text = reader.GetString("name");
                        txtSFNationality.Text = reader.GetString("nationality");
                        txtSFAuRes.Text = reader.GetString("australianresidence");
                        txtSFAuAbor.Text = reader.GetString("auabortorres");
                        txtSFSchoolEdu.Text = reader.GetString("schooleducation");
                        txtSFNonSchool.Text = reader.GetString("nonschooleducation");
                        txtSFOccupation.Text = reader.GetString("occupation");
                        txtSFHomeAdd.Text = reader.GetString("homeaddress");
                        txtSFHomeState.Text = reader.GetString("homestate");
                        txtSFHomeCountry.Text = reader.GetString("homecountry");
                        txtSFSuburb.Text = reader.GetString("suburb");
                        txtSFPostCode.Text = reader.GetString("postcode");
                        txtSFPostalAdd.Text = reader.GetString("postaladdress");
                        txtSFPostalState.Text = reader.GetString("postalstate");
                        txtSFPostalSuburb.Text = reader.GetString("postalsuburb");
                        txtSFPostalCode.Text = reader.GetString("postalcode");
                        txtSFPostalCountry.Text = reader.GetString("postalcountry");
                        txtSFHomePhone.Text = reader.GetString("homephoneno");
                        txtSFMobileNumb.Text = reader.GetString("mobilenumb");
                        txtSFFaxNumb.Text = reader.GetString("faxnumb");
                        txtSFEmailAdd.Text = reader.GetString("emailaddress");
                        txtSFWhatsappNumb.Text = reader.GetString("whatsapp");
                        txtSFParentsMainLang.Text = reader.GetString("mainlang");
                        txtSFOtherThanEnglish.Text = reader.GetString("otherthanenglish");
                        try
                        {
                            picStepFather.Image = Image.FromFile(reader.GetString("photo"));
                            stepFatherPhoto = reader.GetString("photo");

                        }
                        catch (System.Data.SqlTypes.SqlNullValueException ex)
                        {
                            picStepFather.Image = Resources.icons8_male_user_100;
                        }
                    }
                }
                reader.Close();
                //mother
                cmd = new MySqlCommand("select * from stud_relationship where relationshiptostud = @aisid and relationship = @relationship", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Mother";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    motheriIsSaved = true;
                    RelationshipCount++;
                    btnAddMother.Enabled = false;
                    switch (RelationshipCount)
                    {
                        case 1:
                            entity1 = _entity.Mother;
                            break;
                        case 2:
                            entity2 = _entity.Mother;
                            break;
                        case 3:
                            entity3 = _entity.Mother;
                            break;
                        case 4:
                            entity4 = _entity.Mother;
                            break;
                    }
                    TopNavEntity(RelationshipCount, _entity.Mother);
                    while (reader.Read())
                    {
                        txtMName.Text = reader.GetString("name");
                        txtMnationality.Text = reader.GetString("nationality");
                        txtMAuRes.Text = reader.GetString("australianresidence");
                        txtMAuaborigine.Text = reader.GetString("auabortorres");
                        txtMSchoolEdu.Text = reader.GetString("schooleducation");
                        txtMNonschool.Text = reader.GetString("nonschooleducation");
                        txtMOccupation.Text = reader.GetString("occupation");
                        txtmHomeAdd.Text = reader.GetString("homeaddress");
                        txtMHomeState.Text = reader.GetString("homestate");
                        txtMHomeCountry.Text = reader.GetString("homecountry");
                        txtMSuburb.Text = reader.GetString("suburb");
                        txtMPostcode.Text = reader.GetString("postcode");
                        txtMPostalAddress.Text = reader.GetString("postaladdress");
                        txtMPostalState.Text = reader.GetString("postalstate");
                        txtMPostalSuburb.Text = reader.GetString("postalsuburb");
                        txtMPostalcode.Text = reader.GetString("postalcode");
                        txtMPostalCountry.Text = reader.GetString("postalcountry");
                        txtMHomePhoneNumb.Text = reader.GetString("homephoneno");
                        txtMMobileNumb.Text = reader.GetString("mobilenumb");
                        txtMFaxNumb.Text = reader.GetString("faxnumb");
                        txtMEmailAdd.Text = reader.GetString("emailaddress");
                        txtMWhatsapp.Text = reader.GetString("whatsapp");
                        txtMMainLang.Text = reader.GetString("mainlang");
                        txtMOtherEng.Text = reader.GetString("otherthanenglish");
                        try
                        {
                            picMother.Image = Image.FromFile(reader.GetString("photo"));
                            motherPhoto = reader.GetString("photo");
                        }
                        catch (Exception ex)
                        {
                            picMother.Image = Resources.icons8_Person_Female_Skin_Type_5_80px;
                        }
                    }
                }
                reader.Close();
                //stepmother
                cmd = new MySqlCommand("select * from stud_relationship where relationshiptostud = @aisid and relationship = @relationship", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Step Mother";
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    stepMotherIsSaved = true;
                    RelationshipCount++;
                    BtnAddStepMother.Enabled = false;
                    switch (RelationshipCount)
                    {
                        case 1:
                            entity1 = _entity.StepMother;
                            break;
                        case 2:
                            entity2 = _entity.StepMother;
                            break;
                        case 3:
                            entity3 = _entity.StepMother;
                            break;
                        case 4:
                            entity4 = _entity.StepMother;
                            break;
                    }
                    TopNavEntity(RelationshipCount, _entity.StepMother);
                    while (reader.Read())
                    {
                        txtSMname.Text = reader.GetString("name");
                        txtSMnationality.Text = reader.GetString("nationality");
                        txtSMAures.Text = reader.GetString("australianresidence");
                        txtSMAuAborigine.Text = reader.GetString("auabortorres");
                        txtSMSchool.Text = reader.GetString("schooleducation");
                        txtSMNonSchool.Text = reader.GetString("nonschooleducation");
                        txtSMOccu.Text = reader.GetString("occupation");
                        txtSMHomeAdd.Text = reader.GetString("homeaddress");
                        txtSMHomeState.Text = reader.GetString("homestate");
                        txtSMHomeCountry.Text = reader.GetString("homecountry");
                        txtSMSuburb.Text = reader.GetString("suburb");
                        txtSMPostCode.Text = reader.GetString("postcode");
                        txtSMPostalAddress.Text = reader.GetString("postaladdress");
                        txtSMPostalState.Text = reader.GetString("postalstate");
                        txtSMPostalSuburb.Text = reader.GetString("postalsuburb");
                        txtSMPostalCode.Text = reader.GetString("postalcode");
                        txtSMPostalCountry.Text = reader.GetString("postalcountry");
                        txtSMHomephone.Text = reader.GetString("homephoneno");
                        txtSMMobileNumber.Text = reader.GetString("mobilenumb");
                        txtSMFaxNumb.Text = reader.GetString("faxnumb");
                        txtSMEmailAdd.Text = reader.GetString("emailaddress");
                        txtSMWhatsapp.Text = reader.GetString("whatsapp");
                        txtSMParentsMainLang.Text = reader.GetString("mainlang");
                        txtSMOtherThanEnglish.Text = reader.GetString("otherthanenglish");
                        try
                        {
                            PicStepMother.Image = Image.FromFile(reader.GetString("photo"));
                            stepMotherPhoto = reader.GetString("photo");
                        }
                        catch (System.Data.SqlTypes.SqlNullValueException ex)
                        {
                            PicStepMother.Image = Resources.icons8_Person_Female_Skin_Type_5_80px;
                        }
                    }
                }
                reader.Close();

                //guardian
                cmd = new MySqlCommand("SELECT * FROM aisDb.stud_relationship where relationshiptostud = @aisid and relationship != 'father' and relationship != 'mother';", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    guardianIsSaved = true;
                    RelationshipCount++;
                    BtnAddGuardian.Enabled = false;
                    switch (RelationshipCount)
                    {
                        case 1:
                            entity1 = _entity.Guardian;
                            break;
                        case 2:
                            entity2 = _entity.Guardian;
                            break;
                        case 3:
                            entity3 = _entity.Guardian;
                            break;
                        case 4:
                            entity4 = _entity.Guardian;
                            break;
                    }
                    TopNavEntity(RelationshipCount, _entity.Guardian);
                    while (reader.Read())
                    {
                        txtGname.Text = reader.GetString("name");
                        txtGNationality.Text = reader.GetString("nationality");
                        txtGAuRes.Text = reader.GetString("australianresidence");
                        txtGAuAborigine.Text = reader.GetString("auabortorres");
                        txtGSchEdu.Text = reader.GetString("schooleducation");
                        txtGNoNSch.Text = reader.GetString("nonschooleducation");
                        txtGOccupation.Text = reader.GetString("occupation");
                        txtGHomeAdd.Text = reader.GetString("homeaddress");
                        txtGHomeState.Text = reader.GetString("homestate");
                        txtGHomeCountry.Text = reader.GetString("homecountry");
                        txtGSuburb.Text = reader.GetString("suburb");
                        txtGPostCode.Text = reader.GetString("postcode");
                        txtGPostalAddress.Text = reader.GetString("postaladdress");
                        txtGPostalState.Text = reader.GetString("postalstate");
                        txtGPostalSuburb.Text = reader.GetString("postalsuburb");
                        txtGPostalCode.Text = reader.GetString("postalcode");
                        txtGPostalCountry.Text = reader.GetString("postalcountry");
                        txtGHomePhoneNumb.Text = reader.GetString("homephoneno");
                        txtGMobileNumb.Text = reader.GetString("mobilenumb");
                        txtGFaxNumb.Text = reader.GetString("faxnumb");
                        txtGEmailAdd.Text = reader.GetString("emailaddress");
                        txtGWhatsapp.Text = reader.GetString("whatsapp");
                        txtGMainLang.Text = reader.GetString("mainlang");
                        txtGRelationshipChild.Text = reader.GetString("relationship");
                        try
                        {
                            PicGuardian.Image = Image.FromFile(reader.GetString("photo"));
                            guardianPhoto = reader.GetString("photo");
                        }
                        catch (Exception)
                        {
                            PicGuardian.Image = Resources.icons8_Person_Female_Skin_Type_5_80px;
                        }
                    }
                }
                reader.Close();
            }

            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        void AutoComplete()
        {
            //student
            txtPlaceOfBirth.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPlaceOfBirth.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPlaceOfBirth.AutoCompleteCustomSource = collection.placeofbirth;

            txtHomeAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtHomeAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtHomeAddress.AutoCompleteCustomSource = collection.homeaddress;

            txtCountryOfBirth.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCountryOfBirth.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCountryOfBirth.AutoCompleteCustomSource = collection.countryofbirth;

            txtNationality.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtNationality.AutoCompleteCustomSource = collection.nationality;

            txtHomeState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtHomeState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtHomeState.AutoCompleteCustomSource = collection.HomeState;

            txtSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSuburb.AutoCompleteCustomSource = collection.suburb;

            txtHomeCountry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtHomeCountry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtHomeCountry.AutoCompleteCustomSource = collection.homecountry;

            txtPostalSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPostalSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPostalSuburb.AutoCompleteCustomSource = collection.postalsuburb;

            txtPostalState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPostalState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPostalState.AutoCompleteCustomSource = collection.postalstate;

            txtLangSpoken.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtLangSpoken.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtLangSpoken.AutoCompleteCustomSource = collection.langspoken;

            //father
            txtFOccupation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFOccupation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFOccupation.AutoCompleteCustomSource = collection.occupation;

            txtFNationality.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFNationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFNationality.AutoCompleteCustomSource = collection.nationality;

            txtFOccupation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFOccupation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFOccupation.AutoCompleteCustomSource = collection.occupation;

            txtFNonSchoolEdu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFNonSchoolEdu.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFNonSchoolEdu.AutoCompleteCustomSource = collection.nonschooledu;

            txtFSchoolEdu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFSchoolEdu.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFSchoolEdu.AutoCompleteCustomSource = collection.schooledu;

            txtFAuRes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFAuRes.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFAuRes.AutoCompleteCustomSource = collection.AustralianResidence;

            txtFAuAborigine.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFAuAborigine.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFAuAborigine.AutoCompleteCustomSource = collection.aborigine;

            txtFMainLanguage.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFMainLanguage.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFMainLanguage.AutoCompleteCustomSource = collection.langspoken;

            txtFOtherThanEnglishSpoken.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFOtherThanEnglishSpoken.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFOtherThanEnglishSpoken.AutoCompleteCustomSource = collection.langspoken;

            txtFHomeAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFHomeAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFHomeAddress.AutoCompleteCustomSource = collection.homeaddress;

            txtFHomeState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFHomeState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFHomeState.AutoCompleteCustomSource = collection.HomeState;

            txtFHomeCountry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFHomeCountry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFHomeCountry.AutoCompleteCustomSource = collection.homecountry;

            txtFSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFSuburb.AutoCompleteCustomSource = collection.suburb;

            txtFPostalState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFPostalState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFPostalState.AutoCompleteCustomSource = collection.HomeState;

            txtFPostalSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFPostalSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFPostalSuburb.AutoCompleteCustomSource = collection.suburb;

            txtFPostalAdd.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFPostalAdd.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFPostalAdd.AutoCompleteCustomSource = collection.homeaddress;

            //mother

            txtMOccupation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMOccupation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMOccupation.AutoCompleteCustomSource = collection.occupation;

            txtMnationality.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMnationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMnationality.AutoCompleteCustomSource = collection.nationality;

            txtMMainLang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMMainLang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMMainLang.AutoCompleteCustomSource = collection.langspoken;

            txtMNonschool.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMNonschool.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMNonschool.AutoCompleteCustomSource = collection.nonschooledu;

            txtMSchoolEdu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMSchoolEdu.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMSchoolEdu.AutoCompleteCustomSource = collection.schooledu;

            txtMAuRes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMAuRes.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMAuRes.AutoCompleteCustomSource = collection.AustralianResidence;

            txtMAuaborigine.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMAuaborigine.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMAuaborigine.AutoCompleteCustomSource = collection.aborigine;

            txtMOtherEng.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMOtherEng.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMOtherEng.AutoCompleteCustomSource = collection.langspoken;

            txtmHomeAdd.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtmHomeAdd.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtmHomeAdd.AutoCompleteCustomSource = collection.homeaddress;

            txtMHomeState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMHomeState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMHomeState.AutoCompleteCustomSource = collection.HomeState;

            txtMHomeCountry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMHomeCountry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMHomeCountry.AutoCompleteCustomSource = collection.homecountry;

            txtMSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMSuburb.AutoCompleteCustomSource = collection.suburb;

            txtMPostalState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMPostalState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMPostalState.AutoCompleteCustomSource = collection.HomeState;

            txtMPostalSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMPostalSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMPostalSuburb.AutoCompleteCustomSource = collection.suburb;

            txtMPostalAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMPostalAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMPostalAddress.AutoCompleteCustomSource = collection.homeaddress;

            //step father
            txtSFOccupation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFOccupation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFOccupation.AutoCompleteCustomSource = collection.occupation;

            txtSFNationality.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFNationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFNationality.AutoCompleteCustomSource = collection.nationality;

            txtSFOtherThanEnglish.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFOtherThanEnglish.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFOtherThanEnglish.AutoCompleteCustomSource = collection.langspoken;

            txtSFNonSchool.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFNonSchool.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFNonSchool.AutoCompleteCustomSource = collection.nonschooledu;

            txtSFSchoolEdu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFSchoolEdu.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFSchoolEdu.AutoCompleteCustomSource = collection.schooledu;

            txtSFAuRes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFAuRes.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFAuRes.AutoCompleteCustomSource = collection.AustralianResidence;

            txtSFAuAbor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFAuAbor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFAuAbor.AutoCompleteCustomSource = collection.aborigine;

            txtSFParentsMainLang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFParentsMainLang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFParentsMainLang.AutoCompleteCustomSource = collection.langspoken;

            txtFOtherThanEnglishSpoken.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFOtherThanEnglishSpoken.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFOtherThanEnglishSpoken.AutoCompleteCustomSource = collection.langspoken;

            txtSFHomeAdd.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFHomeAdd.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFHomeAdd.AutoCompleteCustomSource = collection.homeaddress;

            txtSFHomeState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFHomeState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFHomeState.AutoCompleteCustomSource = collection.HomeState;

            txtSFHomeCountry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFHomeCountry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFHomeCountry.AutoCompleteCustomSource = collection.homecountry;

            txtSFSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFSuburb.AutoCompleteCustomSource = collection.suburb;

            txtSFPostalState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFPostalState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFPostalState.AutoCompleteCustomSource = collection.HomeState;

            txtSFPostalSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFPostalSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFPostalSuburb.AutoCompleteCustomSource = collection.suburb;

            txtSFPostalAdd.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSFPostalAdd.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSFPostalAdd.AutoCompleteCustomSource = collection.homeaddress;

            //step mother


            txtSMOccu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMOccu.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMOccu.AutoCompleteCustomSource = collection.occupation;

            txtSMnationality.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMnationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMnationality.AutoCompleteCustomSource = collection.nationality;

            txtSMParentsMainLang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMParentsMainLang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMParentsMainLang.AutoCompleteCustomSource = collection.langspoken;

            txtSMNonSchool.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMNonSchool.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMNonSchool.AutoCompleteCustomSource = collection.nonschooledu;

            txtSMSchool.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMSchool.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMSchool.AutoCompleteCustomSource = collection.schooledu;

            txtSMAures.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMAures.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMAures.AutoCompleteCustomSource = collection.AustralianResidence;

            txtSMAuAborigine.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMAuAborigine.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMAuAborigine.AutoCompleteCustomSource = collection.aborigine;

            txtSMOtherThanEnglish.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMOtherThanEnglish.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMOtherThanEnglish.AutoCompleteCustomSource = collection.langspoken;

            txtSMHomeAdd.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMHomeAdd.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMHomeAdd.AutoCompleteCustomSource = collection.homeaddress;

            txtSMHomeState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMHomeState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMHomeState.AutoCompleteCustomSource = collection.HomeState;

            txtSMHomeCountry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMHomeCountry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMHomeCountry.AutoCompleteCustomSource = collection.homecountry;

            txtSMSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMSuburb.AutoCompleteCustomSource = collection.suburb;

            txtSMPostalState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMPostalState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMPostalState.AutoCompleteCustomSource = collection.HomeState;

            txtSMPostalSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMPostalSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMPostalSuburb.AutoCompleteCustomSource = collection.suburb;

            txtSMPostalAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSMPostalAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSMPostalAddress.AutoCompleteCustomSource = collection.homeaddress;

            //guardian

            txtGOccupation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGOccupation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGOccupation.AutoCompleteCustomSource = collection.occupation;

            txtGNationality.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGNationality.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGNationality.AutoCompleteCustomSource = collection.nationality;

            txtGMainLang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGMainLang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGMainLang.AutoCompleteCustomSource = collection.langspoken;

            txtGNoNSch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGNoNSch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGNoNSch.AutoCompleteCustomSource = collection.nonschooledu;

            txtGSchEdu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGSchEdu.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGSchEdu.AutoCompleteCustomSource = collection.schooledu;

            txtGAuRes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGAuRes.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGAuRes.AutoCompleteCustomSource = collection.AustralianResidence;

            txtGAuAborigine.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGAuAborigine.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGAuAborigine.AutoCompleteCustomSource = collection.aborigine;

            txtGHomeAdd.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGHomeAdd.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGHomeAdd.AutoCompleteCustomSource = collection.homeaddress;

            txtGHomeState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGHomeState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGHomeState.AutoCompleteCustomSource = collection.HomeState;

            txtGHomeCountry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGHomeCountry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGHomeCountry.AutoCompleteCustomSource = collection.homecountry;

            txtGSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGSuburb.AutoCompleteCustomSource = collection.suburb;

            txtGPostalState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGPostalState.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGPostalState.AutoCompleteCustomSource = collection.HomeState;

            txtGPostalSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGPostalSuburb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGPostalSuburb.AutoCompleteCustomSource = collection.suburb;

            txtGPostalAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGPostalAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGPostalAddress.AutoCompleteCustomSource = collection.homeaddress;
        }

        private MainMenu menuChoosed;

        public enum MouseEvent
        {
            Hover,
            Leave,
            Click
        }

        public void defaultMainMenuPosition()
        {
            showMenu();
            //location
            PanelRecordStudent1.Location = new Point(0, 0);
            PanelStudentDirectory2.Location = new Point(640, 0);
            PanelClassAssignment3.Location = new Point(0, 305);
            PanelEnquiry4.Location = new Point(640, 305);
            //size
            PanelRecordStudent1.Size = panelMainMenu; PanelStudentDirectory2.Size = panelMainMenu; PanelClassAssignment3.Size = panelMainMenu; PanelEnquiry4.Size = panelMainMenu;

        }

        //init panel size
        void PanelSize()
        {

        }

        void showMenu()
        {
            panelRecStud.Hide();
            panelStudDirectory.Hide();
            PanelRecordStudent1.Show();
            PanelStudentDirectory2.Show();
            PanelClassAssignment3.Show();
            PanelEnquiry4.Show();
            PanelRecordStudent1.BringToFront();
            PanelStudentDirectory2.BringToFront();
            PanelClassAssignment3.BringToFront();
            PanelEnquiry4.BringToFront();
        }

        void hideMenu()
        {
            PanelRecordStudent1.Hide();
            PanelStudentDirectory2.Hide();
            PanelClassAssignment3.Hide();
            PanelEnquiry4.Hide();
        }

        private void PanelRecordStudent_Click(object sender, EventArgs e)
        {
            //edit to recordstudent data
            UIState(UIStateEnum.RecordStudent);
        }

        public void MenuFocused(MainMenu menu)
        {
            int xDefault = 640;
            int yDefault = 305;
            int xMax = 848;
            int yMax = 422;
            int xMin = 0;
            int yMin = 188;
            menuChoosed = menu;
            switch (menuChoosed)
            {
                case MainMenu.RecordStudent:
                    PanelRecordStudent1.Size = new Size(xMax, yMax);
                    PanelStudentDirectory2.Size = new Size(433, 305);
                    PanelStudentDirectory2.Location = new Point(847, 0);
                    PanelEnquiry4.Size = new Size(433, 305);
                    PanelEnquiry4.Location = new Point(847, 305);
                    PanelClassAssignment3.Size = new Size(848, 189);
                    PanelClassAssignment3.Location = new Point(0, 421);
                    break;
                case MainMenu.StudentDirectory:
                    PanelRecordStudent1.Size = new Size(432, 305);
                    PanelStudentDirectory2.Size = new Size(xMax, yMax);
                    PanelStudentDirectory2.Location = new Point(432, 0);
                    PanelEnquiry4.Size = new Size(848, 189);
                    PanelEnquiry4.Location = new Point(432, 421);
                    PanelClassAssignment3.Size = new Size(432, 305);
                    PanelClassAssignment3.Location = new Point(0, 305);
                    break;
                case MainMenu.FutureFunction:
                    PanelRecordStudent1.Size = new Size(848, 188);
                    PanelStudentDirectory2.Size = new Size(433, 305);
                    PanelStudentDirectory2.Location = new Point(847, 0);
                    PanelEnquiry4.Size = new Size(433, 305);
                    PanelEnquiry4.Location = new Point(847, 305);
                    PanelClassAssignment3.Size = new Size(xMax, yMax);
                    PanelClassAssignment3.Location = new Point(0, 188);
                    break;
                case MainMenu.Enquiry:
                    PanelRecordStudent1.Size = new Size(433, 305);
                    PanelStudentDirectory2.Size = new Size(848, 188);
                    PanelStudentDirectory2.Location = new Point(433, 0);
                    PanelEnquiry4.Size = new Size(xMax, yMax);
                    PanelEnquiry4.Location = new Point(433, 188);
                    PanelClassAssignment3.Size = new Size(433, 305);
                    PanelClassAssignment3.Location = new Point(0, 305);
                    break;
                case MainMenu.def:
                    PanelRecordStudent1.Size = new Size(xDefault, yDefault);
                    PanelStudentDirectory2.Size = new Size(xDefault, yDefault);
                    PanelStudentDirectory2.Location = new Point(640, 0);
                    PanelEnquiry4.Size = new Size(xDefault, yDefault);
                    PanelEnquiry4.Location = new Point(640, 305);
                    PanelClassAssignment3.Size = new Size(xDefault, yDefault);
                    PanelClassAssignment3.Location = new Point(0, 305);
                    break;
            }
        }

        private void PanelRecordStudent_MouseEnter(object sender, EventArgs e)
        {
            MenuFocused(MainMenu.RecordStudent);
        }

        private void PanelStudentDirectory_MouseEnter(object sender, EventArgs e)
        {
            MenuFocused(MainMenu.StudentDirectory);

        }

        private void PanelEnquiry_MouseEnter(object sender, EventArgs e)
        {
            MenuFocused(MainMenu.Enquiry);

        }

        private void PanelFutureFunction_MouseEnter(object sender, EventArgs e)
        {
            MenuFocused(MainMenu.FutureFunction);

        }

        void init()
        {
            this.Size = defaultUcSize;
        }

        public enum RelationshipNavigation
        {
            Student
        }

        void SaveStud()
        {
            if (!StudIsSaved)
            {
                //save
                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO aisDb.student_data
(aisid,
nis,
ausid,
intake,
familyname,
givenname,
middlename,
certificatename,
dob,
pob,
cob,
gender,
religion,
nationality,
homeaddress,
homestate,
suburb,
postcode,
homecountry,
postaladdress,
postalstate,
postalsuburb,
postalcode,
postalcountry,
homephone,
mobilenumb,
faxnumb,
langspoken,
englishproficiency,
studstat,
studentimg,
currclass,
doc,
maker,
current_grade,
proposedgrade)
VALUES
(@aisid,
@nis,
@ausid,
@intake,
@familyname,
@givenname,
@middlename,
@certificatename,
@dob,
@pob,
@cob,
@gender,
@religion,
@nationality,
@homeaddress,
@homestate,
@suburb,
@postcode,
@homecountry,
@postaladdress,
@postalstate,
@postalsuburb,
@postalcode,
@postalcountry,
@homephone,
@mobilenumb,
@faxnumb,
@langspoken,
@englishproficiency,
@studstat,
@studentimg,
@currclass,
@doc,
@maker,
@current_grade,
@proposedgrade)", Db.get_connection());
                    cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = txtais.Text;
                    cmd.Parameters.Add("@nis", MySqlDbType.VarChar).Value = txtNisn.Text;
                    cmd.Parameters.Add("@ausid", MySqlDbType.VarChar).Value = txtAsn.Text;
                    cmd.Parameters.Add("@intake", MySqlDbType.Date).Value = dropIntake.Value.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@familyname", MySqlDbType.VarChar).Value = txtFamilyName.Text;
                    cmd.Parameters.Add("@givenname", MySqlDbType.VarChar).Value = txtGivenName.Text;
                    cmd.Parameters.Add("@middlename", MySqlDbType.VarChar).Value = txtMiddleName.Text;
                    cmd.Parameters.Add("@certificatename", MySqlDbType.VarChar).Value = txtCertificateName.Text;
                    cmd.Parameters.Add("@dob", MySqlDbType.Date).Value = dropDob.Value.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@pob", MySqlDbType.VarChar).Value = txtPlaceOfBirth.Text;
                    cmd.Parameters.Add("@cob", MySqlDbType.VarChar).Value = txtCountryOfBirth.Text;
                    cmd.Parameters.Add("@gender", MySqlDbType.VarChar).Value = gender;
                    cmd.Parameters.Add("@religion", MySqlDbType.VarChar).Value = dropReligion.SelectedValue.ToString();
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtNationality.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtHomeAddress.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtHomeState.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtPostCode.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtHomeCountry.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtPostalCountry.Text;
                    cmd.Parameters.Add("@homephone", MySqlDbType.VarChar).Value = txtHomePhone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtMobileNumber.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtFaxNumber.Text;
                    cmd.Parameters.Add("@langspoken", MySqlDbType.VarChar).Value = txtLangSpoken.Text;
                    cmd.Parameters.Add("@englishproficiency", MySqlDbType.VarChar).Value = dropSpokenEnglish.SelectedValue.ToString();
                    cmd.Parameters.Add("@studstat", MySqlDbType.VarChar).Value = dropStudStat.SelectedValue.ToString();
                    cmd.Parameters.Add("@studentimg", MySqlDbType.VarChar).Value = studentPhoto;
                    cmd.Parameters.Add("@currclass", MySqlDbType.VarChar).Value = 0;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    cmd.Parameters.Add("@current_grade", MySqlDbType.VarChar).Value = 0;
                    cmd.Parameters.Add("@proposedgrade", MySqlDbType.VarChar).Value = dropPropGrade.SelectedValue.ToString();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Student data saved!", frmAlert.AlertType.Info);
                        StudIsSaved = true;
                        txtais.Enabled = false;
                        ShowPanel(panelStud2);
                        aisid = Convert.ToInt32(txtais.Text);
                    }
                    else
                    {
                        Msg.Alert("Something is wrong \nBut we couldn't figure it out :(", frmAlert.AlertType.Error);
                    }
                    Db.close_connection();
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                //pullrevise command
                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand(@"UPDATE aisDb.student_data
SET
nis = @nis,
ausid = @ausid,
intake = @intake,
familyname = @familyname,
givenname = @givenname,
middlename = @middlename,
certificatename = @certificatename,
dob = @dob,
pob = @pob,
cob = @cob,
gender = @gender,
religion = @religion,
nationality = @nationality,
homeaddress = @homeaddress,
homestate = @homestate,
suburb = @suburb,
postcode = @postcode,
homecountry = @homecountry,
postaladdress = @postaladdress,
postalstate = @postalstate,
postalsuburb = @postalsuburb,
postalcode = @postalcode,
postalcountry = @postalcountry,
homephone = @homephone,
mobilenumb = @mobilenumb,
faxnumb = @faxnumb,
langspoken = @langspoken,
englishproficiency = @englishproficiency,
studstat = @studstat,
studentimg = @studentimg,
currclass = @currclass,
doc = @doc,
current_grade = @current_grade,
proposedgrade = @proposedgrade,
revised = @revised
WHERE aisid = @aisid", Db.get_connection());
                    cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                    cmd.Parameters.Add("@nis", MySqlDbType.VarChar).Value = txtNisn.Text;
                    cmd.Parameters.Add("@ausid", MySqlDbType.VarChar).Value = txtAsn.Text;
                    cmd.Parameters.Add("@intake", MySqlDbType.Date).Value = dropIntake.Value.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@familyname", MySqlDbType.VarChar).Value = txtFamilyName.Text;
                    cmd.Parameters.Add("@givenname", MySqlDbType.VarChar).Value = txtGivenName.Text;
                    cmd.Parameters.Add("@middlename", MySqlDbType.VarChar).Value = txtMiddleName.Text;
                    cmd.Parameters.Add("@certificatename", MySqlDbType.VarChar).Value = txtCertificateName.Text;
                    cmd.Parameters.Add("@dob", MySqlDbType.Date).Value = dropDob.Value.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@pob", MySqlDbType.VarChar).Value = txtPlaceOfBirth.Text;
                    cmd.Parameters.Add("@cob", MySqlDbType.VarChar).Value = txtCountryOfBirth.Text;
                    cmd.Parameters.Add("@gender", MySqlDbType.VarChar).Value = gender;
                    cmd.Parameters.Add("@religion", MySqlDbType.VarChar).Value = dropReligion.SelectedValue.ToString();
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtNationality.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtHomeAddress.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtHomeState.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtPostCode.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtHomeCountry.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtPostalCountry.Text;
                    cmd.Parameters.Add("@homephone", MySqlDbType.VarChar).Value = txtHomePhone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtMobileNumber.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtFaxNumber.Text;
                    cmd.Parameters.Add("@langspoken", MySqlDbType.VarChar).Value = txtLangSpoken.Text;
                    cmd.Parameters.Add("@englishproficiency", MySqlDbType.VarChar).Value = dropSpokenEnglish.SelectedValue.ToString();
                    cmd.Parameters.Add("@studstat", MySqlDbType.VarChar).Value = dropStudStat.SelectedValue.ToString();
                    cmd.Parameters.Add("@studentimg", MySqlDbType.VarChar).Value = studentPhoto;
                    cmd.Parameters.Add("@currclass", MySqlDbType.VarChar).Value = 0;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@revised", MySqlDbType.Int32).Value = Dashboard.ownerId;
                    cmd.Parameters.Add("@current_grade", MySqlDbType.VarChar).Value = 0;
                    cmd.Parameters.Add("@proposedgrade", MySqlDbType.VarChar).Value = dropPropGrade.SelectedValue.ToString();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Student data updated!", frmAlert.AlertType.Info);
                        StudIsSaved = true;
                        ShowPanel(panelStud2);
                        aisid = Convert.ToInt32(txtais.Text);
                    }
                    else
                    {
                        Msg.Alert("Something is wrong \nBut we couldn't figure it out :(", frmAlert.AlertType.Error);
                    }
                    Db.close_connection();
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {

            if (txtais.Text == "")
            {
                Msg.Alert("Oops, the student AISID is blank...", frmAlert.AlertType.Error);
            }
            else if (txtCertificateName.Text == "")
            {
                Msg.Alert("Oops, the student doesn't have a name.", frmAlert.AlertType.Error);
            }
            else if (txtCertificateName.Text == "")
            {
                Msg.Alert("Oops. the student doesn't have nationality.", frmAlert.AlertType.Error);
            }
            else
            {
                if (txtAsn.Text == "")
                {
                    txtAsn.Text = "0";
                }
                if (txtNisn.Text == "")
                {
                    txtNisn.Text = "0";
                }
                SaveStud();

            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (!StudIsSaved)
            {
                Msg.Alert("Oops, you haven't enter any student information\nWe don't know where to upload it :(", frmAlert.AlertType.Error);
            }
            else
            {

                opf.Filter = ("(*.JPG), (*.PNG), (*.PDF), (*.DOCX) | *.JPG;*.PNG;*.PDF;*.DOCX");
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    extension = System.IO.Path.GetExtension(opf.FileName);
                    docspath = opf.FileName.ToString();
                    txtDocsPath.Text = docspath;
                    docspathDb = @"\\192.168.30.100\SysInternal\Docs\StudDocs\" + aisid + docstype + docsname + extension;
                }
            }
        }

        private void btnUploadRecord_Click(object sender, EventArgs e)
        {
            if (txtDocsPath.Text == "")
            {
                Msg.Alert("Nothing to upload here :(", frmAlert.AlertType.Error);
            }
            else if (txtDocsName.Text == "")
            {
                Msg.Alert("You forgot to type document name", frmAlert.AlertType.Warning);
            }
            else
            {
                SaveDocs();
            }
        }

        void SaveDocs()
        {
            docsname = txtDocsName.Text + dropDocsType.SelectedValue.ToString();
            docstype = dropDocsType.SelectedValue.ToString();
            docspathDb = @"\\192.168.30.100\SysInternal\Docs\StudDocs\" + aisid + docstype + docsname + extension;

            btnUploadRecord.Enabled = false;
            label8.Visible = true;
            ProgressCopy.Visible = true;
            label33.Visible = true;
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO aisDb.document_students (docsname, docspath, maker, doc, docstype, docsdesc, owner_id) VALUES (@docsname, @docspath, @maker, @doc, @docstype, @docsdesc, @owner_id)", Db.get_connection());
                cmd.Parameters.Add("@docsname", MySqlDbType.VarChar).Value = docsname;
                cmd.Parameters.Add("@docspath", MySqlDbType.VarChar).Value = docspathDb;
                cmd.Parameters.Add("@maker", MySqlDbType.Int32).Value = Dashboard.ownerId;
                cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                cmd.Parameters.Add("@docstype", MySqlDbType.VarChar).Value = docstype;
                cmd.Parameters.Add("@docsdesc", MySqlDbType.LongText).Value = txtDocsDesc.Text;
                cmd.Parameters.Add("@owner_id", MySqlDbType.Int32).Value = aisid;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    try
                    {
                        worker.RunWorkerAsync();
                    }
                    catch (System.IO.IOException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void BtnAddRelat_Click(object sender, EventArgs e)
        {
            if (!addRelatIsClicked)
            {
                BtnAddRelat.FillColor = Color.LightCoral;
                panelAddRelationship.Show();
                panelAddRelationship.BringToFront();
                panelAddRelationship.Size = PanelAddRelationshipSize;
                panelAddRelationship.Location = PanelAddRelationshipLoc;
                addRelatIsClicked = true;
            }
            else
            {
                BtnAddRelat.FillColor = Color.Silver;
                panelAddRelationship.Hide();
                panelAddRelationship.Size = new Size(0, 0);
                panelAddRelationship.Location = new Point(0, 0);
                addRelatIsClicked = false;
            }

        }

        private void picBtnExit_Click(object sender, EventArgs e)
        {
            panelAddRelationship.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (RelationshipCount >= 4)
            {
                Msg.Alert("A Student can only have 4 or less relationship", frmAlert.AlertType.Info);
            }
            else
            {
                RelationshipCount++;
                switch (RelationshipCount)
                {
                    case 1:
                        entity1 = _entity.Father;
                        break;
                    case 2:
                        entity2 = _entity.Father;
                        break;
                    case 3:
                        entity3 = _entity.Father;
                        break;
                    case 4:
                        entity4 = _entity.Father;
                        break;
                }
                TopNavEntity(RelationshipCount, _entity.Father);
                panelAddRelationship.Hide();
                hidePanelRelatAdd();
                btnAddFather.Enabled = false;
            }
        }
        public enum _entity
        {
            Student,
            Father,
            Mother,
            StepFather,
            StepMother,
            Guardian,
            Sibling
        }
        private _entity entity;

        private _entity entity1;
        private _entity entity2;
        private _entity entity3;
        private _entity entity4;


        public enum _topNavEntity
        {
            Student,
            Relationship1,
            Relationship2,
            Relationship3,
            Relationship4
        }

        void BtnNavigate(Guna2Button btn)
        {
            BtnStud_.FillColor = Color.White;
            BtnStud_.ForeColor = Color.Black;
            btnRelat1.FillColor = Color.White;
            btnRelat1.ForeColor = Color.Black;
            btnRelat2.FillColor = Color.White;
            btnRelat2.ForeColor = Color.Black;
            btnRelat3.FillColor = Color.White;
            btnRelat3.ForeColor = Color.Black;
            btnRelat4.FillColor = Color.White;
            btnRelat4.ForeColor = Color.Black;
            //blink
            btn.FillColor = Color.Black;
            btn.ForeColor = Color.White;
        }

        void ShowPanel(Panel p)
        {
            panelStud1.Hide();
            panelStud2.Hide();
            PanelFather1.Hide();
            PanelFather2.Hide();
            PanelMother1.Hide();
            PanelMother2.Hide();
            PanelStepF1.Hide();
            PanelStepF2.Hide();
            PanelStepM1.Hide();
            PanelStepM2.Hide();
            PanelGuardian1.Hide();
            PanelGuardian2.Hide();
            PanelSibling.Hide();
            //show thingy
            p.Show();
            normalizePanelLeft(p);
        }

        private _topNavEntity topnav;
        public void TopNavEntity(int e, _entity e2)
        {
            entity = e2;
            RelationshipCount = e;
            switch (RelationshipCount)
            {
                case 1:
                    btnRelat1.Visible = true;
                    BtnNavigate(btnRelat1);
                    switch (entity)
                    {
                        case _entity.Father:
                            btnRelat1.Text = "Father";
                            ShowPanel(PanelFather1);
                            break;
                        case _entity.Mother:
                            btnRelat1.Text = "Mother";
                            ShowPanel(PanelMother1);
                            break;
                        case _entity.StepFather:
                            btnRelat1.Text = "Step Father";
                            ShowPanel(PanelStepF1);
                            break;
                        case _entity.StepMother:
                            btnRelat1.Text = "Step Mother";
                            ShowPanel(PanelStepM1);
                            break;
                        case _entity.Guardian:
                            btnRelat1.Text = "Guardian";
                            ShowPanel(PanelGuardian1);
                            break;
                        case _entity.Sibling:
                            btnRelat1.Text = "Sibling";
                            ShowPanel(PanelSibling);
                            break;
                    }
                    break;
                case 2:
                    btnRelat2.Visible = true;
                    BtnNavigate(btnRelat2);
                    switch (entity)
                    {
                        case _entity.Father:
                            btnRelat2.Text = "Father";
                            ShowPanel(PanelFather1);
                            break;
                        case _entity.Mother:
                            btnRelat2.Text = "Mother";
                            ShowPanel(PanelMother1);
                            break;
                        case _entity.StepFather:
                            btnRelat2.Text = "Step Father";
                            ShowPanel(PanelStepF1);
                            break;
                        case _entity.StepMother:
                            btnRelat2.Text = "Step Mother";
                            ShowPanel(PanelStepM1);
                            break;
                        case _entity.Guardian:
                            btnRelat2.Text = "Guardian";
                            ShowPanel(PanelGuardian1);
                            break;
                        case _entity.Sibling:
                            btnRelat2.Text = "Sibling";
                            ShowPanel(PanelSibling);
                            break;
                    }
                    break;
                case 3:
                    btnRelat3.Visible = true;
                    BtnNavigate(btnRelat3);
                    switch (entity)
                    {
                        case _entity.Father:
                            btnRelat3.Text = "Father";
                            ShowPanel(PanelFather1);
                            break;
                        case _entity.Mother:
                            btnRelat3.Text = "Mother";
                            ShowPanel(PanelMother1);
                            break;
                        case _entity.StepFather:
                            btnRelat3.Text = "Step Father";
                            ShowPanel(PanelStepF1);
                            break;
                        case _entity.StepMother:
                            btnRelat3.Text = "Step Mother";
                            ShowPanel(PanelStepM1);
                            break;
                        case _entity.Guardian:
                            btnRelat3.Text = "Guardian";
                            ShowPanel(PanelGuardian1);
                            break;
                        case _entity.Sibling:
                            btnRelat3.Text = "Sibling";
                            ShowPanel(PanelSibling);
                            break;
                    }
                    break;
                case 4:
                    btnRelat4.Visible = true;
                    BtnNavigate(btnRelat4);
                    switch (entity)
                    {
                        case _entity.Father:
                            btnRelat4.Text = "Father";
                            ShowPanel(PanelFather1);
                            break;
                        case _entity.Mother:
                            btnRelat4.Text = "Mother";
                            ShowPanel(PanelMother1);
                            break;
                        case _entity.StepFather:
                            btnRelat4.Text = "Step Father";
                            ShowPanel(PanelStepF1);
                            break;
                        case _entity.StepMother:
                            btnRelat4.Text = "Step Mother";
                            ShowPanel(PanelStepM1);
                            break;
                        case _entity.Guardian:
                            btnRelat4.Text = "Guardian";
                            ShowPanel(PanelGuardian1);
                            break;
                        case _entity.Sibling:
                            btnRelat4.Text = "Sibling";
                            ShowPanel(PanelSibling);
                            break;
                    }
                    break;
            }
        }

        private void BtnStud__Click(object sender, EventArgs e)
        {
            BtnNavigate(BtnStud_);
            ShowPanel(panelStud1);
        }

        private void btnRelat1_Click(object sender, EventArgs e)
        {
            TopNavEntity(1, entity1);
        }

        private void PanelRecordStudent_MouseLeave(object sender, EventArgs e)
        {

        }

        private void PanelStudentDirectory_MouseLeave(object sender, EventArgs e)
        {

        }

        public enum TopnavRight
        {
            Document,
            Medical,
            School
        }
        private TopnavRight _topnavRight;

        void topRightNav(TopnavRight t)
        {
            _topnavRight = t;
            switch (_topnavRight)
            {
                case TopnavRight.Document:
                    btnNavDocs.FillColor = Color.Black;
                    btnNavDocs.ForeColor = Color.White;
                    btnNavMedical.FillColor = Color.White;
                    btnNavMedical.ForeColor = Color.Black;
                    btnNavSchool.FillColor = Color.White;
                    btnNavSchool.ForeColor = Color.Black;
                    normalizePanelRight(PanelUploadDocsUp);
                    normalizePanelRightDown(PanelStudDocsDown);
                    PanelUploadDocsUp.Show();
                    PanelStudDocsDown.Show();
                    PanelMedicalUp.Hide();
                    PanelPrevSchoolinfoUp.Hide();
                    PanelPrevSchoolInfoDwn.Hide();
                    break;
                case TopnavRight.Medical:
                    btnNavDocs.FillColor = Color.White;
                    btnNavDocs.ForeColor = Color.Black;
                    btnNavMedical.FillColor = Color.Black;
                    btnNavMedical.ForeColor = Color.White;
                    btnNavSchool.FillColor = Color.White;
                    btnNavSchool.ForeColor = Color.Black;
                    normalizePanelSingleRight(PanelMedicalUp);
                    PanelPrevSchoolinfoUp.Hide(); PanelPrevSchoolInfoDwn.Hide();
                    PanelStudDocsDown.Hide();
                    PanelUploadDocsUp.Hide();
                    PanelMedicalUp.Show();
                    break;
                case TopnavRight.School:
                    btnNavDocs.FillColor = Color.White;
                    btnNavDocs.ForeColor = Color.Black;
                    btnNavMedical.FillColor = Color.White;
                    btnNavMedical.ForeColor = Color.Black;
                    btnNavSchool.FillColor = Color.Black;
                    btnNavSchool.ForeColor = Color.White;
                    PanelStudDocsDown.Hide();
                    PanelUploadDocsUp.Hide();
                    PanelMedicalUp.Hide();
                    PanelPrevSchoolInfoDwn.Show(); PanelPrevSchoolinfoUp.Show();
                    normalizePanelRight(PanelPrevSchoolinfoUp);
                    normalizePanelRightDown(PanelPrevSchoolInfoDwn);
                    break;
                default:
                    btnNavDocs.FillColor = Color.Black;
                    btnNavDocs.ForeColor = Color.White;
                    btnNavMedical.FillColor = Color.White;
                    btnNavMedical.ForeColor = Color.Black;
                    btnNavSchool.FillColor = Color.White;
                    btnNavSchool.ForeColor = Color.Black;
                    normalizePanelRight(PanelUploadDocsUp);
                    normalizePanelRightDown(PanelStudDocsDown);
                    PanelUploadDocsUp.Show();
                    PanelStudDocsDown.Show();
                    PanelMedicalUp.Hide();
                    PanelPrevSchoolinfoUp.Hide();
                    PanelPrevSchoolInfoDwn.Hide();
                    break;
            }
        }

        private void btnNavSchool_Click(object sender, EventArgs e)
        {
            topRightNav(TopnavRight.School);
        }

        private void btnNavMedical_Click(object sender, EventArgs e)
        {
            topRightNav(TopnavRight.Medical);

        }

        private void btnNavDocs_Click(object sender, EventArgs e)
        {
            topRightNav(TopnavRight.Document);

        }

        private void btnRelat2_Click(object sender, EventArgs e)
        {
            TopNavEntity(2, entity2);
        }

        private void btnRelat3_Click(object sender, EventArgs e)
        {
            TopNavEntity(3, entity3);

        }

        void PhotoHandlerParents()
        {

        }

        private void picFather_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = ("Please Compress the file before uploading! (*.JPG), (*.PNG), (*.JPEG) | *.JPG; *.PNG; *.JPEG");
            if (opf.ShowDialog() == DialogResult.OK)
            {
                picFather.Image = Image.FromFile(opf.FileName);
                string photopath = opf.FileName;
                string extension = System.IO.Path.GetExtension(opf.FileName);
                string copyToDestination = @"\\192.168.30.100\SysInternal\Img\ParentsPhoto\" + aisid + "father" + extension;
                fatherPhoto = copyToDestination;
                try
                {
                    File.Copy(photopath, copyToDestination, true);
                }
                catch (IOException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                Msg.Alert("No changes were made father", frmAlert.AlertType.Warning);
            }
            opf.Dispose();
        }

        private void btnSaveFather_Click(object sender, EventArgs e)
        {
            SaveFather();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ShowPanel(PanelFather1);
        }

        private void radSibMale_CheckedChanged(object sender, EventArgs e)
        {
            siblingGender = "Male";
        }

        private void radSibFemale_CheckedChanged(object sender, EventArgs e)
        {
            siblingGender = "Female";

        }

        private void btnRelat4_Click(object sender, EventArgs e)
        {
            TopNavEntity(4, entity4);
        }

        void hidePanelRelatAdd()
        {
            BtnAddRelat.FillColor = Color.Silver;
            panelAddRelationship.Hide();
            panelAddRelationship.Size = new Size(0, 0);
            panelAddRelationship.Location = new Point(0, 0);
            addRelatIsClicked = false;
        }
        private void btnAddMother_Click(object sender, EventArgs e)
        {
            if (RelationshipCount >= 4)
            {
                Msg.Alert("A Student can only have 4 or less relationship", frmAlert.AlertType.Info);
            }
            else
            {
                RelationshipCount++;
                switch (RelationshipCount)
                {
                    case 1:
                        entity1 = _entity.Mother;
                        break;
                    case 2:
                        entity2 = _entity.Mother;
                        break;
                    case 3:
                        entity3 = _entity.Mother;
                        break;
                    case 4:
                        entity4 = _entity.Mother;
                        break;
                }
                TopNavEntity(RelationshipCount, _entity.Mother);
                panelAddRelationship.Hide();
                hidePanelRelatAdd();
                btnAddMother.Enabled = false;
            }
        }

        private void btnMnext_Click(object sender, EventArgs e)
        {
            if (!StudIsSaved)
            {
                Msg.Alert("Please save student information first!", frmAlert.AlertType.Info);
            }
            else
            {
                saveMother();
                ShowPanel(PanelMother2);
            }
        }

        private void PicStepMother_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = ("Please Compress the file before uploading! (*.JPG), (*.PNG), (*.JPEG) | *.JPG; *.PNG; *.JPEG");
            if (opf.ShowDialog() == DialogResult.OK)
            {
                PicStepMother.Image = Image.FromFile(opf.FileName);
                string photopath = opf.FileName;
                string extension = System.IO.Path.GetExtension(opf.FileName);
                string copyToDestination = @"\\192.168.30.100\SysInternal\Img\ParentsPhoto\" + aisid + "StepMother" + extension;
                stepMotherPhoto = copyToDestination;
                try
                {
                    File.Copy(photopath, copyToDestination, true);
                }
                catch (IOException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                Msg.Alert("No changes were made stepmother", frmAlert.AlertType.Warning);
            }
            opf.Dispose();
        }

        private void btnSMSave_Click(object sender, EventArgs e)
        {
            SaveStepMother();
        }

        private void btntBackSm_Click(object sender, EventArgs e)
        {
            ShowPanel(PanelStepM1);
        }

        private void btnGNext_Click(object sender, EventArgs e)
        {
            if (!StudIsSaved)
            {
                Msg.Alert("Please save the student information first!", frmAlert.AlertType.Warning);
            }
            else
            {
                SaveGuardian();
                ShowPanel(PanelGuardian2);
            }
        }

        private void PicGuardian_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = ("Please Compress the file before uploading! (*.JPG), (*.PNG), (*.JPEG) | *.JPG; *.PNG; *.JPEG");
            if (opf.ShowDialog() == DialogResult.OK)
            {
                PicGuardian.Image = Image.FromFile(opf.FileName);
                string photopath = opf.FileName;
                string extension = System.IO.Path.GetExtension(opf.FileName);
                string copyToDestination = @"\\192.168.30.100\SysInternal\Img\ParentsPhoto\" + aisid + "guardian" + extension;
                guardianPhoto = copyToDestination;
                try
                {
                    File.Copy(photopath, copyToDestination, true);
                }
                catch (IOException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                Msg.Alert("No changes were made guardian", frmAlert.AlertType.Warning);
            }
            opf.Dispose();
        }

        private void btnGSave_Click(object sender, EventArgs e)
        {
            SaveGuardian();
        }

        private void btnGBack_Click(object sender, EventArgs e)
        {
            ShowPanel(PanelGuardian1);
        }

        private void picStepFather_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = ("Please Compress the file before uploading! (*.JPG), (*.PNG), (*.JPEG) | *.JPG; *.PNG; *.JPEG");
            if (opf.ShowDialog() == DialogResult.OK)
            {
                picStepFather.Image = Image.FromFile(opf.FileName);
                string photopath = opf.FileName;
                string extension = System.IO.Path.GetExtension(opf.FileName);
                string copyToDestination = @"\\192.168.30.100\SysInternal\Img\StudentPhoto\" + aisid + "StepFather" + extension;
                stepFatherPhoto = copyToDestination;
                try
                {
                    File.Copy(photopath, copyToDestination, true);
                }
                catch (IOException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                Msg.Alert("No changes were made stepfather", frmAlert.AlertType.Warning);
            }
            opf.Dispose();
        }

        private void btnSaveStepFather_Click(object sender, EventArgs e)
        {
            SaveStepFather();
        }

        private void btnSFNext_Click(object sender, EventArgs e)
        {
            if (!StudIsSaved)
            {
                Msg.Alert("Save student information first!", frmAlert.AlertType.Warning);
            }
            else
            {
                SaveStepFather();
                ShowPanel(PanelStepF2);
            }
        }

        private void txtAisid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtNisn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtAsn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtPostCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtFaxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtFHomeCountry_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtFPostCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtFMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtFWhatsapp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtFFaxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtFPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtFHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtMPostcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtMMobileNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtMWhatsapp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtMFaxNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void btnSMNext_Click(object sender, EventArgs e)
        {
            if (!StudIsSaved)
            {
                Msg.Alert("Please save student information first", frmAlert.AlertType.Warning);
            }
            else
            {
                SaveStepMother();
                ShowPanel(PanelStepM2);
            }
        }

        private void txtSMPostCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSMMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSMWhatsapp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSMFaxNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSFPostCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSFMobileNumb_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtSFMobileNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSFWhatsappNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSFFaxNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSMPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSMHomephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtGPostCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtGMobileNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtGWhatsapp_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtGEmailAdd_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtGFaxNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtGMainLang_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtGPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtGHomePhoneNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSFPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtSFHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtMPostalcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void txtMHomePhoneNumb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void btnSiblingAddData_Click(object sender, EventArgs e)
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO aisDb.stud_sibling_info (siblingofstud, siblingfname, siblinglname, currentschoolsibling, dob, gender, doc, maker) values (@siblingofstud, @siblingfname, @siblinglname, @currentschoolsibling, @dob, @gender, @doc, @maker)", Db.get_connection());
                cmd.Parameters.Add("@siblingofstud", MySqlDbType.Int32).Value = aisid;
                cmd.Parameters.Add("@siblingfname", MySqlDbType.VarChar).Value = txtSiblingFirstname.Text;
                cmd.Parameters.Add("@siblinglname", MySqlDbType.VarChar).Value = txtSiblinglastname.Text;
                cmd.Parameters.Add("@currentschoolsibling", MySqlDbType.VarChar).Value = txtCurrSchoolSiblingAttend.Text;
                cmd.Parameters.Add("@dob", MySqlDbType.Date).Value = dropDobSibling.Value.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@gender", MySqlDbType.VarChar).Value = siblingGender;
                cmd.Parameters.Add("@doc", MySqlDbType.Timestamp).Value = DateTime.Now.ToString(timeStamping);
                cmd.Parameters.Add("@maker", MySqlDbType.Int32).Value = Dashboard.ownerId;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    siblingHandler();

                }
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        void siblingHandler()
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select id, concat(ifnull(siblingfname, ''), ' ', ifnull(siblinglname, '')) as 'Sibling Fullname', currentschoolsibling as 'Current School', dob as 'Date of Birth', gender as 'Gender' from stud_sibling_info where siblingofstud = @aisid", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dt;
                dgSibling.DataSource = bindingSource;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void chkmedsNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkmedsNo.Checked)
            {
                chkMedsyes.Checked = false;
                medsdose = "No Details";
                chkmedsNo.Checked = true;
            }
        }

        private void chkMedsyes_CheckedChanged(object sender, EventArgs e)
        {
            medsdose = "No Details";
            chkmedsNo.Checked = false;
        }

        void SaveMeds()
        {
            string medsdetails, allergies, medication, regularmedication, regularmedsdetails;
            if (!StudIsSaved)
            {
                Msg.Alert("Please save student information first!", frmAlert.AlertType.Error);
            }
            else
            {
                try
                {
                    if (!medsIsSaved)
                    {
                        //insert
                        try
                        {
                            Db.open_connection();

                            MySqlCommand cmd = new MySqlCommand(@"INSERT INTO aisDb.stud_medical_info
(medicalofstud,
healthcondition,
allergies,
medication_for_allergies,
regularmedication,
regularmedicationdetails,
maker, doc)
VALUES
(@medicalofstud,
@healthcondition,
@allergies,
@medication_for_allergies,
@regularmedication,
@regularmedicationdetails,
@maker,
@doc);

", Db.get_connection());
                            if (txtMedsDetails.Text == "")
                            {
                                medsdetails = "No serious Health History";
                            }
                            else
                            {
                                medsdetails = txtMedsDetails.Text;
                            }
                            if (txtMEdsAllergies.Text == "")
                            {
                                allergies = "No allergies";
                            }
                            else
                            {
                                allergies = txtMEdsAllergies.Text;
                            }
                            if (txtMedsMedsAller.Text == "")
                            {
                                medication = "No meds needed";
                            }
                            else
                            {
                                medication = txtMedsMedsAller.Text;
                            }
                            if (chkMedsyes.Checked)
                            {
                                regularmedication = "YES";
                            }
                            else
                            {
                                regularmedication = "NO";
                            }
                            if (txtMedsDose.Text == "")
                            {
                                regularmedsdetails = "No Details";
                            }
                            else
                            {
                                regularmedsdetails = txtMedsDose.Text; ;

                            }
                            cmd.Parameters.Add("@medicalofstud", MySqlDbType.Int32).Value = aisid;
                            cmd.Parameters.Add("@healthcondition", MySqlDbType.VarChar).Value = medsdetails;
                            cmd.Parameters.Add("@allergies", MySqlDbType.VarChar).Value = allergies;
                            cmd.Parameters.Add("@medication_for_allergies", MySqlDbType.VarChar).Value = medication;
                            cmd.Parameters.Add("@regularmedication", MySqlDbType.VarChar).Value = regularmedication;
                            cmd.Parameters.Add("@regularmedicationdetails", MySqlDbType.VarChar).Value = regularmedsdetails;
                            cmd.Parameters.Add("@maker", MySqlDbType.Int32).Value = Dashboard.ownerId;
                            cmd.Parameters.Add("@doc", MySqlDbType.Timestamp).Value = DateTime.Now.ToString(timeStamping);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                Msg.Alert("Medication info recorded succesfully!", frmAlert.AlertType.Success);
                            }
                            else
                            {
                                Msg.Alert("Something is wrong", frmAlert.AlertType.Success);

                            }
                            Db.close_connection();
                        }
                        catch (MySqlException ex)
                        {
                            Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                            throw;
                        }
                    }
                    else
                    {
                        //revise
                        try
                        {
                            Db.open_connection();
                            MySqlCommand cmd = new MySqlCommand(@"UPDATE aisDb.stud_medical_info
SET
healthcondition = @healthcondition,
allergies = @allergies,
medication_for_allergies = @medication_for_allergies,
regularmedication = @regularmedication,
regularmedicationdetails = @regularmedicationdetails,
doc = @doc,
revised = @revised
WHERE medicalofstud = @medicalofstud;
", Db.get_connection());
                            if (txtMedsDetails.Text == "")
                            {
                                medsdetails = "No serious Health History";
                            }
                            else
                            {
                                medsdetails = txtMedsDetails.Text;
                            }
                            if (txtMEdsAllergies.Text == "")
                            {
                                allergies = "No allergies";
                            }
                            else
                            {
                                allergies = txtMEdsAllergies.Text;
                            }
                            if (txtMedsMedsAller.Text == "")
                            {
                                medication = "No meds needed";
                            }
                            else
                            {
                                medication = txtMedsMedsAller.Text;
                            }
                            if (chkMedsyes.Checked)
                            {
                                regularmedication = "YES";
                            }
                            else
                            {
                                regularmedication = "NO";
                            }
                            if (txtMedsDose.Text == "")
                            {
                                regularmedsdetails = "No Details";
                            }
                            else
                            {
                                regularmedsdetails = txtMedsDose.Text; ;

                            }
                            cmd.Parameters.Add("@medicalofstud", MySqlDbType.Int32).Value = aisid;
                            cmd.Parameters.Add("@healthcondition", MySqlDbType.VarChar).Value = medsdetails;
                            cmd.Parameters.Add("@allergies", MySqlDbType.VarChar).Value = allergies;
                            cmd.Parameters.Add("@medication_for_allergies", MySqlDbType.VarChar).Value = medication;
                            cmd.Parameters.Add("@regularmedication", MySqlDbType.VarChar).Value = regularmedication;
                            cmd.Parameters.Add("@regularmedicationdetails", MySqlDbType.VarChar).Value = regularmedsdetails;
                            cmd.Parameters.Add("@revised", MySqlDbType.Int32).Value = Dashboard.ownerId;
                            cmd.Parameters.Add("@doc", MySqlDbType.Timestamp).Value = DateTime.Now.ToString(timeStamping);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                Msg.Alert("Medication info recorded succesfully!", frmAlert.AlertType.Success);
                            }
                            else
                            {
                                Msg.Alert("Something is wrong", frmAlert.AlertType.Success);

                            }
                            Db.close_connection();
                        }
                        catch (MySqlException ex)
                        {
                            Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }

        }

        private void btnPrevSchDel_Click(object sender, EventArgs e)
        {
            if (dgSchoolInfo.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgSchoolInfo.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgSchoolInfo.Rows[selectedrowindex];
                string schoolchoosed = Convert.ToString(selectedRow.Cells["ID"].Value);
                schoolId = Convert.ToInt32(schoolchoosed);
            }
            deleteSchool();
        }

        void deleteSchool()
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("delete from student_previous_school_info where id = @id", Db.get_connection());
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = schoolId;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    Msg.Alert("Selected school info deleted succesfully", frmAlert.AlertType.Success);
                    PrevSchoolHandler();
                }
                else
                {
                    Msg.Alert("Failed to delete data", frmAlert.AlertType.Error);
                }
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void btnAddInfoSchool_Click(object sender, EventArgs e)
        {
            InsertSchool();
        }

        void InsertSchool()
        {
            string extraSupp = "NO";
            if (chkExtraSuppYes.Checked)
            {
                extraSupp = "YES";
            }
            else
            {
                extraSupp = "NO";
            }
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("insert into student_previous_school_info (of_student, name_of_school, country, grade, dateattended, language_of_instruction, extra_support, curriculum) values (@of_student, @name_of_school, @country, @grade, @dateattended, @language_of_instruction, @extra_support, @curriculum)", Db.get_connection());
                cmd.Parameters.Add("@of_student", MySqlDbType.Int32).Value = aisid;
                cmd.Parameters.Add("@name_of_school", MySqlDbType.VarChar).Value = txtNameOfSchool.Text;
                cmd.Parameters.Add("@country", MySqlDbType.VarChar).Value = txtSchoolCountry.Text;
                cmd.Parameters.Add("@grade", MySqlDbType.VarChar).Value = dropGradeSchool.SelectedValue.ToString();
                cmd.Parameters.Add("@dateattended", MySqlDbType.VarChar).Value = txtFromTo.Text;
                cmd.Parameters.Add("@language_of_instruction", MySqlDbType.VarChar).Value = txtLangOfInstruction.Text;
                cmd.Parameters.Add("@extra_support", MySqlDbType.VarChar).Value = extraSupp;
                cmd.Parameters.Add("@curriculum", MySqlDbType.VarChar).Value = txtCurriculum.Text;
                if (cmd.ExecuteNonQuery() == 1)
                {
                    Msg.Alert("School Info Added!", frmAlert.AlertType.Success);
                    PrevSchoolHandler();
                }
                else
                {
                    Msg.Alert("Failed to add school info", frmAlert.AlertType.Error);
                }
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void chkExtraSupportNo_CheckedChanged(object sender, EventArgs e)
        {
            chkExtraSuppYes.Checked = false;
        }

        private void chkExtraSuppYes_CheckedChanged(object sender, EventArgs e)
        {
            chkExtraSupportNo.Checked = false;
        }

        private void PanelRecordStudent1_Paint(object sender, PaintEventArgs e)
        {

        }

        void readDocs()
        {
            try
            {
                Db.open_connection();
                try
                {
                    if (dgDocs.SelectedCells.Count > 0)
                    {
                        int selectedrowindex = dgDocs.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgDocs.Rows[selectedrowindex];
                        string selectedDocs = Convert.ToString(selectedRow.Cells["id"].Value);
                        docsSelected = Convert.ToInt32(selectedDocs);
                    }
                    try
                    {
                        Db.open_connection();
                        MySqlCommand cmd = new MySqlCommand("select * FROM aisDb.document_students WHERE id_Docs = @iddocs", Db.get_connection());
                        cmd.Parameters.Add("iddocs", MySqlDbType.Int32).Value = docsSelected;
                        MySqlDataReader readerdocs = cmd.ExecuteReader();
                        while (readerdocs.Read())
                        {
                            docspath = readerdocs.GetString("docspath");
                        }
                        readerdocs.Dispose();
                        try
                        {
                            System.Diagnostics.Process.Start(docspath);

                        }
                        catch (System.InvalidOperationException ex)
                        {
                            Msg.Alert("There is nothing to open!", frmAlert.AlertType.Error); ;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch (System.NullReferenceException ex)
                {
                    Msg.Alert("There is nothing to open!", frmAlert.AlertType.Error); ;

                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void btnOpenDocs_Click(object sender, EventArgs e)
        {
            readDocs();
        }

        private void btnDeleteDocs_Click(object sender, EventArgs e)
        {
            if (dgDocs.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgDocs.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgDocs.Rows[selectedrowindex];
                string selectedDocs = Convert.ToString(selectedRow.Cells["ID"].Value);
                docsSelected = Convert.ToInt32(selectedDocs);
            }
            deleteDocs();
        }

        void loadListDocs()
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmdDocs = new MySqlCommand("select id_Docs as 'ID', docsname as 'Document Name', doc as 'Date of Creation', docstype as 'Document Type' from document_students where owner_id = @owner", Db.get_connection());
                cmdDocs.Parameters.Add("@owner", MySqlDbType.Int32).Value = aisid;
                MySqlDataAdapter da = new MySqlDataAdapter(cmdDocs);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dt;
                dgDocs.DataSource = bindingSource;
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }

        }

        private void panelRecStud_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelStudentDirectory2_Click(object sender, EventArgs e)
        {
            UIState(UIStateEnum.StudentDirectory);

        }

        StringCollection strgender = new StringCollection() { "Male", "Female" };
        StringCollection strorigin = new StringCollection() { "Local", "Foreigner" };
        StringCollection strrevised = new StringCollection() { "Revised", "Not Revised" };

        private void guna2RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            _searcby = Searchby.Name;
            DoSearchControl();
        }

        void DoSearchControl()
        {
            switch (_searcby)
            {
                case Searchby.Name:
                    txtSearchByName.Clear();
                    txtSearchByName.Visible = true;
                    CmbSearch.Visible = false;
                    break;
                case Searchby.ID:
                    txtSearchByName.Visible = true;
                    CmbSearch.Visible = false;
                    break;
                case Searchby.Gender:
                    CmbSearch.DataSource = strgender;
                    CmbSearch.Visible = true;
                    CmbSearch.SelectedIndex = 0;
                    txtSearchByName.Visible = false;
                    break;
                case Searchby.Origin:
                    CmbSearch.DataSource = strorigin;
                    CmbSearch.Visible = true;
                    txtSearchByName.Visible = false;
                    CmbSearch.SelectedIndex = 0;
                    break;
                case Searchby.Revised:
                    CmbSearch.DataSource = strrevised;
                    CmbSearch.Visible = true;
                    txtSearchByName.Visible = false;
                    CmbSearch.SelectedIndex = 0;
                    break;
                default:

                    break;
            }
        }

        void DoSearch()
        {
            switch (_searcby)
            {
                case Searchby.Name:
                    SearchQuery(txtSearchByName.Text);
                    break;
                case Searchby.ID:
                    SearchQuery(txtSearchByName.Text);
                    break;
                case Searchby.Gender:
                    SearchQuery(CmbSearch.SelectedValue.ToString());
                    break;
                case Searchby.Origin:
                    SearchQuery(CmbSearch.SelectedValue.ToString());
                    break;
                case Searchby.Revised:
                    SearchQuery(CmbSearch.SelectedValue.ToString());
                    break;
            }
        }

        void rightPanelDataReader()
        {
            txtBriefStudName.Clear();
            txtBriefBirthdate.Clear();
            txtBriefNationality.Clear();
            txtStudentAddress.Clear();
            txtCurrentClass.Clear();
            txtFatherContact.Clear();
            txtMotherContact.Clear();
            DateTime birthdate;
            DateTime intake;
            DateTime doc;
            int reviser = 0;
            try
            {
                //student
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select aisid, certificatename, dob, intake, nationality, homeaddress, maker, revised, doc, studentimg, currclass, classname, employeeid, emp_fullname from student_data join employee_data on student_data.maker = employee_data.employeeid join class on student_data.currclass = class.class_id where aisid = @aisid", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txtAisidBrief.Text = "AIS ID: " + reader.GetString("aisid");
                    txtBriefStudName.Text = reader.GetString("certificatename");
                    birthdate = reader.GetDateTime("dob");
                    intake = reader.GetDateTime("intake");
                    doc = reader.GetDateTime("doc");
                    lblLastModOn.Text = "Last Modified: " + doc.ToString("d");
                    lblStudIntake.Text = "Student Intake: " + intake.ToString("d");
                    txtBriefBirthdate.Text = birthdate.ToString("D");
                    txtBriefNationality.Text = reader.GetString("nationality");
                    lblstudCreatedBy.Text = "Data Created by: " + reader.GetString("emp_fullname");
                    txtStudentAddress.Text = reader.GetString("homeaddress");
                    try
                    {
                        reviser = reader.GetInt32("revised");

                    }
                    catch (SqlNullValueException)
                    {
                        reviser = 0;
                    }
                    txtCurrentClass.Text = reader.GetString("classname");
                    try
                    {
                        studentPhoto = reader.GetString("studentimg");
                        picStudBrief.Image = Image.FromFile(reader.GetString("studentimg"));
                    }
                    catch (Exception)
                    {
                        picStudBrief.Image = Resources.icons8_student_male_80px;
                        studentPhoto = null;
                    }
                }
                reader.Close();

                //father mobile
                cmd = new MySqlCommand("select mobilenumb from stud_relationship where relationshiptostud = @aisid and relationship = 'Father'", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtFatherContact.Text = reader.GetString("mobilenumb");
                    }
                }
                reader.Close();
                reader.Dispose();

                //mother mobile
                cmd = new MySqlCommand("select mobilenumb from stud_relationship where relationshiptostud = @aisid and relationship = 'Mother'", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtMotherContact.Text = reader.GetString("mobilenumb");
                    }
                }
                reader.Close();
                reader.Dispose();
                //revise
                if (reviser == 0)
                {
                    lblStudRevisedBy.Text = "Data Revised by: Has never been revised";
                }
                else
                {
                    cmd = new MySqlCommand("select emp_fullname from employee_data where employeeid = @reviser", Db.get_connection());
                    cmd.Parameters.Add("@reviser", MySqlDbType.Int32).Value = reviser;
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lblStudRevisedBy.Text = "Data Revised by: " + reader.GetString("emp_fullname");
                        }
                    }
                    reader.Close();
                }
            }

            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
            Db.close_connection();
            btnDCExpand.Visible = true;
            if(aisid != null || aisid != 0)
            {
                StudentDataCompletionLoad();

            }
        }

        void StudentDataCompletionLoad()
        {
            string lblformstat = "You have empty form on field: ";
            string certname = null, religion = null, nationality = null, nat = "", homedaddress = null, homestate = null, suburb = null, 
                postcode = null, homecountry = null, homephone = null, mobilenumb = null;
            bool certnameIsThere = false, religionIsThere = false, nationalityIsThere = false, homeaddressIsThere = false, homestateIsThere = false,
                suburbIsThere = false, postcodeIsthere = false, homecountryIsThere = false, homephoneIsThere = false, mobilenumbIsThere = false, 
                formDC = false, ktp = false, kitas = false, studphotoDC = false, parentsphotoDC = false, passportDC = false;
            try
            {
                //student form
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select certificatename, religion, nationality, homeaddress, homestate, suburb, postcode, homecountry, homephone, mobilenumb from student_data where aisid = @aisid", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    certname = reader.GetString("certificatename");
                    religion = reader.GetString("religion");
                    nationality = reader.GetString("nationality");
                    nat = reader.GetString("nationality");
                    homedaddress = reader.GetString("homeaddress");
                    homestate = reader.GetString("homestate");
                    suburb = reader.GetString("suburb");
                    postcode = reader.GetString("postcode");
                    homecountry = reader.GetString("homecountry");
                    homephone = reader.GetString("homephone");
                    if(homephone == "" | homephone == null)
                    {
                        homephone = reader.GetString("mobilenumb");
                    }
                    mobilenumb = reader.GetString("mobilenumb");
                }
                reader.Close();
                //validation
                if(certname == "" || certname == null)
                {
                    certname = "|Certificate Name| ";
                    certnameIsThere = false;
                }
                else
                {
                    certname = string.Empty;
                    certnameIsThere = true;
                }
                if(religion == "" || religion == null)
                {
                    religion = "|Religion| ";
                    religionIsThere = false;
                }
                else
                {
                    religionIsThere = true;
                    religion = string.Empty;
                }
                if(nationality == "" || nationality == null)
                {
                    nationality = "|Nationality| ";
                    nationalityIsThere = false;
                }
                else
                {
                    nationality = string.Empty;
                    nationalityIsThere = true;
                }
                if(homedaddress == "" || homedaddress == null)
                {
                    homedaddress = "|Home Address| ";
                    homeaddressIsThere = false;
                }
                else
                {
                    homedaddress = string.Empty;
                    homeaddressIsThere = true;
                }
                if(homestate == "" || homestate == null)
                {
                    homestate = "|Home State| ";
                    homestateIsThere = false;
                }
                else
                {
                    homestate = string.Empty;
                    homestateIsThere = true;
                }
                if(suburb == "" || suburb == null)
                {
                    suburb = "|Suburb| ";
                    suburbIsThere = false;
                }
                else
                {
                    suburb = string.Empty;
                    suburbIsThere = true;
                }
                if(postcode==""||postcode==null)
                {
                    postcode = "|Post Code| ";
                    postcodeIsthere = false;
                }
                else
                {
                    postcode = string.Empty;
                    postcodeIsthere = true;
                }
                if(homecountry==""||homecountry==null)
                {
                    homecountry = "|Home Country| ";
                    homecountryIsThere = false;
                }
                else
                {
                    homecountryIsThere = true;
                    homecountry = string.Empty;
                }
                if(homephone==""||homephone==null)
                {
                    homephone = "|Home Phone| ";
                    homephoneIsThere = false;
                }
                else
                {
                    homephone = string.Empty;
                    homephoneIsThere = true;
                }
                if(mobilenumb==""||mobilenumb==null)
                {
                    mobilenumb = "|Mobile Number| ";
                    mobilenumbIsThere = false;
                }
                else
                {
                    mobilenumb = string.Empty;
                    mobilenumbIsThere = true;
                }
                //studentform
                if (!certnameIsThere || !religionIsThere || !nationalityIsThere || !homeaddressIsThere || !homestateIsThere || !suburbIsThere || !postcodeIsthere || !homecountryIsThere || !homephoneIsThere || !mobilenumbIsThere)
                {
                    btnDCStudForm.Visible = true;
                    lblDCstudformstat.Text = "Data not complete!";
                    formDC = false;
                    DCPIcformstat.Image = Resources.icons8_close_window_96;
                    lblExpform = "The following field(s) is empty:\n" + certname + religion + nationality + homedaddress + homestate + suburb + postcode + homecountry + homephone + mobilenumb;
                }
                else
                {
                    formDC = true;
                    DCPIcformstat.Image = Resources.icons8_Check_Mark_48px_1;
                    lblDCstudformstat.Text = "All required form is completed!";
                    btnDCStudForm.Visible = false;
                }
                //parents ktp
                if (nat.Contains("Indonesia") || nat.Contains("Indonesian"))
                {
                    cmd = new MySqlCommand("select docsname, docstype from document_students where owner_id = @owner and docstype = 'Photocopy Parents ID (KTP)'", Db.get_connection());
                    cmd.Parameters.Add("@owner", MySqlDbType.Int32).Value = aisid;
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataTable dat = new DataTable();
                    dataAdapter.Fill(dat);
                    if (dat.Rows.Count < 2)
                    {
                        btnDCKTP.Visible = true;
                        DCPIcKtp.Image = Resources.icons8_close_window_96;
                        lblDCktpstat.Text = "Data not complete!";
                        ktp = false;
                        lblExpktp = "Parents ID Card (KTP) not completed we only detected " + dat.Rows.Count.ToString() + " ID Card/KTP";
                    }
                    else
                    {
                        DCPIcKtp.Image = Resources.icons8_Check_Mark_48px_1;
                        btnDCKTP.Visible = false;
                        ktp = true;
                        lblDCktpstat.Text = "Parents ID Card (KTP) Completed!";
                    }
                    dataAdapter.Dispose();
                }
                else
                {
                    lblDCktpstat.Text = "This student is foreigner";
                    DCPIcKtp.Image = Resources.icons8_Check_Mark_48px_1;
                    btnDCKTP.Visible = false;
                    ktp = true;
                }
                //kitas
                if(nat.Contains("Indonesia") || nat.Contains ("Indonesian"))
                {
                    lblKitaS.Text = "KITAS (FOREIGN ONLY)";
                    lblDCKitasStat.Text = "KITAS NOT REQUIRED";
                    DCPIcKitas.Image = Resources.icons8_Check_Mark_48px_1;
                    btnDCKITAS.Visible = false;
                    kitas = true;
                }
                else
                {
                    cmd = new MySqlCommand("select docsname, docstype from document_students where owner_id = @owner and docstype = 'KITAS'", Db.get_connection());
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataTable data2 = new DataTable();
                    dataAdapter.Fill(data2);
                    if (data2.Rows.Count < 1)
                    {
                        lblKitaS.Text = "KITAS REQUIRED (FOREIGN STUDENT)";
                        lblExpKitas = "NO KITAS Detected!";
                        DCPIcKitas.Image = Resources.icons8_close_window_96;
                        btnDCKITAS.Visible = true;
                        kitas = false;
                    }
                    else if(data2.Rows.Count < 3)
                    {
                        lblExpKitas = "We only detect " + data2.Rows.Count.ToString() + "KITAS" + "\nForeigner student need atleast 3 KITAS!";
                        btnDCKITAS.Visible = true;
                        DCPIcKitas.Image = Resources.icons8_close_window_96;
                        kitas = false;
                    }
                    else
                    {
                        kitas = true;
                        lblExpKitas = "KITAS Completed!";
                        DCPIcKitas.Image = Resources.icons8_Check_Mark_48px_1;
                        btnDCKITAS.Visible = false;
                    }
                    dataAdapter.Dispose();
                }
                string dcFatherPhoto = null, dcMotherPhoto = null, father = null, mother = null;
                //parentsphoto - father
                cmd = new MySqlCommand("select photo from stud_relationship where relationshiptostud = @aisid and relationship = 'Father'", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        dcFatherPhoto = reader.GetString("photo");
                    }
                    catch (Exception)
                    {
                        dcFatherPhoto = null;
                    }
                    father = dcFatherPhoto;
                }
                reader.Close();
                cmd = new MySqlCommand("select photo from stud_relationship where relationshiptostud = @aisid and relationship = 'Mother'", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        dcMotherPhoto = reader.GetString("photo");
                    }
                    catch (Exception)
                    {
                        dcMotherPhoto = null;
                    }
                    mother = dcMotherPhoto;
                }
                reader.Close();
                if(father == "" || father == null)
                {
                    dcFatherPhoto = "Father Photo";
                }
                else
                {
                    dcFatherPhoto = string.Empty;
                }
                if(mother == "" || mother == null)
                {
                    dcMotherPhoto = "Mother Photo";
                }
                else
                {
                    dcMotherPhoto = string.Empty;
                }
                if(father == "" || father == null || mother == "" || mother == null)
                {
                    lblexpParentsPhoto = "You haven't uploaded this student's parents photo :\n" + dcFatherPhoto + "\n" + dcMotherPhoto;
                    DCPIcParentsPhoto.Image = Resources.icons8_close_window_96;
                    btnDCParents.Visible = true;
                    lblDCParentsPhotoStat.Text = "Photo not completed!";
                    parentsphotoDC = false;
                }
                else
                {
                    DCPIcParentsPhoto.Image = Resources.icons8_Check_Mark_48px_1;
                    btnDCParents.Visible = false;
                    lblDCParentsPhotoStat.Text = "Photo Completed!";
                    parentsphotoDC = true;
                }
                //studentphoto
                if(studentPhoto == "" || studentPhoto == null)
                {
                    lblDCStudPhoto.Text = "Photo not uploaded!";
                    DCPIcStudPhoto.Image = Resources.icons8_close_window_96;
                    BTNDCStud.Visible = true;
                    studphotoDC = false;
                }
                else
                {
                    lblDCStudPhoto.Text = "Photo Uploaded!";
                    DCPIcStudPhoto.Image = Resources.icons8_Check_Mark_48px_1;
                    BTNDCStud.Visible = false;
                    studphotoDC = true;
                }
                //passport
                cmd = new MySqlCommand("select docsname, docstype from document_students where owner_id = @aisid and docstype = 'Passport'", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count < 1)
                {
                    btnDCPassport.Visible = true;
                    lblExpPassport = "You haven't upload any passport!";
                    LblDCPassport.Text = "Passport not completed!";
                    lDCPIcPassport.Image = Resources.icons8_close_window_96;
                    passportDC = false;
                }
                else
                {
                    lDCPIcPassport.Image = Resources.icons8_Check_Mark_48px_1;
                    btnDCPassport.Visible = false;
                    LblDCPassport.Text = "Passport completed!";
                    passportDC = true;
                }
                da.Dispose();
                Db.close_connection();       
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
            if (formDC == true & ktp == true & kitas == true & studphotoDC == true & parentsphotoDC == true & passportDC == true)
            {
                lblDCStat.Text = "Data Completion - Data Completed!";
                lblDCStat.ForeColor = Color.Green;
            }
            else
            {
                lblDCStat.Text = "Data Completion - Not Complete";
                lblDCStat.ForeColor = Color.Red;
            }
        }

        void SearchQuery(string where)
        {
            switch (_searcby)
            {
                case Searchby.Name:
                    try
                    {
                        Db.open_connection();
                        MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', student_data.aisid as 'AIS ID', student_data.certificatename as 'Name', student_data.dob as 'Birthdate', class.classname as 'Class' from student_data left join aisDb.class on student_data.currclass = class.class_id where certificatename like '%" + where + "%' order by 'No.' ", Db.get_connection());
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bd = new BindingSource();
                        bd.DataSource = dt;
                        dgStudList.DataSource = bd;
                        rightPanelDataReader();
                        Db.close_connection();
                        if (dgStudList.Rows.Count < 1)
                        {
                            panelEmpty.Visible = true;
                            dgStudList.Visible = false;
                        }
                        else
                        {
                            panelEmpty.Visible = false;
                            dgStudList.Visible = true;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;
                case Searchby.ID:
                    try
                    {
                        Db.open_connection();
                        MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', student_data.aisid as 'AIS ID', student_data.certificatename as 'Name', student_data.dob as 'Birthdate', class.classname as 'Class' from student_data join aisDb.class on student_data.currclass = class.class_id where aisid like '%" + where + "%' order by 'No.' ", Db.get_connection());
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bd = new BindingSource();
                        bd.DataSource = dt;
                        dgStudList.DataSource = bd;
                        if (dgStudList.Rows.Count < 1)
                        {
                            Msg.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                        }
                        rightPanelDataReader();
                        Db.close_connection();
                        if (dgStudList.Rows.Count < 1)
                        {
                            panelEmpty.Visible = true;
                        }
                        else
                        {
                            panelEmpty.Visible = false;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;
                case Searchby.Gender:
                    try
                    {
                        Db.open_connection();
                        MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', student_data.aisid as 'AIS ID', student_data.certificatename as 'Name', student_data.dob as 'Birthdate', class.classname as 'Class' from student_data join aisDb.class on student_data.currclass = class.class_id where gender ='" + where + "' order by 'No.' ", Db.get_connection());
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bd = new BindingSource();
                        bd.DataSource = dt;
                        dgStudList.DataSource = bd;
                        if (dgStudList.Rows.Count < 1)
                        {
                            Msg.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                        }
                        else
                        {
                            Msg.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                        }
                        rightPanelDataReader();
                        Db.close_connection();
                        if (dgStudList.Rows.Count < 1)
                        {
                            panelEmpty.Visible = true;
                        }
                        else
                        {
                            panelEmpty.Visible = false;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;
                case Searchby.Origin:
                    try
                    {
                        Db.open_connection();
                        MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', student_data.aisid as 'AIS ID', student_data.certificatename as 'Name', student_data.dob as 'Birthdate', class.classname as 'Class' from student_data join aisDb.class on student_data.currclass = class.class_id where localforeign ='" + where + "' order by 'No.' ", Db.get_connection());
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bd = new BindingSource();
                        bd.DataSource = dt;
                        dgStudList.DataSource = bd;
                        if (dgStudList.Rows.Count < 1)
                        {
                            Msg.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                        }
                        else
                        {
                            Msg.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                        }
                        rightPanelDataReader();
                        Db.close_connection();
                        if (dgStudList.Rows.Count < 1)
                        {
                            panelEmpty.Visible = true;
                        }
                        else
                        {
                            panelEmpty.Visible = false;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;
                case Searchby.Revised:
                    if (where == "Revised")
                    {
                        try
                        {
                            Db.open_connection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', student_data.aisid as 'AIS ID', student_data.certificatename as 'Name', student_data.dob as 'Birthdate', class.classname as 'Class' from student_data join aisDb.class on student_data.currclass = class.class_id where revised is not null order by 'No.'", Db.get_connection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgStudList.DataSource = bd;
                            if (dgStudList.Rows.Count < 1)
                            {
                                Msg.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                            }
                            else
                            {
                                Msg.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                            }
                            rightPanelDataReader();
                            Db.close_connection();
                            if (dgStudList.Rows.Count < 1)
                            {
                                panelEmpty.Visible = true;
                            }
                            else
                            {
                                panelEmpty.Visible = false;
                            }
                        }
                        catch (MySqlException ex)
                        {
                            Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    else
                    {
                        try
                        {
                            Db.open_connection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', student_data.aisid as 'AIS ID', student_data.certificatename as 'Name', student_data.dob as 'Birthdate', class.classname as 'Class' from student_data join aisDb.class on student_data.currclass = class.class_id where revised is null order by 'No.' ", Db.get_connection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgStudList.DataSource = bd;
                            if (dgStudList.Rows.Count < 1)
                            {
                                Msg.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                            }
                            else
                            {
                                Msg.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                            }
                            rightPanelDataReader();
                            Db.close_connection();
                            if (dgStudList.Rows.Count < 1)
                            {
                                panelEmpty.Visible = true;
                            }
                            else
                            {
                                panelEmpty.Visible = false;
                            }
                        }
                        catch (MySqlException ex)
                        {
                            Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    break;
                default:
                    Msg.Alert("No search criteria was specified", frmAlert.AlertType.Info);
                    break;
            }
        }

        private void guna2RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            _searcby = Searchby.ID;
            DoSearchControl();
        }

        private void RadGender_CheckedChanged(object sender, EventArgs e)
        {
            _searcby = Searchby.Gender;
            DoSearchControl();

        }

        private void RadOrigin_CheckedChanged(object sender, EventArgs e)
        {
            _searcby = Searchby.Origin;
            DoSearchControl();

        }

        private void RadRevised_CheckedChanged(object sender, EventArgs e)
        {
            _searcby = Searchby.Revised;
            DoSearchControl();

        }

        private void txtSearchByName_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (_searcby)
            {
                case Searchby.Name:

                    break;
                case Searchby.ID:
                    // Verify that the pressed key isn't CTRL or any non-numeric digit
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        public void FunctionEnter()
        {
            switch (_State)
            {
                case UIStateEnum.MainMenu:
                    break;
                case UIStateEnum.RecordStudent:
                    break;
                case UIStateEnum.Enquiry:
                    break;
                case UIStateEnum.EditStudentData:
                    break;
                case UIStateEnum.StudentDirectory:
                    DoSearch();
                    break;
                case UIStateEnum.ClassAssignment:
                    break;
                default:
                    break;
            }
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            bool stat = panelExportOptionStud.Visible;
            switch (stat)
            {
                case true:
                    panelExportOptionStud.Visible = false;
                    break;
                case false:
                    panelExportOptionStud.Visible = true;
                    break;
            }
            //Future Function
            Msg.Alert("This will be available soon :)", frmAlert.AlertType.Info);
        }

        private void dgStudList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgStudList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgStudList.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgStudList.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgStudList.Rows[selectedrowindex];
                string aisidchoosed = Convert.ToString(selectedRow.Cells["AIS ID"].Value);
                aisid = Convert.ToInt32(aisidchoosed);
                rightPanelDataReader();
            }
        }

        //goback from student directory
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            showMenu();
        }

        private void btnStudEdit_Click(object sender, EventArgs e)
        {
            if (txtBriefStudName.Text == "")
            {
                Msg.Alert("Oops, you didn't select any student", frmAlert.AlertType.Warning);
            }
            else
            {
                UIState(UIStateEnum.EditStudentData);

            }
        }

        private void BtnBackRecordStud_Click(object sender, EventArgs e)
        {
            //finalize student update on next patch
            Dashboard mainform;
            mainform = (Dashboard)this.FindForm();
            //if (StudIsSaved && RelationshipCount >= 4)
            //{
            DialogControl conform = new DialogControl();
            mainform.PanelContainer.Controls.Add(conform);
            mainform.PanelContainer.Controls[mainform.PanelContainer.Controls.IndexOf(conform)].BringToFront();
            conform.dialodMsg("Cancel student record?", "Beware! Any unsaved data will be lost!", "Yes, i don't feel like to continue", "No, i misclicked it", Resources.icons8_question_mark_480px, DialogControl.Handlerstate.SaveStudent, mainform.PanelContainer.Controls.IndexOf(mainform.UCSchoolAdm));

            // }
            //else
            //  {
            // Msg.Alert("Please provide more information!", frmAlert.AlertType.Warning);
            // }
        }

        private void UCSchoolAdm_Load(object sender, EventArgs e)
        {
        }

        private void txtais_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        private void PanelClassAssignment3_Click(object sender, EventArgs e)
        {
            UIState(UIStateEnum.ClassDirectory);
        }

        private void btnBackEmpDir_Click(object sender, EventArgs e)
        {
            PanelClassAssignment.Hide();
            UIState(UIStateEnum.MainMenu);
        }

        void ClassAssignmentInit()
        {
            dropTc.Items.Clear();
            dropAssTc.Items.Clear();
            collection.EmpidTeacherEmpid.Clear();
            try
            {
                Db.open_connection();
                //careteacher
                MySqlCommand cmd = new MySqlCommand("fetch_teacher_assign_class", Db.get_connection());
                cmd.Parameters.Add("@teacher", MySqlDbType.VarChar).Value = "Teacher";
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dropTc.Items.Add(reader.GetString("emp_fullname"));
                        collection.EmpidTeacherEmpid.Add(reader.GetInt32("employeeid").ToString());
                    }
                }
                reader.Close();
                cmd = new MySqlCommand("fetch_teacher_assign_class", Db.get_connection());
                cmd.Parameters.Add("@teacher", MySqlDbType.VarChar).Value = "Assistant Teacher";
                cmd.CommandType = CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dropAssTc.Items.Add(reader.GetString("emp_fullname"));
                        collection.EmpidAssistantTeacherEmpid.Add(reader.GetInt32("employeeid").ToString());
                    }
                }
                reader.Close();
                Db.close_connection();
                ClassLoader();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void dropGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string grade = "def";
            if (dropClassGradeGrade.SelectedValue == "KINDERGARTEN 1" || dropClassGradeGrade.SelectedValue == "KINDERGARTEN 2" || dropClassGradeGrade.SelectedValue == "KINDERGARTEN 3" || dropClassGradeGrade.SelectedValue == "NURSERY")
            {
                lblAssTc.Visible = true;
                dropAssTc.Visible = true;
                grade = "Preschool";
            }
            else
            {
                dropAssTc.Visible = false;
                lblAssTc.Visible = false;
                grade = "Upper Level";
            }
            switch (grade)
            {
                case "Preschool":
                    try
                    {
                        Db.open_connection();


                        Db.close_connection();
                    }
                    catch (MySqlException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;

                case "Upper Level":

                    break;
                default:
                    break;
            }
        }

        public enum EnumClassLeftPanel
        {
            Student,
            Class
        };
        private EnumClassLeftPanel _LeftPanel;

        void CASwitcher(EnumClassLeftPanel e)
        {
            _LeftPanel = e;
            switch (_LeftPanel)
            {
                case EnumClassLeftPanel.Student:
                    btn_class_addclass.FillColor = Color.Silver;
                    btn_class_addclass.ForeColor = Color.Black;
                    btn_class_stud.FillColor = Color.Black;
                    btn_class_stud.ForeColor = Color.White;
                    PanelAddClass.Hide();
                    panel_class_stud.Show();
                    break;
                case EnumClassLeftPanel.Class:
                    btn_class_addclass.FillColor = Color.Black;
                    btn_class_addclass.ForeColor = Color.White;
                    btn_class_stud.FillColor = Color.Silver;
                    btn_class_stud.ForeColor = Color.White;
                    PanelAddClass.Show();
                    panel_class_stud.Hide();
                    break;
                default:
                    break;
            }
        }

        private void btn_class_stud_Click(object sender, EventArgs e)
        {
            CASwitcher(EnumClassLeftPanel.Student);
        }

        private void btn_class_addclass_Click(object sender, EventArgs e)
        {
            CASwitcher(EnumClassLeftPanel.Class);

        }

        private void PanelClassAssignment_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtClassCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dropTc_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectTeacherController();

        }

        private void dropAssTc_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void dropAssTc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectTeacherController();

        }

        void ClassLoader()
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select class_id as 'Class ID', grade as 'Level', classname as 'Class Section', class_capacity as 'Available Seat', emp_fullname as 'Care Teacher' from aisDb.class join employee_data on employeeid = careteacher where grade = @grade and class_stat = 'ONGOING';", Db.get_connection());
                cmd.Parameters.Add("@grade", MySqlDbType.VarChar).Value = dropChooseGrade.SelectedValue.ToString();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                BindingSource bs = new BindingSource();
                bs.DataSource = dataTable;
                dgClassList.DataSource = bs;
                if(dgClassList.Rows.Count < 1)
                {
                    dgClassList.Visible = false;
                    panelClassIsEmpty.Visible = true;
                }
                else
                {
                    dgClassList.Columns[0].Visible = false;
                    dgClassList.Columns[1].Visible = false;
                    dgClassList.Visible = true;
                    panelClassIsEmpty.Visible = false;

                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        void InsertClass()
        {
            try
            {
                
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("insert into aisDb.class (classname, grade, careteacher, assistant, class_start, class_stat, class_capacity) values (@classname, @grade, @careteacher, @assistant, @class_start, @class_stat, @class_capacity)", Db.get_connection());
                cmd.Parameters.Add("@classname", MySqlDbType.VarChar).Value = dropClassGradeGrade.SelectedValue.ToString() + " " + txtClassName.Text;
                cmd.Parameters.Add("@grade", MySqlDbType.VarChar).Value = dropClassGradeGrade.SelectedValue.ToString();
                cmd.Parameters.Add("@careteacher", MySqlDbType.VarChar).Value = careTeacher;
                cmd.Parameters.Add("@assistant", MySqlDbType.VarChar).Value = AssTeacher;
                cmd.Parameters.Add("@class_start", MySqlDbType.Date).Value = dropClassStart.Value.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@class_stat", MySqlDbType.VarChar).Value = "ONGOING";
                cmd.Parameters.Add("@class_capacity", MySqlDbType.Int32).Value = txtClassCapacity.Text;
                if(cmd.ExecuteNonQuery() == 1)
                {
                    Msg.Alert("Yay! you've succesfully created class\nnow please add some class member :)", frmAlert.AlertType.Success);
                    ClearPropertyClass();
                    ClassLoader();
                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void dgClassList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        void ClassMemberLoad()
        {

            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select grade, classname, class_id as 'Class ID', aisid as 'AIS ID', certificatename as 'Name', gender as 'Gender', religion as 'Religion' from class join student_data on currclass = class_id where class_id = @class_id order by certificatename", Db.get_connection());
                cmd.Parameters.Add("@class_id", MySqlDbType.Int32).Value = ClassId;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dgClassMember.DataSource = bs;
                dgClassMember.Columns[0].Visible = false;
                dgClassMember.Columns[1].Visible = false;
                dgClassMember.Columns[2].Visible = false;
                dgClassMember.Columns[3].Visible = false;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lblClassName.Text = reader.GetString("classname")  + " " + "Members";
                    }
                }
                reader.Close();
                MySqlCommand cmd1 = new MySqlCommand("select grade, classname, class_capacity from class where class_id = @class_id2", Db.get_connection());
                cmd1.Parameters.Add("@class_id2", MySqlDbType.Int32).Value = ClassId;
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    lblClassName.Text = reader1.GetString("classname") + " " + "Members";
                    
                }
                cmd1.Parameters.Add("@class_id", MySqlDbType.Int32).Value = ClassId;
                reader1.Close();
                Db.close_connection();
                if(dgClassMember.Rows.Count < 1)
                {
                    //hide dglist
                    class_ass_member_empty.Visible = true;
                    dgClassMember.Visible = false;
                }
                else
                {
                    //show dglist
                    dgClassMember.Visible = true;
                    class_ass_member_empty.Visible = false;
                }
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void radClassByName_CheckedChanged(object sender, EventArgs e)
        {
            _searcby = Searchby.Name;
        }

        private void radClassById_CheckedChanged(object sender, EventArgs e)
        {
            txtCASearchStud.Clear();
            _searcby = Searchby.ID;
        }

        private void txtCASearchStud_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(_searcby == Searchby.ID)
            {
                // Verify that the pressed key isn't CTRL or any non-numeric digit
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            switch (_searcby)
            {
                case Searchby.Name:
                    SearchQueryCA(txtCASearchStud.Text);
                    break;
                case Searchby.ID:
                    SearchQueryCA(txtCASearchStud.Text);
                    break;
            }
        }

        void RightCAReader()
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select certificatename, religion, gender, studentimg from student_data where aisid = @aisid", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    lblCAStudGender.Visible = true;
                    lblCAStudName.Visible = true;
                    lblCAStudReligion.Visible = true;
                    lblPleaseAssignStud.Visible = false;
                    picSwap.Visible = true;
                    picCAStud.Visible = true;
                    while (reader.Read())
                    {
                        lblCAStudGender.Text = "Gender: " + reader.GetString("gender");
                        lblCAStudName.Text = "Student Name: " + reader.GetString("certificatename");
                        lblCAStudReligion.Text = "Religion: " + reader.GetString("religion");
                        try
                        {
                            picCAStud.Image = Image.FromFile(reader.GetString("studentimg"));

                        }
                        catch (Exception)
                        {
                            picCAStud.Image = Resources.icons8_student_male_80px;
                        }
                    }
                }
                else
                {
                    picSwap.Visible = false;
                    lblCAStudGender.Visible = false;
                    lblCAStudName.Visible = false;
                    lblCAStudReligion.Visible = false;
                    picCAStud.Visible = false;
                    lblPleaseAssignStud.Visible = true;
                }
                reader.Close();
                Db.close_connection(); 
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        void SearchQueryCA(string where)
        {
            switch (_searcby)
            {
                case Searchby.Name:
                    try
                    {
                        Db.open_connection();
                        MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', student_data.aisid as 'AIS ID', student_data.certificatename as 'Name', student_data.dob as 'Birthdate', class.classname as 'Class' from student_data join aisDb.class on student_data.currclass = class.class_id where certificatename like '%" + where + "%' and currclass = '0' order by 'No.' ", Db.get_connection());
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bd = new BindingSource();
                        bd.DataSource = dt;
                        dgClassStudList.DataSource = bd;
                        rightPanelDataReader();
                        Db.close_connection();
                        if (dgClassStudList.Rows.Count < 1)
                        {
                            guna2ShadowPanel7.Visible = true;
                            dgClassStudList.Visible = false;
                        }
                        else
                        {
                            guna2ShadowPanel7.Visible = false;
                            dgClassStudList.Visible = true;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;
                case Searchby.ID:
                    try
                    {
                        Db.open_connection();
                        MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', student_data.aisid as 'AIS ID', student_data.certificatename as 'Name', student_data.dob as 'Birthdate', class.classname as 'Class' from student_data join aisDb.class on student_data.currclass = class.class_id where aisid like '%" + where + "%' and currclass = '0' order by 'No.' ", Db.get_connection());
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bd = new BindingSource();
                        bd.DataSource = dt;
                        dgClassStudList.DataSource = bd;
                        if (dgClassStudList.Rows.Count < 1)
                        {
                            Msg.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                        }
                        rightPanelDataReader();
                        Db.close_connection();
                        if (dgClassStudList.Rows.Count < 1)
                        {
                            panelEmpty.Visible = true;
                            dgClassStudList.Visible = false;
                        }
                        else
                        {
                            panelEmpty.Visible = false;
                            dgClassStudList.Visible = true;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;
            }
        }

        private void dgClassStudList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("delete from class where class_id = @id", Db.get_connection());
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = ClassId;
                if(cmd.ExecuteNonQuery() == 1)
                {
                    Msg.Alert("Class has been deleted!", frmAlert.AlertType.Error);
                }
                else
                {
                    Msg.Alert("Unknown error occured", frmAlert.AlertType.Warning);
                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void btnDCKTP_Click(object sender, EventArgs e)
        {
            DCShowStat(lblExpktp);
        }

        private void dgClassMember_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDCKITAS_Click(object sender, EventArgs e)
        {
            DCShowStat(lblExpKitas);
        }

        private void btnDCParents_Click(object sender, EventArgs e)
        {
            DCShowStat(lblexpParentsPhoto);
        }

        private void BTNDCStud_Click(object sender, EventArgs e)
        {
            DCShowStat("You haven't uploaded student photo!");
        }

        private void btnDCPassport_Click(object sender, EventArgs e)
        {
            DCShowStat(lblExpPassport);
        }

        private void txtClassCapacity_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgClassStudList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgClassStudList.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgClassStudList.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgClassStudList.Rows[selectedrowindex];
                string aisidchoosed = Convert.ToString(selectedRow.Cells["AIS ID"].Value);
                aisid = Convert.ToInt32(aisidchoosed);
                RightCAReader();
            }
        }
        int classCapacity;
        private void dgClassList_SelectionChanged(object sender, EventArgs e)
        {
            //readdg
            if (dgClassList.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgClassList.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgClassList.Rows[selectedrowindex];
                string classchoosed = Convert.ToString(selectedRow.Cells["Class ID"].Value);
                string capacity = Convert.ToString(selectedRow.Cells["Available Seat"].Value);
                ClassId = Convert.ToInt32(classchoosed);
                classCapacity = Convert.ToInt32(capacity);
                ClassMemberLoad();
            }
        }

        private void dgClassMember_SelectionChanged(object sender, EventArgs e)
        {
            if (dgClassMember.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgClassMember.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgClassMember.Rows[selectedrowindex];
                string memberchoosed = Convert.ToString(selectedRow.Cells["AIS ID"].Value);
                aisidCA = Convert.ToInt32(memberchoosed);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            UIState(UIStateEnum.StudentDirectory);

        }

        private void label5_Click(object sender, EventArgs e)
        {
            UIState(UIStateEnum.StudentDirectory);

        }

        private void lblRecord_Click(object sender, EventArgs e)
        {
            UIState(UIStateEnum.RecordStudent);

        }

        private void label4_Click(object sender, EventArgs e)
        {
            UIState(UIStateEnum.RecordStudent);

        }

        private void label6_Click(object sender, EventArgs e)
        {
            CASwitcher(EnumClassLeftPanel.Class);
            UIState(UIStateEnum.ClassAssignment);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            CASwitcher(EnumClassLeftPanel.Class);
            UIState(UIStateEnum.ClassAssignment);
        }

        private void btnStudDetailed_Click(object sender, EventArgs e)
        {
            UIState(UIStateEnum.DetailedStudent);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Controls.Add(summarystud);
            this.Controls[this.Controls.IndexOf(summarystud)].Dock = DockStyle.Fill;
            this.Controls[this.Controls.IndexOf(summarystud)].BringToFront();
        }

        private void PanelClassAssignment3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            try
            {
                //update student
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("update student_data set currclass = 0 where aisid = @aisid", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisidCA;
                if(cmd.ExecuteNonQuery() == 1)
                {
                    cmd = new MySqlCommand("update class set class_capacity = class_capacity + 1 where class_id = @class_id", Db.get_connection());
                    cmd.Parameters.Add("@class_id", MySqlDbType.Int32).Value = ClassId;
                    if(cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Student has been removed succesfully!", frmAlert.AlertType.Success);
                        ClassMemberLoad();
                        ClassLoader();
                        switch (_searcby)
                        {
                            case Searchby.Name:
                                SearchQueryCA(txtCASearchStud.Text);
                                break;
                            case Searchby.ID:
                                SearchQueryCA(txtCASearchStud.Text);
                                break;
                        }
                    }
                    else
                    {
                        Msg.Alert("Unknown Error", frmAlert.AlertType.Error);
                    }
                }
                //add capacity
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            if (dropChooseGrade.SelectedValue.ToString() == "NOT ASSIGNED")
            {
                Msg.Alert("You cannot assign the student to this grade/level", frmAlert.AlertType.Warning);
            }
            else if (dgClassList.Rows.Count < 1)
            {
                Msg.Alert("No Class choosed, please select the class first!", frmAlert.AlertType.Warning);
            }
            else if (classCapacity < 1)
            {
                Msg.Alert("This class section is full, please create another section", frmAlert.AlertType.Error);
            }
            else if (aisid == 0 || aisid == null)
            {
                Msg.Alert("Please select student first!", frmAlert.AlertType.Warning);
            }
            else if(dgClassStudList.Rows.Count < 1)
            {
                Msg.Alert("Please select student first!", frmAlert.AlertType.Warning);
            }
            else
            {
                AssignStudent();
            }

        }

        void AssignStudent()
        {
            try
            {
                Db.open_connection();
                MySqlCommand fetchGrade = new MySqlCommand("select glevel from grade where gname = @gname", Db.get_connection());
                fetchGrade.Parameters.Add("@gname", MySqlDbType.VarChar).Value = dropChooseGrade.SelectedValue.ToString();
                MySqlDataReader readerFetch = fetchGrade.ExecuteReader();
                while (readerFetch.Read())
                {
                    GradeID = readerFetch.GetInt32("glevel");
                }
                readerFetch.Close();
                MySqlCommand cmd = new MySqlCommand("update student_data set current_grade = @grade, currclass = @currclass where aisid = @aisid", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                cmd.Parameters.Add("@grade", MySqlDbType.Int32).Value = GradeID;
                cmd.Parameters.Add("@currclass", MySqlDbType.Int32).Value = ClassId;
                if(cmd.ExecuteNonQuery() == 1)
                {
                    MySqlCommand cmdClassHistory = new MySqlCommand("insert into class_history (stud_id, class_id, assigned_date, assigneDby, class_status) values (@stud_id, @class_id, @assigned_date, @assigneDby, @status)", Db.get_connection());
                    cmdClassHistory.Parameters.Add("@stud_id", MySqlDbType.Int32).Value = aisid;
                    cmdClassHistory.Parameters.Add("@class_id", MySqlDbType.Int32).Value = ClassId;
                    cmdClassHistory.Parameters.Add("@assigned_date", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmdClassHistory.Parameters.Add("@assigneDby", MySqlDbType.Int32).Value = Dashboard.ownerId;
                    cmdClassHistory.Parameters.Add("@status", MySqlDbType.VarChar).Value = "ASSIGNED";
                    if(cmdClassHistory.ExecuteNonQuery() == 1)
                    {
                        cmd = new MySqlCommand("update class set class_capacity = class_capacity - 1 where class_id = @class_id", Db.get_connection());
                        cmd.Parameters.Add("@class_id", MySqlDbType.Int32).Value = ClassId;
                        if(cmd.ExecuteNonQuery() == 1)
                        {
                            Msg.Alert(lblCAStudName.Text + "\nHas Been Assigned to\n" + lblClassName.Text, frmAlert.AlertType.Info);
                            ClassMemberLoad();
                            ClassLoader();
                            switch (_searcby)
                            {
                                case Searchby.Name:
                                    SearchQueryCA(txtCASearchStud.Text);
                                    break;
                                case Searchby.ID:
                                    SearchQueryCA(txtCASearchStud.Text);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Msg.Alert("Something is wrong", frmAlert.AlertType.Error);
                    }
                }
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        private void btnDCExpand_Click(object sender, EventArgs e)
        {
            bool DCIsShow = panelDC.Visible;
            switch (DCIsShow)
            {
                case true:
                    panelDC.Visible = false;
                    btnDCExpand.FillColor = Color.Silver;
                break;
                case false:
                    panelDC.Visible = true;
                    btnDCExpand.FillColor = Color.LightCoral;
                    break;
            }
        }

        void DCShowStat(string w)
        {
            MessageBox.Show(w, "You missed this", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDCStudForm_Click(object sender, EventArgs e)
        {
            DCShowStat(lblExpform);
        }

        void ClearPropertyClass()
        {
            txtClassName.Clear();
            txtClassCapacity.Clear();
            ClassAssignmentInit();
        }
        private void dropAssTc_DropDownClosed(object sender, EventArgs e)
        {
            SelectTeacherController();
        }

        private void dropChooseGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassLoader();
            ClassMemberLoad();
            if(dgClassList.Rows.Count < 1)
            {
                lblClassName.Text = "No Class Selected - Please select class";
            }
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            //validate before can insert
            if(txtClassName.Text == "")
            {
                Msg.Alert("We don't know the class name/section\nPlease specify", frmAlert.AlertType.Warning);
            }

            else if(txtClassCapacity.Text == "")
            {
                Msg.Alert("Class capacity cannot be empty!", frmAlert.AlertType.Warning);
            }
            else if(dropClassGradeGrade.SelectedValue == "NOT ASSIGNED")
            {
                Msg.Alert("You cannot create class on this grade", frmAlert.AlertType.Warning);
            }
            else if(careTeacher == null)
            {
                Msg.Alert("We don't know who's the careteacher", frmAlert.AlertType.Warning);
            }
            else
            {
                InsertClass();
            }
        }

        void SelectTeacherController()
        {
            if(dropClassGradeGrade.SelectedValue == "KINDERGARTEN 1" || dropClassGradeGrade.SelectedValue == "KINDERGARTEN 2" || dropClassGradeGrade.SelectedValue == "KINDERGARTEN 3" || dropClassGradeGrade.SelectedValue == "NURSERY")
            {
                careTeacher = Convert.ToInt32(collection.EmpidTeacherEmpid[dropTc.SelectedIndex]);
                try
                {
                    AssTeacher = Convert.ToInt32(collection.EmpidAssistantTeacherEmpid[dropAssTc.SelectedIndex]);
                }
                catch (Exception)
                {
                    AssTeacher = null;
                }
            }
            else
            {
                careTeacher = Convert.ToInt32(collection.EmpidTeacherEmpid[dropTc.SelectedIndex]);
                AssTeacher = null;
            }
        }

        private void dropTc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectTeacherController();
        }

        void deleteDocs()
        {
            try
            {

                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM aisDb.document_students WHERE id_Docs = @iddocs", Db.get_connection());
                    cmd.Parameters.Add("@iddocs", MySqlDbType.Int32).Value = docsSelected;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Document deleted succesfully!", frmAlert.AlertType.Success);
                        loadListDocs();
                    }
                    else
                    {
                        Msg.Alert("Failed to delete record, check your connection!", frmAlert.AlertType.Error);
                    }
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            catch (System.NullReferenceException ex)
            {
                Msg.Alert("There is nothing to delete!", frmAlert.AlertType.Error);
            }
            Db.close_connection();
        }

        void PrevSchoolHandler()
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select id as 'ID', name_of_school as 'Name of School', country as 'Country', grade as 'Grade', dateattended as 'Date Attended', extra_support as 'Extra Support' from student_previous_school_info where of_student = @aisid", Db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dt;
                dgSchoolInfo.DataSource = bindingSource;
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);

            }
        }

        private void btnAddInfo_Click(object sender, EventArgs e)
        {
            SaveMeds();
        }

        private void btnSiblingRemove_Click(object sender, EventArgs e)
        {
            if (dgSibling.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgSibling.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgSibling.Rows[selectedrowindex];
                string siblingchoosed = Convert.ToString(selectedRow.Cells["ID"].Value);
                siblingid = Convert.ToInt32(siblingchoosed);
            }
            deleteSibling();

        }

        void deleteSibling()
        {
            try
            {
                Db.open_connection();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("delete from stud_sibling_info where siblingofstud = @aisid and id = @id", Db.get_connection());
                    cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = siblingid;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Sibling info deleted succesfully", frmAlert.AlertType.Success);
                        siblingHandler();
                    }
                    else
                    {
                        Msg.Alert("Failed to delete data", frmAlert.AlertType.Error);
                    }
                }
                catch (Exception ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }

        }

        private void picMother_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = ("Please Compress the file before uploading! (*.JPG), (*.PNG), (*.JPEG) | *.JPG; *.PNG; *.JPEG");
            if (opf.ShowDialog() == DialogResult.OK)
            {
                picMother.Image = Image.FromFile(opf.FileName);
                string photopath = opf.FileName;
                string extension = System.IO.Path.GetExtension(opf.FileName);
                string copyToDestination = @"\\192.168.30.100\SysInternal\Img\StudentPhoto\" + aisid + "Mother" + extension;
                motherPhoto = copyToDestination;
                try
                {
                    File.Copy(photopath, copyToDestination, true);
                }
                catch (IOException ex)
                {

                }
            }
            else
            {
                Msg.Alert("No changes were made mother", frmAlert.AlertType.Warning);
            }
            opf.Dispose();
        }

        private void btnMotherSave_Click(object sender, EventArgs e)
        {
            saveMother();
        }

        private void btnBackM_Click(object sender, EventArgs e)
        {
            ShowPanel(PanelMother1);
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            ShowPanel(PanelStepF1);
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            //finalize student update on next patch
            Dashboard mainform;
            mainform = (Dashboard)this.FindForm();
            //if (StudIsSaved && RelationshipCount >= 4)
            //{
            DialogControl conform = new DialogControl();
            mainform.PanelContainer.Controls.Add(conform);
            mainform.PanelContainer.Controls["DialogControl"].BringToFront();
            conform.dialodMsg("Finalize student record?", "You can revise the data on the student directory later!", "Yes, I'm Done", "No, I'm not done yet", Resources.icons8_question_mark_480px, DialogControl.Handlerstate.SaveStudent, mainform.PanelContainer.Controls.IndexOf(mainform.UCSchoolAdm));
            // }
            //else
            //  {
            // Msg.Alert("Please provide more information!", frmAlert.AlertType.Warning);
            // }

        }

        private void BtnAddStepFather_Click(object sender, EventArgs e)
        {
            if (RelationshipCount >= 4)
            {
                Msg.Alert("A Student can only have 4 or less relationship", frmAlert.AlertType.Info);
            }
            else
            {
                RelationshipCount++;
                switch (RelationshipCount)
                {
                    case 1:
                        entity1 = _entity.StepFather;
                        break;
                    case 2:
                        entity2 = _entity.StepFather;
                        break;
                    case 3:
                        entity3 = _entity.StepFather;
                        break;
                    case 4:
                        entity4 = _entity.StepFather;
                        break;
                }
                TopNavEntity(RelationshipCount, _entity.StepFather);
                panelAddRelationship.Hide();
                hidePanelRelatAdd();
                BtnAddStepFather.Enabled = false;
            }
        }

        private void BtnAddStepMother_Click(object sender, EventArgs e)
        {
            if (RelationshipCount >= 4)
            {
                Msg.Alert("A Student can only have 4 or less relationship", frmAlert.AlertType.Info);
            }
            else
            {
                RelationshipCount++;
                switch (RelationshipCount)
                {
                    case 1:
                        entity1 = _entity.StepMother;
                        break;
                    case 2:
                        entity2 = _entity.StepMother;
                        break;
                    case 3:
                        entity3 = _entity.StepMother;
                        break;
                    case 4:
                        entity4 = _entity.StepMother;
                        break;
                }
                TopNavEntity(RelationshipCount, _entity.StepMother);
                panelAddRelationship.Hide();
                hidePanelRelatAdd();
                BtnAddStepMother.Enabled = false;
            }
        }

        private void BtnAddGuardian_Click(object sender, EventArgs e)
        {
            if (RelationshipCount >= 4)
            {
                Msg.Alert("A Student can only have 4 or less relationship", frmAlert.AlertType.Info);
            }
            else
            {
                RelationshipCount++;
                switch (RelationshipCount)
                {
                    case 1:
                        entity1 = _entity.Guardian;
                        break;
                    case 2:
                        entity2 = _entity.Guardian;
                        break;
                    case 3:
                        entity3 = _entity.Guardian;
                        break;
                    case 4:
                        entity4 = _entity.Guardian;
                        break;
                }
                TopNavEntity(RelationshipCount, _entity.Guardian);
                panelAddRelationship.Hide();
                hidePanelRelatAdd();
                BtnAddGuardian.Enabled = false;
            }
        }

        private void btnAddSibling_Click(object sender, EventArgs e)
        {
            if (RelationshipCount >= 4)
            {
                Msg.Alert("A Student can only have 4 or less relationship", frmAlert.AlertType.Info);
            }
            else
            {
                RelationshipCount++;
                switch (RelationshipCount)
                {
                    case 1:
                        entity1 = _entity.Sibling;
                        break;
                    case 2:
                        entity2 = _entity.Sibling;
                        break;
                    case 3:
                        entity3 = _entity.Sibling;
                        break;
                    case 4:
                        entity4 = _entity.Sibling;
                        break;
                }
                TopNavEntity(RelationshipCount, _entity.Sibling);
                panelAddRelationship.Hide();
                hidePanelRelatAdd();
                btnAddSibling.Enabled = false;
            }
        }

        private void btnStuDback_Click(object sender, EventArgs e)
        {
            ShowPanel(panelStud1);
        }

        private void btnStudOk_Click(object sender, EventArgs e)
        {
            SaveStud();
        }

        void saveMother()
        {
            if (!motheriIsSaved)
            {
                //pull save
                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO aisDb.stud_relationship
(relationship,
relationshiptostud,
name,
nationality,
australianresidence,
auabortorres,
schooleducation,
nonschooleducation,
occupation,
homeaddress,
homestate,
homecountry,
suburb,
postcode,
postaladdress,
postalstate,
postalsuburb,
postalcode,
postalcountry,
homephoneno,
mobilenumb,
faxnumb,
emailaddress,
whatsapp,
mainlang,
otherthanenglish,
doc,
photo,
maker)
VALUES
(@relationship,
@relationshiptostud,
@name,
@nationality,
@australianresidence,
@auabortorres,
@schooleducation,
@nonschooleducation,
@occupation,
@homeaddress,
@homestate,
@homecountry,
@suburb,
@postcode,
@postaladdress,
@postalstate,
@postalsuburb,
@postalcode,
@postalcountry,
@homephoneno,
@mobilenumb,
@faxnumb,
@emailaddress,
@whatsapp,
@mainlang,
@otherthanenglish,
@doc,
@photo,
@maker)", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Mother";
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtMName.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtMnationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtMAuRes.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtMAuaborigine.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtMSchoolEdu.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtMNonschool.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtMOccupation.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtmHomeAdd.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtMHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtMHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtMSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtMPostcode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtMPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtMPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtMPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtMPostalcode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtMPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtMHomePhoneNumb.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtMMobileNumb.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtMFaxNumb.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtMEmailAdd.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtMWhatsapp.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtMMainLang.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = txtMOtherEng.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = motherPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Mother information saved succesfully!", frmAlert.AlertType.Success);
                        motheriIsSaved = true;
                    }
                    Db.close_connection();
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                //revise
                try
                {
                    Db.open_connection();

                    MySqlCommand cmd = new MySqlCommand(@"UPDATE aisDb.stud_relationship
SET
relationship = @relationship,
relationshiptostud = @relationshiptostud,
name = @name,
nationality = @nationality,
australianresidence = @australianresidence,
auabortorres = @auabortorres,
schooleducation = @schooleducation,
nonschooleducation = @nonschooleducation,
occupation = @occupation,
homeaddress = @homeaddress,
homestate = @homestate,
homecountry = @homecountry,
suburb = @suburb,
postcode = @postcode,
postaladdress = @postaladdress,
postalstate = @postalstate,
postalsuburb = @postalsuburb,
postalcode = @postalcode,
postalcountry = @postalcountry,
homephoneno = @homephoneno,
mobilenumb = @mobilenumb,
faxnumb = @faxnumb,
emailaddress = @emailaddress,
whatsapp = @whatsapp,
mainlang = @mainlang,
otherthanenglish = @otherthanenglish,
doc = @doc,
photo = @photo,
maker = @maker
WHERE relationshiptostud = @relationshiptostud and relationship = 'Mother'
", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Mother";
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtMName.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtMnationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtMAuRes.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtMAuaborigine.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtMSchoolEdu.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtMNonschool.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtMOccupation.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtmHomeAdd.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtMHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtMHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtMSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtMPostcode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtMPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtMPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtMPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtMPostalcode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtMPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtMHomePhoneNumb.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtMMobileNumb.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtMFaxNumb.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtMEmailAdd.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtMWhatsapp.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtMMainLang.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = txtMOtherEng.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = motherPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Mother information updated succesfully", frmAlert.AlertType.Info);
                    }
                    else
                    {
                        Msg.Alert("Cannot update information", frmAlert.AlertType.Warning);
                    }
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
                Db.close_connection();
            }
        }

        void SaveStepFather()
        {
            if (!stepFatherIsSaved)
            {
                //pull save
                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO aisDb.stud_relationship
(relationship,
relationshiptostud,
name,
nationality,
australianresidence,
auabortorres,
schooleducation,
nonschooleducation,
occupation,
homeaddress,
homestate,
homecountry,
suburb,
postcode,
postaladdress,
postalstate,
postalsuburb,
postalcode,
postalcountry,
homephoneno,
mobilenumb,
faxnumb,
emailaddress,
whatsapp,
mainlang,
otherthanenglish,
doc,
photo,
maker)
VALUES
(@relationship,
@relationshiptostud,
@name,
@nationality,
@australianresidence,
@auabortorres,
@schooleducation,
@nonschooleducation,
@occupation,
@homeaddress,
@homestate,
@homecountry,
@suburb,
@postcode,
@postaladdress,
@postalstate,
@postalsuburb,
@postalcode,
@postalcountry,
@homephoneno,
@mobilenumb,
@faxnumb,
@emailaddress,
@whatsapp,
@mainlang,
@otherthanenglish,
@doc,
@photo,
@maker)", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Step Father";
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtSFName.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtSFNationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtSFAuRes.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtSFAuAbor.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtSFSchoolEdu.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtSFNonSchool.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtSFOccupation.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtSFHomeAdd.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtSFHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtSFHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtSFSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtSFPostCode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtSFPostalAdd.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtSFPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtSFPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtSFPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtSFPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtSFHomePhone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtSFMobileNumb.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtSFFaxNumb.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtSFEmailAdd.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtSFWhatsappNumb.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtSFParentsMainLang.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = txtSFOtherThanEnglish.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = stepFatherPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Step Father information saved succesfully!", frmAlert.AlertType.Success);
                        stepFatherIsSaved = true;
                    }
                    Db.close_connection();
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                //revise
                try
                {
                    Db.open_connection();

                    MySqlCommand cmd = new MySqlCommand(@"UPDATE aisDb.stud_relationship
SET
relationship = @relationship,
relationshiptostud = @relationshiptostud,
name = @name,
nationality = @nationality,
australianresidence = @australianresidence,
auabortorres = @auabortorres,
schooleducation = @schooleducation,
nonschooleducation = @nonschooleducation,
occupation = @occupation,
homeaddress = @homeaddress,
homestate = @homestate,
homecountry = @homecountry,
suburb = @suburb,
postcode = @postcode,
postaladdress = @postaladdress,
postalstate = @postalstate,
postalsuburb = @postalsuburb,
postalcode = @postalcode,
postalcountry = @postalcountry,
homephoneno = @homephoneno,
mobilenumb = @mobilenumb,
faxnumb = @faxnumb,
emailaddress = @emailaddress,
whatsapp = @whatsapp,
mainlang = @mainlang,
otherthanenglish = @otherthanenglish,
doc = @doc,
photo = @photo,
maker = @maker
WHERE relationshiptostud = @relationshiptostud and relationship = 'Step Father'
", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Step Father";
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtSFName.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtSFNationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtSFAuRes.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtSFAuAbor.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtSFSchoolEdu.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtSFNonSchool.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtSFOccupation.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtSFHomeAdd.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtSFHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtSFHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtSFSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtSFPostCode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtSFPostalAdd.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtSFPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtSFPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtSFPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtSFPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtSFHomePhone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtSFMobileNumb.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtSFFaxNumb.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtSFEmailAdd.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtSFWhatsappNumb.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtSFParentsMainLang.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = txtSFOtherThanEnglish.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = stepFatherPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Mother information updated succesfully", frmAlert.AlertType.Info);
                    }
                    else
                    {
                        Msg.Alert("Cannot update information", frmAlert.AlertType.Error);
                    }
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
                Db.close_connection();
            }
        }

        void SaveStepMother()
        {
            if (!stepFatherIsSaved)
            {
                //pull save
                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO aisDb.stud_relationship
(relationship,
relationshiptostud,
name,
nationality,
australianresidence,
auabortorres,
schooleducation,
nonschooleducation,
occupation,
homeaddress,
homestate,
homecountry,
suburb,
postcode,
postaladdress,
postalstate,
postalsuburb,
postalcode,
postalcountry,
homephoneno,
mobilenumb,
faxnumb,
emailaddress,
whatsapp,
mainlang,
otherthanenglish,
doc,
photo,
maker)
VALUES
(@relationship,
@relationshiptostud,
@name,
@nationality,
@australianresidence,
@auabortorres,
@schooleducation,
@nonschooleducation,
@occupation,
@homeaddress,
@homestate,
@homecountry,
@suburb,
@postcode,
@postaladdress,
@postalstate,
@postalsuburb,
@postalcode,
@postalcountry,
@homephoneno,
@mobilenumb,
@faxnumb,
@emailaddress,
@whatsapp,
@mainlang,
@otherthanenglish,
@doc,
@photo,
@maker)", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Step Mother";
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtSMname.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtSMnationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtSMAures.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtSMAuAborigine.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtSMSchool.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtSMNonSchool.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtSMOccu.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtSMHomeAdd.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtSMHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtSMHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtSMSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtSMPostCode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtSMPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtSMPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtSMPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtSMPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtSMPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtSMHomephone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtSMMobileNumber.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtSMFaxNumb.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtSMEmailAdd.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtSMWhatsapp.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtSMParentsMainLang.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = txtSMOtherThanEnglish.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = stepMotherPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Step Mother information saved succesfully!", frmAlert.AlertType.Success);
                        stepMotherIsSaved = true;
                    }
                    Db.close_connection();
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                //revise
                try
                {
                    Db.open_connection();

                    MySqlCommand cmd = new MySqlCommand(@"UPDATE aisDb.stud_relationship
SET
relationship = @relationship,
relationshiptostud = @relationshiptostud,
name = @name,
nationality = @nationality,
australianresidence = @australianresidence,
auabortorres = @auabortorres,
schooleducation = @schooleducation,
nonschooleducation = @nonschooleducation,
occupation = @occupation,
homeaddress = @homeaddress,
homestate = @homestate,
homecountry = @homecountry,
suburb = @suburb,
postcode = @postcode,
postaladdress = @postaladdress,
postalstate = @postalstate,
postalsuburb = @postalsuburb,
postalcode = @postalcode,
postalcountry = @postalcountry,
homephoneno = @homephoneno,
mobilenumb = @mobilenumb,
faxnumb = @faxnumb,
emailaddress = @emailaddress,
whatsapp = @whatsapp,
mainlang = @mainlang,
otherthanenglish = @otherthanenglish,
doc = @doc,
photo = @photo,
maker = @maker
WHERE relationshiptostud = @relationshiptostud and relationship = 'Step Mother'
", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Step Mother";
                    cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtSMname.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtSMnationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtSMAures.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtSMAuAborigine.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtSMSchool.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtSMNonSchool.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtSMOccu.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtSMHomeAdd.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtSMHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtSMHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtSMSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtSMPostCode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtSMPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtSMPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtSMPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtSMPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtSMPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtSMHomephone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtSMMobileNumber.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtSMFaxNumb.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtSMEmailAdd.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtSMWhatsapp.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtSMParentsMainLang.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = txtSMOtherThanEnglish.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = stepMotherPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Mother information updated succesfully", frmAlert.AlertType.Info);
                    }
                    else
                    {
                        Msg.Alert("Cannot update information", frmAlert.AlertType.Error);
                    }
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
                Db.close_connection();
            }
        }

        void SaveGuardian()
        {
            if (!guardianIsSaved)
            {
                //pull save
                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO aisDb.stud_relationship
(relationship,
relationshiptostud,
name,
nationality,
australianresidence,
auabortorres,
schooleducation,
nonschooleducation,
occupation,
homeaddress,
homestate,
homecountry,
suburb,
postcode,
postaladdress,
postalstate,
postalsuburb,
postalcode,
postalcountry,
homephoneno,
mobilenumb,
faxnumb,
emailaddress,
whatsapp,
mainlang,
otherthanenglish,
doc,
photo,
maker)
VALUES
(@relationship,
@relationshiptostud,
@name,
@nationality,
@australianresidence,
@auabortorres,
@schooleducation,
@nonschooleducation,
@occupation,
@homeaddress,
@homestate,
@homecountry,
@suburb,
@postcode,
@postaladdress,
@postalstate,
@postalsuburb,
@postalcode,
@postalcountry,
@homephoneno,
@mobilenumb,
@faxnumb,
@emailaddress,
@whatsapp,
@mainlang,
@otherthanenglish,
@doc,
@photo,
@maker)", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = txtGRelationshipChild.Text;
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtGname.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtGNationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtGAuRes.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtGAuAborigine.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtGSchEdu.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtFNonSchoolEdu.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtGOccupation.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtGHomeAdd.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtGHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtGHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtGSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtGPostCode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtGPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtGPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtGPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtGPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtGPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtGHomePhoneNumb.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtGMobileNumb.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtGFaxNumb.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtGEmailAdd.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtGWhatsapp.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtGMainLang.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = guardianPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert(txtGRelationshipChild.Text + "information saved succesfully!", frmAlert.AlertType.Success);
                        guardianIsSaved = true;
                    }
                    Db.close_connection();
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {

                //revise
                try
                {
                    Db.open_connection();

                    MySqlCommand cmd = new MySqlCommand(@"UPDATE aisDb.stud_relationship
SET
relationship = @relationship,
relationshiptostud = @relationshiptostud,
name = @name,
nationality = @nationality,
australianresidence = @australianresidence,
auabortorres = @auabortorres,
schooleducation = @schooleducation,
nonschooleducation = @nonschooleducation,
occupation = @occupation,
homeaddress = @homeaddress,
homestate = @homestate,
homecountry = @homecountry,
suburb = @suburb,
postcode = @postcode,
postaladdress = @postaladdress,
postalstate = @postalstate,
postalsuburb = @postalsuburb,
postalcode = @postalcode,
postalcountry = @postalcountry,
homephoneno = @homephoneno,
mobilenumb = @mobilenumb,
faxnumb = @faxnumb,
emailaddress = @emailaddress,
whatsapp = @whatsapp,
mainlang = @mainlang,
otherthanenglish = @otherthanenglish,
doc = @doc,
photo = @photo,
maker = @maker
WHERE relationshiptostud = @relationshiptostud and relationship = @relationship
", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = txtGRelationshipChild.Text;
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtGname.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtGNationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtGAuRes.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtGAuAborigine.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtGSchEdu.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtFNonSchoolEdu.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtGOccupation.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtGHomeAdd.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtGHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtGHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtGSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtGPostCode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtGPostalAddress.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtGPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtGPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtGPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtGPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtGHomePhoneNumb.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtGMobileNumb.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtGFaxNumb.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtGEmailAdd.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtGWhatsapp.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtGMainLang.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = guardianPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Mother information updated succesfully", frmAlert.AlertType.Info);
                    }
                    else
                    {
                        Msg.Alert("Cannot update information", frmAlert.AlertType.Error);
                    }
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
                Db.close_connection();
            }
        }



        void SaveFather()
        {
            if (!fatherIsSaved)
            {
                //pull save
                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO aisDb.stud_relationship
(relationship,
relationshiptostud,
name,
nationality,
australianresidence,
auabortorres,
schooleducation,
nonschooleducation,
occupation,
homeaddress,
homestate,
homecountry,
suburb,
postcode,
postaladdress,
postalstate,
postalsuburb,
postalcode,
postalcountry,
homephoneno,
mobilenumb,
faxnumb,
emailaddress,
whatsapp,
mainlang,
otherthanenglish,
doc,
photo,
maker)
VALUES
(@relationship,
@relationshiptostud,
@name,
@nationality,
@australianresidence,
@auabortorres,
@schooleducation,
@nonschooleducation,
@occupation,
@homeaddress,
@homestate,
@homecountry,
@suburb,
@postcode,
@postaladdress,
@postalstate,
@postalsuburb,
@postalcode,
@postalcountry,
@homephoneno,
@mobilenumb,
@faxnumb,
@emailaddress,
@whatsapp,
@mainlang,
@otherthanenglish,
@doc,
@photo,
@maker)", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Father";
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtFName.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtFNationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtFAuRes.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtFAuAborigine.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtFSchoolEdu.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtFNonSchoolEdu.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtFOccupation.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtFHomeAddress.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtFHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtFHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtFSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtFPostCode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtFPostalAdd.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtFPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtFPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtFPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtFPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtFHomePhone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtFMobileNumber.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtFFaxNumber.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtFEmailAddress.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtFWhatsapp.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtFMainLanguage.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = txtFOtherThanEnglishSpoken.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = fatherPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Father information saved succesfully!", frmAlert.AlertType.Success);
                        fatherIsSaved = true;
                    }
                    Db.close_connection();
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                //revise
                try
                {
                    Db.open_connection();

                    MySqlCommand cmd = new MySqlCommand(@"UPDATE aisDb.stud_relationship
SET
name = @name,
nationality = @nationality,
australianresidence = @australianresidence,
auabortorres = @auabortorres,
schooleducation = @schooleducation,
nonschooleducation = @nonschooleducation,
occupation = @occupation,
homeaddress = @homeaddress,
homestate = @homestate,
homecountry = @homecountry,
suburb = @suburb,
postcode = @postcode,
postaladdress = @postaladdress,
postalstate = @postalstate,
postalsuburb = @postalsuburb,
postalcode = @postalcode,
postalcountry = @postalcountry,
homephoneno = @homephoneno,
mobilenumb = @mobilenumb,
faxnumb = @faxnumb,
emailaddress = @emailaddress,
whatsapp = @whatsapp,
mainlang = @mainlang,
otherthanenglish = @otherthanenglish,
doc = @doc,
photo = @photo,
maker = @maker
WHERE relationshiptostud = @relationshiptostud and relationship = @relationship
", Db.get_connection());
                    cmd.Parameters.Add("@relationship", MySqlDbType.VarChar).Value = "Father";
                    cmd.Parameters.Add("@relationshiptostud", MySqlDbType.VarChar).Value = aisid;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = txtFName.Text;
                    cmd.Parameters.Add("@nationality", MySqlDbType.VarChar).Value = txtFNationality.Text;
                    cmd.Parameters.Add("@australianresidence", MySqlDbType.VarChar).Value = txtFAuRes.Text;
                    cmd.Parameters.Add("@auabortorres", MySqlDbType.VarChar).Value = txtFAuAborigine.Text;
                    cmd.Parameters.Add("@schooleducation", MySqlDbType.VarChar).Value = txtFSchoolEdu.Text;
                    cmd.Parameters.Add("@nonschooleducation", MySqlDbType.VarChar).Value = txtFNonSchoolEdu.Text;
                    cmd.Parameters.Add("@occupation", MySqlDbType.VarChar).Value = txtFOccupation.Text;
                    cmd.Parameters.Add("@homeaddress", MySqlDbType.VarChar).Value = txtFHomeAddress.Text;
                    cmd.Parameters.Add("@homestate", MySqlDbType.VarChar).Value = txtFHomeState.Text;
                    cmd.Parameters.Add("@homecountry", MySqlDbType.VarChar).Value = txtFHomeCountry.Text;
                    cmd.Parameters.Add("@suburb", MySqlDbType.VarChar).Value = txtFSuburb.Text;
                    cmd.Parameters.Add("@postcode", MySqlDbType.VarChar).Value = txtFPostCode.Text;
                    cmd.Parameters.Add("@postaladdress", MySqlDbType.VarChar).Value = txtFPostalAdd.Text;
                    cmd.Parameters.Add("@postalstate", MySqlDbType.VarChar).Value = txtFPostalState.Text;
                    cmd.Parameters.Add("@postalsuburb", MySqlDbType.VarChar).Value = txtFPostalSuburb.Text;
                    cmd.Parameters.Add("@postalcode", MySqlDbType.VarChar).Value = txtFPostalCode.Text;
                    cmd.Parameters.Add("@postalcountry", MySqlDbType.VarChar).Value = txtFPostalCountry.Text;
                    cmd.Parameters.Add("@homephoneno", MySqlDbType.VarChar).Value = txtFHomePhone.Text;
                    cmd.Parameters.Add("@mobilenumb", MySqlDbType.VarChar).Value = txtFMobileNumber.Text;
                    cmd.Parameters.Add("@faxnumb", MySqlDbType.VarChar).Value = txtFFaxNumber.Text;
                    cmd.Parameters.Add("@emailaddress", MySqlDbType.VarChar).Value = txtFEmailAddress.Text;
                    cmd.Parameters.Add("@whatsapp", MySqlDbType.VarChar).Value = txtFWhatsapp.Text;
                    cmd.Parameters.Add("@mainlang", MySqlDbType.VarChar).Value = txtFMainLanguage.Text;
                    cmd.Parameters.Add("@otherthanenglish", MySqlDbType.VarChar).Value = txtFOtherThanEnglishSpoken.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.VarChar).Value = DateTime.Now.ToString(timeStamping);
                    cmd.Parameters.Add("@photo", MySqlDbType.VarChar).Value = fatherPhoto;
                    cmd.Parameters.Add("@maker", MySqlDbType.VarChar).Value = Dashboard.ownerId;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Father information updated succesfully", frmAlert.AlertType.Info);
                    }
                    else
                    {
                        Msg.Alert("Cannot update information", frmAlert.AlertType.Error);
                    }
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
                Db.close_connection();
            }
        }


        private void btnFNext_Click(object sender, EventArgs e)
        {
            if (!StudIsSaved)
            {
                Msg.Alert("Please save student information first!", frmAlert.AlertType.Info);
            }
            else
            {
                SaveFather();
                ShowPanel(PanelFather2);
            }
        }

        private void picStud_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileOpen = new OpenFileDialog();
            FileOpen.Filter = ("Please Compress the file before uploading! (*.JPG), (*.PNG), (*.JPEG) | *.JPG; *.PNG; *.JPEG;");
            if (FileOpen.ShowDialog() == DialogResult.OK)
            {
                    picStud.Image = Image.FromFile(FileOpen.FileName);
                    string photopath = FileOpen.FileName;
                    string extension = System.IO.Path.GetExtension(FileOpen.FileName);
                    string copyToDestination = @"\\192.168.30.100\SysInternal\Img\StudentPhoto\" + aisid + extension;
                    studentPhoto = copyToDestination;
                try
                {
                        File.Copy(photopath, copyToDestination, true);
                    }
                    catch (IOException ex)
                    {
                        Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
            }
            else
            {

            }
        }

        private void RadMale_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
        }

        private void radFemale_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }
    }
}
