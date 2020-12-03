using System;
using System.Collections.Generic;
using System.Drawing;
using AisInternalSystem.Controller;
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
        private string _img;
        private Image _image;
        public Image ImageUser
        {
            get { return _image; }
            set { _image = value; }
        }
        public string UserImage
        {
            get { return _img; }
            set { _img = value; }
        }
        public string Roles { get; set ; }
        public int OwnerID { get; set; }
        public int UserID { get; set; }
        public string OwnerName { get; set; }
        public string SecQuestion { get; set; }
        public string Answer { get; set; }
        public RoleIdentifier _role;
        public static List<string> SecretQuestion = new List<string> 
        { 
            "What is the name of your first pet?",
            "What was your first car?",
            "What elementary school did you attend?",
            "What is the name of the town where you were born?",
            "What is your mother name?"};
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
        public static bool UpdatePicture(User user)
        {
            if (Query.Insert("UpdateUserPicture", new string[2] { "@_employeeid", "@_employeepic" }, new MySql.Data.MySqlClient.MySqlDbType[2] {  MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar}, new string[2] { user.OwnerID.ToString(), user.UserImage  }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdateSecretQuestion(User user)
        {
            if (Query.Insert("UpdateUserSecretQuestion", new string[3] { "@_question", "@_answer", "@_ownerid" }, new MySql.Data.MySqlClient.MySqlDbType[3] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[3] { user.SecQuestion, user.Answer, user.OwnerID.ToString() }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Auth()
        {

        }
        #endregion
    }
}

