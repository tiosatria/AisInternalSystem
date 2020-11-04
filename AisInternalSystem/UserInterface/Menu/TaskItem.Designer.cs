namespace AisInternalSystem.UserInterface.Menu
{
    partial class TaskItem
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
            this.lbltitle = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblsubtitle = new System.Windows.Forms.Label();
            this.btnMarkAsDone = new Guna.UI2.WinForms.Guna2Button();
            this.lblstart = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.BackColor = System.Drawing.Color.Transparent;
            this.lbltitle.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.Location = new System.Drawing.Point(1, 3);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(71, 12);
            this.lbltitle.TabIndex = 8;
            this.lbltitle.Text = "Working With";
            this.lbltitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbltitle_MouseClick);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // lblsubtitle
            // 
            this.lblsubtitle.AutoSize = true;
            this.lblsubtitle.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblsubtitle.Location = new System.Drawing.Point(1, 15);
            this.lblsubtitle.Name = "lblsubtitle";
            this.lblsubtitle.Size = new System.Drawing.Size(65, 12);
            this.lblsubtitle.TabIndex = 9;
            this.lblsubtitle.Text = "Working With";
            this.lblsubtitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblsubtitle_MouseClick);
            // 
            // btnMarkAsDone
            // 
            this.btnMarkAsDone.Animated = true;
            this.btnMarkAsDone.AutoRoundedCorners = true;
            this.btnMarkAsDone.BorderRadius = 10;
            this.btnMarkAsDone.CheckedState.Parent = this.btnMarkAsDone;
            this.btnMarkAsDone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarkAsDone.CustomImages.Parent = this.btnMarkAsDone;
            this.btnMarkAsDone.FillColor = System.Drawing.Color.Black;
            this.btnMarkAsDone.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarkAsDone.ForeColor = System.Drawing.Color.Black;
            this.btnMarkAsDone.HoverState.FillColor = System.Drawing.Color.LightCoral;
            this.btnMarkAsDone.HoverState.Parent = this.btnMarkAsDone;
            this.btnMarkAsDone.Image = global::AisInternalSystem.Properties.Resources.icons8_Check_Mark_48px_1;
            this.btnMarkAsDone.Location = new System.Drawing.Point(99, 27);
            this.btnMarkAsDone.Name = "btnMarkAsDone";
            this.btnMarkAsDone.ShadowDecoration.Parent = this.btnMarkAsDone;
            this.btnMarkAsDone.Size = new System.Drawing.Size(25, 23);
            this.btnMarkAsDone.TabIndex = 10;
            this.btnMarkAsDone.Click += new System.EventHandler(this.btnMarkAsDone_Click);
            // 
            // lblstart
            // 
            this.lblstart.AutoSize = true;
            this.lblstart.BackColor = System.Drawing.Color.Transparent;
            this.lblstart.Font = new System.Drawing.Font("Segoe UI Semibold", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstart.Location = new System.Drawing.Point(3, 36);
            this.lblstart.Name = "lblstart";
            this.lblstart.Size = new System.Drawing.Size(38, 12);
            this.lblstart.TabIndex = 11;
            this.lblstart.Text = "Started: ";
            this.lblstart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseClick);
            // 
            // TaskItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.lblstart);
            this.Controls.Add(this.btnMarkAsDone);
            this.Controls.Add(this.lblsubtitle);
            this.Controls.Add(this.lbltitle);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TaskItem";
            this.Size = new System.Drawing.Size(127, 54);
            this.Load += new System.EventHandler(this.TaskItem_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TaskItem_MouseClick);
            this.MouseEnter += new System.EventHandler(this.TaskItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.TaskItem_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbltitle;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label lblsubtitle;
        private System.Windows.Forms.Label lblstart;
        private Guna.UI2.WinForms.Guna2Button btnMarkAsDone;
    }
}
