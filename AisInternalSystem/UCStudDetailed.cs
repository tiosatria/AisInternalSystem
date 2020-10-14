using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AisInternalSystem.Module;
using Guna.UI2.WinForms;
using System.ComponentModel.Design;
using Microsoft.VisualBasic;
using AisInternalSystem.Properties;

namespace AisInternalSystem
{
    public partial class UCStudDetailed : UserControl
    {
        Db db = new Db();
        Dialog msg = new Dialog();
        MySqlCommand cmd = new MySqlCommand();
        PanelNotAvailable NA = new PanelNotAvailable();
        Point NAPos = new Point(248, 100);
        public Int32 aisid;

        public void FillRelationshipPanelData()
        {

        }

        public UCStudDetailed()
        {
            InitializeComponent();
            UIState(UIStateStudDetailed.Relationship);
        }

        public void LoadStudentData(int i)
        {
            try
            {
                db.open_connection();
                aisid = i;
                cmd = new MySqlCommand("SELECT * FROM student_data where aisid = @aisid", db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            studBriefPic.Image = Image.FromFile(reader.GetString("studentimg"));
                        }
                        catch (Exception)
                        {
                            studBriefPic.Image = Resources.icons8_student_male_60px;
                        }
                        txtSTUDIDBRIEF.Text = aisid.ToString();
                        txtSDCertificateName.Text = reader.GetString("certificatename");
                        txtSDReligion.Text = reader.GetString("religion");
                        txtSCertname.Text = reader.GetString("certificatename");
                        txtSGivenName.Text = reader.GetString("givenname");
                        txtsMiddleName.Text = reader.GetString("middlename");
                        txtSFamilyName.Text = reader.GetString("familyname");
                        txtSNationality.Text = reader.GetString("nationality");
                        txtSDateOfBirth.Text = reader.GetString("dob");
                        txtSPOB.Text = reader.GetString("pob");
                        txtSCOB.Text = reader.GetString("cob");
                        txtSGender.Text = reader.GetString("gender");
                        txtSReligion.Text = reader.GetString("religion");
                        txtSLangSpoken.Text = reader.GetString("langspoken");
                        txtSEngProf.Text = reader.GetString("englishproficiency");
                        txtSStudStat.Text = reader.GetString("studstat");
                        txtAAisID.Text = reader.GetString("aisid");
                        txtANisn.Text = reader.GetString("nis");
                        txtAAsn.Text = reader.GetString("ausid");
                        txtAIntake.Text = reader.GetString("intake");
                        txtSHomeAddress.Text = reader.GetString("homeaddress");
                        txtSHomeState.Text = reader.GetString("homestate");
                        txtSSuburb.Text = reader.GetString("suburb");
                        txtSPostCode.Text = reader.GetString("postcode");
                        txtSHomeCountry.Text = reader.GetString("homecountry");
                        txtSPostalAddress.Text = reader.GetString("postaladdress");
                        txtSPostalState.Text = reader.GetString("postalstate");
                        txtSPostalSuburb.Text = reader.GetString("postalsuburb");
                        txtSPostalCode.Text = reader.GetString("postalcode");
                        txtSPOstalCountry.Text = reader.GetString("postalcountry");
                        txtSHomePhone.Text = reader.GetString("homephone");
                        txtSMobileNumber.Text = reader.GetString("mobilenumb");
                        txtsFaxNumber.Text = reader.GetString("faxnumb");
                    }
                }
                reader.Close();
                //initiate the user panel for parents
                cmd = new MySqlCommand("select id, relationship, name, photo from stud_relationship where relationshiptostud = @aisid", db.get_connection());
                cmd.Parameters.Add("@aisid", MySqlDbType.Int32).Value = aisid;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 1)
                {
                    flowRelationshipPanel.Controls.Clear();
                    PanelMSqlCommand[] panelRelatUC = new PanelMSqlCommand[dataTable.Rows.Count];
                    flowRelationshipPanel.Visible = true;
                    NA.Hide();
                    for (i = 0; i < dataTable.Rows.Count; i++)
                    {
                        panelRelatUC[i] = new PanelMSqlCommand();
                        var _title = dataTable.Rows[i][2];
                        var _subtitle = dataTable.Rows[i][1];
                        var _photo = dataTable.Rows[i][3];
                        panelRelatUC[i].Title = _title.ToString();
                        panelRelatUC[i].Subtittle = _subtitle.ToString();
                        try
                        {
                            panelRelatUC[i].Img = Image.FromFile(_photo.ToString());
                        }
                        catch (Exception)
                        {
                            panelRelatUC[i].Img = Resources.icons8_student_male_80px;
                        }
                        flowRelationshipPanel.Controls.Add(panelRelatUC[i]);
                    }
                }
                else
                {
                    flowRelationshipPanel.Visible = false;
                    PanelStudentRelationship.Controls[PanelStudentRelationship.Controls.IndexOf(NA)].BringToFront();
                    NA.BringToFront();
                    NA.Location = NAPos;
                }

