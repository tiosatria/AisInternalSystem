using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AisInternalSystem.Controller;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace AisInternalSystem.UserInterface.Menu
{
    public class ItemMenu
    {
        public ItemMenu()
        {

        }

        public MenuItem Item = new MenuItem();
        #region Properties
        private int _IdItem;

        public int IDItem
        {
            get { return _IdItem; }
            set { _IdItem = value; }
        }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public CategoryMenu Category { get; set; }
        public Image ItemImage { get; set; }
        public UIController.Controls Controls { get; set; }
        public MenuItem.DoingWhatEnum TypeDoing { get; set; }
        public List<Module.User.RoleIdentifier> Accessor {get;set;}
        #endregion
        #region Function
        public void Init()
        {
            Item.Title = ItemName;
            Item.Subtitle = ItemDescription;
            Item.Doing = TypeDoing;
            Item.Cons = Controls;
            Item.Image = ItemImage;
            Item.Category = Category;
            Item.Accessor = Accessor;
            Item.FromMenu = this;
        }
        #endregion
    }
}
