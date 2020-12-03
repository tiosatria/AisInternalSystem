using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AisInternalSystem.Entities;
using AisInternalSystem.Controller;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using AisInternalSystem.UserInterface.Inventory;
using AisInternalSystem.Properties;

namespace AisInternalSystem
{
    public partial class UCInventory : UserControl
    {
        bool isLoaded;
        public UCInventory()
        {

        }

        private void UCInventory_Load(object sender, EventArgs e)
        {
            
        }

        ItemCategories CurrentCategories = null;
        Asset CurrentAsset = null;

        public void InitObject()
        {      
            if (!isLoaded)
            {
                InitializeComponent();
                SwitchEditingMode(EditingMode.Create);
                LeftNavigation(LeftNavigationEnum.Object);
                RightNavigation(RightNavigationEnum.Object);
                Utilities.SetDoubleBuffer(flowAsset, true);
                Utilities.SetDoubleBuffer(FlowCategories, true);
                FetchInitData();
            }
            else
            {

            }
            isLoaded = true;
        }

        private void FetchInitData()
        {
            dropUnitCategory.DataSource = ItemCategories.unit;
            FetchCategories();


        }

        public enum LeftNavigationEnum
        {
            Categories = 0,
            Object = 1
        }
        private LeftNavigationEnum _leftnav;
        public enum RightNavigationEnum
        {
            Object = 0,
            Item = 1,
            Categories = 2,
            Transaction = 3
        }
        private RightNavigationEnum _rightnav;
        public enum EditingMode
        {
            Create,
            Edit,
            View
        }
        EditingMode _modeEditing;

        private void FocusButtonLeft(Guna2Button button)
        {
            btnNavCat.FillColor = Color.White;
            btnNavCat.ForeColor = Color.Black;
            btnNavObj.FillColor = Color.White;
            btnNavObj.ForeColor = Color.Black;
            button.FillColor = Color.Black;
            button.ForeColor = Color.White;
        }
        private void FocusButtonRight(Guna2Button button)
        {
            UIController.HighlightButton(new List<Guna2Button> {btnObject, btnItem, btnCategoriesNav, btnTransaction }, button);
        }

        private void LeftNavigation(LeftNavigationEnum nav)
        {
            _leftnav = nav;
            switch (nav)
            {
                case LeftNavigationEnum.Categories:
                    PanelCategories.BringToFront();
                    UIController.HighlightButton(new List<Guna2Button> { btnNavCat, btnNavObj }, btnNavCat);
                    break;
                case LeftNavigationEnum.Object:
                    PanelItems.BringToFront();
                    UIController.HighlightButton(new List<Guna2Button> { btnNavCat, btnNavObj }, btnNavObj);
                    break;
            }
        }

        private void RightNavigation(RightNavigationEnum nav)
        {
            _rightnav = nav;
            switch (nav)
            {
                case RightNavigationEnum.Object:
                    FocusButtonRight(btnObject);
                    ContainerAsset.BringToFront();
                    break;
                case RightNavigationEnum.Item:
                    FocusButtonRight(btnItem);
                    ContainerItems.BringToFront();
                    break;
                case RightNavigationEnum.Categories:
                    FocusButtonRight(btnCategoriesNav);
                    ContainerCategories.BringToFront();
                    break;
                case RightNavigationEnum.Transaction:
                    FocusButtonRight(btnTransaction);
                    ContainerTransaction.BringToFront();
                    break;
            }
        }

        private void ChangeEditingMode()
        {
            switch (_modeEditing)
            {
                case EditingMode.Create:
                    //category
                    btnCategoriesAction.Text = "Add New Category";
                    BtnCategoriesDElete.Enabled = false;

                    //asset
                    btnAssetAction.Text = "Record new asset";
                    btnDeleteAsset.Enabled = false;
                    break;
                case EditingMode.Edit:
                    btnCategoriesAction.Text = "Edit Selected Category";
                    BtnCategoriesDElete.Enabled = true;
                    btnAssetAction.Text = "Revise asset";
                    btnDeleteAsset.Enabled = true;
                    break;
                case EditingMode.View:

                    break;
            }
        }

