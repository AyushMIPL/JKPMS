using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPOOrderReceiptDetailSturectd : DVOBase
    {
        int _RowId;
        int _rec_doc_no;
        int _rec_line_no;
        decimal _recv_qty;
        decimal _rjct_qty;
        string _rjct_code;
        int _po_doc_no;
        int _po_line_no;
        int _insertby;
        string _insertdate;// datetime
        string _insertmachineinfo;
        int _updateby;
        string _updatedate;// datetime
        string _updatemachineinfo;

        #region Constructor

        public DVOPOOrderReceiptDetailSturectd()
        {
            _RowId = 0;
            _rec_doc_no = 0;
            _rec_line_no = 0;
            _recv_qty = 0;
            _rjct_qty = 0;
            _rjct_code = string.Empty;
            _po_doc_no = 0;
            _po_line_no = 0;
            _insertby = 0;
            _insertdate = "01/01/1900";// datetime
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";// datetime
            _updatemachineinfo = string.Empty;
        }

        #endregion Constructor

        #region public properties

        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public int rec_doc_no
        {
            get { return _rec_doc_no; }
            set { _rec_doc_no = value; }
        }
        public int rec_line_no
        {
            get { return _rec_line_no; }
            set { _rec_line_no = value; }
        }
        public decimal recv_qty
        {
            get { return _recv_qty; }
            set { _recv_qty = value; }
        }
        public decimal rjct_qty
        {
            get { return _rjct_qty; }
            set { _rjct_qty = value; }
        }
        public string rjct_code
        {
            get { return _rjct_code; }
            set { _rjct_code = value; }
        }
        public int po_doc_no
        {
            get { return _po_doc_no; }
            set { _po_doc_no = value; }
        }
        public int po_line_no
        {
            get { return _po_line_no; }
            set { _po_line_no = value; }
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

        #endregion public properties

        #region Stored-Procedures

        public string PO_DETAIL_LINES_FOR_TRANSACTION
        {
            get { return "usppodtllines2"; }//usppodtllines
        }
        public string PO_DETAIL_LINES_FOR_DISPLAY
        {
            get { return "usppodtlget"; }
        }
        public string UNPOSTED_RECEIVED_QUANTITY
        {
            get { return "uspunpstrcvdqty2"; }//uspunpstrcvdqty
        }
        public string PROCESSED_RECEIVED_QUANTITY
        {
            get { return "uspprocrcvdqty"; }
        }

        public override string INSERT_SPNAME
        {
            get { return "usppurrectdtlins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usppurrectdtlupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usppurrectdtldel"; }
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
            get { return "sturectd"; }
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

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT s2.rowid p_rowid,s2.rec_doc_no p_rec_doc_no,s2.rec_line_no p_rec_line_no,s2.recv_qty p_recv_qty,");
            sql.Append(" s2.rjct_qty p_rjct_qty,s2.rjct_code p_rjct_code,s2.po_doc_no p_po_doc_no,s2.po_line_no p_po_line_no");
            sql.Append(" FROM sturectd s2 WHERE 1=1");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND s2.rowid = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND s2.rec_doc_no = " + parameters[1].ToString());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND s2.rec_line_no = " + parameters[2].ToString());
            if (Convert.ToDecimal(parameters[3]) > 0)
                sql.Append(" AND s2.recv_qty = " + parameters[3].ToString());
            if (Convert.ToDecimal(parameters[4]) > 0)
                sql.Append(" AND s2.rjct_qty = " + parameters[4].ToString());
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s2.rjct_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND s2.po_doc_no = " + parameters[6].ToString());
            if (Convert.ToInt32(parameters[7]) > 0)
                sql.Append(" AND s2.po_line_no = " + parameters[7].ToString());
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
