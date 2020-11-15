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
        public static bool isOpen;

        //private static void AddToTask(MenuDoes does)
        //{
        //    if (Data.tasks.Exists(o => o.Does == does))
        //    {

        //        UIController.ResetMenu();
        //        Data.taskExpanders[Data.taskExpanders.FindIndex(o => o.FromGroup == UIController.GetGroup())].Hide();
        //    }
        //    else
        //    {
        //        TasksUser newTask = new TasksUser();
        //        newTask.TaskIndex = Data.tasks.Count + 1;
        //        newTask.Sender = UIController.SenderButton();
        //        newTask.Control = (Guna2ShadowPanel)newTask.Sender.Parent;
        //        newTask.Location = new System.Drawing.Point(newTask.Sender.Location.X, newTask.Sender.Location.Y);
        //        newTask.Group = UIController.GetGroup();
        //        newTask.Does = does;
        //        Data.tasks.Add(newTask);
        //        UIController.ResetMenu();
        //        UIController.InitTask(newTask.Group, newTask.Sender);
        //        if (Data.taskExpanders.Exists(o=> o.FromGroup == UIController.GetGroup()))
        //        {
        //            Data.taskExpanders[Data.taskExpanders.FindIndex(o => o.FromGroup == UIController.GetGroup())].Hide();
        //        }
        //    }
        //}

        //public static void DoAction(MenuDoes does)
        //{
        //    switch (does)
        //    {
        //        case MenuDoes.EmployeeDirectoryService:
        //            AddToTask(does);

        //            break;
        //        case MenuDoes.RecordEmployee:
        //            AddToTask(does);

        //            break;
        //        case MenuDoes.StudentDirectoryService:
        //            AddToTask(does);

        //            break;
        //        case MenuDoes.RecordNewStudentData:
        //            AddToTask(does);
        //            UIController.NavigateUI(UIController.Controls.RecordStudentData);
        //            break;
        //        case MenuDoes.ClassAssignment:
        //            AddToTask(does);

        //            break;
        //        case MenuDoes.ClassInsight:
        //            AddToTask(does);
                    
        //            break;
        //        case MenuDoes.ClassDirectoryService:
        //            AddToTask(does);
        //            UIController.NavigateUI(UIController.Controls.ClassDirectoryService);
        //            break;
        //        case MenuDoes.SubjectDirectoryServices:
        //            AddToTask(does);
        //            UIController.NavigateUI(UIController.Controls.SubjectDirectoryServices);
        //            break;
        //        case MenuDoes.InventoryDirectoryService:
        //            AddToTask(does);
        //            UIController.NavigateUI(UIController.Controls.InventoryDirectory);
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }
}
