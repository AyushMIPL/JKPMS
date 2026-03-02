using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using JKPS.DL;
using JKPS.COMMON;

namespace JKPS.BLL
{
    public class BLLPayrollFunctions
    {
        /// <summary>
        /// rounded value, depending on value type
        /// returns null if value is null
        /// </summary>
        /// <param name="_Type">
        /// types:
        /// a) decimal for normal money amounts.  currently two places
        /// b) decimal for more precise money amounts.  currently three places
        /// c) decimal for physical quantities.  currently three places
        /// d) decimal for hours.  currently two places
        /// e) decimal for rates.  currently three places</param>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public string Al_Round(string _Type, decimal _Value)
        {
            switch (_Type)
            {
                case null: return null;
                case "a":
                    return string.Format("{0:0.00}", _Value);
                case "b":
                    return string.Format("{0:0.000}", _Value);
                case "c":
                    return string.Format("{0:0.000}", _Value);
                case "d":
                    return string.Format("{0:0.00}", _Value);
                case "e":
                    return string.Format("{0:0.000}", _Value);
                default:
                    return "0";
            }
        }
        /// <summary>
        /// rounded value, depending on value type
        /// returns null if value is null
        /// </summary>
        /// <param name="_Type">
        /// types:
        /// a) decimal for normal money amounts.  currently two places
        /// b) decimal for more precise money amounts.  currently three places
        /// c) decimal for physical quantities.  currently three places
        /// d) decimal for hours.  currently two places
        /// e) decimal for rates.  currently three places</param>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public string Al_Round(string _Type, decimal? _Value)
        {
            if (_Value.HasValue)
            {
                switch (_Type)
                {
                    case null: return null;
                    case "a":
                        return string.Format("{0:0.00}", _Value);
                    case "b":
                        return string.Format("{0:0.000}", _Value);
                    case "c":
                        return string.Format("{0:0.000}", _Value);
                    case "d":
                        return string.Format("{0:0.00}", _Value);
                    case "e":
                        return string.Format("{0:0.000}", _Value);
                    default:
                        return "0";
                }
            }
            else
                return null;
            return null;
        }
        
        /// <summary>
        /// rounded value, depending on value type
        /// returns null if value is null
        /// </summary>
        /// <param name="_Type">
        /// types:
        /// a) decimal for normal money amounts.  currently two places
        /// b) decimal for more precise money amounts.  currently three places
        /// c) decimal for physical quantities.  currently three places
        /// d) decimal for hours.  currently two places
        /// e) decimal for rates.  currently three places</param>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public string Al_Round(string _Type, float _Value)
        {
            switch (_Type)
            {
                case null: return null;
                case "a":
                    return string.Format("{0:0.00}", _Value);
                case "b":
                    return string.Format("{0:0.000}", _Value);
                case "c":
                    return string.Format("{0:0.000}", _Value);
                case "d":
                    return string.Format("{0:0.00}", _Value);
                case "e":
                    return string.Format("{0:0.000}", _Value);
                default:
                    return "0";
            }
        }

        /// <summary>
        /// This function is passed a income code, and from
        ///  that, it returns that income description.
        /// </summary>
        /// <param name="_IncomeCode"></param>
        /// <returns></returns>
        public string GetIncomeCodeDescription(string _IncomeCode)
        {
            string _IncomeCodeDesc = "NOT FOUND";
            try
            {
                DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
                objDVOUpdateIncCode.inc_code = _IncomeCode;
                List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);
                if (listDVOUpdateIncCode.Count > 0)
                    _IncomeCodeDesc = listDVOUpdateIncCode[0].description;
                objDVOUpdateIncCode = null;
                listDVOUpdateIncCode = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _IncomeCodeDesc;
        }

        /// <summary>
        /// This function checks to make sure the income code is in
        /// the reference table MasterIncCodes and returns true if found, otherwise false
        /// </summary>
        /// <param name="_IncomeCode"></param>
        /// <returns></returns>
        public bool IncomeCodeExist(string _IncomeCode)
        {
            bool _IncomeCodeExist = false;
            try
            {
                DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
                objDVOUpdateIncCode.inc_code = _IncomeCode;
                List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);
                if (listDVOUpdateIncCode.Count > 0)
                    _IncomeCodeExist = true;
                objDVOUpdateIncCode = null;
                listDVOUpdateIncCode = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _IncomeCodeExist;
        }

