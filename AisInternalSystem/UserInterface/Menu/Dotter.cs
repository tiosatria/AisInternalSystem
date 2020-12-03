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
            TaskItem.TaskRemoved += TaskItem_TaskRemoved1;
            MenuItem.TaskChanged += MenuItem_TaskChanged;
        }

        private void MenuItem_TaskChanged(object sender, Controller.Task e)
        {
            try
            {
                TaskCount = Data.TaskContainers[Data.TaskContainers.FindIndex(o => o.Category == category)].ListTask.Count;
            }
            catch (Exception ex)
            {
                PopUp.Alert(ex.Message, frmAlert.AlertType.Error);
            }
            Expander.InitTask();
            if (TaskCount >= 1)
            {
                this.Visible = true;
            }
        }

        private void TaskItem_TaskRemoved1(object sender, Controller.Task e)
        {
            if (e.taskItem.CategoryMenu == category)
            {
                if (TaskCount < 1)
                {
                    this.Visible = false;
                    Expander.Visible = false;
                }
            }
        }

        public TaskExpander Expander = new TaskExpander();

        private void InitTaskExpander()
        {
            Expander.Location = new Point(this.Location.X -30, this.Location.Y + 30);
            Expander.Category = this.category;
            UIController.AddControlToMainForm(Expander, DockStyle.None);
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
                InitTaskExpander();
            }
        }

        public CategoryMenu category { get; set; }

        private void btnShow_Click(object sender, EventArgs e)
        {
            Expander.Expand();
        }

        private void Dotter_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }

        private void Dotter_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