        private void SwitchEditingMode(EditingMode mode)
        {
            _modeEditing = mode;
            switch (mode)
            {
                case EditingMode.Create:
                    UIController.HighlightButton(new List<Guna2Button> {btnCatCreate, btnCatEdit, btnView }, btnCatCreate);
                    ChangeEditingMode();
                    break;
                case EditingMode.Edit:
                    UIController.HighlightButton(new List<Guna2Button> {btnCatCreate, btnCatEdit, btnView }, btnCatEdit);
                    ChangeEditingMode();
                    break;
                case EditingMode.View:
                    UIController.HighlightButton(new List<Guna2Button> { btnCatCreate, btnCatEdit, btnView }, btnView);
                    ChangeEditingMode();
                    break;
            }
        }

        private void btnCatCreate_Click(object sender, EventArgs e)
        {
            SwitchEditingMode(EditingMode.Create);
        }

        private void btnCatEdit_Click(object sender, EventArgs e)
        {
            SwitchEditingMode(EditingMode.Edit);
        }
        private void CategoryInsert()
        {
            ItemCategories item = new ItemCategories();
            item.Category = txtCategoryName.Text;
            item.CategoryDescription = txtCategoryDescription.Text;
            item.Unit = dropUnitCategory.SelectedItem.ToString();
            if (ItemCategories.Input(item))
            {
                PopUp.Alert("Category Added Succesfully!", frmAlert.AlertType.Success);
                FillDG();

            }
            else
            {
                PopUp.Alert("Failed to Add Category :(", frmAlert.AlertType.Success);

            }
        }
        private void CategoryUpdate()
        {
            if (CurrentCategories != null)
            {
                if(ItemCategories.Update(CurrentCategories))
                {
                    PopUp.Alert("Category Updated Succesfully!", frmAlert.AlertType.Success);
                    FillDG();

                }
                else
                {
                    PopUp.Alert("Failed to Update Category!", frmAlert.AlertType.Warning);

                }
            }
        }
        private void CategoryDelete()
        {
            if (CurrentCategories != null)
            {
                if (ItemCategories.Delete(CurrentCategories))
                {
                    PopUp.Alert("Category Deleted Succesfully!", frmAlert.AlertType.Success);
                    FillDG();
                }
                else
                {
                    PopUp.Alert("Failed to delete Category!", frmAlert.AlertType.Error);
                }
            }
        }
        private void CategoryAction()
        {
            switch (_modeEditing)
            {
                case EditingMode.Create:
                    if (txtCategoryName.Text != "")
                    {
                        CategoryInsert();
                    } 
                    else
                    {
                        PopUp.Alert("Please input Category Name and Description", frmAlert.AlertType.Warning);
                    }
                    break;
                case EditingMode.Edit:
                    if (CurrentCategories != null)
                    {
                        CategoryUpdate();
                    }
                    else
                    {
                        PopUp.Alert("Please select category to edit!", frmAlert.AlertType.Warning);
                    }
                    break;
            }
        }
        private void btnCategoriesAction_Click(object sender, EventArgs e)
        {
            CategoryAction();
        }

        private void FillDG()
        {
            dgCategories.DataSource = ItemCategories.GetDataSource();

        }