        /// <summary>
        /// This function checks to make sure the department number is
        /// in the reference table stxinfor and returns true if found, otherwise false.
        /// </summary>
        /// <param name="_DepartmentNumber">Department-Number or SrcKey</param>
        /// <returns></returns>
        public bool DepartmentNumberExist(string _DepartmentNumber)
        {
            bool _DepartmentNumberExist = false;
            try
            {
                DVOFlxview objDVOFlxview = new DVOFlxview();
                objDVOFlxview.src_key = _DepartmentNumber;
                List<DVOFlxview> listDVOFlxview = BLLFlxsegView.GetSegViewList(ref objDVOFlxview);
                if (listDVOFlxview.Count > 0)
                    _DepartmentNumberExist = true;
                objDVOFlxview = null;
                listDVOFlxview = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _DepartmentNumberExist;
        }

        /// <summary>
        /// This function checks to make sure the deduction code is in
        /// the reference table MasterDedcodes and returns true if found, otherwise false.
        /// </summary>
        /// <param name="_DeductionCode"></param>
        /// <returns></returns>
        public bool DeductionCodeExist(string _DeductionCode)
        {
            bool _DeductionCodeExist = false;
            try
            {
                DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
                objDVOPRDeductionCodesMasterDedcodes.ded_code = _DeductionCode;
                List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = BLLPRDeductionCodesMasterDedcodes.GetData(ref objDVOPRDeductionCodesMasterDedcodes);
                if (listDVOPRDeductionCodesMasterDedcodes.Count > 0)
                    _DeductionCodeExist = true;
                objDVOPRDeductionCodesMasterDedcodes = null;
                listDVOPRDeductionCodesMasterDedcodes = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _DeductionCodeExist;
        }

        /// <summary>
        /// This function is passed a deduction code, and from
        /// that, it returns that deduction description
        /// </summary>
        /// <param name="_IncomeCode"></param>
        /// <returns></returns>
        public string GetDeductionCodeDescription(string _DeductionCode)
        {
            string _DeductionCodeDesc = "NOT FOUND";
            try
            {
                DVOPRDeductionCodesMasterDedcodes objDVOPRDeductionCodesMasterDedcodes = new DVOPRDeductionCodesMasterDedcodes();
                objDVOPRDeductionCodesMasterDedcodes.ded_code = _DeductionCode;
                List<DVOPRDeductionCodesMasterDedcodes> listDVOPRDeductionCodesMasterDedcodes = BLLPRDeductionCodesMasterDedcodes.GetData(ref objDVOPRDeductionCodesMasterDedcodes);
                if (listDVOPRDeductionCodesMasterDedcodes.Count > 0)
                    _DeductionCodeDesc = listDVOPRDeductionCodesMasterDedcodes[0].description;
                objDVOPRDeductionCodesMasterDedcodes = null;
                listDVOPRDeductionCodesMasterDedcodes = null;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _DeductionCodeDesc;
        }
        /// <summary>
        /// this function checks if a deduction should be taken and returns
        /// true if it should be taken, false otherwise. The deduction is
        /// taken the first time the payroll is run in the period given by the
        /// deduction frequency. For example, the first time payroll is
        /// generated for each month all monthly deductions will be taken
        /// </summary>
        /// <param name="_Frequency"></param>
        /// <param name="Old_Date"></param>
        /// <returns></returns>
        public bool Pay_Frequency(string _Frequency, DateTime _currentdate, DateTime Old_Date)
        {
            if (Old_Date == null)
            {
                if (_Frequency == "N")
                    return false;
            }
           // return true;// deduction has never been taken so take it

            // get the month and year the deduction was last taken
            int old_month = Old_Date.Month;
            int old_year = Old_Date.Year;

            DateTime Cur_Date = _currentdate;
            int cur_month = Cur_Date.Month;
            int cur_year = Cur_Date.Year;

            //process the apply code
            switch (_Frequency)
            {
                case "N": return false;// never
                case "M": return (cur_month != old_month) || (cur_year != old_year);// monthly
                case "Q": return (((cur_month - 1) / 3) != ((old_month - 1) / 3)) || (cur_year != old_year);// quarterly
                case "Y": return (cur_year != old_year);// yearly
                default: return true;// always
            }
            return false;
        }

        /// <summary>
        /// <summary>
        /// this function checks if a deduction should be taken and returns
        /// true if it should be taken, false otherwise. The deduction is
        /// taken the first time the payroll is run in the period given by the
        /// deduction frequency. For example, the first time payroll is
        /// generated for each month all monthly deductions will be taken
        /// </summary>
        /// <param name="_Frequency"></param>
        /// <param name="Old_Date"></param>
        /// <returns></returns>
        public bool Payroll_Frequency(string _Frequency, DateTime Old_Date)
        {
            if (Old_Date == null)
            {
                if (_Frequency == "N")
                    return false;
            }
            return true;// deduction has never been taken so take it

            // get the month and year the deduction was last taken
            int old_month = Old_Date.Month;
            int old_year = Old_Date.Year;

            DateTime Cur_Date = DVOApplicationUserInfo.CurrentDate;
            int cur_month = Cur_Date.Month;
            int cur_year = Cur_Date.Year;

            //process the apply code
            switch (_Frequency)
            {
                case "N": return false;// never
                case "M": return (cur_month != old_month);// monthly
                case "Q": return (((cur_month - 1) / 3) != ((old_month - 1) / 3));// quarterly
                case "Y": return (cur_year != old_year);// yearly
                default: return true;// always
            }
            return false;
        }

        /// <summary>
        /// This function returns an accounting control record, stycntrc
        /// </summary>
        /// <returns></returns>
        public DVOUpdatePayDefaults GetAccountingControlRecord()
        {
            DVOUpdatePayDefaults objDVOUpdatePayDefaults = new DVOUpdatePayDefaults();
            List<DVOUpdatePayDefaults> listDVOUpdatePayDefaults = BLLUpdPayDefault.GetAllPayrollDefaults();
            if (listDVOUpdatePayDefaults.Count > 0)
                objDVOUpdatePayDefaults = listDVOUpdatePayDefaults[0];

            return objDVOUpdatePayDefaults;
        }
        public void flxMix(string p_acct_type, string p_keyvalue, string p_dept_acct_type, string p_flex_dept, out int ret_acct_no, out string ret_acct_type, out string ret_keyvalue, out int ret_acct_type_id)
        {
            ret_acct_no = 0;
            ret_acct_type = string.Empty;
            ret_keyvalue = string.Empty;
            ret_acct_type_id = 0;

            if (p_acct_type != p_dept_acct_type)
                return;

            for (int i = 0; i < p_keyvalue.Length; i++)
            {
                ret_keyvalue = ret_keyvalue.Insert(i, p_keyvalue[i].ToString());
                if (p_keyvalue[i] == '#')
                    if (p_flex_dept.Length > i)
                    {
                        ret_keyvalue = ret_keyvalue.Insert(i, p_flex_dept[i].ToString());
                        if (ret_keyvalue.Length > (i + 1))
                            ret_keyvalue = ret_keyvalue.Remove(i + 1);
                    }
            }
            //ret_keyvalue = p_keyvalue;
            ret_acct_type = p_acct_type;
            ret_acct_no = GetAccountNumber(ret_acct_type, ret_keyvalue);
        }

      
        /// <summary>
        /// # This function combines loc_flex_dept (typically the employee's flex
        /// department styemplr.flexdept) with loc_keyvalue (typically the
        /// generic keyvalue indicating an income or obligation expense account)
        /// to produce a keyvalue with no generic segments.  To do this, the
        /// two passed account types must match;  otherwise empty values are returned.
        /// If all goes according to plan, the integer account number from
        /// PayrollGLAccounts is returned for the assembled keyvalue, 
        /// along with the assembled keyvalue and the account type
        /// </summary>
        /// <param name="p_acct_type">the income/obligation expense account type</param>
        /// <param name="p_keyvalue">the income/obligation expense generic keyvalue;  
        /// built up from entries in Flex_Segment_Reference</param>
        /// <param name="p_dept_acct_type">the flex department account type from the employee master record</param>
        /// <param name="p_flex_dept">the flex department displayed on the employee master record;  
        /// built up from entries in Flex_Segment_Reference</param>
        /// <param name="ret_acct_no"></param>
        /// <param name="ret_acct_type"></param>
        /// <param name="ret_keyvalue"></param>
        public void flxMix(string p_acct_type, string p_keyvalue, string p_dept_acct_type, string p_flex_dept, out int ret_acct_no, out string ret_acct_type, out string ret_keyvalue)
        {
            ret_acct_no = 0; 
            ret_acct_type = string.Empty; 
            ret_keyvalue = string.Empty;

            if (p_acct_type != p_dept_acct_type)
                return;

            for (int i = 0; i < p_keyvalue.Length; i++)
            {
                ret_keyvalue = ret_keyvalue.Insert(i, p_keyvalue[i].ToString());
                if (p_keyvalue[i] == '#')
                    if (p_flex_dept.Length > i)
                    {
                        ret_keyvalue = ret_keyvalue.Insert(i, p_flex_dept[i].ToString());
                        if (ret_keyvalue.Length > (i + 1))
                            ret_keyvalue = ret_keyvalue.Remove(i + 1);
                    }
            }
            //ret_keyvalue = p_keyvalue;
            ret_acct_type = p_acct_type;
            ret_acct_no = GetAccountNumber(ret_acct_type, ret_keyvalue);
        }

        /// <summary>
        /// To Get AccountNumber for entered AccountType and keyvalue
        /// </summary>
        /// <param name="p_accountType"></param>
        /// <param name="p_Keyvalue"></param>
        /// <returns></returns>
      private int GetAccountNumber(string p_accountType, string p_Keyvalue)
        {
         int _AccountNumber = 0;
        //    try
        //    {
        //        DVOGeneralLedger objDVOGeneralLedger = new DVOGeneralLedger();
        //        objDVOGeneralLedger.acct_type = p_accountType;
        //        objDVOGeneralLedger.keyvalue = p_Keyvalue;

        //        List<DVOGeneralLedger> listDVOGeneralLedger = BLLGeneralLedger.GetLedgerAccounts(objDVOGeneralLedger);
        //        if (listDVOGeneralLedger.Count > 0)
        //            _AccountNumber = listDVOGeneralLedger[0].acct_no;
        //        objDVOGeneralLedger = null;
        //        listDVOGeneralLedger = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManagement.ExceptionManager.Publish(ex);
        //    }
            return _AccountNumber;
        }
        public void flxMixInc(string p_empl_code, string p_inc_code, out int ret_acct_no, out string ret_acct_type, out string ret_keyvalue, out int ret_acct_type_id)
        {
            // used to grab the acct_types for the employee and the income code
            string loc_emp_accttype = string.Empty; //char(6)  LIKE styemplr.flexdeptaccttype,
            string loc_inc_accttype = string.Empty; //char(6)  LIKE styinccr.dfltaccounttype

            //# the return values
            ret_acct_no = 0;  // LIKE stxchrtr.acct_no,
            ret_acct_type = string.Empty;  //char(6) LIKE stxchrtr.acct_type,
            ret_keyvalue = string.Empty;   //char(100)  LIKE stxchrtr.keyvalue
            ret_acct_type_id = 0;//int

            // grab the account types
            //******** Added by Bharat Dhall[12-21_2009] ************
            if (p_empl_code.Trim().Length <= 0)
            {
                //if there is no employee-code then default flex-dept-account-type will be 'RECEXP'
                loc_emp_accttype = "RECEXP";
            }
            else
            {
                //********************************************************  
                DVOMasterEmployee objDVOPREmployeeStyemplr = new DVOMasterEmployee();
                objDVOPREmployeeStyemplr.EmplCode = p_empl_code;
                List<DVOMasterEmployee> listDVOPREmployeeStyemplr = BLLMasterEmployee.GetData(ref objDVOPREmployeeStyemplr);
                if (listDVOPREmployeeStyemplr.Count > 0)
                    loc_emp_accttype = listDVOPREmployeeStyemplr[0].FlexDeptAcctType;
                objDVOPREmployeeStyemplr = null;
                listDVOPREmployeeStyemplr = null;
            }

            DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
            objDVOUpdateIncCode.inc_code = p_inc_code;
            List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);
            if (listDVOUpdateIncCode.Count > 0)
                loc_inc_accttype = listDVOUpdateIncCode[0].dfltaccounttype;
            objDVOUpdateIncCode = null;
            listDVOUpdateIncCode = null;

            DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
            objDVOFlexSegCommon.EntityType = "styinccr";
            objDVOFlexSegCommon.Code = p_inc_code;
            objDVOFlexSegCommon.AccountType = loc_inc_accttype;
            string _keyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);

            objDVOFlexSegCommon.EntityType = "styemplr";
            objDVOFlexSegCommon.Code = p_empl_code;
            objDVOFlexSegCommon.AccountType = loc_emp_accttype;
            string _flexdepartment = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);

            //call flxMix function
            flxMix(loc_inc_accttype, _keyvalue, loc_emp_accttype, _flexdepartment,
                    out ret_acct_no, out ret_acct_type, out ret_keyvalue, out ret_acct_type_id);
        }

