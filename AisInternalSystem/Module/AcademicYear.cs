using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Module
{
    public class AcademicYear
    {
        public AcademicYear()
        {

        }

        #region Variable
        MySqlCommand cmd;

        #endregion

        //set properties
        #region Properties
        public string academicYearCode { get; set; }
        public string year { get; set; }
        public string terms { get; set; }
        public string status { get; set; }
        public string specialKey { get; set; }
        #endregion

        #region Function
        public static string GetCurrentAcademicYearCode()
        {
            MySqlCommand cmd;
            string AYCode = null;
            try
            {
                cmd = new MySqlCommand("", Db.get_connection());
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    AYCode = reader.GetString("AcademicYearCode");
                }
                return AYCode;
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                return null;
            }
        }
        #endregion
    }
}
