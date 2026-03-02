using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOOrderCustomerInfostrcustr : DVOBase
    {
        private string _cust_code;
        private string _bridge_code;
        private string _bus_name;
        private string _taxable;
        private string _contact;
        private string _phone;
        private string _fax_phone;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _zip;
        private string _country;
        private string _ar_type;
        private string _preferred;
        private string _frequent;
        private Int32 _stmt_cycle;
        private string _fin_chg;
        private decimal _credit_limit;
        private decimal _order_limit;
        private string _terms_code;
        private string _act_grp;
        private Int32 _ar_acct_dflt;
        private string _ar_department_dflt;
        private DateTime _stmt_date;
        private decimal _stmt_amount;
        private decimal _acct_bal;
        private DateTime _obtained_date;
        private DateTime _last_order_date;
        private DateTime _last_pay_date;
        private DateTime _inactive_date;
        private decimal _on_acct_amt;
        private decimal _arch_bal;
        private string _sls_psn_code;
        private string _trd_ds_code;
        private string _st_tx_code;
        private string _co_tx_code;
        private string _ci_tx_code;
        private string _comm_code;
        private string _pay_method;
        private string _card_no;
        private string _exp_date;
        private string _card_holder;
        private string _cc_method;
        private string _mtax_fc;
        private string _currency_code;
        private string _mtax_freight;
        private string _mtax_misc;
        private string _ship_via_cd;

        private int _RowID;
        private string _InsertMachineInfo;
        private string _InsertDate;  //date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;   //date
        private int _UpdateBy;

        //Added By Rajeev
        //Aim: To use in reports
        private string _stDate;
        private string _endDate;

        public DVOOrderCustomerInfostrcustr()
       {
		  _cust_code=string.Empty;
	      _bridge_code=string.Empty ;
	      _bus_name=string.Empty;
	      _taxable=string.Empty;
	      _contact=string.Empty;
	      _phone=string.Empty;
          _fax_phone = string.Empty;
          _address1 = string.Empty;
          _address2 = string.Empty;
          _city = string.Empty;
          _state = string.Empty;
          _zip = string.Empty;
          _country = string.Empty;
          _ar_type = string.Empty;
          _preferred = string.Empty;
          _frequent = string.Empty;
          _stmt_cycle = 0;
          _fin_chg = string.Empty;
          _credit_limit = 0.0M;
          _order_limit = 0.0M;
          _terms_code = string.Empty;
          _act_grp = string.Empty;
          _ar_acct_dflt = 0;
          _ar_department_dflt = string.Empty;
          _stmt_date = Convert.ToDateTime(null);
          _stmt_amount = 0.0M;
          _acct_bal = 0.0M;
          _obtained_date = Convert.ToDateTime(null);
          _last_order_date = Convert.ToDateTime(null);
          _last_pay_date = Convert.ToDateTime(null);
          _inactive_date = Convert.ToDateTime(null);
          _on_acct_amt = 0.0M;
          _arch_bal = 0.0M;
          _sls_psn_code = string.Empty;
          _trd_ds_code = string.Empty;
          _st_tx_code = string.Empty;
          _co_tx_code = string.Empty;
          _ci_tx_code = string.Empty;
          _comm_code = string.Empty;
          _pay_method = string.Empty;
          _card_no = string.Empty;
          _exp_date = string.Empty;
          _card_holder = string.Empty;
          _cc_method = string.Empty;
          _mtax_fc = string.Empty;
          _currency_code = string.Empty;
          _mtax_freight = string.Empty;
          _mtax_misc = string.Empty;
          _ship_via_cd = string.Empty;


          _stDate = string.Empty;
          _endDate = string.Empty;

          _RowID = 0;
          _InsertMachineInfo = string.Empty;
          _InsertDate = "01/01/1900";
          _InsertBy = 0;

          _UpdateMachineInfo = string.Empty;
          _UpdateDate = "01/01/1900";
          _UpdateBy = 0;

          
       }

        #region public properties
       public string cust_code
        {
            get { return _cust_code; }
            set { _cust_code = value; }
        }
        public string bridge_code 
       {
           get { return _bridge_code; }
           set { _bridge_code = value; }
        }

        public string bus_name 
       {
           get { return _bus_name; }
           set { _bus_name = value; }
        }
        public string taxable 
       {
           get { return _taxable; }
           set { _taxable = value; }
        }
        public string contact 
       {
           get { return _contact; }
           set { _contact = value; }
        }
        public string phone 
       {
           get { return _phone; }
           set { _phone = value; }
        }
        public string fax_phone
        {
            get { return _fax_phone; }
            set { _fax_phone = value; }
        }
        public string address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        public string zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string ar_type
        {
            get { return _ar_type; }
            set { _ar_type = value; }
        }
        public string preferred
        {
            get { return _preferred; }
            set { _preferred = value; }
        }
        public string frequent
        {
            get { return _frequent; }
            set { _frequent = value; }
        }
        public Int32 stmt_cycle
        {
            get { return _stmt_cycle; }
            set { _stmt_cycle = value; }
        }
        public string fin_chg
        {
            get { return _fin_chg; }
            set { _fin_chg = value; }
        }
        public decimal credit_limit
        {
            get { return _credit_limit; }
            set { _credit_limit = value; }
        }
        public decimal order_limit
        {
            get { return _order_limit; }
            set { _order_limit = value; }
        }
        public string terms_code
        {
            get { return _terms_code; }
            set { _terms_code = value; }
        }
        public string act_grp
        {
            get { return _act_grp; }
            set { _act_grp = value; }
        }
        public Int32 ar_acct_dflt
        {
            get { return _ar_acct_dflt; }
            set { _ar_acct_dflt = value; }
        }
        public string ar_department_dflt
        {
            get { return _ar_department_dflt; }
            set { _ar_department_dflt = value; }
        }
        public DateTime stmt_date
        {
            get { return _stmt_date; }
            set { _stmt_date = value; }
        }
        public decimal stmt_amount
        {
            get { return _stmt_amount; }
            set { _stmt_amount = value; }
        }
        public decimal acct_bal
        {
            get { return _acct_bal; }
            set { _acct_bal = value; }
        }
        public DateTime obtained_date
        {
            get { return _obtained_date; }
            set { _obtained_date = value; }
        }
        public DateTime last_order_date
        {
            get { return _last_order_date; }
            set { _last_order_date = value; }
        }
        public DateTime last_pay_date
        {
            get { return _last_pay_date; }
            set { _last_pay_date = value; }
        }
        public DateTime inactive_date
        {
            get { return _inactive_date; }
            set { _inactive_date = value; }
        }
        public decimal on_acct_amt
        {
            get { return _on_acct_amt; }
            set { _on_acct_amt = value; }
        }
        public decimal arch_bal
        {
            get { return _arch_bal; }
            set { _arch_bal = value; }
        }
        public string sls_psn_code
        {
            get { return _sls_psn_code; }
            set { _sls_psn_code = value; }
        }
        public string trd_ds_code
        {
            get { return _trd_ds_code; }
            set { _trd_ds_code = value; }
        }
        public string st_tx_code
        {
            get { return _st_tx_code; }
            set { _st_tx_code = value; }
        }
        public string co_tx_code
        {
            get { return _co_tx_code; }
            set { _co_tx_code = value; }
        }
        public string ci_tx_code
        {
            get { return _ci_tx_code; }
            set { _ci_tx_code = value; }
        }
        public string comm_code
        {
            get { return _comm_code; }
            set { _comm_code = value; }
        }
        public string pay_method
        {
            get { return _pay_method; }
            set { _pay_method = value; }
        }
        public string card_no
        {
            get { return _card_no; }
            set { _card_no = value; }
        }

        public string exp_date
        {
            get { return _exp_date; }
            set { _exp_date = value; }
        }
        public string card_holder
        {
            get { return _card_holder; }
            set { _card_holder = value; }
        }
        public string cc_method
        {
            get { return _cc_method; }
            set { _cc_method = value; }
        }
        public string mtax_fc
        {
            get { return _mtax_fc; }
            set { _mtax_fc = value; }
        }
        public string currency_code
        {
            get { return _currency_code; }
            set { _currency_code = value; }
        }
        public string mtax_freight
        {
            get { return _mtax_freight; }
            set { _mtax_freight = value; }
        }
        public string mtax_misc
        {
            get { return _mtax_misc; }
            set { _mtax_misc = value; }
        }

        public string ship_via_cd
        {
            get { return _ship_via_cd; }
            set { _ship_via_cd = value; }
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

        public string stDate
        {
            get { return _stDate; }
            set { _stDate = value; }
        }
        public string endDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
   


        #endregion public properties

        #region Stored-Procedures


        public override string INSERT_SPNAME
        {
            get { return "uspcustinfoins"; }
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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string TABLE_NAME
        {
            get { return "strcustr"; }
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



        public override string FIND_QUERY(ref Object[] parameters)
        {
            //System.Text.StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT grp_key p_grp_key ,grp_desc p_grp_desc,rowid p_rowid ");
            //sql.Append(" from stxactgr");
            //sql.Append(" where 1=1");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(grp_key) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != string.Empty)
            //        sql.Append(" AND Rtrim(grp_desc) LIKE '" + parameters[1].ToString().Trim().Replace("'", "''") + "%'");
            //if (Convert.ToInt32(parameters[2]) > 0)
            //    sql.Append(" AND rowid = " + parameters[2].ToString());

            //return sql.ToString();
            return "";
        }

        #region Report CustomerDetails
        /// <summary>
        /// Added By Rajeev 
        /// Aim : To Print Customer Details
        /// Date : 25/07/2009
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string FINDQUERY_CUSTOMER_DETAILS(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stoinvce.currency_code,stoinvce.currency_rate,stoshipd.doc_no,");
            sql.Append(" stoshipd.gross_margin,stoshipd.inv_date,stoshipd.inv_doc_no,");
            sql.Append(" stoshipd.item_code,stoshipd.net_amount,stoshipd.sell_to_code,strcustr.bus_name");
            sql.Append(" from stoshipd, strcustr, stoinvce ");
            sql.Append(" Where stoshipd.sell_to_code = strcustr.cust_code and stoinvce.inv_doc_no = stoshipd.inv_doc_no and");
            sql.Append(" stoshipd.stage = 'PST'");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND strcustr.cust_code = " + parameters[0].ToString());
            if (parameters[1] != null && parameters[2] != null)
                if (parameters[1].ToString() != string.Empty && parameters[2].ToString() != string.Empty)
                    sql.Append(" AND stoinvce.inv_date between '" + parameters[1].ToString() + "' and '" + parameters[2].ToString() + "'");

            sql.Append(" ORDER BY stoshipd.sell_to_code, stoshipd.inv_date, stoshipd.inv_doc_no");
            return sql.ToString();
        }

        public string  GET_LIKE_TYPE
        {
            get { return "uspliktypget"; }
        }
        /// <summary>
        /// Added By Sanjay Chawla 
        /// Aim : To Print Customer Summary
        /// Date : 26/07/2009
        /// </summary>
        public string FINDQUERY_CUSTOMER_SUMMARY(ref object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select stoinvce.currency_code,stoinvce.currency_rate,stoshipd.doc_no,");
            sql.Append("stoshipd.gross_margin,stoshipd.inv_date,stoshipd.net_amount,stoshipd.sell_to_code,strcustr.bus_name,'" + parameters[0] + "' as start_date,'" + parameters[1] + "' as end_date ");
            sql.Append("from stoshipd, strcustr, stoinvce where 1=1 and stoshipd.sell_to_code = strcustr.cust_code");
            sql.Append(" and stoinvce.inv_doc_no = stoshipd.inv_doc_no");
            sql.Append(" and stoshipd.stage = 'PST'");
            if (parameters[0] != null && parameters[1] != null)
                if (parameters[0].ToString() != string.Empty && parameters[1].ToString() != string.Empty)
                    sql.Append(" AND stoinvce.inv_date between '" + parameters[0].ToString() + "' and '" + parameters[1].ToString() + "'");
            if (parameters[2].ToString() != string.Empty && parameters[2].ToString() != null)//sell_to_code
                sql.Append(" and stoshipd.sell_to_code ='" + parameters[2].ToString() + "'");
            sql.Append(" order by stoshipd.sell_to_code");

            return sql.ToString();
        }
        #endregion Report CustomerDetails




        #endregion store-procedures
    }
}
