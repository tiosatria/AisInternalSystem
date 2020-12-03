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
        public string Contact { get; set; }
        public string ImageLocation { get; set; }
        public DateTime BirthDate { get; set; }
        #endregion
        public static Teacher currentTeacher(int id)
        {
            Teacher tc = GetTeacherInfo(id);
            return tc;
        }
        public static Teacher currentAssistantTeacher(int id)
        {
            Teacher asstc = GetTeacherInfo(id);
            return asstc;
        }
        #region Function
        public static Teacher GetTeacherInfo(int id)
        {
            Teacher teacher = new Teacher();
            DataTable dt = Query.GetDataTable("GetTeacherInfo", new string[1] { "@_employeeid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { id.ToString() });
            if (dt.Rows.Count >= 1)
            {
                try
                {
                    teacher.TeacherID = Convert.ToInt32(dt.Rows[0][0].ToString());
                    teacher.TeacherName = dt.Rows[0][2].ToString();
                    teacher.Contact = dt.Rows[0][3].ToString();
                    teacher.ImageLocation = dt.Rows[0][4].ToString();
                    teacher.BirthDate = Convert.ToDateTime(dt.Rows[0][5].ToString());
                }
                catch (Exception)
                {
                    PopUp.Alert("Error getting teacher information", frmAlert.AlertType.Warning);
                }
            }
            else
            {
                teacher = null;
            }
            return teacher;
        }

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
                    teacher[i].Contact = dt.Rows[i][3].ToString();
                    teacher[i].ImageLocation = dt.Rows[i][4].ToString();
                    teacher[i].BirthDate = Convert.ToDateTime(dt.Rows[i][5].ToString());
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
