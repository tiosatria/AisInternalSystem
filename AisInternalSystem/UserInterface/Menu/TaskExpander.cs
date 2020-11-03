using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;
using AisInternalSystem.Controller;
using System.Web.UI.WebControls;

namespace AisInternalSystem.UserInterface.Menu
{
    public partial class TaskExpander : UserControl
    {
        #region Properties
        private bool isLoaded;
        private int _taskcount;

        public int TaskCount
        {
            get { return _taskcount; }
            set { _taskcount = value; }
        }

        private MenuController.MenuType _group;

        public MenuController.MenuType FromGroup
        {
            get { return _group;  }
            set { _group = value; }
        }

        public void InitObject()
        {
            if (isLoaded)
            {
                GetChild();
            }
            else
            {
                InitializeComponent();
                this.Hide();
                GetChild();
            }
        }

        #endregion
        public TaskExpander()
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Expand()
        {
            this.Show();
            this.BringToFront();
        }

        private void GetChild()
        {
            foreach (TaskItem item in Data.tasksItems)
            {
                if (item.Group == this.FromGroup)
                {
                    flowTasks.Controls.Add(item);
                }
            }
            MessageBox.Show(flowTasks.Controls.Count.ToString());
        }

    }
}
