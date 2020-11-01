using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AisInternalSystem.Module;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace AisInternalSystem
{
    public partial class PanelMSqlCommand : UserControl
    {
        public PanelMSqlCommand()
        {
            InitializeComponent();
        }

        #region Properties
        private string _title;
        private string _subtittle;
        private Image _img;
        private int _data;

        public string Title
        {
            get { return _title; }
            set { _title = value; lblTitle.Text = value; }
        }
        public string Subtittle
        {
            get { return _subtittle; }
            set { _subtittle = value; lblsubtitle.Text = value; }
        }

        public Image Img
        {
            get { return _img; }
            set { _img = value; ImgPic.Image = value; }
        }

        public int Data
        {
            get { return _data; }
            set { _data = value;}
        }

        #endregion

        #region Function
        public void Clicked()
        {
            
        }

        #endregion

        #region MouseEvent
        private void PanelMSqlCommand_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }

        private void PanelMSqlCommand_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
        }
        #endregion

        private void PanelMSqlCommand_Click(object sender, EventArgs e)
        {
            Clicked();
        }
    }
}
