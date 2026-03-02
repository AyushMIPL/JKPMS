using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPaymentDue : DVOBase
    {
        private string _vend_code;
        private string _bus_name;
        private string _TopaydateFrom;
        private string _TopaydateTo;
        #region Constructor
        public DVOPaymentDue()
        {
            _vend_code = string.Empty;
            _bus_name = string.Empty;
            _TopaydateFrom = string.Empty;
            _TopaydateTo = string.Empty;
        }
        #endregion Constructor

        #region Properties
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
        }
        public string TopaydateFrom
        {
            get { return _TopaydateFrom; }
            set { _TopaydateFrom = value; }
        }
        public string TopaydateTo
        {
            get { return _TopaydateTo; }
            set { _TopaydateTo = value; }
        }
        #endregion Properties


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
            get { return "usppaymentsdue"; }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
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
        public string GETPAYTONAME
        {
            get { return "usppaytonameget"; }
        }

        //Added By Rajeev For REport PrintChecksAmountDue
        public string GET_CHECK_AMOUNT_DUE
        {
            get { return "uspapvdchkget"; }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT   s4.vend_code AS p_vend_code, s1.doc_date AS v_doc_date, s3.doc_type AS v_doc_type, s3.inv_chk_no AS v_inv_chk_no,");
            sql.Append("s1.doc_desc AS v_doc_desc, s2.amount AS v_amount, s4.bus_name AS v_bus_name, s5.inv_date AS v_inv_date, s1.doc_no AS v_doc_no,");
            sql.Append("s5.due_date AS v_due_date, s5.to_pay_date AS v_to_pay_date, s5.pay_to_code v_pay_to_code,  s5.disc_amt v_disc_amt, s5.to_pay_amt v_to_pay_amt, s6.keyvalue, s6.acct_desc");
            sql.Append(" FROM   stxtranr AS s1,stpactvd AS s2,stptranr AS s3,stpvendr AS s4, stpopend AS s5,PayrollGLAccounts AS s6 ");
            sql.Append(" WHERE  (s2.act_type <> 'D') AND (s5.balance <> 0)");
            sql.Append(" and s1.orig_journal = s2.orig_journal AND s1.doc_no = s2.doc_no  ");
            sql.Append(" and s1.doc_no = s3.doc_no AND s1.orig_journal = s3.orig_journal");
            sql.Append(" and s4.vend_code = s1.ref_code ");
            sql.Append(" and s5.doc_no = s1.doc_no");
            sql.Append(" and s5.cash_acct_no = s6.acct_no");

            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != string.Empty)//vend_code
                sql.Append(" and s4.vend_code = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null) //Bus_name
                sql.Append(" and s4.bus_name LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null) //FromDate
                sql.Append(" and s5.to_pay_date>= '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null) //ToDate
                sql.Append(" and s5.to_pay_date<='" + parameters[3].ToString().Replace("'", "''") + "'");
            return sql.ToString();
        }
    }
}
