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
    public partial class UIMember_AcademicYear : UserControl
    {
        public UIMember_AcademicYear()
        {
            InitializeComponent();
        }

        #region properties
        private string _aycode;

        public string ayCode
        {
            get { return _aycode; }
            set { _aycode = value; lbl_ay_code.Text = value; }
        }
        private string _ay;

        public string ay
        {
            get { return _ay; }
            set { _ay = value; lbl_ay.Text = value; }
        }
        private string _ayterms;

        public string ayTerms
        {
            get { return _ayterms; }
            set { _ayterms = value; lbl_ay_terms.Text = value; }
        }
        private string _aystatus;
        private byte _uniqueKey;

        public byte uniqueKey
        {
            get { return _uniqueKey; }
            set { _uniqueKey = value; }
        }

        public string ayStatus
        {
            get { return _aystatus; }
            set { _aystatus = value; lbl_ay_status.Text = value;
                if (_aystatus == "Ongoing")
                {
                    lbl_ay_status.ForeColor = Color.Red;
                }
                else
                {
                    lbl_ay_status.ForeColor = Color.Green;
                }
            }
        }

        #endregion
    }
}
