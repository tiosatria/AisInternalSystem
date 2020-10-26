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
        MySqlCommand cmd;

        //set properties
        #region Properties
        public string academicYearCode { get; set; }
        public string year { get; set; }
        public string terms { get; set; }
        public string status { get; set; }
        public string specialKey { get; set; }
        #endregion

        #region Function
        private void GetCurrentAcademicYearCode()
        {
            try
            {
                Db.open_connection();
                cmd = new MySqlCommand("", Db.get_connection());
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }
        #endregion
    }
}
