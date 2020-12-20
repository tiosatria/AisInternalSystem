using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Employee
{
    public partial class EmployeeDirectory : UserControl
    {
        private bool isLoaded = false;
        public EmployeeDirectory()
        {

        }
        public enum SearchBy
        {
            Name,
            ID,
            Gender,
            Origin,
            Education,
            Role
        }
        private SearchBy _searchby;
        public void InitObject()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                InitDGEmployee();
            }
            else
            {

            }
            isLoaded = true;
        }
        private void InitDGEmployee()
        {
            dgEmployeeList.DataSource = Entities.Employee.GetDataSource();
            if (dgEmployeeList.Rows.Count >=1)
            {
                dgEmployeeList.Visible = true;
                dgEmployeeList.Columns[0].Visible = false;
                panelEmpty.Visible = false;
            }
            else
            {
                dgEmployeeList.Visible = false;
                panelEmpty.Visible = true;
            }
        }

        private void InitDGSearchResult(string s, string q)
        {
            dgEmployeeList.DataSource = Entities.Employee.GetSearchResult(s, q);
            if (dgEmployeeList.Rows.Count >= 1)
            { 
                dgEmployeeList.Visible = true;
                dgEmployeeList.Columns[0].Visible = false;
                panelEmpty.Visible = false;
            }
            else
            {
                dgEmployeeList.Visible = false;
                panelEmpty.Visible = true;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DoSearch();
        }
        private void DoSearch()
        {
            //string & query
            string s = string.Empty;
            string q = string.Empty;
            switch (_searchby)
            {
                case SearchBy.Name:
                    s = "name";
                    q = txtSearchByName.Text;
                    break;
                case SearchBy.ID:
                    s = "id";
                    q = txtSearchByName.Text;
                    break;
                case SearchBy.Gender:
                    s = "gender";
                    q = CmbSearch.SelectedItem.ToString();
                    break;
                case SearchBy.Origin:
                    s = "origin";
                    q = CmbSearch.SelectedItem.ToString();
                    break;
                case SearchBy.Education:
                    s = "education";
                    q = CmbSearch.SelectedItem.ToString();
                    break;
                case SearchBy.Role:
                    s = "role";
                    q = CmbSearch.SelectedItem.ToString();
                    break;
                default:
                    s = "name";
                    q = txtSearchByName.Text;
                    break;
            }
            InitDGSearchResult(s, q);
        }
        private void PrepSearch(SearchBy search)
        {
            //search and query
            _searchby = search;
            txtSearchByName.Clear();
            CmbSearch.Items.Clear();
            switch (search)
            {
                case SearchBy.Name:
                    txtSearchByName.Visible = true;
                    CmbSearch.Visible = false;
                    break;
                case SearchBy.ID:
                    txtSearchByName.Visible = true;
                    CmbSearch.Visible = false;
                    break;
                case SearchBy.Gender:
                    CmbSearch.Items.Add("MALE");
                    CmbSearch.Items.Add("FEMALE");
                    CmbSearch.SelectedIndex = 0;
                    CmbSearch.Visible = true;
                    txtSearchByName.Visible = false;
                    break;
                case SearchBy.Origin:
                    CmbSearch.Items.Add("LOCAL");
                    CmbSearch.Items.Add("EXPAT");
                    CmbSearch.SelectedIndex = 0;

                    txtSearchByName.Visible = false;
                    CmbSearch.Visible = true;
                    break;
                case SearchBy.Education:
                    CmbSearch.Items.Add("MASTER");
                    CmbSearch.Items.Add("Bachelor");
                    CmbSearch.Items.Add("PURSUING DEGREE");
                    CmbSearch.Items.Add("HIGH SCHOOL");
                    CmbSearch.Items.Add("NO FORMAL EDUCATION");
                    CmbSearch.SelectedIndex = 0;

                    txtSearchByName.Visible = false;
                    CmbSearch.Visible = true;
                    break;
                case SearchBy.Role:
                    //ENUM('TEACHER', 'ASSISTANT TEACHER', 'ADMIN', 'ACCOUNTING', 'INFORMATION TECHNOLOGY', 'MANAGEMENT', 'DRIVER', 'SECURITY', 'CLEANING SERVICE')
                    CmbSearch.Items.Add("TEACHER");
                    CmbSearch.Items.Add("ASSISTANT TEACHER");
                    CmbSearch.Items.Add("ADMIN");
                    CmbSearch.Items.Add("ACCOUNTING");
                    CmbSearch.Items.Add("INFORMATION TECHNOLOGY");
                    CmbSearch.Items.Add("MANAGEMENT");
                    CmbSearch.Items.Add("DRIVER");
                    CmbSearch.Items.Add("SECURITY");
                    CmbSearch.Items.Add("CLEANING SERVICE");
                    CmbSearch.SelectedIndex = 0;

                    txtSearchByName.Visible = false;
                    CmbSearch.Visible = true; 
                    break;
            }
        }
        private void RadName_CheckedChanged(object sender, EventArgs e)
        {
            PrepSearch(SearchBy.Name);
        }

        private void RadID_CheckedChanged(object sender, EventArgs e)
        {
            PrepSearch(SearchBy.ID);
        }

        private void RadGender_CheckedChanged(object sender, EventArgs e)
        {
            PrepSearch(SearchBy.Gender);
        }

        private void RadOrigin_CheckedChanged(object sender, EventArgs e)
        {
            PrepSearch(SearchBy.Origin);
        }

        private void radEdu_CheckedChanged(object sender, EventArgs e)
        {
            PrepSearch(SearchBy.Education);
        }

        private void guna2RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            PrepSearch(SearchBy.Role);

        }
        public static Entities.Employee employee = null;
        private void FetchEmployeeInfo()
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(Controller.Utilities.GetSelectedDatagridValue(dgEmployeeList, "employeeid"));
            }
            catch (Exception)
            {
                i = 0;
            }
            if (i != 0)
            {
                employee = Entities.Employee.GetEmployeeInfo(i);
                EmployeeBriefInfoFetch(employee);
            }
        }
        private void EmployeeBriefInfoFetch(Entities.Employee e)
        {
            if (e!=null)
            {
                Entities.Education education = null;
                string pursuingdegree = string.Empty;
                txtEmpIdBrief.Text = e.EmployeeID.ToString();
                txtBriefEmpName.Text = e.Fullname;
                txtBriefBirthdate.Text = e.DateOfBirth.ToString("d");
                txtBriefNationality.Text = e.Nationality;
                txtBriefMobile.Text = e.Mobile;
                txbriefEmergency.Text = e.EmergencyContactPhone;
                txtBriefResidential.Text = e.Address;
                txtBriefRole.Text = e.Role;
                if (true)
                {

                }
                if (e.Educations != null)
                {
                    education = e.Educations[e.Educations.FindIndex(o => o.EduLevel == "Doctor" || o.EduLevel == "Master" || o.EduLevel == "Bachelor" || o.EduLevel == "Diploma/Advanced Diploma" || o.EduLevel == "Vocational" || o.EduLevel == "College/Higher Secondary (SMU/SMK)")];
                    if (education.Institution == "" || education.Institution == null)
                    {
                        LblBriefEdu.Text = "No institution name provided";
                    }
                    else
                    {
                        LblBriefEdu.Text = education.Institution;
                    }
                    if (education.EduDesignation == "" || education.EduDesignation == null)
                    {
                        lblBriefDegree.Text = "-";
                    }
                    else
                    {
                        lblBriefDegree.Text = education.EduDesignation;
                    }
                    if (education.GraduatedYear == "" || education.GraduatedYear == null)
                    {
                        if (education.EduDesignation != null || education.EduDesignation != "")
                        {
                            pursuingdegree = $"Currently pursuing degree in {education.EduDesignation}";

                        }
                        else
                        {
                            pursuingdegree = $"Currently pursuing degree";
                        }
                    }
                    else
                    {
                        pursuingdegree = "Currently not pursuing any degree.";
                    }
                }
                else
                {
                    LblBriefEdu.Text = "No Education Information Provided";
                    lblBriefDegree.Text = string.Empty;
                    lblBriefDegree.Text = string.Empty;
                }
                lblCurrentlyPursuingDegree.Text = pursuingdegree;
                InitEmployeePhoto();
            }
        }
        private Image EmployeeIMG = null;
        private BackgroundWorker worker = null;
        private void InitEmployeePhoto()
        {
            empBriefPic.Image = Properties.Resources.down_arrow_200_transparent;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            worker.Dispose();
            empBriefPic.Image = EmployeeIMG;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (Image img = Image.FromFile(employee.PhotoLocation))
                {
                    Bitmap b = new Bitmap(img);
                    EmployeeIMG = b;
                }
            }
            catch (Exception)
            {
                EmployeeIMG = Properties.Resources.icons8_male_user_100;
            }
        }

        private void dgEmployeeList_SelectionChanged(object sender, EventArgs e)
        {
            FetchEmployeeInfo();
        }

        private void btnEmpEdit_Click(object sender, EventArgs e)
        {
            Controller.UIController.NavigateUI(Controller.UIController.Controls.EditEmployee);
        }
    }
}
