using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace AisInternalSystem.Controller
{
    public class Threader
    {
        private static BackgroundWorker worker;
        public Threader()
        {

        }
        private static void InitWorker()
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
        }

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
