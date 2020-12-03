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
using System.Threading;

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
        private bool isLoaded = false;
        public MenuContainer()
        {
            InitializeComponent();
            this.Visible = false;
            Utilities.SetDoubleBuffer(this, true);
            Utilities.SetDoubleBuffer(flowMenuItems, true);
            this.Location = new Point(640, 109);

        }
        public void InitObject(CategoryMenu men)
        {
            if (isLoaded)
            {

            }
            else
            {
                InitMenus();
            }
            GetMenuItem(men);

            isLoaded = true;
        }

        private void GetMenuItem(CategoryMenu menu)
        {
            this.lblMenu.Text = menu.CategoryDescription;
            foreach (MenuItem item in flowMenuItems.Controls)
            {
                if (item.Category == menu && item.Accessor.Contains(Data.user._role))
                {
                    item.Visible = true;
                }
                else
                {
                    item.Visible = false;
                }
            }
        }

        private void InitMenus()
        {
            Menus.InitMenus();
            for (int i = 0; i < Menus.ListOfMenus.Count; i++)
            {
                Menus.ListOfMenus[i].Item.Visible = false;
                flowMenuItems.Controls.Add(Menus.ListOfMenus[i].Item);
            }
        }


        private ControlCollection DefaultControls;

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            UIController.ResetMenu();
        }
    }
}
