using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem
{
    public partial class UCSubjectTeacher : UserControl
    {
        public UCSubjectTeacher()
        {
            InitializeComponent();
        }

        #region Properties
        private string _teachername;

        public string TeacherName
        {
            get { return _teachername; }
            set { _teachername = value; lblTeachername.Text = value; }
        }
        private string _subjecttaught;

        public string SubjectTaught
        {
            get { return _subjecttaught; }
            set { _subjecttaught = value; lblSubjectTaught.Text = value; }
        }
        private Image _image;

        public Image PictureTeacher
        {
            get { return _image; }
            set { _image = value; picTeacher.Image = value; }
        }

        #endregion

        #region EventListener
        private void guna2ShadowPanel1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Function

        #endregion


    }
}
