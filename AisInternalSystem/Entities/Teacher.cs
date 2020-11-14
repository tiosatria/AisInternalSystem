using AisInternalSystem.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.WinControls;

namespace AisInternalSystem.Module
{
    public class Teacher
    {
        public Teacher()
        {

        }
        #region Properties
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        #endregion
        #region Function
        public static List<Teacher> GetTeacherList()
        {
            List<Teacher> teachers = new List<Teacher>();
            DataTable dt = Query.GetDataTable("GetTeacherList", new string[1] { "@_roles" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "Teacher" });
            if (dt.Rows.Count>=1)
            {
                Teacher[] teacher = new Teacher[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    teacher[i] = new Teacher();
                    teacher[i].TeacherID = Convert.ToInt32(dt.Rows[i][0].ToString());
                    teacher[i].TeacherName = dt.Rows[i][2].ToString();
                    teachers.Add(teacher[i]);
                }
            }
            return teachers;
        }
        public static List<Teacher> GetCareTeacherList()
        {
            List<Teacher> teacher = new List<Teacher>();
            DataTable dt = Query.GetDataTable("FetchTeacherAssignClass", new string[1] { "@_teacher" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "Teacher" });
            if (dt.Rows.Count >= 1)
            {
                Teacher[] teachers = new Teacher[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    teachers[i] = new Teacher();
                    teachers[i].TeacherID = Convert.ToInt32(dt.Rows[i][1].ToString());
                    teachers[i].TeacherName = dt.Rows[i][0].ToString();
                    teacher.Add(teachers[i]);
                }
            }
            return teacher;
        }
        public static List<Teacher> GetAssCareTeacherList()
        {
            List<Teacher> teacher = new List<Teacher>();
            DataTable dt = Query.GetDataTable("FetchTeacherAssignClass", new string[1] { "@_teacher" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "Assistant Teacher" });
            if (dt.Rows.Count >= 1)
            {
                Teacher[] teachers = new Teacher[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    teachers[i] = new Teacher();
                    teachers[i].TeacherID = Convert.ToInt32(dt.Rows[i][1].ToString());
                    teachers[i].TeacherName = dt.Rows[i][0].ToString();
                    teacher.Add(teachers[i]);
                }
            }
            return teacher;
        }
        public static List<Teacher> GetAssistantTeacherList()
        {
            List<Teacher> teachers = new List<Teacher>();
            DataTable dt = Query.GetDataTable("GetTeacherList", new string[1] { "@_roles" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "Assistant Teacher" });
            if (dt.Rows.Count >= 1)
            {
                Teacher[] teacher = new Teacher[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    teacher[i] = new Teacher();
                    teacher[i].TeacherID = Convert.ToInt32(dt.Rows[i][0].ToString());
                    teacher[i].TeacherName = dt.Rows[i][2].ToString();
                    teachers.Add(teacher[i]);
                }
            }
            return teachers;
        }
        #endregion
    }
}
