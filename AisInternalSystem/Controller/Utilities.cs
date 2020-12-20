﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Data;
using AisInternalSystem.Module;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using Guna.UI2.WinForms;
using System.Reflection;
using System.Drawing;
using AisInternalSystem.Properties;
using System.Diagnostics;

namespace AisInternalSystem.Controller
{
    public class Utilities
    {
        public static event EventHandler WorkerFinished;

        private static User.RoleIdentifier _role;
        public Utilities()
        {
            
        }
        public static void Clear(Control.ControlCollection controls, bool dispose)
        {
            for (int ix = controls.Count - 1; ix >= 0; --ix)
            {
                if (dispose) controls[ix].Dispose();
                else controls.RemoveAt(ix);
            }
        }
        private static int progressInt;
        private static string[] paramList;
        private static bool ProcessFinished;
        public static BackgroundWorker workerparam = null;
        private static BackgroundWorker InitWorker(WorkerProcess process, string[] listofParams)
        {
            paramList = listofParams;
            _processEnum = process;
            BackgroundWorker worker = new BackgroundWorker();
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            workerparam = worker;
            return worker;
        }

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerFinished?.Invoke(sender, EventArgs.Empty);
        }

        public static string GetSelectedDatagridValue(Guna2DataGridView datagrid, string targetHeader)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                int selectedrowindex = datagrid.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = datagrid.Rows[selectedrowindex];
                string a = Convert.ToString(selectedRow.Cells[targetHeader].Value);
                return a;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetFileDbLocationString(LocationType type, string name, OpenFileDialog dialog)
        {
            string dbstring;
            string ext = Path.GetExtension(dialog.FileName);
            switch (type)
            {
                case LocationType.SubjectPhoto:
                    dbstring = $@"\\{Db.ServerIPAddress}\SysInternal\Img\SubjectPhoto\" + name + ext;
                    return dbstring;
                case LocationType.StudentDocuments:
                    dbstring = $@"\\{Db.ServerIPAddress}\SysInternal\Docs\StudDocs\{ name}{ ext}";
                    return dbstring;
                case LocationType.StudentPhoto:
                    dbstring = $@"\\{Db.ServerIPAddress}\SysInternal\Img\StudentPhoto\{name}{ext}";
                    return dbstring;
                case LocationType.ParentPhoto:
                    dbstring = $@"\\{Db.ServerIPAddress}\SysInternal\Img\ParentsPhoto\{name}{ext}";
                    return dbstring;
                case LocationType.InventoryPhoto:
                    dbstring = $@"\\{Db.ServerIPAddress}\SysInternal\Img\InventoryPhoto\{name}{ext}";
                    return dbstring;
                case LocationType.EmployeePhoto:
                    dbstring = $@"\\{Db.ServerIPAddress}\SysInternal\Img\EmployeePhoto\{name}{ext}";
                    return dbstring;
                case LocationType.EmployeeDocument:
                    dbstring = $@"\\{Db.ServerIPAddress}\SysInternal\Docs\EmployeeDocs\{ name}{ ext}";
                    return dbstring;
                default:
                    return null;
            }
        }
        public static void ClearInputOnPanel(Panel panel)
        {
            foreach (var control in panel.Controls)
            {
                if (control is Guna2TextBox)
                {
                    Guna2TextBox txtbox = control as Guna2TextBox;
                    txtbox.Clear();
                }
                if (control is Guna2ComboBox)
                {
                    Guna2ComboBox combo = control as Guna2ComboBox;
                    if (combo.Items.Count >=1)
                    {
                        combo.SelectedIndex = 0;
                    }
                }
            }
        }
        public static void RunInBackground()
        {
            
        }
        private static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressInt = e.ProgressPercentage;
            if (progressInt == 100)
            {
                ProcessFinished = true;
                workerparam.Dispose();
            }
            ProcessFinished = false;
        }
        public enum LocationType
        {
            SubjectPhoto, StudentDocuments, StudentPhoto, ParentPhoto, InventoryPhoto, EmployeePhoto, EmployeeDocument

        }
        public static void SetDoubleBuffer(Control ctl, bool DoubleBuffered)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, ctl, new object[] { DoubleBuffered });
        }

        private static WorkerProcess _processEnum;
        public enum WorkerProcess
        {
           CopyFile, GetClassList, OpenFileNewThread
        }
        public static void WorkerFire(WorkerProcess process, string[] listofParams)
        {
            BackgroundWorker worker = InitWorker(process, listofParams);
            worker.RunWorkerAsync();
        }
        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (_processEnum)
            {
                case WorkerProcess.CopyFile:
                    CopyToServer(paramList[0], paramList[1]);
                    break;
                case WorkerProcess.GetClassList:

                    break;
                case WorkerProcess.OpenFileNewThread:
                    OpenFileDocs(paramList[0]);
                    break;
                default:
                    break;
            }
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
        public static void OpenFileDocs(string path)
        {
            try
            {
                Process.Start(path);

            }
            catch (Exception)
            {
                PopUp.Alert("We coudldn't find the file you're looking for", frmAlert.AlertType.Error);
            }
        }

        public static Image GetImage(string loc)
        {
            try
            {
                using (Image im = Image.FromFile(loc))
                {
                    Bitmap bm = new Bitmap(im);
                    return bm;
                    im.Dispose();
                }
            }
            catch (Exception)
            {
                return Properties.Resources.icons8_question_mark_480px;
            }
        }

        public static OpenFileDialog OpenImage(PictureBox picture)
        {
            OpenFileDialog opf = new OpenFileDialog() ;
            opf.Filter = ("Image Files(*.BMP; *.JPG; *.JPEG; *.PNG;)| *.BMP; *.JPG; *.JPEG; *.PNG; | All files(*.*) | *.*");
            if (opf.ShowDialog() == DialogResult.OK)
            {
                using(Image im = Image.FromFile(opf.FileName))
                {
                    Bitmap bm = new Bitmap(im);
                    picture.Image = bm;
                    im.Dispose();
                }
                return opf;
            }
            else
            {
                picture.Image = Resources.icons8_question_mark_480px;
                return null;
            }

        }

        public static OpenFileDialog OpenFile(string filter)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = (filter);
            if (opf.ShowDialog() == DialogResult.OK)
            {
                
            }
            return opf;
        }

        private static void CopyToServer(string from, string to)
        {
            try
            {
                FileStream fsOut = new FileStream(to, FileMode.Create);
                FileStream fsIn = new FileStream(from, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] bt = new byte[10000];
                int reaDbyte;
                while ((reaDbyte = fsIn.Read(bt, 0, bt.Length)) > 0)
                {
                    fsOut.Write(bt, 0, reaDbyte);
                    workerparam.ReportProgress((int)(fsIn.Position * 100 / fsIn.Length));
                }
                fsIn.Close();
                fsOut.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Error, file is being used, please try again!");
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
                Data.user.UserImage = dt.Rows[0][6].ToString();
                Data.user.SecQuestion = dt.Rows[0][7].ToString();
                Data.user.Answer = dt.Rows[0][8].ToString();
                try
                {
                    using (Image img = Image.FromFile(Data.user.UserImage))
                    {
                        Bitmap bitmap = new Bitmap(img);
                        Data.user.ImageUser = bitmap;
                        img.Dispose();
                    }
                }
                catch (Exception)
                {
                    Data.user.ImageUser = Resources.icons8_male_user_100;
                }
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

        private static void AuthNext()
        {
            UIController.NavigateUI(UIController.Controls.UpperPanelAdmin);
            UIController.NavigateUI(UIController.Controls.UCDashboardAdmin);
        }

        private static void DoAuth()
        {
            switch (_role)
            {
                case User.RoleIdentifier.Management:
                    PopUp.Alert("You're using the preview build!", frmAlert.AlertType.Info);
                    PopUp.Alert(PopUp.MessageIntroduction(Data.user.OwnerName), frmAlert.AlertType.Info);
                    AuthNext();
                    break;
                case User.RoleIdentifier.Admin:
                    AuthNext();
                    PopUp.Alert(PopUp.MessageIntroduction(Data.user.OwnerName), frmAlert.AlertType.Info);
                    break;
                case User.RoleIdentifier.IT:
                    AuthNext();
                    PopUp.Alert(PopUp.MessageIntroduction(Data.user.OwnerName), frmAlert.AlertType.Info);
                    break;
                case User.RoleIdentifier.Teacher:
                    AuthNext();
                    PopUp.Alert(PopUp.MessageIntroduction(Data.user.OwnerName), frmAlert.AlertType.Info);
                    break;

                case User.RoleIdentifier.Accounting:
                    AuthNext();
                    PopUp.Alert(PopUp.MessageIntroduction(Data.user.OwnerName), frmAlert.AlertType.Info);
                    break;

                default:
                    break;
            }
        }
        public static int GetAgeBasedOnDate(DateTime date)
        {
            int i = (int)Math.Floor((DateTime.Now - date).TotalDays / 365.25D);
            return i;
        }
        public static string GetFullAgeBasedOnDate(DateTime date)
        {
            string age = "";

            return age;
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
