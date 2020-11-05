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
using AisInternalSystem.Controller;

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
                UIController.GetDragControl(panel_Upper_admin);
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

        private void picBtnExit_Click(object sender, EventArgs e)
        {
            Confirmation.Fire(Confirmation.onConfirmEnum.Exit);
        }

        private void btnFeedback_MouseEnter(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(btnFeedback, UIController.stateofControlEnum.Focused);
        }

        private void btnFeedback_MouseLeave(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(btnFeedback, UIController.stateofControlEnum.Iddle);

        }

        private void Btn_Notif_MouseEnter(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(Btn_Notif, UIController.stateofControlEnum.Focused);

        }

        private void Btn_Notif_MouseLeave(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(Btn_Notif, UIController.stateofControlEnum.Iddle);

        }
        private void picBtnExit_MouseEnter(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(picBtnExit, UIController.stateofControlEnum.Focused);

        }
        private void picBtnExit_MouseLeave(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(picBtnExit, UIController.stateofControlEnum.Iddle);

        }
        private void btnMsg_MouseEnter(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(btnMsg, UIController.stateofControlEnum.Focused);
        }

        private void btnMsg_MouseLeave(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(btnMsg, UIController.stateofControlEnum.Iddle);
        }
    }
}
