using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using AisInternalSystem.Module;
using System.Windows.Forms;
using Guna.UI2.WinForms.Suite;
using Guna.UI2.WinForms;
using System.Data.SqlTypes;
using System.Collections.Specialized;
using AisInternalSystem.Controller;
using AisInternalSystem.Properties;

namespace AisInternalSystem
{
    public partial class UCEmployee : UserControl
    {
        Size defaultUcSize = new Size(1280, 611);
        BackgroundWorker worker = new BackgroundWorker();
        Size panelMainMenu = new Size(640, 305);
        Point Panel1Loc = new Point(0, 0);
        Point Panel2Loc = new Point(640, 0);
        Point Panel3Loc = new Point(0, 305);
        Point Panel4Loc = new Point(640, 305);
        Int64 employeeid;

        //menu layout
        public void defaultMainMenuPosition()
        {
            //location
            panelMenu.Show();
            panelMenu.Dock = DockStyle.Fill;
            panelRecordEmployee1.Location = Panel1Loc;
            PanelEmpDirectory2.Location = Panel2Loc;
            PanelEmployeeJobAssignment3.Location = Panel3Loc;
            PanelEmployeeContract4.Location = Panel4Loc;
            //size
            panelRecordEmployee1.Size = panelMainMenu; PanelEmpDirectory2.Size = panelMainMenu; PanelEmployeeJobAssignment3.Size = panelMainMenu; PanelEmployeeContract4.Size = panelMainMenu;

        }

        public enum UIStateEnum
        {
            MainMenu,
            RecordEmployee,
            EmployeeDirectory,
            EmployeeContract,
            EmployeeJobAssignment
        }

        void ShowPanel(System.Windows.Forms.Panel p)
        {
            //hide everypanel
            panelMenu.Hide();
            panelMenu.Dock = DockStyle.None;

            //showtargetted panel
            p.Show();
            p.Dock = DockStyle.Fill;
        }

        private UIStateEnum _uistate;

        void UIState(UIStateEnum state)
        {
            _uistate = state;
            switch (_uistate)
            {
                case UIStateEnum.RecordEmployee:
                    
                    break;
                case UIStateEnum.EmployeeDirectory:
                    ShowPanel(empDir);

                    break;
                case UIStateEnum.EmployeeContract:
                    
                    break;
                case UIStateEnum.EmployeeJobAssignment:
                    
                    break;
                case UIStateEnum.MainMenu:
                    ShowPanel(panelMenu);
                    break;
            }
        }

        //call when user control first initialize
        public UCEmployee()
        {
            InitializeComponent();
            init(); 
        }

        public void init()
        {
            this.Size = defaultUcSize;
            defaultMainMenuPosition();
        }

        private void btnBackEmpDir_Click(object sender, EventArgs e)
        {
            UIState(UIStateEnum.MainMenu);
        }

        //menu animation
        public enum MainMenu
        {
            EmployeeRecord,
            EmployeeDirectory,
            EmployeeAssignment,
            EmployeeContract
        }

