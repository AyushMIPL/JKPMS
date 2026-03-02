using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    /// Implemented by : sanjay chawla
    /// Date : 17 August 2009
    /// Description :common class for Treasury Bill Schemes
    /// Modified Date : 
    /// Description : 
    /// </summary>
    public class DVOTBSchemes : DVOBase
    {

        private int _Rowid;       
        private int _tbschid;
        private string _tbschname;    
        private string _tbschdesc;
        private string _startdate;
        private decimal _minimum_amt;
        private decimal _maximum_amt;
        private int _schdays;
        private string _processstart;
        private decimal _amt_per_100 ;
        private string _enddate;
        private int _maxmembers;
        private string _status;
        private int _Insertby;
        private string _InsertDate;
        private string _InsertMachineInfo;
        private int _UpdateBy;
        private string _Updatedate;
        private string _UpdateMachineInfo;

        private int _bank_acct_no;
        private string _bank_acct_no_keyvalue;
        private string _bank_acct_no_accounttype;
        private int _deposit_acct_no;
        private string _deposit_acct_no_keyvalue;
        private string _deposit_acct_no_accounttype;
        private int _interest_acct_no;
        private string _interest_acct_no_keyvalue;
        private string _interest_acct_no_accounttype;
        private int _pay_acct_no;
        private string _pay_acct_no_keyvalue;
        private string _pay_acct_no_accounttype;

        #region Constructor
        public DVOTBSchemes()
        {
            _Rowid = 0;                             
            _tbschid=0;
            _tbschname=string.Empty;    
            _tbschdesc=string.Empty;
            _startdate=string.Empty;
            _minimum_amt=0;
            _maximum_amt=0;
            _schdays=0;
            _processstart=string.Empty;
            _amt_per_100 =0;
            _enddate=string.Empty;
            _maxmembers=0;
            _status=string.Empty;
            _Insertby=0;
            _InsertDate=string.Empty;
            _InsertMachineInfo=string.Empty;
            _UpdateBy=0;
            _Updatedate=string.Empty;
            _UpdateMachineInfo=string.Empty;

            _bank_acct_no = 0;
            _bank_acct_no_keyvalue = string.Empty;
            _bank_acct_no_accounttype = string.Empty;
            _deposit_acct_no = 0;
            _deposit_acct_no_keyvalue = string.Empty;
            _deposit_acct_no_accounttype = string.Empty;
            _interest_acct_no = 0;
            _interest_acct_no_keyvalue = string.Empty;
            _interest_acct_no_accounttype = string.Empty;
            _pay_acct_no = 0;
            _pay_acct_no_keyvalue = string.Empty;
            _pay_acct_no_accounttype = string.Empty;
        }
        #endregion Constructor

        #region public properties
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }
        public int tbschid
        {
            get { return _tbschid; }
            set { _tbschid = value; }
        }
        public string tbschname
        {
            get { return _tbschname; }
            set { _tbschname = value; }
        }
        public string tbschdesc
        {
            get { return _tbschdesc; }
            set { _tbschdesc = value; }
        }
        public string startdate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }
        public decimal minimum_amt
        {
             get { return _minimum_amt; }
             set { _minimum_amt = value; }
        }
        public decimal maximum_amt
        {
            get { return _maximum_amt; }
            set { _maximum_amt = value; }
        }
        public int schdays
        {
            get { return _schdays; }
            set { _schdays = value; }
        }
        public string processstart
        {
            get { return _processstart; }
            set { _processstart = value; }
        }
        public decimal amt_per_100
        {
            get { return _amt_per_100; }
            set { _amt_per_100 = value; }
        }
        public string enddate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }
        public int maxmembers
        {
            get { return _maxmembers; }
            set { _maxmembers = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public int Insertby
        {
            get { return _Insertby; }
            set { _Insertby = value; }
        }
        public string InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }
        public string InsertMachineInfo
        {
            get { return _InsertMachineInfo; }
            set { _InsertMachineInfo = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public string Updatedate
        {
            get { return _Updatedate; }
            set { _Updatedate = value; }
        }
        public string UpdateMachineInfo
        {
            get { return _UpdateMachineInfo; }
            set { _UpdateMachineInfo = value; }
        }
        public int bank_acct_no
        {
            get { return _bank_acct_no; }
            set { _bank_acct_no = value; }
        }
        public string bank_acct_no_keyvalue
        {
            get { return _bank_acct_no_keyvalue; }
            set { _bank_acct_no_keyvalue = value; }
        }
        public string bank_acct_no_accounttype
        {
            get { return _bank_acct_no_accounttype; }
            set { _bank_acct_no_accounttype = value; }
        }
        public int deposit_acct_no
        {
            get { return _deposit_acct_no; }
            set { _deposit_acct_no = value; }
        }
        public string deposit_acct_no_keyvalue
        {
            get { return _deposit_acct_no_keyvalue; }
            set { _deposit_acct_no_keyvalue = value; }
        }
        public string deposit_acct_no_accounttype
        {
            get { return _deposit_acct_no_accounttype; }
            set { _deposit_acct_no_accounttype = value; }
        }

        public int interest_acct_no
        {
            get { return _interest_acct_no; }
            set { _interest_acct_no = value; }
        }
        public string interest_acct_no_keyvalue
        {
            get { return _interest_acct_no_keyvalue; }
            set { _interest_acct_no_keyvalue = value; }
        }
        public string interest_acct_no_accounttype
        {
            get { return _interest_acct_no_accounttype; }
            set { _interest_acct_no_accounttype = value; }
        }
        public int pay_acct_no
        {
            get { return _pay_acct_no; }
            set { _pay_acct_no = value; }
        }
        public string pay_acct_no_keyvalue
        {
            get { return _pay_acct_no_keyvalue; }
            set { _pay_acct_no_keyvalue = value; }
        }
        public string pay_acct_no_accounttype
        {
            get { return _pay_acct_no_accounttype; }
            set { _pay_acct_no_accounttype = value; }
        }
        #endregion public properties
        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "usp_ins_trea_schem"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usp_upd_trea_schem"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "usp_del_tbschemes"; }
        }
       
        public override string FIND_SPNAME
        {
            get { return " "; }
        }
        //Added by Sunil Pahwa on 24/10/09 for Control Form  adding MCGDEfault Schema Field
        public override string ALL_SPNAME
        {
            get { return "uspdefsalschgetall"; }
        }       
        public override string TABLE_NAME
        {
            get { return "tbschemes"; }
        }
        public override int UNIQUE_ID
        {
            get { return _Rowid;}
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }


        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select tbschemes.rowid,tbschemes.tbschid,tbschemes.tbschname,tbschemes.tbschdesc,tbschemes.startdate,tbschemes.minimumamount, ");
            sql.Append(" tbschemes.maximumamount,tbschemes.schdays,tbschemes.processstart,tbschemes.amt_per_100,tbschemes.schemeendson, ");
            sql.Append(" tbschemes.maxmembers,tbschemes.status,tbschemes.bank_acct_no,tbschemes.deposit_acct_no, ");
            sql.Append(" tbschemes.interest_acct_no,tbschemes.pay_acct_no, ");
            sql.Append(" s1.acct_type as BankAcctType,s2.acct_type as DepoAcctType,s3.acct_type as IntAcctType, s4.acct_type as PayAcctType, ");
            sql.Append(" s1.keyvalue as Bankkeyvalue,s2.keyvalue as Depokeyvalue,s3.keyvalue as Intkeyvalue, s4.keyvalue as Paykeyvalue ");
            sql.Append(" from tbschemes ,outer(PayrollGLAccounts s1,PayrollGLAccounts s2,PayrollGLAccounts s3,PayrollGLAccounts s4) ");
            sql.Append(" where tbschemes.bank_acct_no=s1.acct_no ");
            sql.Append(" AND tbschemes.deposit_acct_no=s2.acct_no");
            sql.Append(" AND tbschemes.interest_acct_no=s3.acct_no ");
            sql.Append(" AND tbschemes.pay_acct_no=s4.acct_no ");

            if (Convert.ToInt32(parameters[0]) > 0)//rowid
                sql.Append(" AND tbschemes.rowid =" + parameters[0].ToString());
            if (parameters[1].ToString() != string.Empty && parameters[1].ToString() != null)//tbschname
                sql.Append(" AND tbschemes.tbschname like '" + parameters[1].ToString().Replace("'", "''") + "'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//tbschdesc
                sql.Append(" AND tbschemes.tbschdesc like '" + parameters[2].ToString().Replace("'", "''") + "%'");
            if (parameters[3].ToString() != string.Empty && parameters[3].ToString() != null)//startdate
                sql.Append(" AND tbschemes.startdate >= '" + parameters[3].ToString().Replace("'", "''") + "'");
            if (parameters[4].ToString() != string.Empty && parameters[4].ToString() != null)//schemeendson
                sql.Append(" AND tbschemes.schemeendson <= '" + parameters[4].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[5]) > 0)//schdays
                sql.Append(" AND tbschemes.schdays =" + parameters[5].ToString());
            if (Convert.ToInt32(parameters[6]) > 0)//minimumamount
                sql.Append(" AND tbschemes.minimumamount =" + parameters[6].ToString());
            if (Convert.ToInt32(parameters[7]) > 0)//maximumamount
                sql.Append(" AND tbschemes.maximumamount =" + parameters[7].ToString());
            if (parameters[8].ToString() != string.Empty && parameters[8].ToString() != null)//processstart
                sql.Append(" AND tbschemes.processstart = '" + parameters[8].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[9]) > 0)//schdays
                sql.Append(" AND tbschemes.amt_per_100 =" + parameters[9].ToString());
            if (Convert.ToInt32(parameters[10]) > 0)//schdays
                sql.Append(" AND tbschemes.maxmembers =" + parameters[10].ToString());
            if (parameters[11].ToString() != string.Empty && parameters[11].ToString() != null)//tbschname
                sql.Append(" AND tbschemes.status like '" + parameters[11].ToString().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[12]) > 0)//schemeid
                sql.Append(" AND tbschemes.tbschid =" + parameters[12].ToString());
            if (Convert.ToInt32(parameters[13]) > 0)//bank_acct_no
                sql.Append(" AND tbschemes.bank_acct_no =" + parameters[13].ToString());
            if (Convert.ToInt32(parameters[14]) > 0)//deposit_acct_no
                sql.Append(" AND tbschemes.deposit_acct_no =" + parameters[14].ToString());
            if (Convert.ToInt32(parameters[15]) > 0)//interest_acct_no
                sql.Append(" AND tbschemes.interest_acct_no =" + parameters[15].ToString());
            if (Convert.ToInt32(parameters[16]) > 0)//pay_acct_no
                sql.Append(" AND tbschemes.pay_acct_no =" + parameters[16].ToString());
            return sql.ToString();
        }

        public string FIND_ISSUE_BY_SCHEME(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(*) Exists ");
            sql.Append(" from tbissuer where 1=1 ");
            if(parameters[0] != null)
            if (Convert.ToInt32(parameters[0]) > 0)//schemeid
                sql.Append(" AND tbissuer.tbschid =" + parameters[0].ToString());
            return sql.ToString();
        }
       
        #endregion store-procedures
    }     
}

