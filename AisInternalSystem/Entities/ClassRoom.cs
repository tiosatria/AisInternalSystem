using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities
{
    public class ClassRoom
    {
        public ClassRoom()
        {

        }
        public static event EventHandler<ClassRoom> CurrentClassChangedEvent;
        #region Properties
        public string AYCode { get; set; }
        public string AcademicYear { get; set; }
        public string ClassName { get; set; }
        public int Term { get; set; }
        private int _classIdentifier;
        public int ClassIdentifier
        {
            get { return _classIdentifier; }
            set { _classIdentifier = value; }
        }
        public int CareTeacherID { get; set; }
        public int AssCareTeacherID { get; set; }
        public List<Student> ListOfStudent { get; set; }
        public Module.Teacher CareTeacher { get; set; }
        public Module.Teacher AssCareteacher { get; set; }
        public static ClassRoom CurrentClass = null;
        #endregion

        #region Variables

        #endregion
        
        #region Function
        public static void InitCurrentClass(int id)
        {
            CurrentClass = new ClassRoom();
            DataTable dt =  Controller.Query.GetDataTable("fetch_class_info", new string[1] {"@classid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { id.ToString() });
            if (dt.Rows.Count >= 1)
            {
                CurrentClass.AYCode = dt.Rows[0][9].ToString();
                CurrentClass.AcademicYear = dt.Rows[0][10].ToString();
                CurrentClass.ClassName = dt.Rows[0][1].ToString();
                CurrentClass.Term = Convert.ToInt32(dt.Rows[0][11].ToString());
                CurrentClass.ClassIdentifier = id;
                CurrentClass.CareTeacherID = Convert.ToInt32(dt.Rows[0][3].ToString());
                CurrentClass.ListOfStudent = Student.GetListOfStudent(id);
                CurrentClass.CareTeacher = Module.Teacher.GetTeacherInfo(CurrentClass.CareTeacherID);
                CurrentClass.AssCareteacher = Module.Teacher.GetTeacherInfo(CurrentClass.AssCareTeacherID);
                CurrentClassChangedEvent?.Invoke(id, CurrentClass);
            }
            else
            {
                CurrentClass = null;
                System.Windows.Forms.MessageBox.Show("NULLED");
            }
        }
        public static DataTable ActiveClassName()
        {
            DataTable dt = Controller.Query.GetDataTable("GetActiveClassNameList", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
            return dt;
        }
        #endregion
    }
}
