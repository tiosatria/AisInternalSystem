﻿using System;
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
        private string _img;

        public string UserImage
        {
            get { return _img; }
            set { _img = value;  }
        }
        
        public string Roles { get; set ; }
        public int OwnerID { get; set; }
        public int UserID { get; set; }
        public string OwnerName { get; set; }
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

        #endregion
    }
}

