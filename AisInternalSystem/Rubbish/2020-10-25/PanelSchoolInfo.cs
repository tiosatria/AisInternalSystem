using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI.Design;
using AisInternalSystem.Properties;

namespace AisInternalSystem
{
    public partial class PanelSchoolInfo : UserControl
    {
        public PanelSchoolInfo()
        {
            InitializeComponent();
        }

        #region Properties
        private string _schoolName;

        public string SchoolName
        {
            get { return _schoolName; }
            set { _schoolName = value;lblSchoolName.Text = value; }
        }
        private string _curriculum;

        public string Curriculum
        {
            get { return _curriculum; }
            set { _curriculum = value; lblCurriculum.Text = value; }
        }
        private string _dateattended;

        public string DateAttended
        {
            get { return _dateattended; }
            set { _dateattended = value; lblDateAttended.Text = value; }
        }
        private string _grade;

        public string Grade
        {
            get { return _grade; }
            set { _grade = value; lblgrade.Text = value; }
        }
        private string _extraSupport;

        public string ExtraSupport
        {
            get { return _extraSupport; }
            set { _extraSupport = value; lblwithextrasupport.Text = value; }
        }

        private string _country;

        public string Country   
        {
            get { return _country; }
            set { _country = value; lblcountry.Text = value; }
        }

        private Image _picSchool;

        public Image PicSchool
        {
            get { return _picSchool; }
            set { _picSchool = value; Picschool.Image = value; }
        }

        #endregion

        #region function
        public void SchooLLogo()
        {
            //avava
            if(lblSchoolName.Text.Contains("avava") | lblSchoolName.Text.Contains("Avava"))
            {
                Picschool.Image = Resources.Avava;
            }
            else
            {
                Picschool.Image = Resources.icons8_school_96px;
            }
        }

        #endregion
    }
}
