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
        }

        private void openMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UCMaster master = new UCMaster();
            this.Controls.Add(master);
            this.Controls[Controls.IndexOf(master)].BringToFront();
            master.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
            {
                MessageBox.Show(i.ToString());
            }
        }
    }
}
