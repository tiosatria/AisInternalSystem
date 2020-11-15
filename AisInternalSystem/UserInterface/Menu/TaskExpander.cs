using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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
                _taskcount = value; }
        }

        #endregion

        public CategoryMenu Category { get; set;}

        public TaskExpander()
        {
                this.Hide();
                InitializeComponent();
        }

        public void InitTask()
        {
            List<Task> task = Data.TaskContainers[Data.TaskContainers.FindIndex(o => o.Category == Category)].ListTask;
            for (int i = 0; i < task.Count; i++)
            {
                flowTasks.Controls.Add(task[i].taskItem);
            }
        }

        public void Deletecontrol(UserControl control)
        {
            flowTasks.Controls.Remove(control);
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
    }
}
