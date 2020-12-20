using MySql.Data.MySqlClient;
using System;
using System.Data;
using AisInternalSystem.Controller;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AisInternalSystem.Controller;

namespace AisInternalSystem.Entities
{
    public class AcademicYear
    {
        public AcademicYear()
        {

        }

        #region Variable

        #endregion

        //set properties
        #region Properties
        public int IDAY { get; set; }
        public string academicYearCode { get; set; }
        public string year { get; set; }
        public string terms { get; set; }
        public string status { get; set; }
        public string specialKey { get; set; }
        public static AcademicYear CurrentAcademicYear = null;
        #endregion

        #region Function
        public static AcademicYear GetOngoingAcademicYear()
        {
            DataTable dt = Query.GetDataTable("GetCurrentAcademicYear", new string[1] { "noparam" }, new MySqlDbType[1] { MySqlDbType.VarChar }, new string[1] { "" });
            if (dt.Rows.Count >=1)
            {
                AcademicYear ay = new AcademicYear();
                ay.IDAY = Convert.ToInt32(dt.Rows[0][0].ToString());
                ay.academicYearCode = dt.Rows[0][1].ToString();
                ay.year = dt.Rows[0][2].ToString();
                ay.terms = dt.Rows[0][3].ToString();
                ay.status = dt.Rows[0][4].ToString();
                ay.specialKey = dt.Rows[0][5].ToString();
                
                return ay;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
