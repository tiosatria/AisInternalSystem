using System;
using System.Collections.Generic;
using System.Linq;
using AisInternalSystem.UserInterface;
using System.Text;
using AisInternalSystem.UserInterface.Menu;
using System.Threading.Tasks;

namespace AisInternalSystem.Controller
{
    public class Task
    {
        public Task(MenuItem menu)
        {
            TaskID = TaskCount+=1;
            TaskName = menu.Title;
            Category = menu.Category;
            AddTask(menu);
        }
        public static int TaskCount = 0;
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public TaskItem taskItem = new TaskItem();
        public CategoryMenu Category { get; set; }
        #region Function
        private void AddTask(MenuItem menu)
        {
            taskItem.Title = menu.Title;
            taskItem.Subtitle = menu.Title;
            taskItem.IndexItem = TaskID;
            taskItem.Control = menu.Cons;
            taskItem.TaskStart = DateTime.Now;
            taskItem.CategoryMenu = Category;
        }
        
        #endregion
    }
}
