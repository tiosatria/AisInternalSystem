using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.UserInterface.Notification
{
    public class Notification
    {
        public Notification()
        {

        }
        #region Properties

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Image ImageNotif { get; set; }
        Controller.UIController.Controls controls;

        #endregion
        
        #region Function
        public static void OnClick()
        {

        }
        public static void OnRemove()
        {

        }
        #endregion
    }
}
