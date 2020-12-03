namespace AisInternalSystem.UserInterface.Inventory
{
    partial class ModelAsset
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.elipser = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblAssetName = new System.Windows.Forms.Label();
            this.lblAssetDesc = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblLocationStore = new System.Windows.Forms.Label();
            this.AssetImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.AssetImage)).BeginInit();
            this.SuspendLayout();
            // 
            // elipser
            // 
            this.elipser.TargetControl = this;
            // 
            // lblAssetName
            // 
            this.lblAssetName.AutoSize = true;
            this.lblAssetName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssetName.Location = new System.Drawing.Point(12, 7);
            this.lblAssetName.Name = "lblAssetName";
            this.lblAssetName.Size = new System.Drawing.Size(97, 21);
            this.lblAssetName.TabIndex = 0;
            this.lblAssetName.Text = "Asset Name";
            // 
            // lblAssetDesc
            // 
            this.lblAssetDesc.AutoSize = true;
            this.lblAssetDesc.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssetDesc.Location = new System.Drawing.Point(12, 36);
            this.lblAssetDesc.Name = "lblAssetDesc";
            this.lblAssetDesc.Size = new System.Drawing.Size(85, 20);
            this.lblAssetDesc.TabIndex = 1;
            this.lblAssetDesc.Text = "Description";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(13, 72);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(35, 20);
            this.lblQty.TabIndex = 2;
            this.lblQty.Text = "Qty:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(13, 97);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(44, 20);
            this.lblPrice.TabIndex = 3;
            this.lblPrice.Text = "Price:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(12, 139);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(76, 20);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category: ";
            // 
            // lblLocationStore
            // 
            this.lblLocationStore.AutoSize = true;
            this.lblLocationStore.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationStore.Location = new System.Drawing.Point(13, 118);
            this.lblLocationStore.Name = "lblLocationStore";
            this.lblLocationStore.Size = new System.Drawing.Size(117, 20);
            this.lblLocationStore.TabIndex = 6;
            this.lblLocationStore.Text = "Location Stored:";
            // 
            // AssetImage
            // 
            this.AssetImage.Image = global::AisInternalSystem.Properties.Resources.icons8_trolley_100px;
            this.AssetImage.Location = new System.Drawing.Point(290, 6);
            this.AssetImage.Name = "AssetImage";
            this.AssetImage.Size = new System.Drawing.Size(70, 66);
            this.AssetImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AssetImage.TabIndex = 5;
            this.AssetImage.TabStop = false;
            // 
            // ModelAsset
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.lblLocationStore);
            this.Controls.Add(this.AssetImage);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblAssetDesc);
            this.Controls.Add(this.lblAssetName);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ModelAsset";
            this.Size = new System.Drawing.Size(369, 167);
            this.MouseEnter += new System.EventHandler(this.ModelAsset_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ModelAsset_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.AssetImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse elipser;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblAssetDesc;
        private System.Windows.Forms.Label lblAssetName;
        private System.Windows.Forms.PictureBox AssetImage;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblLocationStore;
    }
}
