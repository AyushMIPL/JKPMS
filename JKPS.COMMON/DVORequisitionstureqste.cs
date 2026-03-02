using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVORequisitionstureqste : DVOBase
    {
       private int _rowid;
       private int _doc_no;
       private string _req_typ_code;
       private string _whse_shipto;
       private string _whse_billto;
       private int _requester_id; 
       private string _status; 
       private string _initial_status; 
       private string _request_date; 
       private string _required_date; 
       private int _gl_acct_no; 
       private string _department;
       private string _vend_code; 
       private string _cpu_po_no; 
       private string _reg_po_no; 
       private int _approved_by; 
       private string _approval_date;
       private string _processed_date; 
       private int _ref_req_doc_no;
       private string _cpu_notes; 
       private int _cpu_handled_by; 
       private string _dprt_doc_ref_no; 
       private int _insertby;
       private string _insertdate; 
       private string _insertmachineinfo;
       private int _updateby;
       private string _updatedate;
       private string _updatemachineinfo;

       private string _keyvalue;
       private string _acct_desc;
       private string _acct_type;
        private string _requestor;
        private string _requestor_min;
        private string _requestor_dept;
        private string _bus_name;

        public DVORequisitionstureqste()
        {

            _rowid = 0;
            _doc_no = 0;
            _req_typ_code = string.Empty;
            _whse_shipto = string.Empty;
            _whse_billto = string.Empty;
            _requester_id = 0;
            _status = string.Empty;
            _initial_status = string.Empty;
            _request_date = string.Empty;
            _required_date = string.Empty;
            _gl_acct_no = 0;
            _department = string.Empty;
            _vend_code = string.Empty;
            _cpu_po_no = string.Empty;
            _reg_po_no = string.Empty;
            _approved_by = 0;
            _approval_date = string.Empty;
            _processed_date = string.Empty;
            _ref_req_doc_no = 0;
            _cpu_notes = string.Empty;
            _cpu_handled_by = 0;
            _dprt_doc_ref_no = string.Empty;
            _insertby = 0;
            _insertdate = string.Empty;
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = string.Empty;
            _updatemachineinfo = string.Empty;

            _keyvalue = string.Empty;
            _acct_desc = string.Empty;
            _acct_type = string.Empty;
            _requestor = string.Empty;
            _requestor_min = string.Empty;
            _requestor_dept = string.Empty;
            _bus_name = string.Empty;
        }


        public int rowid 
          { get { return _rowid; } set { _rowid = value; } }
        public int doc_no 
          { get { return _doc_no; } set { _doc_no = value; } }
        public string req_typ_code 
          { get { return _req_typ_code; } set { _req_typ_code = value; } }
        public string whse_shipto 
          { get { return _whse_shipto; } set { _whse_shipto = value; } }
        public string whse_billto 
          { get { return _whse_billto; } set { _whse_billto = value; } }
        public int requester_id
        { get { return _requester_id; } set { _requester_id = value; } }
        public string status
        { get { return _status; } set { _status = value; } }
        public string initial_status 
          { get { return _initial_status; } set { _initial_status = value; } }
        public string request_date 
          { get { return _request_date; } set { _request_date = value; } }
        public string required_date 
          { get { return _required_date; } set { _required_date = value; } }
        public int gl_acct_no 
          { get { return _gl_acct_no; } set { _gl_acct_no = value; } }
        public string department 
          { get { return _department; } set { _department = value; } }
        public string vend_code 
          { get { return _vend_code; } set { _vend_code = value; } }
        public string cpu_po_no 
          { get { return _cpu_po_no; } set { _cpu_po_no = value; } }
        public string reg_po_no 
          { get { return _reg_po_no; } set { _reg_po_no = value; } }
        public int approved_by 
          { get { return _approved_by; } set { _approved_by = value; } }
        public string approval_date 
          { get { return _approval_date; } set { _approval_date = value; } }
        public string processed_date 
          { get { return _processed_date; } set { _processed_date = value; } }
        public int ref_req_doc_no 
          { get { return _ref_req_doc_no; } set { _ref_req_doc_no = value; } }
        public string cpu_notes 
          { get { return _cpu_notes; } set { _cpu_notes = value; } }
        public int cpu_handled_by 
          { get { return _cpu_handled_by; } set { _cpu_handled_by = value; } }
        public string dprt_doc_ref_no 
          { get { return _dprt_doc_ref_no; } set { _dprt_doc_ref_no = value; } }
        public int insertby 
          { get { return _insertby; } set { _insertby = value; } }
        public string insertdate 
          { get { return _insertdate; } set { _insertdate = value; } }
        public string insertmachineinfo 
          { get { return _insertmachineinfo; } set { _insertmachineinfo = value; } }
        public int updateby 
          { get { return _updateby; } set { _updateby = value; } }
        public string updatedate 
          { get { return _updatedate; } set { _updatedate = value; } }
        public string updatemachineinfo 
          { get { return _updatemachineinfo; } set { _updatemachineinfo = value; } }

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
        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string requestor
        {
            get { return _requestor; }
            set { _requestor = value; }
        }
        public string requestor_min
        {
            get { return _requestor_min; }
            set { _requestor_min = value; }
        }
        public string requestor_dept
        {
            get { return _requestor_dept; }
            set { _requestor_dept = value; }
        }
        public string bus_name
        {
            get { return _bus_name; }
            set { _bus_name = value; }
        }

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usprequihins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usprequihupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usprequidel"; }
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
            get { return "stureqste"; }
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

        public string UPDATE_APPROVAL_REQUISTION
        {
            get { return "uspaprvrequpd"; }
        }
        public string CANCEL_REQUISTION
        {
            get { return "uspstureqstedel"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {           
            StringBuilder sql = new StringBuilder();
            sql.Append("select stureqste.rowid,stureqste.doc_no,stureqste.req_typ_code,stureqste.whse_shipto,stureqste.whse_billto,stureqste.requester_id,stureqste.status,stureqste.initial_status,");
            sql.Append("stureqste.request_date,stureqste.required_date,stureqste.gl_acct_no,stureqste.department,stureqste.vend_code,stureqste.cpu_po_no,stureqste.reg_po_no,");
            sql.Append("stureqste.approved_by,stureqste.approval_date,stureqste.processed_date,stureqste.ref_req_doc_no,stureqste.cpu_notes,stureqste.cpu_handled_by,");
            sql.Append("stureqste.dprt_doc_ref_no,stureqste.insertby,stureqste.insertdate,stureqste.insertmachineinfo,stureqste.updateby,stureqste.updatedate,");
            sql.Append("stureqste.updatemachineinfo,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,PayrollGLAccounts.acct_type,secusers.loginid ");
            sql.Append(" from stureqste,outer PayrollGLAccounts ,outer secusers ");
            sql.Append(" where stureqste.requester_id = secusers.userid ");
            sql.Append(" AND stureqste.gl_acct_no=PayrollGLAccounts.acct_no ");  

            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append(" AND stureqste.requester_id=" +Convert.ToInt32(parameters[0]));
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) != 0)
                    sql.Append(" AND stureqste.doc_no =" + Convert.ToInt32(parameters[1]));
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(PayrollGLAccounts.acct_type) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(PayrollGLAccounts.keyvalue) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stureqste.vend_code) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stureqste.whse_shipto) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stureqste.req_typ_code) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stureqste.status) LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[8] != null)
                if (Convert.ToInt32(parameters[8]) > 0)
                    sql.Append(" AND stureqste.rowid =" + Convert.ToInt32(parameters[8]));
            if (parameters[9] != null)
                if (Convert.ToDateTime(parameters[9]) != Convert.ToDateTime(null))
                    sql.Append(" AND stureqste.required_date = '" + Convert.ToDateTime(parameters[9]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (Convert.ToDateTime(parameters[10]) != Convert.ToDateTime(null))
                    sql.Append(" AND stureqste.request_date = '" + Convert.ToDateTime(parameters[10]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");

            //Added By Rahul Jain  
            if (parameters[11] != null)
                if (parameters[11].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(secusers.loginid) = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[12] != null)
                if (parameters[12].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stureqste.initial_status) = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[13] != null)
                if (parameters[13].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(stureqste.req_typ_code) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[14] != null)
                if (parameters[14].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(secusers.usrmin) = '" + parameters[14].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[15] != null)
                if (parameters[15].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(secusers.usrdept) = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }

        public string GET_PRINTREQ(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select  stureqste.doc_no,stureqste.req_typ_code,stureqste.whse_shipto,stureqste.whse_billto,stureqste.requester_id,");
            sql.Append(" stureqste.status,stureqste.request_date,stureqste.required_date,stureqste.gl_acct_no,stureqste.department,");
            sql.Append(" stureqste.vend_code,stureqste.cpu_po_no,stureqste.reg_po_no,stureqste.approved_by,stureqste.approval_date,");
            sql.Append(" stureqste.processed_date,stureqste.ref_req_doc_no,stureqste.dprt_doc_ref_no,");

            sql.Append(" stureqstd.line_no,stureqstd.line_type,stureqstd.line_stage,stureqstd.item_code,stureqstd.item_cat_id,");
            sql.Append(" stureqstd.description,stureqstd.unit,stureqstd.ordr_quantity,stureqstd.aprvd_quantity,stureqstd.cost,");
            sql.Append(" stureqstd.aprvd_cost,stureqstd.whse_shipto,stureqstd.whse_billto,");

            sql.Append(" s1.firstname reqFname ,s1.lastname reqLname,s2.firstname aprFname,s2.lastname aprLname,");
            sql.Append(" stiinvtr.desc1,stiinvtr.desc2,stuitmcat.itemcat_name,stuitmcat.itemcat_desc,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc ,stpvendr.bus_name");

            sql.Append(" from stureqste,stureqstd,secusers s1,outer ");
            sql.Append(" secusers s2,outer stiinvtr,outer stuitmcat,outer PayrollGLAccounts,outer stpvendr");
            sql.Append(" where stureqste.doc_no=stureqstd.doc_no");
            sql.Append(" and stureqste.requester_id=s1.userid");
            sql.Append(" and stureqste.approved_by=s2.userid");
            sql.Append(" and stureqstd.item_code=stiinvtr.item_code");
            sql.Append(" and stureqstd.item_cat_id=stuitmcat.itemcat_id");
            sql.Append(" and stureqste.gl_acct_no=PayrollGLAccounts.acct_no");
            sql.Append(" and stureqste.vend_code=stpvendr.vend_code");


            sql.Append(" and s1.usrmin ='" + parameters[0].ToString() + "'");
            sql.Append(" and s1.usrdept ='" + parameters[1].ToString() + "'");

            if (parameters[2] != null && parameters[2].ToString().Trim().Length > 0 && Convert.ToInt32(parameters[2])>0)
                sql.Append(" and stureqste.requester_id=" + Convert.ToInt32(parameters[2]));

            if(parameters[3]!=null && parameters[3].ToString().Trim().Length>0 && parameters[3].ToString()!="01/01/1900" && parameters[3].ToString()!="01/01/0001")
                sql.Append(" and stureqste.request_date='"+parameters[3].ToString()+"'");

            if (parameters[4] != null && parameters[4].ToString().Trim().Length > 0)
                sql.Append(" and stureqste.status='" + parameters[4].ToString() + "'");

            if (parameters[5] != null && parameters[5].ToString().Trim().Length > 0)
                sql.Append(" and stureqste.dprt_doc_ref_no='" + parameters[5].ToString() + "'");

            return sql.ToString();
        }

     

       

        #endregion Stored-Procedures

    }
}
