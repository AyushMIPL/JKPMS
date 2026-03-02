using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOFiscalPrepaired : DVOBase
    {
        private int _RowID;
        private string _acct_type;
        private string _objCode;
        private string _dtlObjCode;
        private string _objDesc;
        private string _dtlObjDesc;
        private decimal _budget_year;
        private decimal _actual_Prev_Year;
        private decimal _expected_month;
        private string _period_month;
        private string _period_year;
        private decimal _atl_month;
        private decimal _btl_month;
        private decimal _actual_month;

        private string _maincatdesc;
        private string _maincatPrintodr;
        private string _catdesc;
        private string _catprintodr;
        private string _subcatdesc;
        private string _subcatprintodr;

        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;


        public DVOFiscalPrepaired()
        {
            _RowID=0;
            _acct_type = string.Empty;
            _objCode=string.Empty;
            _dtlObjCode = string.Empty;
            _objDesc = string.Empty;
            _dtlObjDesc = string.Empty;
            _budget_year=0.0M;
            _actual_Prev_Year = 0.0M;
            _expected_month = 0.0M;
            _period_month = string.Empty;
            _period_year = string.Empty;
            _atl_month = 0.0M;
            _btl_month = 0.0M;
            _actual_month = 0.0M;

            _maincatdesc = string.Empty;
            _maincatPrintodr = string.Empty;
            _catdesc = string.Empty;
            _catprintodr = string.Empty;
            _subcatdesc = string.Empty;
            _subcatprintodr = string.Empty;

            _InsertMachineInfo = string.Empty;
            _InsertDate = string.Empty;
            _InsertBy = 0;

            _UpdateMachineInfo = string.Empty;
            _UpdateDate = string.Empty;
            _UpdateBy = 0;
        }

        # region Properties
        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }

        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string objCode
        {
            get { return _objCode; }
            set { _objCode = value; }
        }
        public string dtlObjCode
        {
            get { return _dtlObjCode; }
            set { _dtlObjCode = value; }
        }
        public string objDesc
        {
            get { return _objDesc; }
            set { _objDesc = value; }
        }
        public string dtlObjDesc
        {
            get { return _dtlObjDesc; }
            set { _dtlObjDesc = value; }
        }
        public decimal budget_year
        {
            get { return _budget_year; }
            set { _budget_year = value; }
        }
        public decimal actual_Prev_Year
        {
            get { return _actual_Prev_Year; }
            set { _actual_Prev_Year = value; }
        }
        public decimal expected_month
        {
            get { return _expected_month; }
            set { _expected_month = value; }
        }
        public string period_month
        {
            get { return _period_month; }
            set { _period_month = value; }
        }
        public string period_year
        {
            get { return _period_year; }
            set { _period_year = value; }
        }
        public decimal atl_month
        {
            get { return _atl_month; }
            set { _atl_month = value; }
        }
        public decimal btl_month
        {
            get { return _btl_month; }
            set { _btl_month = value; }
        }
        public decimal actual_month
        {
            get { return _actual_month; }
            set { _actual_month = value; }
        }

        public string maincatdesc
        {
            get { return _maincatdesc; }
            set { _maincatdesc = value; }
        }
        public string maincatPrintodr
        {
            get { return _maincatPrintodr; }
            set { _maincatPrintodr = value; }
        }
        public string catdesc
        {
            get { return _catdesc; }
            set { _catdesc = value; }
        }


        public string catprintodr
        {
            get { return _catprintodr; }
            set { _catprintodr = value; }
        }
        public string subcatdesc
        {
            get { return _subcatdesc; }
            set { _subcatdesc = value; }
        }
        public string subcatprintodr
        {
            get { return _subcatprintodr; }
            set { _subcatprintodr = value; }
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

       
        # endregion Properties

        #region StoredProcedure


        public override string INSERT_SPNAME
        {
            get { return "uspfisrecins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string DELETE_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string FIND_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string ALL_SPNAME
        {
            get { throw new Exception("The method or operation is not implemented."); }
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
            //StringBuilder sql = new StringBuilder();
            ////sql.Append("select recrevbymins1.ministry ministry1 ,recrevbymins1.budgetedamt,allministry.ministry,");
            ////sql.Append("allministry.desc description, recrevbymins1.budgetedyear,recrevbymins2.ministry ministry2,");
            ////sql.Append("recrevbymins2.description desc,recrevbymins2.p_yeartodate,");
            ////sql.Append("recrevbymins2.period_month,recrevbymins2.period_year");
            ////sql.Append(" FROM allministry, outer (recrevbymins2,recrevbymins1)");
            ////sql.Append(" where allministry.ministry= recrevbymins2.ministry");
            ////sql.Append(" and recrevbymins1.ministry=recrevbymins2.ministry");
            ////sql.Append(" and recrevbymins1.budgetedyear='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            ////sql.Append(" and recrevbymins2.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            ////sql.Append(" and recrevbymins2.period_year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

            ////sql.Append(" ORDER By allministry.ministry");
            ////return sql.ToString();
            //sql.Append("select PayrollGLAccounts.keyvalue[1,2] ministry,");
            //sql.Append(" Master_Segment.desc description , ");
            //sql.Append(" sum(balance) p_yeartodate ,sum(revised) budgetedamt ");
            //sql.Append(" from PayrollGLAccounts, outer stxchrtd, outer inbestid,Master_Segment ");
            //sql.Append(" where PayrollGLAccounts.acct_no =stxchrtd.acct_no");
            //sql.Append(" and PayrollGLAccounts.acct_no =account and ");
            //sql.Append(" year='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //sql.Append(" and segmentid=23");
            //sql.Append(" and PayrollGLAccounts.keyvalue[1,2] =Master_Segment.keyvalue");

            //sql.Append(" and stxchrtd.period_month='" + parameters[0].ToString().Trim().Replace("'", "''") + "' and stxchrtd.period_year ='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            //sql.Append(" and PayrollGLAccounts.acct_type='RECREV'");
            //sql.Append(" group by PayrollGLAccounts.keyvalue[1,2],Master_Segment.desc");
            //return sql.ToString();
            return "";
        }

        public string FIND_FISCAL_DATA
        {
            get { return "uspfisrecordget"; }
        }

        # endregion StoredProcedure
    }
}
