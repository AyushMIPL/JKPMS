using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMasterEmployee : DVOBase
    {
        private int _EmplrCode;
        private string _actPayrollDepartmentKeyValue;
        private string _EmplrName;
        private int _RowID;
        private string _EmplCode;
        private string _SocSecNum;
        private string _TypeCode;
        private string _Birthdate;//date
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _Address1;
        private string _Address2;
        private string _City;
        private string _State;
        private string _Zip;
        private string _Phone;
        private int _CashAcct;
        private string _Department;
        private string _JobCode;
        private string _JobTitle;
        private string _DateHired;//date
        private string _Terminated;//date
        private string _EmplStatus;
        private string _PayPeriod;
        private int _Allowances;
        private int _StateAllow;
        private string _MaritalStat;
        private string _VacCode;
        private decimal? _VacAllowed;
        private decimal? _VacUsed;
        private string _SickCode;
        private decimal? _SickAllowed;
        private decimal? _SickUsed;
        private string _LastPay;
        private string _HoldPayment;
        private string _StaTaxCode;
        private string _LocTaxCode;
        private string _SickAccrCodr;
        private int _SickAccrCtr;
        private string _SickLapseDate;//date
        private string _VacAccrCode;
        private int _VacAccrCtr;
        private string _VacLapseDate;//date
        private string _DirDept;
        private int _DfiDest;
        private int _ChkDigit;
        private string _BankAcctNo;
        private decimal? _StateUdf;
        private string _FlexDeptAcctType;
        private string _LastIncDate;//date
        private string _AppointDate;//date
        private string _LastVerified;//date
        private string _Gender;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;

        private string _cash_acct_kv;
        private string _type_desc;
        //Added By sanjay //For Report Print Employee List By Ministry
        private string _acct_type;
        private string _segment_code;
        private string _segment_value;

        private string _ded_code;
        private string _type_code;
        private string _month;
        private string _year;
        private string _inc_code;
        private string _mailid;

        private string _Prefix;
        private string _Suffix;
        private string _MaidenName;
        private string _Nationality;
        private string _PostalAddress;
        private string _PhoneOffice;
        private string _Mobile;
        private string _PensionerType;
        private string _PersonID;
        private int _LengthOfQualifyingServiceInMonthsTo31Dec2003;
        private int _LengthOfQualifyingServiceInMonthsFrom1Jan2014;
        private decimal _AnnualSalary;
        private string _Id;



        #region Constructor

        public DVOMasterEmployee()
        {
            _RowID = 0;
            _EmplCode = string.Empty;
            _SocSecNum = string.Empty;
            _TypeCode = string.Empty;
            _Birthdate = "01/01/1900";
            _FirstName = string.Empty;
            _MiddleName = string.Empty;
            _LastName = string.Empty;
            _Address1 = string.Empty;
            _Address2 = string.Empty;
            _City = string.Empty;
            _State = string.Empty;
            _Zip = string.Empty;
            _Phone = string.Empty;
            _CashAcct = 0;
            _Department = string.Empty;
            _JobCode = string.Empty;
            _JobTitle = string.Empty;
            _DateHired = "01/01/1900";
            _Terminated = "01/01/1900";
            _EmplStatus = string.Empty;
            _PayPeriod = string.Empty;
            _Allowances = 0;
            _StateAllow = 0;
            _MaritalStat = string.Empty;
            _VacCode = string.Empty;
            _VacAllowed = null;
            _VacUsed = null;
            _SickCode = string.Empty;
            _SickAllowed = null;
            _SickUsed = null;
            _LastPay = "01/01/1900";
            _HoldPayment = string.Empty;
            _StaTaxCode = string.Empty;
            _LocTaxCode = string.Empty;
            _SickAccrCodr = string.Empty;
            _SickAccrCtr = 0;
            _SickLapseDate = "01/01/1900";
            _VacAccrCode = string.Empty;
            _VacAccrCtr = 0;
            _VacLapseDate = "01/01/1900";
            _DirDept = string.Empty;
            _DfiDest = 0;
            _ChkDigit = 0;
            _BankAcctNo = string.Empty;
            _StateUdf = null;
            _FlexDeptAcctType = string.Empty;
            _LastIncDate = "01/01/1900";
            _AppointDate = "01/01/1900";
            _LastVerified = "01/01/1900";
            _Gender = string.Empty;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";
            _UpdateBy = 0;

            _cash_acct_kv = string.Empty;
            _type_desc = string.Empty;
            //Added By sanjay 
            _acct_type = string.Empty; ;
            _segment_code = string.Empty;
            _segment_value = string.Empty;
            _month = "01";
            _year = "1900";
            _ded_code = string.Empty;
            _type_code = string.Empty;
            _inc_code = string.Empty;
            _AnnualSalary = 0;
            _mailid = string.Empty;

            _Prefix = string.Empty;
            _Suffix = string.Empty;
            _MaidenName = string.Empty;
            _Nationality = string.Empty;
            _PostalAddress = string.Empty;
            _PhoneOffice = string.Empty;
            _Mobile = string.Empty;
            _PensionerType = string.Empty;
            _PersonID = string.Empty;
            _LengthOfQualifyingServiceInMonthsTo31Dec2003 = 0;
            _LengthOfQualifyingServiceInMonthsFrom1Jan2014 = 0;
            _actPayrollDepartmentKeyValue = string.Empty;

        }

        #endregion Constructor

        #region Properties

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string actPayrollDepartmentKeyValue
        {
            get { return _actPayrollDepartmentKeyValue; }
            set { _actPayrollDepartmentKeyValue = value; }
        }

        public decimal AnnualSalary
        {
            get { return _AnnualSalary; }
            set { _AnnualSalary = value; }
        }

        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }

        public string PensionerType
        {
            get { return _PensionerType; }
            set { _PensionerType = value; }
        }
        public int LengthOfQualifyingServiceInMonthsTo31Dec2003
        {
            get { return _LengthOfQualifyingServiceInMonthsTo31Dec2003; }
            set { _LengthOfQualifyingServiceInMonthsTo31Dec2003 = value; }
        }
        public int LengthOfQualifyingServiceInMonthsFrom1Jan2014
        {
            get { return _LengthOfQualifyingServiceInMonthsFrom1Jan2014; }
            set { _LengthOfQualifyingServiceInMonthsFrom1Jan2014 = value; }
        }

        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }
        public string Suffix
        {
            get { return _Suffix; }
            set { _Suffix = value; }
        }
        public string MaidenName
        {
            get { return _MaidenName; }
            set { _MaidenName = value; }
        }
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }
        public string PostalAddress
        {
            get { return _PostalAddress; }
            set { _PostalAddress = value; }
        }
        public string PhoneOffice
        {
            get { return _PhoneOffice; }
            set { _PhoneOffice = value; }
        }
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        public int EmplrCode
        {
            get { return _EmplrCode; }
            set { _EmplrCode = value; }
        }
        public string EmplrName
        {
            get { return _EmplrName; }
            set { _EmplrName = value; }
        }

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public string EmplCode
        {
            get { return _EmplCode; }
            set { _EmplCode = value; }
        }
        public string SocSecNum
        {
            get { return _SocSecNum; }
            set { _SocSecNum = value; }
        }
        public string TypeCode
        {
            get { return _TypeCode; }
            set { _TypeCode = value; }
        }
        public string Birthdate
        {
            get { return _Birthdate; }
            set { _Birthdate = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public int CashAcct
        {
            get { return _CashAcct; }
            set { _CashAcct = value; }
        }
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        public string JobCode
        {
            get { return _JobCode; }
            set { _JobCode = value; }
        }
        public string JobTitle
        {
            get { return _JobTitle; }
            set { _JobTitle = value; }
        }
        public string DateHired
        {
            get { return _DateHired; }
            set { _DateHired = value; }
        }
        public string Terminated
        {
            get { return _Terminated; }
            set { _Terminated = value; }
        }
        public string EmplStatus
        {
            get { return _EmplStatus; }
            set { _EmplStatus = value; }
        }
        public string PayPeriod
        {
            get { return _PayPeriod; }
            set { _PayPeriod = value; }
        }
        public int Allowances
        {
            get { return _Allowances; }
            set { _Allowances = value; }
        }
        public int StateAllow
        {
            get { return _StateAllow; }
            set { _StateAllow = value; }
        }
        public string MaritalStat
        {
            get { return _MaritalStat; }
            set { _MaritalStat = value; }
        }
        public string VacCode
        {
            get { return _VacCode; }
            set { _VacCode = value; }
        }
        public decimal? VacAllowed
        {
            get { return _VacAllowed; }
            set { _VacAllowed = value; }
        }
        public decimal? VacUsed
        {
            get { return _VacUsed; }
            set { _VacUsed = value; }
        }
        public string SickCode
        {
            get { return _SickCode; }
            set { _SickCode = value; }
        }
        public decimal? SickAllowed
        {
            get { return _SickAllowed; }
            set { _SickAllowed = value; }
        }
        public decimal? SickUsed
        {
            get { return _SickUsed; }
            set { _SickUsed = value; }
        }
        public string LastPay
        {
            get { return _LastPay; }
            set { _LastPay = value; }
        }
        public string HoldPayment
        {
            get { return _HoldPayment; }
            set { _HoldPayment = value; }
        }
        public string StaTaxCode
        {
            get { return _StaTaxCode; }
            set { _StaTaxCode = value; }
        }
        public string LocTaxCode
        {
            get { return _LocTaxCode; }
            set { _LocTaxCode = value; }
        }
        public string SickAccrCodr
        {
            get { return _SickAccrCodr; }
            set { _SickAccrCodr = value; }
        }
        public int SickAccrCtr
        {
            get { return _SickAccrCtr; }
            set { _SickAccrCtr = value; }
        }
        public string SickLapseDate
        {
            get { return _SickLapseDate; }
            set { _SickLapseDate = value; }
        }
        public string VacAccrCode
        {
            get { return _VacAccrCode; }
            set { _VacAccrCode = value; }
        }
        public int VacAccrCtr
        {
            get { return _VacAccrCtr; }
            set { _VacAccrCtr = value; }
        }
        public string VacLapseDate
        {
            get { return _VacLapseDate; }
            set { _VacLapseDate = value; }
        }
        public string DirDept
        {
            get { return _DirDept; }
            set { _DirDept = value; }
        }
        public int DfiDest
        {
            get { return _DfiDest; }
            set { _DfiDest = value; }
        }
        public int ChkDigit
        {
            get { return _ChkDigit; }
            set { _ChkDigit = value; }
        }
        public string BankAcctNo
        {
            get { return _BankAcctNo; }
            set { _BankAcctNo = value; }
        }
        public decimal? StateUdf
        {
            get { return _StateUdf; }
            set { _StateUdf = value; }
        }
        public string FlexDeptAcctType
        {
            get { return _FlexDeptAcctType; }
            set { _FlexDeptAcctType = value; }
        }
        public string LastIncDate
        {
            get { return _LastIncDate; }
            set { _LastIncDate = value; }
        }
        public string AppointDate
        {
            get { return _AppointDate; }
            set { _AppointDate = value; }
        }
        public string LastVerified
        {
            get { return _LastVerified; }
            set { _LastVerified = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
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

        public string cash_acct_kv
        {
            get { return _cash_acct_kv; }
            set { _cash_acct_kv = value; }
        }
        public string type_desc
        {
            get { return _type_desc; }
            set { _type_desc = value; }
        }
        //Added By sanjay 
        public string acct_type
        {
            get { return _acct_type; }
            set { _acct_type = value; }
        }
        public string segment_code
        {
            get { return _segment_code; }
            set { _segment_code = value; }
        }
        public string segment_value
        {
            get { return _segment_value; }
            set { _segment_value = value; }
        }

        public string month
        {
            get { return _month; }
            set { _month = value; }
        }
        public string year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string ded_code
        {
            get { return _ded_code; }
            set { _ded_code = value; }
        }
        public string type_code
        {
            get { return _type_code; }
            set { _type_code = value; }
        }

        public string inc_code
        {
            get { return _inc_code; }
            set { _inc_code = value; }
        }

        public string mailid
        {
            get { return _mailid; }
            set { _mailid = value; }
        }
        //rohit
        public string ApplicationReferenceNo { get; set; }
        public string SubmissionLocation { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string AppliedBy { get; set; }
        public string TSWO { get; set; }
        public string District { get; set; }
        public string NameoftheApplicant { get; set; }
        public int AgeInYears { get; set; }
        public string AgeInYears_String { get; set; }
        public string DoyouhaveBPLcard { get; set; }
        public string FatherOrHusbandOrGuardianName { get; set; }
        public string EMail { get; set; }
        public string Category { get; set; }
        public string PresentAddress { get; set; }
        public string PresentDistrict { get; set; }
        public string PresentVillageName { get; set; }
        public string Pincode { get; set; }
        public string PresentHalqaPanchayatMunicipalityName { get; set; }
        public string PresentTehsil { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentDistrict { get; set; }
        public string PermanentTehsil { get; set; }
        public string PermanentHalqaPanchayatMunicipalityName { get; set; }
        public string PermanentVillageName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNooftheApplicant { get; set; }
        public string BankName { get; set; }
        public string PercentageofDisability { get; set; }
        public string CivilCondition { get; set; }
        public string JKISSS { get; set; }
        public string ApplicationSanctionedunderSchemeName { get; set; }
        public string CurrentTask { get; set; }
        public string CurrentStatus { get; set; }
        public string LastTask { get; set; }
        public string VersionNo { get; set; }
        public string recordCount { get; set; }

        public string Userlog { get; set; }

        [Required(ErrorMessage = "Remarks is required.")]
        public string ReasonForChange { get; set; }
        public string ACCOUNT_STATUS { get; set; }
        public string SelectDistrict { get; set; }
        public string Last_pay_date { get; set; }
        public string Application_approve_on { get; set; }

        public string CBS_Name { get; set; }
        // public string CBS_Status { get; set; }
        //public string CBS_Branch { get; set; }



        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_EmployeeIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "USP_EmployeeUpd"; }
        }

        public override string DELETE_SPNAME
        {
            get { return "USP_EmployeeDel"; }
        }

        public override string FIND_SPNAME
        {
            get { return "uspEmployeeGet"; }
        }

        public override string ALL_SPNAME
        {
            get { return "USP_EmployeeGetAll"; }
        }
        public override string TABLE_NAME
        {
            get { return "MasterEmployee"; }
        }

        public override int UNIQUE_ID
        {
            get { return _RowID; }
        }

        public override string NOTES_TABLE_RECORD_ID
        {
            get { return _EmplCode; }
            set { _EmplCode = value; }
        }

        public string FIND_EXISTS_EMPL_CODE
        {
            get { return "USP_EmplcodeGet"; }
        }


        public string GET_EMP_SOCIAL_SECURITY_INFO
        {
            get { return "USP_EmpSocSecinfo"; }
        }

        public string GET_DTLANALFORALLPAYCODE
        {
            get { return "uspdtlanlbypaycode"; }
        }

        public string FND_TYPE_DETAILS
        {
            get { return "USP_EmplrInfoGet"; }
        }
        public string FND_INCOMES_DETAILS
        {
            get { return "USP_EmpIncGet"; }
        }
        public string FND_YTD_DETAILS
        {
            get { return "USP_IncYtdGet"; }
        }
        //**********************************************************
        public string UPD_ONHOLD
        {
            get { return "USP_onHoldUpd"; }
        }
        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT empl_code,soc_sec_num,MasterEmployee.type_code,birthdate,first_name,middle_name,last_name,address1,address2,city,state,zip,");
            sql.Append(" phone,MasterEmployee.cash_acct,MasterEmployee.department,job_code,job_title,date_hired,terminated,MasterEmployee.empl_status,MasterEmployee.pay_period,allowances,");
            sql.Append(" state_allow,marital_stat,MasterEmployee.vac_code,MasterEmployee.vac_allowed,vac_used,MasterEmployee.sick_code,MasterEmployee.sick_allowed,sick_used,last_pay,MasterEmployee.hold_pymnt,");
            sql.Append(" MasterEmployee.statax_code,MasterEmployee.loctax_code,MasterEmployee.sick_accr_code,sick_accr_ctr,sick_lapse_date,MasterEmployee.vac_accr_code,vac_accr_ctr,vac_lapse_date,");
            sql.Append(" dir_dept,dfi_dest,chk_digit,bank_acct_no,state_udf,flexdeptaccttype,last_inc_date,appoint_date,gender,");
            sql.Append(" MasterEmployee.EmployeeID,PayrollGLAccounts.keyvalue cash_acct_kv,MasterEmpType.description type_desc,mailid,");

            sql.Append(" Prefix,Suffix,MaidenName,Nationality,PostalAddress,PhoneOffice,MasterEmployee.Mobile,");
            sql.Append(" EmployerID,MasterEmployer.EmployerName,PensionerType,PersonID,AnnualSalary,currentTask,currentStatus,lastTask," +
              "versionNo,applicationReferenceNo,phoneOffice,eMail,percentageofDisability,areYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP," +
              "submissionLocation,submissionDate,selectTehsilSocialWelfareOffice_TSWO,selectDistrict,CAST(DATEDIFF(YEAR, birthdate, GETDATE())  AS VARCHAR(10)) as [age_InYears],doYouHaveBPLcard,fatherOrHusbandOrGuardianName,category,permanentAddress," +
              "permanentDistrict,permanentTehsil,permanentHalqaPanchayatOrMunicipalityName,permanentVillageName,applicationSanctionedunderSchemeName,presentVillageName,presentDistrict,presentAddress,civilCondition, PresentHalqaPanchayatOrMunicipalityName, PresentTehsil, MasterEmployee.ReasonForChange,MasterEmployee.Last_pay_date,MasterEmployee.Application_approve_on ");

            sql.Append(" FROM MasterEmployee LEFT  outer JOIN PayrollGLAccounts ON MasterEmployee.cash_acct=PayrollGLAccounts.acct_no ");
            sql.Append("    LEFT OUTER JOIN MasterEmpType ");
            sql.Append(" ON MasterEmployee.type_code=MasterEmpType.type_code ");
            sql.Append(" LEFT OUTER JOIN MasterEmployer ON MasterEmployer.ID=MasterEmployee.EmployerID Where 1 =1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND MasterEmployee.RowID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(soc_sec_num) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.type_Code) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty && Convert.ToDateTime(parameters[4].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND birthDate = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(first_name) LIKE  '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Middle_name) LIKE  '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(last_name) LIKE  '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Address1) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Address2) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(City) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[11] != null)
                if (parameters[11].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(State) = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[12] != null)
                if (parameters[12].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Zip) = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[13] != null)
                if (parameters[13].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Phone) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[14]) > 0)
                sql.Append(" AND MasterEmployee.Cash_Acct = " + parameters[14].ToString());
            if (parameters[15] != null)
                if (parameters[15].ToString() != string.Empty && Convert.ToDateTime(parameters[15].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Terminated = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[16] != null)
                if (parameters[16].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.hold_pymnt) = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[17] != null)
                if (parameters[17].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(FlexDeptAcctType) = '" + parameters[17].ToString().Replace("'", "''") + "'");
            if (parameters[18] != null)
                if (parameters[18].ToString() != string.Empty && Convert.ToDateTime(parameters[18].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Last_Inc_Date = '" + parameters[18].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[19] != null)
                if (parameters[19].ToString() != string.Empty && Convert.ToDateTime(parameters[19].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Appoint_Date = '" + parameters[19].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[20] != null)
                if (parameters[20].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Gender) = '" + parameters[20].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[21] != null)
                if (parameters[21].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.Empl_Status) = '" + parameters[21].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[22] != null)
                if (parameters[22].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Job_Code) = '" + parameters[22].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[23] != null)
                if (parameters[23].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Job_Title) = '" + parameters[23].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[24] != null)
                if (parameters[24].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.Pay_Period) = '" + parameters[24].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[25] != null)
                if (parameters[25].ToString() != string.Empty && Convert.ToDateTime(parameters[25].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND last_pay = '" + parameters[25].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[26] != null)
                if (parameters[26].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(mailid) LIKE  '" + parameters[26].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[27] != null)
                if (parameters[27].ToString() != string.Empty)
                    sql.Append(" AND PersonID = '" + parameters[27].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[28] != null)
                if (parameters[28].ToString() != string.Empty)
                    sql.Append(" AND applicationReferenceNo = '" + parameters[28].ToString().Trim().Replace("'", "''") + "'");


            return sql.ToString();
        }


        public string GET_EMP_UNPAID_DEDUCTIONS
        {
            get { return "uspunpaiddedget"; }
        }


        public string FIND_GET_EMP_NOT_TERMINATED
        {
            get { return "USP_EmpNotTermiGet"; }
        }

        public string PayrollAnalysis
        {
            get { return "Usp_GetAnalysisdetails"; }
        }
        public string PayrollAccountAnalysis
        {
            get { return "Usp_GetACAnalysisdetails"; }
        }
        public string GET_Employee_List_Without_SocialSecurity(ref Object[] parameters)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT empl_code,soc_sec_num,MasterEmployee.type_code,birthdate,first_name,middle_name,last_name,address1,address2,city,state,zip,");
            sql.Append(" phone,MasterEmployee.cash_acct,MasterEmployee.department,job_code,job_title,date_hired,terminated,MasterEmployee.empl_status,MasterEmployee.pay_period,allowances,");
            sql.Append(" state_allow,marital_stat,MasterEmployee.vac_code,MasterEmployee.vac_allowed,vac_used,MasterEmployee.sick_code,MasterEmployee.sick_allowed,sick_used,last_pay,MasterEmployee.hold_pymnt,");
            sql.Append(" MasterEmployee.statax_code,MasterEmployee.loctax_code,MasterEmployee.sick_accr_code,sick_accr_ctr,sick_lapse_date,MasterEmployee.vac_accr_code,vac_accr_ctr,vac_lapse_date,");
            sql.Append(" dir_dept,dfi_dest,chk_digit,bank_acct_no,state_udf,flexdeptaccttype,last_inc_date,appoint_date,gender,");
            sql.Append(" MasterEmployee.EmployeeID,PayrollGLAccounts.keyvalue cash_acct_kv,MasterEmpType.description type_desc,mailid");
            sql.Append(" Prefix,Suffix,MaidenName,Nationality,PostalAddress,PhoneOffice,MasterEmployee.Mobile,");
            sql.Append(" EmployerID,MasterEmployer.EmployerName,PensionerType,PersonID,AnnualSalary");
            sql.Append(" FROM MasterEmployee LEFT  outer JOIN PayrollGLAccounts ON MasterEmployee.cash_acct=PayrollGLAccounts.acct_no ");
            sql.Append("    LEFT OUTER JOIN MasterEmpType ");
            sql.Append(" ON MasterEmployee.type_code=MasterEmpType.type_code ");
            sql.Append(" LEFT OUTER JOIN MasterEmployer ON MasterEmployer.ID=MasterEmployee.EmployerID Where 1 =1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND MasterEmployee.RowID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(soc_sec_num) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.type_Code) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty && Convert.ToDateTime(parameters[4].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND birthDate = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(first_name) LIKE  '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Middle_name) LIKE  '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(last_name) LIKE  '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Address1) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Address2) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(City) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[11] != null)
                if (parameters[11].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(State) = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[12] != null)
                if (parameters[12].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Zip) = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[13] != null)
                if (parameters[13].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Phone) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[14]) > 0)
                sql.Append(" AND MasterEmployee.Cash_Acct = " + parameters[14].ToString());
            if (parameters[15] != null)
                if (parameters[15].ToString() != string.Empty && Convert.ToDateTime(parameters[15].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Terminated = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[16] != null)
                if (parameters[16].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.hold_pymnt) = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[17] != null)
                if (parameters[17].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(FlexDeptAcctType) = '" + parameters[17].ToString().Replace("'", "''") + "'");
            if (parameters[18] != null)
                if (parameters[18].ToString() != string.Empty && Convert.ToDateTime(parameters[18].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Last_Inc_Date = '" + parameters[18].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[19] != null)
                if (parameters[19].ToString() != string.Empty && Convert.ToDateTime(parameters[19].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Appoint_Date = '" + parameters[19].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[20] != null)
                if (parameters[20].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Gender) = '" + parameters[20].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[21] != null)
                if (parameters[21].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.Empl_Status) = '" + parameters[21].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[22] != null)
                if (parameters[22].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Job_Code) = '" + parameters[22].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[23] != null)
                if (parameters[23].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Job_Title) = '" + parameters[23].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[24] != null)
                if (parameters[24].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.Pay_Period) = '" + parameters[24].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[25] != null)
                if (parameters[25].ToString() != string.Empty && Convert.ToDateTime(parameters[25].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND last_pay = '" + parameters[25].ToString().Trim().Replace("'", "''") + "'");



            return sql.ToString();

        }

        public string GET_PayrollAnalysis(ref Object[] parameters)
        {

            StringBuilder sqlquery = new StringBuilder("select process_payemployee.PayProcess_id,process_payemployee.doc_no,process_payemployee.doc_date,process_payemployee.pay_date,process_payemployee.eop_date, ");
            sqlquery.Append("process_payemployee.cash_amount,process_payemployee.inc_gross,Process_PayDeductions.ded_code,Process_PayDeductions.amount,Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc] as SegDesc, ");
            sqlquery.Append("Flex_Segment_Value_Details.keyvalue, MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name, ");
            sqlquery.Append("  MasterEmployee.middle_name,MasterEmployee.terminated,Process_Payobligations.obl_code,Process_Payobligations.amount as oblamount from process_payemployee LEFT OUTER JOIN Process_PayDeductions ON Process_PayEmployee.doc_no= ");
            sqlquery.Append(" Process_PayDeductions.doc_no LEFT OUTER JOIN  MasterEmployee ON process_payemployee.empl_code=Masteremployee.empl_code LEFT OUTER JOIN ");
            sqlquery.Append("Flex_Segment_Reference ON MasterEmployee.empl_code = Flex_Segment_Reference.code LEFT OUTER JOIN  Flex_Segment_Value_Details ON Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id LEFT OUTER JOIN Master_Segment ON Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid  LEFT OUTER JOIN Flex_struct_Header On  MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype ");
            sqlquery.Append("  LEFT OUTER JOIN Flex_struct_Details  ON Flex_struct_Header.id = Flex_struct_Details.strucid  LEFT OUTER JOIN Process_Payobligations on process_payemployee.doc_no =Process_Payobligations.doc_no where     Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid ");
            sqlquery.Append("AND Flex_Segment_Reference.entity_type = 'styemplr'                 and Flex_Segment_Value_Details.segmentid=2   AND ok_to_post IN('Y','P') ");
            if (parameters[0] != null && parameters[0].ToString() != string.Empty)
                sqlquery.Append("and pay_date >= '" + parameters[0] + "' ");
            if (parameters[1] != null && parameters[1].ToString() != string.Empty)
                sqlquery.Append(" AND pay_date <= '" + parameters[1] + "' ");
            sqlquery.Append(" order by doc_no ASC");

            return sqlquery.ToString();

        }

        public string GET_Employee_List_By_Ministry
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("select Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc] as SegDesc,Flex_Segment_Value_Details.keyvalue,MasterCompany.company_name,");
                sqlquery.Append("MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,");
                sqlquery.Append("MasterEmployee.middle_name,MasterEmployee.terminated from Flex_Segment_Reference, MasterEmployee, Flex_Segment_Value_Details, Master_Segment,Flex_struct_Header,");
                sqlquery.Append("Flex_struct_Details,MasterCompany where MasterEmployee.empl_code = Flex_Segment_Reference.code ");
                sqlquery.Append(" AND MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype");
                sqlquery.Append(" AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid");
                sqlquery.Append(" AND Flex_Segment_Reference.entity_type = 'styemplr'");
                if (acct_type.ToString() != string.Empty && acct_type != null)
                    sqlquery.Append(" AND Flex_struct_Header.accounttype='" + acct_type.ToString().Replace("'", "''") + "'");
                if (segment_code.ToString() != string.Empty && segment_code != null)
                    sqlquery.Append(" AND Master_Segment.SegmentDesc ='" + segment_code.ToString().Replace("'", "''") + "'");
                if (segment_value.ToString() != string.Empty && segment_value != null)
                    sqlquery.Append(" AND Flex_Segment_Value_Details.[desc] = '" + segment_value.ToString().Replace("'", "''") + "'");

                return sqlquery.ToString();
            }
        }


        public string GET_Employee_List_By_Ministrywithcountry
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("select   A.position,A.SegmentDesc,A.SegDesc,A.keyvalue,A.company_name,A.Empl_Code,A.first_name,A.flexdeptaccttype,A.last_name,A.middle_name,A.terminated,Datediff(yy,A.birthdate,'" + LastPay + "') As 'currentAge',A.birthdate,A.Country,A.gender,B.GrossAmount,B.NetAmount,A.State from ");


                sqlquery.Append("(Select empl_code, SUM(cash_amount	) AS NetAmount ,SUM(inc_gross) AS GrossAmount  from Process_PayEmployee  where  MONTH( pay_date)=MONTH('" + LastPay + "') and YEAR(pay_date)=YEAR('" + LastPay + "') and ok_to_post='P' Group By Process_PayEmployee.empl_code) B LEFT OUTER JOIN");
                sqlquery.Append("(select Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc] as SegDesc,Flex_Segment_Value_Details.keyvalue,MasterCompany.company_name,");
                sqlquery.Append("MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,");
                sqlquery.Append("MasterEmployee.middle_name,MasterEmployee.terminated,Datediff(yy,masteremployee.birthdate,'" + LastPay + "') As 'currentAge',birthdate,country,gender,MasterEmployee.State from MasterEmployee LEFT OUTER JOIN Flex_struct_Header ON  MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype , Flex_Segment_Reference, Flex_Segment_Value_Details, Master_Segment,");
                sqlquery.Append("Flex_struct_Details,MasterCompany where MasterEmployee.empl_code = Flex_Segment_Reference.code ");
                //      sqlquery.Append("--AND Masteremployee.empl_code  IN( Select empl_code from Process_PayEmployee where month(pay_date) =month('01/01/2013') and YEAR(pay_date) =YEAR('01/01/2013'))");

                sqlquery.Append(" AND MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype ");

                sqlquery.Append(" AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid ");

                if (acct_type.ToString() != string.Empty && acct_type != null)
                    sqlquery.Append(" AND Flex_struct_Header.accounttype='" + acct_type.ToString().Replace("'", "''") + "'");
                if (segment_code.ToString() != string.Empty && segment_code != null)
                    sqlquery.Append(" AND Master_Segment.SegmentDesc ='" + segment_code.ToString().Replace("'", "''") + "'");
                if (segment_value.ToString() != string.Empty && segment_value != null)
                    sqlquery.Append(" AND Flex_Segment_Value_Details.[desc] = '" + segment_value.ToString().Replace("'", "''") + "'");

                sqlquery.Append(" AND Flex_Segment_Reference.entity_type = 'styemplr') A ");
                sqlquery.Append(" ON B.empl_code =A.empl_code");

                return sqlquery.ToString();
            }
        }

        public string GET_Employee_List_By_Ministrywithdegree
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("select   A.position,A.SegmentDesc,A.SegDesc,A.keyvalue,A.company_name,A.Empl_Code,A.first_name,A.flexdeptaccttype,A.last_name,A.middle_name,A.terminated,Datediff(yy,A.birthdate,'" + LastPay + "') As 'currentAge',A.birthdate,A.Country,A.gender,B.GrossAmount,B.NetAmount,A.State,B.pay_date,Masteremployeeincomes.inc_rate from ");


                sqlquery.Append("(Select empl_code,pay_date, SUM(cash_amount	) AS NetAmount ,SUM(inc_gross) AS GrossAmount  from Process_PayEmployee  where  MONTH( pay_date)=MONTH('" + LastPay + "') and YEAR(pay_date)=YEAR('" + LastPay + "') and ok_to_post='P' Group By Process_PayEmployee.empl_code,pay_date) B LEFT OUTER JOIN");
                sqlquery.Append("(select Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc] as SegDesc,Flex_Segment_Value_Details.keyvalue,MasterCompany.company_name,");
                sqlquery.Append("MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,");
                sqlquery.Append("MasterEmployee.middle_name,MasterEmployee.terminated,Datediff(yy,masteremployee.birthdate,'" + LastPay + "') As 'currentAge',birthdate,country,gender,MasterEmployee.State from MasterEmployee LEFT OUTER JOIN Flex_struct_Header ON  MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype , Flex_Segment_Reference, Flex_Segment_Value_Details, Master_Segment,");
                sqlquery.Append("Flex_struct_Details,MasterCompany where MasterEmployee.empl_code = Flex_Segment_Reference.code ");
                //      sqlquery.Append("--AND Masteremployee.empl_code  IN( Select empl_code from Process_PayEmployee where month(pay_date) =month('01/01/2013') and YEAR(pay_date) =YEAR('01/01/2013'))");

                sqlquery.Append(" AND MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype ");

                sqlquery.Append(" AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid ");

                if (acct_type.ToString() != string.Empty && acct_type != null)
                    sqlquery.Append(" AND Flex_struct_Header.accounttype='" + acct_type.ToString().Replace("'", "''") + "'");
                if (segment_code.ToString() != string.Empty && segment_code != null)
                    sqlquery.Append(" AND Master_Segment.SegmentDesc ='" + segment_code.ToString().Replace("'", "''") + "'");
                if (segment_value.ToString() != string.Empty && segment_value != null)
                    sqlquery.Append(" AND Flex_Segment_Value_Details.[desc] = '" + segment_value.ToString().Replace("'", "''") + "'");

                sqlquery.Append(" AND Flex_Segment_Reference.entity_type = 'styemplr') A ");
                sqlquery.Append(" ON B.empl_code =A.empl_code INNER JOIN MasteremployeeIncomes ON A.empl_code =MasteremployeeIncomes.empl_code and  MasteremployeeIncomes.inc_rate IN( 10.0,18.75) ");

                return sqlquery.ToString();
            }
        }
        public string GET_Employee_List_By_Employer
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("select Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc] as SegDesc,Flex_Segment_Value_Details.keyvalue,MasterCompany.company_name,");
                sqlquery.Append("MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,");
                sqlquery.Append("MasterEmployee.middle_name,MasterEmployee.terminated,MasterEmployee.hold_pymnt,MasterEmployee.Soc_sec_num,MasterEmployee.Type_code from Flex_Segment_Reference, MasterEmployee, Flex_Segment_Value_Details, Master_Segment,Flex_struct_Header,");
                sqlquery.Append("Flex_struct_Details,MasterCompany where MasterEmployee.empl_code = Flex_Segment_Reference.code ");
                sqlquery.Append(" AND MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype");
                sqlquery.Append(" AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid");
                sqlquery.Append(" AND Flex_Segment_Reference.entity_type = 'styemplr' AND Masteremployee.terminated IS NULL ");
                if (acct_type.ToString() != string.Empty && acct_type != null)
                    sqlquery.Append(" AND Flex_struct_Header.accounttype='" + acct_type.ToString().Replace("'", "''") + "'");
                if (segment_code.ToString() != string.Empty && segment_code != null)
                    sqlquery.Append(" AND Master_Segment.SegmentDesc ='" + segment_code.ToString().Replace("'", "''") + "'");
                if (segment_value.ToString() != string.Empty && segment_value != null)
                    sqlquery.Append(" AND Flex_Segment_Value_Details.[desc] = '" + segment_value.ToString().Replace("'", "''") + "'");

                return sqlquery.ToString();
            }
        }
        public string GET_Employee_ListwithoutBankAcct
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("select Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc] as SegDesc,Flex_Segment_Value_Details.keyvalue,MasterCompany.company_name,");
                sqlquery.Append("MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,");
                sqlquery.Append("MasterEmployee.middle_name,MasterEmployee.terminated ,Masteremployee.Birthdate,Masteremployee.phone from Flex_Segment_Reference, MasterEmployee, Flex_Segment_Value_Details, Master_Segment,Flex_struct_Header,");
                sqlquery.Append("Flex_struct_Details,MasterCompany where MasterEmployee.empl_code = Flex_Segment_Reference.code ");
                sqlquery.Append(" AND MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype");
                sqlquery.Append(" AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid");
                sqlquery.Append(" AND Flex_Segment_Reference.entity_type = 'styemplr'");
                if (acct_type.ToString() != string.Empty && acct_type != null)
                    sqlquery.Append(" AND Flex_struct_Header.accounttype='" + acct_type.ToString().Replace("'", "''") + "'");
                if (segment_code.ToString() != string.Empty && segment_code != null)
                    sqlquery.Append(" AND Master_Segment.SegmentDesc ='" + segment_code.ToString().Replace("'", "''") + "'");
                if (segment_value.ToString() != string.Empty && segment_value != null)
                    sqlquery.Append(" AND Flex_Segment_Value_Details.[desc] = '" + segment_value.ToString().Replace("'", "''") + "'");
                sqlquery.Append(" AND  dir_dept='N' ");
                return sqlquery.ToString();
            }
        }

        public string GET_Employee_Listwithoutsocsec
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("select Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc] as SegDesc,Flex_Segment_Value_Details.keyvalue,MasterCompany.company_name,");
                sqlquery.Append("MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,");
                sqlquery.Append("MasterEmployee.middle_name,MasterEmployee.terminated ,Masteremployee.Birthdate,Masteremployee.phone, Masteremployee.Soc_sec_num from Flex_Segment_Reference, MasterEmployee, Flex_Segment_Value_Details, Master_Segment,Flex_struct_Header,");
                sqlquery.Append("Flex_struct_Details,MasterCompany where MasterEmployee.empl_code = Flex_Segment_Reference.code ");
                sqlquery.Append(" AND MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype");
                sqlquery.Append(" AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid");
                sqlquery.Append(" AND Flex_Segment_Reference.entity_type = 'styemplr'");
                if (acct_type.ToString() != string.Empty && acct_type != null)
                    sqlquery.Append(" AND Flex_struct_Header.accounttype='" + acct_type.ToString().Replace("'", "''") + "'");
                if (segment_code.ToString() != string.Empty && segment_code != null)
                    sqlquery.Append(" AND Master_Segment.SegmentDesc ='" + segment_code.ToString().Replace("'", "''") + "'");
                if (segment_value.ToString() != string.Empty && segment_value != null)
                    sqlquery.Append(" AND Flex_Segment_Value_Details.[desc] = '" + segment_value.ToString().Replace("'", "''") + "'");
                sqlquery.Append(" AND ( MasterEmployee.soc_sec_num IN('','999999') ");
                sqlquery.Append(" OR LEN( MasterEmployee.Soc_sec_num) !=6 )");
                return sqlquery.ToString();
            }
        }
        public string GET_Employee_Forverification
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("  Select A.position,A.SegmentDesc,A.SegDesc,A.keyvalue,A.company_name,A.empl_code,A.first_name,A.flexdeptaccttype,A.last_name,A.middle_name,A.terminated ,A.Birthdate,A.phone, A.Soc_sec_num,");
                sqlquery.Append(" A.dir_dept , MasterEmpBankDetails.typeofacct,MasterEmpBankDetails.bank_acct_no,MasterBanks.bank_desc FROM (select Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc]");
                sqlquery.Append("  as SegDesc,Flex_Segment_Value_Details.keyvalue,MasterCompany.company_name,MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,MasterEmployee.middle_name,MasterEmployee.terminated ,Masteremployee.Birthdate,Masteremployee.phone, Masteremployee.Soc_sec_num,");
                sqlquery.Append(" MasterEmployee.dir_dept from Flex_Segment_Reference, MasterEmployee, Flex_Segment_Value_Details, Master_Segment,Flex_struct_Header,");
                sqlquery.Append(" Flex_struct_Details,MasterCompany Where MasterEmployee.empl_code = Flex_Segment_Reference.code ");
                sqlquery.Append("   AND MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid");
                sqlquery.Append("  AND Flex_Segment_Reference.entity_type = 'styemplr'");
                if (acct_type.ToString() != string.Empty && acct_type != null)
                    sqlquery.Append(" AND Flex_struct_Header.accounttype='" + acct_type.ToString().Replace("'", "''") + "'");
                if (segment_code.ToString() != string.Empty && segment_code != null)
                    sqlquery.Append(" AND Master_Segment.SegmentDesc ='" + segment_code.ToString().Replace("'", "''") + "'");
                if (segment_value.ToString() != string.Empty && segment_value != null)
                    sqlquery.Append(" AND Flex_Segment_Value_Details.[desc] = '" + segment_value.ToString().Replace("'", "''") + "'");

                sqlquery.Append(" )A LEFT OUTER JOIN MasterEmpBankDetails ON A.empl_code =MasterEmpBankDetails.empl_code LEFT OUTER JOIN MasterBanks ON  MasterEmpBankDetails.bank_code = MasterBanks.bank_code ");
                //sqlquery.Append(" AND ( A.soc_sec_num IN('','999999') ");
                //sqlquery.Append(" OR LEN( A.Soc_sec_num) !=6 )");
                return sqlquery.ToString();
            }
        }
        public string GET_EmployerList
        {
            get
            {
                StringBuilder sqlquery = new StringBuilder("select Flex_struct_Details.position,Master_Segment.SegmentDesc,Flex_Segment_Value_Details.[desc] as SegDesc,Flex_Segment_Value_Details.keyvalue,MasterCompany.company_name,");
                sqlquery.Append("MasterEmployee.empl_code,MasterEmployee.first_name,MasterEmployee.flexdeptaccttype,MasterEmployee.last_name,");
                sqlquery.Append("MasterEmployee.middle_name,MasterEmployee.terminated ,Masteremployee.Birthdate,Masteremployee.phone, Masteremployee.Soc_sec_num ,Flex_Segment_Value_Details.abbreviation,ApplicationReferenceNo,Masteremployee.PermanentDistrict,Masteremployee.PermanentTehsil  from Flex_Segment_Reference, MasterEmployee, Flex_Segment_Value_Details, Master_Segment,Flex_struct_Header,");
                sqlquery.Append("Flex_struct_Details,MasterCompany where MasterEmployee.empl_code = Flex_Segment_Reference.code ");
                sqlquery.Append(" AND MasterEmployee.flexdeptaccttype = Flex_struct_Header.accounttype");
                sqlquery.Append(" AND Flex_Segment_Reference.segvd_id = Flex_Segment_Value_Details.id AND Flex_Segment_Value_Details.segmentid = Master_Segment.Segmentid ");
                sqlquery.Append(" AND Flex_struct_Header.id = Flex_struct_Details.strucid AND Flex_struct_Details.flexsegid = Flex_Segment_Value_Details.segmentid");
                sqlquery.Append(" AND Flex_Segment_Reference.entity_type = 'styemplr'");
                if (acct_type.ToString() != string.Empty && acct_type != null)
                    sqlquery.Append(" AND Flex_struct_Header.accounttype='" + acct_type.ToString().Replace("'", "''") + "'");
                if (segment_code.ToString() != string.Empty && segment_code != null)
                    sqlquery.Append(" AND Master_Segment.SegmentDesc ='" + segment_code.ToString().Replace("'", "''") + "'");
                if (segment_value.ToString() != string.Empty && segment_value != null)
                    sqlquery.Append(" AND Flex_Segment_Value_Details.[desc] = '" + segment_value.ToString().Replace("'", "''") + "'");

                return sqlquery.ToString();
            }
        }
        public string GET_ALL_EMPLOYEE_NOT_TERMINATED
        {
            get { return "USP_EmpAllNotTer"; }
        }

        public string GET_ALL_EMPLOYEE_AS_REQUESTOR
        {
            get { return "uspempreqtor"; }

        }

        public string FINDEMPLOYEESUNDEREMPLOYER(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT empl_code,soc_sec_num,MasterEmployee.type_code,birthdate,first_name,middle_name,last_name,address1,address2,city,state,zip,");
            sql.Append(" phone,MasterEmployee.cash_acct,MasterEmployee.department,job_code,job_title,date_hired,terminated,MasterEmployee.empl_status,MasterEmployee.pay_period,allowances,");
            sql.Append(" state_allow,marital_stat,MasterEmployee.vac_code,MasterEmployee.vac_allowed,vac_used,MasterEmployee.sick_code,MasterEmployee.sick_allowed,sick_used,last_pay,MasterEmployee.hold_pymnt,");
            sql.Append(" MasterEmployee.statax_code,MasterEmployee.loctax_code,MasterEmployee.sick_accr_code,sick_accr_ctr,sick_lapse_date,MasterEmployee.vac_accr_code,vac_accr_ctr,vac_lapse_date,");
            sql.Append(" dir_dept,dfi_dest,chk_digit,bank_acct_no,state_udf,flexdeptaccttype,last_inc_date,appoint_date,gender,");
            sql.Append(" MasterEmployee.EmployeeID,PayrollGLAccounts.keyvalue cash_acct_kv,MasterEmpType.description type_desc");
            sql.Append(" FROM MasterEmployee LEFT  outer JOIN PayrollGLAccounts ON MasterEmployee.cash_acct=PayrollGLAccounts.acct_no ");
            sql.Append("    LEFT OUTER JOIN MasterEmpType ");
            sql.Append(" ON MasterEmployee.type_code=MasterEmpType.type_code Where 1 =1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND MasterEmployee.RowID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(soc_sec_num) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[3].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.type_Code) = '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[4] != null)
                if (parameters[4].ToString() != string.Empty && Convert.ToDateTime(parameters[4].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND birthDate = '" + parameters[4].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[5] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(first_name) LIKE  '" + parameters[5].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Middle_name) LIKE  '" + parameters[6].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(last_name) LIKE  '" + parameters[7].ToString().Trim().Replace("'", "''") + "%'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Address1) = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[9] != null)
                if (parameters[9].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Address2) = '" + parameters[9].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[10] != null)
                if (parameters[10].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(City) = '" + parameters[10].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[11] != null)
                if (parameters[11].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(State) = '" + parameters[11].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[12] != null)
                if (parameters[12].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Zip) = '" + parameters[12].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[13] != null)
                if (parameters[13].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Phone) = '" + parameters[13].ToString().Trim().Replace("'", "''") + "'");
            if (Convert.ToInt32(parameters[14]) > 0)
                sql.Append(" AND MasterEmployee.Cash_Acct = " + parameters[14].ToString());
            if (parameters[15] != null)
                if (parameters[15].ToString() != string.Empty && Convert.ToDateTime(parameters[15].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Terminated = '" + parameters[15].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[16] != null)
                if (parameters[16].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.hold_pymnt) = '" + parameters[16].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[17] != null)
                if (parameters[17].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(FlexDeptAcctType) = '" + parameters[17].ToString().Replace("'", "''") + "'");
            if (parameters[18] != null)
                if (parameters[18].ToString() != string.Empty && Convert.ToDateTime(parameters[18].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Last_Inc_Date = '" + parameters[18].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[19] != null)
                if (parameters[19].ToString() != string.Empty && Convert.ToDateTime(parameters[19].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Appoint_Date = '" + parameters[19].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[20] != null)
                if (parameters[20].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Gender) = '" + parameters[20].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[21] != null)
                if (parameters[21].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.Empl_Status) = '" + parameters[21].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[22] != null)
                if (parameters[22].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Job_Code) = '" + parameters[22].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[23] != null)
                if (parameters[23].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(Job_Title) = '" + parameters[23].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[24] != null)
                if (parameters[24].ToString() != string.Empty)
                    sql.Append(" AND RTRIM(MasterEmployee.Pay_Period) = '" + parameters[24].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[25] != null)
                if (parameters[25].ToString() != string.Empty && Convert.ToDateTime(parameters[25].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND last_pay = '" + parameters[25].ToString().Trim().Replace("'", "''") + "'");



            return sql.ToString();
        }
        public string FIND_EMP(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT empl_code,soc_sec_num,MasterEmployee.type_code,first_name,middle_name,last_name,address1,address2,city,state,zip,");
            sql.Append(" phone,MasterEmployee.cash_acct,hold_pymnt,flexdeptaccttype,gender,");
            sql.Append(" PayrollGLAccounts.keyvalue ,MasterEmployee.type_Code");
            sql.Append(" FROM MasterEmployee  LEFT outer JOIN  PayrollGLAccounts ON ");
            sql.Append("  MasterEmployee.cash_acct=PayrollGLAccounts.acct_no  Where 1=1 ");


            if (parameters[0] != null && parameters[0].ToString().Trim().Length > 0)
                sql.Append(" AND MasterEmployee.type_Code LIKE '" + parameters[0].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[1] != null && parameters[1].ToString().Trim().Length > 0)
            {
                sql.Append(" and (MasterEmployee.terminated is null ");
                sql.Append(" or MasterEmployee.terminated >='" + parameters[1].ToString() + "')");
            }
            if (parameters[2] != null)
            {
                if (parameters[2] != string.Empty && parameters[2].ToString().Trim().Length > 0)
                {
                    sql.Append(" and MasterEmployee.first_name LIKE '" + parameters[2].ToString() + "%'");
                }
            }
            if (parameters[3] != null)
            {
                if (parameters[3] != string.Empty && parameters[3].ToString().Trim().Length > 0)
                {
                    sql.Append(" and MasterEmployee.last_name LIKE '" + parameters[3].ToString() + "%'");
                }

            }

            return sql.ToString();
        }



        #endregion Stored-Procedures
    }
}
