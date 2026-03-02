using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOBCBudgetSets : DVOBase
    {
        private int _rowid;
       
        private string _src_type;
        private string _src_key;
        private string _src_desc;
        private decimal _src_num_desc;
        private string _src_char_desc;
        //*****************For budget entries **********************//
        private string _year;
        private string _set;
        private int _account;
        private decimal _initialrequest;
        private decimal _currestimate;
        private decimal _approved;
        private decimal _revised;
        private decimal _allocatedtodate;
        private decimal _allocpercent;
        private decimal _extra_funds;
        private decimal _projected;
        private decimal _nextyearprojected;
        //****************************End ***********************//
        //Added By Rahul Jain on 09/07/2009
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;
        private int _src_acct_no;

        #region Constructor

        public DVOBCBudgetSets()
        {
            _rowid = 0;
            
            _src_type = string.Empty;
            _src_key = string.Empty;
            _src_desc = string.Empty;
            _src_num_desc = 0;
            _src_char_desc = string.Empty;

            //***************************This is for Budget Entries********************//
            _year = string.Empty;
            _set = string.Empty;
            _account = 0;
            _initialrequest = 0;
            _currestimate = 0;
            _approved = 0;
            _revised = 0;
            _allocatedtodate = 0;
            _allocpercent = 0;
            _extra_funds = 0;
            _projected = 0;
            _nextyearprojected = 0;
            //***************************    ****************************************//
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;
            _src_acct_no = 0;
        }

        #endregion Constructor

        #region Public Properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string src_type
        {
            get { return _src_type; }
            set { _src_type = value; }
        }
        public string src_key
        {
            get { return _src_key; }
            set { _src_key = value; }
        }

        public string src_desc
        {
            get { return _src_desc; }
            set { _src_desc = value; }
        }
        public decimal src_num_desc
        {
            get { return _src_num_desc; }
            set { _src_num_desc = value; }
        }
        public string src_char_desc
        {
            get { return _src_char_desc; }
            set { _src_char_desc = value; }
        }
        //****************for budget entries********************
        public string year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string set
        {
            get { return _set; }
            set { _set = value; }
        }
        public int account
        {
            get { return _account; }
            set { _account = value; }
        }
        public decimal initialrequest
        {
            get { return _initialrequest; }
            set { _initialrequest = value; }
        }

        public decimal currestimate
        {
            get { return _currestimate; }
            set { _currestimate = value; }
        }
        public decimal approved
        {
            get { return _approved; }
            set { _approved = value; }
        }
        public decimal revised
        {
            get { return _revised; }
            set { _revised = value; }
        }
        public decimal allocatedtodate
        {
            get { return _allocatedtodate; }
            set { _allocatedtodate = value; }
        }
        public decimal allocpercent
        {
            get { return _allocpercent; }
            set { _allocpercent = value; }
        }
        public decimal extra_funds
        {
            get { return _extra_funds; }
            set { _extra_funds = value; }
        }
        public decimal projected
        {
            get { return _projected; }
            set { _projected = value; }
        }
        public decimal nextyearprojected
        {
            get { return _nextyearprojected; }
            set { _nextyearprojected = value; }
        }

        //*******************end *********************************
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
        public int src_acct_no
        {
            get { return _src_acct_no ; }
            set { _src_acct_no = value; }
        }
        #endregion Public Properties

        #region Stored-Procedures

        public string GET_BUDGET_ENTRIES
        {
            get { return "uspbsbdgentget"; }
        }
        public string COPY_BUDGET_ENTRIES
        {
            get { return "uspbsbdgentcopy"; }
        }
        public string GET_ACCOUNT_TERMS
        {
            get { return "uspapatermsget"; }
        }

        public override string INSERT_SPNAME
        {
            get { return "uspbsbdgsetins"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspbsbdgsetupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspbsbdgsetdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspbsbdgsetget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspbsbdgsetgetall"; }
        }

        public override string TABLE_NAME
        {
            get { return "stxinfor"; }
        }

        public override int UNIQUE_ID
        {
            get { return _rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        //Added By Rahul jain 0n 09/07/2009 using in inventory module
        public  string INSERT_COMMISSION
        {
            get { return "uspinvcommins"; }
        }
        public  string UPDATE_COMMISSION
        {
            get { return "uspinvcommiupd"; }
        }
        //************************************************************

        //Added by Sunil Pahwa on 21/7/09 using salesmans summary ****
        public string GET_SRC_DESC
        {
            get { return "uspsalpersonkget"; }
        }
       
        
        //***********************************************************

        public string GET_ORDER_ENTRY_EDIT_INFO
        {
            get { return "uspordrsinforget"; }
        }
        public string GET_INFO_FOR_ORDER_ENTRY
        {
            get { return "uspordentinforget"; }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  rowid,src_key, src_desc, src_num_desc, src_char_desc");
            sql.Append(" FROM  stxinfor where 1=1");

            if (parameters[1] != null)//src_type
                if (parameters[1].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(src_type) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)//src_key
                if (parameters[2].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(src_key) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)//src_desc
                if (parameters[3].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(src_desc) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)//src_num_desc
                if (Convert.ToDecimal(parameters[4]) != 0)
                    sql.Append(" and src_num_desc = " + parameters[4]);

            //Added By Sunil Pahwa
            if (Convert.ToInt32(parameters[0]) != 0)
                sql.Append(" AND rowid = " + parameters[0].ToString());
            //********************
            sql.Append(" order by src_key");

            return sql.ToString();
        }


        //Added By Rajeev
        //Addition Date : 12/07/2009
        //Aim :To Bind the Sales Person DDl On Customer Information

        public string Get_SalesPersonCodeDesc
        {
            get { return "uspodrslsperget"; }
        }
        #endregion Stored-Procedures
    }
}