                db.close_connection();
            }
            catch (MySqlException ex)
            {
                msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }

        }

        public void RelationshipPanelClicked()
        {

        }

        public enum UIStateStudDetailed
        {
            Student,
            Academic,
            Personal,
            Relationship,
            MedicalInfo,
            Documents
        }

        public enum UIStateAcademic
        {
            General,
            ClassHistory,
            Grade,
            PreviousSchool
        };

        public enum UIStatePersonal
        {
            General, Contact
        }

        public enum UIStateRelationship
        {
            Mainmenu, General,
            Contact
        };

        private UIStateAcademic _StateAcademic;
        private UIStateStudDetailed _StudState;
        private UIStatePersonal _uIStatePersonal;
        private UIStateRelationship _StateRelationship;

        public void RelationshipSwitcher(UIStateRelationship _state)
        {
            _StateRelationship = _state;
            switch (_StateRelationship)
            {
                case UIStateRelationship.Mainmenu:
                    BtnRelationshipContact.Visible = false;
                    BtnRelationshipGeneral.Visible = false;
                    PanelStudRelationship.BringToFront();
                    break;
                case UIStateRelationship.General:
                    BtnRelationshipContact.Visible = true;
                    BtnRelationshipGeneral.Visible = true;
                    BtnRelationshipContact.FillColor = Color.LightCoral;
                    BtnRelationshipContact.ForeColor = Color.Black;
                    BtnRelationshipGeneral.FillColor = Color.Red;
                    BtnRelationshipGeneral.ForeColor = Color.White;
                    PanelStudentRelationship.BringToFront();
                    break;
                case UIStateRelationship.Contact:
                    BtnRelationshipContact.Visible = true;
                    BtnRelationshipGeneral.Visible = true;
                    BtnRelationshipContact.FillColor = Color.Red;
                    BtnRelationshipContact.ForeColor = Color.White;
                    BtnRelationshipGeneral.FillColor = Color.LightCoral;
                    BtnRelationshipGeneral.ForeColor = Color.Black;
                    PanelRelationshipContact.BringToFront();
                    break;
                default:
                    BtnRelationshipContact.Visible = false;
                    BtnRelationshipGeneral.Visible = false;
                    PanelStudRelationship.BringToFront();
                    break;
            }
        }

        public void PersonalSwitcher(UIStatePersonal _state)
        {
            _uIStatePersonal = _state;
            switch (_uIStatePersonal)
            {
                case UIStatePersonal.General:
                    PanelStudentPersonalInformation.BringToFront();
                    btnStudPersonalContact.FillColor = Color.Red;
                    btnStudPersonalContact.ForeColor = Color.White;
                    btnStudPersonalContactt.FillColor = Color.LightCoral;
                    btnStudPersonalContactt.ForeColor = Color.Black;
                    break;
                case UIStatePersonal.Contact:
                    PanelStudentContactinformation.BringToFront();
                    btnStudPersonalContact.FillColor = Color.LightCoral;
                    btnStudPersonalContact.ForeColor = Color.Black;
                    btnStudPersonalContactt.FillColor = Color.Red;
                    btnStudPersonalContactt.ForeColor = Color.White;
                    break;
                default:
                    PanelStudentPersonalInformation.BringToFront();
                    btnStudPersonalContact.FillColor = Color.Red;
                    btnStudPersonalContact.ForeColor = Color.White;
                    btnStudPersonalContactt.FillColor = Color.LightCoral;
                    btnStudPersonalContactt.ForeColor = Color.Black;
                    break;
            }
        }

        public void AcademicSwitcher(UIStateAcademic _state)
        {
            _StateAcademic = _state;
            switch (_StateAcademic)
            {
                case UIStateAcademic.General:
                    PanelAcademicGeneral.BringToFront();
                    AcademicButtonSwitcher(BtnAcademicGeneral);
                    break;
                case UIStateAcademic.ClassHistory:
                    PanelAcademicClassHistory.BringToFront();
                    AcademicButtonSwitcher(BtnAcademicClassHistory);
                    break;
                case UIStateAcademic.Grade:
                    PanelAcademicStudentGrade.BringToFront();
                    AcademicButtonSwitcher(BtnAcademicGrade);
                    break;
                case UIStateAcademic.PreviousSchool:
                    PanelPreviousSchool.BringToFront();
                    AcademicButtonSwitcher(BtnAcademicPrevSchool);
                    break;
            }
        }

        private void AcademicButtonSwitcher(Guna2Button button)
        {
            BtnAcademicGeneral.ForeColor = Color.Black;
            BtnAcademicGeneral.FillColor = Color.LightCoral;
            BtnAcademicClassHistory.FillColor = Color.LightCoral;
            BtnAcademicClassHistory.ForeColor = Color.Black;
            BtnAcademicGrade.FillColor = Color.LightCoral;
            BtnAcademicGrade.ForeColor = Color.Black;
            BtnAcademicPrevSchool.FillColor = Color.LightCoral;
            BtnAcademicPrevSchool.ForeColor = Color.Black;
            button.FillColor = Color.Red;
            button.ForeColor = Color.White;
        }

        public void UIState(UIStateStudDetailed state)
        {
            _StudState = state;
            switch (_StudState)
            {
                case UIStateStudDetailed.Academic:
                    switch (_StateAcademic)
                    {
                        case UIStateAcademic.General:
                            PanelAcademicGeneral.BringToFront();
                            AcademicButtonSwitcher(BtnAcademicGeneral);
                            break;
                        case UIStateAcademic.ClassHistory:
                            PanelAcademicClassHistory.BringToFront();
                            AcademicButtonSwitcher(BtnAcademicClassHistory);
                            break;
                        case UIStateAcademic.Grade:
                            PanelAcademicStudentGrade.BringToFront();
                            AcademicButtonSwitcher(BtnAcademicGrade);
                            break;
                        case UIStateAcademic.PreviousSchool:
                            PanelPreviousSchool.BringToFront();
                            AcademicButtonSwitcher(BtnAcademicPrevSchool);
                            break;
                        default:
                            PanelAcademicGeneral.BringToFront();
                            AcademicButtonSwitcher(BtnAcademicGeneral);
                            break;
                    }
                    FocusedButton(btnAcademic);
                    ShowButton();
                    break;
                case UIStateStudDetailed.Personal:
                    switch (_uIStatePersonal)
                    {
                        case UIStatePersonal.General:
                            PanelStudentPersonalInformation.BringToFront();
                            btnStudPersonalContact.FillColor = Color.Red;
                            btnStudPersonalContact.ForeColor = Color.White;
                            btnStudPersonalContactt.FillColor = Color.LightCoral;
                            btnStudPersonalContactt.ForeColor = Color.Black;
                            break;
                        case UIStatePersonal.Contact:
                            PanelStudentContactinformation.BringToFront();
                            btnStudPersonalContact.FillColor = Color.LightCoral;
                            btnStudPersonalContact.ForeColor = Color.Black;
                            btnStudPersonalContactt.FillColor = Color.Red;
                            btnStudPersonalContactt.ForeColor = Color.White;
                            break;
                        default:
                            PanelStudentPersonalInformation.BringToFront();
                            btnStudPersonalContact.FillColor = Color.Red;
                            btnStudPersonalContact.ForeColor = Color.White;
                            btnStudPersonalContactt.FillColor = Color.LightCoral;
                            btnStudPersonalContactt.ForeColor = Color.Black;
                            break;
                    }
                    FocusedButton(btnPersonal);
                    ShowButton();
                    break;
                case UIStateStudDetailed.Relationship:
                    ShowButton();
                    FocusedButton(btnRelationship);
                    switch (_StateRelationship)
                    {
                        case UIStateRelationship.Mainmenu:
                            BtnRelationshipContact.Visible = false;
                            BtnRelationshipGeneral.Visible = false;
                            PanelStudRelationship.BringToFront();
                            break;
                        case UIStateRelationship.General:
                            BtnRelationshipContact.Visible = true;
                            BtnRelationshipGeneral.Visible = true;
                            BtnRelationshipContact.FillColor = Color.LightCoral;
                            BtnRelationshipContact.ForeColor = Color.Black;
                            BtnRelationshipGeneral.FillColor = Color.Red;
                            BtnRelationshipGeneral.ForeColor = Color.White;
                            PanelStudentRelationship.BringToFront();
                            break;
                        case UIStateRelationship.Contact:
                            BtnRelationshipContact.Visible = true;
                            BtnRelationshipGeneral.Visible = true;
                            BtnRelationshipContact.FillColor = Color.Red;
                            BtnRelationshipContact.ForeColor = Color.White;
                            BtnRelationshipGeneral.FillColor = Color.LightCoral;
                            BtnRelationshipGeneral.ForeColor = Color.Black;
                            PanelRelationshipContact.BringToFront();
                            break;
                        default:
                            BtnRelationshipContact.Visible = false;
                            BtnRelationshipGeneral.Visible = false;
                            PanelStudRelationship.BringToFront();
                            break;
                    }
                    break;
                case UIStateStudDetailed.MedicalInfo:
                    ShowButton();
                    FocusedButton(btnMedical);
                    PanelStudentMedicalInformation.BringToFront();
                    break;
                case UIStateStudDetailed.Documents:
                    ShowButton();
                    FocusedButton(btnDocs);
                    PanelStudentDocuments.BringToFront();
                    break;

                default:
                    ShowButton();
                    FocusedButton(btnRelationship);
                    switch (_StateRelationship)
                    {
                        case UIStateRelationship.Mainmenu:
                            BtnRelationshipContact.Visible = false;
                            BtnRelationshipGeneral.Visible = false;
                            PanelStudRelationship.BringToFront();
                            break;
                        case UIStateRelationship.General:
                            BtnRelationshipContact.Visible = true;
                            BtnRelationshipGeneral.Visible = true;
                            BtnRelationshipContact.FillColor = Color.LightCoral;
                            BtnRelationshipContact.ForeColor = Color.Black;
                            BtnRelationshipGeneral.FillColor = Color.Red;
                            BtnRelationshipGeneral.ForeColor = Color.White;
                            PanelStudentRelationship.BringToFront();
                            break;
                        case UIStateRelationship.Contact:
                            BtnRelationshipContact.Visible = true;
                            BtnRelationshipGeneral.Visible = true;
                            BtnRelationshipContact.FillColor = Color.Red;
                            BtnRelationshipContact.ForeColor = Color.White;
                            BtnRelationshipGeneral.FillColor = Color.LightCoral;
                            BtnRelationshipGeneral.ForeColor = Color.Black;
                            PanelRelationshipContact.BringToFront();
                            break;
                        default:
                            BtnRelationshipContact.Visible = false;
                            BtnRelationshipGeneral.Visible = false;
                            PanelStudRelationship.BringToFront();
                            break;
                    }
                    break;
            }
        } 

        void ShowButton()
        {
            switch (_StudState)
            {
                case UIStateStudDetailed.Academic:
                    BtnAcademicClassHistory.Visible = true;
                    BtnAcademicGeneral.Visible = true;
                    BtnAcademicGrade.Visible = true;
                    BtnAcademicPrevSchool.Visible = true;
                    BtnRelationshipContact.Visible = false;
                    BtnRelationshipGeneral.Visible = false;
                    btnStudPersonalContact.Visible = false;
                    btnStudPersonalContactt.Visible = false;
                    break;
                case UIStateStudDetailed.Personal:
                    BtnAcademicClassHistory.Visible = false;
                    BtnAcademicGeneral.Visible = false;
                    BtnAcademicGrade.Visible = false;
                    BtnAcademicPrevSchool.Visible = false;
                    BtnRelationshipContact.Visible = false;
                    BtnRelationshipGeneral.Visible = false;
                    btnStudPersonalContact.Visible = true;
                    btnStudPersonalContactt.Visible = true;
                    break;
                case UIStateStudDetailed.Relationship:
                    BtnAcademicClassHistory.Visible = false;
                    BtnAcademicGeneral.Visible = false;
                    BtnAcademicGrade.Visible = false;
                    BtnAcademicPrevSchool.Visible = false;
                    BtnRelationshipContact.Visible = true;
                    BtnRelationshipGeneral.Visible = true;
                    btnStudPersonalContact.Visible = false;
                    btnStudPersonalContactt.Visible = false;
                    break;
                case UIStateStudDetailed.MedicalInfo:
                    BtnAcademicClassHistory.Visible = false;
                    BtnAcademicGeneral.Visible = false;
                    BtnAcademicGrade.Visible = false;
                    BtnAcademicPrevSchool.Visible = false;
                    BtnRelationshipContact.Visible = false;
                    BtnRelationshipGeneral.Visible = false;
                    btnStudPersonalContact.Visible = false;
                    btnStudPersonalContactt.Visible = false;
                    break;
                case UIStateStudDetailed.Documents:
                    BtnAcademicClassHistory.Visible = false;
                    BtnAcademicGeneral.Visible = false;
                    BtnAcademicGrade.Visible = false;
                    BtnAcademicPrevSchool.Visible = false;
                    BtnRelationshipContact.Visible = false;
                    BtnRelationshipGeneral.Visible = false;
                    btnStudPersonalContact.Visible = false;
                    btnStudPersonalContactt.Visible = false;
                    break;
            }
        }

        void FocusedButton(Guna2Button btn)
        {
            //set all button to not focus
            btnAcademic.FillColor = Color.LightGray;
            btnAcademic.ForeColor = Color.Black;
            btnPersonal.FillColor = Color.LightGray;
            btnPersonal.ForeColor = Color.Black;
            btnRelationship.FillColor = Color.LightGray;
            btnRelationship.ForeColor = Color.Black;
            btnMedical.FillColor = Color.LightGray;
            btnMedical.ForeColor = Color.Black;
            btnDocs.FillColor = Color.LightGray;
            btnDocs.ForeColor = Color.Black;

            //set focused button
            btn.FillColor = Color.Black;
            btn.ForeColor = Color.White;
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            UIState(UIStateStudDetailed.Academic);
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            UIState(UIStateStudDetailed.Personal);
        }

        private void btnRelationship_Click(object sender, EventArgs e)
        {
            UIState(UIStateStudDetailed.Relationship);
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            UIState(UIStateStudDetailed.MedicalInfo);
        }

        private void btnDocs_Click(object sender, EventArgs e)
        {
            UIState(UIStateStudDetailed.Documents);
        }

        private void btnBackEmpDir_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void UCEmployeeDetailed_Load(object sender, EventArgs e)
        {

        }

        private void BtnAcademicGeneral_Click(object sender, EventArgs e)
        {
            AcademicSwitcher(UIStateAcademic.General);
        }

        private void BtnAcademicPrevSchool_Click(object sender, EventArgs e)
        {
            AcademicSwitcher(UIStateAcademic.PreviousSchool);
        }

        private void BtnAcademicGrade_Click(object sender, EventArgs e)
        {
            AcademicSwitcher(UIStateAcademic.Grade);
        }

        private void BtnAcademicClassHistory_Click(object sender, EventArgs e)
        {
            AcademicSwitcher(UIStateAcademic.ClassHistory);
        }

        private void btnStudPersonalContact_Click(object sender, EventArgs e)
        {
            PersonalSwitcher(UIStatePersonal.General);
        }

        private void btnStudPersonalContactt_Click(object sender, EventArgs e)
        {
            PersonalSwitcher(UIStatePersonal.Contact);
        }

        private void BtnRelationshipContact_Click(object sender, EventArgs e)
        {
            RelationshipSwitcher(UIStateRelationship.Contact);
        }

        private void BtnRelationshipGeneral_Click(object sender, EventArgs e)
        {
            RelationshipSwitcher(UIStateRelationship.General);
        }
    }
}
