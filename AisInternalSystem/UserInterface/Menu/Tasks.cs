﻿using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace AisInternalSystem.UserInterface.Menu
{
    public class TasksUser
    {
        public TasksUser()
        { 
        
        }

        private int index;

        public int TaskIndex
        {
            get { return index; }
            set { index = value; }
        }

        private Guna2ShadowPanel _uc;

        public Guna2ShadowPanel Control
        {
            get { return _uc; }
            set { _uc = value; }
        }

        private MenuController.MenuDoes menuDoes;

        public MenuController.MenuDoes Does
        {
            get { return menuDoes; }
            set { menuDoes = value; }
        }

        private Guna2Button _sender;

        public Guna2Button Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        private MenuController.MenuType _fromgroup;

        public MenuController.MenuType Group
        {
            get { return _fromgroup; }
            set { _fromgroup = value; }
        }


        private System.Drawing.Point _location;

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(_sender.Location.X, Sender.Location.Y); }
            set { _location = value; }
        }

    }
}
