
namespace AisInternalSystem.UserInterface.Notification
{
    partial class UCNotification
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.picBtnExit = new System.Windows.Forms.PictureBox();
            this.PanelNoNotif = new System.Windows.Forms.Panel();
            this.PicNoNotif = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnExit)).BeginInit();
            this.PanelNoNotif.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicNoNotif)).BeginInit();
            this.SuspendLayout();
            // 
            // Elipser
            // 
            this.Elipser.TargetControl = this;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 39);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(7);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(405, 279);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Notification";
            // 
            // picBtnExit
            // 
            this.picBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtnExit.Image = global::AisInternalSystem.Properties.Resources.icons8_shutdown_60px_1;
            this.picBtnExit.Location = new System.Drawing.Point(391, 4);
            this.picBtnExit.Name = "picBtnExit";
            this.picBtnExit.Size = new System.Drawing.Size(29, 29);
            this.picBtnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBtnExit.TabIndex = 11;
            this.picBtnExit.TabStop = false;
            // 
            // PanelNoNotif
            // 
            this.PanelNoNotif.Controls.Add(this.PicNoNotif);
            this.PanelNoNotif.Location = new System.Drawing.Point(13, 32);
            this.PanelNoNotif.Name = "PanelNoNotif";
            this.PanelNoNotif.Size = new System.Drawing.Size(407, 286);
            this.PanelNoNotif.TabIndex = 12;
            // 
            // PicNoNotif
            // 
            this.PicNoNotif.Location = new System.Drawing.Point(75, 26);
            this.PicNoNotif.Name = "PicNoNotif";
            this.PicNoNotif.Size = new System.Drawing.Size(253, 221);
            this.PicNoNotif.TabIndex = 0;
            this.PicNoNotif.TabStop = false;
            // 
            // UCNotification
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.PanelNoNotif);
            this.Controls.Add(this.picBtnExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UCNotification";
            this.Size = new System.Drawing.Size(431, 331);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnExit)).EndInit();
            this.PanelNoNotif.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicNoNotif)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse Elipser;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBtnExit;
        private System.Windows.Forms.Panel PanelNoNotif;
        private System.Windows.Forms.PictureBox PicNoNotif;
    }
}
