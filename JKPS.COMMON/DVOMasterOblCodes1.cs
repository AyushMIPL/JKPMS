using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
   
    /// Modified by: 
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class DVOMasterOblCodes1 : DVOBase
    {
        private string _obl_codes;
        #region Constructor
        public DVOMasterOblCodes1()
        {
            _obl_codes = string.Empty;
        }
        #endregion

        #region Public properties
        public string obl_codes
        {
            get
            {
                return _obl_codes;
            }
            set
            {
                _obl_codes = value;
            }
        }

        #endregion


        #region Stored-Procedures

        //public string AUTHENTICATION_SPNAME
        //{
        //    get { return "uspsecauthenticate"; }
        //}
        public string GetEmployeeType
        {
            get { return ""; }
        }
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
            get { return "UspobliCodeGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
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


        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select MasterOblCodes.obl_code,MasterOblCodes.description,MasterOblCodes.dflt_rate,");
            sql.Append(" MasterOblCodes.dflt_limit,MasterOblCodes.dflt_pay_limit,MasterOblCodes.obl_type,");
            sql.Append(" PayrollGLAccounts.acct_desc,PayrollGLAccounts.keyvalue,MasterOblCodes.dfltaccounttype,MasterOblCodes.dflt_acct,s1.keyvalue ExpKeyvalue");
            sql.Append(" from MasterOblCodes LEFT Outer JOIN PayrollGLAccounts ON PayrollGLAccounts.acct_no=MasterOblCodes.dflt_bacct");
           sql.Append(" LEFT Outer JOIN PayrollGLAccounts s1 ON  MasterOblCodes.dflt_acct=s1.acct_no ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(obl_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

            return sql.ToString();
        }
        public string GetObligationData(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select MasterOblCodes.obl_code,");
            sql.Append(" ");
            sql.Append(" MasterOblCodes.dfltaccounttype ");
            sql.Append(" from MasterOblCodes ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(obl_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

            return sql.ToString();
        }
        #endregion store-procedures

    }
}

