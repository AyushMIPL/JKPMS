using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterEmployeeIncomes : DVOBase
    {
        private string _empl_code;
        private string _inc_code;
        private int _line_no;
        private decimal? _inc_rate;
        private decimal? _inc_number;
        private decimal? _inc_hours;
        private int _acct_no;
        private string _department;
        private decimal? _inc_qtd1;
        private decimal? _inc_qtd2;
        private decimal? _inc_qtd3;
        private decimal? _inc_qtd4;
        private decimal? _inc_ytd;
        private decimal? _lo_inc_amt;
        private decimal? _hi_inc_amt;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;
        private int _rowid;
        private string _acct_no_kv;
        int _acct_no_typeid;
        string _acct_no_type;

        private string _SSN;
        private string _typeCode;
        private string _lastName;
        private string _firstName;
        private string _empl_status;
        private string _jobCode;
        private string _jobTitle;
        private string _pay_period;
        private string _lastPay;//date

        #region Constructor

        public DVOMasterEmployeeIncomes()
        {
            _empl_code = string.Empty;
            _inc_code = string.Empty;
            _line_no = 0;
            _inc_rate = null;
            _inc_number = null;
            _inc_hours = null;
            _acct_no = 0;
            _department = string.Empty;
            _inc_qtd1 = null;
            _inc_qtd2 = null;
            _inc_qtd3 = null;
            _inc_qtd4 = null;
            _inc_ytd = null;
            _lo_inc_amt = null;
            _hi_inc_amt = null;
            _rowid = 0;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";//date
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";//date
            _UpdateBy = 0;

            _acct_no_kv = string.Empty;
            _acct_no_typeid = 0;
            _acct_no_type = string.Empty;

            _SSN = string.Empty;
            _typeCode = string.Empty;
            _lastName = string.Empty;
            _firstName = string.Empty;
            _empl_status = string.Empty;
            _jobCode = string.Empty;
            _jobTitle = string.Empty;
            _pay_period = string.Empty;
            _lastPay = "01/01/1900";//date
        }

        #endregion Constructor

        #region public properties

        public int Rowid
        {
            get { return _rowid; }
            set { _rowid = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public string inc_code
        {
            get { return _inc_code; }
            set { _inc_code = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public decimal? inc_rate
        {
            get { return _inc_rate; }
            set { _inc_rate = value; }
        }
        public decimal? inc_number
        {
            get { return _inc_number; }
            set { _inc_number = value; }
        }
        public decimal? inc_hours
        {
            get { return _inc_hours; }
            set { _inc_hours = value; }
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
        public decimal? inc_qtd1
        {
            get { return _inc_qtd1; }
            set { _inc_qtd1 = value; }
        }
        public decimal? inc_qtd2
        {
            get { return _inc_qtd2; }
            set { _inc_qtd2 = value; }
        }
        public decimal? inc_qtd3
        {
            get { return _inc_qtd3; }
            set { _inc_qtd3 = value; }
        }
        public decimal? inc_qtd4
        {
            get { return _inc_qtd4; }
            set { _inc_qtd4 = value; }
        }
        public decimal? inc_ytd
        {
            get { return _inc_ytd; }
            set { _inc_ytd = value; }
        }
        public decimal? lo_inc_amt
        {
            get { return _lo_inc_amt; }
            set { _lo_inc_amt = value; }
        }
        public decimal? hi_inc_amt
        {
            get { return _hi_inc_amt; }
            set { _hi_inc_amt = value; }
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

        public string acct_no_kv
        {
            get { return _acct_no_kv; }
            set { _acct_no_kv = value; }
        }
        public int acct_no_typeid
        {
            get { return _acct_no_typeid; }
            set { _acct_no_typeid = value; }
        }
        public string acct_no_type
        {
            get { return _acct_no_type; }
            set { _acct_no_type = value; }
        }

        public string SSN
        {
            get { return _SSN; }
            set { _SSN = value; }
        }
        public string typeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }
        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string empl_status
        {
            get { return _empl_status; }
            set { _empl_status = value; }
        }
        public string jobCode
        {
            get { return _jobCode; }
            set { _jobCode = value; }
        }
        public string jobTitle
        {
            get { return _jobTitle; }
            set { _jobTitle = value; }
        }
        public string pay_period
        {
            get { return _pay_period; }
            set { _pay_period = value; }
        }
        public string lastPay
        {
            get { return _lastPay; }
            set { _lastPay = value; }
        }

        #endregion public properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmpIncomIns"; }
        }

        public override string  UPDATE_SPNAME
        {
            get { return "USP_EmpIncomeUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpIncmCodDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspEmpIncmCodGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_EmpIncomeGetAll"; }
        }

        public override string TABLE_NAME
        {
            get { return "MasterEmployeeIncomes"; }
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
      
        public string EMPLOYEEINCOME_AUTOPAY
        {
            get { return "USP_EmpIncomeGet"; }
        }
        public string EMPLINCOMECNT_AUTOPAY
        {
            get { return "USP_IncomeCountGet"; }
        }
        public string GET_ROWID
        {
            get { return "USP_PayId_RowID"; }
        }

        //*********************************************
        public override string FIND_QUERY(ref Object[] parameters)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT empl_code p_empl_code,inc_code p_inc_code,line_no p_line_no,inc_rate p_inc_rate,");
            sql.Append(" inc_number p_inc_number,inc_hours p_inc_hours,MasterEmployeeIncomes.acct_no p_acct_no,department p_department,");
            sql.Append(" inc_qtd1 p_inc_qtd1,inc_qtd2 p_inc_qtd2,inc_qtd3 p_inc_qtd3,inc_qtd4 p_inc_qtd4,inc_ytd p_inc_ytd,");
            sql.Append(" lo_inc_amt p_lo_inc_amt,hi_inc_amt p_hi_inc_amt,PayrollGLAccounts.keyvalue acct_no_kv,Flex_struct_Header.id acct_no_typeid,PayrollGLAccounts.acct_type");
            sql.Append(" FROM MasterEmployeeIncomes LEFT  outer JOIN (PayrollGLAccounts INNER JOIN Flex_struct_Header ON PayrollGLAccounts.acct_type=Flex_struct_Header.accounttype) ON MasterEmployeeIncomes.acct_no=PayrollGLAccounts.acct_no Where 1=1  ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND line_no = " + parameters[0].ToString().Trim());
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(inc_code) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[3]) > 0)
                sql.Append(" AND inc_rate = " + parameters[3].ToString().Trim());
            if (Convert.ToInt32(parameters[4]) > 0)
                sql.Append(" AND inc_number = " + parameters[4].ToString().Trim());
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND inc_hours = " + parameters[5].ToString().Trim());
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND MasterEmployeeIncomes.acct_no = " + parameters[6].ToString().Trim());
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(department) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[8]) > 0)
                sql.Append(" AND inc_qtd1 = " + parameters[8].ToString().Trim());
            if (Convert.ToInt32(parameters[9]) > 0)
                sql.Append(" AND inc_qtd2 = " + parameters[9].ToString().Trim());
            if (Convert.ToInt32(parameters[10]) > 0)
                sql.Append(" AND inc_qtd3 = " + parameters[10].ToString().Trim());
            if (Convert.ToInt32(parameters[11]) > 0)
                sql.Append(" AND inc_qtd4 = " + parameters[11].ToString().Trim());
            if (Convert.ToInt32(parameters[12]) > 0)
                sql.Append(" AND inc_ytd = " + parameters[12].ToString().Trim());
            if (Convert.ToInt32(parameters[13]) > 0)
                sql.Append(" AND lo_inc_amt = " + parameters[13].ToString().Trim());
            if (Convert.ToInt32(parameters[14]) > 0)
                sql.Append(" AND hi_inc_amt = " + parameters[14].ToString().Trim());

            if ((parameters[15] == null || parameters[15].ToString() == string.Empty) &&//_SSN
                (parameters[16] == null || parameters[16].ToString() == string.Empty) &&//_typeCode
                (parameters[17] == null || parameters[17].ToString() == string.Empty) &&//_lastName
                (parameters[18] == null || parameters[18].ToString() == string.Empty) &&//_firstName
                (parameters[19] == null || parameters[19].ToString() == string.Empty) &&//_empl_status
                (parameters[20] == null || parameters[20].ToString() == string.Empty) &&//_jobCode
                (parameters[21] == null || parameters[21].ToString() == string.Empty) &&//_jobTitle
                (parameters[22] == null || parameters[22].ToString() == string.Empty) &&//_pay_period
                (parameters[23] == null || parameters[23].ToString() == string.Empty || parameters[23].ToString().Trim() == "01/01/1900"))//_lastPay
            {
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            }
            else
            {
                sql.Append(" AND RTRIM(empl_code) IN (");
                sql.Append(" SELECT empl_code FROM MasterEmployee WHERE 1=1 ");
                if (parameters[1] != null)
                    if (parameters[1].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[15] != null)
                    if (parameters[15].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(soc_sec_num) = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[16] != null)
                    if (parameters[16].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.type_Code) = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[17] != null)
                    if (parameters[17].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(last_name) LIKE  '" + parameters[17].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[18] != null)
                    if (parameters[18].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(first_name) LIKE  '" + parameters[18].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[19] != null)
                    if (parameters[19].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.Empl_Status) = '" + parameters[19].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[20] != null)
                    if (parameters[20].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Job_Code) = '" + parameters[20].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[21] != null)
                    if (parameters[21].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Job_Title) = '" + parameters[21].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[22] != null)
                    if (parameters[22].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.Pay_Period) = '" + parameters[22].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[23] != null)
                    if (parameters[23].ToString().Trim() != string.Empty && !parameters[23].ToString().Trim().Contains("1900"))
                        sql.Append(" AND last_pay = '" + parameters[23].ToString().Trim().Replace("'", "''") + "'");
                sql.Append(" )");
            }
            return sql.ToString();
        }
        #endregion store-procedures
    }
}
