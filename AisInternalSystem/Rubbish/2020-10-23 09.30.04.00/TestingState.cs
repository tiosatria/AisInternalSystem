using Microsoft.Reporting.WinForms;
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
            this.reportViewer1.RefreshReport();
        }
    }
}
