using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AISTour.UI;
using System.Windows.Forms;
using System.Drawing;
using Guna.UI2.WinForms;
using Guna.UI2.AnimatorNS;

namespace AISTour.Controller
{
    public class UIController
    {
        public UIController()
        {

        }

        #region UserInterface
        public static WelcomePage WelcomePage = new WelcomePage();
        public static Top_Overlay top_Overlay = new Top_Overlay();
        private static Form MainForm = Application.OpenForms[0];
        private static Guna2Transition transition;
        #endregion

        #region Function
        public static void Animate(Control control, AnimationType animationType)
        {
            transition = new Guna2Transition();
            transition.AnimationType = animationType;
            control.Visible = false;
            transition.Show(control);
        }
        public static void Show(UserControl ctrl, DockStyle dock)
        {
            MainForm.Controls.Add(ctrl);
            ctrl.BringToFront();
            MainForm.Controls[MainForm.Controls.IndexOf(ctrl)].Dock = dock;
        }
        public static void MakePictureOverlayTransparent(PictureBox pic, List<Control> controls)
        {
            foreach (var item in controls)
            {
                item.Parent = pic;
                item.BackColor = Color.Transparent;
            }
        }
        #endregion
    }
}
