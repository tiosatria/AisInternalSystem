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
using AisInternalSystem.Controller;
using System.Runtime.Versioning;
using System.Configuration;
using System.Windows.Markup;
using AisInternalSystem.Entities;

namespace AisInternalSystem
{
    public partial class UCSubjectList : UserControl
    {
        public UCSubjectList()
        {
            InitializeComponent();
        }
        #region Properties
        private int _subjectId;

        public int IndexOnControl;

        public int SubjectID
        {
            get { return _subjectId; }
            set { _subjectId = value; }
        }
        private string _subjectName;

        public string SubjectName
        {
            get { return _subjectName; }
            set { _subjectName = value; lblSubjectName.Text = value; }
        }
        private string _subjectdesc;

        public string SubjectDescription
        {
            get { return _subjectdesc; }
            set { _subjectdesc = value; lblSubjectDesc.Text = value; }
        }

        private Image _image;

        public Image SubjectImage
        {
            get { return _image; }
            set { _image = value; pictureSubject.Image = value; }
        }

        private string _imageLocation;

        public string ImageLocation
        {
            get { return _imageLocation; }
            set
            {
                _imageLocation = value;
                if (value == "Default")
                {
                    SubjectImage = Resources.subjectdefault;
                }
                else if (value == "Biology")
                {
                    SubjectImage = Resources.subjectBiology;
                }
                else if (value == "English")
                {
                    SubjectImage = Resources.subjectEnglish;
                }
                else if (value == "Geography")
                {
                    SubjectImage = Resources.subjectGeography;
                }
                else if (value == "History")
                {
                    SubjectImage = Resources.subjectHistory;
                }
                else if(value == "Math")
                {
                    SubjectImage = Resources.subjectMath;
                }
                else if(value == "Science")
                {
                    SubjectImage = Resources.subjectScience;
                }
                else if (value == "Social")
                {
                    SubjectImage = Resources.subjectSocial;
                }
                else if(value == "Technology")
                {
                    SubjectImage = Resources.subjectTechnology;
                }
                else {
                    try
                    {
                        SubjectImage = Image.FromFile(_imageLocation);
                    }
                    catch (Exception)
                    {
                        SubjectImage = Resources.subjectdefault;
                    }
                }

            }
        }


        private string _taughtBy;

        public string TaughtBy
        {
            get { return _taughtBy; }
            set { _taughtBy = value;
                if (value == "" || value == null)
                {
                    lblIsTaughtBy.Text = "No teacher taught this subject";
                }
                else
                {
                    lblIsTaughtBy.Text = $"Is taught by: {value}";
                }
            }
        }


        private string _taughtin;

        public string TaughtIn
        {
            get { return _taughtin; }
            set
            {
                _taughtin = value; 
                if (value == "" || value == null)
                {
                    lblSubTaught.Text = "Has never been taught";
                }
                else
                {
                    lblSubTaught.Text = $"Also taught in: {value}";
                }
            }
        }


        #endregion

        #region Function

        #endregion

        #region EventListener

        #endregion
        private void OnClick()
        {
            UIController.SubjectChoosed(this);
        }
        private void guna2ShadowPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            OnClick();
        }
    }
}
