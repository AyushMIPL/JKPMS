using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    /// <summary>
    ///      Aim*******************************Created By(Midified By)**********Created Date(Modivied Date)******
    /// 1) To Update Time Card                       Rajeev                                 21/11/08
    ///    
    /// 2) In Auto pay system to calculate Timecard info 
    /// </summary>
    public class DVOUpdateTimeCard : DVOBase
    {
        private int _RowID;
        /// <summary>
        /// Private Variable Used To initialize the value from the Table
        /// EmployeeTCard_Header
        /// </summary>
        /// 
        private int _empTcardID;
        private int _card_no;
        private string _empl_code;
        private string _empl_name;
        private DateTime _start_date;
        private DateTime _end_date;
        private string _used_flag;

        /// <summary>
        /// Private variable applicable only for SQL-Server
        /// </summary       
        private string _InsertMachineInfo;
        private DateTime _InsertDate;
        private int _InsertBy;

        private string _UpdateMachineInfo;
        private DateTime _UpdateDate;
        private int _UpdateBy;

        ///<summary>
        /// Declare private variable used to get the Description value 
        /// Table Used: MasterEmployeeIncomes,MasterIncCodes
        ///</summary>

        private string _inc_code_id; //Like MasterEmployeeIncomes
        private int _line_no_id;
        private decimal? _inc_rate_id;
        private decimal? _inc_number_id;
        private decimal? _inc_hours_id;
        private int _acct_no_id;
        private string _department_id;

        private decimal? _inc_Amount;
        private int _ret_acc_no;
        private string _ret_acc_type;
        private string _ret_keyvalue;
        private decimal? _lo_inc_amt_id;
        private decimal? _hi_inc_amt_id;

        private string _description_cr; //Like MasterIncCodes
        private decimal? _dflt_num_cr;
        private decimal? _dflt_rate_cr;
        private decimal? _dflt_hours_cr;
        private decimal? _dflt_lo_inc_amt_cr;
        private decimal? _dflt_hi_inc_amt_cr;
        private int _dflt_acct_cr;
        private string _dflt_dept_cr;
        private string _inc_type_cr;
        private string _dftAccountType;

        private string _add_code_cr;
        //***********************************
        private string _dfltkeyvalue;
        private int _timecd_acct_no;




        //Constructor to Assign the initial value to the decleared class variable
        public DVOUpdateTimeCard()
        {

            // EmployeeTCard_Header
            _RowID = 0;
            _empTcardID = 0;
            _card_no = 0;
            _empl_code = string.Empty;
            _empl_name = string.Empty;
            _start_date = Convert.ToDateTime("01/01/1900");
            _end_date = Convert.ToDateTime("01/01/1900");// DateTime.Now;
            _used_flag = "N";

            //Like MasterEmployeeIncomes
            _inc_code_id = string.Empty;
            _line_no_id = 0;
            _inc_rate_id = null;
            _inc_number_id = null;
            _inc_hours_id = null;
            _acct_no_id = 0;
            _department_id = string.Empty;

            _inc_Amount = null;
            _ret_acc_no = 0;
            _ret_acc_type = string.Empty;
            _ret_keyvalue = string.Empty;

            //Like MasterIncCodes
            _description_cr = string.Empty;
            //Still there is no use of these variables
            _dflt_num_cr = null;
            _dflt_rate_cr = null;
            _dflt_hours_cr = null;
            _dflt_acct_cr = 0;
            _dflt_dept_cr = string.Empty;
            _inc_type_cr = string.Empty;
            _dftAccountType = string.Empty;

            _InsertMachineInfo = "App";
            _InsertDate = Convert.ToDateTime("01/01/1900");// DateTime.Now;
            _InsertBy = -1;
            _UpdateMachineInfo = "App";
            _UpdateDate = Convert.ToDateTime("01/01/1900");// DateTime.Now;
            _UpdateBy = -1;
            _dfltkeyvalue = string.Empty;
            _lo_inc_amt_id = null;
            _hi_inc_amt_id = null;
            _dflt_lo_inc_amt_cr = null;
            _dflt_hi_inc_amt_cr = null;
            _add_code_cr = "N";
            _timecd_acct_no = 0;

        }


        #region StartProperties
        //
        public int EmpTcardID
        {
            get { return _empTcardID; }
            set { _empTcardID = value; }
        }
        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public int card_no
        {
            get { return _card_no; }
            set { _card_no = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string empl_name
        {
            get { return _empl_name; }
            set { _empl_name = value; }
        }
        public DateTime start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public DateTime end_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }
        public string used_flag
        {
            get { return _used_flag; }
            set { _used_flag = value; }
        }
        //Properties Used to get and set the variable value of the table "MasterEmployeeIncomes"        
        public string inc_code_id
        {
            get { return _inc_code_id; }
            set { _inc_code_id = value; }
        }
        public int line_no_id
        {
            get { return _line_no_id; }
            set { _line_no_id = value; }
        }
        public decimal? inc_rate_id
        {
            get { return _inc_rate_id; }
            set { _inc_rate_id = value; }
        }
        public decimal? inc_number_id
        {
            get { return _inc_number_id; }
            set { _inc_number_id = value; }
        }
        public decimal? inc_hours_id
        {
            get { return _inc_hours_id; }
            set { _inc_hours_id = value; }
        }
        public int acct_no_id
        {
            get { return _acct_no_id; }
            set { _acct_no_id = value; }
        }
        public decimal? lo_inc_amt_id
        {
            get { return _lo_inc_amt_id; }
            set { _lo_inc_amt_id = value; }
        }
        public decimal? hi_inc_amt_id
        {
            get { return _hi_inc_amt_id; }
            set { _hi_inc_amt_id = value; }
        }
        public decimal? dflt_lo_inc_amt_cr
        {
            get { return _dflt_lo_inc_amt_cr; }
            set { _dflt_lo_inc_amt_cr = value; }
        }
        public decimal? dflt_hi_inc_amt_cr
        {
            get { return _dflt_hi_inc_amt_cr; }
            set { _dflt_hi_inc_amt_cr = value; }
        }
        public string add_code_cr
        {
            get { return _add_code_cr; }
            set { _add_code_cr = value; }
        }





        public string department_id
        {
            get { return _department_id; }
            set { _department_id = value; }
        }

        //Properties Used to get and set the variable value of the table "PayrollGLAccounts"        

        public string description_cr
        {
            get { return _description_cr; }
            set { _description_cr = value; }
        }

        public decimal? dflt_num_cr
        {
            get { return _dflt_num_cr; }
            set { _dflt_num_cr = value; }
        }
        public decimal? dflt_rate_cr
        {
            get { return _dflt_rate_cr; }
            set { _dflt_rate_cr = value; }
        }
        public decimal? dflt_hours_cr
        {
            get { return _dflt_hours_cr; }
            set { _dflt_hours_cr = value; }
        }
        public int dflt_acct_cr
        {
            get { return _dflt_acct_cr; }
            set { _dflt_acct_cr = value; }
        }
        public string dflt_dept_cr
        {
            get { return _dflt_dept_cr; }
            set { _dflt_dept_cr = value; }
        }
        public string inc_type_cr
        {
            get { return _inc_type_cr; }
            set { _inc_type_cr = value; }
        }
        public string dftAccountType
        {
            get { return _dftAccountType; }
            set { _dftAccountType = value; }
        }


        public decimal? inc_Amount
        {
            get { return _inc_Amount; }
            set { _inc_Amount = value; }
        }

        public int ret_acc_no
        {
            get { return _ret_acc_no; }
            set { _ret_acc_no = value; }
        }
        public string ret_acc_type
        {
            get { return _ret_acc_type; }
            set { _ret_acc_type = value; }
        }
        public string ret_keyvalue
        {
            get { return _ret_keyvalue; }
            set { _ret_keyvalue = value; }
        }
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
            get
            {
                return _UpdateDate;
            }
            set { _UpdateDate = value; }
        }
        public int UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }
        public string dfltkeyvalue
        {
            get { return _dfltkeyvalue; }
            set { _dfltkeyvalue = value; }
        }
        public int timecd_acct_no
        {
            get { return _timecd_acct_no; }
            set { _timecd_acct_no = value; }
        }

        #endregion Properties


        #region Stored-Procedures
        public override string INSERT_SPNAME
        {
            get { return "USP_TCardIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_TCardUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_TCardDetDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "usptcardrecget"; }
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT EmpTcardID v_rowid,card_no v_card_no,empl_code v_empl_code,empl_name v_empl_name,start_date v_start_date,end_date v_end_date,used_flag v_used_flag FROM EmployeeTCard_Header where 1=1 and used_flag='N'");

            if (parameters[0] != null)
                if (parameters[0].ToString() != string.Empty)
                    if (!parameters[0].ToString().Contains("1900"))
                        sql.Append(" AND  start_date>=" + "'" + parameters[0].ToString() + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    if (!parameters[0].ToString().Contains("1900"))
                        sql.Append(" AND  end_date<=" + "'" + parameters[1].ToString() + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND  empl_code= " + "'" + parameters[2].ToString().Replace("'", "''") + "'");

            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND EmpTcardID = " + parameters[3].ToString());


            return sql.ToString();

        }

        public override string TABLE_NAME
        {
            get { return "EmployeeTCard_Header"; }
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


        public string FIND_INC_LOAD
        {
            get { return "USP_IncLoadGet"; }
        }

        public string FIND_INC_TIMECARD_Exist
        {
            get { return "USP_TimCardExstGet"; }
        }

        public string FIND_DETAILS_BY_INCCODE
        {
            get { return "USP_IncDtlGet"; }
        }

        public string INSERT_TIMEDETAIL
        {
            get { return "USP_TimeDetailIns"; }
        }
        public string FIND_DETILSEARCH_BY_CARDNO
        {
            get { return "USP_Timecdetailget"; }
        }
        public string UPDATE_DETAILS
        {
            get { return "USP_TimeCardDtlUpd"; }
        }
        public string FIND_INC_LINENO
        {
            get { return "USP_MinlineNoGet"; }
        }
       
        public string FIND_DETAILS_FORAUTOPAY
        {
            get { return "USP_EmpTimeIncome"; }
        }
        //**************************************
        public string FIND_DETAILS_FORAUTOPAY_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct MasterIncCodes.dfltaccounttype,EmployeeTCard_Details.acct_no,EmployeeTCard_Details.inc_code,");
			sql.Append(" EmployeeTCard_Details.line_no,EmployeeTCard_Details.card_no,EmployeeTCard_Details.inc_rate,EmployeeTCard_Details.inc_number,");
			sql.Append(" EmployeeTCard_Details.inc_hours,MasterIncCodes.description,MasterIncCodes.dflt_num,MasterIncCodes.dflt_rate,");
			sql.Append(" MasterIncCodes.dflt_hours,MasterIncCodes.dflt_lo_inc_amt,MasterIncCodes.dflt_hi_inc_amt,MasterIncCodes.dflt_acct,");
            sql.Append(" MasterIncCodes.dflt_dept,MasterIncCodes.inc_type,EmployeeTCard_Header.start_date,EmployeeTCard_Header.empl_code");
            sql.Append(" from EmployeeTCard_Details,MasterIncCodes,EmployeeTCard_Header");
            sql.Append(" where  MasterIncCodes.inc_code = EmployeeTCard_Details.inc_code and EmployeeTCard_Details.card_no = EmployeeTCard_Header.card_no");
            if (parameters[8] != null)
                if (parameters[8].ToString().Trim() != string.Empty && !parameters[8].ToString().Trim().Contains("1900") && !parameters[8].ToString().Trim().Contains("0001"))
                {
                    sql.Append(" and EmployeeTCard_Header.end_date >= '" + parameters[8].ToString().Trim() + "'");
                }
            sql.Append(" and EmployeeTCard_Details.card_no in (select EmployeeTCard_Header.card_no from EmployeeTCard_Header,MasterEmployee");
            sql.Append(" where EmployeeTCard_Header.empl_code = MasterEmployee.empl_code");
            sql.Append(" and (MasterEmployee.hold_pymnt is null or MasterEmployee.hold_pymnt != 'Y')");
            sql.Append("  and EmployeeTCard_Header.used_flag='N'");
            //if (Convert.ToInt32(parameters[0]) > 0)
            //    sql.Append(" AND MasterEmployee.RowID = " + parameters[0].ToString());
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.empl_code) = '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.soc_sec_num) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString().Trim() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.first_name) LIKE  '" + parameters[2].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[3].ToString().Trim() != string.Empty)
                sql.Append(" AND RTRIM(MasterEmployee.last_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[4] != null)
                if (parameters[4].ToString().Trim() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.type_code) LIKE '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString().Trim() != string.Empty)
                    sql.Append(" AND Rtrim(MasterEmployee.job_code) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[6] != null)
                if (parameters[6].ToString().Trim() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.job_title) = '" + parameters[6].ToString().Replace("'", "''") + "'");
            if (parameters[7] != null)
                if (parameters[7].ToString().Trim() != string.Empty)
                    sql.Append(" AND MasterEmployee.pay_period = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            sql.Append(" and not exists (select empl_code from Process_PayEmployee where Process_PayEmployee.empl_code = MasterEmployee.empl_code and Process_PayEmployee.ok_to_post not in ('P', 'C')");
            sql.Append(" and Process_PayEmployee.pay_date <'" + parameters[8].ToString().Trim().Replace("'", "''") + "')");
            // add check for termination date
            if (parameters[8] != null)
                if (parameters[8].ToString().Trim() != string.Empty && !parameters[8].ToString().Trim().Contains("1900") && !parameters[8].ToString().Trim().Contains("0001"))
                {
                    sql.Append(" and (MasterEmployee.terminated is null ");
                    sql.Append(" or MasterEmployee.terminated >='" + parameters[8].ToString().Trim() + "')");
                    sql.Append(" and EmployeeTCard_Header.end_date >= '" + parameters[8].ToString().Trim() + "'");
                }
            sql.Append(" )");
            sql.Append(" order by EmployeeTCard_Details.card_no, EmployeeTCard_Details.line_no");

            return sql.ToString();
        }
    
        public string Delete_TimeCard_Details
        {
            get { return "USP_TimeCardDtlDel"; }
        }
        #endregion Stored-Procedures

    }

}
