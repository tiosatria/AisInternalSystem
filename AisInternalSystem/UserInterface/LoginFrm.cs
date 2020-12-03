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
using System.Net.Sockets;
using System.Threading;
using Microsoft.VisualBasic;
using AisInternalSystem.Properties;
using AisInternalSystem.Entities;
using Guna.UI2.AnimatorNS;
using AisInternalSystem.Controller;

namespace AisInternalSystem
{
    public partial class LoginFrm : UserControl
    {
        private bool isLoaded = false;

        public LoginFrm()
        {

        }

        public void InitializeObject()
        {
            if (isLoaded)
            {

            }
            else
            {
                InitializeComponent();
                this.Visible = false;
                UIController.Animate(this, AnimationType.Transparent);
                InitObject();
            }
            isLoaded = true;
        }
        private void InitObject()
        {
            dropSecretQuestion.DataSource = User.SecretQuestion;
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Utilities.Auth(txt_username.Text, txt_password.Text);
        }

        private void lblsecret_Click(object sender, EventArgs e)
        {
            PopUp.Alert("In that case, please contact the IT Department!", frmAlert.AlertType.Info);
        }

        private void lblREqUserChange_Click(object sender, EventArgs e)
        {
            PanelForgot.BringToFront();
            UIController.AnimateControl(PanelForgot, AnimationType.HorizSlide);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PanelLogin.BringToFront();
            UIController.AnimateControl(PanelLogin, AnimationType.HorizSlide);
        }
        private void SecretQuestionValidation(string username, string question, string answer)
        {
            DataTable dt = User.SecretQuestionValidation(username, question, answer);
            if(dt.Rows.Count >=1)
            {
                DummyUser = new User();
                DummyUser.usrName = username;
                DummyUser.OwnerID = Convert.ToInt32(dt.Rows[0][3].ToString());
                DummyUser.SecQuestion = dt.Rows[0][4].ToString();
                DummyUser.Answer = dt.Rows[0][5].ToString();
                PopUp.Alert("We found your account, now it's time to reset its password!", frmAlert.AlertType.Success);
                PanelChangePassword.BringToFront();
                UIController.AnimateControl(PanelChangePassword, AnimationType.HorizBlind);
            }
            else
            {
                PopUp.Alert("We didn't found an account with that secret question :(", frmAlert.AlertType.Warning);
            }
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtAnswerSecretQuestion.Text != "" || txtAnswerSecretQuestion.Text != null)
            {
                SecretQuestionValidation(txtUsername.Text, dropSecretQuestion.SelectedItem.ToString(), txtAnswerSecretQuestion.Text);
            }
            else
            {
                PopUp.Alert("Please enter valid username and secret question!", frmAlert.AlertType.Warning);
            }
        }

        private User DummyUser = null;

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtnewpassword.Text != txtconfirmnewpassword.Text)
            {
                PopUp.Alert("Please make sure both password match!", frmAlert.AlertType.Warning);
            }
            else
            {
                if (txtnewpassword.Text.Length < 6)
                {
                    PopUp.Alert("Minimum password length is 6, try something longer!", frmAlert.AlertType.Warning);
                }
                else
                {
                    DummyUser.Password = txtconfirmnewpassword.Text;
                    ChangePassword();
                }
            }
        }
        private void GoBackToLogin()
        {
            PanelLogin.BringToFront();
            UIController.AnimateControl(PanelLogin, AnimationType.Transparent);
        }
        private void ChangePassword()
        {
            if (User.UpdatePassword(DummyUser))
            {
                PopUp.Alert("Your password has been resetted successfully!\nYou can now continue to login using new password!", frmAlert.AlertType.Success);
                Utilities.ClearInputOnPanel(PanelChangePassword);
                Utilities.ClearInputOnPanel(PanelForgot);
                GoBackToLogin();
            }
            else
            {
                PopUp.Alert("Failed to reset your password, please try again", frmAlert.AlertType.Warning);
            }

        }
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            PanelChangePassword.SendToBack();
        }
    }
}
