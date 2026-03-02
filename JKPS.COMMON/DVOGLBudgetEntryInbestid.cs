using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOGLBudgetEntryInbestid : DVOBase
    {
        private int _Id;
        private string _Year;
        private string _Set;
        private int _Account;
        private string _AccountDescription;
        private int _AccountTypeId;
        private string _AccountKeyValue;
        private decimal _InitialRequest;
        private decimal _CurrentEstimate;
        private decimal _Approved;
        private decimal _Revised;
        private decimal _AllocateToDate;
        private decimal _AllocPercent;
        private decimal _ExtraFunds;
        private decimal _Projected;
        private decimal _NextYearProjected;
        private string _AccountType;

        #region Constructor

        public DVOGLBudgetEntryInbestid()
        {
            _Id = 0;
            _Year = string.Empty;
            _Set = string.Empty;
            _Account = 0;
            _AccountDescription = string.Empty;
            _AccountTypeId = 0;
            _AccountKeyValue = string.Empty;
            _InitialRequest = 0;
            _CurrentEstimate = 0;
            _Approved = 0;
            _Revised = 0;
            _AllocateToDate = 0;
            _AllocPercent = 0;
            _ExtraFunds = 0;
            _Projected = 0;
            _NextYearProjected = 0;
            _AccountType = string.Empty;
        }

        #endregion Constructor

        #region Public Property

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public string Set
        {
            get { return _Set; }
            set { _Set = value; }
        }

        public int Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

        public string AccountDescription
        {
            get { return _AccountDescription; }
            set { _AccountDescription = value; }
        }
        public string AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }

        public int AccountTypeId
        {
            get { return _AccountTypeId; }
            set { _AccountTypeId = value; }
        }

        public string AccountKeyValue
        {
            get { return _AccountKeyValue; }
            set { _AccountKeyValue = value; }
        }

        public decimal InitialRequest
        {
            get { return _InitialRequest; }
            set { _InitialRequest = value; }
        }

        public decimal CurrentEstimate
        {
            get { return _CurrentEstimate; }
            set { _CurrentEstimate = value; }
        }

        public decimal Approved
        {
            get { return _Approved; }
            set { _Approved = value; }
        }

        public decimal Revised
        {
            get { return _Revised; }
            set { _Revised = value; }
        }

        public decimal AllocateToDate
        {
            get { return _AllocateToDate; }
            set { _AllocateToDate = value; }
        }

        public decimal AllocPercent
        {
            get { return _AllocPercent; }
            set { _AllocPercent = value; }
        }

        public decimal ExtraFunds
        {
            get { return _ExtraFunds; }
            set { _ExtraFunds = value; }
        }

        public decimal Projected
        {
            get { return _Projected; }
            set { _Projected = value; }
        }

        public decimal NextYearProjected
        {
            get { return _NextYearProjected; }
            set { _NextYearProjected = value; }
        }

        #endregion Public Property

        #region Procedures

        public string GET_BUDGET_ADJUSTMENTS
        {
            get { return "uspGLBgtAdjstGet"; }//uspglbgtadjstget
        }

        public override string INSERT_SPNAME
        {
            get { return "uspGLBgtEntrIns"; }//uspglbgtentrins
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspGLBgtEntrUpd"; }//uspglbgtentrupd
        }

        public override string DELETE_SPNAME
        {
            get { return "uspGLBgtEntrDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspGLBgtEntrGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "inbestid"; }
        }

        public override int UNIQUE_ID
        {
            get { return _Id; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ibi.id  p_id, ibi.year  p_year, ibi.set  p_set, ibi.account  p_account, ");
            sql.Append(" ibi.initialrequest  p_initialrequest, ibi.currestimate  p_currestimate,");
            sql.Append(" ibi.approved  p_approved, ibi.revised  p_revised,");
            sql.Append(" ibi.allocatedtodate  p_allocatedtodate, ibi.allocpercent  p_allocpercent,");
            sql.Append(" ibi.extra_funds  v_extra_funds, ibi.projected  p_projected, ");
            sql.Append(" ibi.nextyearprojected  p_nxtyrprojected, tr.acct_desc  p_acounttdesc,tr.keyvalue v_keyvalue,kh.id v_accounttypeid");
            sql.Append(" FROM inbestid ibi, PayrollGLAccounts tr,Flex_struct_Header kh");
            sql.Append(" WHERE ibi.account = tr.acct_no AND Rtrim(tr.acct_type)=trim(kh.accounttype)");

            if (Convert.ToInt32(parameters[0]) > 0)//Id
                sql.Append(" AND ibi.id = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)//Year
                    sql.Append(" AND ibi.year = '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)//Set
                    sql.Append(" AND ibi.set = '" + parameters[2].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[3]) > 0)//Account
                sql.Append(" AND ibi.account = " + parameters[3].ToString());
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty && parameters[1] != null)//AccountDescription
                    sql.Append(" AND tr.acct_desc = '" + parameters[4].ToString().Replace("'", "''") + "'");
            if (Convert.ToDouble(parameters[5]) > 0)//InitialRequest
                sql.Append(" AND ibi.initialrequest  = " + parameters[5].ToString());
            if (Convert.ToDouble(parameters[6]) > 0)//CurrentEstimate
                sql.Append(" AND ibi.currestimate = " + parameters[6].ToString());
            if (Convert.ToDouble(parameters[7]) > 0)//Approved
                sql.Append(" AND ibi.approved = " + parameters[7].ToString());
            if (Convert.ToDouble(parameters[8]) > 0)//Revised
                sql.Append(" AND ibi.revised = " + parameters[8].ToString());
            if (Convert.ToDouble(parameters[9]) > 0)//AllocateToDate
                sql.Append(" AND ibi.allocatedtodate = " + parameters[9].ToString());
            if (Convert.ToDouble(parameters[10]) > 0)//AllocPercent
                sql.Append(" AND ibi.allocpercent = " + parameters[10].ToString());
            if (Convert.ToDouble(parameters[11]) > 0)//Projected
                sql.Append(" AND ibi.projected = " + parameters[11].ToString());
            if (Convert.ToDouble(parameters[12]) > 0)//NextYearProjected
                sql.Append(" AND ibi.nextyearprojected = " + parameters[12].ToString());

            if (parameters[13] != null)
                if (parameters[13].ToString() != string.Empty)//Accountkeyvalue
                    sql.Append(" AND tr.keyvalue LIKE '" + parameters[13].ToString().Replace("'", "''") + "%'");
            if (parameters[13] != null)
                if (parameters[14].ToString() != string.Empty)//AccountType
                    sql.Append(" AND tr.acct_type LIKE '" + parameters[14].ToString().Replace("'", "''") + "'");
            sql.Append(" Order by ibi.id");

            return sql.ToString();
        }

        #endregion Procedures
    }
}
