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
            }
            isLoaded = true;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Utilities.Auth(txt_username.Text, txt_password.Text);
        }
    }
}
