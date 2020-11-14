using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AisInternalSystem.Controller;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities
{
    public class IndividualItem
    {
        public IndividualItem()
        {

        }

        public int idItem { get; set; }
        public string ItemName { get; set; }
        public string ItemVariant { get; set; }
        public string ItemDescription { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }

        #region Function
        public static bool Input(IndividualItem item)
        {
            if (Query.Insert("InsertInventoryItem", new string[5] { "@_itemName", "@_itemVariant", "@_itemDescription", "@_serialNumber", "@_location" }, new MySql.Data.MySqlClient.MySqlDbType[5] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text }, new string[5] { item.ItemName, item.ItemVariant, item.ItemDescription, item.SerialNumber, item.Location }))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Update(IndividualItem item)
        {
            if (Query.Insert("UpdateInventoryItem", new string[6] { "@_idItem" ,"@_itemName", "@_itemVariant", "@_itemDescription", "@_serialNumber", "@_location" }, new MySql.Data.MySqlClient.MySqlDbType[6] { MySql.Data.MySqlClient.MySqlDbType.Int32 , MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text }, new string[6] { item.idItem.ToString() , item.ItemName, item.ItemVariant, item.ItemDescription, item.SerialNumber, item.Location }))
            {

                return true;
            }
            else
            {

                return false;
            }
        }
        public static bool Delete(IndividualItem item)
        {
            if (Query.Delete("DeleteInventoryItem", new string[1] { "@_iditem" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { item.idItem.ToString() }))
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
