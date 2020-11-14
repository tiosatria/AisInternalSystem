using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Entities;
using Microsoft.VisualBasic.CompilerServices;
using Guna.UI2.WinForms;
using Microsoft.ReportingServices.Interfaces;
using AisInternalSystem.Controller;

namespace AisInternalSystem
{
    public partial class UCClassModel : UserControl
    {
        UCClassView ClassView = new UCClassView();
        public UCClassModel()
        {
            InitializeComponent();
            Utilities.SetDoubleBuffer(this, true);

        }

        #region Properties
        private string _careteacher;

        public string CareTeacher
        {
            get { return _careteacher; }
            set { _careteacher = value; lblCareTeacher.Text = value; }
        }

        private string _classname;

        public string ClassName
        {
            get { return _classname; }
            set { _classname = value; lblClassName.Text = value; }
        }
        private string _classMember;

        public string ClassMember
        {
            get { return _classMember; }
            set { _classMember = value; lblClassMember.Text = value; }
        }
        private string _department;

        public string ClassDepartment
        {
            get { return _department; }
            set { _department = value; lblDepartment.Text = value; }
        }
        private Image _picclas;

        public Image PicClass
        {
            get { return _picclas; }
            set { _picclas = value; picClass.Image = value; }
        }
        private int _classidentifier;

        public int ClassIdentifier
        {
            get { return _classidentifier; }
            set { _classidentifier = value; }
        }

        #endregion

        #region Function
        private void OnClick()
        {
            guna2ShadowPanel1.FillColor = Color.Coral;
            guna2ShadowPanel2.FillColor = Color.Coral;
            UIController.ClassChoosed(this);
        }
        #endregion
        
        #region EventListener
        private void guna2ShadowPanel1_Click(object sender, EventArgs e)
        {
            OnClick();
        }
        private void guna2ShadowPanel1_MouseEnter(object sender, EventArgs e)
        {
      
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.ClassName == "NOT ASSIGNED")
            {
                PopUp.Alert("Could not open Unassigned class!", frmAlert.AlertType.Error);
            }
            else
            {
                ClassView.InitObject();
                ClassView.loadStudentList(this.ClassIdentifier);
                ClassView.Cswitcher(UCClassView.ClassViewEnum.Grading);
                UIController.OverrideControl(ClassView, DockStyle.Fill);
            }

            
        }
        private void UCClassModel_MouseEnter(object sender, EventArgs e)
        {

        }

        private void UCClassModel_MouseLeave(object sender, EventArgs e)
        {

        }
        private void UCClassModel_MouseClick(object sender, MouseEventArgs e)
        {

        }

        #endregion

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
