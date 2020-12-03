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
    public partial class ClassHistory : UserControl
    {
        public ClassHistory()
        {
            InitializeComponent();
        }

        #region Properties

        private string _classname;

        public string Classname
        {
            get { return _classname; }
            set { _classname = value; lblClassName.Text = value; }
        }
        private string _careteacher;

        public string CareTeacher
        {
            get { return _careteacher; }
            set { _careteacher = value; lblClassCareTeacher.Text = value; }
        }
        private string _schoolyear;

        public string SchoolYear
        {
            get { return _schoolyear; }
            set { _schoolyear = value; lblClassSchoolYear.Text = value; }
        }
        private string _classStatus;

        public string ClassStatus
        {
            get { return _classStatus; }
            set { _classStatus = value; lblClassStat.Text = value; }
        }
        private Image _classPic;

        public Image ClassPic
        {
            get { return _classPic; }
            set { _classPic = value; imgClassPic.Image = value; }
        }

        #endregion

        #region function
        public void ClassImage()
        {

        }

        #endregion

    }
}
