using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Module;
using AisInternalSystem.Controller;

namespace AisInternalSystem
{
    public partial class DashboardUC : UserControl
    {
        private bool IsLoaded = false;
        DialogControl confirmation = new DialogControl();

        public DashboardUC()
        {

        }
        public void InitializeObject()
        {
            if (IsLoaded)
            {

            }
            else
            {
                InitializeComponent();
                this.Visible = false;
                UIController.Animate(this, Guna.UI2.AnimatorNS.AnimationType.Transparent);
                SwitchDashboard();
            }
            IsLoaded = true;
        }
        private void DashboardUC_Load(object sender, EventArgs e)
        {

        }

        private void BtnAddTodo_Click(object sender, EventArgs e)
        {
            //add in future
            PopUp.Alert("We're still working on finishing it!", frmAlert.AlertType.Info);
        }
        #region properties
        private string _username;

        public string NameUser
        {
            get { return _username; }
            set { _username = value; lblNameUser.Text = $"Hello, {value }\nLet's get some work done today!"; }
        }
        private string _role;

        public string UserRole
        {
            get { return _role; }
            set { _role = value; }
        }
        private Point PanelActivitiesExpandLocation = new Point(853, 91);
        private Point ButtonActivitiesExpandLocation = new Point(814, 319);
        private Point PanelActivitiesShrinkLocation = new Point();
        private Point ButtonActivitiesShrinkLocation = new Point(1252, 319);
        #endregion

        private void lblViewAllActivities_Click(object sender, EventArgs e)
        {
            PopUp.Alert("This feature will be up soon!", frmAlert.AlertType.Info);
        }

        public enum RightPanelActivities
        {
            Shrink, 
            Expand
        }

        private RightPanelActivities _rightActivities;

        private void ActivitiesSwitcher()
        {
            switch (role)
            {
                case User.RoleIdentifier.Management:

                    break;
                case User.RoleIdentifier.Admin:

                    break;
                case User.RoleIdentifier.IT:

                    break;
                case User.RoleIdentifier.Teacher:

                    break;
                case User.RoleIdentifier.Accounting:

                    break;
            }
            Entities.Activities.InitActivities(role);
            dropActivities.DataSource = Entities.Activities.ActivityTypeString;
            dropActivities.SelectedIndex = 0;
        }

