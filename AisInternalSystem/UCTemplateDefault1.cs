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
    
    public partial class UCFeedback : UserControl
    {

        Dialog msg = new Dialog();
        Db db = new Db();


        public UCFeedback()
        {
            InitializeComponent();
        }

        private void btnBackFeedBack_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

        void LoadFeedback()
        {
            try
            {
                db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select id as 'Ticket ID', feedback as 'Feedback', stat as 'Status', doc as 'Submitted' from feedback;", db.get_connection());
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
                db.close_connection();
            }
            catch (MySqlException ex)
            {
                msg.Alert(ex.Message, frmAlert.AlertType.Error);   
            }
        }

        private void BtnDashboardAdmin_Click(object sender, EventArgs e)
        {
            if(txtFeedback.Text != "")
            {
                try
                {
                    db.open_connection();
                    MySqlCommand cmd = new MySqlCommand("insert into aisdb.feedback (maker, feedback, doc, stat) values (@maker, @feedback, @doc, @stat)", db.get_connection());
                    cmd.Parameters.Add("@maker", MySqlDbType.Int32).Value = Dashboard.ownerId;
                    cmd.Parameters.Add("@feedback", MySqlDbType.Text).Value = txtFeedback.Text;
                    cmd.Parameters.Add("@doc", MySqlDbType.Timestamp).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add("@stat", MySqlDbType.VarChar).Value = "Input Received";
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        msg.Alert("Thank you for your feedback!\nWe'll get back to you soon! :)", frmAlert.AlertType.Info);
                        LoadFeedback();
                    }
                    else
                    {
                        msg.Alert("We're sorry we're having some trouble", frmAlert.AlertType.Error);
                    }
                    db.close_connection();
                }
                catch (MySqlException ex)
                {
                    msg.Alert(ex.Message, frmAlert.AlertType.Error);
                }
            }
            else
            {
                msg.Alert("Please tell us something...", frmAlert.AlertType.Warning);
            }
        }

        private void UCFeedback_Load(object sender, EventArgs e)
        {
            LoadFeedback();

        }
    }
}
