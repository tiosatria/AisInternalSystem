﻿using AisInternalSystem.Controller;
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
        public static bool isOpen;
        private static MenuType _memmnu;
        public static MenuType Memmnu
        {
            get
            {
                if (isOpen)
                {
                    return _memmnu;
                }
                else
                {
                    return _memmnu;
                }
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

        public static void GetTaskProps(TaskItem child, MenuDoes does)
        {
            switch (does)
            {
                case MenuDoes.EmployeeDirectoryService:
                    child.Title = "Viewing:";
                    child.TaskStart = DateTime.Now;
                    break;
                case MenuDoes.RecordEmployee:
                    child.Title = "Working on:";
                    break;
                case MenuDoes.StudentDirectoryService:
                    child.Title = "";
                    break;
                case
                MenuDoes.RecordNewStudentData:
                
                    break;
                default:
                    break;
            }
        }

        private static void AddToTask(MenuDoes does)
        {
            if (Data.tasks.Exists(o => o.Does == does))
            {

            }
            else
            {
                TasksUser newTask = new TasksUser();
                TaskItem taskItem = new TaskItem();
                newTask.TaskIndex = Data.tasks.Count + 1;
                newTask.Sender = UIController.SenderButton();
                newTask.Control = (Guna2ShadowPanel)newTask.Sender.Parent;
                newTask.Location = new System.Drawing.Point(newTask.Sender.Location.X, newTask.Sender.Location.Y);
                newTask.Does = does;
                newTask.Group = UIController.GetGroup();
                taskItem.Does = does;
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
