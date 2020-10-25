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
        Db db = new Db();
        public TestingState()
        {
            InitializeComponent();
        }

        private void TestingState_Load(object sender, EventArgs e)
        {
            db.open_connection();
            MySqlCommand cmd = new MySqlCommand("getstudstat", db.get_connection());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ReportDataSource dataSource = new ReportDataSource("GetStudStat", dt.);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }
    }
}