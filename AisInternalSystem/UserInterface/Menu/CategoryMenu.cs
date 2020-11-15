using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using Guna.UI2.WinForms;
using AisInternalSystem.Controller;
using System.Threading.Tasks;

namespace AisInternalSystem.UserInterface.Menu
{
    public class CategoryMenu
    {
        public CategoryMenu(int ID, string name, string description, Size _size, Point loc)
        {
            CategoryID = ID;
            CategoryName = name;
            CategoryDescription = description;
            Size = _size;
            Location = loc;
            Handler();
        }

        #region Properties
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        #region Handler;
        public Size Size { get; private set; }
        public Point Location { get; private set; }
        private Point startingPointX = new System.Drawing.Point(672);
        private Point startingPointY = new System.Drawing.Point(7);
        public Size NormalSize = new Size(142, 57);
        public int pointBetween = 156;
        
        public Guna2Button CategoryHandler = new Guna2Button();
        public Guna2VSeparator separator = new Guna2VSeparator();
        #endregion
        
        #endregion

        private void Handler()
        {
            CategoryHandler.Location = Location;
            CategoryHandler.Animated = true;
            CategoryHandler.BorderRadius = 6;
            CategoryHandler.Cursor = System.Windows.Forms.Cursors.Hand;
            CategoryHandler.FillColor = System.Drawing.Color.White;
            CategoryHandler.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CategoryHandler.ForeColor = System.Drawing.Color.Black;
            CategoryHandler.HoverState.Parent = CategoryHandler;
            CategoryHandler.Name = CategoryName;
            CategoryHandler.ShadowDecoration.Parent = CategoryHandler;
            CategoryHandler.Size = Size;
            CategoryHandler.TabIndex = 5;
            CategoryHandler.Text = CategoryName;
            CategoryHandler.Click += new EventHandler(OnClicked);
            separator.Location = new Point(CategoryHandler.Location.X + 144, 7);
            separator.Size = new Size(10, 61);
        }

        private void OnClicked(object sender, EventArgs e)
        {
            UIController.CategoryClicked(this);
        }

        public event EventHandler HandlerClicked;         

        private void HandlerProps(Guna2Button button)
        {

        }

        public static void GetMenu()
        {

        }
        /*
        1. Create List Of Menu Category
        2. Create Handle When Category is Created
        3. 

        */
    }
}
