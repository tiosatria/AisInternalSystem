﻿using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AisInternalSystem.Module;

namespace AisInternalSystem
{
    public partial class TestingState : Form
    {
        public TestingState()
        {
            InitializeComponent();
        }

        private void TestingState_Load(object sender, EventArgs e)
        {
            Db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("getstudstat", Db.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.ReportPath = @"C:\Users\it\source\repos\AisInternalSystem\AisInternalSystem\StudSummary.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private void openMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UCMaster master = new UCMaster();
            this.Controls.Add(master);
            this.Controls[Controls.IndexOf(master)].BringToFront();
            master.Dock = DockStyle.Fill;
        }
    }
}
