using AisInternalSystem.Entities;
using AisInternalSystem.Properties;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace AisInternalSystem.Controller
{
    public class Confirmation
    {
        public static event EventHandler CancelStudent;
        public static event EventHandler CancelEmployee;
        public static event EventHandler<int> ProceedBuyAdmissionForm;
        private static DialogControl dialog = new DialogControl();

        public Confirmation()
        {

        }

        public enum onConfirmEnum
        {
            Exit   , Update, CancelStudentRecord, DeleteStudentRecord, CancelEmployee, BuyAdmissionForm, ProceedBuyAdmissionForm
        }
        private static onConfirmEnum OnConfirm;
        public static void Fire(Confirmation.onConfirmEnum ok)
        {
            OnConfirm = ok;
            UIController.OverrideControl(dialog, DockStyle.Fill);
            DialogProperties(ok);
        }

        public static void DialogProperties(onConfirmEnum ok)
        {
            switch (ok)
            {
                case onConfirmEnum.BuyAdmissionForm:
                    dialog.Title = "Buy the Admission form?";
                    dialog.Subtitle = "This transaction will be recorded and cannot be cancelled, the new AIS ID Will be generated!";
                    dialog.ImageType = Resources.icons8_Warning_192px;
                    dialog.YesLabel = "Yes, i understand!";
                    dialog.NoLabel = "No, i miscliked it!";
                    break;
                    break; 
                case onConfirmEnum.Exit:
                    dialog.Title = "Taking a break?";
                    dialog.Subtitle = $"Any unsaved data will be lost, make sure you've already save everything before you leave the desk\nHave a nice day!";
                    dialog.ImageType = Resources.icons8_question_mark_480px;
                    dialog.YesLabel = "Yes, let me go!";
                    dialog.NoLabel = "No, i misclicked it";
                    break;
                case onConfirmEnum.Update:
                    dialog.Title = "We've got an update for you!";
                    dialog.Subtitle = "Yeah, we know... it's been awhile... so... you want to update the system or not?";
                    dialog.ImageType = Resources.update;
                    dialog.YesLabel = "Yes, give me that sweet update!";
                    dialog.NoLabel = "Nah, just let me do my job";
                    break;
                case onConfirmEnum.CancelStudentRecord:
                    dialog.Title = "Cancel student data input?";
                    dialog.Subtitle = "This will ignore all the changes that you made before you saved the data, continue?";
                    dialog.ImageType = Resources.icons8_question_mark_480px;
                    dialog.YesLabel = "Yes, i understand";
                    dialog.NoLabel = "No, i miscliked it";
                    break;
                case onConfirmEnum.CancelEmployee:
                    dialog.Title = "Cancel employee data input?";
                    dialog.Subtitle = "This will ignore all the changes that you made before you saved the data, continue?";
                    dialog.ImageType = Resources.icons8_question_mark_480px;
                    dialog.YesLabel = "Yes, i understand";
                    dialog.NoLabel = "No, i miscliked it";
                    break;
                case onConfirmEnum.ProceedBuyAdmissionForm:
                    dialog.Title = "Transaction has been done succesfully!";
                    dialog.Subtitle = "";
                    dialog.ImageType = Resources.icons8_google_forms_50px;
                    dialog.YesLabel = "Proceed to next step";
                    dialog.NoLabel = "This button should not be visible";
                    break;
            }
        }

        public static string StringWrapper = null;
        public static int IntegerWrapper = 0;


        public static void OnYes()
        {
            switch (OnConfirm)
            {
                case onConfirmEnum.Exit:
                    Application.Exit();
                    break;
                case onConfirmEnum.Update:
                    string workingdir = Directory.GetCurrentDirectory();
                    Process.Start($@"{workingdir}\Updater.exe");
                    Application.Exit();
                    break;
                case onConfirmEnum.CancelStudentRecord:
                    CancelStudent?.Invoke(dialog, EventArgs.Empty);
                    break;
                case onConfirmEnum.CancelEmployee:
                    CancelEmployee?.Invoke(dialog, EventArgs.Empty);
                    break;
                case onConfirmEnum.BuyAdmissionForm:
                    Confirmation.Fire(onConfirmEnum.ProceedBuyAdmissionForm);
                    break;
                case onConfirmEnum.ProceedBuyAdmissionForm:

                    break;
            }
        }

        public static void onNo()
        {
            switch (OnConfirm)
            {
                case onConfirmEnum.Exit:
                    dialog.SendToBack();            
                    break;
                default:
                case onConfirmEnum.Update:
                    dialog.SendToBack();
                    UIController.NavigateUI(UIController.Controls.UCLogin);
                    break;
                case onConfirmEnum.CancelStudentRecord:
                    dialog.SendToBack();
                    break;
                case onConfirmEnum.CancelEmployee:
                    dialog.SendToBack();
                    break;
                case onConfirmEnum.BuyAdmissionForm:
                    dialog.SendToBack();
                    break;
            }
        }


    }
}
