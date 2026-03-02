using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa on 24/9/09
    public class DVOOrderTypeStuotypd:DVOBase
    {
        private int _rowid;
        private string _po_type;
        private string _vend_code;
        private string _vend_code_edit;
        private string _terms_code;
        private string _terms_code_edit;
        private int    _glaccount;
        private string _glaccount_edit;
        private string _po_status;
        private string _po_status_edit;
        private string _po_stage;
        private string _po_stage_edit;
        private string _vend_po_req;
        private string _vend_po_edit;
        private string _buyer_code;
        private string _buyer_code_edit;
        private string _ship_via;
        private string _ship_via_edit;
        private string _whse_shipto;
        private string _whse_shipto_edit;
        private string _podate_current;
        private string _podate_edit;
        private string _reqdate_current;
        private string _reqdate_edit;


        private decimal _misc_amt;
        private string _misc_amt_edit;
        private decimal _fright_amt;
        private string _fright_amt_edit;


        private string _line_type;
        private string _line_type_edit;
        private string _line_stage;
        private string _line_stage_edit;
        private string _add_new_item;
        private string _includ_cpu_items;
        private string _add_in_catalog;
        private string _purch_unit;
        private string _purch_unit_edit;

        private string _mtaxg_code;
        private string _mtaxg_code_edit;
        private string _frgt_taxcode;
        private string _frgt_taxcode_edit;
        private string _misc_taxcode;
        private string _misc_taxcode_edit;

        private string _acct_type;
        private string _keyvalue;
        private string _acct_desc;




        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        private string _podt_reqdt_same;


        public DVOOrderTypeStuotypd()
        {
            _rowid = 0;
            _po_type = string.Empty;
            _vend_code = string.Empty;
            _vend_code_edit = string.Empty;
            _terms_code = string.Empty;
            _terms_code_edit = string.Empty;
            _glaccount = 0;
            _glaccount_edit = string.Empty;
            _po_status = string.Empty;
            _po_status_edit = string.Empty;
            _po_stage = string.Empty;
            _po_stage_edit = string.Empty;
            _vend_po_req = string.Empty;
            _vend_po_edit = string.Empty;
            _buyer_code = string.Empty;
            _buyer_code_edit = string.Empty;
            _ship_via = string.Empty;
            _ship_via_edit = string.Empty;
            _whse_shipto = string.Empty;
            _whse_shipto_edit = string.Empty;
            _podate_current = string.Empty;
            _podate_edit = string.Empty;
            _reqdate_current = string.Empty;
            _reqdate_edit = string.Empty;
            _misc_amt = 0;
            _misc_amt_edit = string.Empty;
            _fright_amt = 0;
            _fright_amt_edit = string.Empty;
            _line_type = string.Empty;
            _line_type_edit = string.Empty;
            _line_stage = string.Empty;
            _line_stage_edit = string.Empty;
            _add_new_item = string.Empty;
            _includ_cpu_items = string.Empty;
            _add_in_catalog = string.Empty;
            _purch_unit = string.Empty;
            _purch_unit_edit = string.Empty;

            _mtaxg_code = string.Empty;
            _mtaxg_code_edit = string.Empty;
            _frgt_taxcode = string.Empty;
            _frgt_taxcode_edit = string.Empty;
            _misc_taxcode = string.Empty;
            _misc_taxcode_edit = string.Empty;


            _acct_type=string.Empty;
            _keyvalue=string.Empty;
            _acct_desc=string.Empty;

            _insertby = -1;
            _insertdate = string.Empty;
            _insertmachineinfo = "App";
            _updateby = -1;
            _updatedate = string.Empty;
            _updatemachineinfo = "App";

            _podt_reqdt_same = string.Empty;

        }

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string po_type
        {
            get { return _po_type; }
            set { _po_type = value; }
        }
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string vend_code_edit
        {
            get { return _vend_code_edit; }
            set { _vend_code_edit = value; }
        }
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }
        public string terms_code_edit
        {
            get { return _terms_code_edit; }
            set { _terms_code_edit = value; }
        }
        public int glaccount
        {
            get { return _glaccount; }
            set { _glaccount = value; }
        }
        public string glaccount_edit
        {
            get { return _glaccount_edit; }
            set { _glaccount_edit = value; }
        }
            public string po_status
        {
            get { return _po_status; }
            set { _po_status = value; }
        }
        public string po_status_edit
        {
            get { return _po_status_edit; }
            set { _po_status_edit = value; }
        }
        public string po_stage
        {
            get { return _po_stage; }
            set { _po_stage = value; }
        }
        public string po_stage_edit
        {
            get { return _po_stage_edit; }
            set { _po_stage_edit = value; }
        }
        public string vend_po_req
        {
            get { return _vend_po_req; }
            set { _vend_po_req = value; }
        }
        public string vend_po_edit
        {
            get { return _vend_po_edit; }
            set { _vend_po_edit = value; }
        }
        public string buyer_code
        {
            get { return _buyer_code; }
            set { _buyer_code = value; }
        }

        public string buyer_code_edit
        {
            get { return _buyer_code_edit; }
            set { _buyer_code_edit= value; }
        }

        public string ship_via
        {
            get { return _ship_via; }
            set { _ship_via = value; }
        }
        public string ship_via_edit
        {
            get { return _ship_via_edit; }
            set { _ship_via_edit = value; }
        }
       
        public string whse_shipto
        {
            get { return _whse_shipto; }
            set { _whse_shipto = value; }
        }
        public string whse_shipto_edit
        {
            get { return _whse_shipto_edit; }
            set { _whse_shipto_edit = value; }
        }
        public string podate_current
        {
            get { return _podate_current; }
            set { _podate_current = value; }
        }
        public string podate_edit
        {
            get { return _podate_edit; }
            set { _podate_edit = value; }
        }
        public string reqdate_current
        {
            get { return _reqdate_current; }
            set { _reqdate_current = value; }
        }
        public string reqdate_edit
        {
            get { return _reqdate_edit; }
            set { _reqdate_edit = value; }
        }
        public decimal  misc_amt
        {
            get { return _misc_amt; }
            set { _misc_amt = value; }
        }
        public string misc_amt_edit
        {
            get { return _misc_amt_edit; }
            set { _misc_amt_edit = value; }
        }
        public decimal  fright_amt
        {
            get { return _fright_amt; }
            set { _fright_amt = value; }
        }
        public string fright_amt_edit
        {
            get { return _fright_amt_edit; }
            set { _fright_amt_edit = value; }
        }
        public string line_type
        {
            get { return _line_type; }
            set { _line_type = value; }
        }

        public string line_type_edit
        {
            get { return _line_type_edit; }
            set { _line_type_edit = value; }
        }
        public string line_stage
        {
            get { return _line_stage; }
            set { _line_stage = value; }
        }
        public string line_stage_edit
        {
            get { return _line_stage_edit; }
            set { _line_stage_edit = value; }
        }
        public string add_new_item
        {
            get { return _add_new_item; }
            set { _add_new_item = value; }
        }
        public string includ_cpu_items
        {
            get { return _includ_cpu_items; }
            set { _includ_cpu_items = value; }
        }
        public string add_in_catalog
        {
            get { return _add_in_catalog; }
            set { _add_in_catalog = value; }
        }
        public string purch_unit
        {
            get { return _purch_unit; }
            set { _purch_unit = value; }
        }
        public string purch_unit_edit
        {
            get { return _purch_unit_edit; }
            set { _purch_unit_edit = value; }
        }

        public string mtaxg_code
        {
            get { return _mtaxg_code; }
            set { _mtaxg_code = value; }
        }
        public string mtaxg_code_edit
        {
            get { return _mtaxg_code_edit; }
            set { _mtaxg_code_edit = value; }
        }

        public string frgt_taxcode
        {
            get { return _frgt_taxcode; }
            set { _frgt_taxcode = value; }
        }

        public string frgt_taxcode_edit
        {
            get { return _frgt_taxcode_edit; }
            set { _frgt_taxcode_edit = value; }
        }
        public string misc_taxcode
        {
            get { return _misc_taxcode; }
            set { _misc_taxcode = value; }
        }

        public string misc_taxcode_edit
        {
            get { return _misc_taxcode_edit; }
            set { _misc_taxcode_edit = value; }
        }

        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value; }
        }
      
           

        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }

        public string podt_reqdt_same
        {
            get { return _podt_reqdt_same; }
            set { _podt_reqdt_same = value; }
        }
             
        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspordstuotypdins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspordstuotypdupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspordstuotypddel"; }
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
            get { return "stuotypd"; }
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
            sql.Append(" select  stuotypd.po_type ,stuotypd.vend_code,stuotypd.vend_code_edit,stuotypd.terms_code ,stuotypd.terms_code_edit, ");
            sql.Append(" stuotypd.glaccount,stuotypd.glaccount_edit ,stuotypd.po_status ,stuotypd.po_status_edit,stuotypd.po_stage ,stuotypd.po_stage_edit,");
            sql.Append(" stuotypd.vend_po_req,vend_po_edit,stuotypd.buyer_code,stuotypd.buyer_code_edit,stuotypd.ship_via,stuotypd.ship_via_edit,");
            sql.Append(" stuotypd.whse_shipto,stuotypd.whse_shipto_edit,stuotypd.podate_current,stuotypd.podate_edit,stuotypd.reqdate_current,");
            sql.Append(" stuotypd.reqdate_edit,stuotypd.misc_amt,stuotypd.misc_amt_edit,stuotypd.fright_amt,stuotypd.fright_amt_edit ,");
            sql.Append(" stuotypd.line_type,stuotypd.line_type_edit,stuotypd.line_stage,stuotypd.line_stage_edit,");
            sql.Append(" stuotypd.add_new_item ,stuotypd.includ_cpu_items,stuotypd.add_in_catalog ,");
            sql.Append(" stuotypd.purch_unit,stuotypd.purch_unit_edit,stuotypd.mtaxg_code,stuotypd.mtaxg_code_edit ,");

            sql.Append(" stuotypd.frgt_taxcode,stuotypd.frgt_taxcode_edit,stuotypd.misc_taxcode ,stuotypd.misc_taxcode_edit,stuotypd.rowid ,");
            sql.Append(" PayrollGLAccounts.acct_no,PayrollGLAccounts.acct_type,PayrollGLAccounts.acct_desc,PayrollGLAccounts.keyvalue,stuotypd.podt_reqdt_same ");
            sql.Append(" from stuotypd,outer PayrollGLAccounts where stuotypd.glaccount=PayrollGLAccounts.acct_no");

            if (parameters[0].ToString() != string.Empty)
                sql.Append(" AND Rtrim(stuotypd.po_type) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
           
           
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
           
          
            

   
}
