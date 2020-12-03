using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
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
            get { return _password; }
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
        public string TemporaryUsername { get; set; }
        public int UserID { get; set; }
        public string OwnerName { get; set; }
        public string SecQuestion { get; set; }
        public string SecQuestionTemp { get; set; }
        public string Answer { get; set; }
        public string AnswerTemporary { get; set; }
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
            if (Query.Insert("UpdateUserPicture", new string[2] { "@_employeeid", "@_employee_pic" }, new MySql.Data.MySqlClient.MySqlDbType[2] {  MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar}, new string[2] { user.OwnerID.ToString(), user.UserImage  }))
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
            if (Query.Insert("UpdateUserSecretQuestion", new string[3] { "@_question", "@_answer", "@_owner_id" }, new MySql.Data.MySqlClient.MySqlDbType[3] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[3] { user.SecQuestionTemp, user.AnswerTemporary, user.OwnerID.ToString() }))
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

        public static bool UpdatePassword(User user)
        {
            if (Query.Insert("UpdateUserPassword", new string[2] { "@_owner_id", "_password" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[2] { user.OwnerID.ToString(), user.Password })) 
            { 
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RequestChangeUserName(User user, string temporaryUsrname)
        {
            if (Query.Insert("UserAccountRequestChangeUsername", new string[3] { "@_ownerId", "@_currUsername", "@_ReqUsername"}, new MySql.Data.MySqlClient.MySqlDbType[3] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[3] { user.OwnerID.ToString(), user.usrName, user.TemporaryUsername }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable SecretQuestionValidation(string username, string question, string answer)
        {
            DataTable dt = Query.GetDataTable("SecretQuestionValidation", new string[3] { "@_username", "@_question", "@_answer" }, new MySql.Data.MySqlClient.MySqlDbType[3] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[3] { username, question, answer });
            return dt;
        }

        #endregion
    }
}

