using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AisInternalSystem.Module;
using System.Runtime.CompilerServices;
using Guna.UI2.AnimatorNS;
using AisInternalSystem.Properties;
using System.Runtime.Remoting.Channels;
using Microsoft.VisualBasic;
using MySqlX.XDevAPI;
using Telerik.WinControls.UI;

namespace AisInternalSystem
{
    public partial class Dashboard : Form
    {
        double appVer = 2.5, appVerDb;
        bool isExit, loaded = false;
        public static int ownerId;
        public static string ownerName, username, role, userPhotoPath;
        public bool isLoggedIn;
        public static string SelectedString;
        public static string[] DropDownListAy, Classname;
        public enum RoleState
        {
            Administration,
            Accounting,
            Management
        }
        MySqlCommand command;
        public static DataTable ClassList;
        DialogControl confirmation = new DialogControl();
        DashboardUC ucDashboard = new DashboardUC();                                                                                            
        UCInventory UCInventory = new UCInventory();
        UCEmployee UCEmployee = new UCEmployee();
        UCFeeDback UCFeedback = new UCFeeDback();
        PleaseWait waitform = new PleaseWait();
        LoginFrm loginfrm = new LoginFrm();
        public UCSchoolAdm UCSchoolAdm = new UCSchoolAdm();
        Dialog msg = new Dialog();

        public Dashboard()
        {
            InitializeComponent();
            waitform.Show();
            CheckForUpdate();
            loginFrm1.Focus();
            loginFrm1.txt_username.Select();
        }

        void CheckForUpdate()
        {
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select max(ver) as 'ver' from app_ver", Db.get_connection());
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    appVerDb = reader.GetDouble("ver");
                }
                reader.Close();
                if(appVerDb>appVer)
                {
                    loaded = true;
                    confirmation.dialodMsg("YAAY We found an update for you!", "Our system programmer just launch some system update\nUpdate to get the latest features and stay out of bugs!\nDo you want to update now?", "Yes, keep me updated!", "No, let me do my job", Resources.update, DialogControl.Handlerstate.Updatesystem, this.PanelContainer.Controls.IndexOf(loginfrm));
                    PanelContainer.Controls.Add(confirmation);
                    PanelContainer.Controls["DialogControl"].BringToFront();
                }
                else
                {
                    loaded = true;
                    msg.Alert("Succesfully connected to AIS SERVER", frmAlert.AlertType.Info);
                    msg.Alert("Please introduce yourself to continue :)", frmAlert.AlertType.Info);
                }
                waitform.Close();
                this.BringToFront();
                this.Activate();
            }
            catch (MySqlException ex)
            {
                msg.Alert(ex.Message, frmAlert.AlertType.Error);
                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            isLoggedIn = false;
            BtnExit.Visible = true;
        }

