using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AisInternalSystem;
using System.Web.UI;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace AisInternalSystem.Module
{
    public class UserInterface
    {
        Form mainform = Application.OpenForms[1];

        //Master Data
        public UserInterface()
        {

        }

        public enum NavigationEnumeration
        {
            Login,
            Home,
            Student,
            Employee,

        }

        private static NavigationEnumeration _navenum;

        public static void Navigation(NavigationEnumeration nav)
        {
            _navenum = nav;

            var mainform = Application.OpenForms[1];

            LoginFrm login = new LoginFrm();
            UpperPanel upper = new UpperPanel();

            
            //   mainform = (Dashboard)this.FindForm();
            switch (nav)
            {
                case NavigationEnumeration.Login:
                    if(mainform.Controls.Contains(upper))
                    {
                        mainform.Controls[mainform.Controls.IndexOf(upper)].BringToFront();
                        mainform.Controls[mainform.Controls.IndexOf(upper)].Dock = DockStyle.Top;
                    }
                    else
                    {
                        upper = new UpperPanel();
                        mainform.Controls.Add(upper);
                        mainform.Controls[mainform.Controls.IndexOf(upper)].BringToFront();
                        mainform.Controls[mainform.Controls.IndexOf(upper)].Dock = DockStyle.Top;
                    }
                    if(mainform.Controls.Contains(login))
                    {
                        mainform.Controls[mainform.Controls.IndexOf(login)].BringToFront();
                        mainform.Controls[mainform.Controls.IndexOf(login)].Dock = DockStyle.Top;
                    }
                    else
                    {
                        login = new LoginFrm();
                        mainform.Controls.Add(login);
                        mainform.Controls[mainform.Controls.IndexOf(login)].BringToFront();
                        mainform.Controls[mainform.Controls.IndexOf(login)].Dock = DockStyle.Top;
                    }
                    break;
                case NavigationEnumeration.Home:

                    break;
                case NavigationEnumeration.Student:

                    break;
                case NavigationEnumeration.Employee:

                    break;
                default:
                    break;
            }
            MessageBox.Show(mainform.Controls.Count.ToString());

        }

        private void GetControl(System.Windows.Forms.Control userControl)
        {

        }

        public void UI()
        {

        }
        #region UCMaster
                
        #endregion

    }
}
