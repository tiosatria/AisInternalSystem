using AisInternalSystem.Controller;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Module
{
    public class Grade
    {
        public Grade()
        {

        }
        #region Properties
        public int GradeLevel { get; set; }
        public string GradeName { get; set; }
        public string GradeDesc { get; set; }
        public string GradeDepartment { get; set; }
        #endregion
        #region Function
        public static List<Grade> GradeList()
        {
            List<Grade> grades = new List<Grade>();
            DataTable dt = Query.GetDataTable("GetGradeList", new string[1] {"@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] {MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] {"" } );
            Grade[] grade = new Grade[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grade[i] = new Grade();
                grade[i].GradeName = dt.Rows[i][0].ToString();
                grade[i].GradeLevel = Convert.ToInt32(dt.Rows[i][1].ToString());
                grade[i].GradeDepartment = dt.Rows[i][2].ToString();
                grade[i].GradeDesc = dt.Rows[i][3].ToString();
                grades.Add(grade[i]);
            }
            return grades;
        }
        public static Grade GetGradeInfo(string gname)
        {
            Grade grade = new Grade();
            DataTable dt = Query.GetDataTable("GetGrade", new string[1] { "@gname" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { gname });
            grade.GradeName = dt.Rows[0][0].ToString();
            grade.GradeLevel = Convert.ToInt32(dt.Rows[0][1].ToString());
            grade.GradeDesc = dt.Rows[0][3].ToString();
            grade.GradeDesc = dt.Rows[0][4].ToString();
            return grade;
        }
        #endregion
    }
}
