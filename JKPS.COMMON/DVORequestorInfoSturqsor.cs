using System;
using System.Collections.Generic;
using System.Text;



namespace JKPS.COMMON
{
    public class DVORequestorInfoSturqsor : DVOBase
    {
        private int _rowid;
        private string _requiredDate;
        /// <summary>
        /// Added by Shrishanshu
        /// For the Report "Print Requition"
        /// Table Used : sturqste , sturqstd , sturqsor
        /// </summary>
        private int _request_no;
        private string _request_date;
        private string _request_status;
        private string _whse_billto;
        private string _requestor_code;
        //End of Modification
        private string _request_desc;
        private string _authorization_code;
        private int _approval_level;
        private string _whse_shipto;
        private string _whse_code;
        private string _whsedesc;
        private string _department;
        private string _po_type;
        public DVORequestorInfoSturqsor()
        {

            _rowid = 0;
            _requestor_code = string.Empty;
            _request_desc = string.Empty;
            _authorization_code = string.Empty;
            _approval_level = 0;
            _whse_shipto = string.Empty;
            _whse_code = string.Empty;
            _whsedesc = string.Empty;
            _department = string.Empty;
            _po_type = string.Empty;
            _request_no = 0;
            _request_date = string.Empty;
            _requiredDate = string.Empty;
            _request_status = string.Empty;
            _whse_billto = string.Empty;
        }

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        private int _doc_no;
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
      

        public string requestor_code
        {
            get { return _requestor_code; }
            set { _requestor_code = value; }
        }

        public string request_desc
        {
            get { return _request_desc; }
            set { _request_desc = value; }
        }
        public string authorization_code
        {
            get { return _authorization_code; }
            set { _authorization_code = value; }
        }

        public int approval_level
        {
            get { return _approval_level; }
            set { _approval_level = value; }
        }
        public string whse_shipto
        {
            get { return _whse_shipto; }
            set { _whse_shipto = value; }
        }

        public string whse_code
        {
            get { return _whse_code; }
            set { _whse_code = value; }
        }
        public string whsedesc
        {
            get { return _whsedesc; }
            set { _whsedesc = value; }
        }

        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public int request_no
        {
            get { return _request_no; }
            set { _request_no = value; }
        }
        public string request_date
        {
            get { return _request_date; }
            set { _request_date = value; }
        }
        public string requiredDate
        {
            get { return _requiredDate; }
            set { _requiredDate = value; }
        }
        public string request_status
        {
            get { return _request_status; }
            set { _request_status = value; }
        }
        public string whse_billto
        {
            get { return _whse_billto; }
            set { _whse_billto = value; }
        }
        public string po_type
        {
            get { return _po_type; }
            set { _po_type = value; }
        }
        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspRequestorDefIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspRequestorDefupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspRequestorDefdel"; }
        }

        public string INSERT_HEADER_UPDATE_REQUISITION
        {
            get { return "usp_sturqste_ins"; }
        }
        public string UPDATE_HEADER_UPDATE_REQUISITION
        {
            get { return "usp_sturqste_upd"; }
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
            get { return "sturqsor"; }
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
            sql.Append(" select sturqsor.rowid,sturqsor.requestor_code ,sturqsor.request_desc,");
            sql.Append(" sturqsor.whse_shipto,sturqsor.approval_level,");
            sql.Append(" stiwhser.description ");
            sql.Append(" from sturqsor,outer stiwhser");
            sql.Append(" where sturqsor.whse_shipto =stiwhser.whse_code ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND requestor_code= '" + parameters[0].ToString().Trim()+"'");


            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(request_desc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");

            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND approval_level = " + parameters[2].ToString());

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(whse_shipto) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND sturqsor.rowid = " + parameters[4].ToString());

            return sql.ToString();
        }

        public  string FIND_RptData(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();


            sql.Append("select sturqste.request_no v_request_no, sturqste.doc_no v_doc_no,sturqste.request_date v_request_date,sturqste.required_date v_required_date,");
            sql.Append("sturqste.requestor_code v_requestor_code,sturqsor.request_desc v_request_desc,sturqste.authorization_code v_auth_code,sturqste.request_status v_request_status,");
            sql.Append("sturqste.po_type v_po_type, sturqste.whse_billto v_whse_billto,sturqste.whse_shipto v_whse_shipto,sturqstd.line_type v_line_type,sturqstd.line_stage v_line_stage,");
            sql.Append("sturqstd.item_code v_item_code,sturqstd.reference_no v_reference_no,sturqstd.ordr_qty v_ordr_qty, sturqstd.desc1 v_desc1, sturqstd.desc2 v_desc2");
            sql.Append(" from sturqste, sturqstd, outer sturqsor");  
            sql.Append(" where sturqste.request_no = sturqstd.request_no and sturqste.requestor_code = sturqsor.requestor_code ");
           
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0])!=0)
                    sql.Append(" AND sturqste.request_no =" + Convert.ToInt32(parameters[0]));
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0 && parameters[1].ToString().Trim()!="01/01/1900")
                    sql.Append(" AND sturqste.request_date = " + "'" + parameters[1].ToString().Replace("'", "''") + "'"); //Convert.ToDateTime(parameters[1]).ToString("MM/dd/yyyy").Replace("'", "''") + "'");

            //if (parameters[1] != null)
            //     sql.Append(" AND sturqste.request_date =" + (parameters[1]));

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.requestor_code) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.request_status) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.authorization_code) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.whse_billto) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(sturqste.whse_shipto) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            sql.Append(" order by sturqste.request_no");



            return sql.ToString();
        }

        public string FIND_RequisitionHeader(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();


            sql.Append("select sturqste.request_no v_request_no, sturqste.doc_no v_doc_no,sturqste.request_date v_request_date,sturqste.required_date v_required_date,");
            sql.Append("sturqste.requestor_code v_requestor_code,sturqste.authorization_code v_auth_code,sturqste.request_status v_request_status,");
            sql.Append("sturqste.po_type v_po_type, sturqste.whse_shipto v_whse_shipto,");
            sql.Append(" from sturqste");
            sql.Append(" where 1=1");

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
           
                
          
            
           
           return sql.ToString();
            
        }
        
#endregion Stored-Procedures
    }
}

/*
 * DEFINE v_request_no integer                                 
DEFINE v_doc_no integer                                 
DEFINE v_request_date date                                    
DEFINE v_required_date date                                    
DEFINE v_requestor_code char(6)                                 
DEFINE v_authorization_code  char(6)                                 
DEFINE v_request_status char(3)                                 
DEFINE v_po_type  char(3)                                 
DEFINE v_whse_billto  char(10)                                
DEFINE v_whse_shipto char(10)                                
DEFINE v_request_desc char(30)
DEFINE v_line_type  char(3)
DEFINE v_line_stage char(3)
DEFINE v_item_code char(20)
DEFINE v_reference_no  char(13)
DEFINE v_ordr_qty  decimal(12)
DEFINE v_desc1  char(30)
DEFINE v_desc2 char(30)
 */