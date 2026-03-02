using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    ///<Development and modification Details>
    /// *************Aim***************************** Developed By(Modified By)******************** DevelopmentDate(Modified Date)
    ///1.)     DVO For Fiscal  Linker                Rajeev(D)                                 08/06/2009(DD)
    ///2.) 
    ///<summery>
    public class DVOFiscalLinker : DVOBase
    {
        private int _linkid;
        private string _period_month;
        private string _period_year;
        private int _mcatid;
        private int _catid;
        private int _subcatid;
        private string _description;
        private Int32 _order;

        private int _RowID;
        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;

        private string _keyvalue;

        private string _ToPeroidMonth;
        private string _ToPeroidYear;

        //Added By Rajeev
        //Aim : To get the Monthly and Yearly Budget
        private decimal _Ybedget;
        private decimal _Mbudget;

        string _showdtlonrpt;


        //Constructor to Assign the initial value to the decleared variable
        public DVOFiscalLinker()
        {
            _linkid = 0;
            _period_month = string.Empty;
            _period_year = string.Empty;
            _mcatid = 0;
            _catid = 0;
            _subcatid = 0;
            _description = string.Empty;
            _order = 0;

            _RowID = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";
            _InsertBy = 0;

            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";
            _UpdateBy = 0;

            _ToPeroidMonth = string.Empty;
            _keyvalue = string.Empty;
            _ToPeroidYear = string.Empty;

            _showdtlonrpt = string.Empty;
        }

        #region StartProperties

        public int linkid
        {
            get { return _linkid; }
            set { _linkid = value; }
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
        public int mcatid
        {
            get { return _mcatid; }
            set { _mcatid = value; }
        }
        public int catid
        {
            get { return _catid; }
            set { _catid = value; }
        }
        public int subcatid
        {
            get { return _subcatid; }
            set { _subcatid = value; }
        }

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Int32 order
        {
            get { return _order; }
            set { _order = value; }
        }

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
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

        public string ToPeroidMonth
        {
            get { return _ToPeroidMonth; }
            set { _ToPeroidMonth = value; }
        }

        public string ToPeroidYear
        {
            get { return _ToPeroidYear; }
            set { _ToPeroidYear = value; }
        }
        public string keyvalue
        {
            get { return _keyvalue; }
            set { _keyvalue = value; }
        }

        public decimal Ybedget
        {
            get { return _Ybedget; }
            set { _Ybedget = value; }
        }

        public decimal Mbudget
        {
            get { return _Mbudget; }
            set { _Mbudget = value; }
        }

        public string showdtlonrpt
        {
            get { return _showdtlonrpt; }
            set { _showdtlonrpt = value; }
        }


        #endregion StartProperties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspfislinkins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspfislinkupd"; }
        }

        public string UPDATE_FISCAL_MONTHLY_BUDGET
        {
            get { return "uspfismnthbgt"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspfislikdel"; } //delete by linkid
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
            get { return "fislinker"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,linkid,period_month,period_year,mcatid,catid,subcatid,description,order,InsertMachineInfo,InsertDate,InsertBy,UpdateMachineInfo,");
            sql.Append(" UpdateDate,UpdateBy,showdtlonrpt from fislinker");
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) != 0)
                    sql.Append(" AND catid=" + Convert.ToInt32(parameters[0]));
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) != 0)
                    sql.Append(" AND mcatid=" + Convert.ToInt32(parameters[1]));
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) != 0)
                    sql.Append(" AND subcatid=" + Convert.ToInt32(parameters[2]));
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND period_month=" + parameters[3].ToString().Trim().Replace("'", "''"));
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND period_year=" + parameters[4].ToString().Trim().Replace("'", "''"));
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(description) LIKE '%" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (Convert.ToInt32(parameters[6]) != 0)
                    sql.Append(" AND order=" + Convert.ToInt32(parameters[6]));
            if (parameters[7] != null)
                if (Convert.ToInt32(parameters[7]) != 0)
                    sql.Append(" AND Rowid=" + Convert.ToInt32(parameters[7]));

            return sql.ToString();
        }



        public string GET_LINKER_ADD_DETILS
        {
            get { return "usplinkeraddget"; }
        }

        public string GET_BALANCE_DETILS
        {
            get { return "uspfisbalancget"; }
        }

        public string GET_BUDGET_DETILS
        {
            get { return "uspfisbudgetget"; }
        }

        public string CHECK_DELETEDATA
        {
            get { return "uspfischkdel"; }
        }

        public string FIND_OBJECT_DTL_DESCRIPTION_BY_ACCOUNTTYPE
        {
            get { return "uspcatdtlactget"; }
        }

        public string FIND_YEARLY_AND_MONTHLY_BUDGET
        {
            get { return "uspymbudget"; }
        }

        public string Find_next_order
        {
            get { return "uspfisnxtodrget"; }
        }

        public string Find_Fiscal_data_for_Processing(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT fl.RowID,fl.linkid,fl.period_month,fl.period_year,fl.mcatid,fl.catid,fl.subcatid,fl.description,fl.order,");
            sql.Append("  fm.mcatdesc,fm.mcatprintodr,   fc.catdesc,fc.catprintodr, fsub.scatdesc,fsub.scatprintodr,fl.monthbudget FROM fislinker fl,");
            sql.Append(" fismcat fm ,outer fiscat fc,outer fissubcat fsub where      fl.mcatid=fm.mcatid and      fl.catid=fc.catid and     fl.subcatid=fsub.subcatid ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND period_month >='" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND period_month <='" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND  fl.period_year='" + parameters[2].ToString() + "'");
            return sql.ToString();

        }

        public string FIND_FISCAL_LINKER_DETAIL_FOR_TRANSFER(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,linkid,period_month,period_year,mcatid,catid,subcatid,description,order,InsertMachineInfo,InsertDate,InsertBy,UpdateMachineInfo,");
            sql.Append(" UpdateDate,UpdateBy from fislinker");
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND period_month=" + parameters[0].ToString().Trim().Replace("'", "''"));
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND period_year=" + parameters[1].ToString().Trim().Replace("'", "''"));


            return sql.ToString();
        }
        public string Find_Fiscal_Linker_Details(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("Select linkid,acct_type ,keyvalue,addsubstatus, insertmachineinfo,insertdate,insertby,updatemachineinfo,updatedate,updateby");
            sql.Append(" from Fiskeyvaladd");
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND linkid=" + parameters[0].ToString().Trim().Replace("'", "''"));

            return sql.ToString();
        }
        public string FIND_FISCAL_LINKER_DETAIL_BY_IDS(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select rowid,linkid,period_month,period_year,mcatid,catid,subcatid,description,order,InsertMachineInfo,InsertDate,InsertBy,UpdateMachineInfo,");
            sql.Append(" UpdateDate,UpdateBy from fislinker");
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND period_month=" + parameters[0].ToString().Trim().Replace("'", "''"));
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND period_year=" + parameters[1].ToString().Trim().Replace("'", "''"));
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) != 0)
                    sql.Append(" AND mcatid=" + Convert.ToInt32(parameters[2]));
            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) != 0)
                    sql.Append(" AND catid=" + Convert.ToInt32(parameters[3]));
            if (parameters[4] != null)
                if (Convert.ToInt32(parameters[4]) != 0)
                    sql.Append(" AND subcatid=" + Convert.ToInt32(parameters[4]));

            return sql.ToString();
        }

        /// <summary>
        /// Properies used to insert changes data in temp linker table
        /// </summary> 

        public string INSERT_TEMP_TABLE_LINKERDATA
        {
            get { return "uspfisltmpins"; }
        }

        public string Find_next_order_TEMP
        {
            get { return "uspnxtodrtget"; }
        }

        public string GET_LINKER_BY_LINKID
        {
            get { return "usplkrbylidget"; }
        }

        public string CHECK_LINKER_TEMP_RECORD
        {
            get { return "uspchktemprec"; }
        }

        public string GET_TEMP_LINKER_BY_LINKID
        {
            get { return "usplkrtmpget"; }
        }

        public string DELETE_TEMP_TABLE_DATA
        {
            get { return "usptempdel"; }
        }

        //Added By rajeev On 20/09/09
        public string GET_MONTHLY_ACTIVITY_AMOUNT
        {
            get { return "uspactivityget"; }
        }

        public string GET_YEARLY_BALANCE_AMOUNT
        {
            get { return "uspbalanceget"; }
        }

        #endregion Stored-Procedures
    }
}
