using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Student
{
    public partial class UCStudDirectory : UserControl
    {
        private bool IsLoaded = false;
        public static event EventHandler<Entities.Student> CurrentSelectedStudentChanged;
        private Entities.Student CurrentStudent = null;
        public UCStudDirectory()
        {
            UCRecStudent.PictureStudentChanged += UCRecStudent_PictureStudentChanged;
        }
        public void InitObject()
        {
            if (IsLoaded)
            {

            }
            else
            {
                InitializeComponent();
            }

            IsLoaded = true;
        }
        #region Properties
        Entities.Student student = null;
        #endregion
        #region Enum
        public enum SearchBy
        {
            Name,
            ID,
            Gender,
            Origin,
            Revised
        }
        private SearchBy _search = SearchBy.Name;
        #endregion

        private string SearchVal = "";

        private void LoadDGStud()
        {
            switch (_search)
            {
                case SearchBy.Name:
                    dgStudList.DataSource = Entities.Student.GetDataSourceByName(SearchVal);
                    break;
                case SearchBy.ID:
                    if (SearchVal == "")
                    {
                        dgStudList.DataSource = Entities.Student.GetDataSourceByName(SearchVal);
                    }
                    else
                    {
                        dgStudList.DataSource = Entities.Student.GetDataSourceByID(SearchVal);
                    }
                    break;
                case SearchBy.Gender:
                    dgStudList.DataSource = Entities.Student.GetDataSourceByGender(SearchVal);
                    break;
                case SearchBy.Origin:
                    if (SearchVal.ToLower() == "local")
                    {
                        dgStudList.DataSource = Entities.Student.GetDataSourceByLocal();
                    }
                    else
                    {
                        dgStudList.DataSource = Entities.Student.GetDataSourceByExpat();
                    }
                    break;
                case SearchBy.Revised:
                    if (SearchVal.ToLower() == "revised")
                    {

                    }
                    else
                    {
                        dgStudList.DataSource = Entities.Student.GetDataSourceByNotRevised();
                    }
                    break;
            }
            if (dgStudList.Rows.Count >= 1)
            {
                panelEmpty.Visible = false;
                dgStudList.Visible = true;
            }
            else
            {
                panelEmpty.Visible = true;
                dgStudList.Visible = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDGStud();
        }

        private void RadName_CheckedChanged(object sender, EventArgs e)
        {
            txtSearchByName.BringToFront();
            _search = SearchBy.Name;
        }

        private void RadID_CheckedChanged(object sender, EventArgs e)
        {
            txtSearchByName.BringToFront();
            _search = SearchBy.ID;
        }

        private void RadGender_CheckedChanged(object sender, EventArgs e)
        {
            CmbSearch.DataSource = Controller.PublicProperties.Gender;
            CmbSearch.BringToFront();
            CmbSearch.SelectedIndex = 0;
            SearchVal = CmbSearch.SelectedItem.ToString();
            _search = SearchBy.Gender;
        }

        private void RadOrigin_CheckedChanged(object sender, EventArgs e)
        {
            CmbSearch.DataSource = Controller.PublicProperties.Origin;
            CmbSearch.BringToFront();
            CmbSearch.SelectedIndex = 0;
            SearchVal = CmbSearch.SelectedItem.ToString();
            _search = SearchBy.Origin;
        }

        private void RadRevised_CheckedChanged(object sender, EventArgs e)
        {
            CmbSearch.DataSource = Controller.PublicProperties.Revise;
            CmbSearch.BringToFront();
            CmbSearch.SelectedIndex = 0;
            SearchVal = CmbSearch.SelectedItem.ToString();
            _search = SearchBy.Revised;
        }

        private void txtSearchByName_TextChanged(object sender, EventArgs e)
        {
            SearchVal = txtSearchByName.Text;
        }

        private void CmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchVal = CmbSearch.SelectedItem.ToString();
        }

        private int CurrentSelectedAISID = 0;

        private void FetchStudentData()
        {
            try
            {
                CurrentSelectedAISID = Convert.ToInt32(Controller.Utilities.GetSelectedDatagridValue(dgStudList, "AIS ID"));
            }
            catch (Exception)
            {
                CurrentSelectedAISID = 0;
            }

            if (CurrentSelectedAISID != 0)
            {
                student = Entities.Student.GetStudentInfo(CurrentSelectedAISID);
                txtBriefStudName.Text = student.CertificateName;
                txtBriefBirthdate.Text = student.DateofBirth.ToString(Controller.PublicProperties.DateFormat);
                txtBriefNationality.Text = student.Nationality;
                txtFatherContact.Text = student.FatherContact;
                txtMotherContact.Text = student.MotherContact;
                txtStudentAddress.Text = student.HomeAddress;
                txtCurrentClass.Text = student.ClassName;
                if (student.RevisedString == "")
                {
                    lblStudRevisedBy.Text = "Has never been revised";
                }
                else
                {
                    lblStudRevisedBy.Text = $"Data Revised by: {student.RevisedString}";

                }
                lblstudCreatedBy.Text = $"Data Created by: {student.MakerString}";
                lblLastModOn.Text = $"Last modified: {student.DateOfCreation}";
                PhotoLocation = student.PhotoLocation;
                InitPhoto();
                btnDCExpand.Visible = true;
            }

        }
        private BackgroundWorker worker = null;
        private string PhotoLocation = string.Empty;

        private void InitPhoto()
        {
            picStudBrief.Image = Properties.Resources.down_arrow_200_transparent;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            worker.Dispose();
            picStudBrief.Image = cacheImage;
            //Invoke(new MethodInvoker(delegate
            //{
            //    try
            //    {
            //        picStudBrief.Image = cacheImage;
            //    }
            //    catch (Exception)
            //    {
            //        picStudBrief.Image = Properties.Resources.icons8_student_male_80px;
            //    }
            //}
            //       ));
        }

        private Image cacheImage = null;
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (Image img = Image.FromFile(PhotoLocation))
                {
                    Bitmap bitmap = new Bitmap(img);
                    cacheImage = bitmap;
                    img.Dispose();
                }
            }
            catch (Exception)
            {
                cacheImage = Properties.Resources.icons8_student_male_80px;
            }
        }

        private void dgStudList_SelectionChanged(object sender, EventArgs e)
        {
            FetchStudentData();
        }

        private void btnStudEdit_Click(object sender, EventArgs e)
        {
            //picStudBrief.Image.Dispose();
            //cacheImage.Dispose();

            //potential bug
            //if (cacheImage!=null)
            //{
            //    cacheImage = new Bitmap(cacheImage);
            //}
            Controller.UIController.NavigateUI(Controller.UIController.Controls.UpdateStudentData);

        }

        private void UCRecStudent_PictureStudentChanged(object sender, Image e)
        {
            cacheImage = e;
            picStudBrief.Image = cacheImage;
        }

        private void guna2ShadowPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgStudList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
