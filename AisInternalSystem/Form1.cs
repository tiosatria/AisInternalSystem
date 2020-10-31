using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AisInternalSystem.Module;
using System.Runtime.CompilerServices;
using Guna.UI2.AnimatorNS;
using AisInternalSystem.Properties;
using System.Runtime.Remoting.Channels;
using Microsoft.VisualBasic;
using MySqlX.XDevAPI;
using Telerik.WinControls.UI;

namespace AisInternalSystem
{
    public partial class Dashboard : Form
    {
        double appVer = 2.5, appVerDb;
        bool isExit, loaded = false;
        public static int ownerId;
        public static string ownerName, username, role, userPhotoPath;
        public bool isLoggedIn;
        public static string SelectedString;
        public static string[] DropDownListAy, Classname;
        public enum RoleState
        {
            Administration,
            Accounting,
            Management
        }
        MySqlCommand command;
        public static DataTable ClassList;
        PleaseWait waitform = new PleaseWait();
        public UCSchoolAdm UCSchoolAdm = new UCSchoolAdm();

        public Dashboard()
        {
            waitform.Show();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            isLoggedIn = false;
            UserInterface.Navigation(UserInterface.NavigationEnumeration.Login);
        }
    }
}
