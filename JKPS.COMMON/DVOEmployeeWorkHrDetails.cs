using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOEmployeeWorkHrDetails : DVOBase
    {
        private int _EmpWorkingHrDetailsId;
        private int _EmpWorkingHrId;
        private string _empl_code;
        private string _EmpName;
        private string _Soc_Sec_Num;
        private DateTime _WorkingDate;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private string _Status;
        private string _Remark;
        private decimal _WorkingHr;
        private decimal _RgDayOvtm;
        private decimal _PhOvtm;
        private decimal _inc_rate;
        private decimal _Total;
        private string _inc_code;
   
        #region Constructor
        public DVOEmployeeWorkHrDetails()
        {
            _EmpWorkingHrDetailsId = 0;
            _EmpWorkingHrId = 0;
            _empl_code = "";
            _EmpName = "";
            _WorkingDate = Convert.ToDateTime("01/01/1900");
            _StartDate = Convert.ToDateTime("01/01/1900");
            _EndDate = Convert.ToDateTime("01/01/1900");
            _Remark = "";
            _Status = "";
            _WorkingHr = 0;
            _RgDayOvtm = 0;
            _PhOvtm = 0;
            _inc_rate = 0;
            _Total = 0;
            _inc_code = "";
        }
        #endregion Constructor

        #region Properties
        public int EmpWorkingHrDetailsId
        {
            get { return _EmpWorkingHrDetailsId; }
            set { _EmpWorkingHrDetailsId = value; }
        }
        public int EmpWorkingHrId
        {
            get { return _EmpWorkingHrId; }
            set { _EmpWorkingHrId = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string EmpName
        {
            get { return _EmpName; }
            set { _EmpName = value; }
        }
        public string Soc_Sec_Num
        {
            get { return _Soc_Sec_Num; }
            set { _Soc_Sec_Num = value; }
        }
        public DateTime WorkingDate
        {
            get { return _WorkingDate; }
            set { _WorkingDate = value; }
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
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public decimal WorkingHr
        {
            get { return _WorkingHr; }
            set { _WorkingHr = value; }
        }
        public decimal RgDayOvtm
        {
            get { return _RgDayOvtm; }
            set { _RgDayOvtm = value; }
        }
        public decimal PhOvtm
        {
            get { return _PhOvtm; }
            set { _PhOvtm = value; }
        }
        public decimal inc_rate
        {
            get { return _inc_rate; }
            set { _inc_rate = value; }
        }
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        public string inc_code
        {
            get { return _inc_code; }
            set { _inc_code = value; }
        }

        #endregion Properties

        #region Stored-Procedures

        public string GET_TimCardRptGet
        {
            get { return "USP_TimCardRptGet"; }
        }

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpWorkingHrDetailsIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpWorkingHrDetailsUpd"; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_EmpWorkingHrDetailsGet"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpWorkingHrDetailsDel"; }
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
            get;
            set;
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT isnull(EmpWorkingHrDetailsId,0)EmpWorkingHrDetailsId,isnull(EmployeeWorkingHr.EmpWorkingHrId,0)EmpWorkingHrId,MasterEmployee.empl_code,(first_name+' '+middle_name+' '+last_name) EmpName,");
            sql.Append(" CAST(ISNULL(WorkingDate,'1900-01-01') AS DATETIME)WorkingDate,isnull(Remark,'')Remark,isnull(WorkingHr,0)WorkingHr,isnull(RgDayOvtm,0)RgDayOvtm ,isnull(PhOvtm,0)PhOvtm, ");
            sql.Append(" CAST(ISNULL(StartDate,'1900-01-01') AS DATETIME)StartDate,CAST(ISNULL(EndDate,'1900-01-01') AS DATETIME)EndDate,isnull(Status,'')Status ");
            sql.Append(" From  MasterEmployee LEFT OUTER JOIN EmployeeWorkingHrDetails ");
            sql.Append(" ON MasterEmployee.empl_code=EmployeeWorkingHrDetails.empl_code LEFT JOIN EmployeeWorkingHr ON EmployeeWorkingHr.EmpWorkingHrId=EmployeeWorkingHrDetails.EmpWorkingHrId Where 1 =1 AND terminated is null");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND Empl_WorkHr_Id = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");



            return sql.ToString();
        }

        public string FIND_TOTALS_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MasterEmployee.empl_code,(first_name+' '+middle_name+' '+last_name) EmpName,isnull(1,'')Remark,");
            sql.Append(" SUM(isnull(WorkingHr,0))WorkingHr,SUM(isnull(RgDayOvtm,0))RgDayOvtm ,SUM(isnull(PhOvtm,0))PhOvtm,isnull(1,'')Status,isnull(MasterEmployeeIncomes.inc_code,'')inc_code,isnull(MasterEmployeeIncomes.inc_rate,0)inc_rate ");
            sql.Append(" From  MasterEmployee INNER JOIN EmployeeWorkingHrDetails  ");
            sql.Append(" ON MasterEmployee.empl_code=EmployeeWorkingHrDetails.empl_code LEFT JOIN EmployeeWorkingHr ON EmployeeWorkingHr.EmpWorkingHrId=EmployeeWorkingHrDetails.EmpWorkingHrId ");
            sql.Append(" INNER JOIN MasterEmployeeIncomes ON MasterEmployeeIncomes.empl_code=MasterEmployee.empl_code Where 1 =1 AND terminated is null");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND convert(datetime,CONVERT(VARCHAR(10),WorkingDate,101)) >= convert(datetime,CONVERT(VARCHAR(10), CAST('" + parameters[0].ToString().Trim().Replace("'", "''") + "' AS DATE), 101))");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND convert(datetime,CONVERT(VARCHAR(10),WorkingDate,101)) <=  convert(datetime,CONVERT(VARCHAR(10), CAST('" + parameters[1].ToString().Trim().Replace("'", "''") + "' AS DATE), 101))");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND EmployeeWorkingHrDetails.empl_code =  '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");

            sql.Append(" GROUP BY MasterEmployee.empl_code,(first_name+' '+middle_name+' '+last_name) ,isnull(MasterEmployeeIncomes.inc_code,''),isnull(MasterEmployeeIncomes.inc_rate,0)");

            //isnull(Remark,''),  Remark removed from group before status 
            sql.Append(" order by MasterEmployee.empl_code");



            return sql.ToString();
        }

        public string PRINT_QUERY_RPT(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT isnull(EmpWorkingHrDetailsId,0)EmpWorkingHrDetailsId,isnull(EmployeeWorkingHr.EmpWorkingHrId,0)EmpWorkingHrId,MasterEmployee.empl_code,(first_name+' '+middle_name+' '+last_name) EmpName,");
            sql.Append(" CAST(ISNULL(WorkingDate,'1900-01-01') AS DATETIME)WorkingDate,isnull(Remark,'')Remark,isnull(WorkingHr,0)WorkingHr,isnull(RgDayOvtm,0)RgDayOvtm ,isnull(PhOvtm,0)PhOvtm, ");
            sql.Append(" CAST(ISNULL(StartDate,'1900-01-01') AS DATETIME)StartDate,CAST(ISNULL(EndDate,'1900-01-01') AS DATETIME)EndDate, isnull(inc_rate,0)inc_rate, (isnull(WorkingHr,0)+isnull(RgDayOvtm,0)+isnull(PhOvtm,0))*isnull(inc_rate,0) Total,Soc_Sec_Num,isnull(Status,'')Status ");
            sql.Append(" From  MasterEmployee INNER JOIN EmployeeWorkingHrDetails ");
            sql.Append(" ON MasterEmployee.empl_code=EmployeeWorkingHrDetails.empl_code LEFT JOIN EmployeeWorkingHr ON EmployeeWorkingHr.EmpWorkingHrId=EmployeeWorkingHrDetails.EmpWorkingHrId ");
            sql.Append(" INNER JOIN MasterEmpTypeIncomes ON MasterEmpTypeIncomes.type_code=MasterEmployee.Type_Code Where 1 =1 AND terminated is null");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty && parameters[0].ToString()!="1/1/1900 12:00:00 AM")
                    sql.Append(" AND convert(datetime,CONVERT(VARCHAR(10),WorkingDate,101)) >= convert(datetime,CONVERT(VARCHAR(10), CAST('" + parameters[0].ToString().Trim().Replace("'", "''") + "' AS DATE), 101))");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != "1/1/1900 12:00:00 AM")
                    sql.Append(" AND convert(datetime,CONVERT(VARCHAR(10),WorkingDate,101)) <=  convert(datetime,CONVERT(VARCHAR(10), CAST('" + parameters[1].ToString().Trim().Replace("'", "''") + "' AS DATE), 101))");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND EmployeeWorkingHrDetails.empl_code =  '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");


            return sql.ToString();
        }

        public string PRINT_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT isnull(EmpWorkingHrDetailsId,0)EmpWorkingHrDetailsId,isnull(EmployeeWorkingHr.EmpWorkingHrId,0)EmpWorkingHrId,MasterEmployee.empl_code,(first_name+' '+middle_name+' '+last_name) EmpName,");
            sql.Append(" CAST(ISNULL(WorkingDate,'1900-01-01') AS DATETIME)WorkingDate,isnull(Remark,'')Remark,isnull(WorkingHr,0)WorkingHr,isnull(RgDayOvtm,0)RgDayOvtm ,isnull(PhOvtm,0)PhOvtm, ");
            sql.Append(" CAST(ISNULL(StartDate,'1900-01-01') AS DATETIME)StartDate,CAST(ISNULL(EndDate,'1900-01-01') AS DATETIME)EndDate,isnull(Status,'')Status ");
            sql.Append(" From  MasterEmployee INNER JOIN EmployeeWorkingHrDetails ");
            sql.Append(" ON MasterEmployee.empl_code=EmployeeWorkingHrDetails.empl_code LEFT JOIN EmployeeWorkingHr ON EmployeeWorkingHr.EmpWorkingHrId=EmployeeWorkingHrDetails.EmpWorkingHrId Where 1 =1 AND terminated is null");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND convert(datetime,CONVERT(VARCHAR(10),WorkingDate,101)) >= convert(datetime,CONVERT(VARCHAR(10), CAST('" + parameters[0].ToString().Trim().Replace("'", "''") + "' AS DATE), 101))");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND convert(datetime,CONVERT(VARCHAR(10),WorkingDate,101)) <=  convert(datetime,CONVERT(VARCHAR(10), CAST('" + parameters[1].ToString().Trim().Replace("'", "''") + "' AS DATE), 101))");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND EmployeeWorkingHrDetails.empl_code =  '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");


            return sql.ToString();
        }

        public string FIND_EmpTcardID_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT EmpTcardID");
            sql.Append(" From  EmployeeTCard_Header  ");
            sql.Append(" Where 1 =1 ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND convert(datetime,CONVERT(VARCHAR(10),start_date,101)) >= convert(datetime,CONVERT(VARCHAR(10), CAST('" + parameters[0].ToString().Trim().Replace("'", "''") + "' AS DATE), 101))");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND convert(datetime,CONVERT(VARCHAR(10),end_date,101)) <=  convert(datetime,CONVERT(VARCHAR(10), CAST('" + parameters[1].ToString().Trim().Replace("'", "''") + "' AS DATE), 101))");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND empl_code =  '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }

        public string FIND_MasterIncomeCodeSettings_QUERY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT incomecodesettingid,inc_code,applicable_for");
            sql.Append(" From  MasterIncomeCodeSettings  ");
            sql.Append(" Where 1 =1 ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND applicable_for =  '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
