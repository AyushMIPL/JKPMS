using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 20-06-2009 table used stipurce for Inventory Received
    public class DVOInventoryReceivedstipurce : DVOBase
    {
        private int _rowid;
        private int _doc_no;
        private DateTime _doc_date;
        private string _po_no;
        private string _purch_desc;
        private string _vend_code;
        private string _ok_post;


        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        #region Constructor

        public DVOInventoryReceivedstipurce()
        {
            _rowid = 0;
            _doc_no = 0;
            _doc_date = Convert.ToDateTime("01/01/1900");
            _po_no = "";
            _purch_desc = "";
            _vend_code = "";
            _ok_post = "";

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;

        }
        #endregion

        #region Property

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public DateTime doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }
        public string po_no
        {
            get { return _po_no; }
            set { _po_no = value; }
        }
        public string purch_desc
        {
            get { return _purch_desc; }
            set { _purch_desc = value; }
        }
        public string vend_code
        {
            get { return _vend_code; }
            set { _vend_code = value; }
        }
        public string ok_post
        {
            get { return _ok_post; }
            set { _ok_post = value; }
        }


        //Properties used for only SQL Server

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }


        #endregion

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspinvrecins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspinvrecupd"; }
        }

        public override string  DELETE_SPNAME
        {
            get { return "uspinvrecdel"; }
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
            get { return "stipurce"; }
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

        //Added By :Rahul jain on 20/06/09 Using in Inventory Received Posting report--
        public string GET_INVENTORY_RECEIVED
        {
            get { return "uspinvreceivedget"; }
        }
        public string Updateupd_stipurce
        {
            get { return "uspupd_stipurce"; }
        }
        public string Updateupd2_stipurce
        {
            get { return "uspupd2_stipurce"; }
        }
        //-----------------------------------------------------------------------------
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select doc_no,doc_date,po_no,purch_desc,vend_code,ok_post,rowid from stipurce where 1=1 ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(po_no) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1].ToString() != string.Empty && Convert.ToDateTime(parameters[1].ToString()) != Convert.ToDateTime("01/01/1900"))
                sql.Append(" and doc_date ='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND ok_post ='" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(purch_desc) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND doc_no = " + parameters[4].ToString());

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(vend_code) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND rowid = " + parameters[6].ToString());
            sql.Append(" order by doc_no");
            return sql.ToString();
        }


        #endregion Stored-Procedures
    }
}
