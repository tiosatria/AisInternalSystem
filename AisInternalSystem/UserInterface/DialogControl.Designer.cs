namespace AisInternalSystem
{
    partial class DialogControl
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
            this.lbldialogHeader = new System.Windows.Forms.Label();
            this.lbldialogSubtext = new System.Windows.Forms.Label();
            this.BtnYes = new Guna.UI2.WinForms.Guna2Button();
            this.BtnNo = new Guna.UI2.WinForms.Guna2Button();
            this.picIllustration = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIllustration)).BeginInit();
            this.SuspendLayout();
            // 
            // lbldialogHeader
            // 
            this.lbldialogHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbldialogHeader.AutoSize = true;
            this.lbldialogHeader.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldialogHeader.Location = new System.Drawing.Point(21, 17);
            this.lbldialogHeader.Name = "lbldialogHeader";
            this.lbldialogHeader.Size = new System.Drawing.Size(660, 45);
            this.lbldialogHeader.TabIndex = 13;
            this.lbldialogHeader.Text = "Are you sure you want to exit Application?";
            // 
            // lbldialogSubtext
            // 
            this.lbldialogSubtext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbldialogSubtext.AutoSize = true;
            this.lbldialogSubtext.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldialogSubtext.Location = new System.Drawing.Point(24, 73);
            this.lbldialogSubtext.Name = "lbldialogSubtext";
            this.lbldialogSubtext.Size = new System.Drawing.Size(261, 25);
            this.lbldialogSubtext.TabIndex = 15;
            this.lbldialogSubtext.Text = "Any unsaved data will be lost";
            // 
            // BtnYes
            // 
            this.BtnYes.Animated = true;
            this.BtnYes.AutoRoundedCorners = true;
            this.BtnYes.BorderRadius = 30;
            this.BtnYes.CheckedState.Parent = this.BtnYes;
            this.BtnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnYes.CustomImages.Parent = this.BtnYes;
            this.BtnYes.FillColor = System.Drawing.Color.Silver;
            this.BtnYes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnYes.ForeColor = System.Drawing.Color.Black;
            this.BtnYes.HoverState.FillColor = System.Drawing.Color.LightCoral;
            this.BtnYes.HoverState.Parent = this.BtnYes;
            this.BtnYes.Location = new System.Drawing.Point(269, 521);
            this.BtnYes.Name = "BtnYes";
            this.BtnYes.ShadowDecoration.Parent = this.BtnYes;
            this.BtnYes.Size = new System.Drawing.Size(331, 63);
            this.BtnYes.TabIndex = 16;
            this.BtnYes.Text = "Yes, let me get out of here";
            this.BtnYes.Click += new System.EventHandler(this.BtnYes_Click);
            // 
            // BtnNo
            // 
            this.BtnNo.Animated = true;
            this.BtnNo.AutoRoundedCorners = true;
            this.BtnNo.BorderRadius = 30;
            this.BtnNo.CheckedState.Parent = this.BtnNo;
            this.BtnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnNo.CustomImages.Parent = this.BtnNo;
            this.BtnNo.FillColor = System.Drawing.Color.Silver;
            this.BtnNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNo.ForeColor = System.Drawing.Color.Black;
            this.BtnNo.HoverState.FillColor = System.Drawing.Color.LightCoral;
            this.BtnNo.HoverState.Parent = this.BtnNo;
            this.BtnNo.Location = new System.Drawing.Point(634, 521);
            this.BtnNo.Name = "BtnNo";
            this.BtnNo.ShadowDecoration.Parent = this.BtnNo;
            this.BtnNo.Size = new System.Drawing.Size(331, 63);
            this.BtnNo.TabIndex = 17;
            this.BtnNo.Text = "No, let me stay :(";
            this.BtnNo.Click += new System.EventHandler(this.BtnNo_Click);
            // 
            // picIllustration
            // 
            this.picIllustration.Image = global::AisInternalSystem.Properties.Resources.icons8_question_mark_480px;
            this.picIllustration.Location = new System.Drawing.Point(427, 116);
            this.picIllustration.Name = "picIllustration";
            this.picIllustration.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picIllustration.ShadowDecoration.Parent = this.picIllustration;
            this.picIllustration.Size = new System.Drawing.Size(355, 350);
            this.picIllustration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIllustration.TabIndex = 14;
            this.picIllustration.TabStop = false;
            // 
            // DialogControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.BtnNo);
            this.Controls.Add(this.BtnYes);
            this.Controls.Add(this.lbldialogSubtext);
            this.Controls.Add(this.picIllustration);
            this.Controls.Add(this.lbldialogHeader);
            this.DoubleBuffered = true;
            this.Name = "DialogControl";
            this.Size = new System.Drawing.Size(1280, 611);
            ((System.ComponentModel.ISupportInitialize)(this.picIllustration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2CirclePictureBox picIllustration;
        private Guna.UI2.WinForms.Guna2Button BtnYes;
        private Guna.UI2.WinForms.Guna2Button BtnNo;
        public System.Windows.Forms.Label lbldialogHeader;
        public System.Windows.Forms.Label lbldialogSubtext;
    }
}
