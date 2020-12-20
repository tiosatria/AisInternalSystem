
namespace AisInternalSystem.UserInterface.Notification
{
    partial class UCNotificationItem
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
            this.elipser = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.picBtnExit = new System.Windows.Forms.PictureBox();
            this.lbltitle = new System.Windows.Forms.Label();
            this.lblsubtitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnExit)).BeginInit();
            this.SuspendLayout();
            // 
            // elipser
            // 
            this.elipser.TargetControl = this;
            // 
            // picBtnExit
            // 
            this.picBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnExit.Image = global::AisInternalSystem.Properties.Resources.icons8_shutdown_60px_1;
            this.picBtnExit.Location = new System.Drawing.Point(9, 25);
            this.picBtnExit.Name = "picBtnExit";
            this.picBtnExit.Size = new System.Drawing.Size(39, 40);
            this.picBtnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnExit.TabIndex = 11;
            this.picBtnExit.TabStop = false;
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.Location = new System.Drawing.Point(57, 8);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(140, 20);
            this.lbltitle.TabIndex = 12;
            this.lbltitle.Text = "NotificationHeader";
            // 
            // lblsubtitle
            // 
            this.lblsubtitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsubtitle.Location = new System.Drawing.Point(58, 28);
            this.lblsubtitle.Name = "lblsubtitle";
            this.lblsubtitle.Size = new System.Drawing.Size(246, 48);
            this.lblsubtitle.TabIndex = 13;
            this.lblsubtitle.Text = "NotificationHeader";
            // 
            // UCNotificationItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.lblsubtitle);
            this.Controls.Add(this.lbltitle);
            this.Controls.Add(this.picBtnExit);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UCNotificationItem";
            this.Size = new System.Drawing.Size(356, 95);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse elipser;
        private System.Windows.Forms.PictureBox picBtnExit;
        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.Label lblsubtitle;
    }
}
