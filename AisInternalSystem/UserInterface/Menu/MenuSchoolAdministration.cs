using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Menu
{
    public partial class MenuSchoolAdministration : UserControl
    {
        private bool isLoaded;
        private Size DefaultSize = new Size(640, 305);
        private Size FocusedSize = new Size(884, 412);
        private Point DefaultPoint = new Point();

        public MenuSchoolAdministration()
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

        #region properties

        #endregion

        #region Function
        private void focusedPanel(Panel panel)
        {
            panel.Size = FocusedSize;

        }
        #endregion

        #region Event Handler
        private void PanelRecordStudent1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void PanelRecordStudent1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void PanelClassAssignment3_MouseLeave(object sender, EventArgs e)
        {

        }

        private void PanelStudentDirectory2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void PanelStudentDirectory2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void PanelClassAssignment3_MouseEnter(object sender, EventArgs e)
        {

        }

        private void PanelEnquiry4_MouseEnter(object sender, EventArgs e)
        {

        }

        private void PanelEnquiry4_MouseLeave(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
