using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPrintVoteBook1:DVOBase
    {
        private string _acct_type;
       
        private string _ReportingYear;
       
        private string _keyvalue;
       
        

        # region Properties

        public string AccountType
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string ReportingYear
        {
            get { return _ReportingYear; }
            set { _ReportingYear = value; }
        }
       
       

        public string Keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
     
        # endregion Properties
        #region Constructure
        public DVOPrintVoteBook1()
        {
            _acct_type = string.Empty;
            
           
            _keyvalue = string.Empty;
       
            _ReportingYear = string.Empty;
           
        }

        #endregion Constructure
        #region StoreProcedures
        public override string INSERT_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string UPDATE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string DELETE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_SPNAME
        {
            get { return "uspvotebkookstxtr"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspaccmask"; }
        }
        public string FIND_ACCT_MASK
        {
            get { return "uspaccmask1"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
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
          #endregion StoreProcedures

    }
}