        /// <summary>
        /// This function combines the employee's flex department with the
        /// generic expense account for the income code.  This function is a
        /// short-hand for flxMix(...) and has the same return values:  the
        /// acct_no (if it exists), the account type and the keyvalue of resultant combination.
        /// </summary>
        /// <param name="p_empl_code">Employee Code</param>
        /// <param name="p_inc_code">Income Code</param>
        /// <param name="ret_acct_no">return Account Number</param>
        /// <param name="ret_acct_type">return Account Type</param>
        /// <param name="ret_keyvalue">return Keyvalue</param>
        /// 
        public void flxMixInc(string p_empl_code, string p_inc_code, out int ret_acct_no, out string ret_acct_type, out string ret_keyvalue)
        {
            // used to grab the acct_types for the employee and the income code
            string loc_emp_accttype = string.Empty; //char(6)  LIKE styemplr.flexdeptaccttype,
            string loc_inc_accttype = string.Empty; //char(6)  LIKE MasterIncCodes.dfltaccounttype

            //# the return values
            ret_acct_no = 0;  // LIKE PayrollGLAccounts.acct_no,
            ret_acct_type = string.Empty;  //char(6) LIKE PayrollGLAccounts.acct_type,
            ret_keyvalue = string.Empty;   //char(100)  LIKE PayrollGLAccounts.keyvalue

            // grab the account types
            //******** Added by Bharat Dhall[12-21_2009] ************
            if (p_empl_code.Trim().Length <= 0)
            {
                //if there is no employee-code then default flex-dept-account-type will be 'RECEXP'
                loc_emp_accttype = "RECEXP";
            }
            else
            {
            //********************************************************  
                DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();
                objDVOMasterEmployee.EmplCode = p_empl_code;
                List<DVOMasterEmployee> listDVOMasterEmployee = BLLMasterEmployee.GetData(ref objDVOMasterEmployee);
                if (listDVOMasterEmployee.Count > 0)
                    loc_emp_accttype = listDVOMasterEmployee[0].FlexDeptAcctType;
                objDVOMasterEmployee = null;
                listDVOMasterEmployee = null;
            }

            DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
            objDVOUpdateIncCode.inc_code = p_inc_code;
            List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);
            if (listDVOUpdateIncCode.Count > 0)
                loc_inc_accttype = listDVOUpdateIncCode[0].dfltaccounttype;
            objDVOUpdateIncCode = null;
            listDVOUpdateIncCode = null;

            DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
            objDVOFlexSegCommon.EntityType = "MasterIncCodes";
            objDVOFlexSegCommon.Code = p_inc_code;
            objDVOFlexSegCommon.AccountType = loc_inc_accttype;
            string _keyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);

