using System;
using System.Windows.Forms;

namespace AisInternalSystem.Module
{
    public class Dialog
    {
        public static DialogResult dialog = new DialogResult();

        public void ShowFailedDialog(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void ShowSuccessDialog(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ConfirmDialog(string message, string title)
        {

            dialog = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public void BlankDialog(string message, string tittle)
        {
            MessageBox.Show(message, tittle);
        }

        public void Alert(string msg, frmAlert.AlertType type)
        {
            frmAlert f = new frmAlert();
            f.setAlert(msg, type);
        }

    }   
}
