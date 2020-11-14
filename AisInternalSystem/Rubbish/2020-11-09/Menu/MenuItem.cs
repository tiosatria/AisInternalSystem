using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Module;
using System.ComponentModel.Design;

namespace AisInternalSystem.UserInterface.Menu
{
    public partial class MenuItem : UserControl
    {
        #region Properties
        private int _index;

        public enum DoingWhatEnum
        {
            Reviewing, Adding, Working, Checking, Marking
        }

        private DoingWhatEnum _doing;
        public DoingWhatEnum Doing
        {
            get { return _doing; }
            set { _doing = value; _act = GetFancyAct(value); }
        }
        private string GetFancyAct(DoingWhatEnum _do)
        {
            switch (_do)
            {
                case DoingWhatEnum.Reviewing:
                    return "Reviwing: ";
                    break;
                case DoingWhatEnum.Adding:
                    return "Adding: ";
                    break;
                case DoingWhatEnum.Working:
                    return "Working on: ";
                    break;
                case DoingWhatEnum.Checking:
                    return "Checking: ";
                    break;
                case DoingWhatEnum.Marking:
                    return "Marking: ";
                    break;
                default:
                    return "Not defined";
                    break;
            }
        }

        private string _act;

        public string ActInString
        {
            get { return _act; }
            set { _act = value; }
        }


        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; lblTitle.Text = value; }
        }
        private string _subtitle;

        public string Subtitle
        {
            get { return _subtitle; }
            set { _subtitle = value; lblSubtitle.Text = value; }
        }
        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; imgPic.Image = value; }
        }
        private MenuController.MenuDoes menuDoes;

        public MenuController.MenuDoes Does
        {
            get { return menuDoes; }
            set { menuDoes = value; }
        }

        private List<MenuController.MenuType> _category;

        public List<MenuController.MenuType> Category
        {
            get { return _category; }
            set { _category = value; }
        }


        private List<User.RoleIdentifier> _accessor;

        public List<User.RoleIdentifier> Accesor
        {
            get { return _accessor; }
            set { _accessor = value; }
        }


        #endregion
        public MenuItem()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            MenuController.DoAction(Does);
        }

        private void MenuItem_Load(object sender, EventArgs e)
        {

        }

        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Coral;
        }

        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
            MenuController.DoAction(Does);

        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {
            MenuController.DoAction(Does);

        }

        private void imgPic_Click(object sender, EventArgs e)
        {
            MenuController.DoAction(Does);

        }
    }
}
