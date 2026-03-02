using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
     /// <summary>
    /// Implemented by :chandra
    /// Date : 19/08/2008
    /// Description : This is basically used for holding the data of stxinfor.  
    /// Modified by:
    /// Modified Date :
    /// Description :
    /// </summary>
    public class DVOSource:DVOBase
    {
        #region Private Variables
        private string _src_type;
        private string _src_key;
        private string _src_desc;
        private decimal _src_num_desc;
        private string _src_char_desc;
        private int _src_acct_no;

        /// <summary>
        /// General Variables for Maintaning Log Info
        /// </summary>
        private int _InsertBy;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _UpdateMachineInfo;
        #endregion
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
            get { return "uspsrcgetZoom"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
             
            StringBuilder sql = new StringBuilder();
            sql.Append(" select src_type,src_key,src_desc,src_num_desc,src_char_desc,src_acct_no");
            sql.Append(" from stxinfor where 1=1 ");
            
            if (Convert.ToString(parameters[0]).Length>0)//batch_id
                sql.Append(" AND src_type ='" + parameters[0].ToString()+"'");
            return sql.ToString();
          }

        #endregion Stored-Procedures
        #region Constructor
        public DVOSource()
        {
          _src_type=string.Empty;
          _src_key = string.Empty;
          _src_desc = string.Empty;
          _src_num_desc=0;
          _src_char_desc = string.Empty;
          _src_acct_no = 0;
        }
        
        #endregion Constructor
        #region Public Properties

        public string src_type
        {
            get { return _src_type; }
            set { _src_type = value; }
        }
        public string src_key
        {
            get { return _src_key; }
            set { _src_key = value; }
        }

        public string src_desc
        {
            get { return _src_desc; }
            set { _src_desc = value; }
        }
        public decimal src_num_desc
        {
            get { return _src_num_desc; }
            set { _src_num_desc = value; }
        }
        public string src_char_desc
        {
            get { return _src_char_desc; }
            set { _src_char_desc = value; }
        }
        public int src_acct_no
        {
            get
            {
                return _src_acct_no;
            }
            set
            {
                _src_acct_no = value;
            }
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
        #endregion Public Properties
    }

    


     
      
       

  
}
