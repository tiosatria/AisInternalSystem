using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AisInternalSystem.Controller;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities
{
    public class Item
    {
        public Item()
        {

        }

        public int idItem { get; set; }
        public string ItemCategory { get; set; }
        public int Logger { get; set; }
        public string NameItem { get; set; }
        public string DescriptionItem { get; set; }
        public int QtyItem { get; set; }
        public decimal PriceItem { get; set; }
        public string ImageLocation { get; set; }
        public string PrimaryStorageLocation { get; set; }

        #region Function
        public static bool Input(Item item)
        {
            if (Query.Insert("InsertInventory", new string[7] { "@_ItemCategory", "@_logger", "@_nameItem", "@_descItem", "@_priceItem", "@_imgLocation", "@_primarystoragelocation" }, new MySql.Data.MySqlClient.MySqlDbType[7] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.Decimal, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text }, new string[7] { item.ItemCategory, item.Logger.ToString(), item.NameItem, item.DescriptionItem, item.PriceItem.ToString(), item.ImageLocation, item.PrimaryStorageLocation }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Update(Item item)
        {
            if (Query.Insert("InsertInventory", new string[8] { "@_iditem", "@_ItemCategory", "@_logger", "@_nameItem", "@_descItem", "@_priceItem", "@_imgLocation", "@_primarystoragelocation" }, new MySql.Data.MySqlClient.MySqlDbType[8] { MySql.Data.MySqlClient.MySqlDbType.Int32 , MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.Decimal, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text }, new string[8] { item.idItem , item.ItemCategory, item.Logger.ToString(), item.NameItem, item.DescriptionItem, item.PriceItem.ToString(), item.ImageLocation, item.PrimaryStorageLocation }))
            {

                return true;
            }
            else
            {

                return false;
            }
        }
        public static bool Delete(Item item)
        {
            if (Query.Delete("DeleteInventory", new string[1] { "@_iditem" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { item.idItem.ToString() }))
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
