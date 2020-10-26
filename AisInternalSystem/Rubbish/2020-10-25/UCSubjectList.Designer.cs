﻿namespace AisInternalSystem
{
    partial class UCSubjectList
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
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.pictureSubject = new System.Windows.Forms.PictureBox();
            this.lblSubjectName = new System.Windows.Forms.Label();
            this.lblSubjectDesc = new System.Windows.Forms.Label();
            this.guna2ShadowPanel1.SuspendLayout();
            this.guna2ShadowPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSubject)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.lblSubjectDesc);
            this.guna2ShadowPanel1.Controls.Add(this.lblSubjectName);
            this.guna2ShadowPanel1.Controls.Add(this.guna2ShadowPanel2);
            this.guna2ShadowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(385, 100);
            this.guna2ShadowPanel1.TabIndex = 0;
            // 
            // guna2ShadowPanel2
            // 
            this.guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel2.Controls.Add(this.pictureSubject);
            this.guna2ShadowPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2ShadowPanel2.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel2.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            this.guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel2.Size = new System.Drawing.Size(110, 100);
            this.guna2ShadowPanel2.TabIndex = 0;
            // 
            // pictureSubject
            // 
            this.pictureSubject.Location = new System.Drawing.Point(13, 13);
            this.pictureSubject.Name = "pictureSubject";
            this.pictureSubject.Size = new System.Drawing.Size(85, 74);
            this.pictureSubject.TabIndex = 0;
            this.pictureSubject.TabStop = false;
            // 
            // lblSubjectName
            // 
            this.lblSubjectName.AutoSize = true;
            this.lblSubjectName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectName.Location = new System.Drawing.Point(116, 13);
            this.lblSubjectName.Name = "lblSubjectName";
            this.lblSubjectName.Size = new System.Drawing.Size(99, 16);
            this.lblSubjectName.TabIndex = 1;
            this.lblSubjectName.Text = "Subject Name";
            // 
            // lblSubjectDesc
            // 
            this.lblSubjectDesc.AutoSize = true;
            this.lblSubjectDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectDesc.Location = new System.Drawing.Point(116, 38);
            this.lblSubjectDesc.Name = "lblSubjectDesc";
            this.lblSubjectDesc.Size = new System.Drawing.Size(109, 15);
            this.lblSubjectDesc.TabIndex = 2;
            this.lblSubjectDesc.Text = "Subject Description";
            // 
            // UCSubjectList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Name = "UCSubjectList";
            this.Size = new System.Drawing.Size(385, 100);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            this.guna2ShadowPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureSubject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
        private System.Windows.Forms.Label lblSubjectDesc;
        private System.Windows.Forms.Label lblSubjectName;
        private System.Windows.Forms.PictureBox pictureSubject;
    }
}
