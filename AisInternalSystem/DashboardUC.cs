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

namespace AisInternalSystem
{
    public partial class DashboardUC : UserControl
    {

        DialogControl confirmation = new DialogControl();

        public DashboardUC()
        {
            InitializeComponent();
        }

        private void DashboardUC_Load(object sender, EventArgs e)
        {
            lblNameUser.Text = "Hello!" + " " + Dashboard.ownerName + ", " +"Let's get things done today!";
            lblRole.Text = Dashboard.role;
        }

        private void BtnAddTodo_Click(object sender, EventArgs e)
        {
            //add in future
            Msg.Alert("We're still working on finishing it!", frmAlert.AlertType.Info);
        }
    }
}