        private void RightActivitiesExpand()
        {
            PanelActivities.BringToFront();
            _rightActivities = RightPanelActivities.Expand;
            UIController.AnimateShadowPanel(PanelActivities, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
            btnShrinkExpand.Visible = false;
            btnShrinkExpand.Location = ButtonActivitiesExpandLocation;
            btnShrinkExpand.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            UIController.AnimateControl(btnShrinkExpand, Guna.UI2.AnimatorNS.AnimationType.Transparent);
        }

        private void RightActivitiesShrink()
        {
            _rightActivities = RightPanelActivities.Shrink;
            UIController.AnimateHideShadowPanel(PanelActivities, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
            btnShrinkExpand.Location = ButtonActivitiesShrinkLocation;
            btnShrinkExpand.Image = Properties.Resources.chevronleft;
            btnShrinkExpand.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
            UIController.AnimateControl(btnShrinkExpand, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
            PanelActivities.SendToBack();
        }
        private User.RoleIdentifier role = Data.user._role;
        private void SwitchDashboard()
        {
            dropOverview.DataSource = UserInterface.Menu.Menus.Overview(role);
            try
            {
                dropOverview.SelectedIndex = 0;
            }
            catch (Exception)
            {
                PopUp.Alert("No Menu Overview for this department, yet!", frmAlert.AlertType.Warning);
            }
            ActivitiesSwitcher();
        }

        private void btnShrinkExpand_Click(object sender, EventArgs e)
        {
            switch (_rightActivities)
            {
                case RightPanelActivities.Shrink:
                    RightActivitiesExpand();
                    break;
                case RightPanelActivities.Expand:
                    RightActivitiesShrink();
                    break;
                default:
                    RightActivitiesExpand();
                    break;
            }
        }
        private void OverviewStudent()
        {
            //TotalActiveStudent
            DataTable TotalActive = Entities.Student.GetStudentOverview("GetActiveStudentCount");
            int kinder = 0;
            int primary = 0;
            int lowsec = 0;
            int highsec = 0;
            int unassigned = 0;
            try
            {
                kinder = Convert.ToInt32(TotalActive.Rows[1][0].ToString());
            }
            catch (Exception)
            {
                kinder = 0;
            }
            try
            {
                primary = Convert.ToInt32(TotalActive.Rows[2][0].ToString());
            }
            catch (Exception)
            {
                primary = 0;
            }
            try
            {
                lowsec = Convert.ToInt32(TotalActive.Rows[3][0].ToString()); 
            }
            catch (Exception)
            {
                lowsec = 0;
            }
            try
            {
                highsec = Convert.ToInt32(TotalActive.Rows[4][0].ToString());
            }
            catch (Exception)
            {
                highsec = 0;
            }
            try
            {
                unassigned = Convert.ToInt32(TotalActive.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                unassigned = 0;
            }
            int totalStudent = kinder + primary + lowsec + highsec + unassigned;
            lblTotalActiveStudent.Text = totalStudent.ToString();
            lblKindergarten.Text = "Kindergarten: " + kinder.ToString();
            lblPrimary.Text = "Primary: " + primary.ToString();
            lblLowerSec.Text = "Lower Secondary: " + lowsec.ToString();
            lblHigherSec.Text = "Higher Secondary: " + highsec.ToString();
            //TotalActiveClass
            DataTable ActiveClass = Entities.ClassRoom.ActiveClassName();
            lbltotalACtiveClass.Text = ActiveClass.Rows.Count.ToString();
            listClass.DataSource = ActiveClass;
            listClass.DisplayMember = "classname";
            //TotalOutStudent
            DataTable OutStudent = Entities.Student.GetStudentOverview("GetOutStudentCount");
            int graduate = 0;
            int suspend = 0;
            int transfer = 0;
            int defer = 0;
            try
            {
                graduate = Convert.ToInt32(OutStudent.Rows[4][0].ToString());
            }
            catch (Exception)
            {
                graduate = 0;   
            }
            try
            {
                suspend = Convert.ToInt32(OutStudent.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                suspend = 0;
            }
            try
            {
                transfer = Convert.ToInt32(OutStudent.Rows[1][0].ToString()) + Convert.ToInt32(OutStudent.Rows[3][0].ToString());
            }
            catch (Exception)
            {
                try
                {
                    transfer = Convert.ToInt32(OutStudent.Rows[1][0].ToString());
                }
                catch (Exception)
                {
                    transfer = 0;
                }
            }
            try
            {
                defer = Convert.ToInt32(OutStudent.Rows[2][0].ToString());
            }
            catch (Exception)
            {
                defer = 0;
            }
            int totalOut = graduate + suspend + transfer + defer;
            lblTotalOutStudent.Text = totalOut.ToString();
            lblGrad.Text = "Graduate: " + graduate.ToString();
            lblSuspend.Text = "Suspend: " + suspend.ToString();
            lblTransfer.Text = "Transfer: " + transfer.ToString();
            lblDefer.Text = "Defer: " + defer.ToString();
            //TotalDataInput
            DataTable inputter = Entities.Student.GetStudentOverview("GetStudentDataInputSummary");
            dgInputter.DataSource = inputter;
            int TotalInputter = 0;
            for (int i = 0; i < inputter.Rows.Count; i++)
            {
                TotalInputter += Convert.ToInt32(inputter.Rows[i][1].ToString());
            }
            lbltotaldatainput.Text = TotalInputter.ToString();
            UIController.AnimateControl(PanelOverViewStudent, Guna.UI2.AnimatorNS.AnimationType.Scale);
        }

        private void InitDasboard(string str)
        {
            switch (str)
            {
                case "Student Overview":
                    OverviewStudent();
                    break;
                case "Accounting Overview":
                    PopUp.Alert("This overview will be available soon!", frmAlert.AlertType.Info);
                    dropOverview.SelectedIndex = 0;
                    break;
                case "Employee Overview":
                    PopUp.Alert("This overview will be available soon!", frmAlert.AlertType.Info);
                    dropOverview.SelectedIndex = 0;
                    break;
            }
        }

        private void dropOverview_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDasboard(dropOverview.SelectedItem.ToString());
        }
    }
}
