using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Entities;
using AisInternalSystem.Controller;

namespace AisInternalSystem.UserInterface.Student
{
    public partial class UCRecStudent : UserControl
    {
        public UCRecStudent()
        {
            InitializeComponent();
        }

        private void btnStud1Next_Click(object sender, EventArgs e)
        {
            UIController.SendPanelBack(panelStud1);    
        }

        private void btnStudBack_Click(object sender, EventArgs e)
        {
            UIController.SendPanelBack(panelStud2);
        }

        private void picStud_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = Utilities.OpenImage(picStud)
        }
    }
}
