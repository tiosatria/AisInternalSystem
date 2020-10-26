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
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.WinForms.Controls.Spreadsheet.Worksheets.Layers;
using System.Windows.Navigation;

namespace AisInternalSystem
{
    public partial class UCMaster : UserControl
    {
        private byte terms = 0;
        private int? uniqueKey = null, NewUniqueKey = null;
        public UCMaster()
        {
            InitializeComponent();
            LoadSchoolYear("Ongoing");
            LoadSchoolYear("Finished");
            txtAyearCode.Text = AcademicYearCodeFiller();
            MenuSwitcher(NavSwitcher.Home);
        }

        private void LoadSchoolYear(string str)
        {
            if(str == "Ongoing")
            {
                FLowOngoingSchoolYear.Controls.Clear();
                DataTable dt = QueryProcessor.Load(QueryProcessor.Process.Master, new string[1] { str });
                if (dt.Rows.Count >= 1)
                {
                    UIMember_AcademicYear[] ongoingSchoolYear = new UIMember_AcademicYear[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        terms = Convert.ToByte(dt.Rows[i][3].ToString());
                        uniqueKey = Convert.ToInt32(dt.Rows[i][5].ToString());
                        ongoingSchoolYear[i] = new UIMember_AcademicYear();
                        ongoingSchoolYear[i].ay = $"Academic Year: {dt.Rows[i][2].ToString()}";
                        ongoingSchoolYear[i].ayCode = $"Academic Year Code: {dt.Rows[i][1].ToString()}";
                        ongoingSchoolYear[i].ayStatus = $"Status: {dt.Rows[i][4].ToString()}";
                        ongoingSchoolYear[i].ayTerms = $"Terms: {dt.Rows[i][3].ToString()}";
                        FLowOngoingSchoolYear.Controls.Add(ongoingSchoolYear[i]);
                    }
                }
                else
                {
                    Msg.Alert("Currently, there are no ongoing schoolyear", frmAlert.AlertType.Error);
                }
            }
            else if(str == "Finished")
            {
                DataTable dt = QueryProcessor.Load(QueryProcessor.Process.Master, new string[1] { str });
                flowPastSchool.Controls.Clear();
                if (dt.Rows.Count >= 1)
                {
                    UIMember_AcademicYear[] ongoingSchoolYear = new UIMember_AcademicYear[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ongoingSchoolYear[i] = new UIMember_AcademicYear();
                        ongoingSchoolYear[i].ay = $"Academic Year: {dt.Rows[i][2].ToString()}";
                        ongoingSchoolYear[i].ayCode = $"Academic Year Code: {dt.Rows[i][1].ToString()}";
                        ongoingSchoolYear[i].ayStatus = $"Status: {dt.Rows[i][4].ToString()}";
                        ongoingSchoolYear[i].ayTerms = $"Terms: {dt.Rows[i][3].ToString()}";
                        flowPastSchool.Controls.Add(ongoingSchoolYear[i]);
                    }
                }
                else
                {
                    Msg.Alert("Currently, there are no ongoing schoolyear", frmAlert.AlertType.Error);
                }
            }
        }

        private void SidePanel(SidePanelSwitcher nav)
        {
            _sidepanel = nav;
            switch (_sidepanel)
            {
                case SidePanelSwitcher.Ongoing:
                    btnOngoing.FillColor = Color.Black;
                    btnOngoing.ForeColor = Color.White;
                    btnPastSchool.FillColor = Color.Silver;
                    btnPastSchool.ForeColor = Color.Black;
                    panelOngoingSchool.BringToFront();
                    break;
                case SidePanelSwitcher.Past:
                    btnOngoing.FillColor = Color.Silver;
                    btnOngoing.ForeColor = Color.Black;
                    btnPastSchool.FillColor = Color.Black;
                    btnPastSchool.ForeColor = Color.White;
                    PanelPastSchoolYear.BringToFront();
                    break;
                default:
                    btnOngoing.FillColor = Color.Black;
                    btnOngoing.ForeColor = Color.White;
                    btnPastSchool.FillColor = Color.Silver;
                    btnPastSchool.ForeColor = Color.Black;
                    panelOngoingSchool.BringToFront();
                    break;
            }
        }

        private NavSwitcher _menu;

        private void MenuSwitcher(NavSwitcher nav)
        {
            _menu = nav;
            switch (nav)
            {
                case NavSwitcher.Home:
                    PanelHome.BringToFront();
                    SidePanel(_sidepanel);
                    break;
                case NavSwitcher.Proceed:
                    PanelStartNewConfirm.BringToFront();
                    PanelHome.SendToBack();
                    break;
                case NavSwitcher.Confirm:
                    PanelStartNewSchoolLastStep.BringToFront();
                    txtPinNew.Text = NewUniqueKey.ToString();
                    break;
                default:

                    break;
            }
        }

        private void MasterDataValidation()
        {
            switch (_menu)
            {
                case NavSwitcher.Home:
                    if(txtAyearCode.Text == "" || txtAyearCode.Text == string.Empty)
                    {
                        Msg.Alert("Not valid academic year code!", frmAlert.AlertType.Error);
                    }
                    else if (Convert.ToByte(dropTerms.SelectedItem.ToString()) <= terms)
                    {
                        Msg.Alert("You cannot start this academic year\non the same/previous terms!", frmAlert.AlertType.Error);
                    }
                    else
                    {
                        MenuSwitcher(NavSwitcher.Proceed);
                    }
                    break;
                case NavSwitcher.Proceed:
                    if(txtEnterLastSchPin.Text == uniqueKey.ToString())
                    {
                        NewUniqueKey = QueryProcessor.GetRandomNumber(QueryProcessor.Process.Master);
                        MenuSwitcher(NavSwitcher.Confirm);
                    }
                    else
                    {
                        Msg.Alert("You entered the wrong key", frmAlert.AlertType.Error);
                    }
                    break;
                case NavSwitcher.Confirm:

                    break;

            }
        }

        private string AcademicYearCodeFiller()
        {
            string str = string.Empty;
           if(dropAyear.SelectedItem != "" || dropAyear.SelectedItem == null)
            {
                str = $"AY{dropAyear.SelectedItem.ToString()}TERM{dropTerms.SelectedItem.ToString()}";
            }
            return str;
        }

        public enum NavSwitcher
        {
            Home, Proceed, Confirm
        }

        private SidePanelSwitcher _sidepanel;
        public enum SidePanelSwitcher
        { 
            Ongoing, Past
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            MasterDataValidation();
        }

        private void btnBackto_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAyearCode.Text = AcademicYearCodeFiller();
        }

        private void btnPastSchool_Click(object sender, EventArgs e)
        {
            SidePanel(SidePanelSwitcher.Past);
        }

        private void btnOngoing_Click(object sender, EventArgs e)
        {
            SidePanel(SidePanelSwitcher.Ongoing);
        }

        private void dropTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAyearCode.Text = AcademicYearCodeFiller();
        }

        private void btnBackToPrev_Click(object sender, EventArgs e)
        {
            MenuSwitcher(NavSwitcher.Home);
        }

        private void btnUnderstand_Click(object sender, EventArgs e)
        {
            MasterDataValidation();
        }

        private void btnStartNewSchoolYear_Click(object sender, EventArgs e)
        {
            Msg.Alert("You cannot start new school year\nError Code [Forbidden]", frmAlert.AlertType.Error);
        }

        private void BtnCancelProgress_Click(object sender, EventArgs e)
        {
            MenuSwitcher(NavSwitcher.Home); 
        }
    }
}
