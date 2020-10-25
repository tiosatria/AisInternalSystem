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
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Microsoft.VisualBasic;

namespace AisInternalSystem
{
    public partial class LoginFrm : UserControl
    {
        Dialog dialog = new Dialog();

        public LoginFrm()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Execute_Login();
        }

        private void DataPassing()
        {
            Dashboard.ownerId = 0;
        }

        private void RoleSwitcher()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("");
            }
            catch (MySqlException ex)
            {
                dialog.Alert(ex.Message, frmAlert.AlertType.Error);
            }

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

        public void Execute_Login()
        {
            string username, password;
            try
            {
                Db.open_connection();
                username = txt_username.Text;
                password = txt_password.Text;
                MySqlCommand cmd = new MySqlCommand("select * from user_account where user=@user and password = @password", Db.get_connection());
                cmd.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dashboard.username = username;
                        Dashboard.role = reader.GetString("role");
                        Dashboard.ownerId = reader.GetInt32("owner_id");
                    }
                    reader.Close();
                    string role = Dashboard.role;
                    string ownername;
                    cmd = new MySqlCommand("insert into loginhistory (emp_id, times, publicip, localip) values (@emp_id, @times, @publicip, @localip)", Db.get_connection());
                    cmd.Parameters.Add("@emp_id", MySqlDbType.Int32).Value = Dashboard.ownerId;
                    cmd.Parameters.Add("@times", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add("@localip", MySqlDbType.VarChar).Value = GetLocalIPAddress();
                    cmd.Parameters.Add("@publicip", MySqlDbType.VarChar).Value = GetPublicIpAddress();
                    if(cmd.ExecuteNonQuery() == 1)
                    {
                        switch (role)
                        {
                            case "Administration":
                                MySqlCommand cmd2 = new MySqlCommand("select emp_fullname, employee_pic from employee_data where employeeid = @employeeid", Db.get_connection());
                                cmd2.Parameters.AddWithValue("@employeeid", Dashboard.ownerId);
                                MySqlDataReader erader2 = cmd2.ExecuteReader();
                                while (erader2.Read())
                                {
                                    Dashboard.userPhotoPath = erader2.GetString("employee_pic");
                                    Dashboard.ownerName = erader2.GetString("emp_fullname");
                                }
                                dialog.Alert("Hello, welcome back " + Dashboard.ownerName + "!" + "\nWe missed you!", frmAlert.AlertType.Info);
                                dialog.Alert("You have been logged in successfully!", frmAlert.AlertType.Success);
                                Dashboard mainform;
                                mainform = (Dashboard)this.FindForm();
                                mainform.RoleSwitcher(Dashboard.RoleState.Administration);
                                mainform.picThumbUser.Image = Image.FromFile(Dashboard.userPhotoPath);
                                mainform.init();
                                mainform.isLoggedIn = true;
                                break;

                            default:
                                dialog.Alert("We've successfully logged you in \n but we don't know your roles :(", frmAlert.AlertType.Info);
                                break;
                        }
                    }
                    else
                    {
                        dialog.Alert("Something is wrong", frmAlert.AlertType.Error);
                    }
                }
                else
                {
                    dialog.Alert("Oopss.. We don't recognise you \nMind to reintroduce yourself again?", frmAlert.AlertType.Error);
                }
                Db.close_connection();

            }
            catch (MySqlException ex)
            {
                dialog.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

    }
}
