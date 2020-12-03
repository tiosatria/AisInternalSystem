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
    public partial class UCClassMember : UserControl
    {
        public UCClassMember()
        {
            InitializeComponent();
        }

        #region Properties

        private int _id;
        public int StudId
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _rownumber;
        public int RowNumber
        {
            get { return _rownumber; }
            set { _rownumber = value; lblNumbering.Text = value.ToString(); }
        }
        private int _aisid;
        public int Aisid
        {
            get { return _aisid; }
            set { _aisid = value; }
        }
        private string _Name;
        public string StudentName
        {
            get { return _Name; }
            set { _Name = value; lblName.Text = value; }
        }
        private string _religion;
        public string Religion
        {
            get { return _religion; }
            set { _religion = value; lblReligion.Text = value; }
        }
        private string _birthdate;
        public string Birthdate
        {
            get { return _birthdate; }
            set { _birthdate = value; lblBirthdate.Text = value; }
        }
        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; lblMobile.Text = value; }
        }
        private string _engprof;
        public string EnglishProficiency
        {
            get { return _engprof; }
            set { _engprof = value; lblEngProf.Text = value; }
        }
        private Image _studentpicture;

        public Image StudentPicture
        {
            get { return _studentpicture; }
            set { _studentpicture = value;
                try
                {
                    PicStud.Image = value;
                }
                catch (Exception)
                {
                    PicStud.Image = AisInternalSystem.Properties.Resources.icons8_student_male_80px;
                }
                 }
        }
        #endregion
        #region ActionListener
        private void guna2ShadowPanel1_Click(object sender, EventArgs e)
        {

        }
        private void lblName_Click(object sender, EventArgs e)
        {

        }
        private void lblReligion_Click(object sender, EventArgs e)
        {

        }
        private void lblBirthdate_Click(object sender, EventArgs e)
        {

        }
        private void lblMobile_Click(object sender, EventArgs e)
        {

        }
        private void lblEngProf_Click(object sender, EventArgs e)
        {

        }
        private void guna2ShadowPanel1_MouseEnter(object sender, EventArgs e)
        {

        }
        private void guna2ShadowPanel2_MouseEnter(object sender, EventArgs e)
        {

        }
        #endregion

        #region Function
        void OnHover()
        {

        }
        void OnClick()
        {

        }
        #endregion
    }
}
