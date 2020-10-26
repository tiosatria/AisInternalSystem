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
    public partial class UCSubjectModel : UserControl
    {
        public UCSubjectModel()
        {
            InitializeComponent();
        }

        #region Properties
        private string _subjectname;

        public string SubjectName
        {
            get { return _subjectname; }
            set { _subjectname = value; lblSubjectName.Text = value; }
        }
        private string _taughtby;

        public string TaughtBy
        {
            get { return _taughtby; }
            set { _taughtby = value; lbltaughtby.Text = value; }
        }
        private Image _subjectImage;

        public Image SubjectImage
        {
            get { return _subjectImage; }
            set { _subjectImage = value; pictureSubject.Image = value; }
        }
        private string _subjectdesc;

        public string SubjectDescription
        {
            get { return _subjectdesc; }
            set { _subjectdesc = value; lblSubjectDesc.Text = value; }
        }

        #endregion

        #region Function


        #endregion

        #region EventListener
        private void label1_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
