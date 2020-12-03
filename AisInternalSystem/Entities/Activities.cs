using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AisInternalSystem.Controller;

namespace AisInternalSystem.Entities
{
    public class Activities
    {
        public Activities()
        {

        }

        #region Properties
        public enum ActivityType
        {
            Employee = 0,
            SchoolFee = 1,
            Accounting = 2,
            Admin = 3
        }
        public static List<string> ActivityTypeString = new List<string>();
        #endregion
        
        #region Function
        public static void InitActivities(Module.User.RoleIdentifier role)
        {
            switch (role)
            {
                case Module.User.RoleIdentifier.Management:
                    ActivityTypeString.AddRange(new List<string> { "Employee", "Accounting"});
                    break;
                case Module.User.RoleIdentifier.Admin:
                    
                    break;
                case Module.User.RoleIdentifier.IT:
                    
                    break;
                case Module.User.RoleIdentifier.Teacher:
                    
                    break;
                case Module.User.RoleIdentifier.Accounting:
                    
                    break;
            }
        }
        #endregion
    }
}
