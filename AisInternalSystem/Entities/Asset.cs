using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AisInternalSystem.Controller;
using System.Data;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities
{
    public class Asset
    {
        public Asset()
        {

        }

        public int idAsset { get; set; }
        public string CategoryAsset { get; set; }
        public int Logger { get; set; }
        public string NameAsset { get; set; }
        public string AssetDescription { get; set; }
        public static int QtyItemAll = 0;
        public static int QtyItemSpecific = 0;
        public decimal AssetPrice { get; set; }
        public string ImageLocation { get; set; }
        public string PrimaryStorageLocation { get; set; }
        public static List<string> AssetNameList = new List<string>();
        public static List<string> SpecificAssetNameList = new List<string>();
        public static Asset CurrentAsset = null;
        #region Function
        public static string MsgDataNotValid = "Oops, data not valid, please check your input";
        public static string MsgFailedRecord = "Failed to record Asset data, please try again";
        public static bool Input(Asset item)
        {
            if (Query.Insert("InsertInventory", new string[7] { "@_ItemCategory", "@_logger", "@_nameItem", "@_descItem", "@_priceItem", "@_imgLocation", "@_primarystoragelocation" }, new MySql.Data.MySqlClient.MySqlDbType[7] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.Decimal, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text }, new string[7] { item.idAsset.ToString(), item.Logger.ToString(), item.NameAsset, item.AssetDescription, item.AssetPrice.ToString(), item.ImageLocation, item.PrimaryStorageLocation }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Update(Asset item)
        {
            if (Query.Insert("InsertInventory", new string[8] { "@_iditem", "@_ItemCategory", "@_logger", "@_nameItem", "@_descItem", "@_priceItem", "@_imgLocation", "@_primarystoragelocation" }, new MySql.Data.MySqlClient.MySqlDbType[8] { MySql.Data.MySqlClient.MySqlDbType.Int32 , MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.Decimal, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text }, new string[8] { item.idAsset.ToString() , item.CategoryAsset, item.Logger.ToString(), item.NameAsset, item.AssetDescription, item.AssetPrice.ToString(), item.ImageLocation, item.PrimaryStorageLocation }))
            {

                return true;
            }
            else
            {

                return false;
            }
        }
        public static DataTable GetDataSource()
        {
            AssetNameList.Clear();
            QtyItemAll = 0;
            DataTable dt = Query.GetDataTable("FetchAllAsset", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] {"" } );
            if (dt.Rows.Count >= 1)
            {
                QtyItemAll = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AssetNameList.Add(dt.Rows[i][3].ToString());
                }
            }
            return dt;
        }
        public static DataTable GetDataSourceByName(string name)
        {
            DataTable dt = Query.GetDataTable("FetchAssetByName", new string[1] { "@_nameItem" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { name });
            QtyItemSpecific = 0;
            SpecificAssetNameList.Clear();
            if (dt.Rows.Count >= 1)
            {
                QtyItemSpecific = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SpecificAssetNameList.Add(dt.Rows[i][3].ToString());
                }
            }
            return dt;
        }
        public static DataTable GetDataSourceByCategory(string category)
        {
            DataTable dt = Query.GetDataTable("FetchAssetByCategory", new string[1] { "@_ItemCategory" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { category });
            QtyItemSpecific = 0;
            SpecificAssetNameList.Clear();
            if (dt.Rows.Count >= 1)
            {
                QtyItemSpecific = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SpecificAssetNameList.Add(dt.Rows[i][3].ToString());
                }
            }
            return dt;
        }
        public static bool Delete(Asset item)
        {
            if (Query.Delete("DeleteInventory", new string[1] { "@_iditem" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { item.idAsset.ToString() }))
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
