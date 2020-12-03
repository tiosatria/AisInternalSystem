using AisInternalSystem.Controller;
using System;
using System.Windows.Forms;

namespace AisInternalSystem
{
    public static class PopUp
    {
        #region Messages
        public static string WrongPassword = "Oops, we couldn't Recognize you\nMind to reintroduce yourself?";
        public static string MessageIntroduction(string usr)
        {
            return $"Hello! {usr}, welcome back!\nWe missed you!";
        }
        public static string NotAuthorized = "We're sorry but you're not authorized to do this\n Contact IT for more information or support.";
        #endregion

        public static void Alert(string msg, frmAlert.AlertType type)
        {
            frmAlert f = new frmAlert();
            f.setAlert(msg, type);
        }
    }   
}
