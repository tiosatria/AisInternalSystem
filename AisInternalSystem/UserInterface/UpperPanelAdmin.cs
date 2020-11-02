using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Entities;
using AisInternalSystem.UserInterface.Menu;
using Guna.UI2.WinForms;
using System.Xml;
using AisInternalSystem.Module;

namespace AisInternalSystem
{
    public partial class UpperPanelAdmin : UserControl
    {
        private bool IsLoaded;
        public UpperPanelAdmin()
        {

        }
        public void InitializeObject()
        {
            if (IsLoaded)
            {

            }
            else
            {
                InitializeComponent();

            }
            IsLoaded = true;
        }


        private MenuController.MenuType _menutype;

        private void BtnDashboardAdmin_Click(object sender, EventArgs e)
        {
            UIController.NavigateUI(UIController.Controls.UCDashboardAdmin);
        }
            
        private void BtnSchoolAdmin_Click(object sender, EventArgs e)
        {
            UIController.MenuChoosed(MenuController.MenuType.SchoolAdministration, BtnSchoolAdmin);
        }

        private void BtnEmployeeAdmin_Click(object sender, EventArgs e)
        {
            UIController.MenuChoosed(MenuController.MenuType.Employee, BtnEmployeeAdmin);
        }

        private void dotter1_Load(object sender, EventArgs e)
        {

        }

        private void BtnInventoryAdmin_Click(object sender, EventArgs e)
        {
            UIController.MenuChoosed(MenuController.MenuType.Inventory, BtnInventoryAdmin);
        }
    }
}
