using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using AisInternalSystem.Entities;

namespace AisInternalSystem
{
    public partial class UpperPanel : UserControl
    {
        private bool isLoaded;
        public UpperPanel()
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

            }
            isLoaded = true;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {

        }
    }
}
