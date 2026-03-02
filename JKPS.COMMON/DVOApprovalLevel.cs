using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOApprovalLevel :DVOBase
    {
        private int _acd_id;
        private int _approval_level;
        private decimal _amount;
        private string _description;
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        #region Constructor

        public DVOApprovalLevel()
        {
            _acd_id=0;
            _approval_level=0;
            _amount=0;
            _description = string.Empty;
          // Informix table to be enhanced 
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
            _UpdateBy = 0;
            _UpdateMachineInfo = string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int acd_id
        {
            get { return _acd_id; }
            set { _acd_id = value; }
        }
        public int approval_level
        {
            get { return _approval_level; }
            set { _approval_level = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value.Trim(); }
        }
        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }
        }

        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value.TrimEnd(); }
        }

        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value.TrimEnd(); }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspGLActins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspGLActupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLActdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLActget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspAprovlLvlgetall"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            return "";
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

        //Added By Rahul Jain on 22/01/2010
        public string FIND_APPROVAL_BY_MODULE(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT acd_id,approval_level,amount,description ");
            sql.Append(" FROM inapplev ");
            sql.Append(" where 1=1 ");
            if (Convert.ToInt32(parameters[0]) > 0)//acd_id
                sql.Append(" AND acd_id =" + parameters[0].ToString());
            sql.Append(" ORDER BY approval_level asc");
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
