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

        private void InitObject()
        {
            if (IsLoaded)
            {

            }
            else
            {
                InitializeComponent();

            }
            IsLoaded = true;
        }

        private void InitData()
        {
            dropSecretQuestion.DataSource = Module.User.SecretQuestion;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblChangePassword_Click(object sender, EventArgs e)
        {

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
    }
}