        private void ReadFromDGCategories()
        {
            try
            {
                if (dgCategories.SelectedRows.Count >= 1)
                {
                    if (Utilities.GetSelectedDatagridValue(dgCategories, "IDCat") != "" || Utilities.GetSelectedDatagridValue(dgCategories, "IDCat") != null)
                    {
                        CurrentCategories = new ItemCategories();
                        CurrentCategories.Category = Utilities.GetSelectedDatagridValue(dgCategories, "Category");
                        CurrentCategories.CategoryDescription = Utilities.GetSelectedDatagridValue(dgCategories, "Description");
                        CurrentCategories.Unit = Utilities.GetSelectedDatagridValue(dgCategories, "UNIT");
                        CurrentCategories.CategoryID = Convert.ToInt32(Utilities.GetSelectedDatagridValue(dgCategories, "IDCat"));
                        txtCategoryName.Text = CurrentCategories.Category;
                        txtCategoryDescription.Text = CurrentCategories.CategoryDescription;
                        dropUnitCategory.SelectedIndex = dropUnitCategory.Items.IndexOf(CurrentCategories.Unit);
                    }
                    else
                    {
                        CurrentCategories = null;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (_modeEditing == EditingMode.Edit)
            {
                ReadFromDGCategories();
            }
        }

        private void BtnCategoriesDElete_Click(object sender, EventArgs e)
        {
            CategoryDelete();
        }

        private void btnNavCat_Click(object sender, EventArgs e)
        {
            LeftNavigation(LeftNavigationEnum.Categories);
            FocusButtonLeft(btnNavCat);
        }

        private void btnNavObj_Click(object sender, EventArgs e)
        {
            LeftNavigation(LeftNavigationEnum.Object);
            FocusButtonLeft(btnNavObj);

        }

        private void btnObject_Click(object sender, EventArgs e)
        {
            RightNavigation(RightNavigationEnum.Object);
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            RightNavigation(RightNavigationEnum.Item);

        }

        private void btnCategoriesNav_Click(object sender, EventArgs e)
        {
            RightNavigation(RightNavigationEnum.Categories);

        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            RightNavigation(RightNavigationEnum.Transaction);

        }

        OpenFileDialog assetImageLocation = null;

        private void picAssetImgLocation_Click(object sender, EventArgs e)
        {
            assetImageLocation = Utilities.OpenImage(picAssetImgLocation);
        }

        private void btnAddAssetRecord_Click(object sender, EventArgs e)
        {
            switch (_modeEditing)
            {
                case EditingMode.Create:
                    if (txtAssetName.Text == "" || txtPrimaryStorageLocation.Text == "")
                    {
                        PopUp.Alert(Asset.MsgDataNotValid, frmAlert.AlertType.Warning);
                    }
                    else
                    {
                        AssetInsert();
                    }
                    break;
                case EditingMode.Edit:
                    if (txtAssetName.Text == "" || txtPrimaryStorageLocation.Text == "")
                    {
                        PopUp.Alert(Asset.MsgDataNotValid, frmAlert.AlertType.Warning);
                    }
                    else
                    {
                        AssetUpdate();
                    }
                    break;
                case EditingMode.View:

                    break;
            }
            
        }

        private void AssetInsert()
        {
            Asset asset = new Asset();
            asset.AssetDescription = txtAssetDescription.Text;
            asset.NameAsset = txtAssetName.Text;
            asset.PrimaryStorageLocation = txtPrimaryStorageLocation.Text;
            asset.CategoryAsset = dropAssetCategory.SelectedValue.ToString();
            try
            {
                asset.AssetPrice = Convert.ToDecimal(txtAssetPrice.Text);
            }
            catch (Exception ex)
            {
                PopUp.Alert($"Price data not valid\nErr Code: {ex.Message}", frmAlert.AlertType.Error);
                asset.AssetPrice = 0;
            }
            if (assetImageLocation != null)
            {
                try
                {
                    asset.ImageLocation = Utilities.GetFileDbLocationString(Utilities.LocationType.InventoryPhoto, txtAssetName.Text, assetImageLocation);
                }
                catch (Exception)
                {
                    asset.ImageLocation = null;
                    PopUp.Alert("An error occured, no image will be saved", frmAlert.AlertType.Warning);
                }
            }
            else
            {
                asset.ImageLocation = null;
            }
            if (Asset.Input(asset))
            {
                PopUp.Alert("Asset recorded succesfully!", frmAlert.AlertType.Success);
                FetchAsset();
                ClearInputAsset();
            }
            else
            {
                PopUp.Alert("Failed to save asset!", frmAlert.AlertType.Error);
            }
        }
        private void ClearInputAsset()
        {
            foreach (var control in PanelAssetInput.Controls)
            {
                if (control is Guna2TextBox)
                {
                    Guna2TextBox txtbox = control as Guna2TextBox;
                    txtbox.Clear();
                }
                if (control is Guna2ComboBox)
                {
                    Guna2ComboBox combo = control as Guna2ComboBox;
                    combo.SelectedIndex = 0;
                }
                if (control is PictureBox)
                {
                    PictureBox picture = control as PictureBox;
                    picture.Image = Resources.icons8_trolley_100px;
                }
                assetImageLocation = null;
            }
        }
        private void FetchCurrentAsset()
        {

        }
        private void FetchCategories()
        {
            BackgroundWorker WorkerCategory = new BackgroundWorker();
            WorkerCategory.DoWork += WorkerCategory_DoWork;
            WorkerCategory.RunWorkerAsync();
            WorkerCategory.RunWorkerCompleted += WorkerCategory_RunWorkerCompleted;
        }

        private void WorkerCategory_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FetchAsset();
            if (ItemCategories.CategoryList.Count >= 1)
            {
                dropAssetCategory.DataSource = ItemCategories.CategoryList;
                dropAssetCategory.SelectedIndex = 0;
            }
        }

        private void WorkerCategory_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = ItemCategories.GetDataSource();
            if (dt.Rows.Count >= 1)
            {
                FlowCategories.Controls.Clear();
                ModelCategory[] modelCategories = new ModelCategory[dt.Rows.Count];
                for (int i = 0; i < modelCategories.Length; i++)
                {
                    modelCategories[i] = new ModelCategory();
                    modelCategories[i].CategoryName = dt.Rows[i][0].ToString();
                    modelCategories[i].CategoryDescription = dt.Rows[i][1].ToString();
                    modelCategories[i].Unit = dt.Rows[i][2].ToString();
                    Invoke(new MethodInvoker(delegate { FlowCategories.Controls.Add(modelCategories[i]); }));
                    Invoke(new MethodInvoker(delegate { lblTotalCategory.Text = $"Loaded: {FlowCategories.Controls.Count.ToString()}"; }));
                }
            }
            Invoke(new MethodInvoker(delegate { lblTotalCategory.Text = $"Total: {FlowCategories.Controls.Count.ToString()}"; }));
        }

        private void FetchAsset()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = Asset.GetDataSource();
            if (dt.Rows.Count >= 1)
            {
                ModelAsset[] modelAssets = new ModelAsset[dt.Rows.Count];
                for (int i = 0; i < modelAssets.Length; i++)
                {
                    modelAssets[i] = new ModelAsset();
                    modelAssets[i].AssetName = dt.Rows[i][3].ToString();
                    modelAssets[i].AssetDescription = dt.Rows[i][4].ToString();
                    modelAssets[i].AssetCategory = dt.Rows[i][1].ToString();
                    modelAssets[i].ImageLocation = dt.Rows[i][6].ToString();
                    modelAssets[i].AssetQty = dt.Rows[i][8].ToString();
                    modelAssets[i].AssetPrice = dt.Rows[i][6].ToString();
                    modelAssets[i].PrimaryLocation = dt.Rows[i][7].ToString();
                    Invoke(new MethodInvoker(delegate { flowAsset.Controls.Add(modelAssets[i]); }));
                    Invoke(new MethodInvoker(delegate { lblAssetCount.Text = "Loaded: " + flowAsset.Controls.Count; }));
                }
            }
            Invoke(new MethodInvoker(delegate { lblAssetCount.Text = "Total: " + flowAsset.Controls.Count; }));
        }

