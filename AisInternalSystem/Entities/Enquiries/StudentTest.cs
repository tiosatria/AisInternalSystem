using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities.Enquiries
{
    public class StudentTest
    {
        public StudentTest()
        {

        }
        #region Properties
        public int TestID { get; set; }
        public Enquiries Enquiries { get; set; }
        public string S1Name { get; set; }
        public string S2Name { get; set; }
        public string S3Name { get; set; }
        public int S1Val { get; set; }
        public int S2Val { get; set; }
        public int S3Val { get; set; }
        public string DocsLocation { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime TestTaken { get; set; }
        public string Maker { get; set; }
        public int teacherID { get; set; }
        public int AisID { get; set; }
        #endregion

        #region Function
        //public static bool Insert()
        //{

        //}
        //public static StudentTest Get()
        //{

        //}
        //public static bool Update()
        //{

        //}
        //public static bool Delete()
        //{

        //}
        #endregion
    }
}