        private MainMenu menuChoosed;

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
                case MainMenu.EmployeeRecord:
                    panelRecordEmployee1.Size = new Size(xMax, yMax);
                    PanelEmpDirectory2.Size = new Size(433, 305);
                    PanelEmpDirectory2.Location = new Point(847, 0);
                    PanelEmployeeContract4.Size = new Size(433, 305);
                    PanelEmployeeContract4.Location = new Point(847, 305);
                    PanelEmployeeJobAssignment3.Size = new Size(848, 189);
                    PanelEmployeeJobAssignment3.Location = new Point(0, 421);
                    break;
                case MainMenu.EmployeeDirectory:
                    panelRecordEmployee1.Size = new Size(432, 305);
                    PanelEmpDirectory2.Size = new Size(xMax, yMax);
                    PanelEmpDirectory2.Location = new Point(432, 0);
                    PanelEmployeeContract4.Size = new Size(848, 189);
                    PanelEmployeeContract4.Location = new Point(432, 421);
                    PanelEmployeeJobAssignment3.Size = new Size(432, 305);
                    PanelEmployeeJobAssignment3.Location = new Point(0, 305);
                    break;
                case MainMenu.EmployeeAssignment:
                    panelRecordEmployee1.Size = new Size(848, 188);
                    PanelEmpDirectory2.Size = new Size(433, 305);
                    PanelEmpDirectory2.Location = new Point(847, 0);
                    PanelEmployeeContract4.Size = new Size(433, 305);
                    PanelEmployeeContract4.Location = new Point(847, 305);
                    PanelEmployeeJobAssignment3.Size = new Size(xMax, yMax);
                    PanelEmployeeJobAssignment3.Location = new Point(0, 188);
                    break;
                case MainMenu.EmployeeContract:
                    panelRecordEmployee1.Size = new Size(433, 305);
                    PanelEmpDirectory2.Size = new Size(848, 188);
                    PanelEmpDirectory2.Location = new Point(433, 0);
                    PanelEmployeeContract4.Size = new Size(xMax, yMax);
                    PanelEmployeeContract4.Location = new Point(433, 188);
                    PanelEmployeeJobAssignment3.Size = new Size(433, 305);
                    PanelEmployeeJobAssignment3.Location = new Point(0, 305);
                    break;
                default:
                    panelRecordEmployee1.Size = new Size(xDefault, yDefault);
                    PanelEmpDirectory2.Size = new Size(xDefault, yDefault);
                    PanelEmpDirectory2.Location = new Point(640, 0);
                    PanelEmployeeContract4.Size = new Size(xDefault, yDefault);
                    PanelEmployeeContract4.Location = new Point(640, 305);
                    PanelEmployeeJobAssignment3.Size = new Size(xDefault, yDefault);
                    PanelEmployeeJobAssignment3.Location = new Point(0, 305);
                    break;
            }
        }

        private void PanelEmpDirectory2_MouseEnter(object sender, EventArgs e)
        {
            MenuFocused(MainMenu.EmployeeDirectory);
        }

        private void panelRecordEmployee1_MouseEnter(object sender, EventArgs e)
        {
            MenuFocused(MainMenu.EmployeeRecord);

        }

        private void PanelEmployeeJobAssignment3_MouseEnter(object sender, EventArgs e)
        {
            MenuFocused(MainMenu.EmployeeAssignment);

        }

        private void PanelEmployeeContract4_MouseEnter(object sender, EventArgs e)
        {
            MenuFocused(MainMenu.EmployeeContract);

        }

        //menu clicked

        private void panelRecordEmployee1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void PanelEmpDirectory2_MouseClick(object sender, MouseEventArgs e)
        {
            UIState(UIStateEnum.EmployeeDirectory);
        }

        private void PanelEmployeeJobAssignment3_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void PanelEmployeeContract4_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dgEmployeeList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEmployeeList.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgEmployeeList.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgEmployeeList.Rows[selectedrowindex];
                string employeechoosed = Convert.ToString(selectedRow.Cells["Employee ID"].Value);
                employeeid = Convert.ToInt64(employeechoosed);
                ucEmployeeDetailed1.empID = employeeid;
                RightPanelDataReader();
            }
        }

        void RightPanelDataReader()
        {
            txtEmpIdBrief.Clear();
            txtBriefBirthdate.Clear();
            txtBriefEmpName.Clear();
            txtBriefNationality.Clear();
            txtBriefMobile.Clear();
            txbriefEmergency.Clear();
            txtBriefResidential.Clear();
            txtBriefRole.Clear();
            try
            {
                Db.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("select emp_id, emp_dob, emp_fullname, emp_roles, emp_joindate, emp_nationality, emp_mobile, emp_emergencycontactphone, emp_address, emp_nonsch1_institution, emp_nonsch1_designation, emp_nonsch2_institution, emp_nonsch2_designation, emp_ispursuingdegree, employee_pic, emp_schedu_name from employee_data where emp_id = @emp_id", Db.GetConnection());
                cmd.Parameters.Add("@emp_id", MySqlDbType.Int64).Value = employeeid;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtEmpIdBrief.Text = reader.GetInt64("emp_id").ToString();
                        txtBriefEmpName.Text = reader.GetString("emp_fullname");
                        DateTime birthdate = reader.GetDateTime("emp_dob");
                        txtBriefBirthdate.Text = birthdate.ToString("d");
                        txtBriefNationality.Text = reader.GetString("emp_nationality");
                        txtBriefMobile.Text = reader.GetString("emp_mobile");
                        txbriefEmergency.Text = reader.GetString("emp_emergencycontactphone");
                        txtBriefResidential.Text = reader.GetString("emp_address");
                        txtBriefRole.Text = reader.GetString("emp_roles");
                        try
                        {
                            empBriefPic.Image = Image.FromFile(reader.GetString("employee_pic"));
                        }
                        catch (Exception ex)
                        {
                            empBriefPic.Image = Resources.icons8_male_user_100;
                        }
                        //education panel
                        string lasteducationuniv, lastdesignation, pursuing;
                        lasteducationuniv = reader.GetString("emp_nonsch2_institution");
                        if(lasteducationuniv == "")
                        {
                            lasteducationuniv = reader.GetString("emp_nonsch1_institution");
                            lastdesignation = reader.GetString("emp_nonsch1_designation");
                            if(lasteducationuniv == "")
                            {
                                lasteducationuniv = reader.GetString("emp_schedu_name");
                                lastdesignation = "No Designation/Degree";
                            }
                        }
                        else
                        {
                            lastdesignation = reader.GetString("emp_nonsch2_designation");
                        }
                        pursuing = reader.GetString("emp_ispursuingdegree");
                        if(pursuing == "YES")
                        {
                            pursuing = "Currently pursuing degree";
                        }
                        else
                        {
                            pursuing = "Not pursuing any degree";
                        }
                        LblBriefEdu.Text = lasteducationuniv;
                        lblBriefDegree.Text = lastdesignation;
                        lblCurrentlyPursuingDegree.Text = pursuing;
                        PanelBriefEducationIn.Visible = true;
                    }
                }
                else
                {
                    PanelBriefEducationIn.Visible = false;
                }
                reader.Dispose();
                Db.CloseConnection();
            }
            catch (MySqlException ex)
            {
                PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

        //search filter

        public enum Searchby
        {
            Name,
            ID,
            Gender,
            Origin,
            Education
        }
        private Searchby _searchby;

        StringCollection strgender = new StringCollection() { "Male", "Female" };
        StringCollection strorigin = new StringCollection() { "Local", "Expatriate" };
        StringCollection strEdu = new StringCollection() { "Bachelor", "Magister", "Higher Secondary", "Pursuing Degree"};

        private void RadName_CheckedChanged(object sender, EventArgs e)
        {
            _searchby = Searchby.Name;
            SearchController();
        }

        private void RadID_CheckedChanged(object sender, EventArgs e)
        {
            _searchby = Searchby.ID;
            SearchController();
        }

        private void RadGender_CheckedChanged(object sender, EventArgs e)
        {
            _searchby = Searchby.Gender;
            SearchController();
        }

        private void RadOrigin_CheckedChanged(object sender, EventArgs e)
        {
            _searchby = Searchby.Origin;
            SearchController();
        }

        private void radEdu_CheckedChanged(object sender, EventArgs e)
        {
            _searchby = Searchby.Education;
            SearchController();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            switch (_searchby)
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
                case Searchby.Education:
                    SearchQuery(CmbSearch.SelectedValue.ToString());
                    break;
            }
        }

        void SearchController()
        {
            switch (_searchby)
            {
                case Searchby.Name:
                    txtSearchByName.Clear();
                    txtSearchByName.Visible = true;
                    CmbSearch.Visible = false;
                    break;
                case Searchby.ID:
                    txtSearchByName.Clear();
                    txtSearchByName.Visible = true;
                    CmbSearch.Visible = false;
                    break;
                case Searchby.Gender:
                    CmbSearch.DataSource = strgender;
                    CmbSearch.Visible = true;
                    txtSearchByName.Visible = false;
                    CmbSearch.SelectedIndex = 0;
                    break;
                case Searchby.Origin:
                    CmbSearch.DataSource = strorigin;
                    CmbSearch.Visible = true;
                    txtSearchByName.Visible = false;
                    CmbSearch.SelectedIndex = 0;
                    break;
                case Searchby.Education:
                    CmbSearch.DataSource = strEdu;
                    CmbSearch.Visible = true;
                    txtSearchByName.Visible = false;
                    CmbSearch.SelectedIndex = 0;
                    break;
            }
        }

        void SearchQuery(string where)
        {
            switch (_searchby)
            {
                case Searchby.Name:
                    try
                    {
                        Db.OpenConnection();
                        MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_fullname like '%" + where + "%' order by 'No.'", Db.GetConnection());
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bd = new BindingSource();
                        bd.DataSource = dt;
                        dgEmployeeList.DataSource = bd;
                        RightPanelDataReader();
                        Db.CloseConnection();
                        if (dgEmployeeList.Rows.Count < 1)
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
                        PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;
                case Searchby.ID:
                    try
                    {
                        Db.OpenConnection();
                        MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_id like '%" + where + "%' order by 'No.'", Db.GetConnection());
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        BindingSource bd = new BindingSource();
                        bd.DataSource = dt;
                        dgEmployeeList.DataSource = bd;
                        if (dgEmployeeList.Rows.Count < 1)
                        {
                            PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                        }
                        RightPanelDataReader();
                        Db.CloseConnection();
                        if (dgEmployeeList.Rows.Count < 1)
                        {
                            lblItsEmpty.Visible = true;
                        }
                        else
                        {
                            lblItsEmpty.Visible = false;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                    }
                    break;
                case Searchby.Gender:
                    if (where == "Male")
                    {
                        try
                        {
                            Db.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_gender = 'Male' order by 'No.'", Db.GetConnection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgEmployeeList.DataSource = bd;
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                                lblItsEmpty.Visible = true;
                                dgEmployeeList.Visible = false;

                            }
                            else
                            {
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                                lblItsEmpty.Visible = false;
                                dgEmployeeList.Visible = true;

                            }
                            RightPanelDataReader();
                            Db.CloseConnection();
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                                lblItsEmpty.Visible = true;
                                dgEmployeeList.Visible = false;
                            }
                            else
                            {
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                                lblItsEmpty.Visible = false;
                                dgEmployeeList.Visible = true;
                            }
                        }
                        catch (MySqlException ex)
                        {
                            PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    else
                    {
                        try
                        {
                            Db.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_gender = 'Female' order by 'No.'", Db.GetConnection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgEmployeeList.DataSource = bd;
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                dgEmployeeList.Visible = false;
                                panelEmpty.Visible = false;
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                            }
                            else
                            {
                                panelEmpty.Visible = false;
                                dgEmployeeList.Visible = true;
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                            }
                            RightPanelDataReader();
                            Db.CloseConnection();
                        }
                        catch (MySqlException ex)
                        {
                            PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    
                    break;
                case Searchby.Origin:
                    if(where == "Local")
                    {
                        try
                        {
                            Db.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_nationality like '%" + "Indonesia" + "%' order by 'No.'", Db.GetConnection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgEmployeeList.DataSource = bd;
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                                panelEmpty.Visible = true;
                                dgEmployeeList.Visible = false;
                            }
                            else
                            {
                                panelEmpty.Visible = false;
                                dgEmployeeList.Visible = true;
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                            }
                            RightPanelDataReader();
                            Db.CloseConnection();
                        }
                        catch (MySqlException ex)
                        {
                            PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    else
                    {
                        try
                        {
                            Db.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_nationality != 'Indonesia' order by 'No.'", Db.GetConnection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgEmployeeList.DataSource = bd;
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                                panelEmpty.Visible = true;
                                dgEmployeeList.Visible = false;
                            }
                            else
                            {
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                                panelEmpty.Visible = false;
                                dgEmployeeList.Visible = true;
                            }
                            RightPanelDataReader();
                            Db.CloseConnection();
                        }
                        catch (MySqlException ex)
                        {
                            PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    
                    break;
                case Searchby.Education:
                   if(where == "Bachelor")
                    {
                        try
                        {
                            Db.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_nonsch1_institution != '' and emp_nonsch2_institution = '' order by 'No.'", Db.GetConnection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgEmployeeList.DataSource = bd;
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                                dgEmployeeList.Visible = false;
                                panelEmpty.Visible = true;
                            }
                            else
                            {
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                                dgEmployeeList.Visible = true;
                                panelEmpty.Visible = false;
                            }
                            RightPanelDataReader();
                            Db.CloseConnection();
                        }
                        catch (MySqlException ex)
                        {
                            PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                   if(where == "Magister")
                    {
                        try
                        {
                            Db.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_nonsch1_institution != '' and emp_nonsch2_institution != '' order by 'No.'", Db.GetConnection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgEmployeeList.DataSource = bd;
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                                dgEmployeeList.Visible = false;
                                panelEmpty.Visible = true;
                            }
                            else
                            {
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                                dgEmployeeList.Visible = true;
                                panelEmpty.Visible = false;
                            }
                            RightPanelDataReader();
                            Db.CloseConnection();
                        }
                        catch (MySqlException ex)
                        {
                            PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    if(where == "Higher Secondary")
                    {
                        try
                        {
                            Db.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_nonsch1_institution = '' and emp_nonsch2_institution = '' order by 'No.'", Db.GetConnection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgEmployeeList.DataSource = bd;
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                                dgEmployeeList.Visible = false;
                                panelEmpty.Visible = true;
                            }
                            else
                            {
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                                dgEmployeeList.Visible = true;
                                panelEmpty.Visible = false;
                            }
                            RightPanelDataReader();
                            Db.CloseConnection();
                        }
                        catch (MySqlException ex)
                        {
                            PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    if(where == "Pursuing Degree")
                    {
                        try
                        {
                            Db.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand("set @row_number = 0; select @row_number:=(@row_number+1) AS 'No.', employee_data.emp_id as 'Employee ID', employee_data.emp_fullname as 'Full Name', employee_data.emp_department as 'Department', emp_roles as 'Role' from employee_data where emp_ispursuingdegree = 'YES' order by 'No.'", Db.GetConnection());
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            BindingSource bd = new BindingSource();
                            bd.DataSource = dt;
                            dgEmployeeList.DataSource = bd;
                            if (dgEmployeeList.Rows.Count < 1)
                            {
                                PopUp.Alert("Oops we couldn't find what you're looking for :( \nTry searching with different condition", frmAlert.AlertType.Warning);
                                dgEmployeeList.Visible = false;
                                panelEmpty.Visible = true;
                            }
                            else
                            {
                                PopUp.Alert("Here's all the info you need", frmAlert.AlertType.Success);
                                dgEmployeeList.Visible = true;
                                panelEmpty.Visible = false;
                            }
                            RightPanelDataReader();
                            Db.CloseConnection();
                        }
                        catch (MySqlException ex)
                        {
                            PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
                        }
                    }
                    break;
                default:
                    PopUp.Alert("No search criteria was specified", frmAlert.AlertType.Info);
                    break;
            }
        }

        private void btnEmpEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnEmpDetail_Click(object sender, EventArgs e)
        {
            ucEmployeeDetailed1.BringToFront();
            ucEmployeeDetailed1.Dock = DockStyle.Fill;
            ucEmployeeDetailed1.UIState(UCEmployeeDetailed.UIStateEmployeeDetailed.Personal);
            ucEmployeeDetailed1.LoadEmployeeData();
        }
    }
}
