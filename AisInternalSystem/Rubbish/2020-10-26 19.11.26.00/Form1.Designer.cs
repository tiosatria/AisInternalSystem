namespace AisInternalSystem
{
    partial class Dashboard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Guna.UI2.AnimatorNS.Animation animation1 = new Guna.UI2.AnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.DragControl = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panel_Upper_admin = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.BtnDashboardAdmin = new Guna.UI2.WinForms.Guna2Button();
            this.BtnInventoryAdmin = new Guna.UI2.WinForms.Guna2Button();
            this.BtnEmployeeAdmin = new Guna.UI2.WinForms.Guna2Button();
            this.BtnSchoolAdmin = new Guna.UI2.WinForms.Guna2Button();
            this.PanelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.loginFrm1 = new AisInternalSystem.LoginFrm();
            this.ClassTransition = new Guna.UI2.WinForms.Guna2Transition();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnMsg = new System.Windows.Forms.PictureBox();
            this.picBtnExit = new System.Windows.Forms.PictureBox();
            this.Btn_Notif = new System.Windows.Forms.PictureBox();
            this.btnFeedback = new System.Windows.Forms.PictureBox();
            this.picThumbUser = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.panel_Upper_admin.SuspendLayout();
            this.PanelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Notif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFeedback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbUser)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            this.guna2Elipse1.TargetControl = this;
            // 
            // DragControl
            // 
            this.DragControl.TargetControl = this.panel_Upper_admin;
            // 
            // panel_Upper_admin
            // 
            this.panel_Upper_admin.BackColor = System.Drawing.Color.Transparent;
            this.panel_Upper_admin.Controls.Add(this.guna2PictureBox1);
            this.panel_Upper_admin.Controls.Add(this.btnMsg);
            this.panel_Upper_admin.Controls.Add(this.picBtnExit);
            this.panel_Upper_admin.Controls.Add(this.Btn_Notif);
            this.panel_Upper_admin.Controls.Add(this.btnFeedback);
            this.panel_Upper_admin.Controls.Add(this.picThumbUser);
            this.panel_Upper_admin.Controls.Add(this.BtnDashboardAdmin);
            this.panel_Upper_admin.Controls.Add(this.BtnInventoryAdmin);
            this.panel_Upper_admin.Controls.Add(this.BtnEmployeeAdmin);
            this.panel_Upper_admin.Controls.Add(this.BtnSchoolAdmin);
            this.ClassTransition.SetDecoration(this.panel_Upper_admin, Guna.UI2.AnimatorNS.DecorationType.None);
            this.panel_Upper_admin.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Upper_admin.FillColor = System.Drawing.Color.White;
            this.panel_Upper_admin.Location = new System.Drawing.Point(0, 0);
            this.panel_Upper_admin.Name = "panel_Upper_admin";
            this.panel_Upper_admin.ShadowColor = System.Drawing.Color.Black;
            this.panel_Upper_admin.Size = new System.Drawing.Size(1280, 109);
            this.panel_Upper_admin.TabIndex = 1;
            this.panel_Upper_admin.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Upper_admin_Paint);
            this.panel_Upper_admin.MouseEnter += new System.EventHandler(this.panel_Upper_admin_MouseEnter);
            // 
            // BtnDashboardAdmin
            // 
            this.BtnDashboardAdmin.Animated = true;
            this.BtnDashboardAdmin.AutoRoundedCorners = true;
            this.BtnDashboardAdmin.BorderColor = System.Drawing.Color.Transparent;
            this.BtnDashboardAdmin.BorderRadius = 27;
            this.BtnDashboardAdmin.CheckedState.Parent = this.BtnDashboardAdmin;
            this.BtnDashboardAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDashboardAdmin.CustomImages.Parent = this.BtnDashboardAdmin;
            this.ClassTransition.SetDecoration(this.BtnDashboardAdmin, Guna.UI2.AnimatorNS.DecorationType.None);
            this.BtnDashboardAdmin.FillColor = System.Drawing.Color.Transparent;
            this.BtnDashboardAdmin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDashboardAdmin.ForeColor = System.Drawing.Color.Black;
            this.BtnDashboardAdmin.HoverState.Parent = this.BtnDashboardAdmin;
            this.BtnDashboardAdmin.Location = new System.Drawing.Point(682, 22);
            this.BtnDashboardAdmin.Name = "BtnDashboardAdmin";
            this.BtnDashboardAdmin.ShadowDecoration.Parent = this.BtnDashboardAdmin;
            this.BtnDashboardAdmin.Size = new System.Drawing.Size(142, 57);
            this.BtnDashboardAdmin.TabIndex = 6;
            this.BtnDashboardAdmin.Text = "Dashboard";
            this.BtnDashboardAdmin.Click += new System.EventHandler(this.BtnDashboardAdmin_Click);
            // 
            // BtnInventoryAdmin
            // 
            this.BtnInventoryAdmin.Animated = true;
            this.BtnInventoryAdmin.AutoRoundedCorners = true;
            this.BtnInventoryAdmin.BorderRadius = 27;
            this.BtnInventoryAdmin.CheckedState.Parent = this.BtnInventoryAdmin;
            this.BtnInventoryAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnInventoryAdmin.CustomImages.Parent = this.BtnInventoryAdmin;
            this.ClassTransition.SetDecoration(this.BtnInventoryAdmin, Guna.UI2.AnimatorNS.DecorationType.None);
            this.BtnInventoryAdmin.FillColor = System.Drawing.Color.Transparent;
            this.BtnInventoryAdmin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnInventoryAdmin.ForeColor = System.Drawing.Color.Black;
            this.BtnInventoryAdmin.HoverState.Parent = this.BtnInventoryAdmin;
            this.BtnInventoryAdmin.Location = new System.Drawing.Point(830, 22);
            this.BtnInventoryAdmin.Name = "BtnInventoryAdmin";
            this.BtnInventoryAdmin.ShadowDecoration.Parent = this.BtnInventoryAdmin;
            this.BtnInventoryAdmin.Size = new System.Drawing.Size(142, 57);
            this.BtnInventoryAdmin.TabIndex = 5;
            this.BtnInventoryAdmin.Text = "Inventory";
            this.BtnInventoryAdmin.Click += new System.EventHandler(this.BtnInventoryAdmin_Click);
            // 
            // BtnEmployeeAdmin
            // 
            this.BtnEmployeeAdmin.Animated = true;
            this.BtnEmployeeAdmin.AutoRoundedCorners = true;
            this.BtnEmployeeAdmin.BorderRadius = 27;
            this.BtnEmployeeAdmin.CheckedState.Parent = this.BtnEmployeeAdmin;
            this.BtnEmployeeAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnEmployeeAdmin.CustomImages.Parent = this.BtnEmployeeAdmin;
            this.ClassTransition.SetDecoration(this.BtnEmployeeAdmin, Guna.UI2.AnimatorNS.DecorationType.None);
            this.BtnEmployeeAdmin.FillColor = System.Drawing.Color.Transparent;
            this.BtnEmployeeAdmin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEmployeeAdmin.ForeColor = System.Drawing.Color.Black;
            this.BtnEmployeeAdmin.HoverState.Parent = this.BtnEmployeeAdmin;
            this.BtnEmployeeAdmin.Location = new System.Drawing.Point(978, 22);
            this.BtnEmployeeAdmin.Name = "BtnEmployeeAdmin";
            this.BtnEmployeeAdmin.ShadowDecoration.Parent = this.BtnEmployeeAdmin;
            this.BtnEmployeeAdmin.Size = new System.Drawing.Size(142, 57);
            this.BtnEmployeeAdmin.TabIndex = 4;
            this.BtnEmployeeAdmin.Text = "Employee";
            this.BtnEmployeeAdmin.Click += new System.EventHandler(this.BtnEmployeeAdmin_Click);
            // 
            // BtnSchoolAdmin
            // 
            this.BtnSchoolAdmin.AutoRoundedCorners = true;
            this.BtnSchoolAdmin.BorderRadius = 27;
            this.BtnSchoolAdmin.CheckedState.Parent = this.BtnSchoolAdmin;
            this.BtnSchoolAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSchoolAdmin.CustomImages.Parent = this.BtnSchoolAdmin;
            this.ClassTransition.SetDecoration(this.BtnSchoolAdmin, Guna.UI2.AnimatorNS.DecorationType.None);
            this.BtnSchoolAdmin.FillColor = System.Drawing.Color.Transparent;
            this.BtnSchoolAdmin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSchoolAdmin.ForeColor = System.Drawing.Color.Black;
            this.BtnSchoolAdmin.HoverState.Parent = this.BtnSchoolAdmin;
            this.BtnSchoolAdmin.Location = new System.Drawing.Point(1126, 22);
            this.BtnSchoolAdmin.Name = "BtnSchoolAdmin";
            this.BtnSchoolAdmin.ShadowDecoration.Parent = this.BtnSchoolAdmin;
            this.BtnSchoolAdmin.Size = new System.Drawing.Size(142, 57);
            this.BtnSchoolAdmin.TabIndex = 3;
            this.BtnSchoolAdmin.Text = "School Administration";
            this.BtnSchoolAdmin.Click += new System.EventHandler(this.BtnSchoolAdmin_Click);
            // 
            // PanelContainer
            // 
            this.PanelContainer.Controls.Add(this.loginFrm1);
            this.ClassTransition.SetDecoration(this.PanelContainer, Guna.UI2.AnimatorNS.DecorationType.None);
            this.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContainer.Location = new System.Drawing.Point(0, 109);
            this.PanelContainer.Name = "PanelContainer";
            this.PanelContainer.ShadowDecoration.Parent = this.PanelContainer;
            this.PanelContainer.Size = new System.Drawing.Size(1280, 611);
            this.PanelContainer.TabIndex = 2;
            this.PanelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelContainer_Paint);
            // 
            // loginFrm1
            // 
            this.loginFrm1.BackColor = System.Drawing.Color.White;
            this.ClassTransition.SetDecoration(this.loginFrm1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.loginFrm1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginFrm1.Location = new System.Drawing.Point(0, 0);
            this.loginFrm1.Name = "loginFrm1";
            this.loginFrm1.Size = new System.Drawing.Size(1280, 611);
            this.loginFrm1.TabIndex = 0;
            this.loginFrm1.Load += new System.EventHandler(this.loginFrm1_Load);
            // 
            // ClassTransition
            // 
            this.ClassTransition.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Scale;
            this.ClassTransition.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            this.ClassTransition.DefaultAnimation = animation1;
            this.ClassTransition.MaxAnimationTime = 50000;
            this.ClassTransition.TimeStep = 0.01F;
            // 
            // guna2PictureBox1
            // 
            this.ClassTransition.SetDecoration(this.guna2PictureBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2PictureBox1.Image = global::AisInternalSystem.Properties.Resources.AIS;
            this.guna2PictureBox1.Location = new System.Drawing.Point(14, 22);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.ShadowDecoration.Parent = this.guna2PictureBox1;
            this.guna2PictureBox1.Size = new System.Drawing.Size(170, 57);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            // 
            // btnMsg
            // 
            this.btnMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClassTransition.SetDecoration(this.btnMsg, Guna.UI2.AnimatorNS.DecorationType.None);
            this.btnMsg.Image = global::AisInternalSystem.Properties.Resources.icons8_envelope_60px_1;
            this.btnMsg.Location = new System.Drawing.Point(284, 32);
            this.btnMsg.Name = "btnMsg";
            this.btnMsg.Size = new System.Drawing.Size(39, 40);
            this.btnMsg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMsg.TabIndex = 11;
            this.btnMsg.TabStop = false;
            this.btnMsg.Click += new System.EventHandler(this.btnMsg_Click);
            this.btnMsg.MouseEnter += new System.EventHandler(this.btnMsg_MouseEnter);
            this.btnMsg.MouseLeave += new System.EventHandler(this.btnMsg_MouseLeave);
            // 
            // picBtnExit
            // 
            this.picBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClassTransition.SetDecoration(this.picBtnExit, Guna.UI2.AnimatorNS.DecorationType.None);
            this.picBtnExit.Image = global::AisInternalSystem.Properties.Resources.icons8_shutdown_60px_1;
            this.picBtnExit.Location = new System.Drawing.Point(342, 32);
            this.picBtnExit.Name = "picBtnExit";
            this.picBtnExit.Size = new System.Drawing.Size(39, 40);
            this.picBtnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnExit.TabIndex = 10;
            this.picBtnExit.TabStop = false;
            this.picBtnExit.Click += new System.EventHandler(this.picBtnExit_Click);
            this.picBtnExit.MouseEnter += new System.EventHandler(this.picBtnExit_MouseEnter);
            this.picBtnExit.MouseLeave += new System.EventHandler(this.picBtnExit_MouseLeave);
            // 
            // Btn_Notif
            // 
            this.Btn_Notif.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClassTransition.SetDecoration(this.Btn_Notif, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Btn_Notif.Image = global::AisInternalSystem.Properties.Resources.notif_freeze;
            this.Btn_Notif.Location = new System.Drawing.Point(400, 32);
            this.Btn_Notif.Name = "Btn_Notif";
            this.Btn_Notif.Size = new System.Drawing.Size(39, 40);
            this.Btn_Notif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Btn_Notif.TabIndex = 9;
            this.Btn_Notif.TabStop = false;
            this.Btn_Notif.Click += new System.EventHandler(this.Btn_Notif_Click);
            this.Btn_Notif.MouseEnter += new System.EventHandler(this.Btn_Notif_MouseEnter);
            this.Btn_Notif.MouseLeave += new System.EventHandler(this.Btn_Notif_MouseLeave);
            // 
            // btnFeedback
            // 
            this.btnFeedback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClassTransition.SetDecoration(this.btnFeedback, Guna.UI2.AnimatorNS.DecorationType.None);
            this.btnFeedback.Image = global::AisInternalSystem.Properties.Resources.FeedBack;
            this.btnFeedback.Location = new System.Drawing.Point(457, 32);
            this.btnFeedback.Name = "btnFeedback";
            this.btnFeedback.Size = new System.Drawing.Size(39, 40);
            this.btnFeedback.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnFeedback.TabIndex = 14;
            this.btnFeedback.TabStop = false;
            this.btnFeedback.Click += new System.EventHandler(this.btnFeedback_Click_1);
            this.btnFeedback.MouseEnter += new System.EventHandler(this.btnFeedback_MouseEnter_1);
            this.btnFeedback.MouseLeave += new System.EventHandler(this.btnFeedback_MouseLeave_1);
            // 
            // picThumbUser
            // 
            this.picThumbUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picThumbUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClassTransition.SetDecoration(this.picThumbUser, Guna.UI2.AnimatorNS.DecorationType.None);
            this.picThumbUser.Image = global::AisInternalSystem.Properties.Resources.icons8_male_user_100;
            this.picThumbUser.Location = new System.Drawing.Point(582, 12);
            this.picThumbUser.Name = "picThumbUser";
            this.picThumbUser.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picThumbUser.ShadowDecoration.Parent = this.picThumbUser;
            this.picThumbUser.Size = new System.Drawing.Size(76, 75);
            this.picThumbUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picThumbUser.TabIndex = 7;
            this.picThumbUser.TabStop = false;
            this.picThumbUser.Click += new System.EventHandler(this.picThumbUser_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.PanelContainer);
            this.Controls.Add(this.panel_Upper_admin);
            this.ClassTransition.SetDecoration(this, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dashboard_KeyDown);
            this.panel_Upper_admin.ResumeLayout(false);
            this.PanelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Btn_Notif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFeedback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DragControl DragControl;
        private Guna.UI2.WinForms.Guna2ShadowPanel panel_Upper_admin;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2Button BtnSchoolAdmin;
        private Guna.UI2.WinForms.Guna2Button BtnInventoryAdmin;
        private Guna.UI2.WinForms.Guna2Button BtnEmployeeAdmin;
        private Guna.UI2.WinForms.Guna2Button BtnDashboardAdmin;
        private LoginFrm loginFrm1;
        public Guna.UI2.WinForms.Guna2Panel PanelContainer;
        private Guna.UI2.WinForms.Guna2Transition ClassTransition;
        public Guna.UI2.WinForms.Guna2CirclePictureBox picThumbUser;
        private System.Windows.Forms.PictureBox Btn_Notif;
        private System.Windows.Forms.PictureBox picBtnExit;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private System.Windows.Forms.PictureBox btnMsg;
        private System.Windows.Forms.PictureBox btnFeedback;
    }
}

