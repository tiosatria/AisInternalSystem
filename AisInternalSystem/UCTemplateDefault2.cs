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
using System.Data.OleDb;

namespace AisInternalSystem
{
    public partial class UCEmployeeDetailed : UserControl
    {
        Dialog msg = new Dialog();
        MySqlCommand cmd = new MySqlCommand();

        public Int64 empID;
        public Int32 employeeID;

        public UCEmployeeDetailed()
        {
            InitializeComponent();

        }

        public void LoadEmployeeData()
        {
            try
            {
                Db.open_connection();
                cmd = new MySqlCommand("select * from employee_data where emp_id = @emp_id", Db.get_connection());
                cmd.Parameters.Add("@emp_id", MySqlDbType.Int64).Value = empID;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            try
                            {
                                empBriefPic.Image = Image.FromFile(reader.GetString("employee_pic"));
                            }
                            catch (Exception)
                            {
                                empBriefPic.Image = Resources.icons8_male_user_100;
                            }
                            txtEmpIdBrief.Text = reader.GetString("emp_id");
                            employeeID = reader.GetInt32("employeeid");
                            txtEDFullname.Text = reader.GetString("emp_fullname");
                            txtEDMobilePhone.Text = reader.GetString("emp_mobile");
                            txtEDPemail.Text = reader.GetString("emp_personal_email");
                            txtEDResAddress.Text = reader.GetString("emp_address");
                            //roles
                            txtEmpRoles.Text = reader.GetString("emp_roles");
                            txtEmpDepartment.Text = reader.GetString("emp_department");
                            txtEmpJoinDate.Text = reader.GetString("emp_joindate");
                            txtEmpStatus.Text = reader.GetString("emp_status");
                            txtEmpID.Text = empID.ToString();
                            //personal
                            txtFullName.Text = reader.GetString("emp_fullname");
                            txtGender.Text = reader.GetString("emp_gender");
                            txtReligion.Text = reader.GetString("emp_religion");
                            txtPOB.Text = reader.GetString("emp_pob");
                            txtNationality.Text = reader.GetString("emp_nationality");
                            txtDateOfBirth.Text = reader.GetString("emp_dob");
                            txtResidentialAdd.Text = reader.GetString("emp_address");
                            txtHomePhone.Text = reader.GetString("emp_homephone");
                            txtHomeTownAdd.Text = reader.GetString("emp_hometown_address");
                            txtHomeTownNumber.Text = reader.GetString("emp_hometown_number");
                            txtMobileNumber.Text = reader.GetString("emp_mobile");
                            txtWhatsapp.Text = reader.GetString("emp_whatsapp");
                            txtPersonalEmail.Text = reader.GetString("emp_personal_email");
                            txtNPWP.Text = reader.GetString("emp_npwp");
                            txtNIK.Text = reader.GetString("emp_nik");
                            txtBPJSKES.Text = reader.GetString("emp_bpjs_kes");
                            txtBPJSTK.Text = reader.GetString("emp_bpjs_tk");
                            txtPassport.Text = reader.GetString("emp_passport_no");
                            //relationship
                            txtMaritalStatus.Text = reader.GetString("emp_marital_stat");
                            txtSpouseName.Text = reader.GetString("emp_spouse_name");
                            txtNoDependant.Text = reader.GetString("emp_no_dependant");
                            txtEmerContactName.Text = reader.GetString("emp_emergencycontactname");
                            txtEmerContactRelationship.Text = reader.GetString("emp_emergencyrelationship");
                            tztEmerContactPhone.Text = reader.GetString("emp_emergencycontactphone");
                            //education
                            txtNonSch1Institution.Text = reader.GetString("emp_nonsch1_institution");
                            txtNonSch2Institution.Text = reader.GetString("emp_nonsch2_institution");
                            if(txtNonSch1Institution.Text == "")
                            {
                                panelnonsch1.Visible = false;
                            }
                            else
                            {
                                panelnonsch1.Visible = true;
                                txtNonSch1Degree.Text = reader.GetString("emp_nonsch1_designation");
                                txtNonSch1GradYear.Text = reader.GetString("emp_nonsch1_year");
                                txtNonSch1.Text = reader.GetString("emp_nonsch1");
                            }
                            if (txtNonSch2Institution.Text == "")
                            {
                                panelnonsch2.Visible = false;
                            }
                            else
                            {
                                panelnonsch2.Visible = true;
                                txtNonSch2Degree.Text = reader.GetString("emp_nonsch2_designation");
                                txtNonSch2GradYear.Text = reader.GetString("emp_nonsch2_year");
                                txtNonSch2.Text = reader.GetString("emp_nonsch2");
                            }
                            txtSchoolEdu.Text = reader.GetString("emp_schedu");
                            txtSchooleduInstitutionName.Text = reader.GetString("emp_schedu_name");
                            txtSchoolEduGradName.Text = reader.GetString("emp_schedu_graduated");
                            if(txtSchoolEdu.Text == "")
                            {
                                txtSchoolEdu.Text = "No information provided";
                            }
                            if(txtSchooleduInstitutionName.Text == "")
                            {
                                txtSchooleduInstitutionName.Text = "No information provided";
                            }
                            if(txtSchoolEduGradName.Text == "")
                            {
                                txtSchoolEduGradName.Text = "No information provided";
                            }
                            string ispursuingDegree = reader.GetString("emp_ispursuingdegree");
                            if(ispursuingDegree == "YES")
                            {
                                panelPursuing.Visible = true;
                                try
                                {
                                    txtPursuingDegreePursued.Text = reader.GetString("emp_ispursuingdegree_degree");
                                    txtPursuingInstitution.Text = reader.GetString("emp_ispursuingdegree_institution");
                                    txtPursuingDuration.Text = reader.GetString("emp_ispursuingdegree_duration");
                                    txtPursuingStarted.Text = reader.GetString("emp_ispursuingdegree_startemployee");
                                }
                                catch (Exception)
                                {
                                    msg.Alert("Error Occured", frmAlert.AlertType.Warning);
                                }
                            }
                            else
                            {
                                panelPursuing.Visible = false;

                            }
                        }
                        catch (Exception)
                        {
                            msg.Alert("Error occured", frmAlert.AlertType.Warning);
                        }
                    }
                }
                reader.Close();
                flowDocuments.Controls.Clear();
                ReadDocuments();
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        public void ReadDocuments()
        {
            //read documents
            cmd = new MySqlCommand("select docsname, docspath, docstype, docsdesc from document_employee where owner_id = @owner_id", Db.get_connection());
            cmd.Parameters.Add("@owner_id", MySqlDbType.Int32).Value = employeeID;
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            if (dt.Rows.Count > 1)
            {
                UCDocsList[] documents = new UCDocsList[dt.Rows.Count];
                for (int i = 0; i < documents.Length; i++)
                {
                    flowDocuments.Visible = true;
                    documents[i] = new UCDocsList();
                    var title = dt.Rows[i][0];
                    var subtitle = dt.Rows[i][3];
                    var appPath = dt.Rows[i][1];
                    var docstype = dt.Rows[i][2];
                    documents[i].Title = title.ToString();
                    if (subtitle == "")
                    {
                        subtitle = "No description";
                    }
                    documents[i].Subtitle = subtitle.ToString();
                    documents[i].Pic = Resources.icons8_question_mark_480px;
                    documents[i].AppPath = appPath.ToString();
                    documents[i].Doctype = docstype.ToString();
                    documents[i].Appearance();
                    flowDocuments.Controls.Add(documents[i]);
                }
            }
            else
            {
                flowDocuments.Visible = false;
            }
        }