            objDVOFlexSegCommon.EntityType = "styemplr";
            objDVOFlexSegCommon.Code = p_empl_code;
            objDVOFlexSegCommon.AccountType = loc_emp_accttype;
            string _flexdepartment = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);

            //call flxMix function
            flxMix(loc_inc_accttype, _keyvalue, loc_emp_accttype, _flexdepartment,
                    out ret_acct_no, out ret_acct_type, out ret_keyvalue);
        }

        /// <summary>
        /// This function combines the employee's flex department with the
        /// generic expense account for the Obligation code.  This function is a
        /// short-hand for flxMix(...) and has the same return values:  the
        /// acct_no (if it exists), the account type and the keyvalue of resultant combination.
        /// </summary>
        /// <param name="p_empl_code">Employee Code</param>
        /// <param name="p_obl_code">Obligation Code</param>
        /// <param name="ret_acct_no">return Account Number</param>
        /// <param name="ret_acct_type">return Account Type</param>
        /// <param name="ret_keyvalue">return Keyvalue</param>
        public void flxMixObl(string p_empl_code, string p_obl_code, out int ret_acct_no, out string ret_acct_type, out string ret_keyvalue)
        {
            // used to grab the acct_types for the employee and the income code
            string loc_emp_accttype = string.Empty; //char(6)  LIKE styemplr.flexdeptaccttype,
            string loc_obl_accttype = string.Empty; //char(6)  LIKE MasterIncCodes.dfltaccounttype

            //# the return values
            ret_acct_no = 0;  // LIKE PayrollGLAccounts.acct_no,
            ret_acct_type = string.Empty;  //char(6) LIKE PayrollGLAccounts.acct_type,
            ret_keyvalue = string.Empty;   //char(100)  LIKE PayrollGLAccounts.keyvalue

            if (p_empl_code.Trim().Length <= 0)
            {
                loc_emp_accttype = "RECEXP";
            }
            else
            {
                // grab the account types
                DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();
                objDVOMasterEmployee.EmplCode = p_empl_code;
                List<DVOMasterEmployee> listDVOMasterEmployee = BLLMasterEmployee.GetData(ref objDVOMasterEmployee);
                if (listDVOMasterEmployee.Count > 0)
                    loc_emp_accttype = listDVOMasterEmployee[0].FlexDeptAcctType;
                objDVOMasterEmployee = null;
                listDVOMasterEmployee = null;
            }

            DVOMasterOblCodes objDVOMasterOblCodes = new DVOMasterOblCodes();
            objDVOMasterOblCodes.obl_code = p_obl_code;
            List<DVOMasterOblCodes> listDVOMasterOblCodes = BLLPRObligationCodesMasterOblCodes.GetData(ref objDVOMasterOblCodes);
            if (listDVOMasterOblCodes.Count > 0)
                loc_obl_accttype = listDVOMasterOblCodes[0].dfltaccounttype;
            objDVOMasterOblCodes = null;
            listDVOMasterOblCodes = null;

            DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
            objDVOFlexSegCommon.EntityType = "MasterOblCodes";
            objDVOFlexSegCommon.Code = p_obl_code;
            objDVOFlexSegCommon.AccountType = loc_obl_accttype;
            string _keyvalue = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);

            objDVOFlexSegCommon.EntityType = "styemplr";
            objDVOFlexSegCommon.Code = p_empl_code;
            objDVOFlexSegCommon.AccountType = loc_emp_accttype;
            string _flexdepartment = BLLFlexSegCommon.Flexseg_Load(ref objDVOFlexSegCommon);

            //call flxMix function
            flxMix(loc_obl_accttype, _keyvalue, loc_emp_accttype, _flexdepartment,
                    out ret_acct_no, out ret_acct_type, out ret_keyvalue);
        }
        
        public bool pay_time(DateTime last_pay, DateTime pay_date, string payperiod,bool Bonus_Check)
        {
            //written by     Rohit Wadhwa 
            //written Date   22/12/2008
            // AIM :This function determines if a minimum amount of time has elapsed
            //since the last pay date relative to the length of the pay period. If so
            //it returns true otherwise false.

            //modified Date 

            Int32 pay_lapse;
            bool paytime=false;
            switch (payperiod)
            {
                case "W" :
                    pay_lapse = 5;
                    break;
                case "B":
                    pay_lapse = 11;
                    break;
                case "S":
                    pay_lapse = 11;
                    break;
                case "M":
                    pay_lapse = 25;
                    break;
                case "Q":
                    pay_lapse = 80;
                    break;
                case "H":
                    pay_lapse = 160;
                    break;
                case "Y":
                    pay_lapse = 330;
                    break;
                case "D":
                    pay_lapse = 1;
                    break;
                default:
                    pay_lapse = 11;
                    break;

            }
            if (!Bonus_Check)
            {
                if (last_pay.AddDays(pay_lapse) > pay_date)
                {
                    paytime = false;
                }
                else
                {
                    paytime = true;
                }
            }
            else
            {
                paytime = true;
            }
            return paytime;
            
        }
        public float ded_fedgrs(int n, float tax_wages, string pay_period)
        {
            //written by     Rohit Wadhwa 
            //written Date   22/12/2008
            // AIM :Calculates federal tax liability for use in calculating other deductions
            //(typically state income taxes).
            //Returns the amount of federal tax given annualized gross income.
            //Have to work on it to get the exact working .  
            //Even Parameters need to be modified with the list instead of " n "


            bool Check_Year=false;
            bool year_is_current = true;
            float allow_amt;
            float t_base_amt;
            float t_over_amt;
            float t_rate;
            float t_period;
            float t_maritual;
            float t_total;
            allow_amt = 0.0F;
            return allow_amt;


        }
        public float ded_taxcalc(int n, float tax_wages, string pay_period)
        {
            //written by     Rohit Wadhwa 
            //written Date   22/12/2008
            // AIM :.
            //
            //Have to work on it to get the exact working .  
            //Even Parameters need to be modified with the list instead of " n "
            return tax_wages;
        }
        public float state_calc(int n, float tax_wages, string pay_period)
        {
            /*
             *  //written by     Rohit Wadhwa 
            //written Date   22/12/2008
            // AIM :.
            //
                #   - this function is reserved for any custom modifications you
                #   may require in order to handle a state's tax liability.  It
                #   contains the basic logic used to calculate deductions in
                #   the ded_recalc() case statement.  Add any custom
                #   modification you require here rather than complicating the
                #   standard deduction calculation logic in ded_recalc().
                #
                #   This function serves the needs of state tax calculation
                #   regardless of the deduction type defined for the
                #   code.  A number of functions have been written to make the job
                #   of state-by-state customization easier and more modular.  The
                #   functions and their objectives follow:
                #
                #    - py_annual(wage, pay_period)  This function annualizes
                #   the wage level passed to it given the pay period specified.
                #   That is, many states require that the wage (gross wages,
                #   taxable wages, etc.) be annualized as a preliminary step
                #   to calculation.  All we need to do is call py_annual(),
                #   passing it the wage amount and pay period.  Example:
                #       call py_annual(p_ypayre.inc_taxable, ctrl_ref.pay_period)
                #         returning annual_wage
                #
                #    - py_period(amount, pay_period)  Alter ego of py_annual.
                #   Call this function when you need to divide an amount
                #   by the number of pay periods in a year.  This function turns
                #   an annual amount of state tax liability into a figure
                #   applicable to the current pay period.  Example:
                #       call py_period(annual_tax, ctrl_ref.pay_period)
                #         returning gross_tax
                #
                - ded_fedgrs(n, wage_amount, pay_period)  Used to
                #   calculate and return the federal tax liability.  Some
                #   states calculate their tax as a function of the employee's
                #   federal tax liability.  This function consults the federal tax
                #   table, somewhat like the existing ded_taxcalc() function.  Example:
                #        call ded_fedgrs(n, annual_wage, "A") returning fed_amount
                #
                #   A case statement can be setup within this function to
                #   handle the idiosyncracies of each state you must figure for.
                #   A suggested approach is to build a table of state income
                #   tax codes, validate the state tax code on entry in styemplr,
                #   and setup a case statement here to handle each state's
                #   calculation method appropriately.
                #
                #   The i_emplee (Update Employee Information) program will be
                #   modified to include columns for tax credit amount and
                #   exemptions amounts to account for those calculations that
                #   are not solved through the traditional tax table approach.
                #   This is a simple, table-driven way to handle many complex
                #   approaches used by state taxing authorities.  This allows you
                #   to enter in the amount of the credit and/or exemption the
                #   employee is entitled to for state purposes, and we can do
                #   a simple lookup here to calculate taxes properly.
                #
                #   Last note--the ded_taxcalc function MUST be changed to accept
                #   a pay period argument in addition to the (n, amount) arguments.
                #

            */
            return tax_wages;
            //Have to work on it to get the exact working .  
            //Even Parameters need to be modified with the list instead of " n "
        }
        public bool tbl_check(string ded_code, DateTime Paydate)
        {
            //#   - this function is called to verify that if a table exists, that
            //#   - the Tax Year matches the payroll date year.
            bool year_is_current = false;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            // Code to check the table values will come here ...........
             object[] parameters = new object[2];
            parameters[0]=ded_code.Trim();
            parameters[1]=Paydate;
            DataSet Result = objDALBaseClass.GetData(ref parameters, typeof(DVOCommonEntities),(new DVOCommonEntities()).PAYROLL_CHECKTAXTABLE);
            //if (Result == 0)
            //{
            //    year_is_current = true;
            //}
            //else
            //{
            //    year_is_current = false;
            //}
            return year_is_current;
        }



        //******************************************************************************************
        // Aim                         Developed By                Development Date                *
        //******************************************************************************************
        //To develop public method       Rajeev                       05/01/2009

        public static bool chk_post(ref object objTransection, ref DVOPostGLGlobal objNewDVOPostGLGlobal, ref DVOPayrollProcess_PayEmployee newGlobalObjDVOPayrollProcess_PayEmployee, ref DVOMasterEmployee newGlobalObjDVOMasterEmployee, DVOUpdatePayDefaults objDVOUpdatePayDefaultsstycntrc, DataTable objDataTable, ref DataRow dr, out int status, string CHECK_POST, int new_doc_no, out int py_status, out string py_description, out string py_installed, ref decimal py_c_accum, ref decimal py_i_accum, ref decimal py_d_accum, ref decimal py_ox_accum, ref decimal py_ol_accum, ref DataTable GLSumTable)
        {
            // This function calls py_post and gl_post to post the check to
            // stytranr, styactvd, stgtranr, and stgactvd.
            try
            {
                //Initializing out parameter
                py_status = 0;
                py_description = "";
                py_installed = "";

                status = 0;

                string db_cr = string.Empty;
                // make sure a check number has been assigned and printed

                if (dr["check_no"] != DBNull.Value)
                {
                    if (dr["check_no"].ToString().Trim() == string.Empty)
                    {
                        dr["check_no"] = 0;
                    }
                }
                else
                    dr["check_no"] = 0;

                if ((dr["print_check"].ToString().Trim() == "N" && Convert.ToInt32(dr["check_no"]) != 0) && dr["deposit"].ToString().Trim() == "N")
                { }
                else
                {
                    //status = 1;
                    //dr["ok_to_post"] = false;
                    //dr["err_3"] = "**** Warning: No check number and/or document has not been printed/deposited.";
                    if (CHECK_POST == "POST")
                    {
                        dr["ok_to_post"] = false;
                        py_description = "Error: No check number and/or document has not been printed/deposited.";
                        return false;
                    }
                }



                //make sure values are valid
                if (dr["pay_date"] == DBNull.Value)
                    dr["pay_date"] = dr["doc_date"];
                if (dr["eop_date"] == DBNull.Value)
                    dr["eop_date"] = dr["pay_date"];
                if (dr["cash_acct_no"] == DBNull.Value)
                {
                    dr["cash_acct_no"] = dr["emp_cash_acct"]; //still did not get this column
                    if (dr["cash_acct_no"] == DBNull.Value)
                    {
                        dr["cash_acct_no"] = dr["py_cash_acct"];
                        if (dr["cash_acct_no"] == DBNull.Value)
                        {
                            //status = 1;
                            dr["ok_to_post"] = false;
                            dr["err_2"] = "**** Error: No cash account number.";


                        }
                    }
                }
                if (dr["department"] == DBNull.Value)
                    dr["department"] = dr["emp_department"]; //still did not get this column               

                if (!py_post(ref objTransection, objDVOUpdatePayDefaultsstycntrc, CHECK_POST, "PY", Convert.ToInt32(dr["doc_no"]), Convert.ToInt32(dr["post_seq"]), DVOApplicationUserInfo.CurrentDate, Convert.ToDateTime(dr["doc_date"]), Convert.ToDateTime(dr["pay_date"]), dr["empl_code"].ToString(), "PAYROLL ENTRY", dr["check_no"].ToString(), "CHECK", "A", Convert.ToDecimal(dr["cash_amount"]), Convert.ToInt32(dr["cash_acct_no"]), dr["department"].ToString(), Convert.ToDateTime(dr["eop_date"]), "0", Convert.ToDecimal(dr["total_hours"]), "0", "0", out py_status, out py_description, out  py_installed, ref  py_c_accum, ref  py_i_accum, ref  py_d_accum, ref  py_ox_accum, ref  py_ol_accum))
                {
                    if (py_status == 1)
                    {
                        //status = 1;
                        dr["ok_to_post"] = false;
                        dr["err_1"] = "**** Error: " + py_description;
                        //dr["Problem1"] = "ON";
                        return false;
                    }
                    else if (py_status == 4) //document number out of sequence
                    {
                        //status = 1;
                        dr["err_5"] = "**** Error: " + py_description;

                        return false;
                    }
                    else
                    {
                        //status = 1;
                        dr["ok_to_post"] = false;
                        dr["err_1"] = "**** Error: " + py_description;

                        return false;
                    }
                }
                //if the amount is negative, reverse the sense of the debit/credit
                //and reverse the amount
                db_cr = "C";
                decimal amount = Convert.ToDecimal(dr["cash_amount"]);
                if (amount < 0)
                {

                    db_cr = "D";
                    amount = amount * (-1);
                }
                // post check to general ledger if stycntrc.post_gl field is set to Y

                if (objDVOUpdatePayDefaultsstycntrc.post_gl == "Y")
                {
                    //insert the required information into the temporary table to be printed
                    // after each payroll department
                    //IF exceptions
                    //   THEN
                    //      EXECUTE insDeptDist USING rpt.flexdept, rpt.cash_acct_no, amount, db_cr
                    //   END IF
                    // CHECK_POST, "PY", Convert.ToInt32(dr["txt_doc_no"].ToString()), Convert.ToInt32(dr["post_seq"].ToString()), DateTime.Now, Convert.ToDateTime(dr["doc_date"].ToString()), Convert.ToDateTime(dr["pay_date"].ToString()), dr["empl_code"].ToString(), "PAYROLL ENTRY", dr["check_no"].ToString(), "CHECK", "A", Convert.ToDecimal(dr["cash_amount"].ToString()), Convert.ToInt32(dr["cash_acct_no"].ToString()), dr["department"].ToString(), Convert.ToDateTime(dr["eop_date"].ToString()), "", dr["total_hours"].ToString(), "", "", out py_status, out py_description))
                    //string post_or_check, string orig_journal, int doc_no, int post_no, DateTime post_date, DateTime doc_date, DateTime pay_date, string ref_code, string doc_desc, string check_no, string act_code, string act_type, decimal amount, int acct_no, string dept_code, DateTime eop_date, string number, decimal hours, string rate, string line_no, out int py_status, out string py_description

                    //        if not gl_post(check_post, "PY", new_doc_no, post_no, today,
                    //rpt.doc_date, rpt.empl_code, "PAYROLL ENTRY", rpt.check_no,
                    //rpt.cash_acct_no, rpt.department, amount, db_cr)                 
                    DVOPostGL objDVOPostGL = new DVOPostGL();
                    objDVOPostGL.post_or_check = CHECK_POST;
                    objDVOPostGL.orig_journal = "PY";
                    objDVOPostGL.doc_no = Convert.ToInt32(dr["doc_no"]);
                    objNewDVOPostGLGlobal.next_doc_no = Convert.ToInt32(dr["txt_doc_no"]);
                    objDVOPostGL.post_no = Convert.ToInt32(dr["post_seq"]);
                    objDVOPostGL.post_date = DVOApplicationUserInfo.CurrentDate;
                    objDVOPostGL.doc_date = Convert.ToDateTime(dr["doc_date"]);
                    objDVOPostGL.ref_code = dr["empl_code"].ToString();
                    objDVOPostGL.doc_desc = "PAYROLL ENTRY";
                    objDVOPostGL.inv_chk_no = dr["check_no"].ToString();
                    objDVOPostGL.acct_no = Convert.ToInt32(dr["cash_acct_no"].ToString());
                    objDVOPostGL.amount = amount;
                    objDVOPostGL.debit_credit = db_cr;
                    objNewDVOPostGLGlobal = BLLAccountingLiberary.Gl_post(ref objDVOPostGL, ref objNewDVOPostGLGlobal, ref objTransection);
                    if (objNewDVOPostGLGlobal.sql_error != 0)
                    {
                        if (CHECK_POST == "POST")
                        {
                            objNewDVOPostGLGlobal.description = "**** Error:" + "An SQL Error has occured while posting into GL";
                            return false;
                        }
                    }
                    if (objNewDVOPostGLGlobal.status == 0 || objNewDVOPostGLGlobal.status == 3)
                    {
                    }
                    else
                    {
                        //status = 1;
                        dr["ok_to_post"] = false;
                        dr["err_3"] = "**** Error: " + objNewDVOPostGLGlobal.description;
                        dr["Problem3"] = "*****Error has been detected on this report.";
                        if (CHECK_POST == "POST")
                        {
                            return false;
                        }
                    }

                    #region Collect data for GL Summary ..........
                    DataRow drGL = GLSumTable.NewRow();
                    drGL["doc_no"] = objDVOPostGL.doc_no;
                    drGL["doc_date"] = objDVOPostGL.doc_date;
                    drGL["acctno"] = objDVOPostGL.acct_no;
                    drGL["amount"] = objDVOPostGL.amount;
                    drGL["debit_credit"] = objDVOPostGL.debit_credit;
                    drGL["flexdept"] = dr["flexdept"];
                    string keyvalue = string.Empty;
                    int id = 0;
                    string acct_type = string.Empty;
                    string acct_desc = string.Empty;
                    int acct_no = objDVOPostGL.acct_no;
                    BLLCommonUtilities.GetAccountInformation(acct_no, out keyvalue, out acct_type, out id, out acct_desc);
                    drGL["keyvalue"] = keyvalue;
                    drGL["acct_desc"] = acct_desc;
                    GLSumTable.Rows.Add(drGL.ItemArray);
                    #endregion
                }
                else
                {
                    dr["ok_to_post"] = false;
                    dr["err_3"] = "****Error: Cannot post into GL because stycntrc.post_gl field is not set to Y";
                    dr["Problem3"] = "*****Error has been detected on this report.";
                    objNewDVOPostGLGlobal.description = "Error: Cannot post into GL because stycntrc.post_gl field is not set to Y";
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        #region parameter and datatype
        //#  This routine uses prepares/declares/and executes that build the
        //#    sql query from a string because it needs to be able to compile
        //#    without any Payroll files installed.
        //#
        //#  Data elements passed:
        //#    post_or_check char(5),  "POST" or "CHECK"  tells py_post to check
        //#                            the posting (for edit lists), or post it.
        //#    orig_journal char(2),   original journal (PY, AR, OE, etc...)
        //#    doc_no integer,         document number (sequential and congruent
        //#                            for each document within a journal)
        //#    post_no integer,        posting number (post only)
        //#    post_date date,         posting date (post only)
        //#    doc_date date,          document entry date
        //#    pay_date char(6),       payroll date
        //#    ref_code char(6),       employee code
        //#    doc_desc char(30),      document description
        //#    check_no char(10),      check# (freeform - not used as a key)
        //#    act_code char(6),       activity code
        //#                            (income, deduction, obligation code)
        //#    act_type char(1),       activity types:
        //#                            A = check
        //#                            B = income
        //#                            C = deduction
        //#                            D = employer obligation
        //#                            E = employer obligation (liability)
        //#    amount like stgactvd.amount,   signed amount
        //#                            (+increases, -decreases balances)
        //#    acct_no integer,        posting account number
        //#    dept_code               posting department
        //#    eop_date                pay period ending date
        //#    number                  Number used to calculate amount
        //#    hours                   hours worked for income, used by Income and
        //#                            Check activity types,Check stores the total
        //#    rate                    rate used in amount calculation - needed
        //#                            primarily for Income types but used when
        #endregion parameter and datatype
        /// <summary>       
        /// ########################################################################
        /// This routine takes the Payroll transaction data as arguments,
        /// and either posts to Payroll or checks for an ok posting.
        /// It is designed to be run in "CHECK" mode during the edit list phase,
        /// and in "POST" mode during the posting phase.  It modifies elements
        /// in the global record post_py defined in py_glob.4gl in this
        /// directory.  All routines that post into the 4gen Payroll
        ///  module should do so via this routine.
        ///  ########################################################################
        /// Payroll entries call this routine once per invoice
        /// </summary>
        /// <param name="post_or_check"></param>
        /// <param name="orig_journal"></param>
        /// <param name="doc_no"></param>
        /// <param name="post_no"></param>
        /// <param name="post_date"></param>
        /// <param name="doc_date"></param>
        /// <param name="pay_date"></param>
        /// <param name="ref_code"></param>
        /// <param name="doc_desc"></param>
        /// <param name="check_no"></param>
        /// <param name="act_code"></param>
        /// <param name="act_type"></param>
        /// <param name="amount"></param>
        /// <param name="acct_no"></param>
        /// <param name="dept_code"></param>
        /// <param name="eop_date"></param>
        /// <param name="number"></param>
        /// <param name="hours"></param>
        /// <param name="rate"></param>
        /// <param name="line_no"></param>
        // <returns> returns true/false based on pass/fail of the routine</returns>
        static DVOExceptionReports objDVOExceptionReports = new DVOExceptionReports();
                             
        public static Boolean py_post(ref object objTransection, DVOUpdatePayDefaults objPaydefaultsStycntrc, string post_or_check, string orig_journal, int doc_no, int post_no, DateTime post_date, DateTime doc_date, DateTime pay_date, string ref_code, string doc_desc, string check_no, string act_code, string act_type, decimal amount, int acct_no, string dept_code, DateTime eop_date, string number, decimal? hours, string rate, string line_no, out int py_status, out string py_description, out string py_installed, ref decimal py_c_accum, ref decimal py_i_accum, ref decimal py_d_accum, ref decimal py_ox_accum, ref decimal py_ol_accum)
        {
            py_status = 0;     
            int stat_flag;    
            int doc_status;
            string type;
            int quarter;
            // post_py record
            py_installed=string.Empty;
            string py_ein_number;     
            int last_doc_no=0;
            int i_line_no = line_no != string.Empty ? Convert.ToInt32(line_no) : 0;
            py_description=string.Empty;
            try
            { 
                
                    //DataSet dsTableName = BLLPyPostException.GetTableName();
                    //if (dsTableName.Tables[0].Rows.Count > 0)
                    //{
                    //    py_installed = "Y";
                    //}
                    //else
                    //{
                    //    py_installed = "N";
                    //}
                    //if (py_installed == "Y")
                    //{
                    //    if (objPaydefaultsStycntrc.ein_number == string.Empty)
                    //    {
                    //        py_status = 2;
                    //        py_description = "Cannot read payroll control table";
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    //    py_status = 1;
                    //    py_description = "Payroll isn't installed.";
                    //    return false;
                    //}
                    //py_c_accum = 0;
                    //py_i_accum=0;
                    //py_d_accum = 0;
                    //py_ox_accum = 0;
                    //py_ol_accum = 0;
                    ////First of a group
                    //if (last_doc_no == doc_no - 1)
                    //{
                    //    py_status = 4;
                    //    py_description = "Document number out of sequence.";                       
                    //}
                    //last_doc_no = doc_no;
                    //   (post_or_check, orig_journal, doc_no,
                    //post_no, post_date, doc_date, ref_code, doc_desc
                    ///This routine posts a row to stxtranr for each document.  It is called
                    ///by each library posting function (eg. gl_post, ar_post and so on) once
                    ///for each document.  It is designed to be run in "CHECK" mode during
                    ///the edit list phase, and in "POST" mode during the posting phase.  (In
                    ///CHECK mode, it does nothing.) Because this routine may be called more
                    ///than once for a single transaction, it must detect when a new document
                    ///starts, and only post then.
                    ///
                    ///  Return values:
                    ///         0 = if "CHECK" or if "POST" and posted ok
                    /// What this process does:
                    ///    if "CHECK" then just return.
                    ///    if "POST" then insert the row into stxtranr
                    if (post_or_check == "POST")
                    {
                        DVOPostTrx ObjPostTransactions = new DVOPostTrx();
                        ObjPostTransactions.post_or_check = post_or_check;
                        ObjPostTransactions.orig_journal = orig_journal;
                        ObjPostTransactions.doc_no = doc_no;
                        ObjPostTransactions.post_no = post_no;
                        ObjPostTransactions.post_date = post_date;
                        ObjPostTransactions.doc_date = doc_date;
                        ObjPostTransactions.ref_code = ref_code;
                        ObjPostTransactions.doc_desc = doc_desc;
                        //Modified by Sarvjeet On 14/05/2009 added a Transactions parameter into trx_post
                        doc_status = BLLAccountingLiberary.trx_post(ref ObjPostTransactions, ref objTransection);
                        if (doc_status == 1)
                        {
                            py_status = 6;
                            py_description = "Cannot insert a new document.";
                            return false;
                        }
                        else if (doc_status == 2)
                        {
                            py_status = 5;
                            py_description = "Duplicate document number exists in stxtranr.";
                            return false;
                        }
                    
                        //post to stytranr
                        //check for existing entry
                        post_no = BLLPyPostException.GetCount1(orig_journal, doc_no);
                        if (post_no == 0)
                        {
                            //ok to insert...
                            int retvalue = BLLPyPostException.Insert1_In_stytranr(ref objTransection, ref objDVOExceptionReports, orig_journal, doc_no, check_no, pay_date, eop_date);
                            if (retvalue != 1)
                            {
                                py_status = 6;
                                py_description = "Cannot insert a new document.";
                                return false;
                            }
                        }
                       
                    }
              
                //End of first document in batch
                //accumulate check amounts
                switch (act_type)
                {
                    case "A":
                        py_c_accum += amount;
                        break;
                    case "B":
                        py_i_accum += amount;     
                        break;
                    case "C":
                        py_d_accum += amount;
                        break;
                    case "D":
                        py_ox_accum += amount;
                        break;
                    case "E":
                        py_ol_accum += amount;
                        break;
                    default:
                        break;
                }

                //return true on check
                if (post_or_check != "POST" )
                {
                    py_status = 0;
                    return true;
                }
                //POST type...
                // insert the styactvd row
                int RetvalIns2 = BLLPyPostException.Insert2_In_styactvd(ref objTransection, ref objDVOExceptionReports, orig_journal, doc_no, act_code, act_type,Convert.ToDecimal(amount),Convert.ToDecimal(number),Convert.ToDecimal(hours),Convert.ToDecimal(rate),Convert.ToInt32(acct_no), dept_code);
                if (RetvalIns2 != 1)
                {
                    py_status = 6;
                    py_description = "Cannot insert a new document into styactvd.";
                    return false;
                }
                //Calculate Quarter Number
                quarter = qtr_number(pay_date);
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
                if (doc_desc == "PAYROLL SETUP")
                {                   
                    //update accumulation for the income
                    switch (act_type)
                    {
                        case "B":
                            {
                                DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes();

                                int rowid = GetMasterEmployeeIncomesRowId(ref_code, act_code, i_line_no);
                                objDVOMasterEmployeeIncomes.Rowid = rowid;
                                int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo,false,false,false);
                                if (i == 1)
                                {
                                    int retKeyval = 0;
                                    try
                                    {
                                        retKeyval = BLLPyPostException.UPDATE_3_MasterEmployeeIncomes(ref objTransection, ref objDVOExceptionReports, amount, ref_code, act_code, i_line_no);
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        if (retKeyval != 1)
                                        {
                                            objDVOMasterEmployeeIncomes = null;
                                            py_status = 7;
                                            py_description = "Cannot update income accumulation.";
                                            return false;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        throw ex;
                                    }                                  
                                }
                                else
                                {
                                    objDVOMasterEmployeeIncomes = null;
                                    py_status = 7;
                                    py_description = "Could not lock MasterEmployeeIncomes.";
                                    return false;

                                }
                            }
                            break;                        

                        case "C":
                            {
                                //update accumulation and last taken if a deduction
                                DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
                                int rowid = GetMasterEmployeeDeductionsRowId(ref_code, act_code, i_line_no);
                                objDVOMasterEmployeeDeductions.Rowid =rowid;                           
                                int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo,false,false,false);
                                if (i == 1)
                                {
                                    try
                                    {
                                        int retval4 = BLLPyPostException.UPDATE_4_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, amount, ref_code, act_code, i_line_no);
                                        int retval6 = BLLPyPostException.UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        if (retval4 != 1 || retval6 != 1)
                                        {
                                            objDVOMasterEmployeeDeductions = null;
                                            py_status = 7;
                                            py_description = "Cannot update deduction accumulation.";
                                            return false;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        throw ex;
                                    }
                                }
                                else
                                {
                                    objDVOMasterEmployeeDeductions = null;
                                    py_status = 7;
                                    py_description = "Could not lock MasterEmployeeDeductions.";
                                    return false;
                                }
                            }
                            break;
                        case "D":
                            {
                                DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations();

                                int rowid = GetMasterEmployeeObligationsRowId(ref_code, act_code, i_line_no);
                                objDVOMasterEmployeeObligations.Rowid =rowid;
                                  
                                int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo,false,false,false);
                                if (i == 1)
                                {
                                    try
                                    {
                                        int retKeyval = BLLPyPostException.UPDATE_5_MasterEmployeeObligations(ref objTransection, ref objDVOExceptionReports, amount, ref_code, act_code, i_line_no);
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        if (retKeyval != 1)
                                        {
                                            objDVOMasterEmployeeObligations = null;
                                            py_status = 8;
                                            py_description = "Cannot update obligation accumulation.";
                                            return false;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        throw ex;
                                    }
                                }
                                else
                                {
                                    objDVOMasterEmployeeObligations = null;
                                    py_status = 8;
                                    py_description = "Could not lock MasterEmployeeObligations";
                                    return false;
                                }
                            }
                            break;                      
                        default:
                            break;
                    }
                }
                else
                {
                     //update accumulation for the income
                    switch (act_type)
                    {
                        case "B":
                            {
                                #region Update Income Accumulation when accountType=B
                                
                                int retvalue = 0;
                                DVOMasterEmployeeIncomes objDVOMasterEmployeeIncomes = new DVOMasterEmployeeIncomes();
                                int rowid = GetMasterEmployeeIncomesRowId(ref_code, act_code, i_line_no);
                                objDVOMasterEmployeeIncomes.Rowid = rowid;
                                int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo,false,false,false);
                                if (i == 1)
                                {
                                    try
                                    {
                                        switch (quarter)
                                        {
                                            case 1:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_INC1(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1)
                                                    {
                                                        objDVOMasterEmployeeIncomes = null;
                                                        py_status = 7;
                                                        py_description = "Cannot update income accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                            case 2:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_INC2(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1)
                                                    {
                                                        objDVOMasterEmployeeIncomes = null;
                                                        py_status = 7;
                                                        py_description = "Cannot update income accumulation.";
                                                        return false;
                                                    }

                                                }
                                                break;
                                            case 3:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_INC3(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1)
                                                    {
                                                        objDVOMasterEmployeeIncomes = null;
                                                        py_status = 7;
                                                        py_description = "Cannot update income accumulation.";
                                                        return false;
                                                    }

                                                }
                                                break;
                                            case 4:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_INC4(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1)
                                                    {
                                                        objDVOMasterEmployeeIncomes = null;
                                                        py_status = 7;
                                                        py_description = "Cannot update income accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeIncomes, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        throw ex;
                                    }
                                }
                                else
                                {
                                    objDVOMasterEmployeeIncomes = null;
                                    py_status = 7;
                                    py_description = "Could not lock MasterEmployeeIncomes";
                                    return false;

                                }
                                #endregion Update Income Accumulation when accountType=B
                            }
                            break;
                        case "C":
                            {
                                // update accumulation and last taken if a deduction
                                #region Update Deduction Accumulation when accountType=C
                                int retvalue=0;
                                int retval6 = 0;
                                DVOMasterEmployeeDeductions objDVOMasterEmployeeDeductions = new DVOMasterEmployeeDeductions();
                                int rowid = GetMasterEmployeeDeductionsRowId(ref_code, act_code, i_line_no);
                                objDVOMasterEmployeeDeductions.Rowid = rowid;
                                int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false,false);
                                if (i == 1)
                                {
                                    try
                                    {
                                        switch (quarter)
                                        {
                                            case 1:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_DED1(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    retval6 = BLLPyPostException.UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1 || retval6 != 1)
                                                    {

                                                        objDVOMasterEmployeeDeductions = null;
                                                        py_status = 7;
                                                        py_description = "Cannot update deduction accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                            case 2:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_DED2(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    retval6 = BLLPyPostException.UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1 || retval6 != 1)
                                                    {

                                                        objDVOMasterEmployeeDeductions = null;
                                                        py_status = 7;
                                                        py_description = "Cannot update deduction accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                            case 3:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_DED3(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    retval6 = BLLPyPostException.UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1 || retval6 != 1)
                                                    {

                                                        objDVOMasterEmployeeDeductions = null;
                                                        py_status = 7;
                                                        py_description = "Cannot update deduction accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                            case 4:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_DED4(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    retval6 = BLLPyPostException.UPDATE_6_7_MasterEmployeeDeductions(ref objTransection, ref objDVOExceptionReports, eop_date, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1 || retval6 != 1)
                                                    {

                                                        objDVOMasterEmployeeDeductions = null;
                                                        py_status = 7;
                                                        py_description = "Cannot update deduction accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeDeductions, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        throw ex;
                                    }
                                }
                                else
                                {
                                    objDVOMasterEmployeeDeductions = null;
                                    py_status = 7;
                                    py_description = "Could not lock MasterEmployeeDeductions";
                                    return false;
                                }

                                #endregion Update Deduction Accumulation when accountType=C
                            }
                            break;
                        case "D":
                            {
                                // update accumulation and last taken if a Obligation
                                #region Update Deduction Accumulation when accountType=C
                                int retvalue = 0;
                               
                                DVOMasterEmployeeObligations objDVOMasterEmployeeObligations = new DVOMasterEmployeeObligations();
                                int rowid = GetMasterEmployeeObligationsRowId(ref_code, act_code, i_line_no);
                                objDVOMasterEmployeeObligations.Rowid = rowid;
                                int i = BLLCommonUtilities.LockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false, false,false);
                                if (i == 1)
                                {
                                    try
                                    {
                                        switch (quarter)
                                        {
                                            case 1:
                                                {                       // UPDATE_OBL1(ref object objTransection, ref DVOExceptionReports objDVOExceptionReports, decimal obl_qtd1, decimal obl_ytd, string empl_code, string obl_code, int line_no)
                                                    retvalue = BLLPyPostException.UPDATE_OBL1(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1)
                                                    {
                                                        objDVOMasterEmployeeObligations = null;
                                                        py_status = 8;
                                                        py_description = "Cannot update obligation accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                            case 2:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_OBL2(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1)
                                                    {
                                                        objDVOMasterEmployeeObligations = null;
                                                        py_status = 8;
                                                        py_description = "Cannot update obligation accumulation.";
                                                        return false;
                                                    }

                                                }
                                                break;
                                            case 3:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_OBL3(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1)
                                                    {
                                                        objDVOMasterEmployeeObligations = null;
                                                        py_status = 8;
                                                        py_description = "Cannot update obligation accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                            case 4:
                                                {
                                                    retvalue = BLLPyPostException.UPDATE_OBL4(ref objTransection, ref objDVOExceptionReports, amount, amount, ref_code, act_code, i_line_no);
                                                    BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                                    if (retvalue != 1)
                                                    {
                                                        objDVOMasterEmployeeObligations = null;
                                                        py_status = 8;
                                                        py_description = "Cannot update obligation accumulation.";
                                                        return false;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        BLLCommonUtilities.ReleaseLockCurrentRecord(ref objTransection, objDVOMasterEmployeeObligations, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, false);
                                        throw ex;
                                    }
                                }
                                else
                                {
                                    objDVOMasterEmployeeObligations = null;
                                    py_status = 8;
                                    py_description = "Could not lock MasterEmployeeObligations";
                                    return false;
                                }
                            }
                            break;

                        #endregion Update Deduction Accumulation when accountType=C
                        default:
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                py_status = 1;
                py_description = ex.Message;
                throw ex;
            }            
        }

        public static int qtr_number(DateTime pay_date)
        {
            if (pay_date.Month == 1 || pay_date.Month == 2 || pay_date.Month == 3)
                return 1;
            else if (pay_date.Month == 4 || pay_date.Month == 5 || pay_date.Month == 6)
                return 2;
            else if (pay_date.Month == 7 || pay_date.Month == 8 || pay_date.Month == 9)
                return 3;
            else
                return 4;
        }

        public static int GetMasterEmployeeIncomesRowId(string  Empl_Code, string code,int line_no)
        {
            int rowid = 0;
            object[] parameter = new object[3];
            parameter[0] = Empl_Code;
            parameter[1] = code;
            parameter[2] = line_no;     
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object o = objDalBaseClass.ExecuteScalar(ref parameter, (new DVOExceptionReports()).GET_MasterEmployeeIncomes_ROWID);
            if (o != null)
                if (o.ToString().Trim() != string.Empty)
                  rowid= Convert.ToInt32(o);
          return rowid;
        }
        public static int GetMasterEmployeeDeductionsRowId(string Empl_Code, string code, int line_no)
        {
            int rowid = 0;
            object[] parameter = new object[3];
            parameter[0] = Empl_Code;
            parameter[1] = code;
            parameter[2] = line_no; 
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object o = objDalBaseClass.ExecuteScalar(ref parameter, (new DVOExceptionReports()).GET_MasterEmployeeDeductions_ROWID);
            if (o != DBNull.Value)
                if (o.ToString().Trim() != string.Empty)
                    rowid = Convert.ToInt32(o);
            return rowid;
        }
        public static int GetMasterEmployeeObligationsRowId(string Empl_Code, string code, int line_no)
        {
            int rowid = 0;
            object[] parameter = new object[3];
            parameter[0] = Empl_Code;
            parameter[1] = code;
            parameter[2] = line_no; 
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object o = objDalBaseClass.ExecuteScalar(ref parameter, (new DVOExceptionReports()).GET_MasterEmployeeObligations_ROWID);
            if (o != null)
                if (o.ToString().Trim() != string.Empty)
                    rowid = Convert.ToInt32(o);
            return rowid;
        }
    }
}
