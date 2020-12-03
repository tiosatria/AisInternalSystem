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

        #region Properties
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public int Maker { get; set; }
        public DateTime LastModified { get; set; }
        public string DocumentType { get; set; }
        public string DocumentDescription { get; set; }
        public int OwnerID { get; set; }
        #endregion

        #region Function
        //public static bool Update(Document document)
        //{
        //    if (Query.Insert("UpdateDocument", new string[8] { "", "", "", "", "", "", "", "" }, new MySql.Data.MySqlClient.MySqlDbType[8] { }, new string[8] { }))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public static bool Delete(Document document)
        {
            if (Query.Delete("DeleteDocument", new string[1] { "" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] {"" }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable GetDocumentbyID(int id, string o)
        {
            DataTable dt = Query.GetDataTable("GetDocumentList", new string[2] { "@_owner_id", "@_object" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar}, new string[2] { id.ToString(), o });
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
