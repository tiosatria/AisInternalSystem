using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem
{
    public partial class StudSummary : UserControl
    {
        public StudSummary()
        {
            InitializeComponent();
        }

        private void btnBackEmpDir_Click(object sender, EventArgs e)
        {
            this.Dock = DockStyle.None;
            this.SendToBack();
        }

        private void StudSummary_Load(object sender, EventArgs e)
        {
            //using (aisdbEntities2 )
        }
    }
}
