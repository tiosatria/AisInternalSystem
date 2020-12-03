using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using AisInternalSystem.Controller;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Entities;

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
            set { _teachername = value; lblsubteacher.Text = value; }
        }
        private string _grade;

        public string Grade
        {
            get { return _grade; }
            set { _grade = value; lblGrade.Text = value; }
        }

        public int TeacherID { get; set; }

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
            UIController.TeacherRevoke(this);
        }
        #endregion

        #region Function

        #endregion


    }
}