        public enum UIStateEmployeeDetailed
        {
            Role, 
            Personal,
            Relationship,
            Education,
            Documents
        }
        private UIStateEmployeeDetailed _EmployeeState;

        public void UIState(UIStateEmployeeDetailed state)
        {
            _EmployeeState = state;
            switch (_EmployeeState)
            {
                case UIStateEmployeeDetailed.Role:
                    PanelRoles.BringToFront();
                    FocuseDbutton(btnRoles);
                    break;
                case UIStateEmployeeDetailed.Personal:
                    PanelPersonal.BringToFront();
                    FocuseDbutton(btnPersonal);
                    break;
                case UIStateEmployeeDetailed.Relationship:
                    FocuseDbutton(btnRelationship);
                    PanelRelationship.BringToFront();
                    break;
                case UIStateEmployeeDetailed.Education:
                    FocuseDbutton(btnEducation);
                    PanelEducation.BringToFront();
                    break;
                case UIStateEmployeeDetailed.Documents:
                    FocuseDbutton(btnDocs);
                    panelDocuments.BringToFront();
                    break;
            }
        }

        void FocuseDbutton(Guna2Button btn)
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
            UIState(UIStateEmployeeDetailed.Role);
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            UIState(UIStateEmployeeDetailed.Personal);
        }

        private void btnRelationship_Click(object sender, EventArgs e)
        {
            UIState(UIStateEmployeeDetailed.Relationship);
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            UIState(UIStateEmployeeDetailed.Education);
        }

        private void btnDocs_Click(object sender, EventArgs e)
        {
            UIState(UIStateEmployeeDetailed.Documents);
        }

        private void btnBackEmpDir_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void UCEmployeeDetailed_Load(object sender, EventArgs e)
        {
            
        }

        private void btnPersonalNext_Click(object sender, EventArgs e)
        {
            PanelPersonalInfo2.BringToFront();
        }

        private void btnPersonalBack_Click(object sender, EventArgs e)
        {
            PanelPersonal.BringToFront();
        }
    }
}
