using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOUpdateVoidChecks : DVOBase
    {
        //Added by sunil Pahwa

        private int _Rowid;
        private int _doc_no;
        private int _chk_doc_no;
        private string _posted;
        private string _ok_to_posted;
        private string _void_date;


        //stxckrgd

        private string _orig_journal;
        private int _docu_no;
        private int _acct_no;
        private string _department;
        private string _inv_chk_no;
        private decimal _amount;
        private string _debit_credit;
        private string _reconciled;
        private string _chk_voided;


        private string _doc_date;
        private string _doc_desc;
        // private string _doc_desc;
        private int _post_no;
        private string _post_date;
        private string _ref_code;


        private string _check_no;
        private decimal _amt;

        private string _vend_code;
        private string _bus_name;

        public DVOUpdateVoidChecks()
        {
            _Rowid = 0;
            _doc_no = 0;
            _chk_doc_no = 0;
            _posted = string.Empty;
            _ok_to_posted = string.Empty;
            _void_date = string.Empty;

            _orig_journal = string.Empty;
            _docu_no = 0;
            _acct_no = 0;
            _department = string.Empty;
            _inv_chk_no = string.Empty;
            _amount = 0.0M;
            _debit_credit = string.Empty;
            _reconciled = string.Empty;
            _chk_voided = string.Empty;


            _doc_date = string.Empty;
            _doc_desc = string.Empty;
            _doc_desc = string.Empty;
            _post_no = 0;
            _post_date = string.Empty;
            _ref_code = string.Empty;

            _check_no = string.Empty;
            _amt = 0.0M;

            _vend_code=string.Empty ;
            _bus_name=string.Empty ;



        }

        public int rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }

        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }

        public int chk_doc_no
        {
            get { return _chk_doc_no; }
            set { _chk_doc_no = value; }
        }

        public string posted
        {
            get { return _posted; }
            set { _posted = value; }
        }
        public string void_date
        {
            get { return _void_date; }
            set { _void_date = value; }
        }
         
        public string ok_to_posted
        {
            get { return _ok_to_posted; }
            set { _ok_to_posted = value; }
        }

        //***************


        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }



        public int docu_no
        {
            get { return _docu_no; }
            set { _docu_no = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }

        public string department
        {
            get { return _department; }
            set { _department = value; }
        }

        public string inv_chk_no
        {
            get { return _inv_chk_no; }
            set { _inv_chk_no = value; }
        }


        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string debit_credit
        {
            get { return _debit_credit; }
            set { _debit_credit = value; }
        }
        public string reconciled
        {
            get { return _reconciled; }
            set { _reconciled = value; }
        }

        public string chk_voided
        {
            get { return _chk_voided; }
            set { _chk_voided = value; }
        }
        //*********

        public string doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }


        public string doc_desc
        {
            get { return _doc_desc; }
            set { _doc_desc = value; }
        }

        public int post_no
        {
            get { return _post_no; }
            set { _post_no = value; }
        }
        public string post_date
        {
            get { return _post_date; }
            set { _post_date = value; }
        }
        public string ref_code
        {
            get { return _ref_code; }
            set { _ref_code = value; }
        }


        public string check_no
        {
            get { return _check_no; }
            set { _check_no = value; }
        }


        public decimal amt
        {
            get { return _amt; }
            set { _amt = value; }
        }


        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }

        public string bus_name
        {
            get { return _bus_name ; }
            set { _bus_name  = value; }
        }

        public string FIND_VOID_CHK_HEAD
        {
            get { return "uspUpdVoidChkGet"; }
        }

        public string FIND_VOID_CHK_HEAD1
        {
            get { return "uspUpdVoidChkGet1"; }//uspupdvoidchkget1
        }




        //******************
        public string GET_VOID_CHK_HEAD
        {
            get { return "uspUpdVoidChkGet"; }
        }


        public string GET_VOID_CHK_DETAIL
        {
            get { return "uspUpdVChkGetDtl1"; }//uspupdvchkgetdtl1
        }

        public string GET_VOID_CHK_DETAIL1
        {
            get { return "uspUpdVChkGetDtl2"; }//uspupdvchkgetdtl2
        }

        public string GET_VOID_CHK_VENDER_DETAIL
        {
            get { return "uspUpdVChkGetDtl3"; }//uspupdvchkgetdtl3
        }
        //Added by Sarvjeet On 24 Feb. 2009
        public string GET_VOIDCHECKLIST
        {
            get { return "uspvoidckget"; }
        }
        public string GETDOCNO
        {
            get { return "uspdocnoget"; }
        }
        public string GEBUSNAME
        {
            get { return "uspbusnameget"; }
        }
        public string GET_XTRANR
        {
            get { return "UspVdCkxtranrGet"; }
        }
        public string GET_inv_chk_no
        {
            get { return "uspinvcknoget"; }
        }
        public string GET_APFIELDS
        {
            get { return "UspVdCkAPFieldsGet"; }
        }
        public string GET_GLFIELDS
        {
            get { return "UspVdCkGLFieldsGet"; }
        }

        public string UPD_VOIDR_PST
        {
            get { return "uspstpvoidrupd"; }
        }
        public string UPD_VOIDR_CK
        {
            get { return "uspstpvoidrupd1"; }
        }
        public string UPD_XKRGD
        {
            get { return "uspstxckrgdupd"; }
        }
        public string GET_VEND_NAME
        {
            get { return "uspvend_nameget"; }
        }
        //*************



        public override string INSERT_SPNAME
        {
            get { return "uspUpdvoidChkIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspupdVoidCHkupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }//uspUpdvoidChkDel
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }


        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "stpvoidr"; }
        }

        public override int UNIQUE_ID
        {
            get { return  _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public  string UPDATE_WHEN_DELETE
        {
            get { return "uspUpdvoidChkDel"; }
        }
      

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append("select *,rowid from stpvoidr where posted<>'C'");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND chk_doc_no= " + parameters[0].ToString());

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(posted) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND doc_no= " + parameters[2].ToString());

            if (parameters[3].ToString() != string.Empty)
                sql.Append(" AND void_date = '" + parameters[3].ToString() + "'");

            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND rowid= " + parameters[4].ToString());

            sql.Append(" order by chk_doc_no");



            return sql.ToString();
        }

    }
}