        private void AssetUpdate()
        {
            CurrentAsset.AssetDescription = txtAssetDescription.Text;
            CurrentAsset.NameAsset = txtAssetName.Text;
            CurrentAsset.PrimaryStorageLocation = txtPrimaryStorageLocation.Text;
            CurrentAsset.CategoryAsset = dropAssetCategory.SelectedValue.ToString();
            try
            {
                CurrentAsset.AssetPrice = Convert.ToDecimal(txtAssetPrice.Text);
            }
            catch (Exception ex)
            {
                PopUp.Alert($"Price data not valid\nErr Code: {ex.Message}", frmAlert.AlertType.Error);
                CurrentAsset.AssetPrice = 0;
            }
            if (assetImageLocation != null)
            {
                try
                {
                    CurrentAsset.ImageLocation = Utilities.GetFileDbLocationString(Utilities.LocationType.InventoryPhoto, txtAssetName.Text, assetImageLocation);
                }
                catch (Exception)
                {
                    CurrentAsset.ImageLocation = null;
                    PopUp.Alert("An error occured, no image will be saved", frmAlert.AlertType.Warning);
                }
            }
            if (Asset.Input(CurrentAsset))
            {
                PopUp.Alert("Asset revised succesfully!", frmAlert.AlertType.Success);
                FetchAsset();
            }
            else
            {
                PopUp.Alert("Failed to save asset!", frmAlert.AlertType.Error);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            SwitchEditingMode(EditingMode.View);
        }
    }
}
