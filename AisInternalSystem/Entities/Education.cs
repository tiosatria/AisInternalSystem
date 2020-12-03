using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities
{
    public class Education
    {
        public Education()
        {

        }
        #region Properties
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EduLevel { get; set; }
        public string EduDesignation { get; set; }
        public string StartYear { get; set; }
        public string GraduatedYear { get; set; }
        public string Institution { get; set; }
        #endregion

        #region Function
        public static List<Education> GetEducation(int o)
        {
            List<Education> edu = new List<Education>();
            DataTable dt = Controller.Query.GetDataTable("GetEmployeeEducation", new string[1] { "@_employeeid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { o.ToString() });
            if (dt.Rows.Count >= 1)
            {
                Education[] e = new Education[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    e[i] = new Education();
                    e[i].ID = Convert.ToInt32(dt.Rows[i][0].ToString());
                    e[i].EmployeeID = Convert.ToInt32(dt.Rows[i][1].ToString());
                    e[i].EduLevel = dt.Rows[i][2].ToString();
                    e[i].EduDesignation = dt.Rows[i][3].ToString();
                    e[i].StartYear = dt.Rows[i][4].ToString();
                    e[i].GraduatedYear = dt.Rows[i][5].ToString();
                    e[i].Institution = dt.Rows[i][6].ToString();
                    edu.Add(e[i]);
                }
                return edu;
            }
            else
            {
                return null;
            }
        }
        public static bool Insert(Education education)
        {
            if (Controller.Query.Insert("InsertEducationEmployee", new string[6] { "@_employeeid", "@_edulevel", "@_edudesignation", "@_eduyearstart", "@_eduyeargraduated", "@_eduinstitution" }, new MySql.Data.MySqlClient.MySqlDbType[6] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[6] { education.EmployeeID.ToString(), education.EduLevel, education.EduDesignation, education.StartYear, education.GraduatedYear ,education.Institution }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Delete(Education education)
        {
            if (Controller.Query.Delete("DeleteEducationEmployee", new string[1] { "@_EmployeeEducationID" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { education.ID.ToString() }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Update(Education education)
        {
            if (Controller.Query.Insert("UpdateEducationEmployeee", new string[6] { "@_EmployeeEducationID", "@_edulevel", "@_edudesignation", "@_eduyearstart", "@_eduyeargraduated", "@_eduinstitution" }, new MySql.Data.MySqlClient.MySqlDbType[6] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[6] { education.ID.ToString(), education.EduLevel, education.EduDesignation, education.StartYear, education.GraduatedYear, education.Institution }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
