using System;
using System.Windows.Forms;

namespace AisInternalSystem.Module
{
    public static class Msg
    {
        public static void Alert(string msg, frmAlert.AlertType type)
        {
            frmAlert f = new frmAlert();
            f.setAlert(msg, type);
        }

    }   
}
