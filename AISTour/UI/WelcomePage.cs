using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AISTour.Controller;
using System.Windows.Forms;

namespace AISTour.UI
{
    public partial class WelcomePage : UserControl
    {
        public WelcomePage()
        {
            InitializeComponent();
            InitLabel();
        }

        private void InitLabel()
        {
            UIController.Animate(Background, Guna.UI2.AnimatorNS.AnimationType.Transparent);
            UIController.MakePictureOverlayTransparent(Background, new List<Control> { LabelWelcome, pictureBox1, guna2GradientButton1, label1});
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
