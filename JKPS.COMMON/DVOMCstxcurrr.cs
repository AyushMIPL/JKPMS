using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 27-07-2009 get or set property of stxcurrr table.
    //Use in Multicurrency Module
    public class DVOMCstxcurrr : DVOBase
    {
        private int _Rowid;
        private string _currency_code;
        private string _description;
        private string _curr_ex_rate;
        private string _per_ex_rate;
        private int _ex_diff_acct_no;


        //Class Member Declearation Used For SqlServer
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        public DVOMCstxcurrr()
        {
            _Rowid = 0;
            _currency_code = string.Empty;
            _description = string.Empty;
            _curr_ex_rate = string.Empty;
            _per_ex_rate = string.Empty;
            _ex_diff_acct_no = 0;

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
        public string currency_code
        {
            get { return _currency_code; }
            set { _currency_code = value; }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string curr_ex_rate
        {
            get { return _curr_ex_rate; }
            set { _curr_ex_rate = value; }
        }
        public string per_ex_rate
        {
            get { return _per_ex_rate; }
            set { _per_ex_rate = value; }
        }
        public int ex_diff_acct_no
        {
            get { return _ex_diff_acct_no; }
            set { _ex_diff_acct_no = value; }
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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "stxcurrr"; }
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
