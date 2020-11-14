using AisInternalSystem.Controller;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.Module
{
    public class Document
    {
        public Document()
        {

        }

        #region Function
        public static DataTable GetDocumentbystudentid(int id)
        {
            DataTable dt = Query.GetDataTable("GetDocumentList", new string[1] { "@_owner_id" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { id.ToString() });
            return dt;
        }
        public static bool Insert(string[] val)
        {
            if (Query.Insert("InsertDocument", new string[5] { "@_docspath", "@_maker", "@_docstype", "@_docsdesc", "@_owner_id" }, new MySql.Data.MySqlClient.MySqlDbType[5] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.Int32 }, val))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
