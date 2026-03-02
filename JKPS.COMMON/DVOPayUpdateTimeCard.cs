using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.)  DVo Used for Update Time Card                    Rajeev(D)                                    20/12/2008(DD)
    ///2.) 
    ///<summery>
    public class DVOPayUpdateTimeCard :DVOBase
    {


        //Constructor Used To Initialize the Class Data member
        public DVOPayUpdateTimeCard()
        {
        }


        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
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
