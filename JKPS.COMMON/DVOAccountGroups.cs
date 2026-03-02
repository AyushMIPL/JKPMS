using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Implemented By: Sunil Pahwa
    public class DVOAccountGroups : DVOBase
    {
        private string _grp_key;
        private string _grp_desc;
        private string _acct_type;
        private string _acct_desc;
        private string _keyvalue;
        private int _Accounttypeid;
        private string _desc;
        private int _acct_no;
        private int _Rowid;

        #region Constructor
        public DVOAccountGroups()
        {
            _grp_key = string.Empty;
            _grp_desc = string.Empty;
            _acct_type = string.Empty;
            _acct_desc = string.Empty;
            _keyvalue = string.Empty;
            _acct_type = string.Empty;
            _desc = string.Empty;
            _acct_no = 0;
            _Rowid = 0;
        }
        #endregion Constructor

        #region public properties
        public string grp_key
        {
            get { return _grp_key; }
            set { _grp_key = value; }
        }

        public string grp_desc
        {
            get { return _grp_desc; }
            set { _grp_desc = value; }
        }
        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }

        public string acct_desc
        {
            get { return _acct_desc; }
            set { _acct_desc = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }
        public string acct_typ
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }



        public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }


        public int Rowid
        {
            get { return _Rowid ; }
            set { _Rowid  = value; }
        }

        public int Accounttypeid
        {
            get { return _Accounttypeid; }
            set { _Accounttypeid = value; }
        }

        #endregion public properties

        #region Stored-Procedures
            
        public string INSERT_ACCOUNT_GROUP_DETAIL
        {
            get { return "uspAccGrpsDtlIns"; }
        }

        //public string UPDATE_ACCOUNT_GROUP_DETAIL
        //{
        //    get { return ""; }//uspAccGrpsDtlupd
        //}

        //public string DELETE_ACCOUNT_GROUP_DETAIL
        //{
        //    get { return "uspAccGrpsDtldel"; }
        //}

        public override string INSERT_SPNAME
        {
            get { return "uspAccGrpsIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspaccgrpsupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspAccGrpsDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspAccGrpsGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspsecusergetall"; }
        }

        public override string TABLE_NAME
        {
            get { return "stxactgd"; }
        }

        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string uspAccGrpsDtlget
        {
            get { return "uspAccGrpsDtlget"; }
        }

        //LASTLY
        public string DEL_ACCOUNT_GROUP_DETAILS
        {
            get { return "uspaccgrpdtldel"; }
        }
        public string UPD_ACCOUNT_GROUP_DETAILS
        {
            get { return "uspaccgrpdtlupd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT grp_key p_grp_key ,grp_desc p_grp_desc,rowid p_rowid ");
            sql.Append(" from stxactgr");
            sql.Append(" where 1=1");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(grp_key) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(grp_desc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND rowid = " + parameters[2].ToString());

            return sql.ToString();
        }

        #endregion store-procedures
    }
}
