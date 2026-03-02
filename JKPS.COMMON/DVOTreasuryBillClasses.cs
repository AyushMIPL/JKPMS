using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by : sanjay chawla
    /// Date : 25 May 2009
    /// Description :common class for Treasury Bill Classes
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class DVOTreasuryBillClasses:DVOBase
    {
        private int _Rowid;
        private string _Class_code;
        private string _Class_desc;
        private int _Insertby;
        private DateTime _InsertDate;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private DateTime _Updatedate;
        private string _UpdateMachineInfo;
        

        #region Constructor
        public DVOTreasuryBillClasses()
        {
            _Rowid = 0;
            _Class_code=string.Empty;
            _Class_desc=string.Empty;
            _Insertby=0;
            _InsertDate=Convert.ToDateTime("01/01/1900");
            _InsertMachineInfo=string.Empty;
            _UpdateBy=0;
            _Updatedate=Convert.ToDateTime("01/01/1900");
            _UpdateMachineInfo=string.Empty;
        }
        #endregion Constructor

        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public string Class_code
        {
            get { return _Class_code; }
            set { _Class_code = value; }
        }
        public string Class_desc
        {
            get { return _Class_desc; }
            set { _Class_desc = value; }
        }  
        public int Insertby
        {
            get { return _Insertby; }
            set { _Insertby = value; }
        }
        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
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
        public DateTime Updatedate
        {
            get { return _Updatedate; }
            set { _Updatedate = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        #endregion public properties
        #region Stored-Procedures
       
        public string GET_Treasury_Classes
        {
            get { return "usp_get_trea_class"; }
        }
        public override string INSERT_SPNAME
        {
            get { return "usp_ins_trea_class"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_trea_class"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usp_del_trea_class"; }
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
            get { return "tbclasses"; }
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
       

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT rowid,class_code,class_desc FROM tbclasses where 1=1");
            if(parameters[0]!=null)
            if (parameters[0].ToString().Trim().Length > 0)//class_code
                sql.Append(" AND class_code like '" + parameters[0].ToString().Replace("'", "''") + "'");
            if(parameters[1]!=null)
            if (parameters[1].ToString().Trim().Length > 0)//class_desc
                sql.Append(" AND class_desc LIKE '" + parameters[1].ToString().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND  rowid=" + parameters[2].ToString());
            return sql.ToString();
        }
        #endregion store-procedures
    }     
}
