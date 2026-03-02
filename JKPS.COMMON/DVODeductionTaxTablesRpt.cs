using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
   public class DVODeductionTaxTablesRpt:DVOBase
    {
    private int _RowID;
        private string _tax_year;
        private string _ded_code;
        private decimal  _week_allow; 
        private decimal  _biweek_allow; 
        private decimal  _smonth_allow; 
        private decimal  _month_allow; 
        private decimal  _quarter_allow; 
        private decimal  _syear_allow; 

        private decimal  _year_allow;
        private decimal _misc_allow;
        private string _pay_period;
        private string _marital_stat;
        private decimal _over_amt;
        private decimal _base_amt;
        private decimal _tax_rate;
        private int _order_no;
        

        #region Constructor

        public DVODeductionTaxTablesRpt()
        {
            _RowID = 0;
            _tax_year=string.Empty;
            _ded_code=string.Empty;
            _week_allow = 0.0M;
            _biweek_allow = 0.0M;
            _smonth_allow = 0.0M;
            _month_allow = 0.0M;
            _quarter_allow = 0.0M;
            _syear_allow = 0.0M;
            _year_allow = 0.0M;
            _misc_allow = 0.0M;
            _pay_period = string.Empty;
            _marital_stat = string.Empty;
            _over_amt = 0.0M;
            _base_amt = 0.0M;
            _tax_rate = 0.0M;
            _order_no = 0;
           
        }

        #endregion Constructor

        #region public properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string tax_year
        {
            get { return _tax_year; }
            set { _tax_year = value; }
        }
        public string ded_code
        {
            get { return _ded_code; }
            set { _ded_code = value; }
        }
        public decimal week_allow
        {
            get { return _week_allow; }
            set { _week_allow = value; }
        }
        public decimal biweek_allow
        {
            get { return _biweek_allow; }
            set { _biweek_allow = value; }
        }
        public decimal smonth_allow
        {
            get { return _smonth_allow; }
            set { _smonth_allow = value; }
        }
        public decimal month_allow
        {
            get { return _month_allow; }
            set { _month_allow = value; }
        }
        public decimal quarter_allow
        {
            get { return _quarter_allow; }
            set { _quarter_allow = value; }
        }
        public decimal syear_allow
        {
            get { return _syear_allow; }
            set { _syear_allow = value; }
        }
        public decimal year_allow
        {
            get { return _year_allow; }
            set { _year_allow = value; }
        }
        public decimal misc_allow
        {
            get { return _misc_allow; }
            set { _misc_allow = value; }
        }
        public string pay_period
        {
            get { return _pay_period; }
            set { _pay_period = value; }
        }
        public string marital_stat
        {
            get { return _marital_stat; }
            set { _marital_stat = value; }
        }
        public decimal over_amt
        {
            get { return _over_amt; }
            set { _over_amt = value; }
        }
        public decimal base_amt
        {
            get { return _base_amt; }
            set { _base_amt = value; }
        }
        public decimal tax_rate
        {
            get { return _tax_rate; }
            set { _tax_rate = value; }
        }
        public int order_no
        {
            get { return _order_no; }
            set { _order_no = value;}
        }
       

        #endregion public properties

        #region Stored-Procedures   
       
       
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
            get { return "uspTaxTabGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspTaxTabGetAll"; }
        }

        public override string TABLE_NAME
        {
                get { return "Deductions_Tax_Table_Header"; }
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
            StringBuilder sql = new StringBuilder();
            sql.Append("select MasterDedcodes.description,Deductions_Tax_Table_Details.base_amt,Deductions_Tax_Table_Details.ded_code,Deductions_Tax_Table_Details.marital_stat,");
            sql.Append("Deductions_Tax_Table_Details.order_no,Deductions_Tax_Table_Details.over_amt,Deductions_Tax_Table_Details.pay_period,Deductions_Tax_Table_Details.tax_rate,");
            sql.Append("Deductions_Tax_Table_Details.tax_year,Deductions_Tax_Table_Header.biweek_allow,Deductions_Tax_Table_Header.misc_allow,Deductions_Tax_Table_Header.month_allow,Deductions_Tax_Table_Header.quarter_allow,");
            sql.Append("Deductions_Tax_Table_Header.smonth_allow,Deductions_Tax_Table_Header.syear_allow,Deductions_Tax_Table_Header.week_allow,Deductions_Tax_Table_Header.year_allow ");
            sql.Append("from Deductions_Tax_Table_Details,Deductions_Tax_Table_Header,outer(MasterDedcodes) where Deductions_Tax_Table_Header.ded_code = Deductions_Tax_Table_Details.ded_code  and MasterDedcodes.ded_code = Deductions_Tax_Table_Details.ded_code ");
            
            if (parameters[0] != null && parameters[0].ToString() != string.Empty)
                sql.Append(" AND Deductions_Tax_Table_Details.tax_year = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null && parameters[1].ToString() != string.Empty)
                sql.Append(" AND Deductions_Tax_Table_Details.ded_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
