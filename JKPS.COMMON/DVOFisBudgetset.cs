using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOFisBudgetset : DVOBase
    {
        private Int32 _budgetid;
        private string _acct_type;
        private string _lmonth;
        private string _lyear;
        private Int32 _catid;
        private Int32 _linkid;
        private decimal _lyrevised;
        private decimal _lymbudget;
        private string _cmonth;
        private string _cyear;
        private decimal _cmbudget;

        private int _RowID;
        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;   

        public DVOFisBudgetset()
        {
            _budgetid=0;
            _acct_type = string.Empty;
            _lmonth =string.Empty;
            _lyear =string.Empty;
            _catid =0;
            _linkid =0;
            _lyrevised =0.0M;
            _lymbudget =0.0M;
            _cmonth =string.Empty;
            _cyear =string.Empty;
            _cmbudget =0.0M;
            _RowID = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = string.Empty;
            _InsertBy = 0;

            _UpdateMachineInfo = string.Empty;
            _UpdateDate = string.Empty;
            _UpdateBy = 0;
        }

        #region StartProperties
        public Int32 budgetid
         {
             get { return _budgetid; }
             set { _budgetid = value; }
        }

        public string acct_type
         {
             get { return _acct_type; }
             set { _acct_type = value; }
        }
        public string lmonth
         {
             get { return _lmonth; }
             set { _lmonth = value; }
        }
        public string lyear
         {
             get { return _lyear; }
             set { _lyear = value; }
        }
        public Int32 catid
         {
             get { return _catid; }
             set { _catid = value; }
        }
        public Int32 linkid
         {
             get { return _linkid; }
             set { _linkid = value; }
        }
        public decimal lyrevised
         {
             get { return _lyrevised; }
             set { _lyrevised = value; }
        }
        public decimal lymbudget
         {
             get { return _lymbudget; }
             set { _lymbudget = value; }
        }
        public string cmonth
         {
             get { return _cmonth; }
             set { _cmonth = value; }
        }
        public string cyear
         {
             get { return _cyear; }
             set { _cyear = value; }
        }
        public decimal cmbudget
        {
            get { return _cmbudget; }
            set { _cmbudget = value; }
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

        #endregion StartProperties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspbuddins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspbuddupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspbudddel"; }
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
            get { return "fisbudd"; }
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
            sql.Append("select Rowid,budgetid,acct_type,lmonth,lyear,catid,linkid,lyrevised,lymbudget,cmonth,cyear,cmbudget,");
            sql.Append("insertmachineinfo,insertdate,insertby,updatemachineinfo,updatedate,updateby from fisbudd");
            sql.Append(" WHERE 1=1");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(acct_type) LIKE '%" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");                     
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) != 0)
                    sql.Append(" AND catid=" + Convert.ToInt32(parameters[1]));
            if (parameters[2] != null)
                if (Convert.ToInt32(parameters[2]) != 0)
                    sql.Append(" AND linkid=" + Convert.ToInt32(parameters[2]));
            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) != 0)
                    sql.Append(" AND Rowid=" + Convert.ToInt32(parameters[3]));
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND lmonth=" + parameters[4].ToString().Trim().Replace("'", "''"));
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND lyear=" + parameters[5].ToString().Trim().Replace("'", "''"));
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND cmonth=" + parameters[6].ToString().Trim().Replace("'", "''"));
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND cyear=" + parameters[7].ToString().Trim().Replace("'", "''"));
            if (parameters[8] != null)
                if (Convert.ToDecimal(parameters[8]) != 0)
                    sql.Append(" AND cmbudget=" + Convert.ToInt32(parameters[8]));

            return sql.ToString();
        }

        public string FIND_MBUDGETAMOUNT
        {
            get { return "fisbudmget"; }
        }
      

        #endregion Stored-Procedures

    }
}
