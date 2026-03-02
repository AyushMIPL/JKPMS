using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSecAccountRanges: DVOBase
    {
        private int _rowid;
        private int _curr_asset;
        private string _d_curr_asset;
        private int _fixed_asset;
        private string _d_fixed_asset;
        private int _curr_liab;
        private string _d_curr_liab;
        private int _long_term_liab;
        private string _d_long_term_liab;
        private int _capital;
        private string _d_capital;
        private int _income;
        private string _d_income;
        private int _cost_goods;
        private string _d_cost_goods;
        private int _expense;
        private string _d_expense;
       
        #region Constructor

        public DVOSecAccountRanges()
        {
              _rowid = 0;  
              _curr_asset=0;
              _d_curr_asset=string.Empty;
              _fixed_asset=0;
              _d_fixed_asset=string.Empty;
              _curr_liab=0;
              _d_curr_liab=string.Empty;
              _long_term_liab=0;
              _d_long_term_liab=string.Empty;
              _capital=0;
              _d_capital=string.Empty;
              _income=0;
              _d_income=string.Empty;
              _cost_goods=0;
              _d_cost_goods=string.Empty;
              _expense=0;
              _d_expense=string.Empty;
        }

        #endregion Constructor

        #region Public Properties

        public int rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public int curr_asset
        {
            get { return _curr_asset; }
            set { _curr_asset = value; }
        }

        public string d_curr_asset
        {
            get { return _d_curr_asset; }
            set { _d_curr_asset = value; }
        }

        public int fixed_asset
        {
            get { return _fixed_asset; }
            set { _fixed_asset = value; }
        }

        public string d_fixed_asset
        {
            get { return _d_fixed_asset; }
            set { _d_fixed_asset = value; }
        }

        public int curr_liab
        {
            get { return _curr_liab; }
            set { _curr_liab = value; }
        }

        public string d_curr_liab
        {
            get { return _d_curr_liab; }
            set { _d_curr_liab = value; }
        }
        public int long_term_liab
        {
            get { return _long_term_liab; }
            set { _long_term_liab = value; }
        }

        public string d_long_term_liab
        {
            get { return _d_long_term_liab; }
            set {  _d_long_term_liab= value; }
        }
        public int capital
        {
            get { return _capital; }
            set { _capital = value; }
        }

        public string d_capital
        {
            get { return _d_capital; }
            set { _d_capital = value; }
        }
        public int income
        {
            get { return _income; }
            set { _income = value; }
        }

        public string d_income
        {
            get { return _d_income; }
            set { _d_income = value; }
        }
        public int cost_goods
        {
            get { return _cost_goods; }
            set {_cost_goods  = value; }
        }

        public string d_cost_goods
        {
            get { return _d_cost_goods; }
            set { _d_cost_goods = value; }
        }
        public int expense
        {
            get { return _expense; }
            set { _expense = value; }
        }

        public string d_expense
        {
            get { return _d_expense; }
            set { _d_expense = value; }
        }

        #endregion Public Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "uspglacctrngins"; }//uspglactrngins
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspglactrngupd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspglactrngdel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspglactrngget"; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspglactrngall"; }
        }
        public override string TABLE_NAME
        {
            get { return "MasterCompany"; }
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
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  rowid,* FROM MasterCompany");           

            return sql.ToString();
        }

        #endregion Stored-Procedures
    }
}
