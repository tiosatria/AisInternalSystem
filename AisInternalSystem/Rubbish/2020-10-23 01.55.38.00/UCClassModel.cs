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
    public partial class UCClassModel : UserControl
    {
        bool IsClicked = false;
        UCClassView ClassView = new UCClassView();
        public UCClassModel()
        {
            InitializeComponent();
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
            Dashboard.SelectedString = ClassIdentifier.ToString();
            guna2ShadowPanel1.FillColor = Color.LightCoral;
            guna2ShadowPanel2.FillColor = Color.LightCoral;
            IsClicked = true;

        }
        #endregion
        
        #region EventListener
        private void guna2ShadowPanel1_Click(object sender, EventArgs e)
        {
            OnClick();
        }
        private void guna2ShadowPanel1_MouseEnter(object sender, EventArgs e)
        {
            guna2ShadowPanel1.FillColor = Color.Gainsboro;
            guna2ShadowPanel2.FillColor = Color.Gainsboro;
        }
        private void guna2ShadowPanel1_MouseLeave(object sender, EventArgs e)
        {
            if(!IsClicked)
            {
                guna2ShadowPanel1.FillColor = Color.White;
                guna2ShadowPanel2.FillColor = Color.White;
            }
            else
            {
                if(ClassIdentifier == Convert.ToInt32(Dashboard.SelectedString))
                {
                    guna2ShadowPanel1.FillColor = Color.LightCoral;
                    guna2ShadowPanel2.FillColor = Color.LightCoral;
                }
                else
                {
                    guna2ShadowPanel1.FillColor = Color.White;
                    guna2ShadowPanel2.FillColor = Color.White;
                }

            }
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            Dashboard mainform;
            mainform = (Dashboard)this.FindForm();
            mainform.Controls.Add(ClassView);
            mainform.Controls[mainform.Controls.IndexOf(ClassView)].BringToFront();
            mainform.Controls[mainform.Controls.IndexOf(ClassView)].Dock = DockStyle.Fill;
            ClassView.loadStudentList(ClassIdentifier);
            ClassView.Cswitcher(UCClassView.ClassViewEnum.Grading);
            //mainform.Controls[mainform.Controls.IndexOf(UCSchoolAdm)].BringToFront();
        }
        private void UCClassModel_MouseEnter(object sender, EventArgs e)
        {

        }

        private void UCClassModel_MouseLeave(object sender, EventArgs e)
        {

        }
        private void UCClassModel_MouseClick(object sender, MouseEventArgs e)
        {
            //this shit is useless
        }
        #endregion


    }
}
