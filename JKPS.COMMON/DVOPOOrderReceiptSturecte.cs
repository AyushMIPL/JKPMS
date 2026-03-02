using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPOOrderReceiptSturecte : DVOBase
    {
        int _RowId;
        int _rec_doc_no;
        string _receipt_date;//date
        string _receipt_ref;
        string _po_no;
        int _po_doc_no;
        string _ok_post;
        string _ship_via;
        int _batch_id;
        string _ship_date;//date

        int _insertby;
        string _insertdate;//date
        string _insertmachineinfo;
        int _updateby;
        string _updatedate;//date
        string _updatemachineinfo;

        string _po_type;

        #region Constructor

        public DVOPOOrderReceiptSturecte()
        {
            _RowId = 0;
            _rec_doc_no = 0;
            _receipt_date = "01/01/1900";//date
            _receipt_ref = string.Empty;
            _po_no = string.Empty;
            _po_doc_no = 0;
            _ok_post = string.Empty;
            _ship_via = string.Empty;
            _batch_id = 0;
            _ship_date = "01/01/1900";//date

            _insertby = 0;
            _insertdate = "01/01/1900";//date
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";//date
            _updatemachineinfo = string.Empty;

            _po_type = string.Empty;
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

        public string receipt_date
        {
            get { return _receipt_date; }
            set { _receipt_date = value; }
        }

        public string receipt_ref
        {
            get { return _receipt_ref; }
            set { _receipt_ref = value; }
        }

        public string po_no
        {
            get { return _po_no; }
            set { _po_no = value; }
        }

        public int po_doc_no
        {
            get { return _po_doc_no; }
            set { _po_doc_no = value; }
        }

        public string ok_post
        {
            get { return _ok_post; }
            set { _ok_post = value; }
        }

        public string ship_via
        {
            get { return _ship_via; }
            set { _ship_via = value; }
        }

        public int batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; }
        }

        public string ship_date
        {
            get { return _ship_date; }
            set { _ship_date = value; }
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

        public string po_type
        {
            get { return _po_type; }
            set { _po_type = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usppurrectins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usppurrectupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usppurrectdel"; }
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
            get { return "sturecte"; }
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
        //Added By Rahul Jain on 01/06/2009 for getting data Post Receipts reports********
        public string GET_EDIT_RECEIPTS_DATA
        {
            get { return "usppoeditrecget"; }
        }
        public string GET_POST_RECEIPTS_DATA
        {
            get { return "usppopstrecget"; }
        }
        public string Update_sturecte_stmt
        {
            get { return "uspsturecte_stmt"; }
        }
        public string Update_sturecte_upd
        {
            get { return "uspstuordre_upd"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT s1.rowid p_rowid,s1.rec_doc_no p_rec_doc_no,s1.receipt_date p_receipt_date,");
            sql.Append(" s1.receipt_ref p_receipt_ref,s1.po_no p_po_no,s1.po_doc_no p_po_doc_no,s1.ok_post p_ok_post,");
            sql.Append(" s1.ship_via p_ship_via,s1.batch_id p_batch_id,s1.ship_date p_ship_date");
            sql.Append(" FROM sturecte s1,stuordre s2 WHERE s1.po_no = s2.po_no");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND s1.rowid = " + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)
                sql.Append(" AND s1.rec_doc_no = " + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty && !parameters[2].ToString().Trim().Contains("1900"))
                    sql.Append(" AND s1.receipt_date = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.receipt_ref) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.po_no) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND s1.po_doc_no = " + parameters[5].ToString());
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.ok_post) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.ship_via) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[8]) > 0)
                sql.Append(" AND s1.batch_id = " + parameters[8].ToString());
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty && !parameters[9].ToString().Trim().Contains("1900"))
                    sql.Append(" AND s1.p_ship_date = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null && parameters[10].ToString().Trim().Length > 0)
                if (parameters[10].ToString().Trim() == "-CPU")
                    sql.Append(" AND Rtrim(s2.po_type) <> 'CPU'");
                else
                    sql.Append(" AND Rtrim(s2.po_type) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
