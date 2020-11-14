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
using Telerik.Charting;

namespace AisInternalSystem.Controller
{
    public class Utilities
    {
        private static User.RoleIdentifier _role;
        public Utilities()
        {

        }

        public static DateTime GetDateTimeNow()
        {
            DateTime date = DateTime.Now;
            return date;
        }

        public static string GetTimeStamp()
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return str;
        }

        public static int GetCurrentUserID()
        {
            return Data.user.OwnerID;
        }

        public static string OpenFile()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = ("Image Files(*.BMP; *.JPG; *.JPEG; *.PNG;)| *.BMP; *.JPG; *.JPEG; *.PNG; | All files(*.*) | *.*");
            if (opf.ShowDialog() == DialogResult.OK)
            {

            }
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
            switch (str)
            {
                case "Administration":
                    _role = User.RoleIdentifier.Admin;
                    Data.user._role = User.RoleIdentifier.Admin;
                    break;
                case "Teacher":
                    _role = User.RoleIdentifier.Teacher;
                    Data.user._role = User.RoleIdentifier.Teacher;
                    break;
                case "Management":
                    _role = User.RoleIdentifier.Management;
                    Data.user._role = User.RoleIdentifier.Management;
                    break;
                case "IT":
                    _role = User.RoleIdentifier.IT;
                    Data.user._role = User.RoleIdentifier.IT;
                    break;
                case "Accounting":
                    _role = User.RoleIdentifier.Accounting;
                    Data.user._role = User.RoleIdentifier.Accounting;
                    break;
            }
        }

        private static void DoAuth()
        {
            switch (_role)
            {
                case User.RoleIdentifier.Management:
                    PopUp.Alert("You're using the preview build!", frmAlert.AlertType.Info);
                    break;
                case User.RoleIdentifier.Admin:
                    UIController.NavigateUI(UIController.Controls.UpperPanelAdmin);
                    UIController.NavigateUI(UIController.Controls.UCDashboardAdmin);
                    PopUp.Alert(PopUp.MessageIntroduction(Data.user.OwnerName), frmAlert.AlertType.Info);
                    break;
                case User.RoleIdentifier.IT:

                    break;
                case User.RoleIdentifier.Teacher:
                    PopUp.Alert(PopUp.NotAuthorized, frmAlert.AlertType.Warning);
                    break;

                case User.RoleIdentifier.Accounting:
                    PopUp.Alert(PopUp.NotAuthorized, frmAlert.AlertType.Warning);
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
