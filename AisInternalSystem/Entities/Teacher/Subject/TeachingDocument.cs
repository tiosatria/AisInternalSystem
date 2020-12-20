using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities.Teacher.Subject
{
    public class TeachingDocument
    {
        public TeachingDocument()
        {

        }
        #region Properties
        public enum DocumentType
        {
            JudgingStandard, Paperwork, YearlyPlanner, WeeklyPlanner, CourseOutline, Curriculum
        }
        private DocumentType _doctype;
        
        #endregion
        
        #region Function

        #endregion
    }
}
