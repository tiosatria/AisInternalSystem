using AisInternalSystem.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;
using Telerik.WinControls.UI;
using System.Threading;

namespace AisInternalSystem.Controller
{
    public class Startup
    {
        private static double Appver = 2.8;
        public static string Apppath = string.Empty;
        private static double appver2 = 0;
        public Startup()
        {

        }
        public static void Update()
        {
            DataTable dt = Query.GetDataTable("CheckForUpdate", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
            appver2 = Convert.ToDouble(dt.Rows[0][0].ToString());
            string notes = dt.Rows[0][1].ToString();
            Apppath = dt.Rows[0][2].ToString();
            if (appver2 > Appver)
            {
                PopUp.Alert("System is outdated, please update the system!", frmAlert.AlertType.Warning);
                Confirmation.Fire(Confirmation.onConfirmEnum.Update);
            }
            else
            {
                PopUp.Alert("System is up-to-date", frmAlert.AlertType.Info);
                UIController.NavigateUI(UIController.Controls.UCLogin);
            }
        }
    }
}
