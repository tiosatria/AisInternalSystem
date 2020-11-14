using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities
{
    public class Transaction
    {
        public Transaction()
        {

        }

        public int TransactionID { get; set; }
        public int ItemID { get; set; }
        public string TransactionDescription { get; set; }
        public int TransactionStarter { get; set; }
        public int TransactionAffector { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionTime { get; set; }
        public DateTime TransactionSchedule { get; set; }

        #region Function

        #endregion


    }
}
