namespace AisInternalSystem
{
    partial class UCDocsList
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
            this.lblDocsTitle = new System.Windows.Forms.Label();
            this.lblDocsDesc = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.PicDocs = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicDocs)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDocsTitle
            // 
            this.lblDocsTitle.AutoSize = true;
            this.lblDocsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocsTitle.Location = new System.Drawing.Point(118, 18);
            this.lblDocsTitle.Name = "lblDocsTitle";
            this.lblDocsTitle.Size = new System.Drawing.Size(115, 21);
            this.lblDocsTitle.TabIndex = 1;
            this.lblDocsTitle.Text = "Document Title";
            this.lblDocsTitle.Click += new System.EventHandler(this.lblDocsTitle_Click);
            // 
            // lblDocsDesc
            // 
            this.lblDocsDesc.AutoSize = true;
            this.lblDocsDesc.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocsDesc.Location = new System.Drawing.Point(119, 45);
            this.lblDocsDesc.Name = "lblDocsDesc";
            this.lblDocsDesc.Size = new System.Drawing.Size(41, 17);
            this.lblDocsDesc.TabIndex = 2;
            this.lblDocsDesc.Text = "label2";
            this.lblDocsDesc.Click += new System.EventHandler(this.lblDocsDesc_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PicDocs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(112, 91);
            this.panel1.TabIndex = 3;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            this.guna2Elipse1.TargetControl = this;
            // 
            // PicDocs
            // 
            this.PicDocs.Image = global::AisInternalSystem.Properties.Resources.icons8_certificate_80px;
            this.PicDocs.Location = new System.Drawing.Point(12, 3);
            this.PicDocs.Name = "PicDocs";
            this.PicDocs.Size = new System.Drawing.Size(90, 85);
            this.PicDocs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicDocs.TabIndex = 0;
            this.PicDocs.TabStop = false;
            // 
            // UCDocsList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDocsDesc);
            this.Controls.Add(this.lblDocsTitle);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "UCDocsList";
            this.Size = new System.Drawing.Size(398, 91);
            this.Click += new System.EventHandler(this.UCDocsList_Click);
            this.MouseEnter += new System.EventHandler(this.UCDocsList_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UCDocsList_MouseLeave);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicDocs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicDocs;
        private System.Windows.Forms.Label lblDocsTitle;
        private System.Windows.Forms.Label lblDocsDesc;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
