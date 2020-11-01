using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Controller;

namespace AisInternalSystem.UserInterface.Menu
{
    public partial class Dotter : UserControl
    {
        public Dotter()
        {
            InitializeComponent();
        }

        private int _taskcount;

        public int TaskCount
        {
            get { return _taskcount; }
            set { _taskcount = value;
                if (_taskcount > 1)
                {
                    lbltasks.Text = $"{value} Tasks";
                }
                else
                {
                    lbltasks.Text = $"{value} Task";
                }
            }
        }

        private MenuController.MenuType _fromgroup;

        public MenuController.MenuType FromGroup
        {
            get { return _fromgroup; }
            set { _fromgroup = value; }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            
        }
    }
}
