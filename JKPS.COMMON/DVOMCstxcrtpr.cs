using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 27-07-2009 get or set property of stxcrtpr table.
    //Use in Multicurrency Module
    public class DVOMCstxcrtpr : DVOBase
    {
        private int _Rowid;
        private string _rate_type;
        private string _rate_desc;
        private string _rate_frequency;

        //Class Member Declearation Used For SqlServer
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;


        public DVOMCstxcrtpr()
        {
            _Rowid = 0;
            _rate_type = string.Empty;
            _rate_desc = string.Empty;
            _rate_frequency = string.Empty;

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
        }

        #region Public Property
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public string rate_type
        {
            get { return _rate_type; }
            set { _rate_type = value; }
        }
        public string rate_desc
        {
            get { return _rate_desc; }
            set { _rate_desc = value; }
        }
        public string rate_frequency
        {
            get { return _rate_frequency; }
            set { _rate_frequency = value; }
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

        #region Stored Procedure

        public override string INSERT_SPNAME
        {
            get { return "uspstxdcrtrins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspstxdcrtrupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspstxdcrtrdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspstxdcrtrget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "stxcrtpr"; }
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
        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            return sql.ToString();
        }

        #endregion Stored Procedures
    }
}
