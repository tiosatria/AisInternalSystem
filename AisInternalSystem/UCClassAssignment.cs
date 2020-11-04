using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem
{
    public partial class UCClassAssignment : UserControl
    {
        public UCClassAssignment()
        {
            InitializeComponent();
        }

        public enum EnumClassLeftPanel
        {
            Student,
            Class
        };
        private EnumClassLeftPanel _LeftPanel;

        void CASwitcher(EnumClassLeftPanel e)
        {
            _LeftPanel = e;
            switch (_LeftPanel)
            {
                case EnumClassLeftPanel.Student:
                    btn_class_addclass.FillColor = Color.Silver;
                    btn_class_addclass.ForeColor = Color.Black;
                    btn_class_stud.FillColor = Color.Black;
                    btn_class_stud.ForeColor = Color.White;
                    PanelAddClass.Hide();
                    panel_class_stud.Show();
                    break;
                case EnumClassLeftPanel.Class:
                    btn_class_addclass.FillColor = Color.Black;
                    btn_class_addclass.ForeColor = Color.White;
                    btn_class_stud.FillColor = Color.Silver;
                    btn_class_stud.ForeColor = Color.White;
                    PanelAddClass.Show();
                    panel_class_stud.Hide();
                    break;
                default:
                    break;
            }
        }

    }
}
