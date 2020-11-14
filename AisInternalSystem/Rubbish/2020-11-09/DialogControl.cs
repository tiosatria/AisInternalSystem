using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem;
using System.Diagnostics;
using System.IO;
using AisInternalSystem.Controller;

namespace AisInternalSystem
{
    public partial class DialogControl : UserControl
    {

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _subtitle;

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; }
        }
        private Image _image;

        public Image ImageType
        {
            get { return _image; }
            set { _image = value; }
        }
        private string _btnYesLabel;

        public string YesLabel
        {
            get { return _btnYesLabel; }
            set { _btnYesLabel = value; }
        }
        private string _noLabel;

        public string NoLabel
        {
            get { return _noLabel; }
            set { _noLabel = value; }
        }



        public DialogControl()
        {
            InitializeComponent();
        }

        private void FunctionYes()
        {

        }

        private void FunctionNo()
        {
            
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            Confirmation.OnYes();
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            Confirmation.onNo();
        }
    }
}
