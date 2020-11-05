using AisInternalSystem.Module;
using AisInternalSystem.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace AisInternalSystem.UserInterface.Menu
{
    public class Menus
    {
        private static bool MenuLoaded;

        public Menus()
        {

        }

        #region Properties

        #endregion
        
        public static void InitMenus()
        {
            AddMenu("Employee Directory", "Employee Directory Services", MenuItem.DoingWhatEnum.Reviewing, Resources.icons8_people_48px, new List<MenuController.MenuType> { MenuController.MenuType.Employee }, new List<User.RoleIdentifier> { User.RoleIdentifier.Accounting, User.RoleIdentifier.Teacher, User.RoleIdentifier.Admin }, MenuController.MenuDoes.EmployeeDirectoryService);
            AddMenu("Record Employee", "Record new employee data", MenuItem.DoingWhatEnum.Working, Resources.icons8_add_60px, new List<MenuController.MenuType> { MenuController.MenuType.Employee }, new List<User.RoleIdentifier> { User.RoleIdentifier.Accounting, User.RoleIdentifier.Admin, User.RoleIdentifier.Management }, MenuController.MenuDoes.RecordEmployee);
            AddMenu("Student Directory", "Student Directory Services", MenuItem.DoingWhatEnum.Reviewing, Resources.icons8_student_male_60px, new List<MenuController.MenuType> { MenuController.MenuType.SchoolAdministration }, new List<User.RoleIdentifier> { User.RoleIdentifier.Accounting, User.RoleIdentifier.Admin, User.RoleIdentifier.IT, User.RoleIdentifier.Management, User.RoleIdentifier.Teacher }, MenuController.MenuDoes.StudentDirectoryService);
            AddMenu("Record Student Data", "Record new student data", MenuItem.DoingWhatEnum.Working, Resources.icons8_add_60px, new List<MenuController.MenuType> { MenuController.MenuType.SchoolAdministration }, new List<User.RoleIdentifier> { User.RoleIdentifier.Accounting, User.RoleIdentifier.Admin, User.RoleIdentifier.IT, User.RoleIdentifier.Management }, MenuController.MenuDoes.RecordNewStudentData);
            AddMenu("Class Directory", "Class Directory Services", MenuItem.DoingWhatEnum.Reviewing, Resources.icons8_classroom_200px, new List<MenuController.MenuType> { MenuController.MenuType.SchoolAdministration }, new List<User.RoleIdentifier> { User.RoleIdentifier.Admin, User.RoleIdentifier.Accounting }, MenuController.MenuDoes.ClassDirectoryService);
        }

        private static int itemIndex = 0;
        private static void AddMenu(string title, string description, MenuItem.DoingWhatEnum act, Image img, List<MenuController.MenuType> Categories, List<User.RoleIdentifier> Accessor, MenuController.MenuDoes does)
        {
            if (ListMenu.Exists(o => o.Title == title))
            {
                //check to see whether the menu is existed
                //do nothing
            }
            else
            {
                itemIndex++;
                MenuItem menu = new MenuItem();
                menu.Title = title;
                menu.Index = itemIndex; 
                menu.Subtitle = description;
                menu.Image = img;
                menu.Doing = act;
                menu.Category = Categories;
                menu.Does = does;
                menu.Accesor = Accessor;
                ListMenu.Add(menu);
            }
        }
        #region List of categories
        private static List<MenuController.MenuType> Categories = new List<MenuController.MenuType>();
        #endregion
        #region List of Menu
        public static List<MenuItem> ListMenu = new List<MenuItem>();
    #endregion

}
}