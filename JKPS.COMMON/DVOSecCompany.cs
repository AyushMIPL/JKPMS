using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;



namespace JKPS.COMMON
{
    //Created By Branda 
    public class DVOSecCompany : DVOBase 
    {
        private int _Company_id;
        private string _co_name;
        private string _addr1;
        private string _addr2;
        private string _city;
        private string _state;
        private string _zip;
        private string _county;
        private string _country;
        private string _mtax;
        private string _use_mtax_grps;


     
        private int _rowid;
        private string _src_type;
        private string _src_key ;
        private string _src_desc;



        #region Constructor

        public DVOSecCompany()
        {
            _Company_id = 0;
            _co_name = string.Empty;
            _addr1 = string.Empty;
            _addr2 = string.Empty;
            _city = string.Empty;
            _state = string.Empty;
            _zip = string.Empty;
            _county = string.Empty;
            _country = string.Empty;
            _mtax = string.Empty;
            _use_mtax_grps = string.Empty;

            _Company_id = 0;
            _src_key=string.Empty  ;
            _src_desc=string.Empty ;
            _src_type = string.Empty;
            



        }
        #endregion Constructor


        #region Public Properties
        public int Company_id
        {
            get { return _Company_id; }
            set { _Company_id = value; }
        }
        public  string co_name
        {
            get { return _co_name; }
            set { _co_name = value; }
        }
        public string addr1
        {
            get { return _addr1; }
            set { _addr1  = value; }
        }
        public string addr2
        {
            get { return _addr2 ; }
            set { _addr2  = value; }
        }
        public string city
        {
            get { return _city; }
            set { _city  = value; }
        }
        public string state
        {
            get { return _state ; }
            set { _state  = value; }
        }
        public string zip
        {
            get { return _zip ; }
            set { _zip  = value; }
        }
        public string county
        {
            get { return _county ; }
            set { _county  = value; }
        }
        public string country
        {
            get { return _country ; }
            set { _country  = value; }
        }
        public string mtax
        {
            get { return _mtax ; }
            set { _mtax  = value; }
        }
        public string  use_mtax_grps
        {
            get { return _use_mtax_grps ; }
            set { _use_mtax_grps  = value; }
        }

        //public int Company_id
        //{
        //    get { return _Company_id; }
        //    set { _Company_id = value; }
        //}
        public string  src_type
        {
            get { return _src_type ; }
            set { _src_type  = value; }
        }
        public string  src_key
        {
            get { return _src_key; }
            set { _src_key = value; }
        }

        public string  src_desc
        {
            get { return _src_desc; }
            set { _src_desc = value; }
        }

          public string uspCompDetail
       {
           get { return "uspCompDetail"; }
       }

        
        public string INSERT_COMPANY_DETAIL
        {
            get { return "uspcompdetailins"; }
        }
        public string UPDATE_COMPANY_DETAIL
        {
            get { return "uspCompDetailupd"; }
        }

        public string DELETE_COMPANY_DETAIL
        {
            get { return "uspcompdetaildel"; }
        }

        #endregion Public Properties

       

        public override string INSERT_SPNAME
        {
            get { return "USP_CompanyInsert"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_CompanyUpdate"; }
        }

        public override string DELETE_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_SPNAME
        {
            get { return "USP_CompanyGet"; }
        }

       
        public override string ALL_SPNAME
        {
            get { return "USP_CompanyGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "MasterCompany"; }
        }

        public override int UNIQUE_ID
        {
            get { return _Company_id; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }
        public string GetCompanyData(ref object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Company_ID,company_name,Address1,Address2,");
            sql.Append("City,State,Zip,County,Country,Curr_Asset,");
            sql.Append("d_curr_asset,fixed_asset,d_fixed_asset,");
            sql.Append("curr_liab,d_curr_liab,long_term_liab,");
            sql.Append("d_long_trm_liab,capital,d_capital,");
            sql.Append("income,d_income,cost_goods,d_cost_goods,");
            sql.Append("expense,d_expense,mtax,use_mtax_grps");
            sql.Append("FROM MasterCompany");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND rowid = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(company_name) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");//co_name

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Address1) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Address2) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(City) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");//

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(State) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Zip) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(County) LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Country) LIKE '" + parameters[8].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(mtax) LIKE '" + parameters[9].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(use_mtax_grps) LIKE '" + parameters[10].ToString().Trim().Replace("'", "''") + "%'");//

            return sql.ToString();
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Company_ID,company_name,Address1,Address2,");
            sql.Append("City,State,Zip,County,Country,Curr_Asset, ");
             sql.Append("d_curr_asset,fixed_asset,d_fixed_asset, ");
             sql.Append("curr_liab,d_curr_liab,long_term_liab, ");
             sql.Append("d_long_trm_liab,capital,d_capital, ");
             sql.Append("income,d_income,cost_goods,d_cost_goods, ");
             sql.Append("expense,d_expense,mtax,use_mtax_grps ");
             sql.Append("FROM MasterCompany Where 1=1");
            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND Company_ID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(company_name) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");//co_name

            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Address1) LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Address2) LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(City) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");//

            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(State) LIKE '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Zip) LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(County) LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");//
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(Country) LIKE '" + parameters[8].ToString().Trim().Replace("'", "''") + "%'");//
           
           
            return sql.ToString();
        }
       
    }
    
}
