using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using AisInternalSystem.Entities.Enquiries;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Enquiries
{
    public partial class ReviewEnquiriesUC : UserControl
    {
        private bool isLoaded = false;
        private Entities.Enquiries.Enquiries CurrentEnq = null;
        public ReviewEnquiriesUC()
        {

        }
        public void InitObject()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                isLoaded = true;
                ChangeFilterBy(FilterBy.Status);
                InitDGData();
            }
            else
            {

            }
        }

        #region Function
        private void Pointer(int i)
        {
            CurrentEnq = Entities.Enquiries.Enquiries.Get(i);
            switch (CurrentEnq.EnquiryStatus)
            {
                case "Initiate":
                    containerinit.BringToFront();
                    OpenedPanel(new List<Panel> { pinit });
                    break;
                case "Test Schedule":
                    PanelScheduleTest.BringToFront();
                    OpenedPanel(new List<Panel> { containerinit, PanelScheduleTest });
                    break;
                case "Test":
                    PanelTestResult.BringToFront();
                    OpenedPanel(new List<Panel> { containerinit, PanelTestResult });
                    break;
                case "Academic Review":
                    PanelAcademicReview.BringToFront();
                    OpenedPanel(new List<Panel> { containerinit, PanelTestResult, PanelAcademicReview });
                    break;
                case "Document Review":
                    

                    break;
                case "Payment Settlement":
                    PanelPaymentSettlement.BringToFront();
                    OpenedPanel(new List<Panel> { containerinit, PanelTestResult, PanelAcademicReview, PanelPaymentSettlement });
                    break;
                case "Material Delivery":
                    
                    break;
                case "Finalization":

                    break;
                case "Cancelled":

                    break;
                case "Finished":

                    break;
            }
        }
        private void OpenedPanel(List<Panel> panels)
        {
            foreach (Panel item in panelprocedureindicator.Controls)
            {
                item.BackColor = Color.Silver;
            }
            foreach (Panel panel in panels)
            {
                panel.BackColor = Color.GhostWhite;
            }
        }
        private void checkForEmptyRowPotential()
        {
            if (dgPotList.Rows.Count < 1)
            {
                PanelPotentialStudentData.Visible = false;
                PanelNotFoundPotential.BringToFront();
                PanelNotFoundPotential.Visible = true;
            }
            else
            {
                PanelNotFoundPotential.Visible = false;
                PanelPotentialStudentData.Visible = true;
                PanelPotentialStudentData.BringToFront();
            }
        }
        private void InitDGData()
        {
            dgPotList.DataSource = Entities.Enquiries.Enquiries.GetDataSourceByFilter(FilterBy.Status, "ongoing");
            checkForEmptyRowPotential();
        }
        #endregion

        private void btnSearchPar_Click(object sender, EventArgs e)
        {
            switch (_filter)
            {
                case FilterBy.Name:
                    filterKey = txtSearchPar.Text;
                    break;
                case FilterBy.Status:
                    filterKey = dropkey.Text;
                    break;
                case FilterBy.Procedure:
                    filterKey = dropkey.Text;
                    break;
                case FilterBy.Grade:
                    filterKey = dropkey.Text;
                    break;
            }
            dgPotList.DataSource =  Entities.Enquiries.Enquiries.GetDataSourceByFilter(_filter, filterKey);
            checkForEmptyRowPotential();
        }

        private FilterBy _filter;
        private string filterKey;


        public enum FilterBy
        {
            Name, Status, Procedure, Grade
        }
        private void ChangeFilterBy(FilterBy filter)
        {
            _filter = filter;
            switch (filter)
            {
                case FilterBy.Name:
                    txtSearchPar.Clear();
                    txtSearchPar.BringToFront();
                    filterKey = "";
                    break;
                case FilterBy.Status:
                    dropkey.BringToFront();
                    dropkey.DataSource = StatusItem;
                    dropkey.SelectedIndex = 0;
                    filterKey = dropkey.Text;
                    break;
                 case FilterBy.Procedure:
                    dropkey.BringToFront();
                    dropkey.DataSource = ProcedureItem;
                    dropkey.SelectedIndex = 0;
                    filterKey = dropkey.Text;
                    break;
                case FilterBy.Grade:
                    dropkey.BringToFront();
                    dropkey.DataSource = Controller.Data.grades;
                    dropkey.DisplayMember = "GradeName";
                    dropkey.SelectedIndex = 0;
                    filterKey = dropkey.Text;
                    break;
                default:
                    dropkey.BringToFront();
                    dropkey.DataSource = StatusItem;
                    dropkey.SelectedIndex = 0;
                    filterKey = dropkey.Text;
                    break;
            }
        }

        private List<string> StatusItem = new List<string>() { "FINISHED", "ONGOING", "CANCELLED" };
        private List<string> ProcedureItem = new List<string>() { "INITIATE", "TEST", "ACADEMIC REVIEW", "DOCUMENT REVIEW", "PAYMENT SETTLEMENT", "MATERIAL DELIVERY", "FINALIZATION" };
        
        private void dropFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropFilter.Text.ToLower() == "name")
            {
                ChangeFilterBy(FilterBy.Name);  
            }
            if (dropFilter.Text.ToLower() == "status")
            {
                ChangeFilterBy(FilterBy.Status);
            }
            if (dropFilter.Text.ToLower() == "procedure")
            {   
                ChangeFilterBy(FilterBy.Procedure);
            }
            if (dropFilter.Text.ToLower()  == "grade")
            {
                ChangeFilterBy(FilterBy.Grade);
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Controller.Confirmation.Fire(Controller.Confirmation.onConfirmEnum.BuyAdmissionForm);
        }

        private void dgPotList_SelectionChanged(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(Controller.Utilities.GetSelectedDatagridValue(dgPotList, "idenq"));
            }
            catch (Exception)
            {
                i = 0;
            }
            if (i!=0)
            {
                Pointer(i);
            }
        }
    }

}
