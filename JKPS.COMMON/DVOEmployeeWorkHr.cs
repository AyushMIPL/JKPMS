using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOEmployeeWorkHr : DVOBase
    {
        private int _rowid;
        private int  _EmpWorkingHrId;
	    private DateTime _StartDate;
        private DateTime _EndDate;
        private DateTime _InsertDate;
        private int _InsertBy;
        private string _InsertMachineInfo;
   
        #region Constructor
        public DVOEmployeeWorkHr()
        {
            _EmpWorkingHrId = 0;
            _StartDate = Convert.ToDateTime("01/01/1900");
            _EndDate = Convert.ToDateTime("01/01/1900");
            _InsertDate = Convert.ToDateTime("01/01/1900");
            _InsertBy = 0;
            _InsertMachineInfo = string.Empty;
        }
        #endregion Constructor

        #region Properties

        public int RowID
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int EmpWorkingHrId
        {
            get { return _EmpWorkingHrId; }
            set { _EmpWorkingHrId = value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
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

        public int InsertBy
        {
            get { return _InsertBy; }
            set { _InsertBy = value; }

        }
        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpWorkingHrIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpWorkingHrUpd"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_EmpWorkingHrGet"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpWorkingHrDel"; }
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
            sql.Append("SELECT isnull(EmpWorkingHrDetailsId,0)EmpWorkingHrDetailsId,isnull(EmployeeWorkingHr.EmpWorkingHrId,0)EmpWorkingHrId,MasterEmployee.empl_code,(first_name+' '+middle_name+' '+last_name) EmpName,");
            sql.Append(" CAST(ISNULL(WorkingDate,'1900-01-01') AS DATETIME)WorkingDate,isnull(Remark,'')Remark,isnull(WorkingHr,0)WorkingHr,isnull(RgDayOvtm,0)RgDayOvtm ,isnull(PhOvtm,0)PhOvtm, ");
            sql.Append(" CAST(ISNULL(StartDate,'1900-01-01') AS DATETIME)StartDate,CAST(ISNULL(EndDate,'1900-01-01') AS DATETIME)EndDate ");
            sql.Append(" From  MasterEmployee LEFT OUTER JOIN EmployeeWorkingHrDetails ");
            sql.Append(" ON MasterEmployee.empl_code=EmployeeWorkingHrDetails.empl_code LEFT JOIN EmployeeWorkingHr ON EmployeeWorkingHr.EmpWorkingHrId=EmployeeWorkingHrDetails.EmpWorkingHrId Where 1 =1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND Empl_WorkHr_Id = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");



             return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
