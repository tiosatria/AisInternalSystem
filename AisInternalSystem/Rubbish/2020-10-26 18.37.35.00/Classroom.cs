using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace AisInternalSystem.Module
{
    public class Classroom
    {
        #region Properties
        public int classID { get; set; }
        public string className { get; set; }
        public string grade { get; set; }
        public string careTeacherName { get; set; }
        public string assistantName { get; set; }
        public string academicYear { get; set; }
        public int careTeacherId { get; set; }
        public int assistantId { get; set; }
        public string classStat { get; set; }
        public DateTime classStart { get; set; }
        public DateTime classEnd { get; set; }
        public int availableSeat { get; set; }
        public string ayCode { get; set; }
        public byte terms { get; set; }
        #endregion
        public Classroom()
        {

        }

        public Classroom(int i)
        {
            this.classID = i;
            GetClassInfo();
        }
        #region Function
        
        public static List<Student> GetClassMember(int clsid)
        {
            List<Student> ClassMember = new List<Student>();
            DataTable dt = QueryProcessor.Load(QueryProcessor.Process.ClassList, new string[1] { clsid.ToString() });
            if(dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ClassMember[i] = new Student();
                    ClassMember[i].studentName = dt.Rows[i][0].ToString();
                    ClassMember[i].aisID = Convert.ToInt32(dt.Rows[i][7]);

                }
            }
            else
            {

            }
            return ClassMember;
        }

        private void GetClassInfo()
        {
            DataTable dt = QueryProcessor.Load(QueryProcessor.Process.Class, new string[1] {classID.ToString() });
            if(dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    className = dt.Rows[i][0].ToString();
                    grade = dt.Rows[i][1].ToString();
                    careTeacherName = dt.Rows[i][11].ToString();
                    assistantName = dt.Rows[i][16].ToString();
                    careTeacherId = Convert.ToInt32(dt.Rows[i][2].ToString());
                    assistantId = Convert.ToInt32(dt.Rows[i][3].ToString());
                    classStat =  dt.Rows[i][6].ToString();
                    classStart = Convert.ToDateTime(dt.Rows[i][4].ToString());
                    classEnd = Convert.ToDateTime(dt.Rows[i][5].ToString());
                    availableSeat = Convert.ToInt32(dt.Rows[i][7]);
                    ayCode = dt.Rows[i][8].ToString();
                    academicYear = dt.Rows[i][9].ToString();
                    terms = Convert.ToByte(dt.Rows[i][10]);
                }
            }
            else
            {
                Msg.Alert("Class not found", frmAlert.AlertType.Error);
            }
        }
        #endregion


    }
}
