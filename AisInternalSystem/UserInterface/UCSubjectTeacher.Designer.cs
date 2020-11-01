namespace AisInternalSystem
{
    partial class UCSubjectTeacher
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
            this.picTeacher = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lblTeachername = new System.Windows.Forms.Label();
            this.lblSubjectTaught = new System.Windows.Forms.Label();
            this.btnRevoke = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ShadowPanel1.SuspendLayout();
            this.guna2ShadowPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTeacher)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.btnRevoke);
            this.guna2ShadowPanel1.Controls.Add(this.lblSubjectTaught);
            this.guna2ShadowPanel1.Controls.Add(this.lblTeachername);
            this.guna2ShadowPanel1.Controls.Add(this.guna2ShadowPanel2);
            this.guna2ShadowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(361, 89);
            this.guna2ShadowPanel1.TabIndex = 0;
            this.guna2ShadowPanel1.MouseEnter += new System.EventHandler(this.guna2ShadowPanel1_MouseEnter);
            // 
            // guna2ShadowPanel2
            // 
            this.guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel2.Controls.Add(this.picTeacher);
            this.guna2ShadowPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2ShadowPanel2.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel2.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            this.guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel2.Size = new System.Drawing.Size(107, 89);
            this.guna2ShadowPanel2.TabIndex = 1;
            // 
            // picTeacher
            // 
            this.picTeacher.Location = new System.Drawing.Point(9, 11);
            this.picTeacher.Name = "picTeacher";
            this.picTeacher.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picTeacher.ShadowDecoration.Parent = this.picTeacher;
            this.picTeacher.Size = new System.Drawing.Size(91, 69);
            this.picTeacher.TabIndex = 0;
            this.picTeacher.TabStop = false;
            // 
            // lblTeachername
            // 
            this.lblTeachername.AutoSize = true;
            this.lblTeachername.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeachername.Location = new System.Drawing.Point(113, 12);
            this.lblTeachername.Name = "lblTeachername";
            this.lblTeachername.Size = new System.Drawing.Size(103, 16);
            this.lblTeachername.TabIndex = 2;
            this.lblTeachername.Text = "Teacher Name";
            // 
            // lblSubjectTaught
            // 
            this.lblSubjectTaught.AutoSize = true;
            this.lblSubjectTaught.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectTaught.Location = new System.Drawing.Point(114, 31);
            this.lblSubjectTaught.Name = "lblSubjectTaught";
            this.lblSubjectTaught.Size = new System.Drawing.Size(89, 17);
            this.lblSubjectTaught.TabIndex = 3;
            this.lblSubjectTaught.Text = "Is also taught:";
            // 
            // btnRevoke
            // 
            this.btnRevoke.Animated = true;
            this.btnRevoke.CheckedState.Parent = this.btnRevoke;
            this.btnRevoke.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRevoke.CustomImages.Parent = this.btnRevoke;
            this.btnRevoke.FillColor = System.Drawing.Color.Silver;
            this.btnRevoke.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevoke.ForeColor = System.Drawing.Color.Black;
            this.btnRevoke.HoverState.FillColor = System.Drawing.Color.Coral;
            this.btnRevoke.HoverState.Parent = this.btnRevoke;
            this.btnRevoke.Location = new System.Drawing.Point(298, 22);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.ShadowDecoration.Parent = this.btnRevoke;
            this.btnRevoke.Size = new System.Drawing.Size(51, 42);
            this.btnRevoke.TabIndex = 121;
            this.btnRevoke.Text = "Revoke";
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // 
            // UCSubjectTeacher
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Name = "UCSubjectTeacher";
            this.Size = new System.Drawing.Size(361, 89);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            this.guna2ShadowPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTeacher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private System.Windows.Forms.Label lblSubjectTaught;
        private System.Windows.Forms.Label lblTeachername;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
        private Guna.UI2.WinForms.Guna2CirclePictureBox picTeacher;
        private Guna.UI2.WinForms.Guna2Button btnRevoke;
    }
}
