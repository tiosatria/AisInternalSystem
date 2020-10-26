namespace AisInternalSystem
{
    partial class UIMember_AcademicYear
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
            this.lbl_ay_code = new System.Windows.Forms.Label();
            this.lbl_ay = new System.Windows.Forms.Label();
            this.lbl_ay_terms = new System.Windows.Forms.Label();
            this.lbl_ay_status = new System.Windows.Forms.Label();
            this.guna2ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.lbl_ay_status);
            this.guna2ShadowPanel1.Controls.Add(this.lbl_ay_terms);
            this.guna2ShadowPanel1.Controls.Add(this.lbl_ay_code);
            this.guna2ShadowPanel1.Controls.Add(this.lbl_ay);
            this.guna2ShadowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(710, 143);
            this.guna2ShadowPanel1.TabIndex = 0;
            // 
            // lbl_ay_code
            // 
            this.lbl_ay_code.AutoSize = true;
            this.lbl_ay_code.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ay_code.Location = new System.Drawing.Point(197, 51);
            this.lbl_ay_code.Name = "lbl_ay_code";
            this.lbl_ay_code.Size = new System.Drawing.Size(309, 21);
            this.lbl_ay_code.TabIndex = 1;
            this.lbl_ay_code.Text = "Academic Year Code: AY2019-2020-TERM1";
            // 
            // lbl_ay
            // 
            this.lbl_ay.AutoSize = true;
            this.lbl_ay.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ay.Location = new System.Drawing.Point(212, 16);
            this.lbl_ay.Name = "lbl_ay";
            this.lbl_ay.Size = new System.Drawing.Size(277, 30);
            this.lbl_ay.TabIndex = 0;
            this.lbl_ay.Text = "Academic Year : 2019-2020";
            // 
            // lbl_ay_terms
            // 
            this.lbl_ay_terms.AutoSize = true;
            this.lbl_ay_terms.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ay_terms.Location = new System.Drawing.Point(314, 79);
            this.lbl_ay_terms.Name = "lbl_ay_terms";
            this.lbl_ay_terms.Size = new System.Drawing.Size(67, 21);
            this.lbl_ay_terms.TabIndex = 2;
            this.lbl_ay_terms.Text = "Terms: 4";
            // 
            // lbl_ay_status
            // 
            this.lbl_ay_status.AutoSize = true;
            this.lbl_ay_status.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ay_status.ForeColor = System.Drawing.Color.SeaGreen;
            this.lbl_ay_status.Location = new System.Drawing.Point(267, 110);
            this.lbl_ay_status.Name = "lbl_ay_status";
            this.lbl_ay_status.Size = new System.Drawing.Size(145, 25);
            this.lbl_ay_status.TabIndex = 3;
            this.lbl_ay_status.Text = "Status: Ongoing";
            // 
            // UIMember_AcademicYear
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Name = "UIMember_AcademicYear";
            this.Size = new System.Drawing.Size(710, 143);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private System.Windows.Forms.Label lbl_ay_code;
        private System.Windows.Forms.Label lbl_ay;
        private System.Windows.Forms.Label lbl_ay_status;
        private System.Windows.Forms.Label lbl_ay_terms;
    }
}
