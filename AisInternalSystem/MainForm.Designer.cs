namespace AisInternalSystem
{
    partial class MainForm
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
            this.StyleRounder = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.StyleShadower = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.Waiter = new AisInternalSystem.UserInterface.Menu.UCWaiter();
            this.SuspendLayout();
            // 
            // StyleRounder
            // 
            this.StyleRounder.TargetControl = this;
            // 
            // Waiter
            // 
            this.Waiter.BackColor = System.Drawing.Color.Gainsboro;
            this.Waiter.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Waiter.Location = new System.Drawing.Point(374, 184);
            this.Waiter.Name = "Waiter";
            this.Waiter.Size = new System.Drawing.Size(457, 333);
            this.Waiter.TabIndex = 0;
            this.Waiter.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.Waiter);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse StyleRounder;
        private Guna.UI2.WinForms.Guna2ShadowForm StyleShadower;
        private UserInterface.Menu.UCWaiter Waiter;
    }
}