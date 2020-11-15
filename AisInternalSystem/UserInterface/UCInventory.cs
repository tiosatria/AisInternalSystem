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

        public void InitObject()
        {
            if (!isLoaded)
            {
                InitializeComponent();
                SwitchEditingMode(EditingMode.Create);
                LeftNavigation(LeftNavigationEnum.Categories);
                RightNavigation(RightNavigationEnum.Object);
                Utilities.SetDoubleBuffer(flowObject, true);
                Utilities.SetDoubleBuffer(FlowCategories, true);
            }
            else
            {

            }
            isLoaded = true;
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
            Edit
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
                    break;
                case LeftNavigationEnum.Object:
                    PanelItems.BringToFront();
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
                    ContainerObject.BringToFront();
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

        private void editingModeCategories()
        {
            switch (_modeEditing)
            {
                case EditingMode.Create:
                    btnCategoriesAction.Text = "Add New Category";
                    BtnCategoriesDElete.Enabled = false;
                    break;
                case EditingMode.Edit:
                    btnCategoriesAction.Text = "Edit Selected Category";
                    BtnCategoriesDElete.Enabled = true;
                    break;
            }
        }

        private void SwitchEditingMode(EditingMode mode)
        {
            _modeEditing = mode;
            switch (mode)
            {
                case EditingMode.Create:
                    btnCatCreate.FillColor = Color.Black;
                    btnCatCreate.ForeColor = Color.White;
                    btnCatEdit.FillColor = Color.Silver;
                    btnCatEdit.ForeColor = Color.Black;
                    editingModeCategories();
                    break;
                case EditingMode.Edit:
                    btnCatCreate.ForeColor = Color.Black;
                    btnCatCreate.FillColor = Color.Silver;
                    btnCatEdit.FillColor = Color.Black;
                    btnCatEdit.ForeColor = Color.White;
                    editingModeCategories();
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
        ItemCategories CurrentCategories = null;
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
            catch (Exception ex)
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
    }
}
