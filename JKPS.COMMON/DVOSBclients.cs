using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOSBclients : DVOBase
    {
        //Written By Rohit for Saving Banks Client Master table :- sbclientsr
        //Written Date :30/01/2009

        private string _acct_cat;
        private Int32 _acct_no;
        private string _acct_id;
        private string _acct_status;
        private string _acct_closed;
        private string _last_name;
        private string _first_name;
        private string _n_join;
        private string _first_name1;
        private string _last_name1;
        private string _remarks;
        private string _address_1;
        private string _address_2;
        private DateTime _acct_date;
        private DateTime _close_date;
        private decimal _acct_balance;
        private decimal _active_balance;
        private int _Rowid;
        private string _telephone;
        private DateTime _date_of_birth;
        private string _occupation;
        private string _year;
        private string _old_acct_cat;
        private Int32 _old_acct_no;

        // Declare by rajeev 
        //Aim: To store the interest calculation date from the form
        // Modification Date: 02/02/2009
        private DateTime _Interest_cal_date;
        private int _TaxCalYear;

        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        private string _startdate;
        private string _enddate;



        #region Constructor
        public DVOSBclients()
        {
            _acct_cat = string.Empty;
            _acct_no = 0;
            _acct_id = string.Empty;
            _acct_status = string.Empty;
            _acct_closed = string.Empty;
            _last_name = string.Empty;
            _first_name = string.Empty;
            _n_join = string.Empty;
            _first_name1 = string.Empty;
            _last_name1 = string.Empty;
            _remarks = string.Empty;
            _address_1 = string.Empty;
            _address_2 = string.Empty;
            _acct_date = Convert.ToDateTime(null);
            _close_date = Convert.ToDateTime(null);
            _Rowid = 0;
            _telephone = string.Empty;
            _date_of_birth = Convert.ToDateTime(null);
            _occupation = string.Empty;
            _acct_balance = 0.0M;
            _active_balance = 0.0M;
            _Interest_cal_date = Convert.ToDateTime(null);
            _TaxCalYear = 0;

            _old_acct_cat = string.Empty;
            _old_acct_no = 0;

            _year = string.Empty;
            _startdate = string.Empty;
            _enddate = string.Empty;
            _InsertMachineInfo = "App";
            _InsertDate = DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = DateTime.Now;
            _UpdateBy = -1;




        }
        #endregion Constructor

        #region public properties
        public string acct_cat
        {
            get { return _acct_cat; }
            set { _acct_cat = value; }
        }
        public Int32 acct_no
        {
            get { return _acct_no; }
            set { _acct_no = value; }
        }//
        public string acct_id
        {
            get { return _acct_id; }
            set { _acct_id = value; }
        }
        public string acct_status
        {
            get { return _acct_status; }
            set { _acct_status = value; }
        }
        public string acct_closed
        {
            get { return _acct_closed; }
            set { _acct_closed = value; }
        }
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        public string n_join
        {
            get { return _n_join; }
            set { _n_join = value; }
        }
        public string first_name1
        {
            get { return _first_name1; }
            set { _first_name1 = value; }
        }
        public string last_name1
        {
            get { return _last_name1; }
            set { _last_name1 = value; }
        }
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        public string Address1
        {
            get { return _address_1; }
            set { _address_1 = value; }
        }
        public string Address2
        {
            get { return _address_2; }
            set { _address_2 = value; }
        }
        public DateTime acct_date
        {
            get { return _acct_date; }
            set { _acct_date = value; }
        }
        public DateTime close_date
        {
            get { return _close_date; }
            set { _close_date = value; }
        }
        public string telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
        public DateTime date_of_birth
        {
            get { return _date_of_birth; }
            set { _date_of_birth = value; }
        }
        public string occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }
        public int Rowid
        {
            get { return _Rowid; }
            set { _Rowid = value; }
        }

        public DateTime Interest_cal_date
        {
            get { return _Interest_cal_date; }
            set { _Interest_cal_date = value; }
        }

        public int TaxCalYear
        {
            get { return _TaxCalYear; }
            set { _TaxCalYear = value; }
        }
        public string old_acct_cat
        {
            get { return _old_acct_cat; }
            set { _old_acct_cat = value; }
        }
        public Int32 old_acct_no
        {
            get { return _old_acct_no; }
            set { _old_acct_no = value; }
        }
        public decimal acct_balance
        {
            get { return _acct_balance; }
            set { _acct_balance = value; }
        }
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
        public decimal active_balance
        {
            get { return _active_balance; }
            set { _active_balance = value; }
        }
        public string start_date
        {
            get { return _startdate; }
            set { _startdate = value; }
        }
        public string end_date
        {
            get { return _enddate; }
            set { _enddate = value; }

        }
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }


        #endregion public properties

        #region Stored-Procedures

        //****** Added by Bharat Dhall [04/27/2009] *******
        public string UPDATE_ACTIVE_BALANCE
        {
            get { return "uspSBActvBalUpd"; }
        }
        //***************************************************

        public override string INSERT_SPNAME
        {
            get { return "uspSBAcctIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "uspSBAcctUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "uspSBacctDel"; }
        }
        //
        public override string FIND_SPNAME
        {
            get { return ""; }
        }
        //********sunil till not in use
        public override string ALL_SPNAME
        {
            get { return "uspstgcntrcall"; }
        }
        //***************
       
        public override string TABLE_NAME
        {
            get { return "sbclientsr"; }
        }

        public override int UNIQUE_ID
        {
            get { return _Rowid; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return string.Empty; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public string GET_OPEN_BALANCE
        {
            get { return "uspclientopenbal"; }
        }

        public string GET_SAVING_BANK_ACCOUNT_BALANCE
        {
            get { return "uspSavBankDepGet"; }
        }
        public string SAVING_BANK_ACCOUNT_GET
        {
            get { return "uspsavbanktaccget "; }

        }
        public string GET_SBINTEREST
        {
            get { return "uspBankInterget"; }
        }

        public string GET_SAVING_BANK_TRANSACTION
        {
            get { return "uspsbAccountget"; }
        }


        //*************************************
        public string DELETE_FOR_CLOSE_SAVING_BANK_ACCOUNT
        {
            get { return "uspSbDtlDel"; }
        }
        public string SBCLIENT_DETAIL_FOR_YEAR_INTEREST
        {
            get { return "uspsb_clientget"; }
        }


        //     public string GET_ALL_FOR_CLOSE_SAVING_BANK_ACCOUNT
        //{
        //    get { return "uspstgcntrcall"; }
        //}

        //****************************************

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();

            if (parameters[7] == null) parameters[7] = string.Empty;
            if (parameters[7].ToString().Trim() == "SAVINGBANKCLIENTREPORT")
            {
                sql.Append("Select acct_id ,acct_cat ,acct_no ,acct_status ,acct_closed ,");
                sql.Append(" last_name ,first_name ,n_join ,first_name1,last_name1");
                sql.Append(" ,remarks ,address_1,address_2 ,acct_date,birth_date,occ_emp ");
                sql.Append(" ,active_balance,acct_balance,close_date  ");
                sql.Append(" from sbclientsr where 1=1 ");

                if (parameters[0] != null)
                    if (parameters[0].ToString().Trim() != string.Empty)
                        sql.Append(" AND sbclientsr.acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
                if (parameters[1] != null)
                    if (Convert.ToInt32(parameters[1]) > 0)
                        sql.Append(" AND sbclientsr.acct_no =" + parameters[1].ToString());

                if (parameters[2] != null)
                    if (parameters[2].ToString().Trim() != string.Empty)
                        sql.Append(" AND sbclientsr.last_name LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[3] != null)
                    if (parameters[3].ToString().Trim() != string.Empty)
                        sql.Append(" AND sbclientsr.first_name LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

                if (parameters[4] != null)
                    if (parameters[4].ToString().Trim() != string.Empty)
                        sql.Append(" AND sbclientsr.acct_status LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");

                if (parameters[5] != null)
                    if (!parameters[5].ToString().Trim().Contains("1900") && parameters[5].ToString().Trim() != string.Empty )
                        sql.Append(" and acct_date >= '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");//DocdateFrom
                if (parameters[6] != null)//YearTo
                    if (parameters[6].ToString() != "01/01/9998" && parameters[5].ToString().Trim() != string.Empty)
                    sql.Append(" and acct_date <= '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");//DocDateTo 

            }
            else
            {
                sql.Append("SELECT sbclientsr.acct_cat,sbclientsr.acct_no,sbclientsr.acct_status,sbclientsr.acct_closed,");
                sql.Append("sbclientsr.last_name,sbclientsr.first_name,sbclientsr.n_join,sbclientsr.first_name1,sbclientsr.last_name1,");
                sql.Append("sbclientsr.remarks,sbclientsr.address_1,sbclientsr.address_2,sbclientsr.acct_date,sbclientsr.close_date,");
                sql.Append("sbclientsr.telephone,sbclientsr.birth_date,sbclientsr.acct_balance,sbclientsr.occ_emp,sbclientsr.rowid ,");
                sql.Append("sbclientsr.acct_id,sbclientsr.active_balance ");
                sql.Append(" FROM sbclientsr ");
                //if (parameters[13].ToString() == string.Empty)
                //{
                //    sql.Append(" WHERE sbclientsr.acct_closed='N' ");
                //}
                //else
                //{
                    sql.Append("WHERE 1=1 ");
                //}
                if (parameters[0] != null)
                    if (parameters[0].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
                if (parameters[1] != null)
                    if (Convert.ToInt32(parameters[1]) > 0)
                        sql.Append(" AND sbclientsr.acct_no =" + parameters[1].ToString());

                if (parameters[2] != null)
                    if (parameters[2].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.first_name LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[3] != null)
                    if (parameters[3].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.last_name LIKE '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[4] != null)
                    if (parameters[4].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.acct_status LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[5] != null)
                    if (!parameters[5].ToString().Contains("0001"))
                        sql.Append(" AND sbclientsr.acct_date =" + "'" + parameters[5].ToString().Trim() + "'");
                if (parameters[6] != null)
                    if (parameters[6].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.first_name1 LIKE '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[7] != null)
                    if (parameters[7].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.last_name1 LIKE '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[8] != null)
                    if (parameters[8].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.address_1 LIKE '" + parameters[8].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[9] != null)
                    if (!parameters[9].ToString().Contains("0001"))
                        sql.Append(" AND sbclientsr.birth_date =" + "'" + parameters[9].ToString().Trim() + "'");
                if (parameters[10] != null)
                    if (parameters[10].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.n_join LIKE '" + parameters[10].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[11] != null)
                    if (parameters[11].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.occ_emp LIKE '" + parameters[11].ToString().Trim().Replace("'", "''") + "%'");
                if (Convert.ToInt32(parameters[12]) > 0)
                    sql.Append(" AND sbclientsr.rowid = " + parameters[12].ToString());
                if (parameters[13] != null)
                    if (parameters[13].ToString() != string.Empty)
                        sql.Append(" AND sbclientsr.acct_closed = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
                sql.Append(" order by sbclientsr.acct_id");
            }
            return sql.ToString();
        }

        public string FIND_SBInterest(ref Object[] parameters)
        {

            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("select sbclientsr.acct_id, sbclientsr.acct_cat,");
            sql.Append("sbclientsr.acct_no ,sbclientsr.last_name,");
            sql.Append("sbclientsr.first_name,sbclientsr.n_join,sbclientsr.first_name1,");
            sql.Append("sbclientsr.last_name1,sbclientsr.acct_balance,sbposttranr.deposit_amt ");
            sql.Append(" from  sbclientsr,sbposttranr");
            sql.Append(" where sbclientsr.acct_id=sbposttranr.acct_id");
            sql.Append(" and sbposttranr.tran_type='I'");
            sql.Append(" and year(doc_date)=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim() != string.Empty)
                    sql.Append(" and sbclientsr.acct_status=" + "'" + parameters[1].ToString().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim() != string.Empty)
                    sql.Append(" and sbclientsr.acct_closed=" + "'" + parameters[2].ToString().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString().Trim() != string.Empty)
                    sql.Append(" and sbclientsr.acct_cat=" + "'" + parameters[3].ToString().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" and sbclientsr.acct_no=" + parameters[4].ToString());

            return sql.ToString();
        }

        public string FIND_ALLCLIENTS(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sbclientsr.acct_cat,sbclientsr.acct_no,sbclientsr.acct_status,sbclientsr.acct_closed,");
            sql.Append("sbclientsr.last_name,sbclientsr.first_name,sbclientsr.n_join,sbclientsr.first_name1,sbclientsr.last_name1,");
            sql.Append("sbclientsr.remarks,sbclientsr.address_1,sbclientsr.address_2,sbclientsr.acct_date,sbclientsr.close_date,");
            sql.Append("sbclientsr.telephone,sbclientsr.birth_date,sbclientsr.acct_balance,sbclientsr.occ_emp,sbclientsr.rowid ,");
            sql.Append("sbclientsr.acct_id,sbclientsr.active_balance ");
            sql.Append(" FROM sbclientsr ");
            sql.Append(" order by sbclientsr.acct_id");
            return sql.ToString();
        }
        public string FIND_ACTIVECLIENTS(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sbclientsr.acct_cat,sbclientsr.acct_no,sbclientsr.acct_status,sbclientsr.acct_closed,");
            sql.Append("sbclientsr.last_name,sbclientsr.first_name,sbclientsr.n_join,sbclientsr.first_name1,sbclientsr.last_name1,");
            sql.Append("sbclientsr.remarks,sbclientsr.address_1,sbclientsr.address_2,sbclientsr.acct_date,sbclientsr.close_date,");
            sql.Append("sbclientsr.telephone,sbclientsr.birth_date,sbclientsr.acct_balance,sbclientsr.occ_emp,sbclientsr.rowid ,");
            sql.Append("sbclientsr.acct_id,sbclientsr.active_balance ");
            sql.Append(" FROM sbclientsr ");
            sql.Append(" WHERE sbclientsr.acct_closed='N' and sbclientsr.acct_status = 'ACTIVE'");
            sql.Append(" order by sbclientsr.acct_id");
            return sql.ToString();
        }

        public string FIND_ISEXISTSACCOUNT_NO(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT sbclientsr.acct_cat,sbclientsr.acct_no,sbclientsr.acct_status,sbclientsr.acct_closed,");
            sql.Append("sbclientsr.last_name,sbclientsr.first_name,sbclientsr.n_join,sbclientsr.first_name1,sbclientsr.last_name1,");
            sql.Append("sbclientsr.remarks,sbclientsr.address_1,sbclientsr.address_2,sbclientsr.acct_date,sbclientsr.close_date,");
            sql.Append("sbclientsr.telephone,sbclientsr.birth_date,sbclientsr.acct_balance,sbclientsr.occ_emp,sbclientsr.rowid ,");
            sql.Append("sbclientsr.acct_id,sbclientsr.active_balance ");
            sql.Append(" FROM sbclientsr ");
            sql.Append("WHERE 1=1 ");
            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    sql.Append(" AND sbclientsr.acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND sbclientsr.acct_no =" + parameters[1].ToString());
            if (parameters[2] != null)
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND sbclientsr.rowid != " + parameters[2].ToString());
            sql.Append(" order by sbclientsr.acct_id");
            return sql.ToString();
        }
        public string FIND_ACCT_DETAILS(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("Select s1.acct_id ,s1.acct_cat ,s1.acct_no ,");
            sql.Append(" s1.last_name ,s1.first_name ,s1.n_join ,s1.first_name1,s1.last_name1,");
            sql.Append(" s1.active_balance,s2.doc_date ");
            sql.Append(" from sbclientsr s1,sbposttranr s2 ");
            sql.Append(" WHERE s1.acct_id=s2.acct_id ");

            if (parameters[0] != null)
                if (parameters[0].ToString().Trim() != string.Empty)
                    sql.Append(" AND s1.acct_cat=" + "'" + parameters[0].ToString().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (Convert.ToInt32(parameters[1]) > 0)
                    sql.Append(" AND s1.acct_no =" + parameters[1].ToString());

            if (parameters[2] != null)
                if (parameters[2].ToString().Trim() != string.Empty)
                    sql.Append(" AND s1.acct_status LIKE '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3] != null)
                if (!parameters[3].ToString().Trim().Contains("1900") && parameters[3].ToString().Trim() != string.Empty)
                    sql.Append(" and s1.acct_date >= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");//DocdateFrom
            if (parameters[4] != null)//YearTo
                if (parameters[4].ToString() != "01/01/9998" && parameters[4].ToString().Trim() != string.Empty)
                    sql.Append(" and s1.acct_date <= '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");//DocDateTo 
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
