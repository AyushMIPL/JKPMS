using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace JKPS.COMMON
{
    public class DVOUserApprovalInfoOverAll :DVOBase
    {
        /// <summary>
        /// 
        /// </summary>
        private string _position;
        /// <summary>
        /// User Selected from the DROp down
        /// </summary>
        private int _user_id;
        /// <summary>
        /// General Variables for Maintaning Log Info
        /// </summary>
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        private List<DVOAccountPermission> _DVOAccountPermissions;
        #region Constructor
        public DVOUserApprovalInfoOverAll()
        {
            _user_id = 0;
            //_InsertBy = 0;
            //_InsertMachineInfo = string.Empty;
            //_UpdateBy = 0;
            //_UpdateMachineInfo = string.Empty;
            _DVOAccountPermissions = new List<DVOAccountPermission>();
        }
        public DVOUserApprovalInfoOverAll(int puser_id)
        {
            _user_id = puser_id;
            //_InsertBy = 0;
            //_InsertMachineInfo = string.Empty;
            //_UpdateBy = 0;
            //_UpdateMachineInfo = string.Empty;
            _DVOAccountPermissions = new List<DVOAccountPermission>();
        }
       
        #endregion Constructor

        #region Public Properties
        public object DVOAccountPermissions
        {
            get
            {
                object obj=_DVOAccountPermissions;
                return obj;
            }
            set
            {
                _DVOAccountPermissions.Add((DVOAccountPermission)value);
            }
        }
        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public string position
        {
            get { return _position; }
            set { _position = value.Trim(); }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspUsrApprInfoins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspUsrApprInfoupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspUsrApprInfodel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspUsrApprInfoget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspUsrApprInfogetall"; }
        }
        public override string TABLE_NAME
        {
            get { return ""; }
        }

        public override int UNIQUE_ID
        {
            get { return 0; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string DELETE_APPROVAL_DETAILS
        {
            get { return "uspusrapprinfddel"; }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT inaccdef.user_id,inxuserr.login_id,inxuserr.first_name,inxuserr.last_name,inxuserr.position,"); 
            sql.Append("inaccdef.acct_type_id,Flex_struct_Header.accounttype,inaccdef.acc_mask,inaccdef.acd_id,inappcls.dept,");
            sql.Append("inaccdef.approval_level,inaccdef.rowid");
            sql.Append(" FROM inaccdef,Flex_struct_Header,inappcls,inxuserr,secusers ");
            sql.Append(" where inaccdef.acct_type_id = Flex_struct_Header.id");
            sql.Append(" and inaccdef.acd_id = inappcls.acd_id");
            sql.Append(" and inxuserr.user_id=secusers.userid ");
            sql.Append(" and inaccdef.user_id = inxuserr.user_id and inaccdef.User_id is not null ");
            sql.Append(" and secusers.active = 1 ");
            if (Convert.ToInt32(parameters[0]) > 0)//User Id
                sql.Append(" AND inaccdef.user_id =" + parameters[0].ToString());
            sql.Append(" ORDER BY inaccdef.user_id asc");
            return sql.ToString();
        }

        public string GET_USER_APPROVAL(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT inaccdef.user_id,inxuserr.login_id,inxuserr.first_name,inxuserr.last_name,");
            sql.Append(" inxuserr.position,inaccdef.acct_type_id,Flex_struct_Header.accounttype,inaccdef.acc_mask,");
            sql.Append(" inaccdef.acd_id,inappcls.dept,inaccdef.approval_level,inaccdef.rowid,");
            sql.Append(" inapplev.description,inapplev.amount,secusers.usrmin,secusers.usrdept,s1.desc mindesc,s2.desc deptdesc");
            sql.Append(" FROM inaccdef,Flex_struct_Header,inappcls,inxuserr,inapplev,secusers,outer Master_Segment s1,outer Master_Segment s2 ");
            sql.Append(" where inaccdef.acct_type_id = Flex_struct_Header.id and inaccdef.acd_id = inappcls.acd_id");
            sql.Append(" and inaccdef.user_id = inxuserr.user_id and inaccdef.User_id is not null");
            sql.Append(" and inappcls.acd_id=inapplev.acd_id");
            sql.Append(" and inxuserr.user_id=secusers.userid");
            sql.Append(" and secusers.usrmin=s1.keyvalue and s1.segmentid=23");
            sql.Append(" and secusers.usrdept=s2.keyvalue and s2.segmentid=24");



            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" and inaccdef.user_id =" + parameters[0].ToString().Trim());
            return sql.ToString();
        }


         public string LOCK_SPName
        {
            get
            {
                return "usp_Lock_Row";
            }
        }
        #endregion Stored-Procedures
    }
}
