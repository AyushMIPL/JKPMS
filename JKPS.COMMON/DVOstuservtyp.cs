using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOstuservtyp : DVOBase
    {
        private int _rowid;
        private int _serv_typ_id;
        private string _serv_typ_name;
        private string _serv_typ_desc;
        private string _need_prev_ordr_no;//char(1)
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        #region Constructor
        public DVOstuservtyp()
        {
            _rowid = 0;
            _serv_typ_id = 0;
            _serv_typ_name = string.Empty;
            _serv_typ_desc = string.Empty;
            _need_prev_ordr_no = string.Empty;
            _insertby = 0;
            _insertdate = string.Empty;
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = string.Empty;
            _updatemachineinfo = string.Empty;
        }
        #endregion Constructor

        #region Properties
        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int serv_typ_id
        {
            get { return _serv_typ_id; }
            set { _serv_typ_id = value; }
        }
        public string serv_typ_name
        {
            get { return _serv_typ_name; }
            set { _serv_typ_name = value; }
        }
        public string serv_typ_desc
        {
            get { return _serv_typ_desc; }
            set { _serv_typ_desc = value; }
        }
        public string need_prev_ordr_no
        {
            get { return _need_prev_ordr_no; }
            set { _need_prev_ordr_no = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertDate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineInfo
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
        public string updatemachineInfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }
#endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspstuservtypins"; } 
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstuservtypupd"; } 
        }

        public override string DELETE_SPNAME
        {
            get { return "uspstuservtypdel"; } 
        }

        public override string FIND_SPNAME
        {
            get { return " "; }
        }
        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "stuservtyp"; }
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
            sql.Append("SELECT rowid,serv_typ_id,serv_typ_name,serv_typ_desc,need_prev_ordr_no ");
            sql.Append(" FROM stuservtyp ");
            sql.Append(" WHERE 1=1 ");
            if (Convert.ToInt32(parameters[0]) > 0)//rowid
                sql.Append(" AND rowid =" + parameters[0].ToString());
            if (Convert.ToInt32(parameters[1]) > 0)//serv_typ_id
                sql.Append(" AND serv_typ_id =" + parameters[1].ToString());
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//serv_typ_name
                sql.Append(" AND serv_typ_name like '" + parameters[2].ToString().Replace("'", "''") + "%'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//serv_typ_desc
                sql.Append(" AND serv_typ_desc like '" + parameters[3].ToString().Replace("'", "''") + "%'");
            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//need_prev_ordr_no
                sql.Append(" AND need_prev_ordr_no ='" + parameters[4].ToString().Replace("'", "''") + "'");
            return sql.ToString();
        }
        public string FIND_DUPLICATE(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid,serv_typ_id,serv_typ_name,serv_typ_desc,need_prev_ordr_no ");
            sql.Append(" FROM stuservtyp ");
            sql.Append(" WHERE 1=1 ");
            if (parameters[0].ToString() != string.Empty && parameters[0].ToString() != null)//serv_typ_name
                sql.Append(" AND serv_typ_name ='" + parameters[0].ToString().Replace("'", "''") + "'");
           
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
