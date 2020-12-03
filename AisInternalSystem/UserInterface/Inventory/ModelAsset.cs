using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using AisInternalSystem.Properties;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Inventory
{
    public partial class ModelAsset : UserControl
    {
        public ModelAsset()
        {
            InitializeComponent();
        }

        #region Properties
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _assetname;

        public string AssetName
        {
            get { return _assetname; }
            set { _assetname = value; lblAssetName.Text = value; }
        }

        private string _desc;

        public string AssetDescription
        {
            get { return _desc; }
            set
            {
                _desc = value; if (value == "" || value == null)
                {
                    lblAssetDesc.Text = "No description.";
                }; }
        }

        private string _qty;

        public string AssetQty
        {
            get { return _qty; }
            set { _qty = value; lblQty.Text = $"Qty: {value}"; }
        }

        private string _price;

        public string AssetPrice
        {
            get { return _price; }
            set { _price = value; lblPrice.Text =  $"Price: {value}"; }
        }

        private string _category;

        public string AssetCategory
        {
            get { return _category; }
            set { _category = value; lblCategory.Text = $"Category: {value}"; }
        }

        private string _imgLocation;

        public string ImageLocation
        {
            get { return _imgLocation; }
            set {
                _imgLocation = value;
                {
                    try
                    {
                        _image = Image.FromFile(value);
                    }

                catch (Exception ex)
                    {
                        _image = Resources.icons8_trolley_100px;
                    }
                }
            }
        }

        private string _primarylocation;

        public string PrimaryLocation
        {
            get { return _primarylocation; }
            set { _primarylocation = value; lblLocationStore.Text = $"Location stored: {value}"; }
        }


        private Image _image;

        public Image AssetIMG
        {
            get { return _image; }
            set { _image = value; AssetImage.Image = value; }
        }


        #endregion

        #region EventHandler

        #endregion

        private void ModelAsset_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
        }

        private void ModelAsset_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }
    }
}
