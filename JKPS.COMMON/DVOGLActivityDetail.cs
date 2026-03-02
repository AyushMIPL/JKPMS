using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLActivityDetail : DVOBase
    {
        private string _acct_type;
        private string _StartingMonth;
        private string _EndingMonth;
        private string _StartingYear;
        private string _EndingYear;
        private string _keyvalue;
        private string _DocDateFrom;
        private string _DocDateTo;
        private string _orig_joun;
       

        #region Constructor
        public DVOGLActivityDetail()
        {
            _acct_type = string.Empty;
            _EndingMonth = string.Empty;
            _EndingYear = string.Empty;
            _keyvalue = string.Empty;
            _StartingMonth = string.Empty;
            _StartingYear = string.Empty;
            _DocDateFrom = string.Empty;
            _DocDateTo = string.Empty;
            _orig_joun = string.Empty;
           
        }
        #endregion Constructor

        #region Properties
        public string AccountType
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string StartingYear
        {
            get { return _StartingYear; }
            set { _StartingYear = value; }
        }
        public string EndingYear
        {
            get { return _EndingYear; }
            set { _EndingYear = value; }
        }
        public string StartingMonth
        {
            get { return _StartingMonth; }
            set { _StartingMonth = value; }
        }
        public string EndingMonth
        {
            get { return _EndingMonth; }
            set { _EndingMonth = value; }
        }

        public string Keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string DocDateFrom
        {
            get { return _DocDateFrom;}
            set { _DocDateFrom=value;}
        }
        public string DocDateTo
        {
            get {return _DocDateTo;}
            set {_DocDateTo=value; }
        }
        public string orig_journal
        {
            get { return _orig_joun; }
            set { _orig_joun = value; }
        }
      
       
        #endregion Properties

        #region StoreProcedure
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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get
            {
                return "uspglactledpst";
            }
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
        public string GET_PAYROLLCHKJRN
        {
            get { return "usp_chkjrnget"; }
        }
        public string GETACT_DESC
        {
            get { return "uspact_descget"; }
        }
        public string GET_VOIDE
        {
            get { return "uspvoideget"; }
        }
        public string GET_EXP
        {
            get { return "uspgldtlget"; }
        }
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc, stgactvd.amount,");
            sql.Append("stgactvd.debit_credit,stxtranr.orig_journal,stxtranr.doc_no,stxtranr.doc_date,");
            sql.Append("stxckrgd.inv_chk_no, stxtranr.ref_code,stxtranr.doc_desc,stgtranr.acct_period, stgtranr.acct_year,PayrollGLAccounts.acct_type v_acct_type, ");
            sql.Append(" stxtranr.post_date "); //Added By Rahul Jain on 04-Jan-2010
            sql.Append(" from  PayrollGLAccounts,  stxtranr, stgtranr, stgactvd,  outer (stxckrgd)");
            sql.Append(" WHERE stgtranr.status in ('N','P')");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//Acct_type
                sql.Append(" and PayrollGLAccounts.acct_type ='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//Acct_No
            {
                parameters[1] = parameters[1].ToString().Trim().Replace("*","");
                sql.Append("  and PayrollGLAccounts.keyvalue LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%" + "'");
            }

            if (parameters[8].ToString() != string.Empty && parameters[8].ToString() != null)//Acct_No
                sql.Append("  and  stxtranr.orig_journal ='" + parameters[8].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[3].ToString() != parameters[5].ToString())
            {
                if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//MonthFrom
                    sql.Append(" and stgtranr.acct_period >= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//YearFrom
                    sql.Append(" and stgtranr.acct_year >= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//MonthTo
                    sql.Append(" and stgtranr.acct_period <= '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[5].ToString() != string.Empty && parameters[5].ToString() != null)//YearTo
                    sql.Append(" and stgtranr.acct_year <= '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            }
            else
            {
                if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//MonthFrom
                    sql.Append(" and stgtranr.acct_period >= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//YearFrom
                    sql.Append(" and stgtranr.acct_year  = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//MonthTo
                    sql.Append(" and stgtranr.acct_period <= '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
                
            }
            if (parameters[6].ToString() != string.Empty && parameters[6].ToString() != null)//YearTo
                sql.Append(" and stxtranr.doc_date >= '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");//DocdateFrom
            if (parameters[7].ToString() != string.Empty && parameters[7].ToString() != null)//YearTo
                sql.Append(" and stxtranr.doc_date <= '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");//DocDateTo 

            sql.Append(" and PayrollGLAccounts.acct_no = stgactvd.acct_no");
            sql.Append(" and stxtranr.doc_no = stgtranr.doc_no");
            sql.Append(" and stxtranr.orig_journal = stgtranr.orig_journal");
            sql.Append(" and stgtranr.doc_no = stgactvd.doc_no ");
            sql.Append(" and stgtranr.orig_journal = stgactvd.orig_journal");
            sql.Append(" and stxckrgd.orig_journal = stxtranr.orig_journal");
            sql.Append(" and stxckrgd.doc_no = stxtranr.doc_no");
            sql.Append(" and stxckrgd.acct_no = stgactvd.acct_no");

           
           
           return sql.ToString();
        }

        public string GETACTDOC
        {
            get { return "uspgetglact2"; }//uspgetglact
        }

        #endregion StoreProcedure
    }

}