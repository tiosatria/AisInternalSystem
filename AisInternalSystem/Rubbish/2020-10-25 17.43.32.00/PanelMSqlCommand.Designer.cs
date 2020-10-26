namespace AisInternalSystem
{
    partial class PanelMSqlCommand
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
            this.ImgPic = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblsubtitle = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ImgPic)).BeginInit();
            this.guna2ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImgPic
            // 
            this.ImgPic.Location = new System.Drawing.Point(18, 20);
            this.ImgPic.Name = "ImgPic";
            this.ImgPic.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.ImgPic.ShadowDecoration.Parent = this.ImgPic;
            this.ImgPic.Size = new System.Drawing.Size(93, 82);
            this.ImgPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgPic.TabIndex = 0;
            this.ImgPic.TabStop = false;
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.ImgPic);
            this.guna2ShadowPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.Silver;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 10;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(131, 124);
            this.guna2ShadowPanel1.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(147, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(39, 21);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Title";
            // 
            // lblsubtitle
            // 
            this.lblsubtitle.AutoSize = true;
            this.lblsubtitle.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsubtitle.Location = new System.Drawing.Point(148, 55);
            this.lblsubtitle.Name = "lblsubtitle";
            this.lblsubtitle.Size = new System.Drawing.Size(49, 17);
            this.lblsubtitle.TabIndex = 3;
            this.lblsubtitle.Text = "Subtitle";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            this.guna2Elipse1.TargetControl = this;
            // 
            // PanelMSqlCommand
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.lblsubtitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "PanelMSqlCommand";
            this.Size = new System.Drawing.Size(386, 124);
            this.Click += new System.EventHandler(this.PanelMSqlCommand_Click);
            this.MouseEnter += new System.EventHandler(this.PanelMSqlCommand_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.PanelMSqlCommand_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.ImgPic)).EndInit();
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox ImgPic;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblsubtitle;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
