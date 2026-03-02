using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Added by Sunil Pahwa
    //**********************
    public class DVORequisitionTypeStureqtyp:DVOBase 
    {
        private int _req_typ_id;//  serial  
        private string _req_typ_code;//  char(3) 
        private string _req_typ_desc;//char(300)  
        private string _need_approv;// char(1)
        private string _stk_itms;//char(1) 
        private string _non_stk_itms;//char(1) 
        private string _dflt_vend_code;//  char(6),
        private string _can_vend_change;//  char(1),
        private string _dflt_warehouse;// char(10),
        private string _can_whse_change;// char(1),
        private string _dflt_line_type;// char(3),
        private string _can_lintyp_change;// char(1)

        private int    _insertby;// integer 
        private string _insertdate;// datetime year to fraction(3) 
        private string _insertmachineinfo;//char(50)
        private int    _updateby;// integer
        private string _updatedate;// datetime year to fraction(3)
        private string _updatemachineinfo;//char(50)
        private int _rowid;
        public DVORequisitionTypeStureqtyp()
        {
            _req_typ_id = 0;
            _req_typ_code = string.Empty;
            _req_typ_desc = string.Empty;
            _need_approv = string.Empty;
            _stk_itms = string.Empty;
            _non_stk_itms = string.Empty;
            _dflt_vend_code = string.Empty;
            _can_vend_change = string.Empty;
            _dflt_warehouse = string.Empty;
            _can_whse_change = string.Empty;
            _dflt_line_type = string.Empty;
            _can_lintyp_change = string.Empty;
            _insertby = 0;
            _insertdate = "01/01/1900";
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";
            _updatemachineinfo = string.Empty;
            _rowid = 0;
        }
        public int req_typ_id
        {
            get { return _req_typ_id; }
            set { _req_typ_id = value; }
        }
        public string  req_typ_code
        {
            get { return _req_typ_code; }
            set { _req_typ_code = value; }
        }
        public string  req_typ_desc
        {
            get { return _req_typ_desc; }
            set { _req_typ_desc = value; }
        }
        public string need_approv
        {
            get { return _need_approv; }
            set { _need_approv = value; }
        }

        public string stk_itms
        {
            get { return _stk_itms; }
            set { _stk_itms = value; }
        }
        public string non_stk_itms
        {
            get { return _non_stk_itms; }
            set { _non_stk_itms = value; }
        }

        public string dflt_vend_code
        {
            get { return _dflt_vend_code; }
            set { _dflt_vend_code = value; }
        }
        public string can_vend_change
        {
            get { return _can_vend_change; }
            set { _can_vend_change = value; }
        }
        public string dflt_warehouse
        {
            get { return _dflt_warehouse; }
            set { _dflt_warehouse = value; }
        }
        public string can_whse_change
        {
            get { return _can_whse_change; }
            set { _can_whse_change = value; }
        }
        public string dflt_line_type
        {
            get { return _dflt_line_type; }
            set { _dflt_line_type = value; }
        }

        public string can_lintyp_change
        {
            get { return _can_lintyp_change; }
            set { _can_lintyp_change = value; }
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
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }

        #region storedProcedures


        public override string INSERT_SPNAME
        {
            get { return "uspreqstureqtypins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspreqstureqtypupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspreqstureqtypdel"; }
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
            get { return "stureqtyp"; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" select req_typ_id,req_typ_code,req_typ_desc,need_approv,stk_itms,non_stk_itms, ");
            sql.Append(" dflt_vend_code,can_vend_change,dflt_warehouse,can_whse_change,dflt_line_type,");
            sql.Append(" can_lintyp_change,rowid from stureqtyp where 1=1");


            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                    sql.Append(" and req_typ_code ='" + parameters[0].ToString().Trim() + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                    sql.Append(" AND Rtrim(req_typ_desc) LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");

            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" and stk_itms ='" + parameters[2].ToString().Trim() + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString().Trim().Length > 0)
                    sql.Append(" and non_stk_itms ='" + parameters[3].ToString().Trim() + "'");
            if (parameters[4] != null && parameters[4].ToString().Trim().Length > 0 && Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" and stureqtyp.rowid=" + parameters[4].ToString());
            sql.Append(" order by req_typ_id ");

            return sql.ToString();
        }
        #endregion StoredProcedures


    }

}
