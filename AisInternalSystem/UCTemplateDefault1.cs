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
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select id as 'Ticket ID', feeDback as 'FeeDback', stat as 'Status', doc as 'Submitted' from feeDback;", Db.get_connection());
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
                Db.close_connection();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);   
            }
        }

        private void BtnDashboardAdmin_Click(object sender, EventArgs e)
        {
            if(txtFeedback.Text != "")
            {
                try
                {
                    Db.open_connection();
                    MySqlCommand cmd = new MySqlCommand("insert into aisDb.feeDback (maker, feeDback, doc, stat) values (@maker, @feeDback, @doc, @stat)", Db.get_connection());
                    cmd.Parameters.Add("@maker", MySqlDbType.Int32).Value = Dashboard.ownerId;
                    cmd.Parameters.Add("@feeDback", MySqlDbType.Text).Value = txtFeedback.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add("@stat", MySqlDbType.VarChar).Value = "Input Received";
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Msg.Alert("Thank you for your feeDback!\nWe'll get back to you soon! :)", frmAlert.AlertType.Info);
                        LoadFeeDback();
                    }
                    else
                    {
                        Msg.Alert("We're sorry we're having some trouble", frmAlert.AlertType.Error);
                    }
                    Db.close_connection();
                }
                catch (MySqlException ex)
                {
                    Msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                Msg.Alert("Please tell us something...", frmAlert.AlertType.Warning);
            }
        }

        private void UCFeeDback_Load(object sender, EventArgs e)
        {
            LoadFeeDback();

        }
    }
}
