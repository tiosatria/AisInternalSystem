using AisInternalSystem.Module;
using AisInternalSystem.Properties;
using System;
using System.Collections.Generic;
using AisInternalSystem.Controller;
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
            AddMenu(1, "Employee Directory", "Employee Directory Services", MenuItem.DoingWhatEnum.Reviewing, Resources.icons8_people_48px, Employee, new List<User.RoleIdentifier> { User.RoleIdentifier.Accounting, User.RoleIdentifier.Teacher, User.RoleIdentifier.Admin }, UIController.Controls.EmployeeDirectoryService);
            AddMenu(2, "Record Employee", "Record new employee data", MenuItem.DoingWhatEnum.Working, Resources.icons8_add_60px, Employee, new List<User.RoleIdentifier> { User.RoleIdentifier.Accounting, User.RoleIdentifier.Admin, User.RoleIdentifier.Management }, UIController.Controls.RecordEmployee);
            AddMenu(3, "Student Directory", "Student Directory Services", MenuItem.DoingWhatEnum.Reviewing, Resources.icons8_student_male_60px, SchoolAdministration, new List<User.RoleIdentifier> { User.RoleIdentifier.Accounting, User.RoleIdentifier.Admin, User.RoleIdentifier.IT, User.RoleIdentifier.Management, User.RoleIdentifier.Teacher }, UIController.Controls.StudentDirectoryService);
            AddMenu(4, "Record Student Data", "Record new student data", MenuItem.DoingWhatEnum.Working, Resources.icons8_add_60px, SchoolAdministration, new List<User.RoleIdentifier> { User.RoleIdentifier.Accounting, User.RoleIdentifier.Admin, User.RoleIdentifier.IT, User.RoleIdentifier.Management }, UIController.Controls.RecordStudentData);
            AddMenu(5, "Class Directory", "Class Directory Services", MenuItem.DoingWhatEnum.Reviewing, Resources.icons8_classroom_200px, SchoolAdministration, new List<User.RoleIdentifier> { User.RoleIdentifier.Admin, User.RoleIdentifier.Accounting }, UIController.Controls.ClassDirectoryService);
            AddMenu(6, "Subject Directory", "Subject Directory Services", MenuItem.DoingWhatEnum.Reviewing, Resources.ReportCard, SchoolAdministration, new List<User.RoleIdentifier> { User.RoleIdentifier.Admin }, UIController.Controls.SubjectDirectoryServices);
            AddMenu(7, "Inventory Directory", "Inventory Directory Services", MenuItem.DoingWhatEnum.Reviewing, Resources.icons8_Travel_Diary_32px, Inventory, new List<User.RoleIdentifier> { User.RoleIdentifier.Admin, User.RoleIdentifier.IT, User.RoleIdentifier.Accounting, User.RoleIdentifier.Management }, UIController.Controls.InventoryDirectory);
        }

        public static void InitCategories()
        {
                
        }

        private static void AddMenu(int id, string title, string description, MenuItem.DoingWhatEnum act, Image img, CategoryMenu category, List<User.RoleIdentifier> Accessor, UIController.Controls controls)
        {
            ItemMenu item = new ItemMenu();
            item.IDItem = id;
            item.ItemName = title;
            item.ItemDescription = description;
            item.TypeDoing = act;
            item.ItemImage = img;
            item.Category = category;
            item.Accessor = Accessor;
            item.Controls = controls;
            item.Init();
            ListOfMenus.Add(item);
        }
        public static List<ItemMenu> ListOfMenus = new List<ItemMenu>();
        private static Size CategoryNormalSize = new Size(142, 57);

        private static void MenuAccounting()
        {
            Generate = new CategoryMenu(1, "Create", "What do you want to create?", CategoryNormalSize, new Point(672, 7));
            
        }

        private static void MenuAdmin()
        {
            Generate = new CategoryMenu(1, "Create", "things you can generate: ", CategoryNormalSize, new Point(672, 7));
            SchoolAdministration = new CategoryMenu(2, "School Administration", "School Administration Menu", CategoryNormalSize, new Point(Generate.Location.X + 156, 7));
            Employee = new CategoryMenu(3, "Employee", "Employee Menu", CategoryNormalSize, new Point(SchoolAdministration.Location.X + 156, 7));
            Inventory = new CategoryMenu(4, "Inventory", "Inventory Menus: ", CategoryNormalSize, new Point(Employee.Location.X + 156, 7));
            Category.AddRange(new List<CategoryMenu> { Generate, SchoolAdministration, Employee, Inventory});
        }

        private static void MenuManagement()
        {
            Generate = new CategoryMenu(1, "Create", "List of things that you can create: ", CategoryNormalSize, new Point(672, 7));
            Employee = new CategoryMenu(2, "Employee", "Employee Administration Menu", CategoryNormalSize, new Point(Generate.Location.X + 156, 7));
            Inventory = new CategoryMenu(3, "Inventory", "Inventory Menu", CategoryNormalSize, new Point(Employee.Location.X + 156, 7));
        }

        public static List<CategoryMenu> GetCategory(User.RoleIdentifier roleIdentifier)
        {
            switch (roleIdentifier)
            {
                case User.RoleIdentifier.Management:
                    MenuManagement();
                    break;
                case User.RoleIdentifier.Admin:
                    MenuAdmin();
                    break;
                case User.RoleIdentifier.IT:

                    break;
                case User.RoleIdentifier.Teacher:

                    break;
                case User.RoleIdentifier.Accounting:

                    break;
                default:

                    break;
            }
            return Category;
        }

        #region List of categories
        public static List<CategoryMenu> Category = new List<CategoryMenu>();
        #region Admin
        private static CategoryMenu SchoolAdministration = null;
        private static CategoryMenu Inventory = null;
        private static CategoryMenu Employee = null;
        private static CategoryMenu Generate = null;
        #endregion


        #endregion

}
}