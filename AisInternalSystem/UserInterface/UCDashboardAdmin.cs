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
        private bool IsLoaded;
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

            }

        }
        private void DashboardUC_Load(object sender, EventArgs e)
        {

        }

        private void BtnAddTodo_Click(object sender, EventArgs e)
        {
            //add in future
            PopUp.Alert("We're still working on finishing it!", frmAlert.AlertType.Info);
        }
    }
}
