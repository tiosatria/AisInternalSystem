using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Guna.UI2.WinForms;
using AisInternalSystem.Controller;

namespace AisInternalSystem.UserInterface.Menu
{
    public class TaskContainer
    {
        public TaskContainer()
        {

        }

        private CategoryMenu _category;

        public CategoryMenu Category
        {
            get { return _category; }
            set { _category = value; InitDotter(); }
        }
        public List<Task> ListTask = new List<Task>();

        public Dotter dotter = new Dotter();
        public void InitDotter()
        {
            Guna2Button Handler = Menus.Category[Menus.Category.FindIndex(o => o.CategoryID == Category.CategoryID)].CategoryHandler;
            dotter.category = this.Category;
            dotter.Location = new System.Drawing.Point(Handler.Location.X + 20, Handler.Size.Height + 15);
            dotter.TaskCount = ListTask.Count;
            UIController.AddControlToMainForm(dotter, System.Windows.Forms.DockStyle.None);
        }

    }
}
