using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOProcessHolidayPayCal : DVOBase
    {
        private int _RowId;
        private int _ProcessHolidayPayCalId;
        private string _empl_code;
        private string _LRD;
        private string _start_date;// smalldatetime,
        private string _end_date;// smalldatetime,
        private decimal _TotalEarning;
        private int _Proportionate;
        private decimal _Calculation;
        private decimal _LessSocialSecurity;
        private decimal _NetHolidayPay;
        private int _WeekDays;
        private string _ExpectedToResumeDuty;
        private string _InsertMachineInfo;
        private string _InsertDate;// datetime,
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;// datetime,
        private int _UpdateBy;

        #region Constructor

        public DVOProcessHolidayPayCal()
        {
            _RowId = 0;
            _ProcessHolidayPayCalId = 0;
            _empl_code = string.Empty;
            _LRD = string.Empty;
            _start_date = "01/01/1900";// smalldatetime,
            _end_date = "01/01/1900";// smalldatetime,
            _TotalEarning = 0;
            _Proportionate = 0;
            _Calculation = 0;
            _LessSocialSecurity = 0;
            _NetHolidayPay = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";// datetime,
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";// datetime,
            _UpdateBy = 0;
            _ExpectedToResumeDuty = "01/01/1900";
            _WeekDays = 0;
        }

        #endregion Constructor

        #region public properties

        
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public int ProcessHolidayPayCalId
        {
            get { return _ProcessHolidayPayCalId; }
            set { _ProcessHolidayPayCalId = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string LRD
        {
            get { return _LRD; }
            set { _LRD = value; }
        }
        public string start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public string end_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }
        public decimal TotalEarning
        {
            get { return _TotalEarning; }
            set { _TotalEarning = value; }
        }
        public int Proportionate 
        {
            get { return _Proportionate; }
            set { _Proportionate = value; }
        }
        public decimal Calculation
        {
            get { return _Calculation; }
            set { _Calculation = value; }
        }
        public decimal LessSocialSecurity
        {
            get { return _LessSocialSecurity; }
            set { _LessSocialSecurity = value; }
        }
        public decimal NetHolidayPay
        {
            get { return _NetHolidayPay; }
            set { _NetHolidayPay = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public string InsertDate
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
        public string UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public int WeekDays
        {
            get { return _WeekDays; }
            set { _WeekDays = value; }
        }
        public string ExpectedToResumeDuty
        {
            get { return _ExpectedToResumeDuty; }
            set { _ExpectedToResumeDuty = value; }
        }
        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_ProcessHolidayPayCalIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_ProcessHolidayPayCalUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_ProcessHolidayPayCalDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_ProcessHolidayPayGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_ProcessHolidayPayGet"; }
        }

        public override string TABLE_NAME
        {
            get { return "ProcessHolidayPayCal"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * ");
            sql.Append(" FROM ProcessHolidayPayCal WHERE 1=1 ");
                if (parameters[5].ToString().Trim() != string.Empty && !parameters[5].ToString().Trim().Contains("1900"))
                    sql.Append(" AND start_date = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim() != string.Empty && !parameters[6].ToString().Trim().Contains("1900"))
                    sql.Append(" AND end_date = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
