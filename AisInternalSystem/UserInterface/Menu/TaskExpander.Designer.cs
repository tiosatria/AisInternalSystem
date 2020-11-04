﻿namespace AisInternalSystem.UserInterface.Menu
{
    partial class TaskExpander
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
            this.Elipser = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.BtnLogin = new Guna.UI2.WinForms.Guna2Button();
            this.flowTasks = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTaskcount = new System.Windows.Forms.Label();
            this.guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // Elipser
            // 
            this.Elipser.TargetControl = this;
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.btnClose);
            this.guna2ShadowPanel1.Controls.Add(this.BtnLogin);
            this.guna2ShadowPanel1.Controls.Add(this.flowTasks);
            this.guna2ShadowPanel1.Controls.Add(this.lblTaskcount);
            this.guna2ShadowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.WhiteSmoke;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(138, 254);
            this.guna2ShadowPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::AisInternalSystem.Properties.Resources.icons8_close_window_48px;
            this.btnClose.Location = new System.Drawing.Point(119, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(14, 14);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 7;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Animated = true;
            this.BtnLogin.AutoRoundedCorners = true;
            this.BtnLogin.BorderRadius = 10;
            this.BtnLogin.CheckedState.Parent = this.BtnLogin;
            this.BtnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLogin.CustomImages.Parent = this.BtnLogin;
            this.BtnLogin.FillColor = System.Drawing.Color.Silver;
            this.BtnLogin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLogin.ForeColor = System.Drawing.Color.Black;
            this.BtnLogin.HoverState.FillColor = System.Drawing.Color.LightCoral;
            this.BtnLogin.HoverState.Parent = this.BtnLogin;
            this.BtnLogin.Location = new System.Drawing.Point(18, 221);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.ShadowDecoration.Parent = this.BtnLogin;
            this.BtnLogin.Size = new System.Drawing.Size(100, 22);
            this.BtnLogin.TabIndex = 6;
            this.BtnLogin.Text = "Mark all as done";
            // 
            // flowTasks
            // 
            this.flowTasks.AutoScroll = true;
            this.flowTasks.Location = new System.Drawing.Point(4, 36);
            this.flowTasks.Name = "flowTasks";
            this.flowTasks.Size = new System.Drawing.Size(132, 178);
            this.flowTasks.TabIndex = 2;
            // 
            // lblTaskcount
            // 
            this.lblTaskcount.AutoSize = true;
            this.lblTaskcount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskcount.Location = new System.Drawing.Point(11, 10);
            this.lblTaskcount.Name = "lblTaskcount";
            this.lblTaskcount.Size = new System.Drawing.Size(73, 13);
            this.lblTaskcount.TabIndex = 1;
            this.lblTaskcount.Text = "lblTaskCount";
            // 
            // TaskExpander
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TaskExpander";
            this.Size = new System.Drawing.Size(138, 254);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse Elipser;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private System.Windows.Forms.Label lblTaskcount;
        private System.Windows.Forms.FlowLayoutPanel flowTasks;
        private Guna.UI2.WinForms.Guna2Button BtnLogin;
        private System.Windows.Forms.PictureBox btnClose;
    }
}
