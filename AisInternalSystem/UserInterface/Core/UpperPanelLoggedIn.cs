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
using AisInternalSystem.Properties;

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
                GetCategory();
                UIController.Animate(this, Guna.UI2.AnimatorNS.AnimationType.HorizSlide);
                UIController.GetDragControl(panel_Upper_admin);
                UserInterface.Settings.UserSettings.PictureBeingChanged += UserSettings_PictureBeingChanged;
                UserInterface.Settings.UserSettings.PictureChangedEvent += UserSettings_PictureChangedEvent;
            }
            IsLoaded = true;
        }

        private void UserSettings_PictureChangedEvent(object sender, EventArgs e)
        {
            using (Image img = Image.FromFile(Data.user.UserImage))
            {
                Bitmap bitmap = new Bitmap(img);
                picThumbUser.Image = bitmap;
                img.Dispose();
            }
        }

        private void UserSettings_PictureBeingChanged(object sender, EventArgs e)
        {
            
        }

        private Image imgUser;
        List<CategoryMenu> Categories;
        private void GetCategory()
        {
            Categories = Menus.GetCategory(Data.user._role);
            for (int i = 0; i < Categories.Count; i++)
            {
                panel_Upper_admin.Controls.Add(Categories[i].CategoryHandler);
                panel_Upper_admin.Controls.Add(Categories[i].separator);
            }
        }
        private string _img;
        public string imgLocation
        {
            get { return _img; }
            set
            {
                _img = value;
                try
                {
                    using (Image img = Image.FromFile(value))
                    {
                        Bitmap b = new Bitmap(img);
                        picThumbUser.Image = b;
                        img.Dispose();
                    }
                }
                catch (Exception)
                {
                    picThumbUser.Image = Resources.icons8_male_user_100;
                }
            }
        }
        public Image UserImage
        {
            get { return imgUser; }
            set
            {
                imgUser = value;
            }
        }

        private void BtnDashboardAdmin_Click(object sender, EventArgs e)
        {
            UIController.NavigateUI(UIController.Controls.UCDashboardAdmin);
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

        private void picThumbUser_Click(object sender, EventArgs e)
        {
            UIController.NavigateUI(UIController.Controls.UserSettings);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UIController.MinimizeApp();
        }

        private int picThumb = 0;
        private void picThumbUser_MouseEnter(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(picThumbUser, UIController.stateofControlEnum.Focused);
            if (picThumb <= 2)
            {
                PopUp.Alert("Click to open user settings!", frmAlert.AlertType.Info);
                picThumb++;
            }
        }

        private void picThumbUser_MouseLeave(object sender, EventArgs e)
        {
            UIController.ImageButtonZoom(picThumbUser, UIController.stateofControlEnum.Iddle);

        }

        private int picMinimize = 0;
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (picMinimize <= 2)
            {
                PopUp.Alert("Click ('-') button to minimize the system!", frmAlert.AlertType.Info);
                picMinimize++;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            UIController.NavigateUI(UIController.Controls.UCDashboardAdmin);
        }
    }
}
