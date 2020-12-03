using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using AisInternalSystem.Module;
using System.Threading.Tasks;
using System.Windows.Forms;
using AisInternalSystem.Controller;

namespace AisInternalSystem
{
    
    public partial class UCFeeDback : UserControl
    {

        public UCFeeDback()
        {
            InitializeComponent();
        }

        private void btnBackFeeDback_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        void LoadFeeDback()
        {
            try
            {
                Db.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("select id as 'Ticket ID', feeDback as 'FeeDback', stat as 'Status', doc as 'Submitted' from feeDback;", Db.GetConnection());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dg_feedback.DataSource = bs;
                if(dg_feedback.Rows.Count <= 1)
                {
                    panelEmpty.Visible = true;
                    dg_feedback.Visible = false;
                }
                else
                {
                    panelEmpty.Visible = false;
                    dg_feedback.Visible = true ;

                }
                Db.CloseConnection();
            }
            catch (MySqlException ex)
            {
                PopUp.Alert(ex.Message, frmAlert.AlertType.Error);   
            }
        }

        private void BtnDashboardAdmin_Click(object sender, EventArgs e)
        {
          
        }

        private void UCFeeDback_Load(object sender, EventArgs e)
        {
            LoadFeeDback();

        }
    }
}
