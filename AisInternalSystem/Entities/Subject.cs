using AisInternalSystem.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace AisInternalSystem.Module
{
    public class Subject
    {
        #region Properties
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; }
        public DateTime SubjectCreated { get; set; }
        public string SubjectCreatedBy { get; set; }
        public Image SubjectImage { get; set; }
        public string SubjectImageLocation { get; set; }
        public string IsTaughtBy { get; set; }
        public string TaughtIn { get; set; }
        #endregion
        #region Variables

        #endregion
        #region Function
        private static bool listLoaded;
        public static List<Subject> SubjectList()
        {
                List<Subject> subjects = new List<Subject>();
                DataTable dt = Query.GetDataTable("GetSubjectList", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
                if (dt.Rows.Count >= 1)
                {
                    Subject[] subject = new Subject[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        subject[i] = new Subject();
                        subject[i].SubjectID = Convert.ToInt32(dt.Rows[i][0].ToString());
                        subject[i].SubjectName = dt.Rows[i][1].ToString();
                        subject[i].SubjectDescription = dt.Rows[i][5].ToString();
                        subject[i].SubjectCreated = Convert.ToDateTime(dt.Rows[i][3].ToString());
                        subject[i].SubjectCreatedBy = dt.Rows[i][2].ToString();
                        subject[i].SubjectImageLocation = dt.Rows[i][4].ToString();
                        subject[i].IsTaughtBy = dt.Rows[i][6].ToString();
                        subject[i].TaughtIn = dt.Rows[i][7].ToString();
                        subjects.Add(subject[i]);
                    }
                    return subjects;
                }
                else
                {
                    return null;
                }
            
        }
        public static bool InsertSubject(string[] str)
        {
            if (Query.Insert("InsertSubject", new string[5]
            { "@_subjectname", "@_createdby", "@_createdon", "@_subjectimg", "@_subjectdesc" },
            new MySql.Data.MySqlClient.MySqlDbType[5]
            {MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32,
                MySql.Data.MySqlClient.MySqlDbType.Timestamp, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                MySql.Data.MySqlClient.MySqlDbType.Text },
            new string[5] { str[0], str[1], str[2], str[3], str[4] }))
            {
                return true;
            }
            else
            {
                return false;
            }
;
        }
        #endregion
        public Subject()
        {

        }

    }
}
