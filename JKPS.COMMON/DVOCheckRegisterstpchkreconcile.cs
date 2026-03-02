using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOCheckRegisterstpchkreconcile : DVOBase
    {
        private int _RowId;
        private string _orig_journal;
        private int _doc_no;
        private string _check_no;
        private decimal _amount;
        private int _acct_no;
        private string _department;
        private string _debit_credit;
        private string _reconciled;
        private string _chk_voided;
        private string _date_reconcile;
        private string _date_clr_frm_bank;
        private string _date_deposit;
        private string _rec_month;
        private string _rec_year;
        private decimal _activity;
        private decimal _balance;
        private string _incr_with_cred;
        private int _insertby;
        private string _insertdate;
        private string _insertmachineinfo;
        private int _updateby;
        private string _updatedate;
        private string _updatemachineinfo;

        private string _FromDate;
        private string _ToDate;

        #region Constructor
        public DVOCheckRegisterstpchkreconcile()
        {
            _RowId = 0;
            _orig_journal = string.Empty;
            _doc_no = 0;
            _check_no = string.Empty;
            _amount = 0.0M;
            _acct_no = 0;
            _department = string.Empty;
            _debit_credit = string.Empty;
            _reconciled = string.Empty;
            _chk_voided = string.Empty;
            _date_reconcile = "01/01/1900";//date
            _date_clr_frm_bank = "01/01/1900";//date
            _date_deposit = "01/01/1900";
            _rec_month = string.Empty;
            _rec_year = string.Empty;
            _activity = 0;
            _balance = 0;
            _incr_with_cred = string.Empty;

            _insertby = 0;
            _insertdate = "01/01/1900";//datetime
            _insertmachineinfo = string.Empty;
            _updateby = 0;
            _updatedate = "01/01/1900";//datetime
            _updatemachineinfo = string.Empty;

            _FromDate = "01/01/1900";//datetime
            _ToDate = "01/01/1900";//datetime
        }
        #endregion Constructor

        #region  Public Properties
        public int RowId
        {
            get { return _RowId; }
            set { _RowId = value; }
        }
        public string orig_journal
        {
            get { return _orig_journal; }
            set { _orig_journal = value; }
        }
        public int doc_no
        {
            get { return _doc_no; }
            set { _doc_no = value; }
        }
        public string check_no
        {
            get { return _check_no; }
            set { _check_no = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public int acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        public string debit_credit
        {
            get { return _debit_credit; }
            set { _debit_credit = value; }
        }
        public string incr_with_cred
        {
            get { return _incr_with_cred; }
            set { _incr_with_cred = value; }
        }
        public string reconciled
        {
            get { return _reconciled; }
            set { _reconciled = value; }
        }
        public string chk_voided
        {
            get { return _chk_voided; }
            set { _chk_voided = value; }
        }
        public string date_clr_frm_bank
        {
            get { return _date_clr_frm_bank; }
            set { _date_clr_frm_bank = value; }
        } 
        public string date_deposit
        {
            get { return _date_deposit; }
            set { _date_deposit = value; }
        } 
        public string date_reconcile
        {
            get { return _date_reconcile; }
            set { _date_reconcile = value; }
        }
        public string rec_month
        {
            get { return _rec_month; }
            set { _rec_month = value; }
        }
        public string rec_year
        {
            get { return _rec_year; }
            set { _rec_year = value; }
        }
        public decimal activity
        {
            get { return _activity; }
            set { _activity = value; }
        }
        public decimal balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public int insertby
        {
            get { return _insertby; }
            set { _insertby = value; }
        }
        public string insertdate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
        }
        public string insertmachineinfo
        {
            get { return _insertmachineinfo; }
            set { _insertmachineinfo = value; }
        }
        public int updateby
        {
            get { return _updateby; }
            set { _updateby = value; }
        }
        public string updatedate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }
        public string updatemachineinfo
        {
            get { return _updatemachineinfo; }
            set { _updatemachineinfo = value; }
        }

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        #endregion  Public Properties

        #region Stored-Procedures        
        
        public override string INSERT_SPNAME
        {
            get { return "uspinsstxckrgd"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspupdstxckrgd"; }
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
            get { return "stpchkreconcile"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowId; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }



        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT distinct s1.orig_journal,s1.doc_no,s1.acct_no ,");
            sql.Append(" s1.department, s1.inv_chk_no,s1.amount,s1.reconciled,s1.chk_voided");
            sql.Append(" from stxckrgd s1 ,stpcashe");//s1.rowid,,s1.debit_credit
            sql.Append(" where s1.doc_no=stpcashe.doc_no");
            sql.Append(" ANd s1.orig_journal IN ('CD')  ");
            sql.Append(" AND (s1.reconciled <>'Y' ");
            sql.Append(" or s1.reconciled is null)");
            //sql.Append(" AND stpcashe.ok_to_post ='P'");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(s1.inv_chk_no) = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND s1.amount =" + parameters[1].ToString());

            sql.Append(" UNION");

            sql.Append(" SELECT distinct s1.orig_journal,s1.doc_no,s1.acct_no ,");
            sql.Append(" s1.department, s1.inv_chk_no,Process_PayEmployee.cash_amount,s1.reconciled,s1.chk_voided");
            sql.Append(" from stxckrgd s1, Process_PayEmployee");//s1.rowid,,s1.debit_credit
            sql.Append(" where s1.doc_no=Process_PayEmployee.doc_no");
            sql.Append(" AND s1.orig_journal IN ('PY')  ");
            sql.Append(" AND (s1.reconciled <>'Y' ");
            sql.Append(" or s1.reconciled is null) ");
            //sql.Append(" AND Process_PayEmployee.ok_to_post ='P'");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND  trim(s1.inv_chk_no) = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND s1.amount =" + parameters[1].ToString());
            sql.Append(" order by 3");



            //sql.Append("SELECT distinct s1.rowid,s1.orig_journal,s1.doc_no,s1.acct_no ,s1.department, s1.inv_chk_no,s1.amount, ");
            //sql.Append(" s1.reconciled,s1.chk_voided,s1.debit_credit ");//

            //sql.Append(" AND stpcashe.ok_to_post ='P'");
            //sql.Append(" where s1.orig_journal IN ('CD','PY') ");
            //sql.Append(" AND (s1.reconciled <>'Y' or s1.reconciled is null) ");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString() != string.Empty)
            //        sql.Append(" AND  s1.inv_chk_no = '" + parameters[0].ToString().Replace("'", "''") + "'");
            //if (parameters[1] != null)
            //    if (Convert.ToInt32(parameters[1]) > 0)
            //        sql.Append(" AND s1.amount =" + parameters[1].ToString());

            //sql.Append(" order by s1.doc_no ");
            return sql.ToString();
        }
        
        public string FIND_QUERY2(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT distinct s1.rowid,s1.orig_journal,s1.doc_no,s1.acct_no ,s1.department, s1.inv_chk_no,s1.amount, ");
            sql.Append(" s1.reconciled,s1.chk_voided");//s1.debit_credit,

            sql.Append(" from stxckrgd s1");
            sql.Append(" where s1.orig_journal IN ('CD','PY') ");
            sql.Append(" AND s1.reconciled ='Y' ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND  s1.inv_chk_no = '" + parameters[0].ToString().Replace("'", "''") + "'");
           

            sql.Append(" order by s1.doc_no ");
            return sql.ToString();
        }
        public string FIND_QUERY_3(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT distinct s1.orig_journal,s1.doc_no,s1.acct_no,");
            sql.Append(" s1.department, s1.inv_chk_no,s1.amount,s1.reconciled,s1.chk_voided");
            sql.Append(" from stxckrgd s1 ,strcashe");//s1.rowid,,s1.debit_credit
            sql.Append(" where s1.doc_no=strcashe.doc_no");
            sql.Append(" ANd s1.orig_journal IN ('CR')  ");
            sql.Append(" AND (s1.reconciled <>'Y' ");
            sql.Append(" or s1.reconciled is null)");
            sql.Append(" AND strcashe.ok_to_post ='P' ");
            if (parameters[0] != null)
                if (Convert.ToInt32(parameters[0]) > 0)
                    sql.Append(" AND  s1.doc_no = '" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND s1.amount =" + parameters[1].ToString());
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim() != string.Empty && parameters[2].ToString().Trim() !="01/01/1900")
                    sql.Append(" AND  strcashe.rcpt_date=" + "'" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (Convert.ToInt32(parameters[3]) > 0)
                    sql.Append(" AND  s1.acct_no = " + parameters[3].ToString().Replace("'", "''"));
            //Added by Sarvjeet on 20/01/2010
            if(parameters.Length>4)
                if(parameters[4]!=null)
                    if(parameters[4].ToString().Trim().Length>0)
                        sql.Append(" AND  s1.acct_no in( "+ parameters[4].ToString()+")");

            return sql.ToString();
        }
        public string FIND_QUERY_4(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT distinct s1.rowid,s1.orig_journal,s1.doc_no,s1.acct_no ,s1.department, s1.inv_chk_no,s1.amount, ");
            sql.Append(" s1.reconciled,s1.chk_voided");//s1.debit_credit,

            sql.Append(" from stxckrgd s1");
            sql.Append(" where s1.orig_journal IN ('CR') ");
            sql.Append(" AND s1.reconciled ='Y' ");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND  s1.doc_no = '" + parameters[0].ToString().Replace("'", "''") + "'");


            sql.Append(" order by s1.doc_no ");
            return sql.ToString();
        }


        public string FIND_CLEARED_CHECKS(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append(" select distinct cile.orig_journal ,cile.doc_no ,cile.check_no,cile.amount,");
            sql.Append(" cile.date_reconcile,cile.date_clr_frm_bank,0,'','' from ");//,gd.acct_no,tr.keyvalue
            sql.Append(" stpchkreconcile cile,stxckrgd gd where  ");//,outer(PayrollGLAccounts tr)
            sql.Append(" cile.doc_no=gd.doc_no and reconciled = 'Y' ");// and gd.acct_no=tr.acct_no
        
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty && !parameters[0].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(cile.date_clr_frm_bank) >= date('" + parameters[0].ToString().Replace("'", "''") + "')");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty && !parameters[1].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(cile.date_clr_frm_bank) <=  date('" + parameters[1].ToString().Trim().Replace("'", "''") + "')");
   

            return sql.ToString();
        }

        public string FIND_DEPOSIT_AMT(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append(" select distinct cile.doc_no ,cile.check_no,cile.amount,");
            sql.Append(" cile.date_reconcile,cile.date_clr_frm_bank,0,'',cashe.doc_desc from ");//,gd.acct_no,tr.keyvalue
            sql.Append(" stpchkreconcile cile,strcashe cashe, stxckrgd gd where cile.doc_no=gd.doc_no ");//,outer(PayrollGLAccounts tr)
            sql.Append(" and cile.doc_no=cashe.doc_no and cile.orig_journal='CR' and gd.reconciled = 'Y' ");// and gd.acct_no=tr.acct_no

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty && !parameters[0].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(cile.date_clr_frm_bank) >= date('" + parameters[0].ToString().Replace("'", "''") + "')");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty && !parameters[1].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(cile.date_clr_frm_bank) <=  date('" + parameters[1].ToString().Trim().Replace("'", "''") + "')");


            return sql.ToString();
        }

        public string FIND_ACCT_STATEMENT(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append(" select distinct cile.orig_journal,cile.doc_no ,cile.check_no,cile.amount,");
            sql.Append(" cile.date_reconcile,cile.date_clr_frm_bank,gd.acct_no,'',cashe.doc_desc from ");//,gd.acct_no,tr.keyvalue
            sql.Append(" stpchkreconcile cile,strcashe cashe, stxckrgd gd where cile.doc_no=gd.doc_no ");//,outer(PayrollGLAccounts tr)
            sql.Append(" and cile.doc_no=cashe.doc_no and gd.reconciled = 'Y' ");// and gd.acct_no=tr.acct_no

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND gd.acct_no =" + parameters[0].ToString());
                if (parameters[1].ToString() != string.Empty && !parameters[1].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(cile.date_clr_frm_bank) >= date('" + parameters[1].ToString().Replace("'", "''") + "')");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty && !parameters[2].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(cile.date_clr_frm_bank) <=  date('" + parameters[2].ToString().Trim().Replace("'", "''") + "')");


            return sql.ToString();
        }



        public string FIND_RECONCILED_CHECKS(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            sql.Append("SELECT s1.orig_journal,s1.doc_no,s1.check_no,s1.date_reconcile,");
            sql.Append(" s1.date_clr_frm_bank,  s2.acct_no,s2.department,s1.insertby,");
            sql.Append(" s2.chk_voided, s2.amount,'',s2.reconciled,s2.chk_voided,");//,s2.debit_credit
            sql.Append(" s2.reconciled,s3.LoginId UserName,s4.keyvalue keyvalue,");
            sql.Append(" stpcashe.ap_type ap_type,stpcashe.chk_date chk_date ,");
            sql.Append(" stppytor.pay_to_name pay_to_name, stpcashe.bus_name bus_name ,");
            sql.Append(" stpvendr.bus_name vend_name  from stpchkreconcile s1,stxckrgd s2,");
            sql.Append(" secusers s3,PayrollGLAccounts s4 ,stpcashe,outer (stppytor) ,outer stpvendr");
            sql.Append(" where  s1.orig_journal=s2.orig_journal  AND s1.doc_no=s2.doc_no");
            sql.Append(" AND s2.doc_no=stpcashe.doc_no  AND stpcashe.cash_acct= s2.acct_no ");
            sql.Append(" AND stpcashe.vend_code= stpvendr.vend_code");
            sql.Append(" AND stpcashe.pay_to_code = stppytor.pay_to_code  AND s1.insertby=s3.UserId");
            sql.Append(" AND s2.acct_no=s4.acct_no AND s2.orig_journal IN ('CD')");
            sql.Append(" AND s2.reconciled='Y'  ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty && !parameters[0].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(s1.date_reconcile) >= date('" + parameters[0].ToString().Replace("'", "''") + "')");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty && !parameters[1].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(s1.date_reconcile) <=  date('" + parameters[1].ToString().Trim().Replace("'", "''") + "')");
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND s1.insertby =" + parameters[2].ToString());

            sql.Append(" UNION");

            sql.Append(" SELECT s1.orig_journal,s1.doc_no,s1.check_no,s1.date_reconcile,");
            sql.Append(" s1.date_clr_frm_bank,  s2.acct_no,s2.department,s1.insertby,");
            sql.Append(" s2.chk_voided,Process_PayEmployee.cash_amount,'',s2.reconciled,s2.chk_voided,");//, s2.amount,s2.debit_credit
            sql.Append(" s2.reconciled,s3.LoginId UserName,s4.keyvalue keyvalue,");
            sql.Append(" '' ap_type, Process_PayEmployee.pay_date chk_date,");
            sql.Append(" MasterEmployee.last_name pay_to_name, MasterEmployee.first_name bus_name,");
            sql.Append(" MasterEmployee.middle_name vend_name");
            sql.Append(" from stpchkreconcile s1,stxckrgd s2,");
            sql.Append(" secusers s3,PayrollGLAccounts s4 ,");
            sql.Append(" Process_PayEmployee LEFT outer JOIN MasterEmployee ON Process_PayEmployee.empl_code=MasterEmployee.empl_code");
            sql.Append(" where  s1.orig_journal=s2.orig_journal  AND s1.doc_no=s2.doc_no");
            sql.Append(" AND s2.doc_no=Process_PayEmployee.doc_no AND Process_PayEmployee.cash_acct_no=s2.acct_no ");
           // sql.Append(" AND ");
            sql.Append(" AND s1.insertby=s3.UserId");
            sql.Append(" AND s2.acct_no=s4.acct_no AND s2.orig_journal IN ('PY')");
            sql.Append(" AND s2.reconciled='Y'  ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty && !parameters[0].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(s1.date_reconcile) >= date('" + parameters[0].ToString().Replace("'", "''") + "')");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty && !parameters[1].ToString().Trim().Contains("1900"))
                    sql.Append(" AND  date(s1.date_reconcile) <=  date('" + parameters[1].ToString().Trim().Replace("'", "''") + "')");
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND s1.insertby =" + parameters[2].ToString());
            sql.Append(" order by 2");



            //sql.Append("SELECT s1.orig_journal,s1.doc_no,s1.check_no,s1.date_reconcile,s1.date_clr_frm_bank, ");
            //sql.Append(" s2.acct_no,s2.department,s1.insertby, s2.chk_voided,");
            //sql.Append(" s2.amount,s2.debit_credit,s2.reconciled,s2.chk_voided,s2.reconciled,s3.LoginId UserName,s4.keyvalue keyvalue,stpcashe.ap_type ap_type,stpcashe.chk_date chk_date , ");
            //sql.Append(" stppytor.pay_to_name pay_to_name, stpcashe.bus_name bus_name , stpvendr.bus_name vend_name ");
            //sql.Append(" from stpchkreconcile s1,stxckrgd s2,secusers s3,PayrollGLAccounts s4 ,stpcashe,outer (stppytor) ,outer stpvendr ");
            //sql.Append(" where  s1.orig_journal=s2.orig_journal ");
            //sql.Append(" AND s1.doc_no=s2.doc_no");
            //sql.Append(" AND s2.doc_no=stpcashe.doc_no ");
            //sql.Append(" AND stpcashe.cash_acct= s2.acct_no ");
            //sql.Append(" AND stpcashe.vend_code= stpvendr.vend_code ");
            //sql.Append(" AND stpcashe.pay_to_code = stppytor.pay_to_code ");
            //sql.Append(" AND s1.insertby=s3.UserId");
            //sql.Append(" AND s2.acct_no=s4.acct_no");
            //sql.Append(" AND s2.orig_journal IN ('CD','PY') ");
            //sql.Append(" AND s2.reconciled='Y' ");

            //if (parameters[0] != null)
            //    if (parameters[0].ToString() != string.Empty && !parameters[0].ToString().Trim().Contains("1900"))
            //        sql.Append(" AND  date(s1.date_reconcile) >= date('" + parameters[0].ToString().Replace("'", "''") + "')");
            //if (parameters[1] != null)
            //    if (parameters[1].ToString() != string.Empty && !parameters[1].ToString().Trim().Contains("1900"))
            //        sql.Append(" AND  date(s1.date_reconcile) <=  date('" + parameters[1].ToString().Trim().Replace("'", "''") + "')");
            //if (Convert.ToInt32(parameters[2]) > 0)
            //    sql.Append(" AND s1.insertby =" + parameters[2].ToString());

            //sql.Append(" order by s1.doc_no ");
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
