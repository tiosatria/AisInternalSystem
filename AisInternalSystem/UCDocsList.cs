using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Properties;
using System.Resources;
using Microsoft.VisualBasic;
using AisInternalSystem.Module;

namespace AisInternalSystem
{
    public partial class UCDocsList : UserControl
    {
        public UCDocsList()
        {
            InitializeComponent();
        }

        #region Properties
        private string _title;
        private Image _pic;
        private string _subtitle;
        private string _appPath;
        private string _DocType;


        public void Appearance()
        {
            switch (_DocType)
            {
                case "Report Card":
                    this.BackColor = Color.Silver;
                    PicDocs.Image = Resources.ReportCard;
                    break;
                case "Birth Certificate":
                    this.BackColor = Color.Silver;
                    PicDocs.Image = Resources.icons8_birthday_80px;
                    break;
                case "KITAS":
                    this.BackColor = Color.Silver;
                    PicDocs.Image = Resources.icons8_Travel_Diary_32px;
                    break;
                case "Photocopy Family Card (KK)":
                    this.BackColor = Color.Silver;
                    PicDocs.Image = Resources.icons8_family_48px;
                    break;
                case "Photocopy Parents ID (KTP)":
                    this.BackColor = Color.Silver;
                    PicDocs.Image = Resources.icons8_security_pass_24px;
                    break;
                case "Passport":
                    this.BackColor = Color.Silver;
                    PicDocs.Image = Resources.icons8_passport_50px;
                    break;
                case "Transfer Letter":
                    this.BackColor = Color.Silver;
                    PicDocs.Image = Resources.icons8_reply_all_arrow_50px;
                    break;
                case "Other":
                    this.BackColor = Color.Teal;
                    PicDocs.Image = Resources.icons8_google_forms_50px;
                    break;
                case "Certificate":
                    this.BackColor = Color.Silver;
                    PicDocs.Image = Resources.icons8_certificate_80px;
                    break;
            }
        }

        [Category("Custom Properties")]
        public string Title
        {
            get { return _title; }
            set { _title = value; lblDocsTitle.Text = value; }
        }
        [Category("Custom Properties")]
        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; lblDocsDesc.Text = value; }
        }
        public string Doctype
        {
            get { return _DocType; }
            set { _DocType = value; }
        }
        public string AppPath
        {
            get { return _appPath; }
            set { _appPath = value; }
        }
        [Category("Custom Properties")]
        public Image Pic
        {
            get { return _pic; }
            set { _pic = value; PicDocs.Image = value; }
        }
        #endregion

        #region MouseEvent

        void OpenDocument()
        {
            Msg.Alert("Please wait\nWe're looking for the document you clicked.", frmAlert.AlertType.Success);
            System.Diagnostics.Process.Start(AppPath);
        }

        private void UCDocsList_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void UCDocsList_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }

        private void UCDocsList_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
        }
        #endregion

        private void lblDocsTitle_Click(object sender, EventArgs e)
        {
            OpenDocument();

        }

        private void lblDocsDesc_Click(object sender, EventArgs e)
        {
            OpenDocument();

        }
    }
}
