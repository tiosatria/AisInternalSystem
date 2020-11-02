using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Module
{
    public class User
    {
        public User()
        {

        }
        #region Properties
        public string usrName { get; set; }
        private string _password;
        public string Password
        {
            get { return "Password"; }
            set { _password = value; }
        }
        public string Roles { get; set ; }
        public int OwnerID { get; set; }
        public int UserID { get; set; }
        public string OwnerName { get; set; }
        public RoleIdentifier _role;
        public enum RoleIdentifier
        {
            Management,
            Admin,
            IT,
            Teacher,
            Accounting
        }
        #endregion

        #region Variable

        #endregion

        #region Function

        #endregion
    }
}

