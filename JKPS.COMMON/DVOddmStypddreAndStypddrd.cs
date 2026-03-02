using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOddmStypddreAndStypddrd:DVOBase
    {
        #region Private Variables
        private string _banck_code;
        private string _genck;
        private int _cash_acct_no;
        private decimal _amount;
        private string _empl_code;
        private string  _bank_code;
        private string _bank_acct_no;
        private string _bank_desc;
        private int _doc_no;
        private DateTime _date;
        private string _type_code;
        //Added By rahul jain on 05-02-2010
        private DateTime _dateTo;
        private DateTime _dateFrom;
        //Added By rohit on 02-11-2023
        private string _District;


        #endregion

        #region Constructor
        public DVOddmStypddreAndStypddrd()
        {
            _banck_code = string.Empty;
            _cash_acct_no = 0;
            _genck = string.Empty;
            _amount = 0;
            _empl_code = string.Empty;
            _doc_no = 0;
            _date = Convert.ToDateTime(null);
            _type_code=string.Empty;

            _dateFrom = Convert.ToDateTime(null);
            _dateTo = Convert.ToDateTime(null);
        }
        #endregion 

        #region Properties
        public string BanckCode
        {
            get { return _banck_code; }
            set { _banck_code = value; }
        }
        public int Cash_acct_no
        {
            get { return _cash_acct_no; }
            set { _cash_acct_no = value; }
        }
        public string GenCheck
        {
            get { return _genck; }
            set { _genck = value; }
        }
        public string empl_code 
        {
            get { return _empl_code;}
            set { _empl_code = value; }
        }
        public string  bank_code
        {
            get { return _bank_code; }
            set { _bank_code = value; }
        }
        public string bank_acct_no
        {
            get { return _bank_acct_no; }
            set { _bank_acct_no = value; }
        }
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string bank_desc
        {
            get { return _bank_desc; }
            set { _bank_desc = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string type_code
        {
            get { return _type_code; }
            set { _type_code = value; }
        }

        public DateTime dateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; }
        }
        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }
        public string District
        {
          get { return _District; }
          set { _District = value; }
        }
    #endregion

    #region Store Procedure
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
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "uspbanckcodeget"; }
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
        public string GET_DDMData
        {
            get { return "uapautmget"; }// uapautmget2
        }
        //Added By Rahul jain on 15-Jan-2010 
        public string GET_DUPLICATE_DDMData
        {
            get { return "uapdup_autmget"; }// uapdup_autmget
        }

        public string GET_FIELD_ID
        {
            get { return "USP_Field_IdGet"; }
        }
        public string UPD_YDDRE
        {
            get { return "USP_DDHeaderUpd"; }
        }
        public string GET_TYPEOFACCT
        {
            get { return "USP_TypeofAcctGet"; }
        }
        public string GET_DDL
        {
            get { return "USP_DDListGet"; }
        }
        public string GET_Test
        {
            get { return "usptestpammu"; }
        }

        public override string FIND_QUERY(ref object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("   select DISTINCT MasterBanks.bank_desc, Process_DirectDeposit_Details.amount amount,Process_DirectDeposit_Details.bank_acct_no,Process_DirectDeposit_Details.chk_digit,Process_DirectDeposit_Details.dfi_dest,Process_DirectDeposit_Details.empl_code,");
            sql.Append(" Process_DirectDeposit_Details.empl_name,Process_DirectDeposit_Details.pay_date,Process_DirectDeposit_Details.trace_number,Process_DirectDeposit_Details.trans_code,Process_DirectDeposit_Header.bank_code,Process_DirectDeposit_Header.batch_date,");
            sql.Append(" Process_DirectDeposit_Header.batch_no,Process_DirectDeposit_Header.dfi_immed,Process_DirectDeposit_Header.doc_no,Process_DirectDeposit_Header.entry_desc,Process_DirectDeposit_Header.file_id,");
            sql.Append(" Process_DirectDeposit_Header.svc_class,Process_DirectDeposit_Header.used,MasterBanks.suppliercode,Process_DirectDeposit_Header.company_name,MasterEmpBankDetails.typeofacct");
            sql.Append(" from Process_DirectDeposit_Header, Process_DirectDeposit_Details, MasterBanks,MasterEmpBankDetails, MasterEmployee");
            sql.Append(" where Process_DirectDeposit_Header.doc_no = Process_DirectDeposit_Details.doc_no and Process_DirectDeposit_Header.bank_code = MasterBanks.bank_code");
            sql.Append(" and Process_DirectDeposit_Details.empl_code =MasterEmpBankDetails.empl_code and Process_DirectDeposit_Header.bank_code=MasterEmpBankDetails.bank_code AND Rtrim(Process_DirectDeposit_Details.bank_acct_no)=Rtrim(MasterEmpBankDetails.bank_acct_no)");
            
            //if (parameters[0] != null)
            //{
            //    if (parameters[0].ToString().Trim().Length > 0)
            //    {
            //        // char[] test="^";
            //        string[] Bankcodes = parameters[0].ToString().Split('^');
            //        if (Bankcodes.Length >= 1)
            //            sql.Append(" and Process_DirectDeposit_Header.bank_code IN ('" + Bankcodes[0].ToString() + "'");

            //        for (int i = 1; i <= Bankcodes.Length - 1; i++)
            //            if (Bankcodes[i] != null)
            //                if (Bankcodes[i].ToString().Trim().Length > 0)
            //                    sql.Append(" ,'" + Bankcodes[i].ToString() + "'");

            //        if (Bankcodes.Length >= 1)
            //            sql.Append(" )");
            //    }
            //}
            sql.Append(" and Process_DirectDeposit_Header.used = 'N'");
            if(parameters[5] != null) { 
              sql.Append(" and MasterEmployee.SelectDistrict IN (SELECT Item FROM dbo.SplitString('"+ parameters[5] + "'))");
            }
            //if (parameters[3] != null)
            //{
            //    sql.Append(" and Process_DirectDeposit_Details.pay_date ='" + Convert.ToDateTime(parameters[3])+"'");
            //}
            //if (parameters[4] != null)
            //{
            //    if (parameters[4] != string.Empty)
            //    {

            //        sql.Append(" and Process_DirectDeposit_Details.empl_code  IN(select empl_code from Masteremployee where Type_Code IN ('" + parameters[4].ToString() + "'))");
            //    }
            //}

            sql.Append(" order by Process_DirectDeposit_Header.bank_code,Process_DirectDeposit_Header.doc_no");
           
            return sql.ToString();
        }

        public string GET_DUPLICATE_DIRECT_DEPOSIT_MEDIA_ITEM(ref object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("   select DISTINCT MasterBanks.bank_desc, Process_DirectDeposit_Details.amount amount,Process_DirectDeposit_Details.bank_acct_no,Process_DirectDeposit_Details.chk_digit,Process_DirectDeposit_Details.dfi_dest,Process_DirectDeposit_Details.empl_code,");
            sql.Append(" Process_DirectDeposit_Details.empl_name,Process_DirectDeposit_Details.pay_date,Process_DirectDeposit_Details.trace_number,Process_DirectDeposit_Details.trans_code,Process_DirectDeposit_Header.bank_code,Process_DirectDeposit_Header.batch_date,");
            sql.Append(" Process_DirectDeposit_Header.batch_no,Process_DirectDeposit_Header.dfi_immed,Process_DirectDeposit_Header.doc_no,Process_DirectDeposit_Header.entry_desc,Process_DirectDeposit_Header.file_id,");
            sql.Append(" Process_DirectDeposit_Header.svc_class,Process_DirectDeposit_Header.used,MasterBanks.suppliercode,Process_DirectDeposit_Header.company_name,MasterEmpBankDetails.typeofacct");
            sql.Append(" from Process_DirectDeposit_Header, Process_DirectDeposit_Details, MasterBanks,MasterEmpBankDetails");
            sql.Append(" where Process_DirectDeposit_Header.doc_no = Process_DirectDeposit_Details.doc_no and Process_DirectDeposit_Header.bank_code = MasterBanks.bank_code");
            sql.Append(" and Process_DirectDeposit_Details.empl_code =MasterEmpBankDetails.empl_code and Process_DirectDeposit_Header.bank_code=MasterEmpBankDetails.bank_code AND Rtrim(Process_DirectDeposit_Details.bank_acct_no)=Rtrim(MasterEmpBankDetails.bank_acct_no)");
            if (parameters[0] != null)
            {
                if (parameters[0].ToString().Trim().Length > 0)
                {
                    // char[] test="^";
                    string[] Bankcodes = parameters[0].ToString().Split('^');
                    if (Bankcodes.Length >= 1)
                        sql.Append(" and Process_DirectDeposit_Header.bank_code IN ('" + Bankcodes[0].ToString() + "'");

                    for (int i = 1; i <= Bankcodes.Length - 1; i++)
                        if (Bankcodes[i] != null)
                            if (Bankcodes[i].ToString().Trim().Length > 0)
                                sql.Append(" ,'" + Bankcodes[i].ToString() + "'");

                    if (Bankcodes.Length >= 1)
                        sql.Append(" )");
                }
            }
            if (parameters[3] != null && parameters[3].ToString().Trim().Length > 0 && !parameters[3].ToString().Trim().Contains("1900") && !parameters[3].ToString().Trim().Contains("0001"))
                sql.Append(" and Process_DirectDeposit_Details.pay_date = '" + parameters[3].ToString().Trim() + "'");

            //sql.Append(" and Process_DirectDeposit_Header.used = 'N'");
            sql.Append(" order by Process_DirectDeposit_Header.bank_code,Process_DirectDeposit_Header.doc_no");
            return sql.ToString();
        }
        //Added By rahul jain on 05-02-2010
        public string GET_DIRECT_DEPOSIT_BY_DEPOSIT_DATE(ref object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT MasterBanks.bank_desc,Process_DirectDeposit_Details.amount,");
            sql.Append(" Process_DirectDeposit_Details.bank_acct_no,Process_DirectDeposit_Details.empl_code,");
            sql.Append(" Process_DirectDeposit_Details.empl_name,Process_DirectDeposit_Header.bank_code,Process_DirectDeposit_Header.batch_date");
            sql.Append(" FROM Process_DirectDeposit_Header, Process_DirectDeposit_Details, MasterBanks");
            sql.Append(" WHERE Process_DirectDeposit_Header.doc_no = Process_DirectDeposit_Details.doc_no ");
            sql.Append(" AND Process_DirectDeposit_Header.bank_code = MasterBanks.bank_code");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty )// && !parameters[0].ToString().Contains("1900"))
                    sql.Append(" AND Process_DirectDeposit_Header.batch_date >= '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty )// && !parameters[3].ToString().Contains("1900"))
                    sql.Append(" AND Process_DirectDeposit_Header.batch_date <= '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

            sql.Append(" ORDER BY Process_DirectDeposit_Details.empl_name");
            return sql.ToString();
        }
        //Added By rahul jain on 05-02-2010
        public string GET_DIRECT_DEPOSIT_BY_DOCUMENT_DATE(ref object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT MasterBanks.bank_desc,Process_DirectDeposit_Details.amount,");
            sql.Append(" Process_DirectDeposit_Details.bank_acct_no,Process_DirectDeposit_Details.empl_code,");
            sql.Append(" Process_DirectDeposit_Details.empl_name,Process_DirectDeposit_Header.bank_code,Process_PayEmployee.doc_date ");//Process_DirectDeposit_Header.batch_date");
            sql.Append(" FROM Process_DirectDeposit_Header, Process_DirectDeposit_Details, MasterBanks ,Process_PayEmployee ");
            sql.Append(" WHERE Process_DirectDeposit_Header.doc_no = Process_DirectDeposit_Details.doc_no ");
            sql.Append(" AND Process_DirectDeposit_Header.bank_code = MasterBanks.bank_code");
            sql.Append(" AND Process_DirectDeposit_Details.pay_doc_no = Process_PayEmployee.doc_no ");
            sql.Append(" AND Process_PayEmployee.ok_to_post <> 'C' ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)// && !parameters[0].ToString().Contains("1900"))
                    sql.Append(" AND Process_PayEmployee.doc_date >= '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)// && !parameters[3].ToString().Contains("1900"))
                    sql.Append(" AND Process_PayEmployee.doc_date <= '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");

            sql.Append(" ORDER BY Process_DirectDeposit_Details.empl_name");
            return sql.ToString();
        }

        //Added By rahul jain on 05-02-2010
        public string GET_DIRECT_DEPOSIT_BY_RECONCILE_DATE(ref object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT MasterBanks.bank_desc,Process_DirectDeposit_Details.amount,");
            sql.Append(" Process_DirectDeposit_Details.bank_acct_no,Process_DirectDeposit_Details.empl_code,");
            sql.Append(" Process_DirectDeposit_Details.empl_name,Process_DirectDeposit_Header.bank_code,stpchkreconcile.date_reconcile ");
            sql.Append(" FROM Process_DirectDeposit_Header, Process_DirectDeposit_Details, MasterBanks, Process_PayEmployee ,stxckrgd, stpchkreconcile");
            sql.Append(" WHERE Process_DirectDeposit_Header.doc_no = Process_DirectDeposit_Details.doc_no ");
            sql.Append(" AND Process_DirectDeposit_Header.bank_code = MasterBanks.bank_code ");
            sql.Append(" AND Process_DirectDeposit_Details.pay_doc_no = Process_PayEmployee.doc_no ");
            sql.Append("AND Process_PayEmployee.doc_no= stxckrgd.doc_no ");
            sql.Append("AND stxckrgd.doc_no = stpchkreconcile.doc_no ");
            sql.Append("AND stxckrgd.reconciled='Y' ");
            sql.Append("AND stypyre.ok_to_post='P' ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)// && !parameters[0].ToString().Contains("1900"))
                    sql.Append(" AND stpchkreconcile.date_reconcile >= date('" + parameters[0].ToString().Trim().Replace("'", "''") + "')");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)// && !parameters[3].ToString().Contains("1900"))
                    sql.Append(" AND stpchkreconcile.date_reconcile <= date('" + parameters[1].ToString().Trim().Replace("'", "''") + "')");

            sql.Append(" ORDER BY Process_DirectDeposit_Details.empl_name");
            return sql.ToString();
        }
        #endregion
    }
}
