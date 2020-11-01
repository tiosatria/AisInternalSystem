using AisInternalSystem.Controller;
using System;
using System.Windows.Forms;

namespace AisInternalSystem.Controller
{
    public static class PopUp
    {
        #region Messages
        public static string WrongPassword = "Oops, we couldn't Recognize you\nMind to reintroduce yourself?";
        public static string MessageIntroduction(string usr)
        {
            return $"Hello! {usr}, welcome back!\nWe missed you!";
        }
        #endregion

        public static void Alert(string msg, frmAlert.AlertType type)
        {
            frmAlert f = new frmAlert();
            f.setAlert(msg, type);
        }
    }   
}
