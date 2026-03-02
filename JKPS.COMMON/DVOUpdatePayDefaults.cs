using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOUpdatePayDefaults:DVOBase
    {
        private int _rowid;
        private string _post_gl;
        private string _ein_number;
        private string _state_number;
        private string _fedtax_code;
        private string _fica_code;
        private string _medicare_code;
        private string _statax_code;
        private string _loctax_code;
        private string _futa_code;
        private string _fica_ob_code;
        private string _medicare_ob_code;
        private string _eic_code;
        private int _exp_acct;
        private int _liab_acct;
        private int _cash_acct;
        private string _mmedia_file;
        private string _mmedia_command;
        private int _py_doc_no;
        private int _py_post_no;
        private int _immed_dest_dfi;
        private int _immed_chk_digit;
        private string _immed_dest_name;
        private string _co_bank_acct_no;
        private string _offset_debit;
        private string _set_up;
        private string _suta_code;
        //************************For Deduction***********************//
        private string _ded_code;
        private string _description;       
        //************************For Obligation***********************//
        private string _obl_code;
        private string _acct_type;
        private string _keyvalue;
        private string _exp_keyvalue;
        private string _liab_keyvalue;
        private string _cash_keyvalue;
        //Added By Rahul Jain on 31-03-2009 for account type
        private string _liab_acct_type;
        private string _exp_acct_type;

        private int _exp_AccountNo;
        private int _liab_AccountNo;
        private int _cash_AccountNo;

        string _cash_acct_type;
        string _liab_acct_desc;
        string _exp_acct_desc;
        string _cash_acct_desc;
       

        #region Constructor

        public DVOUpdatePayDefaults()
        {
            _rowid = 0;
            _post_gl =string.Empty;
            _ein_number =string.Empty;
            _state_number=string.Empty;
            _fedtax_code=string.Empty;
            _fica_code=string.Empty;
            _medicare_code=string.Empty;
            _statax_code=string.Empty;
            _loctax_code=string.Empty;
            _futa_code=string.Empty;
            _fica_ob_code=string.Empty;
            _medicare_ob_code =string.Empty;
            _eic_code=string.Empty;
            _exp_acct=0;
            _liab_acct=0;
            _cash_acct=0;
            _mmedia_file=string.Empty;
            _mmedia_command=string.Empty;
            _py_doc_no=0;
            _py_post_no=0;
            _immed_dest_dfi=0;
            _immed_chk_digit=0;
            _immed_dest_name=string.Empty;
            _co_bank_acct_no=string.Empty;
            _offset_debit=string.Empty;
            _set_up=string.Empty;
            _suta_code=string.Empty;
            _ded_code = string.Empty;
            _description = string.Empty;
            _obl_code = string.Empty;
            _keyvalue = string.Empty;
            _acct_type = string.Empty;
            _exp_keyvalue = string.Empty;
            _liab_keyvalue = string.Empty;
            _cash_keyvalue = string.Empty;
            //Added By Rahul Jain on 31-03-2009 for account type
            _liab_acct_type = string.Empty;
            _exp_acct_type = string.Empty;

             _exp_AccountNo=0;
            _liab_AccountNo=0;
            _cash_AccountNo=0;

            _cash_acct_type = string.Empty;
            _liab_acct_desc = string.Empty;
            _exp_acct_desc = string.Empty;
            _cash_acct_desc = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        } 
        public string post_gl
         {
             get {return _post_gl; }
             set{_post_gl=value; }
         }
         public string ein_number
         {
             get {return _ein_number; }
             set{_ein_number=value; }
         }
        public string state_number
         {
             get {return _state_number; }
             set{_state_number=value; }
         }
         public string fedtax_code
         {
             get {return _fedtax_code; }
             set{_fedtax_code=value; }
         }
        public string fica_code
         {
             get {return _fica_code; }
             set{_fica_code=value; }
         }
        public string medicare_code
         {
             get {return _medicare_code; }
             set{_medicare_code=value; }
         }
        public string statax_code
         {
             get {return _statax_code; }
             set{_statax_code=value; }
         }
        public string futa_code
         {
             get { return _futa_code; }
             set { _futa_code = value; }
         }
        public string medicare_ob_code
         {
             get { return _medicare_ob_code; }
             set { _medicare_ob_code = value; }
         }
        public string fica_ob_code
         {
             get {return _fica_ob_code; }
             set{_fica_ob_code=value; }
         }
         public string loctax_code
         {
             get {return _loctax_code; }
             set{_loctax_code=value; }
         }       
        public string eic_code
         {
             get {return _eic_code; }
             set{_eic_code=value; }
         }
        public int exp_acct
         {
             get {return _exp_acct; }
             set{_exp_acct=value; }
         } 
        public int liab_acct
         {
             get {return _liab_acct; }
             set{_liab_acct=value; }
         } 
        public int cash_acct
         {
             get {return _cash_acct; }
             set{_cash_acct=value; }
         } 
        public string mmedia_file
         {
             get {return _mmedia_file; }
             set{_mmedia_file=value; }
         } 
        public string mmedia_command
         {
             get {return _mmedia_command; }
             set{_mmedia_command=value; }
         } 
        public int py_doc_no
         {
             get {return _py_doc_no; }
             set{_py_doc_no=value; }
         } 
        public int py_post_no
         {
             get {return _py_post_no; }
             set{_py_post_no=value; }
         } 
          public int immed_dest_dfi
         {
             get {return _immed_dest_dfi; }
             set{ _immed_dest_dfi=value; }
         } 
        public int immed_chk_digit
         {
             get {return _immed_chk_digit; }
             set{_immed_chk_digit=value; }
         } 
        public string immed_dest_name
         {
             get {return _immed_dest_name; }
             set{_immed_dest_name=value; }
         } 
        public string co_bank_acct_no
         {
             get {return _co_bank_acct_no; }
             set{_co_bank_acct_no=value; }
         } 
        public string offset_debit
         {
             get {return _offset_debit; }
             set{_offset_debit=value; }
         } 
        public string set_up
         {
             get {return _set_up; }
             set{_set_up=value; }
         }
        public string suta_code
         {
             get {return _suta_code; }
             set{_suta_code=value; }
         }
        public string ded_code
        {
            get { return _ded_code; }
            set { _ded_code = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string obl_code
        {
            get { return _obl_code; }
            set { _obl_code = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string exp_keyvalue
        {
            get { return _exp_keyvalue; }
            set { _exp_keyvalue = value; }
        }
        public string liab_keyvalue
        {
            get { return _liab_keyvalue; }
            set { _liab_keyvalue = value; }
        }
        public string cash_keyvalue
        {
            get { return _cash_keyvalue; }
            set { _cash_keyvalue = value;}
        }
        //Added By rahul Jain on 31-03-2009 for getting account type
        public string liab_acct_type
        {
            get { return _liab_acct_type; }
            set { _liab_acct_type = value; }
        }
        public string exp_acct_type
        {
            get { return _exp_acct_type; }
            set { _exp_acct_type = value; }
        }

        public string cash_acct_type
        {
            get { return _cash_acct_type; }
            set { _cash_acct_type = value; }
        }
        public string liab_acct_desc
        {
            get { return _liab_acct_desc; }
            set { _liab_acct_desc = value; }
        }
        public string exp_acct_desc
        {
            get { return _exp_acct_desc; }
            set { _exp_acct_desc = value; }
        }
        public string cash_acct_desc
        {
            get { return _cash_acct_desc; }
            set { _cash_acct_desc = value; }
        }
         

        #endregion Public Properties

        #region Stored-Procedures

        public string GET_Pay_Deduction
        {
            get { return "USP_PayDeduction"; }
        }
        public string GET_Pay_Obligation
        {
            get { return "USP_PayObligation"; }
        }
        public string GET_eic_Income
        {
            get { return "USP_Pay_Eic_Income"; }
        }
        public string UPDATE_PY_DOC_NO
        {
            get { return "USP_PyDocNoUpd"; }
        }
        //****Added by Sunil Pahwa [24/08/09]****************
        public override string INSERT_SPNAME
        {
            get { return "USP_PayDefIns"; }
        }
        //****************************************************
        public override string UPDATE_SPNAME
        {
            get { return "USP_PayDefUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "usppaydefget"; }
        }
 
        public override string ALL_SPNAME
        {
            get { return "USP_PayDefGetAll"; }
        }
     
        public override string TABLE_NAME
        {
            get { return "PayControl"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT PayControl.post_gl, PayControl.ein_number, PayControl.state_number, ");
            sql.Append(" PayControl.fedtax_code, PayControl.fica_code, PayControl.medicare_code, ");
            sql.Append(" PayControl.statax_code, PayControl.loctax_code, PayControl.futa_code, PayControl.fica_ob_code,");
            sql.Append(" PayControl.medicare_ob_code, PayControl.eic_code, PayControl.exp_acct, PayControl.liab_acct, ");
            sql.Append(" PayControl.cash_acct, PayControl.mmedia_file, PayControl.mmedia_command, PayControl.py_doc_no,");
            sql.Append(" PayControl.py_post_no, PayControl.immed_dest_dfi,PayControl.immed_chk_digit,");
            sql.Append(" PayControl.immed_dest_name, PayControl.co_bank_acct_no, PayControl.offset_debit,");
            sql.Append(" PayControl.set_up, PayControl.suta_code, PayControl.PayControl_ID, ");
            sql.Append(" PayrollGLAccounts.keyvalue AS liab_keyvalue,PayrollGLAccounts.acct_type AS liab_acct_type , ");
            sql.Append(" PayrollGLAccounts_1.acct_type AS exp_acct_type, PayrollGLAccounts_1.keyvalue AS exp_keyvalue, ");
            sql.Append(" PayrollGLAccounts_2.keyvalue AS cash_keyvalue, PayrollGLAccounts_2.acct_type AS cash_acct_type,");
            sql.Append(" PayrollGLAccounts.acct_desc AS liab_acct_desc, PayrollGLAccounts_1.acct_desc AS exp_acct_desc,");
            sql.Append(" PayrollGLAccounts_2.acct_desc AS cash_acct_desc");
            sql.Append(" FROM  (((PayControl LEFT OUTER JOIN PayrollGLAccounts  ON PayControl.liab_acct = PayrollGLAccounts.acct_no)");
            sql.Append(" LEFT OUTER JOIN PayrollGLAccounts  as PayrollGLAccounts_1  ON PayControl.exp_acct = PayrollGLAccounts_1.acct_no)");
            sql.Append(" LEFT OUTER JOIN  PayrollGLAccounts as PayrollGLAccounts_2 ON PayControl.cash_acct = PayrollGLAccounts_2.acct_no)");
           
            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
