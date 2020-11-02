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
            set { _title = value; } 
        }
        private string _subtitle;

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; }
        }

        private DateTime _taskstart;

        public DateTime TaskStart
        {
            get { return _taskstart; }
            set { _taskstart = value; }
        }

        private DateTime _taskfinish;

        public DateTime TaskFinish
        {
            get { return _taskfinish; }
            set { _taskfinish = value; }
        }

        private MenuController.MenuDoes _does;

        public MenuController.MenuDoes Does
        {
            get { return _does; }
            set { _does = value; MenuController.GetTaskProps(this, _does); }
        
        }

        private MenuController.MenuType _fromgroup;

        public MenuController.MenuType Group
        {
            get { return _fromgroup; }
            set { _fromgroup = value; }
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

        }

        private void TaskItem_MouseLeave(object sender, EventArgs e)
        {

        }

        private void TaskItem_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnMarkAsDone_Click(object sender, EventArgs e)
        {

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
    }
}
