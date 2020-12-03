using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities
{
    public class Grading
    {
        public Grading()
        {

        }
        #region Properties
        public int IDGrading { get; set; }
        public int FromSubject { get; set; }
        public int INClass { get; set; }
        public int IDGrader { get; set; }
        public string GradingComment { get; set; }
        public string AYCode { get; set; }
        public string FromSubjectString { get; set; }
        public string InClassString { get; set; }
        public string GraderName { get; set; }
        public static Grading CurrentGrade;
        #endregion
        
        #region Function
        //public static bool Insert(Grading grading)
        //{

        //}
        //public static bool Update(Grading grading)
        //{

        //}
        //public static bool Delete(Grading grading)
        //{

        //}
        //public static Grading Load(Student student)
        //{

        //}
        //public static DataTable GetDataTable()
        //{

        //}
        #endregion
    }
}
