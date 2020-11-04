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
            set
            {
                _taskcount = value; if (TaskCount == 1) { lblTaskcount.Text = $"{value.ToString()} Task"; }
                else
                {
                    lblTaskcount.Text = $"{value.ToString()} Tasks\nClick to switch";
                } }
        }

        private MenuController.MenuType _group;

        public MenuController.MenuType FromGroup
        {
            get { return _group;  }
            set { _group = value; }
        }

        public void InitObject(MenuController.MenuType m)
        {
            GetChild(m);
        }

        #endregion
        public TaskExpander(MenuController.MenuType i)
        {
            this.Hide();
            if (isLoaded)
            {
                GetChild(i);
            }
            else
            {
                InitializeComponent();
                GetChild(i);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Expand()
        {
            this.Show();
            this.BringToFront();
            GetChild(this.FromGroup);
        }

        private void GetChild(MenuController.MenuType i)
        {
            foreach (TaskItem item in Data.tasksItems)
            {
                if (item.Group == i)
                {
                    this.flowTasks.Controls.Add(item);
                }
            }
            this.TaskCount = flowTasks.Controls.Count;
        }

    }
}
