using AisInternalSystem.Controller;
using AisInternalSystem.Entities;
using AisInternalSystem.Properties;
using Guna.UI2.WinForms;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.ReportingServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace AisInternalSystem.UserInterface.Menu
{
    public class MenuController
    {
        public MenuController()
        {

        }
        public enum MenuType
        {
            Employee, SchoolAdministration, Inventory
        }
        private MenuType _menutype;
        public enum MenuDoes
        {
            EmployeeDirectoryService, RecordEmployee, StudentDirectoryService, RecordNewStudentData, ClassAssignment, ClassInsight, ClassDirectoryService
        }
        public enum MenuItemsEmployee
        {
            EmployeeDirectoryService, RecordEmployee
        }
        private static MenuItemsEmployee menuItemsEmployee;
        public enum MenuItemSchoolAdministration
        {
            StudentDirectoryService, RecordNewStudentData
        }
        public static bool isOpen;
        private static MenuType _memmnu;
        public static MenuType Memmnu
        {
            get
            {
                    return _memmnu;
            }
            set { _memmnu = value; isOpen = true;  }
        }

        public static void GetContainerProperties(MenuContainer menu, MenuType menuType)
        {
            if (isOpen)
            {
                Memmnu = menuType;
            }
            switch (menuType)
            {
                case MenuType.Employee:
                    menu.Title = "You've opened employee menu";
                    break;
                case MenuType.SchoolAdministration:
                    menu.Title = "You've opened School Administration";
                    break;
                case MenuType.Inventory:
                    menu.Title = "Inventory Menus";
                    break;
                default:

                    break;
            }
        }

        private static List<string> MenuCategories =  new List<string> 
        {
        
        };

        public static void ExpandTask(MenuController.MenuType menu)
        {
            Data.taskExpanders[Data.taskExpanders.FindIndex(o => o.FromGroup== menu)].Expand();
        }   

        private static void AddToTask(MenuDoes does)
        {
            if (Data.tasks.Exists(o => o.Does == does))
            {

                UIController.ResetMenu();
                Data.taskExpanders[Data.taskExpanders.FindIndex(o => o.FromGroup == UIController.GetGroup())].Hide();
            }
            else
            {
                TasksUser newTask = new TasksUser();
                newTask.TaskIndex = Data.tasks.Count + 1;
                newTask.Sender = UIController.SenderButton();
                newTask.Control = (Guna2ShadowPanel)newTask.Sender.Parent;
                newTask.Location = new System.Drawing.Point(newTask.Sender.Location.X, newTask.Sender.Location.Y);
                newTask.Group = UIController.GetGroup();
                newTask.Does = does;
                Data.tasks.Add(newTask);
                UIController.ResetMenu();
                UIController.InitTask(newTask.Group, newTask.Sender);
                if (Data.taskExpanders.Exists(o=> o.FromGroup == UIController.GetGroup()))
                {
                    Data.taskExpanders[Data.taskExpanders.FindIndex(o => o.FromGroup == UIController.GetGroup())].Hide();
                }
            }
        }

        public static void DoAction(MenuDoes does)
        {
            switch (does)
            {
                case MenuDoes.EmployeeDirectoryService:
                    AddToTask(does);

                    break;
                case MenuDoes.RecordEmployee:
                    AddToTask(does);

                    break;
                case MenuDoes.StudentDirectoryService:
                    AddToTask(does);

                    break;
                case MenuDoes.RecordNewStudentData:
                    AddToTask(does);

                    break;
                case MenuDoes.ClassAssignment:
                    AddToTask(does);

                    break;
                case MenuDoes.ClassInsight:
                    AddToTask(does);
                    
                    break;
                case MenuDoes.ClassDirectoryService:
                    AddToTask(does);
                    UIController.NavigateUI(UIController.Controls.ClassDirectoryService);
                    break;
                default:
                    break;
            }
        }

    }
}
