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
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Menu
{
    public class MenuController
    {
        public MenuController()
        {

        }
        public enum MenuType
        {
            Employee, SchoolAdministration
        }
        private MenuType _menutype;
        public enum MenuDoes
        {
            EmployeeDirectoryService, RecordEmployee, StudentDirectoryService, RecordNewStudentData
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

        public static void GetContainerProperties(MenuContainer menu, MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.Employee:
                    menu.Title = "You've opened employee menu";
                    break;
                case MenuType.SchoolAdministration:
                    menu.Title = "You've opened School Administration";
                    break;
                default:
                    break;
            }
        }

        public static void GetItemProperties(MenuItem item, MenuDoes does)
        {
            switch (does)
            {
                case MenuDoes.EmployeeDirectoryService:
                    item.Title = "Employee Directory Services";
                    item.Subtitle = "Employee Directory";
                    item.Image = Resources.icons8_people_48px;
                    break;
                case MenuDoes.RecordEmployee:
                    item.Title = "Record Employee Data";
                    item.Subtitle = "Record new employee data";
                    item.Image = Resources.icons8_add_60px;
                    break;
                case MenuDoes.StudentDirectoryService:
                    item.Title = "Student Directory Services";
                    item.Subtitle = "Search/View Detailed information about students";
                    item.Image = Resources.icons8_student_male_60px;
                    break;
                case MenuDoes.RecordNewStudentData:
                    item.Title = "Record Student Data";
                    item.Subtitle = "Record new student data";
                    item.Image = Resources.icons8_add_60px;
                    break;
                default:
                    break;
            }
        }

        public static List<MenuItem> GetMenu(MenuType menu)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            int enumcount = 0;
            string enumvalue;
            switch (menu)
            {
                case MenuType.Employee:
                    enumcount = Enum.GetNames(typeof(MenuItemsEmployee)).Length;
                    MenuItemsEmployee enumEmployee;
                    if (enumcount >= 1)
                    {
                        MenuItem[] itemsMenu = new MenuItem[enumcount];
                        for (int i = 0; i < enumcount; i++)
                        {
                            itemsMenu[i] = new MenuItem();
                            enumEmployee = (MenuItemsEmployee)i;
                            enumvalue = enumEmployee.ToString();
                            itemsMenu[i].Does = (MenuDoes)Enum.Parse(typeof(MenuDoes), enumvalue);
                            menuItems.Add(itemsMenu[i]);
                        }
                    }
                    return menuItems;
                    break;
                case MenuType.SchoolAdministration:
                    enumcount = Enum.GetNames(typeof(MenuItemSchoolAdministration)).Length;
                    MenuItemSchoolAdministration EnumSchool;
                    if (enumcount >= 1)
                    {
                        MenuItem[] itemsMenu = new MenuItem[enumcount];
                        for (int i = 0; i < enumcount; i++)
                        {
                            itemsMenu[i] = new MenuItem();
                            EnumSchool = (MenuItemSchoolAdministration)i;
                            enumvalue = EnumSchool.ToString();
                            itemsMenu[i].Does = (MenuDoes)Enum.Parse(typeof(MenuDoes), enumvalue);
                            menuItems.Add(itemsMenu[i]);
                        }
                    }
                    return menuItems;
                    break;
                default:
                    return null;
                    break;
            }
        }

        private static List<MenuDoes> tasks = new List<MenuDoes>();


        private static void AddToTask(MenuDoes does)
        {
            if (Data.tasks.Exists(o => o.Does == does))
            {

            }
            else
            {
                TasksUser newTask = new TasksUser();
                newTask.TaskIndex = Data.tasks.Count + 1;
                newTask.Sender = UIController.SenderButton();
                newTask.Control = (Guna2ShadowPanel)newTask.Sender.Parent;
                newTask.Location = new System.Drawing.Point(newTask.Sender.Location.X, newTask.Sender.Location.Y);
                newTask.Does = does;
                newTask.Group = UIController.GetGroup();
                Data.tasks.Add(newTask);
                UIController.ResetMenu();
                UIController.InitTask(newTask.Group, newTask.Sender);
            }
        }

        public static void ExpandTask()
        {

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
                    UIController.NavigateUI(UIController.Controls.RecordStudentData);
                    AddToTask(does);

                    break;
                default:
                    break;
            }
        }

    }
}
