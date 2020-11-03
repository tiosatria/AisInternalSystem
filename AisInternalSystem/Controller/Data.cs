using AisInternalSystem.Module;
using AisInternalSystem.UserInterface.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Controller
{
    public class Data
    {


        #region Enumeration

        #endregion

        #region Entities
        public static User user = new User();
        public static List<TasksUser> tasks = new List<TasksUser>();
        public static List<TaskItem> tasksItems = new List<TaskItem>();
        public static List<TaskExpander> taskExpanders = new List<TaskExpander>();
        #endregion

        #region Variable

        #endregion
        public Data()
        {

        }

    }
}
