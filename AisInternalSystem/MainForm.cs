﻿using AisInternalSystem.Controller;
using AisInternalSystem.Entities;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Startup.Update();
            UIController.NavigateUI(UIController.Controls.UpperPanel);
            UIController.NavigateUI(UIController.Controls.UCLogin);
        }

        private void UIController_FinishedLoadingObject(object sender, EventArgs e)
        {

        }
    }
}