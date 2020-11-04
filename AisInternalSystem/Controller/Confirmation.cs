using AisInternalSystem.Entities;
using AisInternalSystem.Properties;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace AisInternalSystem.Controller
{
    public class Confirmation
    {

        private static DialogControl dialog = new DialogControl();

        public Confirmation()
        {

        }

        public enum onConfirmEnum
        {
            Exit   
        }
        private static onConfirmEnum OnConfirm;

        public static void Fire(Confirmation.onConfirmEnum @enum)
        {
            UIController.OverrideControl(dialog, DockStyle.Fill);
            OnConfirm = @enum;
            DialogProperties();
        }

        public static void DialogProperties()
        {
            switch (OnConfirm)
            {
                case onConfirmEnum.Exit:
                    dialog.Title = "Taking a break?";
                    dialog.Subtitle = $"Any unsaved data will be lost, make sure you've already save everything before you leave the desk\nHave a nice day!";
                    dialog.ImageType = Resources.icons8_question_mark_480px;
                    dialog.YesLabel = "Yes, let me go!";
                    dialog.NoLabel = "No, i misclicked it";
                    break;
                default:
                    break;
            }
        }

        public static void OnYes()
        {
            switch (OnConfirm)
            {
                case onConfirmEnum.Exit:
                    Application.Exit();
                    break;
             
                default:
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
                    
                    break;
            }
        }


    }
}
