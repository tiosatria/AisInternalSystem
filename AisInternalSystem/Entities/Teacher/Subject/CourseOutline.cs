using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities.Teacher.Subject
{
    public class CourseOutline
    {
        public CourseOutline()
        {

        }
        #region Properties
        public string FileName { get; set; }
        public DateTime DateModified { get; set; }
        public int Owner { get; set; }
        public int SubjectID { get; set; }

        #endregion

        #region Function

        #endregion

    }
}
