using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using AisInternalSystem.Controller;
using System.Windows.Forms;
using AisInternalSystem.Module;
using System.ComponentModel.Design;

namespace AisInternalSystem.UserInterface.Menu
{
    public partial class MenuItem : UserControl
    {
        #region Properties
        private int _index;

        public enum DoingWhatEnum
        {
       
            Reviewing, Adding, Working, Checking, Marking
        }

        public List<User.RoleIdentifier> Accessor { get; set; }

        private DoingWhatEnum _doing;
        public DoingWhatEnum Doing
        {
            get { return _doing; }
            set { _doing = value; _act = GetFancyAct(value); }
        }

        private string GetFancyAct(DoingWhatEnum _do)
        {
            switch (_do)
            {
                case DoingWhatEnum.Reviewing:
                    return "Reviwing: ";
                case DoingWhatEnum.Adding:
                    return "Adding: ";
                case DoingWhatEnum.Working:
                    return "Working on: ";
                case DoingWhatEnum.Checking:
                    return "Checking: ";
                case DoingWhatEnum.Marking:
                    return "Marking: ";
                default:
                    return "Not defined";
            }
        }

        private string _act;

        public string ActInString
        {
            get { return _act; }
            set { _act = value; }
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; lblTitle.Text = value; }
        }
        private string _subtitle;

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; lblSubtitle.Text = value; }
        }
        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; imgPic.Image = value; }
        }

        public CategoryMenu Category { get; set; }

        public UIController.Controls Cons { get; set; }

        public ItemMenu FromMenu { get; set; }

        #endregion
        public MenuItem()
        {
            InitializeComponent();
        }
        private void PrepareTaskContainer()
        {
            if (!Data.TaskContainers.Exists(o => o.Category == Category))
            {
                TaskContainer taskContainer = new TaskContainer();
                taskContainer.Category = this.Category;
                Data.TaskContainers.Add(taskContainer);
            }
        }

        private void PrepareTask()
        {
            if (!Data.TaskContainers.Exists(o => o.ListTask.Exists(w => w.TaskName == this.Title)))
            {
                Task task = new Task(this);
                task.Category = Category;
                Data.TaskContainers[Data.TaskContainers.FindIndex(o => o.Category == this.Category)].ListTask.Add(task);
                TaskChanged?.Invoke(this, task);
            }
        }
        public static event EventHandler<Task> TaskChanged;

        private void OnClick()
        {
            PrepareTaskContainer();
            PrepareTask();
            UIController.NavigateUI(Cons);
            UIController.ResetMenu();
        }
        private void MenuItem_Click(object sender, EventArgs e)
        {
            OnClick();
        }

        private void MenuItem_Load(object sender, EventArgs e)
        {

        }

        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Coral;
        }

        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            OnClick();
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {
            OnClick();
        }

        private void imgPic_Click(object sender, EventArgs e)
        {
            OnClick();
        }
    }
}
