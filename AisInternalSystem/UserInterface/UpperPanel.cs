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
using AisInternalSystem.Controller;

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
                this.Visible = false;
                UIController.Animate(this, Guna.UI2.AnimatorNS.AnimationType.VertSlide);
                UIController.GetDragControl(panel_default);
            }
            isLoaded = true;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {

            Confirmation.Fire(Confirmation.onConfirmEnum.Exit);
        }
    }
}
