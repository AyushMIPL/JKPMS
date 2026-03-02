using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    ///     *********************Aim***************************** Developed By(Modified By)*********************** DevelopmentDate(Modified Date)
    ///1.)   DVO To get the Default Data of Saving Bank                  Rahul Jain                                    07/03/2009(DD)
    ///2.) 
    ///<summery>
    public class DVOSBDefaults : DVOBase
    {
        private string _sb_comp;
        private string _sb_address;
        private string _sb_address1;
        private int _dep_acctno;
        private int _with_acctno;
        private int _cash_acctno;
        private int _svng_acctno;
        private int _int_acctno;

        private string _dep_accttype;
        private string _with_accttype;
        private string _cash_accttype;
        private string _svng_accttype;
        private string _int_accttype;
        private string _dep_keyvalue;
        private string _with_keyvalue;
        private string _cash_keyvalue;
        private string _svng_keyvalue;
        private string _int_keyvalue;
        private string _dep_desc;
        private string _with_desc;
        private string _cash_desc;
        private string _svng_desc;
        private string _int_desc;

        private decimal _min_amount;
        private decimal _lower_interest;
        private decimal _upper_interest;
        private decimal _interest_break;
        private int _next_w_no;

        private int _RowID;
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        private int _current_tran_no;


        private int _acct_no;
        private string _acct_cat;
        private string _acct_status;
        private int _sb_post_no;
        private int _approval_code;

        #region Constructor
        public DVOSBDefaults()
        {
            _RowID = 0;
            _sb_comp = string.Empty;
            _sb_address = string.Empty;
            _sb_address1 = string.Empty;
            _dep_acctno = 0;
            _with_acctno = 0;
            _cash_acctno = 0;
            _svng_acctno = 0;
            _int_acctno = 0;

            _dep_accttype = "";
            _with_accttype = "";
            _cash_accttype = string.Empty;
            _svng_accttype = string.Empty;
            _int_accttype = "";
            _dep_keyvalue = "";
            _with_keyvalue = "";
            _cash_keyvalue = string.Empty;
            _svng_keyvalue = string.Empty;
            _int_keyvalue = "";
            _dep_desc = "";
            _with_desc = "";
            _cash_desc = string.Empty;
            _svng_desc = string.Empty;
            _int_desc = "";
            _min_amount = 0.0M;
            _lower_interest = 0.0M;
            _upper_interest = 0.0M;
            _interest_break = 0.0M;
            _next_w_no = 0;

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

            _current_tran_no = 0;
            _sb_post_no = 0;


            _acct_no = 0;
            _acct_cat = string.Empty;
            _acct_status = string.Empty;
            _approval_code = 0;

        }
        #endregion Constructor

        #region Properties
        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string sb_comp
        {
            get { return _sb_comp; }
            set { _sb_comp = value; }
        }
        public string sb_address
        {
            get { return _sb_address; }
            set { _sb_address = value; }
        }
        public string sb_address1
        {
            get { return _sb_address1; }
            set { _sb_address1 = value; }
        }

        public int dep_acctno
        {
            get { return _dep_acctno; }
            set { _dep_acctno = value; }
        }
        public int with_acctno
        {
            get { return _with_acctno; }
            set { _with_acctno = value; }
        }
        public int cash_acctno
        {
            get { return _cash_acctno; }
            set { _cash_acctno = value; }
        }
        public int svng_acctno
        {
            get { return _svng_acctno; }
            set { _svng_acctno = value; }
        }
        public int int_acctno
        {
            get { return _int_acctno; }
            set { _int_acctno = value; }
        }
        public decimal min_amount
        {
            get { return _min_amount; }
            set { _min_amount = value; }
        }
        public string dep_accttype
        {
            get { return _dep_accttype; }
            set { _dep_accttype = value; }
        }
        public string with_accttype
        {
            get { return _with_accttype; }
            set { _with_accttype = value; }
        }
        public string cash_accttype
        {
            get { return _cash_accttype; }
            set { _cash_accttype = value; }
        }
        public string svng_accttype
        {
            get { return _svng_accttype; }
            set { _svng_accttype = value; }
        }
        public string int_accouttype
        {
            get { return _int_accttype; }
            set { _int_accttype = value; }
        }

        public string dep_keyvalue
        {
            get { return _dep_keyvalue; }
            set { _dep_keyvalue = value; }
        }
        public string with_keyvalue
        {
            get { return _with_keyvalue; }
            set { _with_keyvalue = value; }
        }
        public string cash_keyvalue
        {
            get { return _cash_keyvalue; }
            set { _cash_keyvalue = value; }
        }
        public string svng_keyvalue
        {
            get { return _svng_keyvalue; }
            set { _svng_keyvalue = value; }
        }
        public string int_keyvalue
        {
            get { return _int_keyvalue; }
            set { _int_keyvalue = value; }
        }

        public string dep_desc
        {
            get { return _dep_desc; }
            set { _dep_desc = value; }
        }
        public string with_desc
        {
            get { return _with_desc; }
            set { _with_desc = value; }
        }
        public string cash_desc
        {
            get { return _cash_desc; }
            set { _cash_desc = value; }
        }
        public string svng_desc
        {
            get { return _svng_desc; }
            set { _svng_desc = value; }
        }
        public string int_desc
        {
            get { return _int_desc; }
            set { _int_desc = value; }
        }


        public decimal lower_interest
        {
            get { return _lower_interest; }
            set { _lower_interest = value; }
        }
        public decimal upper_interest
        {
            get { return _upper_interest; }
            set { _upper_interest = value; }
        }
        public decimal interest_break
        {
            get { return _interest_break; }
            set { _interest_break = value; }
        }
        public int next_w_no
        {
            get { return _next_w_no; }
            set { _next_w_no = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public int current_tran_no
        {
            get { return _current_tran_no; }
            set { _current_tran_no = value; }
        }





        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }

        public string acct_cat
        {
            get { return _acct_cat; }
            set { _acct_cat = value; }
        }

        public string acct_status
        {
            get { return _acct_status; }
            set { _acct_status = value; }
        }

        public int sb_post_no
        {
            get { return _sb_post_no; }
            set { _sb_post_no = value; }
        }
        public int approval_code
        {
            get { return _approval_code; }
            set { _approval_code = value; }
        }
        #endregion Properties

        #region Stored-Procedures
        //**Added by Sunil Pahwa**************
        public override string INSERT_SPNAME
        {
            get { return "uspsbcontrolsins"; }
        }
        //************************************
        public override string UPDATE_SPNAME
        {
            get { return "uspsbcontrolsUpd"; }
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
            get { return "uspsbcontrolsget"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select acct_id,acct_cat,acct_no,acct_status, ");
            sql.Append(" doc_date,withdrawn_amt,deposit_amt,");
            sql.Append(" acct_balance,operator,tran_type,tran_no,doc_no ");
            sql.Append(" from sbtranr");
            sql.Append(" where ok_to_post NOT IN ('P','C') ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND acct_no =" + parameters[1].ToString());

            //Commented by Rahul Jain no need Status parameter 27/08/2009
            //if (parameters[2] != null)
            //    if (parameters[2].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(acct_status) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            
            sql.Append(" Order by doc_no");
            return sql.ToString();
        }

        //Added by Sunil Pahwa for getting SBDefault information
        public  string GET_SBDEFAULTS(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT sbcontrols.rowid v_RowID,sbcontrols.sb_comp v_sb_comp,sbcontrols.sb_address v_sb_address , ");
            sql.Append(" sbcontrols.sb_address1 v_sb_address1,sbcontrols.dep_acctno v_dep_acctno , ");
            sql.Append(" sbcontrols.with_acctno v_with_acctno,sbcontrols.int_acctno v_int_acctno ,");
            sql.Append("  sbcontrols.min_amount_limit v_min_amt_limit,sbcontrols.lower_int_rate v_lower_int_rate ,");
            sql.Append(" sbcontrols.upper_int_rate v_upper_int_rate,sbcontrols.interest_break v_interest_break ,");
            sql.Append(" s1.acct_type v_dep_accttype,s2.acct_type v_with_accttype ,");
            sql.Append(" s3.acct_type v_int_accttype,s1.keyvalue v_dep_keyvalue ,");
            sql.Append(" s2.keyvalue v_with_keyvalue,s3.keyvalue v_int_keyvalue ,");
            sql.Append(" sbcontrols.current_tran_no v_current_tran_no,s1.acct_desc  v_dep_acctdesc ,");
            sql.Append(" s2.acct_desc  v_with_acctdesc,s3.acct_desc  v_int_acctdesc ,");
            sql.Append(" sbcontrols.cash_acctno v_cash_acctno,s4.acct_type  v_cashAcctType ,");
            sql.Append(" s4.keyvalue  v_cashkeyvalue ,");
            sql.Append(" sbcontrols.svng_acctno v_svng_acctno,s5.acct_type  v_svngAcctType ,");
            sql.Append(" s5.keyvalue v_svngkeyvalue,s4.acct_desc v_cash_acctdesc , ");
            sql.Append(" s5.acct_desc v_svng_acctdesc,sbcontrols.next_w_no v_next_w_no,sbcontrols.sb_post_no v_sb_post_no,approval_code v_approval_code ");
            sql.Append(" FROM sbcontrols, outer(PayrollGLAccounts s1,PayrollGLAccounts s2,PayrollGLAccounts s3,PayrollGLAccounts s4,PayrollGLAccounts s5) ");
            sql.Append(" WHERE sbcontrols.dep_acctno=s1.acct_no ");
            sql.Append(" AND sbcontrols.with_acctno=s2.acct_no AND sbcontrols.int_acctno=s3.acct_no ");
            sql.Append(" AND sbcontrols.cash_acctno=s4.acct_no AND sbcontrols.svng_acctno=s5.acct_no ");


            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND rowid=" + Convert.ToInt32(parameters[0]));

            return sql.ToString();
        }
        //*************************************************
        public override string TABLE_NAME
        {
            get { return "sbcontrols"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion Stored-Procedures
    }
}
