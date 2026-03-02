using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
  public  class DVOOrganiztionalStructureChart:DVOBase
    {


        #region Stored-Procedures
       

        public override string INSERT_SPNAME
        {
            get { return "uspsecuserins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspsecuserupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspsecuserdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspsecuserget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspFlxSegGetAll"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
        }
        public override string TABLE_NAME
        {
            get { return ""; }
        }

        public override int UNIQUE_ID
        {
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        #endregion Stored-Procedures

    }
}
