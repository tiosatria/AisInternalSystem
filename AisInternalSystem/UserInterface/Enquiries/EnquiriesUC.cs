using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Enquiries
{
    public partial class EnquiriesUC : UserControl
    {
        private bool oldEnough = false;
        private bool isLoaded = false;
        private int age = 0;
        private int Progress = 0;

        public EnquiriesUC()
        {

        }
        public void InitObject()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                isLoaded = true;
                dropdob.Value = DateTime.Now;
                DetermineAgeAndGrade();
            }
            else
            {
               
            }
        }

        private void btnStudBasicNext_Click(object sender, EventArgs e)
        {
            if (txtpotentialStudname.Text != "" && txtPob.Text != "")
            {
                if (!oldEnough)
                {
                    PopUp.Alert("This student is not old enough to proceed!", frmAlert.AlertType.Warning);
                }
                else
                {
                    if (dropProposed.Text != "NOT ASSIGNED")
                    {
                        ContainerRelationship.BringToFront();
                        Controller.UIController.AnimateControl(ContainerRelationship, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
                        Progress = 50;
                        progressbar.Value = Progress;
                    }
                    else
                    {
                        PopUp.Alert("Please specify the proposed grade!", frmAlert.AlertType.Warning);
                    }
                }
            }
            else
            {
                PopUp.Alert("Please provide the required information!", frmAlert.AlertType.Warning);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtpicname.Text != "" && txtpicphone.Text != "" && txtpicHomeadd.Text != "")
            {
                Progress = 80;
                ContainerOptional.BringToFront();
                Controller.UIController.AnimateControl(ContainerOptional, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
                progressbar.Value = Progress;

            }
            else
            {
                PopUp.Alert("Please fill in the required information!", frmAlert.AlertType.Warning);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ContainerBasicInfo.BringToFront();
            Controller.UIController.AnimateControl(ContainerBasicInfo, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
        }
          
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtreferral.Text != "" && txtsurvey.Text != "")
            {
                PrepData();
                Progress = 90;
                ContainerFinalize.BringToFront();
                Controller.UIController.AnimateControl(ContainerFinalize, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
                progressbar.Value = Progress;
            }
            else
            {
                PopUp.Alert("Please provide information!", frmAlert.AlertType.Warning);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ContainerRelationship.BringToFront();
            Controller.UIController.AnimateControl(ContainerRelationship, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
        }

        private void Finalize()
        {
            if (enquiries !=null)
            {
                if (Entities.Enquiries.Enquiries.Insert(enquiries))
                {
                    PopUp.Alert("Enquiry recorded succesfully!", frmAlert.AlertType.Success);
                    Progress = 100;
                    progressbar.Value = Progress;
                    ContainerDone.BringToFront();
                    Controller.UIController.AnimateControl(ContainerDone, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
                }
                else
                {
                    PopUp.Alert("Failed to record enquiry!", frmAlert.AlertType.Error);
                }
            }
            else
            {
                PopUp.Alert("Something is wrong, please restart the system!", frmAlert.AlertType.Warning);
            }
        }

        private void DetermineAgeAndGrade()
        {
            string s = "Suggested Grade (Based on student's age): ";
            string i = dropProposed.Text;
            age = Controller.Utilities.GetAgeBasedOnDate(dropdob.Value);
            try
            {
                dropProposed.SelectedIndex = age - 1;
            }
            catch (Exception)
            {
                PopUp.Alert("Failed to determine grade, the student might be too old or too young!", frmAlert.AlertType.Warning);
            }
            if (age < 2)
            {
                oldEnough = false;
            }
            else
            {
                oldEnough = true;
            }
            lblsuggestedgrade.Text = s + i;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Finalize();

        }

        private void dropdob_ValueChanged(object sender, EventArgs e)
        {
            DetermineAgeAndGrade();
        }

        private void ClearPanel()
        {
            Controller.Utilities.ClearInputOnPanel(ContainerBasicInfo);
            Controller.Utilities.ClearInputOnPanel(ContainerRelationship);
            Controller.Utilities.ClearInputOnPanel(ContainerOptional);
            oldEnough = false;
            age = 0;
            Progress = 0;
            progressbar.Value = Progress;
            dropProposed.SelectedIndex = 0;
            dropdob.Value = DateTime.Now;

        }
        private void picFinish_Click(object sender, EventArgs e)
        {
            ClearPanel();
            ContainerBasicInfo.BringToFront();
            PopUp.Alert("Wish you all the best!", frmAlert.AlertType.Success);
            this.SendToBack();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ContainerRelationship.BringToFront();
            Controller.UIController.AnimateControl(ContainerRelationship, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
        }

        private Entities.Enquiries.Enquiries enquiries = null;

        private void PrepData()
        {
            enquiries = new Entities.Enquiries.Enquiries();
            enquiries.StudentName = txtpotentialStudname.Text;
            enquiries.PlaceOfBirth = txtPob.Text;
            enquiries.DateOfBirth = dropdob.Value;
            enquiries.Grade = dropProposed.Text;
            enquiries.StudentCondition = dropstudentcondition.Text;
            enquiries.RelationshipType = dropRelatWithChild.Text;
            enquiries.RelationshipName = txtpicname.Text;
            enquiries.RelationshipPhoneNumber = txtpicphone.Text;
            enquiries.RelationshipWhatsappNumber = txtpicwhatsapp.Text;
            enquiries.Address = txtpicHomeadd.Text;
            enquiries.Maker = Controller.Data.user.OwnerID;
            enquiries.Remarks = txtpicRemarks.Text;
            enquiries.Referral = txtreferral.Text;
            enquiries.Survey = txtsurvey.Text;
            enquiries.PresentSchool = txtpresentschool.Text;
            enquiries.AYCODE = Entities.AcademicYear.GetOngoingAcademicYear().academicYearCode;
            //finalstep container data prep
            lblname.Text = enquiries.StudentName;
            lblpob.Text = enquiries.PlaceOfBirth;
            lbldob.Text = enquiries.DateOfBirth.ToString("D");
            lblpresentschool.Text = enquiries.PresentSchool;
            lblproposedgrade.Text = enquiries.Grade;
            lblrelatwithchild.Text = enquiries.RelationshipType;
            lblnamerelat.Text = enquiries.RelationshipName;
            lblphonenumb.Text = enquiries.RelationshipPhoneNumber;
            lblwhatsappnumb.Text = enquiries.RelationshipWhatsappNumber;
            lbladdress.Text = enquiries.Address;
        }
    }
}
