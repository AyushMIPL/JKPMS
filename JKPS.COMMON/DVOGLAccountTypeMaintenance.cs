using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLAccountTypeMaintenance : DVOBase
    {
        private int _id;
        private string _accounttype;
        private string _desc;
        private int _keylength;
        private int _segmentcnt;
        private string _printsafter;
        private string _dfltacctcat;
        private string _dfltincrwcrdt;
        private string _AccountCategory;
        private string _SegmentDesc;
        private int _SId;
        private string _Subdivides;
        private int _rowid;
        #region Constructor

        public DVOGLAccountTypeMaintenance()
        {
            _id = 0;
            _accounttype = string.Empty;
            _desc = string.Empty;
            _keylength = 0;
            _segmentcnt = 0;
            _printsafter = string.Empty;
            _dfltacctcat = string.Empty;
            _dfltincrwcrdt = string.Empty;
            
            _AccountCategory = string.Empty;
            
            //**************For Report Print Employee List By Ministry
            _SegmentDesc = string.Empty;
            _SId = 0;
            _Subdivides = string.Empty;
            _rowid = 0;
        }

        #endregion Constructor

        #region Public Properties

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string accounttype
        {
            get { return _accounttype; }
            set { _accounttype = value; }
        }

        public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public int keylength
        {
            get { return _keylength; }
            set { _keylength = value; }
        }

        public int segmentcnt
        {
            get { return _segmentcnt; }
            set { _segmentcnt = value; }
        }

        public string printsafter
        {
            get { return _printsafter; }
            set { _printsafter = value; }
        }

        public string dfltacctcat
        {
            get { return _dfltacctcat; }
            set { _dfltacctcat = value; }
        }
        public string dfltincrwcrdt
        {
            get { return _dfltincrwcrdt; }
            set { _dfltincrwcrdt = value; }
        }

        
        public string AccountCategory
        {
            get { return _AccountCategory; }
            set { _AccountCategory = value; }
        }
        
        //**************For Report Print Employee List By Ministry
        public string SegmentDesc
        {
            get { return _SegmentDesc; }
            set { _SegmentDesc = value; }
        }
        public int SId
        {
            get { return _SId; }
            set { _SId = value; }
        }
        public string Subdivides
        {
            get { return _Subdivides; }
            set { _Subdivides = value; }
        }

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }


        #endregion Public Properties

        #region Stored-Procedures

        
        public string GET_ACCOUNT_SEGMENTS
        {
            get { return "USP_GlActypSegGet"; }
        }
        public string GET_ACCOUNT_TYPES
        {
            get { return "USP_GlActypMntGet"; }
        }
        public string VALIDATE_KEYVALUE
        {
            get { return "USP_ValidateKeyVal"; }
        }
        //*************************************************************************************************
        //Used at report Print Employee List By Ministry
        public string GET_SEGMENTS_DESC
        {
            get { return "USP_GetSegmentDesc"; }
        }
        public string GET_GL_ACCOUNT_SEGMENTS
        {
            get { return "USP_GlSegmentGet"; }
        }
        public override string INSERT_SPNAME
        {
            get { return "USP_GlActypMntIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_GlActypMntUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_GlActypMntDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspglactypmntget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_GlactpMntGtAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "Flex_struct_Header"; }
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
        {//accounttype = tr.acct_type
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT distinct Flex_struct_Header.id,Flex_struct_Header.accounttype,Flex_struct_Header.[desc] description,Flex_struct_Header.keylength,Flex_struct_Header.segmentcnt,Flex_struct_Header.printsafter,Flex_struct_Header.dfltacctcat,Flex_struct_Header.dfltincrwcrdt,Flex_struct_Header.rowid from Flex_struct_Header ");
            string s = "LEFT OUTER JOIN PayrollGLAccounts ON Flex_struct_Header.accounttype=PayrollGLAccounts.acct_type ";
            //if (parameters[5] != null)//acctCategory
            // if (parameters[5].ToString() != string.Empty)
            //   s = "PayrollGLAccounts";
            sql.Append(s);
            sql.Append(" WHERE 1=1 ");
            if (parameters[0] != null)//accounttype
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Flex_struct_Header.accounttype) LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[1] != null)//desc
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Flex_struct_Header.[desc]) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[2] != null)//dfltacctcat
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Flex_struct_Header.dfltacctcat) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)//dfltincrwcrdt
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Flex_struct_Header.dfltincrwcrdt) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)//printsafter
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Flex_struct_Header.printsafter) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[5] != null)//acctCategory
                if (parameters[5].ToString() != string.Empty)
                {
                    if (parameters[5].ToString().Trim().Substring(0, 1) == "-")
                        sql.Append(" AND RTRIM(PayrollGLAccounts.acct_cat) <> '" + parameters[5].ToString().Trim().Substring(1).Replace("'", "''") + "'");
                    else
                        sql.Append(" AND RTRIM(PayrollGLAccounts.acct_cat) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
                }

            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND  Flex_struct_Header.rowid=" + parameters[6].ToString());


            sql.Append(" order by id");

            return sql.ToString();
        }


        
        public string ALL_ACCOUNTTYPE
        {
            get { return "USP_AccountGet"; }
        }

        public string Get_Account_ID
        {
            get { return "USP_AcctIdGet"; }
        }


        #endregion Stored-Procedures
    }
}
