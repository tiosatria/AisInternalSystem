using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem;
using System.Diagnostics;
using System.IO;

namespace AisInternalSystem
{
    public partial class DialogControl : UserControl
    {
        int sender;
        public DialogControl()
        {
            InitializeComponent();
        }

        public enum Handlerstate
        {
            Exit, SaveStudent, ReviseStudent, SaveEmployee, ReviseEmployee, Updatesystem
        }

        private Handlerstate state;

        public void dialodMsg(string header, string subheader, string yes, string no, Image res, Handlerstate stateFromForm, int controlindex)
        {
            sender = controlindex;
            lbldialogHeader.Text = header;
            lbldialogSubtext.Text = subheader;
            BtnYes.Text = yes;
            BtnNo.Text = no;
            state = stateFromForm;
            picIllustration.Image = res;
        }

        private void DialogControl_Load(object sender, EventArgs e)
        {
            
        }

        private void FunctionYes()
        {
            switch (state)
            {
                case Handlerstate.Exit:
                    Application.Exit();
                    break;
                case Handlerstate.SaveStudent:
                    break;
                case Handlerstate.Updatesystem:
                    Process.Start(Directory.GetCurrentDirectory() + "\\Updater.exe");
                    Application.Exit();
                    break;
            }
        }

        private void FunctionNo()
        {
            
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            FunctionYes();
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            FunctionNo();
        }
    }
}
