using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVONssVoidClsAcct:DVOBase
    {

        private int _rowid;
        private int _doc_no;// serial,
        private string _acct_no;
        private string _void_date;
        private string _ok_to_post;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        #region Constructor
        public DVONssVoidClsAcct()
        {
            _rowid = 0;
            _doc_no = 0;
           
            _acct_no = string.Empty;
            _void_date = "01/01/1900";
            _ok_to_post = string.Empty;
            _insertmachineinfo = DVOApplicationUserInfo.MachineInfo;
            _insertdate = "01/01/1900";
            _insertby = DVOApplicationUserInfo.UserId;
            _updatemachineinfo = DVOApplicationUserInfo.MachineInfo;
            _updatedate = "01/01/1900";
            _updateby = DVOApplicationUserInfo.UserId;
        }
        #endregion Constructor

        #region Properties
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
        public string acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string void_date
        {
            get { return _void_date; }
            set { _void_date = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
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
        #endregion Properties

        #region Store Procedures
        public override string INSERT_SPNAME
        {
            get { return "uspnssvdclsins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspnssclsactupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspnssclsactdel"; }
        }

        public override string FIND_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string TABLE_NAME
        {
            get { return "nssvoidclsacct"; }
        }

        public override int UNIQUE_ID
        {
            get {return  _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();


            sql.Append("select rowid,doc_no,void_date,acct_no,ok_to_post from nssvoidclsacct where ok_to_post <> 'C'");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND doc_no= " + parameters[0].ToString());

            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(ok_to_post) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[2] != null)
                if (parameters[2].ToString().Trim().Length > 0)
                    sql.Append(" AND acct_no= '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty && !parameters[3].ToString().Trim().Contains("1900"))
                    sql.Append(" AND void_date = '" + parameters[3].ToString().Trim() + "'");

            if (parameters[4] != null)
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" AND rowid= " + parameters[4].ToString());

            sql.Append(" order by doc_no");

            return sql.ToString();
        }
        #endregion
    }
}
