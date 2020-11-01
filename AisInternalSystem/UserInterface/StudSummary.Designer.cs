namespace AisInternalSystem
{
    partial class StudSummary
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
            this.btnBackEmpDir = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.studentdataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.guna2ShadowPanel1.SuspendLayout();
            this.guna2ShadowPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentdataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBackEmpDir
            // 
            this.btnBackEmpDir.Animated = true;
            this.btnBackEmpDir.AutoRoundedCorners = true;
            this.btnBackEmpDir.BorderRadius = 14;
            this.btnBackEmpDir.CheckedState.Parent = this.btnBackEmpDir;
            this.btnBackEmpDir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackEmpDir.CustomImages.Parent = this.btnBackEmpDir;
            this.btnBackEmpDir.FillColor = System.Drawing.Color.White;
            this.btnBackEmpDir.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackEmpDir.ForeColor = System.Drawing.Color.Black;
            this.btnBackEmpDir.HoverState.FillColor = System.Drawing.Color.Coral;
            this.btnBackEmpDir.HoverState.Parent = this.btnBackEmpDir;
            this.btnBackEmpDir.Location = new System.Drawing.Point(0, 2);
            this.btnBackEmpDir.Name = "btnBackEmpDir";
            this.btnBackEmpDir.ShadowDecoration.Parent = this.btnBackEmpDir;
            this.btnBackEmpDir.Size = new System.Drawing.Size(122, 30);
            this.btnBackEmpDir.TabIndex = 81;
            this.btnBackEmpDir.Text = "Go Back";
            this.btnBackEmpDir.Click += new System.EventHandler(this.btnBackEmpDir_Click);
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.label1);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(19, 38);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(321, 225);
            this.guna2ShadowPanel1.TabIndex = 82;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "By Class";
            // 
            // guna2ShadowPanel2
            // 
            this.guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel2.Controls.Add(this.label2);
            this.guna2ShadowPanel2.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel2.Location = new System.Drawing.Point(357, 38);
            this.guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            this.guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel2.Size = new System.Drawing.Size(321, 225);
            this.guna2ShadowPanel2.TabIndex = 83;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "By Level";
            // 
            // 
            // aisdbDataSet
            // 
            // 
            // studentdataBindingSource
            // 
            // 
            // StudSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2ShadowPanel2);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Controls.Add(this.btnBackEmpDir);
            this.Name = "StudSummary";
            this.Size = new System.Drawing.Size(1280, 611);
            this.Load += new System.EventHandler(this.StudSummary_Load);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            this.guna2ShadowPanel2.ResumeLayout(false);
            this.guna2ShadowPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentdataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnBackEmpDir;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource studentdataBindingSource;
    }
}
