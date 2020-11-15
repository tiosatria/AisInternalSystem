using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AisInternalSystem.Controller;

namespace AisInternalSystem.UserInterface.Menu
{

    public partial class TaskItem : UserControl
    {
        #region Properties
        private int _indexItem;

        public int IndexItem
        {
            get { return _indexItem; }
            set { _indexItem = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; lbltitle.Text = value; } 
        }
        private string _subtitle;

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; lblsubtitle.Text = value; }
        }

        private DateTime _taskstart = DateTime.Now;

        public UIController.Controls Control { get; set; }

        public DateTime TaskStart
        {
            get { return _taskstart; }
            set { _taskstart = value; lblstart.Text = "Started at: " + value.ToString("t"); }
        }

        private DateTime _taskfinish;

        public DateTime TaskFinish
        {
            get { return _taskfinish; }
            set { _taskfinish = value; }
        }

        #endregion

        public TaskItem()
        {
            InitializeComponent();
        }

        #region Function

        #endregion

        private void TaskItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }

        public CategoryMenu CategoryMenu { get; set; }

        private void TaskItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }
        private void TaskItem_MouseClick(object sender, MouseEventArgs e)
        {
            Clicked();
        }
        public static event EventHandler<Task> TaskRemoved;

        private void FinishTask()
        {
            TaskContainer container = Data.TaskContainers[Data.TaskContainers.FindIndex(o=>o.Category == CategoryMenu)];
            Task task = container.ListTask[container.ListTask.FindIndex(o => o.TaskID == IndexItem)];
            container.dotter.Expander.Deletecontrol(this);
            container.ListTask.Remove(task);
            container.dotter.TaskCount = container.ListTask.Count;
            TaskRemoved?.Invoke(this, task);
        }

        private void btnMarkAsDone_Click(object sender, EventArgs e)
        {
            FinishTask();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

        }

        private void lbltitle_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void lblsubtitle_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void TaskItem_Load(object sender, EventArgs e)
        {

        }
        
        private void Clicked()
        {
            UIController.NavigateUI(Control);
        }

        private void TaskItem_Click(object sender, EventArgs e)
        {
            Clicked();

        }
    }
}
