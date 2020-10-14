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
                if(dataTable.Rows.Count > 1)
                {
                    flowRelationshipPanel.Controls.Clear();
                    PanelMSqlCommand[] panelRelatUC = new PanelMSqlCommand[dataTable.Rows.Count];
                    flowRelationshipPanel.Visible = true;
                    NA.Hide();
                    for(i = 0; i < dataTable.Rows.Count; i++)
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
            Relationship1,
            Relationship2,
            Relationship3,
            Sibling,
            SchoolInfo,
            MedicalInfo,
            Documents
        }
        private UIStateStudDetailed _StudState;

        public void UIState(UIStateStudDetailed state)
        {
            _StudState = state;
            switch (_StudState)
            {
                
            }
        } 

        void FocusedButton(Guna2Button btn)
        {
            //set all button to not focus
            btnRoles.FillColor = Color.LightGray;
            btnRoles.ForeColor = Color.Black;
            btnPersonal.FillColor = Color.LightGray;
            btnPersonal.ForeColor = Color.Black;
            btnRelationship.FillColor = Color.LightGray;
            btnRelationship.ForeColor = Color.Black;
            btnEducation.FillColor = Color.LightGray;
            btnEducation.ForeColor = Color.Black;
            btnDocs.FillColor = Color.LightGray;
            btnDocs.ForeColor = Color.Black;

            //set focused button
            btn.FillColor = Color.Black;
            btn.ForeColor = Color.White;
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            //UIState(UIStateEmployeeDetailed.Role);
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            //UIState(UIStateEmployeeDetailed.Personal);
        }

        private void btnRelationship_Click(object sender, EventArgs e)
        {
            //UIState(UIStateEmployeeDetailed.Relationship);
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            //UIState(UIStateEmployeeDetailed.Education);
        }

        private void btnDocs_Click(object sender, EventArgs e)
        {
            //UIState(UIStateEmployeeDetailed.Documents);
        }

        private void btnBackEmpDir_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void UCEmployeeDetailed_Load(object sender, EventArgs e)
        {

        }
    }
}
