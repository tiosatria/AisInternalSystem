using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace AisInternalSystem
{
    public partial class UpperPanel : UserControl
    {
        public UpperPanel()
        {
            bool _created = isExist(this);
            if(_created)
            {
                
            }
            else
            {
                InitializeComponent();

            }
        }

        private bool isExist(UserControl ctr)
        {
            if(ctr.Created)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
