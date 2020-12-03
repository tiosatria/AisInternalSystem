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
        public static event EventHandler PictureChangedEvent;
        public static event EventHandler PictureBeingChanged;
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
            if (Data.user.Answer == null || Data.user.Answer == "")
            {
                PopUp.Alert("You haven't set your secret question, please set it now to secure your account!", frmAlert.AlertType.Info);
            }
            dropSecretQuestion.DataSource = Module.User.SecretQuestion;
            PicBox.Image = Data.user.ImageUser;
            Utilities.WorkerFinished += Utilities_WorkerFinished;
            txtUsername.Text = Data.user.usrName;
            txtOldUsername.Text = Data.user.usrName;
            try
            {
                dropSecretQuestion.SelectedIndex = dropSecretQuestion.Items.IndexOf(Data.user.SecQuestion);
            }
            catch (Exception)
            {
                PopUp.Alert("You haven't set your secret question, please set it now to secure your account!", frmAlert.AlertType.Info);
                dropSecretQuestion.SelectedIndex = 0;
            }
            txtAnswerSecretQuestion.Text = Data.user.Answer;
        }

        private void Utilities_WorkerFinished(object sender, EventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate {
                    using (Image image = Image.FromFile(Data.user.UserImage))
                    {
                        Bitmap bitmap = new Bitmap(image);
                        Data.user.ImageUser = bitmap;
                        image.Dispose();
                        PicBox.Image = bitmap; 
                    }
                }));
            }
            catch (Exception)
            {

            }
            PictureChangedEvent?.Invoke(this, EventArgs.Empty);

        }

        private void User_PictureChangedEvent(object sender, EventArgs e)
        {
            Data.user.ImageUser = new Bitmap(Data.user.ImageUser);
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
            panel1.BringToFront();
            btnCancelChangePassword.Text = "Nevermind, go back";
        }

        private void ChangePassword(string password)
        {
            Data.user.Password = password;
            if (User.UpdatePassword(Data.user))
            {
                PopUp.Alert("Password updated succesfully!", frmAlert.AlertType.Success);
                btnCancelChangePassword.Text = "Yeay, go back!";
                Utilities.ClearInputOnPanel(PanelChangePassword);
            }
            else
            {
                PopUp.Alert("Failed to change password", frmAlert.AlertType.Error);
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtCurrPassword.Text != Data.user.Password)
            {
                PopUp.Alert("Wrong current password!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (txtNewPassword.Text == "" || txtNewPasswordConfirm.Text == "")
                {
                    PopUp.Alert("You can't have empty password :/", frmAlert.AlertType.Warning);
                }
                else
                {
                    if (txtNewPassword.Text != txtNewPasswordConfirm.Text)
                    {
                        PopUp.Alert("Please correct your new password, it doesn't look the same", frmAlert.AlertType.Warning);
                    }
                    else
                    {
                        if (txtNewPassword.Text == Data.user.Password)
                        {
                            PopUp.Alert("Please enter new password, that's your old password!", frmAlert.AlertType.Warning);
                        }
                        else
                        {
                            ChangePassword(txtNewPasswordConfirm.Text);
                        }
                    }
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
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
                PictureBeingChanged?.Invoke(this, EventArgs.Empty);
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
                Data.user.Answer = Data.user.AnswerTemporary;
            }
            else
            {
                PopUp.Alert("Failed to update secret question!", frmAlert.AlertType.Warning);
            }
        }
        private void btnChangeSecretQuestion_Click(object sender, EventArgs e)
        {
            if (dropSecretQuestion.SelectedItem.ToString() != "" || dropSecretQuestion.SelectedItem != null)
            {
                if (txtAnswerSecretQuestion.Text != "")
                {
                    if (txtAnswerSecretQuestion.Text == Data.user.Answer)
                    {
                        PopUp.Alert("You cannot have same secret answer with your previous one!", frmAlert.AlertType.Warning);
                    }
                    else
                    {
                        ChangeSecretQuestion();
                    }
                }
                else
                {
                    PopUp.Alert("You cannot leave secret Answer blank!", frmAlert.AlertType.Warning);
                }
            }
            else
            {
                PopUp.Alert("Please select one secret question!", frmAlert.AlertType.Warning);
            }

        }
        private void dropSecretQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropSecretQuestion.SelectedItem != null)
            {
                Data.user.SecQuestionTemp = dropSecretQuestion.SelectedItem.ToString();
            }
            else
            {

            }
        }
        private void txtAnswerSecretQuestion_TextChanged(object sender, EventArgs e)
        {
            Data.user.AnswerTemporary = txtAnswerSecretQuestion.Text;
        }
        private void ChangeUsername(string newusername)
        {
            Data.user.TemporaryUsername = newusername;
            if (User.RequestChangeUserName(Data.user, newusername))
            {
                PopUp.Alert("Your application has been submitted, please standby!\nWe'll notify the IT!", frmAlert.AlertType.Info);
                btnCancelREqUsername.Text = "I understand, Go back";
            }
            else
            {
                PopUp.Alert("Cannot process your request\nYou can only request once!", frmAlert.AlertType.Warning);
            }
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtOldUsername.Text == txtNewUserName.Text)
            {
                PopUp.Alert("Your username cannot be the same as old username!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (txtNewUserName.Text == "")
                {
                    PopUp.Alert("You cannot proceed with blank username!", frmAlert.AlertType.Warning);
                }
                else
                {
                    ChangeUsername(txtNewUserName.Text);
                }
            }
        }
    }
}
