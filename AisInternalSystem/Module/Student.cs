using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Module
{
    public class Student
    {
        #region properties
        public int aisID { get; set; }
        public string studentName { get; set; }

        #endregion

        #region Function
        public static DataTable GetStudentData(int aisid)
        {
            DataTable dt = QueryProcessor.Load(QueryProcessor.Process.Student, new string[1] { aisid.ToString() });
            return dt;
        }
        public static void Update(string[] str)
        {

            //QueryProcessor.Update(QueryProcessor.Process.Student, str);
            

        }
        #endregion

    }

}
