using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{  
        /// <summary>
        /// Added by Shrishanshu
        /// For the Report "Update Requition"
        /// Table Used : sturqste 
        /// </summary>

    public class DVOUpdateRequisitionH : DVOBase
    {
        private int _rowid;
        private int _doc_no;
        private string _requestor_code;
        private string _whse_shipto;
        private string _whse_billto;
        private string _po_type;
        private string _request_status;
        private int _request_no;
        private DateTime _request_date;
        private DateTime _requiredDate;
        private string _authorization_code;

        private string _requestor_min;
        private string _requestor_dept;
        private int _gl_acct_no;
        private int _lvl1aprvl;
        private int _lvl1aprvl_by;
        private DateTime _lvl1aprvl_date;
        private string _lvl1aprvl_machinfo;
        private string _keyvalue;
        private string _acct_desc;
        private string _acct_type;
        private string _approvBy;

        private int _procaprvl;
        private int _procaprvl_by;
        private DateTime _procaprvl_date;
        private string _procaprvl_machinfo;
        #region Constructor
        public DVOUpdateRequisitionH()
        {
            _rowid = 0;
            _requestor_code = string.Empty;
            _authorization_code = string.Empty;
            _request_no = 0;
            _doc_no = 0;
            _whse_shipto = string.Empty;
            _requiredDate = Convert.ToDateTime(null);
            _request_date = Convert.ToDateTime(null);
            _po_type = string.Empty;
            _request_status = string.Empty;
            _whse_billto = string.Empty;


            _requestor_min = string.Empty;
            _requestor_dept = string.Empty;
            _gl_acct_no = 0;
            _lvl1aprvl = 0;
            _lvl1aprvl_by = 0;
            _lvl1aprvl_date = Convert.ToDateTime(null);
            _lvl1aprvl_machinfo = string.Empty;
            _keyvalue = string.Empty;
            _acct_desc = string.Empty;
            _acct_type = string.Empty;
            _approvBy = string.Empty;

            _procaprvl = 0;
            _procaprvl_by = 0;
            _procaprvl_date = Convert.ToDateTime(null);
            _procaprvl_machinfo = string.Empty;

        }
        #endregion Constructor

        #region Properties
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        public string requestor_code
        {
            get { return _requestor_code; }
            set { _requestor_code = value; }
        }

        public string authorization_code
        {
            get { return _authorization_code; }
            set { _authorization_code = value; }
        }

        public int request_no
        {
            get { return _request_no; }
            set { _request_no = value; }
        }

        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }

        public string whse_shipto
        {
            get { return _whse_shipto; }
            set { _whse_shipto = value; }
        }
        public string whse_billto
        {
            get { return _whse_billto; }
            set { _whse_billto = value; }
        }

        public DateTime requiredDate
        {
            get { return _requiredDate; }
            set { _requiredDate = value; }
        }

        public DateTime request_date
        {
            get { return _request_date; }
            set { _request_date = value; }
        }

        public string po_type
        {
            get { return _po_type; }
            set { _po_type = value; }
        }

        public string request_status
        {
            get { return _request_status; }
            set { _request_status = value; }
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
        public int gl_acct_no
        {
            get { return _gl_acct_no; }
            set { _gl_acct_no = value; }
        }
        public int lvl1aprvl
        {
            get { return _lvl1aprvl; }
            set { _lvl1aprvl = value; }
        }
        public int lvl1aprvl_by
        {
            get { return _lvl1aprvl_by; }
            set { _lvl1aprvl_by = value; }
        }
        public string lvl1aprvl_machinfo
        {
            get { return _lvl1aprvl_machinfo; }
            set { _lvl1aprvl_machinfo = value; }
        }
        public DateTime lvl1aprvl_date
        {
            get { return _lvl1aprvl_date; }
            set { _lvl1aprvl_date = value; }
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
        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string approvBy
        {
            get { return _approvBy; }
            set { _approvBy = value; }
        }
        public int procaprvl
        {
            get { return _procaprvl; }
            set { _procaprvl = value; }
        }
        public int procaprvl_by
        {
            get { return _procaprvl_by; }
            set { _procaprvl_by = value; }
        }
        public string procaprvl_machinfo
        {
            get { return _procaprvl_machinfo; }
            set { _procaprvl_machinfo = value; }
        }
        public DateTime procaprvl_date
        {
            get { return _procaprvl_date; }
            set { _procaprvl_date = value; }
        }
        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usp_sturqste_ins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_sturqste_upd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usp_sturqste_del"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspReqDefGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "sturqste"; }
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

        public string Get_Last_Doc_No
        {
            get { return "uspgetldocno"; }
        }

        public string UPDATE_APPROVAL_REQUISTION
        {
            get { return "uspapproverequpd"; }
        }
        public string CANCEL_REQUISTION
        {
            get { return "usprequcancelupd"; }
        }

        //used in CPU Approval
        public string UPDATE_CPU_APPROVAL_REQUISTION
        {
            get { return "uspapprovereqcpu"; }
        }
        //Update Status COM
        public string UPDATE_CPU_APPROVAL_REQSTATUS
        {
            get { return "uspreqstausupd"; }
        }
        //Update Status CAN
        public string UPDATE_CPU_APPROVAL_REQSTATUS_CAN
        {
            get { return "uspreqstauscan"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select request_no v_request_no, doc_no v_doc_no, request_date v_request_date, required_date v_required_date,");
            sql.Append(" requestor_code v_requestor_code, authorization_code v_auth_code, request_status v_request_status,");
            sql.Append(" po_type v_po_type,  whse_shipto v_whse_shipto, sturqste.rowid ,");
            sql.Append(" requestor_min,requestor_dept,gl_acct_no,lvl1aprvl ,lvl1aprvl_by,lvl1aprvl_date,lvl1aprvl_machinfo, ");
            sql.Append(" PayrollGLAccounts.keyvalue v_keyvalue ,PayrollGLAccounts.acct_desc v_acct_desc,PayrollGLAccounts.acct_type v_acct_type ");
            sql.Append(" from sturqste,outer PayrollGLAccounts ");
            sql.Append(" where 1=1");
            sql.Append(" AND sturqste.gl_acct_no=PayrollGLAccounts.acct_no ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.requestor_code) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.authorization_code) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) != 0)
                    sql.Append(" AND sturqste.request_no =" + Convert.ToInt32(parameters[2]));

            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) != 0)
                    sql.Append(" AND sturqste.doc_no =" + Convert.ToInt32(parameters[3]));

            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.whse_shipto) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[5] != null)
                if (Convert.ToDateTime(parameters[5]) != Convert.ToDateTime(null))
                    sql.Append(" AND sturqste.required_date = '" + Convert.ToDateTime(parameters[5]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");

            if (parameters[6] != null)
                if (Convert.ToDateTime(parameters[6]) != Convert.ToDateTime(null))
                    sql.Append(" AND sturqste.request_date = '" + Convert.ToDateTime(parameters[6]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");

            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.po_type) LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.request_status) LIKE '" + parameters[8].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[9] != null)
                if (Convert.ToInt32(parameters[9]) > 0)
                    sql.Append(" AND sturqste.rowid =" + Convert.ToInt32(parameters[9]));
            return sql.ToString();
        }

        public string FIND_POTYPE(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(*) Exists ");
            sql.Append(" from sturqste where 1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)//po_type
                    sql.Append(" AND Rtrim(sturqste.po_type) ='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }


        public string GET_DATA_FOR_APPROVAL_BY_CPU(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select request_no , doc_no , request_date , required_date ,");
            sql.Append(" requestor_code , authorization_code , request_status ,");
            sql.Append(" po_type,whse_shipto,sturqste.rowid ,");
            sql.Append(" requestor_min,requestor_dept,gl_acct_no,lvl1aprvl ,lvl1aprvl_by,lvl1aprvl_date,lvl1aprvl_machinfo, ");
            sql.Append(" PayrollGLAccounts.keyvalue  ,PayrollGLAccounts.acct_desc ,PayrollGLAccounts.acct_type ,");
            sql.Append(" secusers.loginid ");
            sql.Append(" from sturqste,secusers,outer PayrollGLAccounts ");
            sql.Append(" where 1=1");
            sql.Append(" AND sturqste.gl_acct_no=PayrollGLAccounts.acct_no ");
            sql.Append(" AND sturqste.lvl1aprvl_by=secusers.userid ");
            sql.Append(" AND sturqste.lvl1aprvl=1 ");
            sql.Append(" AND sturqste.request_status='REQ' ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.requestor_min) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }
        public string GET_DATA_FOR_ENQUIRY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select request_no , doc_no , request_date , required_date ,");
            sql.Append(" requestor_code , authorization_code , request_status ,");
            sql.Append(" po_type,whse_shipto,sturqste.rowid ,");
            sql.Append(" requestor_min,requestor_dept,gl_acct_no,lvl1aprvl ,lvl1aprvl_by,lvl1aprvl_date,lvl1aprvl_machinfo, ");
            sql.Append(" PayrollGLAccounts.keyvalue  ,PayrollGLAccounts.acct_desc ,PayrollGLAccounts.acct_type ,");
            sql.Append(" secusers.loginid ");
            sql.Append(" from sturqste,outer secusers,outer (PayrollGLAccounts) ");
            sql.Append(" where 1=1");
            sql.Append(" AND sturqste.gl_acct_no=PayrollGLAccounts.acct_no ");
            sql.Append(" AND sturqste.lvl1aprvl_by=secusers.userid ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.requestor_min) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.requestor_dept) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }

        public string FIND_REQUISTION_DOC_DETAILS(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT sturqste.request_no , sturqste.doc_no ,sturqste.request_date ,sturqste.required_date ,");
            //sql.Append(" sturqste.requestor_code ,sturqste.authorization_code ,sturqste.request_status ,");
            //sql.Append(" sturqste.po_type , sturqste.whse_billto ,sturqste.whse_shipto ,");
            //sql.Append(" sturqste.requestor_min,sturqste.requestor_dept,sturqste.gl_acct_no ,");
            //sql.Append(" sturqste.lvl1aprvl,sturqste.lvl1aprvl_by,sturqste.lvl1aprvl_date ,");
            //sql.Append(" sturqste.lvl1aprvl_machinfo,sturqste.procaprvl,sturqste.procaprvl_by ,");
            //sql.Append(" sturqste.procaprvl_date,sturqste.procaprvl_machinfo ,");
            //sql.Append(" sturqstd.line_type ,sturqstd.line_stage  ,sturqstd.item_code,sturqstd.desc1 , sturqstd.desc2 ,");
            //sql.Append(" sturqstd.ordr_qty ,sturqstd.cost,sturqstd.reference_no ,sturqstd.lvl1aprv_qty,sturqstd.lvl1aprv_cost ,");
            //sql.Append(" sturqstd.procaprv_qty,sturqstd.procaprv_cost,MasterEmployee.first_name,MasterEmployee.middle_name,MasterEmployee.last_name ,");
            //sql.Append(" PayrollGLAccounts.keyvalue  ,PayrollGLAccounts.acct_desc ,PayrollGLAccounts.acct_type ,s1.loginid v_aprvl1by,s2.loginid v_procby");
            //sql.Append(" FROM sturqste, sturqstd ,outer MasterEmployee,outer PayrollGLAccounts ,outer secusers s1,outer secusers s2");
            //sql.Append(" WHERE sturqste.doc_no = sturqstd.doc_no  ");
            //sql.Append(" AND sturqste.requestor_code= MasterEmployee.empl_code ");
            //sql.Append(" AND sturqste.gl_acct_no=PayrollGLAccounts.acct_no ");
            //sql.Append(" AND sturqste.lvl1aprvl_by=s1.userid ");
            //sql.Append(" AND sturqste.procaprvl_by=s2.userid ");
            //if (parameters[0] != null)
            //    if (Convert.ToInt32(parameters[0]) != 0)
            //        sql.Append(" AND sturqste.request_no =" + Convert.ToInt32(parameters[0]));
            //if (parameters[1] != null)
            //    if (parameters[1].ToString().Trim().Length > 0 && !parameters[1].ToString().Trim().Contains("0001"))
            //        sql.Append(" AND sturqste.request_date ='" + parameters[1].ToString().Replace("'", "''") + "'"); //Convert.ToDateTime(parameters[1]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");
            //if (parameters[2] != null)
            //    if (parameters[2].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(sturqste.requestor_code) ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[3] != null)
            //    if (parameters[3].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(sturqste.request_status) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[4] != null)
            //    if (parameters[4].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(sturqste.authorization_code) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[5] != null)
            //    if (parameters[5].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(sturqste.whse_billto) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            //if (parameters[6] != null)
            //    if (parameters[6].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(sturqste.whse_shipto) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            //sql.Append(" ORDER BY sturqste.doc_no");


            sql.Append(" select  stureqste.doc_no,stureqste.req_typ_code,stureqste.whse_shipto,stureqste.whse_billto,stureqste.requester_id,");
            sql.Append(" stureqste.status,stureqste.request_date,stureqste.required_date,stureqste.gl_acct_no,stureqste.department,");
            sql.Append(" stureqste.vend_code,stureqste.cpu_po_no,stureqste.reg_po_no,stureqste.approved_by,stureqste.approval_date,");
            sql.Append(" stureqste.processed_date,stureqste.ref_req_doc_no,stureqste.dprt_doc_ref_no,");

            sql.Append(" stureqstd.line_no,stureqstd.line_type,stureqstd.line_stage,stureqstd.item_code,stureqstd.item_cat_id,");
            sql.Append(" stureqstd.description,stureqstd.unit,stureqstd.ordr_quantity,stureqstd.aprvd_quantity,stureqstd.cost,");
            sql.Append(" stureqstd.aprvd_cost,stureqstd.whse_shipto,stureqstd.whse_billto,");

            sql.Append(" s1.firstname reqFname ,s1.lastname reqLname,s2.firstname aprFname,s2.lastname aprFname,");
            sql.Append(" stiinvtr.desc1,stiinvtr.desc2,stuitmcat.itemcat_name,stuitmcat.itemcat_desc");

            sql.Append(" from stureqste,stureqstd,secusers s1,outer ");
            sql.Append(" secusers s2,outer stiinvtr,outer stuitmcat");
            sql.Append(" where 1=1");
            sql.Append(" stureqste.doc_no=stureqstd.doc_no");
            sql.Append(" and stureqste.requester_id=s1.userid");
            sql.Append(" and stureqste.approved_by=s2.userid");
            sql.Append(" and stureqstd.item_code=stiinvtr.item_code");
            sql.Append(" and stureqstd.item_cat_id=stuitmcat.itemcat_id");
            
            sql.Append(" and s1.usrmin ='"+parameters[0].ToString()+"'");
            sql.Append(" and s1.usrdept ='" + parameters[1].ToString() + "'"); 







            return sql.ToString();
        }

        #endregion Stored-Procedures
      
    }
}


    
