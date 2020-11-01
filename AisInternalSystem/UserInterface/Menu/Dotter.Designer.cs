namespace AisInternalSystem.UserInterface.Menu
{
    partial class Dotter
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbltasks = new System.Windows.Forms.Label();
            this.btnShow = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AisInternalSystem.Properties.Resources.Ball_1s_200px1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lbltasks
            // 
            this.lbltasks.AutoSize = true;
            this.lbltasks.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltasks.Location = new System.Drawing.Point(38, 8);
            this.lbltasks.Name = "lbltasks";
            this.lbltasks.Size = new System.Drawing.Size(38, 13);
            this.lbltasks.TabIndex = 1;
            this.lbltasks.Text = "label1";
            // 
            // btnShow
            // 
            this.btnShow.Animated = true;
            this.btnShow.CheckedState.Parent = this.btnShow;
            this.btnShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShow.CustomImages.Parent = this.btnShow;
            this.btnShow.FillColor = System.Drawing.Color.Black;
            this.btnShow.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.HoverState.Parent = this.btnShow;
            this.btnShow.Location = new System.Drawing.Point(82, 4);
            this.btnShow.Name = "btnShow";
            this.btnShow.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnShow.ShadowDecoration.Parent = this.btnShow;
            this.btnShow.Size = new System.Drawing.Size(22, 22);
            this.btnShow.TabIndex = 2;
            this.btnShow.Text = ">";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // Dotter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.lbltasks);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Dotter";
            this.Size = new System.Drawing.Size(104, 30);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbltasks;
        private Guna.UI2.WinForms.Guna2CircleButton btnShow;
    }
}
