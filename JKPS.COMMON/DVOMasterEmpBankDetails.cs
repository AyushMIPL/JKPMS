using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterEmpBankDetails : DVOBase
    {
        private int _RowID;
        private string _empl_code;
        private int _line_no;
        private string _bank_code;//*
        private string _bank_desc;
        private string _bank_acct_no;//*
        private string _type;//*
        private decimal? _amount;//*
        private string _typeofacct;//*
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;
        private int _UpdateBy;

        private string _SSN;
        private string _typeCode;
        private string _lastName;
        private string _firstName;
        private string _empl_status;
        private string _jobCode;
        private string _jobTitle;
        private string _pay_period;
        private string _lastPay;//date

    private string _CBS_NAME;
    private string _BRANCH_CODE;
    private string _ACCOUNT_STATUS;
    private string _AADHAAR_STATUS;


    #region Constructor

    public DVOMasterEmpBankDetails()
        {
            _RowID = 0;
            _empl_code = string.Empty;
            _line_no = 0;
            _bank_code = string.Empty;
            _bank_desc = string.Empty;
            _bank_acct_no = string.Empty;
            _type = string.Empty;
            _amount = null;
            _typeofacct = string.Empty;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";//date
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";
            _UpdateBy = 0;

            _SSN = string.Empty;
            _typeCode = string.Empty;
            _lastName = string.Empty;
            _firstName = string.Empty;
            _empl_status = string.Empty;
            _jobCode = string.Empty;
            _jobTitle = string.Empty;
            _pay_period = string.Empty;
            _lastPay = "01/01/1900";//date

      _CBS_NAME = string.Empty;
      _BRANCH_CODE = string.Empty;
      _ACCOUNT_STATUS = string.Empty;
      _AADHAAR_STATUS = string.Empty;

    }

        #endregion Constructor

        #region public properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string empl_code
        {
            get { return _empl_code; }
            set { _empl_code = value; }
        }
        public int line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }
        public string bank_code
        {
            get { return _bank_code; }
            set { _bank_code = value; }
        }
        public string bank_desc
        {
            get { return _bank_desc; }
            set { _bank_desc = value; }
        }
        public string bank_acct_no
        {
            get { return _bank_acct_no; }
            set { _bank_acct_no = value; }
        }
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
        public decimal? amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string typeofacct
        {
            get { return _typeofacct; }
            set { _typeofacct = value; }
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

    public string CBS_NAME
    {
      get { return _CBS_NAME; }
      set { _CBS_NAME = value; }
    }

    public string BRANCH_CODE
    {
      get { return _BRANCH_CODE; }
      set { _BRANCH_CODE = value; }
    }

    public string ACCOUNT_STATUS
    {
      get { return _ACCOUNT_STATUS; }
      set { _ACCOUNT_STATUS = value; }
    }

    public string AADHAAR_STATUS
    {
      get { return _AADHAAR_STATUS; }
      set { _AADHAAR_STATUS = value; }
    }

    #endregion public properties

    #region Stored-Procedures

    public override string INSERT_SPNAME
        {
            get { return "USP_EmpDirDpstIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmpDirDpstUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmpDirDpstDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return ""; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_EmpDirDpstGetAll"; }
        }

        public override string TABLE_NAME
        {
            //changed from styempbd to MasterEmpBankDetails as found table name as MasterEmpBankDetails 
            //changed by rohit wadhwa on 20/05/2009 

            get { return "MasterEmpBankDetails"; }
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
            System.Text.StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MasterEmpBankDetails.emp_bank_ID p_RowID,empl_code p_empl_code,line_no p_line_no,MasterEmpBankDetails.bank_code p_bank_code,bank_acct_no p_bank_acct_no,");
            sql.Append(" type p_type,amount p_amount,typeofacct p_typeofacct,MasterBanks.bank_desc v_bank_desc");
            sql.Append(" FROM MasterEmpBankDetails,MasterBanks WHERE MasterEmpBankDetails.bank_code = MasterBanks.bank_code ");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND MasterEmpBankDetails.emp_bank_ID = " + parameters[0].ToString().Trim());
            if (Convert.ToInt32(parameters[2]) > 0)
                sql.Append(" AND line_no = " + parameters[2].ToString().Trim());
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmpBankDetails.bank_code) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(bank_acct_no) = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(type) = '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[6]) > 0)
                sql.Append(" AND amount = " + parameters[6].ToString().Trim());
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(typeofacct) = '" + parameters[7].ToString().Trim().Replace("'", "''") + "'");

            if ((parameters[8] == null || parameters[8].ToString() == string.Empty) &&//_SSN
                (parameters[9] == null || parameters[9].ToString() == string.Empty) &&//_typeCode
                (parameters[10] == null || parameters[10].ToString() == string.Empty) &&//_lastName
                (parameters[11] == null || parameters[11].ToString() == string.Empty) &&//_firstName
                (parameters[12] == null || parameters[12].ToString() == string.Empty) &&//_empl_status
                (parameters[13] == null || parameters[13].ToString() == string.Empty) &&//_jobCode
                (parameters[14] == null || parameters[14].ToString() == string.Empty) &&//_jobTitle
                (parameters[15] == null || parameters[15].ToString() == string.Empty) &&//_pay_period
                (parameters[16] == null || parameters[16].ToString() == string.Empty || parameters[16].ToString().Trim() == "01/01/1900"))//_lastPay
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
                if (parameters[8] != null)
                    if (parameters[8].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(soc_sec_num) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[9] != null)
                    if (parameters[9].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.type_Code) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[10] != null)
                    if (parameters[10].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Middle_name) LIKE  '" + parameters[10].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[11] != null)
                    if (parameters[11].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(first_name) LIKE  '" + parameters[11].ToString().Trim().Replace("'", "''") + "%'");
                if (parameters[12] != null)
                    if (parameters[12].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.Empl_Status) = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[13] != null)
                    if (parameters[13].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Job_Code) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[14] != null)
                    if (parameters[14].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(Job_Title) = '" + parameters[14].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[15] != null)
                    if (parameters[15].ToString() != string.Empty)
                        sql.Append(" AND RTRIM(MasterEmployee.Pay_Period) = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
                if (parameters[16] != null)
                    if (parameters[16].ToString().Trim() != string.Empty && !parameters[16].ToString().Trim().Contains("1900"))
                        sql.Append(" AND last_pay = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
                sql.Append(" )");
            }

            return sql.ToString();
        }
        #endregion store-procedures
    }
}
