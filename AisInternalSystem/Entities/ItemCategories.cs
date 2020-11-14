﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AisInternalSystem.Controller;

namespace AisInternalSystem.Entities
{
    class ItemCategories
    {
        public ItemCategories()
        {

        }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public string CategoryDescription { get; set; }
        public string Unit { get; set; }
        #region Function
        public static bool Input(ItemCategories categories)
        {
            if (Query.Insert("InsertNewCategoriesItem", new string[3] { "@_Category", "@_CategoryDescription", "@_CategoryUnit" }, new MySql.Data.MySqlClient.MySqlDbType[3] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.Enum }, new string[3] { categories.Category, categories.CategoryDescription, categories.Unit }))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Update(ItemCategories categories)
        {
            if (Query.Insert("UpdateInventoryCategory", new string[4] { "@_idcat", "@_category", "@_categorydescription", "@_categoryunit" }, new MySql.Data.MySqlClient.MySqlDbType[4] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[4] { categories.CategoryID, categories.Category, categories.CategoryDescription, categories.Unit } ))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Delete(ItemCategories categories)
        {
            if (Query.Delete("DeleteCategories", new string[1] { "@_category" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { categories.CategoryID }))
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