        //preload
        public void init()
        {
            if(isLoggedIn = true)
            {
                NavSwitcher(NavigationState.Dashboard);
            }
            else
            {
                MessageBox.Show("Intruder detected, application will now closing", "Intruder detected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private RoleState roleState;

        public void RoleSwitcher(RoleState str)
        {
            switch (roleState)
            {
                case RoleState.Administration:
                    panel_default.Hide();
                    panel_Upper_admin.Show();
                    PanelContainer.Controls.Clear();
                    PanelContainer.Controls.Add(ucDashboard);
                    PanelContainer.Controls["DashboardUC"].BringToFront();
                    DragControl.TargetControl = panel_Upper_admin;
                    BtnDashboardAdmin.FillColor = Color.Black;
                    BtnDashboardAdmin.ForeColor = Color.White;
                    break;
                case RoleState.Accounting:

                    break;
                case RoleState.Management:

                    break;
            }
        }


        


        private NavigationState Navigation;

        public enum NavigationState
        {
            Login,
            Dashboard,
            Inventory,
            Employee,
            SchoolAdministration, 
            Feedback
        }

        public void NavSwitcher(NavigationState str)
        {
            Navigation = str;
            bool inventLoaded, dashLoaded;
            switch (Navigation)
            {
                case NavigationState.Dashboard:
                    PanelContainer.Controls.Add(ucDashboard);
                    PanelContainer.Controls[PanelContainer.Controls.IndexOf(ucDashboard)].BringToFront();
                    BtnDashboardAdmin.FillColor = Color.Black;
                    BtnDashboardAdmin.ForeColor = Color.White;
                    BtnInventoryAdmin.FillColor = Color.White;
                    BtnInventoryAdmin.ForeColor = Color.Black;
                    BtnEmployeeAdmin.FillColor = Color.White;
                    BtnEmployeeAdmin.ForeColor = Color.Black;
                    BtnSchoolAdmin.FillColor = Color.White;
                    BtnSchoolAdmin.ForeColor = Color.Black;
                    break;
                case NavigationState.Inventory:
                    PanelContainer.Controls.Add(UCInventory);
                    PanelContainer.Controls["UCInventory"].BringToFront();
                    BtnDashboardAdmin.FillColor = Color.White;
                    BtnDashboardAdmin.ForeColor = Color.Black;
                    BtnInventoryAdmin.FillColor = Color.Black;
                    BtnInventoryAdmin.ForeColor = Color.White;
                    BtnEmployeeAdmin.FillColor = Color.White;
                    BtnEmployeeAdmin.ForeColor = Color.Black;
                    BtnSchoolAdmin.FillColor = Color.White;
                    BtnSchoolAdmin.ForeColor = Color.Black;
                    break;
                case NavigationState.Employee:
                    PanelContainer.Controls.Clear();
                    PanelContainer.Controls.Add(UCEmployee);
                    PanelContainer.Controls[PanelContainer.Controls.IndexOf(UCEmployee)].BringToFront();
                    BtnEmployeeAdmin.FillColor = Color.Black;
                    BtnEmployeeAdmin.ForeColor = Color.White;
                    BtnDashboardAdmin.FillColor = Color.White;
                    BtnDashboardAdmin.ForeColor = Color.Black;
                    BtnInventoryAdmin.FillColor = Color.White;
                    BtnInventoryAdmin.ForeColor = Color.Black;
                    BtnSchoolAdmin.FillColor = Color.White;
                    BtnSchoolAdmin.ForeColor = Color.Black;
                    break;
                case NavigationState.SchoolAdministration:
                    PanelContainer.Controls.Clear();
                    PanelContainer.Controls.Add(UCSchoolAdm);
                    PanelContainer.Controls[PanelContainer.Controls.IndexOf(UCSchoolAdm)].BringToFront();
                    BtnEmployeeAdmin.FillColor = Color.White;
                    BtnEmployeeAdmin.ForeColor = Color.Black;
                    BtnDashboardAdmin.FillColor = Color.White;
                    BtnDashboardAdmin.ForeColor = Color.Black;
                    BtnInventoryAdmin.FillColor = Color.White;
                    BtnInventoryAdmin.ForeColor = Color.Black;
                    BtnSchoolAdmin.FillColor = Color.Black;
                    BtnSchoolAdmin.ForeColor = Color.White;
                    break;
                case NavigationState.Feedback:

                    PanelContainer.Controls.Add(UCFeedback);
                    PanelContainer.Controls[PanelContainer.Controls.IndexOf(UCFeedback)].BringToFront();
                    BtnEmployeeAdmin.FillColor = Color.White;
                    BtnEmployeeAdmin.ForeColor = Color.Black;
                    BtnDashboardAdmin.FillColor = Color.White;
                    BtnDashboardAdmin.ForeColor = Color.Black;
                    BtnInventoryAdmin.FillColor = Color.White;
                    BtnInventoryAdmin.ForeColor = Color.Black;
                    BtnSchoolAdmin.FillColor = Color.White;
                    BtnSchoolAdmin.ForeColor = Color.Black;
                    break;
                default:
                    break;
            }
        }

        void ShowPanel()
        {
            PanelContainer.Controls["UCEmployee"].Hide();
            PanelContainer.Controls["DashboardUC"].Hide();
        }

        private void loginFrm1_Load(object sender, EventArgs e)
        {

        }

        public enum state
        {
            Exit, Login
        }

        private state StateAction;

        private void BtnDashboardAdmin_Click(object sender, EventArgs e)
        {
            NavSwitcher(NavigationState.Dashboard);
        }

        private void BtnInventoryAdmin_Click(object sender, EventArgs e)
        {
            msg.Alert("This Feature will arrive soon!", frmAlert.AlertType.Info);
            //NavSwitcher(NavigationState.Inventory);

        }

        private void BtnEmployeeAdmin_Click(object sender, EventArgs e)
        {
            NavSwitcher(NavigationState.Employee);

        }

        private void BtnSchoolAdmin_Click(object sender, EventArgs e)
        {
            NavSwitcher(NavigationState.SchoolAdministration);
        }

        private void picThumbUser_Click(object sender, EventArgs e)
        {
            msg.Alert("You look really good today! :)", frmAlert.AlertType.Info);
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel_default_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_Upper_admin_MouseEnter(object sender, EventArgs e)
        {

        }

        private void Btn_Notif_MouseEnter(object sender, EventArgs e)
        {
            Btn_Notif.Image = Resources.notif_animated;
            Btn_Notif.Width = Btn_Notif.Width + 10;
            Btn_Notif.Height = Btn_Notif.Height + 10;
        }

        private void Btn_Notif_MouseLeave(object sender, EventArgs e)
        {
            Btn_Notif.Image = Resources.notif_freeze;
            Btn_Notif.Width = Btn_Notif.Width - 10;
            Btn_Notif.Height = Btn_Notif.Height - 10;
        }

        private void Btn_Exit_MouseEnter(object sender, EventArgs e)
        {
            BtnExit.Width = BtnExit.Width + 10;
            BtnExit.Height = BtnExit.Height + 10;
        }

        private void Btn_Exit_MouseLeave(object sender, EventArgs e)
        {
            BtnExit.Image = Resources.exit_static;

        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {

        }

        private void picBtnExit_MouseEnter(object sender, EventArgs e)
        {
            picBtnExit.Width = picBtnExit.Width + 10;
            picBtnExit.Height = picBtnExit.Height + 10;
        }

        private void picBtnExit_MouseLeave(object sender, EventArgs e)
        {
            picBtnExit.Width = picBtnExit.Width - 10;
            picBtnExit.Height = picBtnExit.Height - 10;

        }

        private void Btn_Notif_Click(object sender, EventArgs e)
        {
            msg.Alert("This feature will come soon!", frmAlert.AlertType.Info);
        }

        private void btnMsg_Click(object sender, EventArgs e)
        {
            msg.Alert("This feature will come soon!", frmAlert.AlertType.Info);

        }

        public void FuncExit()
        {
            int ctrl = 0;
            if (BtnSchoolAdmin.FillColor == Color.Black)
            {
                ctrl = this.PanelContainer.Controls.IndexOf(UCSchoolAdm);
            }
            if(BtnDashboardAdmin.FillColor == Color.Black)
            {
                ctrl = this.PanelContainer.Controls.IndexOf(ucDashboard);
            }
            if (BtnEmployeeAdmin.FillColor == Color.Black)
            {
                ctrl = this.PanelContainer.Controls.IndexOf(UCEmployee);
            }
            PanelContainer.Controls.Add(confirmation);
            confirmation.dialodMsg("Taking a break?", "We all deserve a break, but don't forget that any unsaved data will be lost!", "Yes, get me out of here!", "No, i misclicked it, let me stay", Resources.icons8_question_mark_480px, DialogControl.Handlerstate.Exit, ctrl);
            PanelContainer.Controls[this.PanelContainer.Controls.IndexOf(confirmation)].BringToFront();
        }

        private void picBtnExit_Click(object sender, EventArgs e)
        {
            FuncExit();
        }

        private void btnMsg_MouseEnter(object sender, EventArgs e)
        {
            btnMsg.Width = btnMsg.Width + 10;
            btnMsg.Height = btnMsg.Height + 10;
        }

        private void btnFeedback_MouseEnter(object sender, EventArgs e)
        {

        }

        

        private void btnFeedback_MouseLeave(object sender, EventArgs e)
        {
            btnFeedback.Width = btnFeedback.Width + 10;
            btnFeedback.Height = btnFeedback.Height + 10;
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {

        }

        private void btnFeedback_MouseEnter_1(object sender, EventArgs e)
        {
            btnFeedback.Width = btnFeedback.Width + 10;
            btnFeedback.Height = btnFeedback.Height + 10;
        }

        private void btnFeedback_MouseLeave_1(object sender, EventArgs e)
        {
            btnFeedback.Width = btnFeedback.Width - 10;
            btnFeedback.Height = btnFeedback.Height - 10;
        }

        void ParentEnterFunction()
        {
            switch (Navigation)
            {
                case NavigationState.Dashboard:

                    break;
                case NavigationState.Inventory:

                    break;
                case NavigationState.Employee:

                    break;
                case NavigationState.SchoolAdministration:
                    UCSchoolAdm.FunctionEnter();
                    break;
                case NavigationState.Feedback:

                    break;
                default:
                    if(!isLoggedIn)
                    {
                        loginFrm1.Execute_Login();
                    }
                    break;
            }
        }

        private void Dashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ParentEnterFunction();
            }
        }

        private void btnFeedback_Click_1(object sender, EventArgs e)
        {
            NavSwitcher(NavigationState.Feedback);
        }

        private void btnMsg_MouseLeave(object sender, EventArgs e)
        {
            btnMsg.Width = btnMsg.Width - 10;
            btnMsg.Height = btnMsg.Height - 10;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if(BtnExit.Text == "Login")
            {
                StateAction = state.Login;
            }
            if(BtnExit.Text == "Exit")
            {
                StateAction = state.Exit;
            }
            switch (StateAction)
            {
                case state.Exit:
                    BtnExit.Text = "Login";
                    PanelContainer.Controls.Add(confirmation);
                    confirmation.dialodMsg("Taking a break?", "We all deserve a break, but don't forget that any unsaved data will be lost!", "Yes, get me out of here!", "No, i misclicked it, let me stay", Resources.icons8_question_mark_480px, DialogControl.Handlerstate.Exit, this.PanelContainer.Controls.IndexOf(loginFrm1)); ;
                    PanelContainer.Controls["DialogControl"].BringToFront();
                    break;
                case state.Login:
                    BtnExit.Text = "Exit";
                    PanelContainer.Controls["loginFrm1"].BringToFront();
                    break;
            }
        }

        private void panel_Upper_admin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelContainer_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
