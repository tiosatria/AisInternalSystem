using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Module
{
    public class AcademicYear
    {
        //set properties
        #region Properties
        public string academicYearCode { get; set; }
        public string year { get; set; }
        public string terms { get; set; }
        public string status { get; set; }
        public string specialKey { get; set; }
        #endregion

        #region Function
        private static void GetCurrentAcademicYearCode()
        {
            try
            {
                Db.open_connection();

                Db.close_connection();
            }
            catch (MySqlException ex)
            {

                throw;
            }
        }
        #endregion
    }
}
