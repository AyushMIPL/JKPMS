using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    //Written By : Rahul Jain on 29-07-2009 get or set property of stxmtxpr table.
    //Use in Multilevel Tax Module
    public class DVOMultiTaxstxmtxpr :DVOBase
    {
        private int _Rowid;
        private string _period;
        private string _period_year;
        private DateTime _start_date;
        private DateTime _end_date;

        //Class Member Declearation Used For SqlServer
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        #region Constructor
        public DVOMultiTaxstxmtxpr()
        {
            _Rowid = 0;
            _period = string.Empty;
            _period_year = string.Empty;
            _start_date = Convert.ToDateTime("01/01/1900");
            _end_date = Convert.ToDateTime("01/01/1900");

            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
        }
        #endregion

        #region Public Property

        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public string period
        {
            get { return _period; }
            set { _period = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public DateTime start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public DateTime end_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }
        ////Properties used for only SQL Server
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
            get { return "stxmtxpr"; }
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

        public string GET_STARTEND_DATE
        {
            get { return "uspstxmtxprget"; }
        }
        public string GetMaxPeriod
        {
            get { return "uspmax_period"; }
        }
        public string GetMaxYear
        {
            get { return "uspmax_pyear"; }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();

            return sql.ToString();
        }
        #endregion Stored Procedures
    }
}
