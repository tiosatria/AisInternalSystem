using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AisInternalSystem.Entities
{
    public class ClassRoom
    {
        public ClassRoom()
        {

        }
        #region Properties
        private int _classIdentifier;

        public int ClassIdentifier
        {
            get { return _classIdentifier; }
            set { _classIdentifier = value; }
        }
        private int _careteacherid;

        public int CareTeacherIdentifier
        {
            get { return _careteacherid; }
            set { _careteacherid = value; }
        }
        private int _assistantTeacherId;

        public int AssistantTeacherId
        {
            get { return _assistantTeacherId; }
            set { _assistantTeacherId = value; }
        }

        #endregion

        #region Variables

        #endregion
        #region Function

        #endregion
    }
}
