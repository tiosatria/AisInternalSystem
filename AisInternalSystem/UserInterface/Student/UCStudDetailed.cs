using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AisInternalSystem.Module;
using Guna.UI2.WinForms;
using System.ComponentModel.Design;
using Microsoft.VisualBasic;
using AisInternalSystem.Properties;
using System.Runtime.InteropServices;
using AisInternalSystem.Controller;

namespace AisInternalSystem
{
    public partial class UCStudDetailed : UserControl
    {

        public void FillRelationshipPanelData()
        {

        }

        private bool Isloaded = false;

        public UCStudDetailed()
        {

        }
        
        private void InitObject()
        {
            if (!Isloaded)
            {
                InitializeComponent();
            }
        }
    }
}
