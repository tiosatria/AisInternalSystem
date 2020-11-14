using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Controller;

namespace AisInternalSystem.UserInterface
{
    public partial class UCRecStud : UserControl
    {
        private bool isLoaded;
        private bool isBusy;
        private State _state;

        public enum State
        {
            Insert, Update
        }
        public UCRecStud()
        {

        }
        public void InitObject(int i)
        {
            if (isLoaded)
            {

            }
            else
            {
                InitializeComponent();
                if (i == 0)
                {
                    _state = State.Insert;
                    Utilities.ClearControl(this);
                    SetPosition();
                }
                else
                {
                    _state = State.Update;
                    SetPosition();
                    FillData(i);
                }
            }
            isLoaded = true;
        }
        private void FillData(int i)
        {
            DataTable dt = new DataTable();
            dt = Query.Load(Query.Process.LoadStudent, new string[1] { i.ToString() });
            if (dt.Rows.Count >= 1)
            {
                
            }
            else
            {
                PopUp.Alert("Error occured", frmAlert.AlertType.Error);
            }
        }
        private void SetPosition()
        {

        }

    }
}
