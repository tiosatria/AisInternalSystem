using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using AisInternalSystem.Entities;
using Telerik.WinControls.UI;
using AisInternalSystem.Controller;

namespace AisInternalSystem.UserInterface.Menu
{
    public partial class MenuContainer : UserControl
    {
        #region Properties
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; lblMenu.Text = value; }
        }

        #endregion
        private bool isLoaded;
        public MenuContainer()
        {

        }
        public void InitObject(MenuController.MenuType men)
        {
            if (isLoaded)
            {
                GetMenuItem(men);
            }
            else
            {
                InitializeComponent();
                GetMenuItem(men);
            }
            isLoaded = true;
        }

        private void GetMenuItem(MenuController.MenuType men)
        {
                    MenuController.GetContainerProperties(this, men);
                    flowMenuItems.Controls.Clear();
                    Menus.InitMenus();
                    List<MenuItem> menuItems = Menus.ListMenu;
                    if (menuItems.Count >= 1)
                    {
                        panelnotfound.Visible = false;
                        flowMenuItems.Visible = true;
                        MenuItem[] items = new MenuItem[menuItems.Count];
                        foreach (MenuItem item in menuItems)
                        {
                            if (item.Category.Contains(men))
                            {
                                if (item.Accesor.Contains(Data.user._role))
                                {
                                    flowMenuItems.Controls.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        flowMenuItems.Visible = false;
                        panelnotfound.Visible = true;
                    }
                    DefaultControls = flowMenuItems.Controls;
        }

        private ControlCollection DefaultControls;

        private void btnClose_Click(object sender, EventArgs e)
        {
            UIController.ResetMenu() ;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DoSearch();
        }
        private void DoSearch()
        {
            int i = 0;
            foreach (MenuItem item in flowMenuItems.Controls)
            {
                if (item.Title.ToLower().Contains(txtSearch.Text))
                {
                    flowMenuItems.Controls[flowMenuItems.Controls.IndexOf(item)].Show();
                    item.Visible = true;
                }
                else if (item.Subtitle.ToLower().Contains(txtSearch.Text))
                {
                    flowMenuItems.Controls[flowMenuItems.Controls.IndexOf(item)].Show();
                    item.Visible = true;
                }
                else
                {
                    flowMenuItems.Controls[flowMenuItems.Controls.IndexOf(item)].Hide();
                    item.Visible = false;
                }
            }

        }

        private void MenuContainer_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == Keys.Escape))
            {
                UIController.ResetMenu();
                //Do custom stuff
                //true if key was processed by control, false otherwise
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void guna2ShadowPanel1_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
