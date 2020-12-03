using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Inventory
{
    public partial class ModelCategory : UserControl
    {
        public ModelCategory()
        {
            InitializeComponent();
        }

        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _catName;

        public string CategoryName
        {
            get { return _catName; }
            set { _catName = value; lblName.Text = $"{value}"; }
        }
        private string _catDescription;

        public string CategoryDescription
        {
            get { return _catDescription; }
            set { _catDescription = value;
                if (value == "" || value == null)
                {
                    lblDesc.Text = "No Description.";
                }
                else
                {
                    lblDesc.Text = $"Description: {value}";
                }

            }
        }
        private string _unit;

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; lblUnit.Text = $"Unit: {value}"; }
        }


        private void ModelCategory_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
        }

        private void ModelCategory_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }
    }
}
