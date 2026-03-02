using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //sunil Pahwa
    public class DvoUpdatePayableDefDetails : DVOBase
    {


        private int _ap_accounttypeid;
        private string _ap_accounttype;
        private string _ap_accountdesc;
        private string _ap_keyvalue;

        private int _cd_accounttypeid;
        private string _cd_accounttype;
        private string _cd_accountdesc;
        private string _cd_keyvalue;
        private int _ap_account;
        private int _cd_account;
        private int _cd_disc_account;



        private int _cd_disc_acctypeid;
        private string _cd_disc_acctype;
        private string _cd_disc_accdesc;
        private string _cd_disc_keyvalue;



        //table stpcntrc
        private string _term_code;
        private int _ap_acct_no;
        private int _cd_cash_acct_no;
        private int _cd_disc_acct_no;
        private int _ap_doc_no;
        private int _ap_post_no;
        private string _ap_balanced;
        private int _cd_doc_no;
        private int _cd_post_no;
        private string _age_datetype;
        private int _age_per1;
        private int _age_per2;
        private int _age_per3;
        private string _age_dsc1;
        private string _age_dsc2;
        private string _age_dsc3;
        private string _age_dsc4;
        private string _federal_tax_id;
        private string _dflt_1099;
        private string _def_mtaxcd;
        private string _gross_entry;
        private string _mtax_dsc;
        private string _entry_by_line;
        private string _disc_frght;
        private string _disc_tax;
        private string _use_batch_inv;
        private string _use_batch_pay;
        private string _use_approv_post;
        private string _approval_code;
        private string _auto_chkno;
        private int _last_chkno;
        private string _terms_desc;
        private int _RowId;

        public DvoUpdatePayableDefDetails()
        {
            _ap_accounttypeid = 0;
            _ap_accounttype = string.Empty;
            _ap_accountdesc = string.Empty;
            _ap_keyvalue = string.Empty;

            _cd_accounttypeid = 0;
            _cd_accounttype = string.Empty;
            _cd_accountdesc = string.Empty;
            _cd_keyvalue = string.Empty;
            _ap_account = 0;
            _cd_account = 0;
            _cd_disc_account = 0;



            _cd_disc_acctypeid = 0;
            _cd_disc_acctype = string.Empty;
            _cd_disc_accdesc = string.Empty;
            _cd_disc_keyvalue = string.Empty;


            //table stpcntrc
            _term_code = string.Empty;
            _ap_acct_no = 0;
            _cd_cash_acct_no = 0;
            _cd_disc_acct_no = 0;
            _ap_doc_no = 0;
            _ap_post_no = 0;
            _ap_balanced = string.Empty;
            _cd_doc_no = 0;
            _cd_post_no = 0;
            _age_datetype = string.Empty;
            _age_per1 = 0;
            _age_per2 = 0;
            _age_per3 = 0;
            _age_dsc1 = string.Empty;
            _age_dsc2 = string.Empty;
            _age_dsc3 = string.Empty;
            _age_dsc4 = string.Empty;
            _federal_tax_id = string.Empty;
            _dflt_1099 = string.Empty;
            _def_mtaxcd = string.Empty;
            _gross_entry = string.Empty;
            _mtax_dsc = string.Empty;
            _entry_by_line = string.Empty;
            _disc_frght = string.Empty;
            _disc_tax = string.Empty;
            _use_batch_inv = string.Empty;
            _use_batch_pay = string.Empty;
            _use_approv_post = string.Empty;
            _approval_code = string.Empty;
            _auto_chkno = string.Empty;
            _last_chkno = 0;
            _terms_desc = string.Empty;
            _RowId = 0;


        }




        public int ap_accounttypeid
        {
            get { return _ap_accounttypeid; }
            set { _ap_accounttypeid = value; }
        }
        public string ap_accounttype
        {
            get { return _ap_accounttype; }
            set { _ap_accounttype = value; }
        }
        public string ap_accountdesc
        {
            get { return _ap_accountdesc; }
            set { _ap_accountdesc = value; }
        }

        public string ap_keyvalue
        {
            get { return _ap_keyvalue; }
            set { _ap_keyvalue = value; }
        }
        public int cd_accounttypeid
        {
            get { return _cd_accounttypeid; }
            set { _cd_accounttypeid = value; }
        }


        public string cd_accounttype
        {
            get { return _cd_accounttype; }
            set { _cd_accounttype = value; }
        }
        public string cd_accountdesc
        {
            get { return _cd_accountdesc; }
            set { _cd_accountdesc = value; }
        }
        public string cd_keyvalue
        {
            get { return _cd_keyvalue; }
            set { _cd_keyvalue = value; }
        }
        public int ap_account
        {
            get { return _ap_account; }
            set { _ap_account = value; }
        }

        public int cd_account
        {
            get { return _cd_account; }
            set { _cd_account = value; }
        }



        public int cd_disc_account
        {
            get { return _cd_disc_account; }
            set { _cd_disc_account = value; }
        }

        public int cd_disc_acctypeid
        {
            get { return _cd_disc_acctypeid; }
            set { _cd_disc_acctypeid = value; }
        }


        public string cd_disc_acctype
        {
            get { return _cd_disc_acctype; }
            set { _cd_disc_acctype = value; }
        }


        public string cd_disc_accdesc
        {
            get { return _cd_disc_accdesc; }
            set { _cd_disc_accdesc = value; }
        }

        public string cd_disc_keyvalue
        {
            get { return _cd_disc_keyvalue; }
            set { _cd_disc_keyvalue = value; }
        }




        public string term_code
        {
            get { return _term_code; }
            set { _term_code = value; }
        }
        public int ap_acct_no
        {
            get { return _ap_acct_no; }
            set { _ap_acct_no = value; }
        }
        public int cd_cash_acct_no
        {
            get { return _cd_cash_acct_no; }
            set { _cd_cash_acct_no = value; }
        }

        public int cd_disc_acct_no
        {
            get { return _cd_disc_acct_no; }
            set { _cd_disc_acct_no = value; }
        }


        public int ap_doc_no
        {
            get { return _ap_doc_no; }
            set { _ap_doc_no = value; }
        }
        public int ap_post_no
        {
            get { return _ap_post_no; }
            set { _ap_post_no = value; }
        }

        public string ap_balanced
        {
            get { return _ap_balanced; }
            set { _ap_balanced = value; }
        }

        public int cd_doc_no
        {
            get { return _cd_doc_no; }
            set { _cd_doc_no = value; }
        }
        public int cd_post_no
        {
            get { return _cd_post_no; }
            set { _cd_post_no = value; }
        }
        public string age_datetype
        {
            get { return _age_datetype; }
            set { _age_datetype = value; }
        }




        public int age_per1
        {
            get { return _age_per1; }
            set { _age_per1 = value; }
        }
        public int age_per2
        {
            get { return _age_per2; }
            set { _age_per2 = value; }
        }
        public int age_per3
        {
            get { return _age_per3; }
            set { _age_per3 = value; }
        }

        public string age_dsc1
        {
            get { return _age_dsc1; }
            set { _age_dsc1 = value; }
        }

        public string age_dsc2
        {
            get { return _age_dsc2; }
            set { _age_dsc2 = value; }
        }

        public string age_dsc3
        {
            get { return _age_dsc3; }
            set { _age_dsc3 = value; }
        }

        public string age_dsc4
        {
            get { return _age_dsc4; }
            set { _age_dsc4 = value; }
        }
        public string federal_tax_id
        {
            get { return _federal_tax_id; }
            set { _federal_tax_id = value; }
        }
        public string dflt_1099
        {
            get { return _dflt_1099; }
            set { _dflt_1099 = value; }
        }
        public string def_mtaxcd
        {
            get { return _def_mtaxcd; }
            set { _def_mtaxcd = value; }
        }
        public string gross_entry
        {
            get { return _gross_entry; }
            set { _gross_entry = value; }
        }
        public string mtax_dsc
        {
            get { return _mtax_dsc; }
            set { _mtax_dsc = value; }
        }
        public string entry_by_line
        {
            get { return _entry_by_line; }
            set { _entry_by_line = value; }
        }
        public string disc_frght
        {
            get { return _disc_frght; }
            set { _disc_frght = value; }
        }
        public string disc_tax
        {
            get { return _disc_tax; }
            set { _disc_tax = value; }
        }
        public string use_batch_inv
        {
            get { return _use_batch_inv; }
            set { _use_batch_inv = value; }
        }

        public string use_batch_pay
        {
            get { return _use_batch_pay; }
            set { _use_batch_pay = value; }
        }

        public string use_approv_post
        {
            get { return _use_approv_post; }
            set { _use_approv_post = value; }
        }

        public string approval_code
        {
            get { return _approval_code; }
            set { _approval_code = value; }
        }
        public string auto_chkno
        {
            get { return _auto_chkno; }
            set { _auto_chkno = value; }
        }
        public int last_chkno
        {
            get { return _last_chkno; }
            set { _last_chkno = value; }
        }

        public string terms_desc
        {
            get { return _terms_desc; }
            set { _terms_desc = value; }
        }

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }

        #region Stored-Procedures
        public override string INSERT_SPNAME
        {
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspUpdPayDefUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "usppayDefget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspUpdPayDefGtAll"; }
        }

        public string UPDATE_LAST_CHECK_NO
        {
            get { return "USP_LastChkUpd"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,terms_code from stpcntrc where 1=1  ");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND rowid = " + parameters[0].ToString());
            return sql.ToString();
        }
        public override string TABLE_NAME
        {
            get { return "stpcntrc"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        #endregion Stored-Procedures
    }
}

  
