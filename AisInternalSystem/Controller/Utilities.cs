using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Security;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AisInternalSystem.Module;
using AisInternalSystem.Entities;
using Microsoft.ReportingServices.Interfaces;
using System.Windows;
using System.Windows.Forms;

namespace AisInternalSystem.Controller
{
    public class Utilities
    {
        private static Data.RoleIdentifier _role;
        public Utilities()
        {

        }

        public static void Auth(string usr, string pwd)
        {
            DataTable dt = new DataTable();
            dt = Query.Load(Query.Process.Auth, new string[2] { usr, pwd });
            if (dt.Rows.Count >= 1)
            {
                Data.user.UserID = Convert.ToInt32(dt.Rows[0][0].ToString());
                Data.user.usrName = dt.Rows[0][1].ToString();
                Data.user.Password = dt.Rows[0][2].ToString();
                Data.user.Roles = dt.Rows[0][3].ToString();
                Data.user.OwnerID = Convert.ToInt32(dt.Rows[0][4].ToString());
                Data.user.OwnerName = dt.Rows[0][5].ToString();
                RoleSwitcher(Data.user.Roles);
                DoAuth();
            }
            else
            {
                PopUp.Alert(PopUp.WrongPassword, frmAlert.AlertType.Warning);
            }
        }

        private static void RoleSwitcher(string str)
        {
            if (str == "Administration")
            {
                _role = Data.RoleIdentifier.Admin;
            }
        }

        private static void DoAuth()
        {
            switch (_role)
            {
                case Data.RoleIdentifier.Management:
                    break;
                case Data.RoleIdentifier.Admin:
                    UIController.NavigateUI(UIController.Controls.UpperPanelAdmin);
                    UIController.NavigateUI(UIController.Controls.UCDashboardAdmin);
                    PopUp.Alert(PopUp.MessageIntroduction(Data.user.OwnerName), frmAlert.AlertType.Info);
                    break;
                case Data.RoleIdentifier.IT:
                    break;
                case Data.RoleIdentifier.Teacher:
                    break;
                default:
                    break;
            }
        }

        public static void ClearControl(UserControl ctrl)
        {
            foreach (var item in ctrl.Controls)
            {

            }
        }

        public static string GetPublicIpAddress()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


    }
}
