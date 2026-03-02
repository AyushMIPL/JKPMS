using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by :chandra
    /// Date : 19/08/2008
    /// Description : This is basically used for holding the data of stxactgr.  
    /// Modified by:
    /// Modified Date :
    /// Description :
    /// </summary>
    public class DVOAccountGroupsZ:DVOBase
    {
        #region Private Variables
       /// <summary>
       /// 
       /// </summary>
        private string _grp_key;
        /// <summary>
        /// 
        /// </summary>
        private string _grp_desc;
        /// <summary>
        /// General Variables for Maintaning Log Info
        /// </summary>
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        #endregion
        #region Constructor
        public DVOAccountGroupsZ()
        {
            _grp_key = string.Empty;
            _grp_desc = string.Empty;
        }
        
        #endregion Constructor
        #region Public Properties


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
            get { return ""; }
        }

        public override string UPDATE_SPNAME
        {
            get { return ""; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspActgrpgetZoom"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
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

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  grp_key,grp_desc FROM  stxactgr where 1=1 ");

           // if (parameters[0].ToString().Length <0)//grp_key
             //   sql.Append(" AND Rtrim(grp_key)like  TRIM('" + parameters[0].ToString() + "%')");
            //if (parameters[1].ToString().Length<=0)//grp_desc
              //  sql.Append(" AND Rtrim(grp_desc)like  TRIM('" + parameters[1].ToString() + "%')");
            return sql.ToString();
          }

        #endregion Stored-Procedures
    }
}
