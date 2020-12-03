using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AisInternalSystem.Entities;
using AisInternalSystem.Controller;
using AisInternalSystem.Module;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Settings
{
    public partial class UserSettings : UserControl
    {
        private bool IsLoaded = false;
        public UserSettings()
        {

        }

        public void InitObject()
        {
            if (IsLoaded)
            {

            }
            else
            {
                InitializeComponent();
                InitData();
            }
            IsLoaded = true;
        }

        private void InitData()
        {
            dropSecretQuestion.DataSource = Module.User.SecretQuestion;
            PicBox.Image = Data.user.ImageUser;
            btnApplyImg.Parent = PicBox;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panelChangeUsername.BringToFront();
        }

        private void lblChangePassword_Click(object sender, EventArgs e)
        {
            PanelChangePassword.BringToFront();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void ChangePassword()
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ChangePassword();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void lblSecretQHelp_Click(object sender, EventArgs e)
        {
            PopUp.Alert("You can use this when you lost your password", frmAlert.AlertType.Info);
        }
        OpenFileDialog pic = null;  
        private void PicBox_Click(object sender, EventArgs e)
        {
            pic =  Utilities.OpenImage(PicBox);
            if (pic != null && pic.FileName != "" && pic.FileName != null)
            {
                Data.user.UserImage = Utilities.GetFileDbLocationString(Utilities.LocationType.EmployeePhoto, Data.user.OwnerID.ToString(), pic);
                btnApplyImg.Visible = true;
            }
            else
            {
                btnApplyImg.Visible = false;
                pic = null;
                PopUp.Alert("You didn't upload anything\nNo changes has been made", frmAlert.AlertType.Info);
            }
        }

        private void FetchSecretAnswerAndQuestion()
        {

        }

        private void ApplyImage()
        {
            if (pic != null)
            {
                if (User.UpdatePicture(Data.user) == true)
                {
                    PopUp.Alert("We're updating your picture!", frmAlert.AlertType.Info);
                    Utilities.WorkerFire(Utilities.WorkerProcess.CopyFile, new string[2] { pic.FileName, Utilities.GetFileDbLocationString(Utilities.LocationType.EmployeePhoto, Data.user.OwnerID.ToString(), pic) });
                }
                else
                {
                    PopUp.Alert("Failed to upload your picture!", frmAlert.AlertType.Error);
                }
            }
            else
            {
                PopUp.Alert("Something is wrong", frmAlert.AlertType.Error);
            }
            btnApplyImg.Visible = false;
            pic = null;
        }

        private void btnApplyImg_Click(object sender, EventArgs e)
        {
            ApplyImage();
        }
        private void ChangeSecretQuestion()
        {
            if (User.UpdateSecretQuestion(Data.user))
            {
                PopUp.Alert("Secret question updated succesfully!", frmAlert.AlertType.Success);
            }
            else
            {
                PopUp.Alert("Failed to update secret question!", frmAlert.AlertType.Warning);
            }
        }
        private void btnChangeSecretQuestion_Click(object sender, EventArgs e)
        {
            if (dropSecretQuestion.SelectedItem.ToString() != "")
            {
                if (txtAnswerSecretQuestion.Text != "")
                {
                    ChangeSecretQuestion();
                }
                else
                {
                    PopUp.Alert("You cannot leave secret question blank!", frmAlert.AlertType.Warning);
                }
            }
            else
            {
                PopUp.Alert("Please select one secret question!", frmAlert.AlertType.Warning);
            }

        }

        private void dropSecretQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.user.SecQuestion = dropSecretQuestion.SelectedItem.ToString();
        }

        private void txtAnswerSecretQuestion_TextChanged(object sender, EventArgs e)
        {
            Data.user.Answer = txtAnswerSecretQuestion.Text;
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
