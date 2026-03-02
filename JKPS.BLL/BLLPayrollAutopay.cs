using System;
using System.Collections.Generic;
using System.Text;
using JKPS.COMMON;
using JKPS.DL;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using App.Data;
using System.Configuration;
using Autofac.Core;

namespace JKPS.BLL
{
    public class BLLPayrollAutopay
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        //public AppDbContext db = new AppDbContext();
        public List<DVOPayrollstypayid> objPayrollIncomesglobal = new List<DVOPayrollstypayid>();
        public List<DVOPayrollstypaydd> objPayrolldeductionsglobal = new List<DVOPayrollstypaydd>();
        public List<DVOPayrollStypayod> objPayrollobligationsglobal = new List<DVOPayrollStypayod>();
        public List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();

        public decimal fica_wages = 0.0M;
        public decimal futa_wages = 0.0M;
        public Int32 dup_ssn_pay = 0;
        public Int32 dup_ssn = 0;
        public DVOPayrollautopay objpaydatasearch = new DVOPayrollautopay();
        object objTransaction;
        BLLPayrollFunctions BPfunctions = new BLLPayrollFunctions();

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //DALBaseClass objDalBaseClass;
        public bool same_person = false;
        public string empl_ssn;
        public Int32 currentempnoid;
        public List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList = new List<DVOPayrollProcess_PayEmployee>();
        public List<DVOPayrollautopay> objListemplforprocess;
        bool year_is_current = true;
        public string ErrMsg1 = string.Empty;
        public string ErrMsg2 = string.Empty;
        public string ErrMsg3 = string.Empty;
        private AppDbContext db;
        //private AppDbContext db = new AppDbContext();
        public BLLPayrollAutopay()
        {
            db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        }

        public List<DVOPayrollautopay> GetEmplListforProcess(DVOPayrollautopay objpayautosearchdata)
        {
            //this function will return the Employee
            //List for which the 
            //payroll is to be processed .
            objpaydatasearch = objpayautosearchdata;
            object[] parameters = new object[11];
            parameters[0] = objpayautosearchdata.RowID;
            parameters[1] = objpayautosearchdata.EmplCode;
            parameters[2] = objpayautosearchdata.SocSecNum;
            parameters[3] = objpayautosearchdata.FirstName;
            parameters[4] = objpayautosearchdata.LastName;
            parameters[5] = objpayautosearchdata.Employee_Type;
            parameters[6] = objpayautosearchdata.Job_Code;
            parameters[7] = "";
            parameters[8] = "";
            if (objpayautosearchdata.Process_TimeCard == "N")
            {
                parameters[9] = false;
            }
            else
            {
                parameters[9] = true;
            }

            parameters[10] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

            List<DVOPayrollautopay> objpayrollautopaylist = new List<DVOPayrollautopay>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //DataSet ds;
            //ds.Tables[0].Select(

            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollautopay)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DVOPayrollautopay objautopay = new DVOPayrollautopay();
                    objautopay.EmplCode = dr[0].ToString().Trim();
                    objautopay.SocSecNum = dr[1].ToString().Trim();
                    objautopay.FirstName = dr[2].ToString().Trim();
                    objautopay.LastName = dr[3].ToString().Trim();
                    objautopay.CashAcct = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objautopay.Department = dr[5].ToString().Trim();
                    objautopay.Terminated = (dr[6] != DBNull.Value ? Convert.ToString(dr[6]).Trim() : string.Empty);
                    objautopay.PayPeriod = dr[7].ToString().Trim();
                    objautopay.Allowances = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                    objautopay.StateAllow = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
                    objautopay.MaritalStat = dr[10].ToString().Trim();
                    objautopay.VacCode = dr[11].ToString().Trim();
                    objautopay.VacAllowed = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                    objautopay.VacUsed = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objautopay.SickCode = dr[14].ToString().Trim();
                    objautopay.SickAllowed = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objautopay.SickUsed = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                    objautopay.LastPay = (dr[17] != DBNull.Value ? Convert.ToDateTime(dr[17]) : Convert.ToDateTime(null));
                    objautopay.HoldPayment = dr[18].ToString().Trim();
                    objautopay.StaTaxCode = dr[19].ToString().Trim();
                    objautopay.LocTaxCode = dr[20].ToString().Trim();
                    objautopay.DirDept = dr[21].ToString().Trim();
                    objautopay.FlexDeptAcctType = dr[22].ToString().Trim();
                    objautopay.LastIncDate = (dr[23] != DBNull.Value ? Convert.ToString(dr[23]).Trim() : string.Empty);
                    objautopay.RowID = (dr[24] != DBNull.Value && dr[24].ToString().Trim() != "" ? Convert.ToInt32(dr[24]) : 0);
                    DVOFlexSegCommon objflexsegloadtype = new DVOFlexSegCommon();
                    objflexsegloadtype.EntityType = objautopay.TABLE_NAME;
                    objflexsegloadtype.Code = objautopay.EmplCode;
                    objflexsegloadtype.AccountType = objautopay.FlexDeptAcctType;
                    objautopay.Flexdeptkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegloadtype);
                    if (BPfunctions.pay_time(objautopay.LastPay, objpayautosearchdata.EOP_Date, objautopay.PayPeriod, Convert.ToBoolean(parameters[9])))
                    {
                        objpayrollautopaylist.Add(objautopay);
                    }
                }

            }
            return objpayrollautopaylist;

        }
        public List<DVOPayrollProcess_PayEmployee> Autopay(ref DVOPayrollautopay objpayautosearchdata)
        {
            /*
             written by     Rohit Wadhwa 
             written Date   22/12/2008
             AIM :.
             this function loads the default income/deduction/obligation
             codes into the internal arrays p_ypayre, p_ypayid, p_ypaydd,
             and p_ypayod, calculates the corresponding amounts and inserts
             them into Process_PayEmployee,stypayid,stypaydd,stypayod etc... It also sets the remaining required
             data in Process_PayEmployee.
             */
            try
            {
                bool prep_flag = false;
                bool year_is_current = true;

                objListemplforprocess = GetEmplListforProcess(objpayautosearchdata);
                DVOUpdatePayDefaults objPayDefaul = new DVOUpdatePayDefaults();
                objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                bool ok_to_commit = true;
                for (Int32 i = 0; i < objListemplforprocess.Count; i++)
                {
                    currentempnoid = i;
                    object[] parameters = new object[1];
                    DVOPayrollautopay objDvopayrollauto = new DVOPayrollautopay();
                    objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                    objDvopayrollauto = objListemplforprocess[i];
                    parameters[0] = objDvopayrollauto.SocSecNum.Trim();
                    //get from DB if it is a duplicate ssn code 
                    dup_ssn = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, objDvopayrollauto.DUP_SSN));
                    //
                    empl_ssn = objListemplforprocess[i].SocSecNum.Trim();
                    ok_to_commit = true;
                    objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    if (dup_ssn > 1)
                    {
                        parameters = new object[2];
                        parameters[0] = objDvopayrollauto.EmplCode.Trim();
                        parameters[1] = objDvopayrollauto.SocSecNum.Trim();
                        dup_ssn_pay = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, objDvopayrollauto.DUP_SSN_PAY));
                    }
                    if (dup_ssn_pay > 0)
                    {
                        same_person = true;
                        // break;
                    }

                    objPayrollProcess_PayEmployee.EmplCode = objDvopayrollauto.EmplCode;

                    if (objpayautosearchdata.Payroll_Date == Convert.ToDateTime(null))
                    {
                        objpayautosearchdata.Payroll_Date = DVOApplicationUserInfo.CurrentDate;
                    }
                    if (objpayautosearchdata.EOP_Date == Convert.ToDateTime(null))
                    {
                        objpayautosearchdata.EOP_Date = objpayautosearchdata.Payroll_Date;
                    }
                    if (objpayautosearchdata.Start_date == Convert.ToDateTime(null))
                    {
                        objpayautosearchdata.Start_date = objpayautosearchdata.EOP_Date.AddDays(-6);
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.deposit = "Y";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.deposit = "N";
                    }
                    //objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_doc_no", ref objTransaction);
                    objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next_PYDocNo();
                    if (objPayrollProcess_PayEmployee.Doc_no == 0)
                    {
                        throw new Exception("Payroll Control table is locked or empty.");
                        //rollback 
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return objDVOPayrollProcess_PayEmployeeList;
                    }

                    objPayrollProcess_PayEmployee.doc_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.pay_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.eop_date = objpayautosearchdata.EOP_Date;
                    objPayrollProcess_PayEmployee.start_date = objpayautosearchdata.Start_date;
                    objPayrollProcess_PayEmployee.bonus = objpayautosearchdata.BonusCeck;
                    if (objPayrollProcess_PayEmployee.bonus == "Y")
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "N";
                        objPayrollProcess_PayEmployee.accrue_vac = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "Y";
                        objPayrollProcess_PayEmployee.accrue_vac = "Y";
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.print_check = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.print_check = "Y";
                    }
                    objPayrollProcess_PayEmployee.ok_to_post = "N";
                    objPayrollProcess_PayEmployee.StateTaxCode = objDvopayrollauto.StaTaxCode;

                    if (objDvopayrollauto.Flexdeptacctno == 0)
                    {
                        //set value from control table is there is no value for Employee cash account .
                        objDvopayrollauto.Flexdeptacctno = objstycntrcList[0].cash_acct;

                    }

                    objPayrollProcess_PayEmployee.Cash_acct_no = objDvopayrollauto.Flexdeptacctno;
                    objPayrollProcess_PayEmployee.Department = objDvopayrollauto.Department;
                    objPayrollIncomesglobal = LoadIncomes(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                    objPayrolldeductionsglobal = LoadDeductions(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                    objPayrollobligationsglobal = LoadObligations(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);

                    objPayrollProcess_PayEmployee = CalculatePayrolls(ref objPayrollProcess_PayEmployee);

                    // post the header record
                    bool re_status = InsertIntoProcess_PayEmployee(ref objPayrollProcess_PayEmployee, ref objTransaction);
                    if (!re_status)
                    {
                        ok_to_commit = false;
                    }
                    // post income detail                  
                    for (int j = 0; j < objPayrollIncomesglobal.Count; j++)
                    {
                        objPayrollIncomesglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool id_ststus = InsertIntoStypayid(objPayrollIncomesglobal[j], ref objTransaction);
                        if (!id_ststus)
                        {
                            ok_to_commit = false;
                        }
                    }
                    //post deduction detail
                    for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
                    {
                        objPayrolldeductionsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool dd_status = InsertIntoStypaydd(objPayrolldeductionsglobal[j], ref objTransaction);
                        if (!dd_status)
                        {
                            ok_to_commit = false;
                        }
                    }
                    //post obligation detail
                    for (int j = 0; j < objPayrollobligationsglobal.Count; j++)
                    {
                        objPayrollobligationsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool od_ststus = InsertIntoStypayod(objPayrollobligationsglobal[j], ref objTransaction);
                        if (!od_ststus)
                        {
                            ok_to_commit = false;
                        }
                    }
                    if (ok_to_commit)
                    {
                        //coomitwork
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        objDVOPayrollProcess_PayEmployeeList.Add(objPayrollProcess_PayEmployee);
                    }
                    else
                    {
                        //rollback 
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    }
                }
            }

            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                objDVOPayrollProcess_PayEmployeeList.Clear();

                throw ex;

            }
            finally
            {
                if (objTransaction != null)
                    objTransaction = null;
            }
            return objDVOPayrollProcess_PayEmployeeList;
        }
        public DVOPayrollProcess_PayEmployee CalculatePayrolls(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee)
        {

            // bool dedflag = false;
            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objProcess_PayEmployee.cash_amount = 0.0M;
            objProcess_PayEmployee.inc_gross = 0.0M;
            objProcess_PayEmployee.inc_taxable = 0.0M;
            objProcess_PayEmployee.ded_fica = 0.0M;
            objProcess_PayEmployee.ded_medicare = 0.0M;
            objProcess_PayEmployee.ded_fedtax = 0.0M;
            objProcess_PayEmployee.ded_statax = 0.0M;
            objProcess_PayEmployee.ded_loctax = 0.0M;
            objProcess_PayEmployee.ded_other = 0.0M;
            objProcess_PayEmployee.obl_futa = 0.0M;
            objProcess_PayEmployee.obl_fica = 0.0M;
            objProcess_PayEmployee.obl_medicare = 0.0M;
            objProcess_PayEmployee.obl_other = 0.0M;
            objProcess_PayEmployee.obl_total = 0.0M;
            objProcess_PayEmployee.inc_net = 0.0M;
            objProcess_PayEmployee.inc_expense = 0.0M;
            objProcess_PayEmployee.total_hours = 0.0M;

            for (int i = 0; i < objPayrollIncomesglobal.Count; i++)
            {
                objProcess_PayEmployee = CalculateIncomes(ref objProcess_PayEmployee, i);
                if (objPayrollIncomesglobal[i].inc_type != "F")
                {
                    objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable + objPayrollIncomesglobal[i].amount;
                }
            }
            // set initial taxable & net income (gross cannot be less than zero)
            //
            //# Taxable income is derived from determining which income codes are to be taxed as
            //#  opposed to which deductions reduce the gross.  Only certain income codes are to
            //#  assessed SOC-SEC and LEVY taxes so we need to calculate thses separately.  These
            //#  are indicated with an "F" which indicates that the income code in question is exempt
            //#  these two taxes.
            //# let p_ypayre.inc_taxable = p_ypayre.inc_gross
            //##### ^^^^^ - - - -- - - - ^^^^^ ##########
            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross;


            // calculate deductions that reduce taxable income
            for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
            {
                if ((objPayrolldeductionsglobal[j].ded_taxred != "N") &&
                (objPayrolldeductionsglobal[j].ded_taxred != null))
                {
                    objPayrolldeductionsglobal[j].dedflag = true;
                    CalculateDeductions(ref objProcess_PayEmployee, j);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[j].amount;
                    // figure the effect on wage bases
                    switch (objPayrolldeductionsglobal[j].ded_taxred)
                    {
                        case "A":
                            {
                                // deduction reduces all wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable -
                                objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "B":
                            {
                                // deduction reduces taxable and fica wage bases

                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "C":
                            {
                                //deduction reduces taxable and futa wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "D":
                            {
                                // deduction reduces futa and fica wage bases
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "F":
                            {
                                // deduction reduces fica wage base only
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "T":
                            {
                                // deduction reduces taxable wage base only
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                break;
                            }
                        case "U":
                            {
                                // deduction reduces futa wage base only
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }

                    }
                }
                else
                {
                    objPayrolldeductionsglobal[j].dedflag = false;
                }


            }
            // calculate deductions that do not reduce taxable income
            for (int k = 0; k < objPayrolldeductionsglobal.Count; k++)
            {
                if (objPayrolldeductionsglobal[k].dedflag == false)
                {
                    CalculateDeductions(ref objProcess_PayEmployee, k);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[k].amount;
                }
            }

            // calculate obligations
            for (int k = 0; k < objPayrollobligationsglobal.Count; k++)
            {
                CalculateObligations(ref objProcess_PayEmployee, k);
            }
            //add final totals
            //adjust totals using overall deduction accumulation instead
            //of running net to take care of possible rounding errors
            objProcess_PayEmployee.cash_amount = objProcess_PayEmployee.inc_gross +
           objProcess_PayEmployee.inc_expense - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_fedtax + objProcess_PayEmployee.ded_medicare +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax +
           objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_medicare + objProcess_PayEmployee.ded_fedtax +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax + objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.obl_total = objProcess_PayEmployee.obl_futa + objProcess_PayEmployee.obl_fica +
            objProcess_PayEmployee.obl_medicare + objProcess_PayEmployee.obl_other;

            return objProcess_PayEmployee;
        }
        public DVOPayrollProcess_PayEmployee CalculateIncomes(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            //this function recalculates the income amount and resets the gross wages.

            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            //recalculate the amount
            objPayrollIncomesglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", (objPayrollIncomesglobal[i].inc_rate * objPayrollIncomesglobal[i].number)));

            // make sure amount is not null and non-negative
            if (objPayrollIncomesglobal[i].amount < 0)
            {
                objPayrollIncomesglobal[i].amount = 0.0M;
            }

            //add to the gross wages or expenses/advances
            switch (objPayrollIncomesglobal[i].inc_type)
            {
                case "H":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "E":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = objpayrollProcess_PayEmployee.inc_expense + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                case "A":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = objpayrollProcess_PayEmployee.inc_expense + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                case "F":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "U":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "B":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                default:
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
            }

            objpayrollProcess_PayEmployee.total_hours = objpayrollProcess_PayEmployee.total_hours + objPayrollIncomesglobal[i].hours;

            return objpayrollProcess_PayEmployee;
        }

        public static void CalculateIncomeChanges(string IncomeCodeType, decimal IncomeAmount, out decimal GrossIncomeChange, out decimal TaxableIncomeChange, out decimal FicaWagesChange, out decimal FutaWagesChange)
        {
            GrossIncomeChange = 0;
            FicaWagesChange = 0;
            FutaWagesChange = 0;
            TaxableIncomeChange = 0;

            switch (IncomeCodeType)
            {
                case "H":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "E":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "A":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "F":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        break;
                    }
                case "U":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "B":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                default:
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
            }
        }


        public DVOPayrollProcess_PayEmployee CalculateDeductions(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_deductions = 0.0M;
            decimal currentdeductions = 0.0M;

            if (objPayrolldeductionsglobal[i].ded_type == null)
            {
                objPayrolldeductionsglobal[i].ded_type = "T";
            }
            //if (objPayrolldeductionsglobal[i].ded_type == null)
            //{
            //    objPayrolldeductionsglobal[i].ded_type = "T";
            //}
            //if (objPayrolldeductionsglobal[i].ded_type == null)
            //{
            //    objPayrolldeductionsglobal[i].ded_type = "T";
            //}
            // set the maximum deduction
            if (objPayrolldeductionsglobal[i].ded_limit == 0.0M)
            {
                Max_deductions = Convert.ToDecimal(999999999999.99);  // large decimal(12) value
            }
            else
            {
                objPayrolldeductionsglobal[i].ded_ytd = 0.0M;
                currentdeductions = 0;
                if (dup_ssn == 1)
                {
                    object[] parameter1 = new object[2];
                    parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    parameter1[1] = objPayrolldeductionsglobal[i].ded_code;
                    object ded_ytd = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrolldeductionsglobal[0].FIND_stypayddytd);
                    if (ded_ytd.ToString().Trim() != "")
                    {
                        objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd);
                    }
                }
                else
                {
                    //currentdeductions;
                    object[] parameter1 = new object[2];
                    parameter1[0] = empl_ssn;
                    parameter1[1] = objPayrolldeductionsglobal[i].ded_code;
                    object ded_ytd = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrolldeductionsglobal[0].FIND_stypayddytd1);
                    if (ded_ytd.ToString().Trim() != "")
                    {
                        objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd);
                    }

                    if (same_person)
                    {
                        object[] parameter2 = new object[3];
                        parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                        parameter1[1] = empl_ssn;
                        parameter1[2] = objPayrolldeductionsglobal[i].ded_code;
                        object ded_ytd1 = objDalBaseClass.ExecuteScalar(ref parameter2, objPayrolldeductionsglobal[0].FIND_stypayddytd2);
                        if (ded_ytd1.ToString().Trim() != "")
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd1);
                        }
                    }
                }


                if (currentdeductions != 0)
                {
                    objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + currentdeductions;
                }
                // get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrolldeductionsglobal[j].ded_code == objPayrolldeductionsglobal[i].ded_code)
                    {
                        if (objPayrolldeductionsglobal[j].amount != 0)
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + objPayrolldeductionsglobal[j].amount;
                        }
                    }
                }
                Max_deductions = (objPayrolldeductionsglobal[i].ded_limit - objPayrolldeductionsglobal[i].ded_ytd) ?? 0; //make nullable decimal By Rahul
            }
            if (Max_deductions < 0.0M)
            {
                Max_deductions = 0.0M;
            }
            //calc the amount of the deduction relative to type
            if (objPayrolldeductionsglobal[i].ded_code.Trim() == objpayrollProcess_PayEmployee.StateTaxCode.Trim())
            {
                //call the state tax calculation logic
                objPayrolldeductionsglobal[i].amount = state_calc(i);
            }
            switch (objPayrolldeductionsglobal[i].ded_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != 0))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_gross));
                            }
                        }
                        else
                        {
                            //make nullable decimal By Rahul
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod);
                        }
                        break;
                    }

                case "T":
                    {
                        //calculate amount using taxable wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != Convert.ToDecimal(null)))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_taxable));
                            }
                        }
                        else
                        {
                            //make nullable decimal By Rahul
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod);
                        }
                        break;
                    }
                case "U":
                    {
                        //calculate amount using futa wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * futa_wages));
                        }
                        break;
                    }
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * fica_wages));
                        }
                        break;
                    }
                case "H":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.total_hours));
                        break;
                    }
                case "N":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        break;
                    }
                default:
                    {
                        objPayrolldeductionsglobal[i].amount = 0.0M;
                        break;
                    }

            }


            //make sure deduction is not greater than net or zero if net < 0
            if (objPayrolldeductionsglobal[i].amount > 0)
            {
                if (objpayrollProcess_PayEmployee.inc_net < 0)
                {
                    objPayrolldeductionsglobal[i].amount = 0;
                }
                else
                {
                    if (objPayrolldeductionsglobal[i].amount > objpayrollProcess_PayEmployee.inc_net)
                    {
                        objPayrolldeductionsglobal[i].amount = objpayrollProcess_PayEmployee.inc_net;
                    }
                }
            }

            if (objPayrolldeductionsglobal[i].pay_limit != 0.0M)
            {
                if (objPayrolldeductionsglobal[i].amount > objPayrolldeductionsglobal[i].pay_limit)
                {
                    objPayrolldeductionsglobal[i].amount = objPayrolldeductionsglobal[i].pay_limit;
                }
            }
            // check for limit
            if (objPayrolldeductionsglobal[i].amount > Max_deductions)
            {
                objPayrolldeductionsglobal[i].amount = Max_deductions;
            }

            // post amount to correct total
            if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fedtax_code)
            {
                objpayrollProcess_PayEmployee.ded_fedtax = objpayrollProcess_PayEmployee.ded_fedtax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].statax_code)
            {
                objpayrollProcess_PayEmployee.ded_statax = objpayrollProcess_PayEmployee.ded_statax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].loctax_code)
            {
                objpayrollProcess_PayEmployee.ded_loctax = objpayrollProcess_PayEmployee.ded_loctax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.ded_fica = objpayrollProcess_PayEmployee.ded_fica + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].medicare_code)
            {
                objpayrollProcess_PayEmployee.ded_medicare = objpayrollProcess_PayEmployee.ded_medicare + objPayrolldeductionsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.ded_other = objpayrollProcess_PayEmployee.ded_other + objPayrolldeductionsglobal[i].amount;
            }



            return objpayrollProcess_PayEmployee;
        }
        public DVOPayrollProcess_PayEmployee CalculateObligations(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_Obligations = 0.0M;
            decimal Current_obligations = 0.0M;
            decimal obligationYTD = 0.0M;
            decimal Max_deductions = 0.0M;
            // this function calculates the obligation amount and updates
            //   cumulative totals for the control obligation codes

            // set obligation type
            if (objPayrollobligationsglobal[i].obl_type == null)
            {
                objPayrollobligationsglobal[i].obl_type = "T";
            }

            // get obligation limit and accrual

            // set the maximum obligation
            if (objPayrollobligationsglobal[i].obl_limit == 0.0M)
            {
                Max_deductions = 999999999.99M;  // large decimal(12) value
            }
            else
            {
                //check for current accrual
                //Current_obligations = 0.0M;
                if (dup_ssn == 1)
                {
                    //obligationYTD = 0.0M; //Set it with Year to date obligations
                    object[] parameter1 = new object[2];
                    parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    object Current_obl = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd);
                    if (Current_obl.ToString().Trim() != "")
                    {
                        objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obl);
                    }
                }
                else
                {
                    //obligationYTD = 0.0M; //Set it with Year to date obligations
                    object[] parameter1 = new object[2];
                    parameter1[0] = empl_ssn;
                    parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    object oblYTD = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd1);
                    if (oblYTD.ToString().Trim() != "")
                    {
                        objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(oblYTD);
                    }

                    if (same_person)
                    {
                        //Current_obligations = 0.0M; //set it with current obligations 
                        object[] parameter2 = new object[3];
                        parameter2[0] = objpayrollProcess_PayEmployee.EmplCode;
                        parameter2[1] = empl_ssn;
                        parameter2[2] = objPayrollobligationsglobal[i].obl_code;
                        object Current_obligations1 = objDalBaseClass.ExecuteScalar(ref parameter2, objPayrollobligationsglobal[0].FIND_stypayodytd2);
                        if (Current_obligations1.ToString().Trim() != "")
                        {
                            objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obligations1);
                        }

                    }
                }

                if (Current_obligations != 0)
                {
                    objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + Current_obligations;//objPayrollobligationsglobal[i].amount;
                }
                //get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrollobligationsglobal[j].obl_code == objPayrollobligationsglobal[i].obl_code)
                    {
                        if (objPayrollobligationsglobal[j].amount != 0.0M)
                        {
                            objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + objPayrollobligationsglobal[j].amount;
                        }
                    }

                }
                Max_Obligations = (objPayrollobligationsglobal[i].obl_limit - objPayrollobligationsglobal[i].obl_ytd) ?? 0;//make nullable decimal By Rahul
            }
            if (Max_Obligations < 0)
            {
                Max_Obligations = 0.0M;
            }
            switch (objPayrollobligationsglobal[i].obl_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_gross));
                        }
                    }
                    break;
                case "T":
                    {
                        //calculate amount using taxable wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_taxable));
                        }
                    }
                    break;
                case "U":
                    //calculate amount using futa wage base
                    {
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_futa));
                        }
                    }
                    break;
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_fica));
                        }
                    }
                    break;
                case "E":
                    // Based on the employees deduction amount
                    {
                        for (int j = 0; j <= objPayrolldeductionsglobal.Count; j++)
                        {
                            if (objPayrollobligationsglobal[i].obl_code == objPayrolldeductionsglobal[j].ded_code)
                            {
                                objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objPayrolldeductionsglobal[j].amount));
                                break;
                            }
                        }
                    }
                    break;
                case "H":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.total_hours));
                    }
                    break;
                case "N":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                    }
                    break;
                    //default:
                    //    {
                    //        objPayrollobligationsglobal[i].amount = 0.0M;
                    //    }
            }

            //Modified by Sarvjeet On 13/08/2009
            if (objPayrollobligationsglobal[i].pay_limit > 0.0M)
            {

                if (objPayrollobligationsglobal[i].amount > objPayrollobligationsglobal[i].pay_limit)
                {

                    objPayrollobligationsglobal[i].amount = objPayrollobligationsglobal[i].pay_limit;

                }

            }
            //check for limit
            if (objPayrollobligationsglobal[i].amount > Max_Obligations)
            {
                objPayrollobligationsglobal[i].amount = Max_Obligations;
            }
            // post amount to correct total
            if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].futa_code)
            {
                objpayrollProcess_PayEmployee.obl_futa = objpayrollProcess_PayEmployee.obl_futa + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.obl_fica = objpayrollProcess_PayEmployee.obl_fica + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].medicare_ob_code)
            {
                objpayrollProcess_PayEmployee.obl_medicare = objpayrollProcess_PayEmployee.obl_medicare + objPayrollobligationsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.obl_other = objpayrollProcess_PayEmployee.obl_other + objPayrollobligationsglobal[i].amount;
            }

            //   DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            return objpayrollProcess_PayEmployee;
        }
        public static List<DVOPayrollstypayid> LoadIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOUpdateTimeCard> objlistTimecard = new List<DVOUpdateTimeCard>();
            objlistTimecard = LoadTimeCardIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            List<DVOPayrollstypayid> objlistpayrollincomes = new List<DVOPayrollstypayid>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            Int32 Maxlineno = 0;
            if (objlistTimecard.Count < 1)
            {
                objlistTimecard = LoadEmployeeIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            }
            for (int i = 0; i < objlistTimecard.Count; i++)
            {
                DVOPayrollstypayid objemployeeincome = new DVOPayrollstypayid();
                objemployeeincome.inc_code = objlistTimecard[i].inc_code_id;
                objemployeeincome.inc_rate = objlistTimecard[i].inc_rate_id;
                objemployeeincome.number = objlistTimecard[i].inc_number_id;
                objemployeeincome.hours = objlistTimecard[i].inc_hours_id;
                objemployeeincome.add_code = objlistTimecard[i].add_code_cr;
                objemployeeincome.inc_type = objlistTimecard[i].inc_type_cr;
                if ((objemployeeincome.add_code == "Y") || (objemployeeincome.add_code == "Z"))
                {
                    parameters[0] = EmployeeCode;

                    object Maxlineno1 = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOPayrollautopay()).EMPLOYEEMAXLINENOGET);
                    if (Maxlineno1 == null)
                    {
                        Maxlineno = 0;

                    }
                    Maxlineno = Maxlineno + i;
                }
                else
                {
                    objemployeeincome.add_code = "N";
                    objemployeeincome.line_no = objlistTimecard[i].line_no_id;
                }
                objemployeeincome.lo_inc_amt = objlistTimecard[i].lo_inc_amt_id;
                objemployeeincome.hi_inc_amt = objlistTimecard[i].hi_inc_amt_id;
                // Take the timecard account number first.  Comment out the following
                // line here, but use it as a default if the timecard account number is null.
                // let p_ypayid[n].acct_no = inc_ref[n].acct_no
                objemployeeincome.acct_no = objlistTimecard[i].timecd_acct_no;
                objemployeeincome.Department = objlistTimecard[i].department_id;

                //Assign Default Values as required ........
                if (objemployeeincome.lo_inc_amt == 0.0M)
                {
                    objemployeeincome.lo_inc_amt = objlistTimecard[i].dflt_lo_inc_amt_cr;
                }
                if (objemployeeincome.hi_inc_amt == 0.0M)
                {
                    objemployeeincome.hi_inc_amt = objlistTimecard[i].dflt_hi_inc_amt_cr;
                }
                if (objemployeeincome.inc_rate == 0.0M)
                {
                    objemployeeincome.inc_rate = objlistTimecard[i].dflt_rate_cr;
                }
                if (objemployeeincome.hours == 0.0M)
                {
                    objemployeeincome.hours = objlistTimecard[i].dflt_hours_cr;
                }
                if (objemployeeincome.number == 0.0M)
                {
                    objemployeeincome.number = objlistTimecard[i].dflt_num_cr;
                }
                // use defaults if necessary

                if (objemployeeincome.acct_no == 0)
                {
                    objemployeeincome.acct_no = objlistTimecard[i].acct_no_id;
                    if (objemployeeincome.acct_no == 0)
                    {
                        objemployeeincome.acct_no = objlistTimecard[i].dflt_acct_cr;
                    }
                }
                if ((objemployeeincome.Department == string.Empty) || (objemployeeincome.Department == null))
                {
                    objemployeeincome.Department = objlistTimecard[i].dflt_dept_cr;
                }
                objlistpayrollincomes.Add(objemployeeincome);
            }
            return objlistpayrollincomes;
        }
        public List<DVOPayrollstypaydd> LoadDeductions(string EmployeeCode, DateTime EOPdate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollstypaydd> objListpayrollstypaydd = new List<DVOPayrollstypaydd>();
            List<DVOMasterEmployeeDeductions> objListemployeeDeddefaultdata = new List<DVOMasterEmployeeDeductions>();
            DVOPayrollstypaydd objpayrollstypaydd = new DVOPayrollstypaydd();
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Dedreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeDeductions), (new DVOMasterEmployeeDeductions()).EmpDedanddefaults))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOMasterEmployeeDeductions objemployeedefaultdedrec = new DVOMasterEmployeeDeductions();

                    objemployeedefaultdedrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dfltkeyvalue = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                    objemployeedefaultdedrec.ded_code = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                    objemployeedefaultdedrec.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objemployeedefaultdedrec.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
                    objemployeedefaultdedrec.ded_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
                    objemployeedefaultdedrec.pay_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                    objemployeedefaultdedrec.balanceamt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                    objemployeedefaultdedrec.ded_apply = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]).Trim() : string.Empty);
                    objemployeedefaultdedrec.lo_ded_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);
                    objemployeedefaultdedrec.hi_ded_amt = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0);
                    objemployeedefaultdedrec.acct_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
                    objemployeedefaultdedrec.department = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]).Trim() : "000");
                    objemployeedefaultdedrec.ded_ytd = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0);
                    objemployeedefaultdedrec.ded_date = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                    objemployeedefaultdedrec.description = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : string.Empty);
                    objemployeedefaultdedrec.ded_type = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]).Trim() : string.Empty);
                    objemployeedefaultdedrec.ded_taxred = (dr[17] != DBNull.Value ? Convert.ToString(dr[17]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dflt_rate = (dr[18] != DBNull.Value ? Convert.ToDecimal(dr[18]) : 0.0M);
                    objemployeedefaultdedrec.dflt_limit = (dr[19] != DBNull.Value ? Convert.ToDecimal(dr[19]) : 0);
                    objemployeedefaultdedrec.dflt_pay_limit = (dr[20] != DBNull.Value ? Convert.ToDecimal(dr[20]) : 0);
                    objemployeedefaultdedrec.yearrollover = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dflt_lo_ded_amt = (dr[22] != DBNull.Value ? Convert.ToDecimal(dr[22]) : 0);
                    objemployeedefaultdedrec.dflt_hi_ded_amt = (dr[23] != DBNull.Value ? Convert.ToDecimal(dr[23]) : 0);
                    objemployeedefaultdedrec.dflt_acct = (dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0);
                    objemployeedefaultdedrec.dflt_dept = (dr[25] != DBNull.Value ? Convert.ToString(dr[25]).Trim() : "000");
                    objemployeedefaultdedrec.dflt_apply = (dr[26] != DBNull.Value ? Convert.ToString(dr[26]).Trim() : string.Empty);
                    parameters = new object[2];
                    parameters[0] = objemployeedefaultdedrec.ded_code;
                    parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    object taxcode = objDalBaseClass.ExecuteScalar(ref parameters, objpaydatasearch.DeductionTaxCodeGet);
                    if (taxcode != DBNull.Value)
                    {
                        objemployeedefaultdedrec.tax_code = taxcode.ToString().Trim();
                    }

                    parameters = new object[2];
                    parameters[0] = objemployeedefaultdedrec.ded_code;
                    parameters[1] = EOPdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                    object result = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeDeductions()).DeductioncodeTaxget);
                    if (result != null)
                        objemployeedefaultdedrec.tax_code = result.ToString().Trim();
                    if (objemployeedefaultdedrec.ded_apply == string.Empty)
                    {
                        objemployeedefaultdedrec.ded_apply = objemployeedefaultdedrec.dflt_apply;
                    }
                    // assign defaults as required
                    if (objemployeedefaultdedrec.lo_ded_amt == 0)
                    {
                        objemployeedefaultdedrec.lo_ded_amt = objemployeedefaultdedrec.dflt_lo_ded_amt;
                    }
                    if (objemployeedefaultdedrec.hi_ded_amt == 0)
                    {
                        objemployeedefaultdedrec.hi_ded_amt = objemployeedefaultdedrec.dflt_hi_ded_amt;
                    }
                    if (objemployeedefaultdedrec.ded_rate == 0)
                    {
                        objemployeedefaultdedrec.ded_rate = objemployeedefaultdedrec.dflt_rate;
                    }
                    if (objemployeedefaultdedrec.acct_no == 0)
                    {
                        objemployeedefaultdedrec.acct_no = objemployeedefaultdedrec.dflt_acct;
                    }
                    if (objemployeedefaultdedrec.department == null)
                    {
                        objemployeedefaultdedrec.department = objemployeedefaultdedrec.dflt_dept;
                    }
                    if (objemployeedefaultdedrec.ded_limit == 0)
                    {
                        objemployeedefaultdedrec.ded_limit = objemployeedefaultdedrec.dflt_limit;
                    }


                    objListemployeeDeddefaultdata.Add(objemployeedefaultdedrec);
                }
            }
            Dedreccount = objListemployeeDeddefaultdata.Count;
            for (int i = 0; i < Dedreccount; i++)
            {
                objpayrollstypaydd = new DVOPayrollstypaydd();
                // check to make sure deduction should be taken now

                //if (objListemployeeDeddefaultdata[i].ded_apply != string.Empty)
                // {
                DateTime ded_date = Convert.ToDateTime(null);
                BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                if (objListemployeeDeddefaultdata[i].ded_date != string.Empty)
                {
                    ded_date = Convert.ToDateTime(objListemployeeDeddefaultdata[i].ded_date);
                }
                else
                {
                    ded_date = Convert.ToDateTime(null);
                }
                if (objpayrollfunctions.Pay_Frequency(objListemployeeDeddefaultdata[i].ded_apply, EOPdate, ded_date))
                {
                    objpayrollstypaydd.ded_rate = objListemployeeDeddefaultdata[i].ded_rate.GetValueOrDefault(0.0M);
                }
                else
                {
                    objListemployeeDeddefaultdata[i].ded_rate = 0.0M;
                }
                //  }
                objpayrollstypaydd.ded_code = objListemployeeDeddefaultdata[i].ded_code;
                objpayrollstypaydd.amount = 0;
                objpayrollstypaydd.lo_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].lo_ded_amt);
                objpayrollstypaydd.hi_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].hi_ded_amt);
                objpayrollstypaydd.ded_taxred = objListemployeeDeddefaultdata[i].ded_taxred;
                objpayrollstypaydd.acct_no = objListemployeeDeddefaultdata[i].acct_no;
                objpayrollstypaydd.Department = objListemployeeDeddefaultdata[i].department;
                objpayrollstypaydd.line_no = objListemployeeDeddefaultdata[i].line_no;
                objpayrollstypaydd.add_code = "N";
                objpayrollstypaydd.ded_type = objListemployeeDeddefaultdata[i].ded_type;
                if (objListemployeeDeddefaultdata[i].pay_limit == 0)
                {
                    objpayrollstypaydd.pay_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_pay_limit);
                }

                if (objListemployeeDeddefaultdata[i].yearrollover == "Y")
                {
                    if (objpayrollstypaydd.amount > Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt))
                    {
                        objpayrollstypaydd.amount = Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt);
                    }
                }
                if (objListemployeeDeddefaultdata[i].ded_limit == 0)
                {
                    objpayrollstypaydd.ded_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_limit);
                }
                if (objpayrollstypaydd.ded_ytd == 0.0M)
                {
                    objpayrollstypaydd.ded_ytd = Convert.ToDecimal(objListemployeeDeddefaultdata[i].ded_ytd);
                }
                if (objpayrollstypaydd.tax_code == string.Empty)
                {
                    objpayrollstypaydd.tax_code = objListemployeeDeddefaultdata[i].tax_code;
                }
                objListpayrollstypaydd.Add(objpayrollstypaydd);
            }

            return objListpayrollstypaydd;
        }
        public static List<DVOPayrollStypayod> LoadObligations(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollStypayod> objListpayrollstypayod = new List<DVOPayrollStypayod>();
            List<DVOMasterEmployeeObligations> objListemployeeObldefaultdate = new List<DVOMasterEmployeeObligations>();

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Oblreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeObligations), (new DVOMasterEmployeeObligations()).Emplobldefaultsget))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //MasterOblCodes.dfltaccounttype,MasterOblCodes.dfltbaccounttype,MasterOblCodes.dfltbkeyvalue,
                    //MasterEmployeeObligations.obl_code,MasterEmployeeObligations.line_no,MasterEmployeeObligations.obl_rate,MasterEmployeeObligations.obl_limit,
                    //MasterEmployeeObligations.pay_limit,MasterEmployeeObligations.acct_no,MasterEmployeeObligations.department,MasterEmployeeObligations.bal_acct_no,
                    //MasterEmployeeObligations.bal_dept,MasterEmployeeObligations.obl_ytd,MasterOblCodes.description,
                    //MasterOblCodes.obl_type,MasterOblCodes.dflt_rate,MasterOblCodes.dflt_limit,
                    //MasterOblCodes.dflt_pay_limit,  MasterOblCodes.dflt_acct, MasterOblCodes.dflt_dept,
                    //MasterOblCodes.dflt_bacct,MasterOblCodes.dflt_bdept

                    DVOMasterEmployeeObligations objemployeedefaultoblrec = new DVOMasterEmployeeObligations();
                    objemployeedefaultoblrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dfltbaccounttype = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dfltbkeyvalue = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                    objemployeedefaultoblrec.obl_code = (dr[3] != DBNull.Value ? Convert.ToString(dr[3]).Trim() : string.Empty);
                    objemployeedefaultoblrec.line_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objemployeedefaultoblrec.obl_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                    objemployeedefaultoblrec.obl_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                    objemployeedefaultoblrec.pay_limit = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                    objemployeedefaultoblrec.acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                    objemployeedefaultoblrec.department = (dr[9] != DBNull.Value ? Convert.ToString(dr[9]).Trim() : "000");
                    objemployeedefaultoblrec.bal_acct_no = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                    objemployeedefaultoblrec.bal_dept = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                    objemployeedefaultoblrec.obl_ytd = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);
                    objemployeedefaultoblrec.description = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]).Trim() : string.Empty);
                    objemployeedefaultoblrec.obl_type = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dflt_rate = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objemployeedefaultoblrec.dflt_limit = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0);
                    objemployeedefaultoblrec.dflt_pay_limit = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0);
                    objemployeedefaultoblrec.dflt_acct = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                    objemployeedefaultoblrec.dflt_dept = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                    objemployeedefaultoblrec.dflt_bacct = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);
                    objemployeedefaultoblrec.dflt_bdept = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : "000");
                    DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                    objflexsegcommon.AccountType = objemployeedefaultoblrec.dfltaccounttype;
                    objflexsegcommon.EntityType = "MasterOblCodes";
                    objflexsegcommon.Code = objemployeedefaultoblrec.obl_code;
                    objemployeedefaultoblrec.dfltbkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    objpayrollfunctions.flxMix(objemployeedefaultoblrec.dfltaccounttype, objemployeedefaultoblrec.dfltbkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                    objemployeedefaultoblrec.acct_no = MixaccountNo;
                    // objemployeedefaultoblrec.dflt_acct
                    if ((objemployeedefaultoblrec.dflt_acct == 0) &&
                     (objemployeedefaultoblrec.acct_no == 0))
                    {
                        //Test if the Account Exists in the table or not with th ekeyvalue we got now .
                        // scratch will contain the description after testFlexAccountKey
                        //create PayrollGLAccounts Flex account Entry
                    }
                    objListemployeeObldefaultdate.Add(objemployeedefaultoblrec);
                }

            }
            Oblreccount = objListemployeeObldefaultdate.Count;
            for (Int32 i = 0; i < Oblreccount; i++)
            {
                DVOPayrollStypayod objpayrollstypayod = new DVOPayrollStypayod();
                objpayrollstypayod.obl_code = objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].obl_rate;
                objpayrollstypayod.amount = 0;//objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].acct_no;
                objpayrollstypayod.Department = objListemployeeObldefaultdate[i].department;
                objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].bal_acct_no;
                objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].bal_dept;
                objpayrollstypayod.line_no = objListemployeeObldefaultdate[i].line_no;
                objpayrollstypayod.add_code = "N";
                objpayrollstypayod.obl_type = objListemployeeObldefaultdate[i].obl_type;
                // assign defaults as required

                if (objpayrollstypayod.obl_rate == 0.0M)
                {
                    objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].dflt_rate;
                }
                if (objpayrollstypayod.acct_no == 0)
                {
                    objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].dflt_acct;
                }
                if ((objpayrollstypayod.Department == "000") || (objpayrollstypayod.Department == null))
                {
                    objpayrollstypayod.Department = objListemployeeObldefaultdate[i].dflt_dept;
                }
                if (objpayrollstypayod.bal_acct_no == 0)
                {
                    objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].dflt_bacct;
                }
                if ((objpayrollstypayod.bal_dept == "000") || (objpayrollstypayod.bal_dept == null))
                {
                    objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].dflt_bdept;
                }
                if (objListemployeeObldefaultdate[i].pay_limit == 0)
                {
                    objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_pay_limit);
                }
                else
                {
                    objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].pay_limit);
                }
                if (objListemployeeObldefaultdate[i].obl_limit == 0)
                {
                    objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_limit);
                }
                else
                {
                    objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].obl_limit);
                }
                objListpayrollstypayod.Add(objpayrollstypayod);

            }



            return objListpayrollstypayod;
        }
        public static List<DVOUpdateTimeCard> LoadTimeCardIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[2];
            parameters[0] = EmployeeCode;
            parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), (new DVOUpdateTimeCard()).FIND_DETAILS_FORAUTOPAY))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                    objUpdatetimecardincome.dfltkeyvalue = "";
                    objUpdatetimecardincome.acct_no_id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                    objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                    objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
                    objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
                    objUpdatetimecardincome.description_cr = dr[8].ToString().Trim();
                    objUpdatetimecardincome.dflt_num_cr = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
                    objUpdatetimecardincome.dflt_rate_cr = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0.0M);
                    objUpdatetimecardincome.dflt_hours_cr = (dr[11] != DBNull.Value ? Convert.ToDecimal(dr[11]) : 0.0M);
                    objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                    objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objUpdatetimecardincome.dflt_acct_cr = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
                    objUpdatetimecardincome.dflt_dept_cr = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : "000");
                    objUpdatetimecardincome.inc_type_cr = dr[16].ToString().Trim();

                    DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                    objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                    objflexsegcommon.EntityType = "MasterIncCodes";
                    objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                    objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                    objUpdatetimecardincome.dflt_acct_cr = MixaccountNo;
                    parameters = new object[3];
                    parameters[0] = EmployeeCode;
                    parameters[1] = objUpdatetimecardincome.inc_code_id;
                    parameters[2] = objUpdatetimecardincome.line_no_id;
                    DataSet dst = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeIncomes), (new DVOMasterEmployeeIncomes()).EMPLOYEEINCOME_AUTOPAY);
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drt in dst.Tables[0].Rows)
                        {
                            objUpdatetimecardincome.lo_inc_amt_id = (drt[0] != DBNull.Value ? Convert.ToDecimal(drt[0]) : 0.0M); ;
                            objUpdatetimecardincome.hi_inc_amt_id = (drt[1] != DBNull.Value ? Convert.ToDecimal(drt[1]) : 0.0M); ;
                            objUpdatetimecardincome.acct_no_id = (drt[2] != DBNull.Value ? Convert.ToInt32(drt[2]) : 0);
                            objUpdatetimecardincome.department_id = (drt[3] != DBNull.Value ? Convert.ToString(drt[3]).Trim() : "000"); ;
                        }

                        if (((objUpdatetimecardincome.timecd_acct_no == 0) || (objUpdatetimecardincome.timecd_acct_no == null)) &&
                        ((objUpdatetimecardincome.acct_no_id == 0) || (objUpdatetimecardincome.acct_no_id == null)) &&
                       ((objUpdatetimecardincome.dflt_acct_cr == 0) || (objUpdatetimecardincome.dflt_acct_cr == null)))
                        {
                            //test and create flex key records 

                        }
                        objUpdatetimecardincome.add_code_cr = "N";
                    }
                    else
                    {
                        // does the code already exist at the employee level?

                        parameters = new object[2];
                        parameters[0] = objUpdatetimecardincome.inc_code_id;
                        parameters[1] = EmployeeCode;
                        dupempinccount = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeIncomes()).EMPLINCOMECNT_AUTOPAY));
                        if (dupempinccount >= 1)
                        {
                            objUpdatetimecardincome.add_code_cr = "Z";
                        }
                        else
                        {
                            objUpdatetimecardincome.add_code_cr = "Y";
                        }
                    }
                    if (lastcard == objUpdatetimecardincome.card_no)
                    {

                    }
                    else
                    {
                        lastcard = objUpdatetimecardincome.card_no;
                        newRec = true;
                        //   objListtimecard.Add(objUpdatetimecardincome);
                    }

                    Timecarddetail = objListtimecard.Count;
                    for (int i = 0; i < Timecarddetail; i++)
                    {
                        if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                            (objUpdatetimecardincome.inc_rate_id == objListtimecard[i].inc_rate_id))
                        {
                            objListtimecard[i].inc_hours_id = objListtimecard[i].inc_hours_id + objUpdatetimecardincome.inc_hours_id;
                            objListtimecard[i].inc_number_id = objListtimecard[i].inc_number_id + objUpdatetimecardincome.inc_number_id;
                            newRec = false;
                            break;
                        }
                        else
                        {
                            // If the code is the same as the employee entry
                            // but the rate differs we do not want to append
                            if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                              (objUpdatetimecardincome.line_no_id == objListtimecard[i].line_no_id))
                            {
                                if ((objUpdatetimecardincome.inc_number_id == 0) && (objListtimecard[i].inc_number_id != 0))
                                {
                                    objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                    objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                    newRec = false;
                                    break;
                                }
                                else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id != 0))
                                {
                                    objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                    objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                    objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                    newRec = false;
                                    break;
                                }
                                else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id == 0))
                                {
                                    objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                    objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                    objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                    newRec = false;
                                    break;
                                }
                                else
                                {
                                    //This code is a duplicate but the rate differs
                                    // and the number is not zero.
                                    objUpdatetimecardincome.add_code_cr = "Z";
                                    // newRec = false;
                                }

                            }
                        }
                    }
                    if (newRec == true)
                    {
                        objListtimecard.Add(objUpdatetimecardincome);
                        objUpdatetimecardincome = new DVOUpdateTimeCard();
                    }

                }
            }
            // If duplicate codes were added on the fly to the timecard, we
            // only want to append one to the employee entry at posting time.
            Timecarddetail = objListtimecard.Count;
            for (int j = 0; j < Timecarddetail; j++)
            {
                if (objListtimecard[j].add_code_cr == "Y")
                {
                    for (int k = 0; k < Timecarddetail; k++)
                    {
                        if (j == k)
                        {
                        }
                        else
                        {
                            if (objListtimecard[j].inc_code_id == objListtimecard[k].inc_code_id)
                            {
                                objListtimecard[j].add_code_cr = "Z";
                            }
                        }
                    }
                }
            }
            return objListtimecard;
        }
        public static List<DVOUpdateTimeCard> LoadEmployeeIncomes(string EmployeeCode, DateTime Eopdate, string FlexacctType, string FlexDepartment)
        {
            //This function loads the default income data from the employee
            //reference tables into an income reference array and reset the array
            // count.

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixAcctNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollautopay), (new DVOPayrollautopay()).EmployeeIncomerefer))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
                    objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                    objUpdatetimecardincome.dfltkeyvalue = "";
                    objUpdatetimecardincome.timecd_acct_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                    objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                    objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
                    objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
                    objUpdatetimecardincome.lo_inc_amt_id = (dr[8] != DBNull.Value ? Convert.ToDecimal(dr[8]) : 0.0M);
                    objUpdatetimecardincome.hi_inc_amt_id = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
                    objUpdatetimecardincome.acct_no_id = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                    objUpdatetimecardincome.department_id = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                    objUpdatetimecardincome.description_cr = dr[12].ToString().Trim();
                    objUpdatetimecardincome.dflt_num_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objUpdatetimecardincome.dflt_rate_cr = (dr[14] != DBNull.Value ? Convert.ToDecimal(dr[14]) : 0.0M);
                    objUpdatetimecardincome.dflt_hours_cr = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                    objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0.0M);
                    objUpdatetimecardincome.dflt_acct_cr = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                    objUpdatetimecardincome.dflt_dept_cr = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                    objUpdatetimecardincome.inc_type_cr = dr[20].ToString().Trim();
                    DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                    objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                    objflexsegcommon.EntityType = "MasterIncCodes";
                    objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                    objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixAcctNo, out MixAcctType, out Mixkeyvalue);
                    objUpdatetimecardincome.dflt_acct_cr = MixAcctNo;
                    if (objUpdatetimecardincome.timecd_acct_no == 0 && objUpdatetimecardincome.acct_no_id == 0 && objUpdatetimecardincome.dflt_acct_cr == 0)
                    {
                        //test and create flex key records 

                    }
                    objListtimecard.Add(objUpdatetimecardincome);
                }
            }
            return objListtimecard;

        }

        public static bool InsertIntoProcess_PayEmployee(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee, ref object objTrx)
        {
            try
            {
                if (objProcess_PayEmployee.bonus.Trim().Length <= 0)
                    objProcess_PayEmployee.bonus = "N";

                object[] parameters = new object[32];
                parameters[0] = objProcess_PayEmployee.Doc_no;
                parameters[1] = objProcess_PayEmployee.EmplCode;
                parameters[2] = objProcess_PayEmployee.doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[3] = objProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[4] = objProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[5] = objProcess_PayEmployee.print_check;
                parameters[6] = objProcess_PayEmployee.Cash_acct_no;
                parameters[7] = objProcess_PayEmployee.Department;
                parameters[8] = objProcess_PayEmployee.cash_amount;
                parameters[9] = objProcess_PayEmployee.check_no;
                parameters[10] = objProcess_PayEmployee.inc_gross;
                parameters[11] = objProcess_PayEmployee.ded_fica;
                parameters[12] = objProcess_PayEmployee.inc_taxable;
                parameters[13] = objProcess_PayEmployee.ded_medicare;
                parameters[14] = objProcess_PayEmployee.ded_fedtax;
                parameters[15] = objProcess_PayEmployee.ded_statax;
                parameters[16] = objProcess_PayEmployee.ded_loctax;
                parameters[17] = objProcess_PayEmployee.ded_other;
                parameters[18] = objProcess_PayEmployee.obl_futa;
                parameters[19] = objProcess_PayEmployee.obl_fica;
                parameters[20] = objProcess_PayEmployee.obl_medicare;
                parameters[21] = objProcess_PayEmployee.obl_other;
                parameters[22] = objProcess_PayEmployee.obl_total;
                parameters[23] = objProcess_PayEmployee.inc_net;
                parameters[24] = objProcess_PayEmployee.inc_expense;
                parameters[25] = objProcess_PayEmployee.total_hours;
                parameters[26] = objProcess_PayEmployee.ok_to_post;
                parameters[27] = objProcess_PayEmployee.accrue_sick;
                parameters[28] = objProcess_PayEmployee.accrue_vac;
                parameters[29] = objProcess_PayEmployee.bonus;
                parameters[30] = objProcess_PayEmployee.deposit;
                parameters[31] = objProcess_PayEmployee.start_date;

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objProcess_PayEmployee.FIND_INSERT_Process_PayEmployee, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertIntoStypayid(DVOPayrollstypayid objDVOPayrollstypayid, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[13];
                parameters[0] = objDVOPayrollstypayid.Doc_no;
                parameters[1] = objDVOPayrollstypayid.line_no;
                parameters[2] = objDVOPayrollstypayid.inc_code.Trim();
                parameters[3] = objDVOPayrollstypayid.inc_rate;
                parameters[4] = objDVOPayrollstypayid.number;
                parameters[5] = objDVOPayrollstypayid.hours;
                parameters[6] = objDVOPayrollstypayid.amount;
                parameters[7] = objDVOPayrollstypayid.acct_no;
                parameters[8] = objDVOPayrollstypayid.Department.Trim();
                parameters[9] = objDVOPayrollstypayid.mod_flag;
                parameters[10] = objDVOPayrollstypayid.add_code.Trim();
                parameters[11] = objDVOPayrollstypayid.lo_inc_amt;
                parameters[12] = objDVOPayrollstypayid.hi_inc_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypayid.INSERT_STYPAYID, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool InsertIntoStypaydd(DVOPayrollstypaydd objDVOPayrollstypaydd, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollstypaydd.Doc_no;
                parameters[1] = objDVOPayrollstypaydd.line_no;
                parameters[2] = objDVOPayrollstypaydd.ded_code.Trim();
                parameters[3] = objDVOPayrollstypaydd.ded_rate;
                parameters[4] = objDVOPayrollstypaydd.amount;
                parameters[5] = objDVOPayrollstypaydd.acct_no;
                parameters[6] = objDVOPayrollstypaydd.Department.Trim();
                parameters[7] = objDVOPayrollstypaydd.mod_flag;
                parameters[8] = objDVOPayrollstypaydd.add_code.Trim();
                parameters[9] = objDVOPayrollstypaydd.lo_ded_amt;
                parameters[10] = objDVOPayrollstypaydd.hi_ded_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypaydd.INSERT_STYPARDD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertIntoStypayod(DVOPayrollStypayod objDVOPayrollStypayod, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollStypayod.Doc_no;
                parameters[1] = objDVOPayrollStypayod.line_no;
                parameters[2] = objDVOPayrollStypayod.obl_code.Trim();
                parameters[3] = objDVOPayrollStypayod.obl_rate;
                parameters[4] = objDVOPayrollStypayod.amount;
                parameters[5] = objDVOPayrollStypayod.acct_no;
                parameters[6] = objDVOPayrollStypayod.Department.Trim();
                parameters[7] = objDVOPayrollStypayod.bal_acct_no;
                parameters[8] = objDVOPayrollStypayod.bal_dept.Trim();
                parameters[9] = objDVOPayrollStypayod.mod_flag;
                parameters[10] = objDVOPayrollStypayod.add_code.Trim();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollStypayod.INSERT_STYPAYOD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public decimal ded_taxcalc(int n, decimal tax_wages, string pay_period)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // check for exempt status for state
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                if (objListemplforprocess[currentempnoid].StateAllow == 99)
                {
                    return 0;
                }
            }
            else
            {
                //if not state tax code then default to federal allowances
                if (objListemplforprocess[currentempnoid].Allowances == 99)
                {
                    return 0;
                }
            }
            // initialize flags
            bool check_year = false;
            year_is_current = true;
            decimal allow_amt = 0;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";

            // get allowance value
            object[] parameters = new object[2];
            parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            switch (pay_period)
            {

                case "W":
                    {
                        object week_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_week_allow);
                        if (week_allow == null || week_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(week_allow);

                        }
                        break;
                    }
                case "B":
                    {
                        object biweek_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_biweek_allow);
                        if (biweek_allow == null || biweek_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(biweek_allow);

                        }
                        break;
                    }
                case "S":
                    {
                        object smonth_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_smonth_allow);
                        if (smonth_allow == null || smonth_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(smonth_allow);

                        }
                        break;
                    }
                case "M":
                    {
                        object month_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_month_allow);
                        if (month_allow == null || month_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(month_allow);

                        }
                        break;
                    }
                case "Q":
                    {
                        object quarter_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_quarter_allow);
                        if (quarter_allow == null || quarter_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(quarter_allow);

                        }
                        break;
                    }
                case "H":
                    {
                        object syear_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_syear_allow);
                        if (syear_allow == null || syear_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(syear_allow);

                        }
                        break;
                    }
                case "A":
                    {
                        object year_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
                        if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(year_allow);

                        }
                        break;
                    }
                case "D":
                    {
                        object misc_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_misc_allow);
                        if (misc_allow == null || misc_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(misc_allow);

                        }
                        break;
                    }
            }
            //check_year is set to true when a table lookup returns nothing
            //check to see if the table does exist, but the Tax Year is not current
            if (check_year)
            {
                //call tbl_check
                tbl_check(n);
            }
            // make sure required values exist
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[currentempnoid].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            // calculate taxable amount
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));

            }
            else
            {
                //if not state tax code then default to federal allowances
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            //get tax table values
            if (year_is_current)
            {
                object[] taxparameter = new object[5];
                taxparameter[0] = objPayrolldeductionsglobal[n].ded_code;
                taxparameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                taxparameter[2] = pay_period.Trim();
                taxparameter[3] = t_total;
                taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                DataSet ds = objDalBaseClass.GetData(ref taxparameter, typeof(DVOPayrollstypaydd), objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                        t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                        t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                        t_period = dr[3].ToString().Trim();
                        t_marital = dr[4].ToString().Trim();
                    }
                }
                //calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }

            return t_total;
        }
        public void tbl_check(int n)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // this function is called to verify that if a table exists, that
            // the Tax Year matches the payroll date year.
            object[] parameter = new object[2];
            parameter[0] = objPayrolldeductionsglobal[n].ded_code;
            parameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollstypaydd), objPayrolldeductionsglobal[n].FIND_usp_tbl_check);
            if (ds.Tables[0].Rows[0][0] == DBNull.Value || ds.Tables[0].Rows[0][0].ToString().Trim() == string.Empty)
            {
                ErrMsg2 = "*** Error: Table(s) not current for employee's deduction codes.";
                ErrMsg1 = "**** End of report.  One or more deduction tables are not current.";
                year_is_current = false;
            }

        }
        public decimal state_calc(int n)
        {
            // define
            decimal wage_amount = 0;
            decimal statax_amount = 0;
            // set wage_amount appropriately
            if (objPayrolldeductionsglobal[n].ded_type.Trim() == "G")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_gross ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "T")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_taxable ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "F")
            {
                wage_amount = fica_wages;

            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "U")
            {
                wage_amount = futa_wages;
            }
            else
            {
                wage_amount = 0;
            }
            if (objPayrolldeductionsglobal[n].tax_code == string.Empty)
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));

                }
                else
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate * wage_amount));
                }
            }
            else
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));
                }
                else
                {
                    statax_amount = ded_taxcalc(n, wage_amount, objListemplforprocess[n].PayPeriod);

                }

            }
            return statax_amount;
        }
        public decimal ded_fedgrs(int n, decimal tax_wages, string pay_period)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal allow_amt = 0;
            bool check_year = false;
            bool year_is_current = true;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";
            object[] parameters = new object[2];
            parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            object year_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
            if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
            {
                check_year = true;
            }
            else
            {
                allow_amt = Convert.ToDecimal(year_allow);

            }
            if (check_year)
            {
                tbl_check(n);
            }
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[n].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));
            }
            else
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            if (year_is_current)
            {
                object[] taxparameter = new object[5];
                taxparameter[0] = objstycntrcList[0].fedtax_code;
                taxparameter[1] = objDVOPayrollProcess_PayEmployeeList[n].pay_date;
                taxparameter[2] = "A";
                taxparameter[3] = t_total;
                taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                DataSet ds = objDalBaseClass.GetData(ref taxparameter, typeof(DVOPayrollstypaydd), objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                    t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                    t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                    t_period = dr[3].ToString().Trim();
                    t_marital = dr[4].ToString().Trim();
                }
                // calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
                if (t_total < 0)
                {
                    t_total = 0;
                }

            }
            return t_total;
        }
    }


    public class BLLCalculatePayroll
    {
        ////public List<DVOPayrollstypayid> objPayrollIncomesglobal = new List<DVOPayrollstypayid>();
        ////public List<DVOPayrollstypaydd> objPayrolldeductionsglobal = new List<DVOPayrollstypaydd>();
        ////public List<DVOPayrollStypayod> objPayrollobligationsglobal = new List<DVOPayrollStypayod>();
        ////public List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();

        ////public decimal fica_wages = 0.0M;
        ////public decimal futa_wages = 0.0M;
        ////public Int32 dup_ssn_pay = 0;
        ////public Int32 dup_ssn = 0;
        ////public DVOPayrollautopay objpaydatasearch = new DVOPayrollautopay();
        //object objTransaction;
        BLLPayrollFunctions BPfunctions = new BLLPayrollFunctions();
        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        ////DALBaseClass objDalBaseClass;
        ////public bool same_person = false;
        ////public string empl_ssn;
        ////public Int32 currentempnoid;
        ////public List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList = new List<DVOPayrollProcess_PayEmployee>();
        ////public List<DVOPayrollautopay> objListemplforprocess;
        ////bool year_is_current = true;
        ////public string ErrMsg1 = string.Empty;
        ////public string ErrMsg2 = string.Empty;
        ////public string ErrMsg3 = string.Empty;

        public List<DVOPayrollautopay> GetEmplListforProcess(DVOPayrollautopay objpayautosearchdata, ref DVOPayrollautopay objpaydatasearch)
        {
            //this function will return the Employee
            //List for which the 
            //payroll is to be processed .
            objpaydatasearch = objpayautosearchdata;
            object[] parameters = new object[11];
            parameters[0] = objpayautosearchdata.RowID;
            parameters[1] = objpayautosearchdata.EmplCode;
            parameters[2] = objpayautosearchdata.SocSecNum;
            parameters[3] = objpayautosearchdata.FirstName;
            parameters[4] = objpayautosearchdata.LastName;
            parameters[5] = objpayautosearchdata.Employee_Type;
            parameters[6] = objpayautosearchdata.Job_Code;
            parameters[7] = "";
            parameters[8] = "";
            if (objpayautosearchdata.Process_TimeCard == "N")
            {
                parameters[9] = false;
            }
            else
            {
                parameters[9] = true;
            }

            parameters[10] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

            List<DVOPayrollautopay> objpayrollautopaylist = new List<DVOPayrollautopay>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //DataSet ds;
            //ds.Tables[0].Select(

            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollautopay)))
            using (DataSet ds = objDalBaseClass.GetData((new DVOPayrollautopay()).FIND_PAYROLL_ENTRY_TO_UPDATE(ref parameters)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DVOPayrollautopay objautopay = new DVOPayrollautopay();
                    objautopay.EmplCode = dr[0].ToString().Trim();
                    objautopay.SocSecNum = dr[1].ToString().Trim();
                    objautopay.FirstName = dr[2].ToString().Trim();
                    objautopay.LastName = dr[3].ToString().Trim();
                    objautopay.CashAcct = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objautopay.Department = dr[5].ToString().Trim();
                    objautopay.Terminated = (dr[6] != DBNull.Value ? Convert.ToString(dr[6]).Trim() : string.Empty);
                    objautopay.PayPeriod = dr[7].ToString().Trim();
                    objautopay.Allowances = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                    objautopay.StateAllow = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
                    objautopay.MaritalStat = dr[10].ToString().Trim();
                    objautopay.VacCode = dr[11].ToString().Trim();
                    objautopay.VacAllowed = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                    objautopay.VacUsed = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objautopay.SickCode = dr[14].ToString().Trim();
                    objautopay.SickAllowed = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objautopay.SickUsed = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                    objautopay.LastPay = (dr[17] != DBNull.Value ? Convert.ToDateTime(dr[17]) : Convert.ToDateTime(null));
                    objautopay.HoldPayment = dr[18].ToString().Trim();
                    objautopay.StaTaxCode = dr[19].ToString().Trim();
                    objautopay.LocTaxCode = dr[20].ToString().Trim();
                    objautopay.DirDept = dr[21].ToString().Trim();
                    objautopay.FlexDeptAcctType = dr[22].ToString().Trim();
                    objautopay.LastIncDate = (dr[23] != DBNull.Value ? Convert.ToString(dr[23]).Trim() : string.Empty);
                    objautopay.RowID = (dr[24] != DBNull.Value && dr[24].ToString().Trim() != "" ? Convert.ToInt32(dr[24]) : 0);
                    DVOFlexSegCommon objflexsegloadtype = new DVOFlexSegCommon();
                    objflexsegloadtype.EntityType = objautopay.TABLE_NAME;
                    objflexsegloadtype.Code = objautopay.EmplCode;
                    objflexsegloadtype.AccountType = objautopay.FlexDeptAcctType;
                    objautopay.Flexdeptkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegloadtype);
                    if (BPfunctions.pay_time(objautopay.LastPay, objpayautosearchdata.EOP_Date, objautopay.PayPeriod, Convert.ToBoolean(parameters[9])))
                    {
                        objpayrollautopaylist.Add(objautopay);
                    }
                }

            }
            return objpayrollautopaylist;

        }

        public List<DVOPayrollProcess_PayEmployee> Autopay(ref object objTransaction,
            ref DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            bool statusObjTransaction = true;
            if (objTransaction == null)
            {
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                statusObjTransaction = false;
            }
            /*
             written by     Rohit Wadhwa 
             written Date   22/12/2008
             AIM :.
             this function loads the default income/deduction/obligation
             codes into the internal arrays p_ypayre, p_ypayid, p_ypaydd,
             and p_ypayod, calculates the corresponding amounts and inserts
             them into Process_PayEmployee,stypayid,stypaydd,stypayod etc... It also sets the remaining required
             data in Process_PayEmployee.
             */

            List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList = new List<DVOPayrollProcess_PayEmployee>();
            DVOPayrollautopay objpaydatasearch = new DVOPayrollautopay();
            List<DVOPayrollstypayid> objPayrollIncomesglobal = new List<DVOPayrollstypayid>();
            List<DVOPayrollstypaydd> objPayrolldeductionsglobal = new List<DVOPayrollstypaydd>();
            List<DVOPayrollStypayod> objPayrollobligationsglobal = new List<DVOPayrollStypayod>();

            try
            {
                Int32 currentempnoid = 0;
                Int32 dup_ssn = 0;
                string empl_ssn = string.Empty;
                Int32 dup_ssn_pay = 0;
                bool same_person = false;
                decimal fica_wages = 0;
                decimal futa_wages = 0;
                string ErrMsg1 = string.Empty;
                string ErrMsg2 = string.Empty;
                string ErrMsg3 = string.Empty;

                bool prep_flag = false;
                bool year_is_current = true;
                List<DVOPayrollautopay> objListemplforprocess = GetEmplListforProcess(objpayautosearchdata, ref objpaydatasearch);
                DVOUpdatePayDefaults objPayDefaul = new DVOUpdatePayDefaults();
                List<DVOUpdatePayDefaults> objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);

                DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                bool ok_to_commit = true;
                for (Int32 i = 0; i < objListemplforprocess.Count; i++)
                {
                    currentempnoid = i;
                    object[] parameters = new object[1];
                    DVOPayrollautopay objDvopayrollauto = new DVOPayrollautopay();
                    objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                    objDvopayrollauto = objListemplforprocess[i];
                    parameters[0] = objDvopayrollauto.SocSecNum.Trim();
                    //get from DB if it is a duplicate ssn code 
                    dup_ssn = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, objDvopayrollauto.DUP_SSN));
                    //
                    empl_ssn = objListemplforprocess[i].SocSecNum.Trim();
                    ok_to_commit = true;
                    //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    if (dup_ssn > 1)
                    {
                        parameters = new object[2];
                        parameters[0] = objDvopayrollauto.EmplCode.Trim();
                        parameters[1] = objDvopayrollauto.SocSecNum.Trim();
                        dup_ssn_pay = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, objDvopayrollauto.DUP_SSN_PAY));
                    }
                    if (dup_ssn_pay > 0)
                    {
                        same_person = true;
                        // break;
                    }

                    objPayrollProcess_PayEmployee.EmplCode = objDvopayrollauto.EmplCode;

                    if (objpayautosearchdata.Payroll_Date == null)
                    {
                        objpayautosearchdata.Payroll_Date = DVOApplicationUserInfo.CurrentDate;
                    }
                    if (objpayautosearchdata.EOP_Date == null)
                    {
                        objpayautosearchdata.EOP_Date = objpayautosearchdata.Payroll_Date;
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.deposit = "Y";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.deposit = "N";
                    }
                    //objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_doc_no", ref objTransaction);
                    objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next_PYDocNo();
                    if (objPayrollProcess_PayEmployee.Doc_no == 0)
                    {
                        throw new Exception("Payroll Control table is locked or empty.");
                        //rollback 
                        if (!statusObjTransaction && objTransaction != null)
                            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return objDVOPayrollProcess_PayEmployeeList;
                    }

                    objPayrollProcess_PayEmployee.doc_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.pay_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.eop_date = objpayautosearchdata.EOP_Date;
                    objPayrollProcess_PayEmployee.bonus = objpayautosearchdata.BonusCeck;
                    if (objPayrollProcess_PayEmployee.bonus == "Y")
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "N";
                        objPayrollProcess_PayEmployee.accrue_vac = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "Y";
                        objPayrollProcess_PayEmployee.accrue_vac = "Y";
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.print_check = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.print_check = "Y";
                    }
                    objPayrollProcess_PayEmployee.ok_to_post = "N";
                    objPayrollProcess_PayEmployee.StateTaxCode = objDvopayrollauto.StaTaxCode;
                    if (objDvopayrollauto.Flexdeptacctno == 0)
                    {
                        //set value from control table is there is no value for Employee cash account .
                        objDvopayrollauto.Flexdeptacctno = objstycntrcList[0].cash_acct;

                    }

                    objPayrollProcess_PayEmployee.Cash_acct_no = objDvopayrollauto.Flexdeptacctno;
                    objPayrollProcess_PayEmployee.Department = objDvopayrollauto.Department;
                    objPayrollIncomesglobal = LoadIncomes(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                    objPayrolldeductionsglobal = LoadDeductions(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue, ref objpaydatasearch);
                    objPayrollobligationsglobal = LoadObligations(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);

                    objPayrollProcess_PayEmployee = CalculatePayrolls(ref objPayrollProcess_PayEmployee, ref objPayrollIncomesglobal, ref objPayrolldeductionsglobal, ref objPayrollobligationsglobal,
                        ref objstycntrcList,
                        ref objListemplforprocess,
                        ref objDVOPayrollProcess_PayEmployeeList,
                        ref objpaydatasearch,
                        ref dup_ssn,
                        ref empl_ssn,
                        ref same_person,
                        ref fica_wages,
                        ref futa_wages,
                        ref currentempnoid,
                        ref year_is_current,
                        ref ErrMsg1,
                        ref ErrMsg2);
                    // post the header record
                    bool re_status = InsertIntoProcess_PayEmployee(ref objPayrollProcess_PayEmployee, ref objTransaction);
                    if (!re_status)
                    {
                        ok_to_commit = false;
                    }
                    // post income detail                  
                    for (int j = 0; j < objPayrollIncomesglobal.Count; j++)
                    {
                        objPayrollIncomesglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool id_ststus = InsertIntoStypayid(objPayrollIncomesglobal[j], ref objTransaction);
                        if (!id_ststus)
                        {
                            ok_to_commit = false;
                        }
                    }
                    //post deduction detail
                    for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
                    {
                        objPayrolldeductionsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool dd_status = InsertIntoStypaydd(objPayrolldeductionsglobal[j], ref objTransaction);
                        if (!dd_status)
                        {
                            ok_to_commit = false;
                        }
                    }
                    //post obligation detail
                    for (int j = 0; j < objPayrollobligationsglobal.Count; j++)
                    {
                        objPayrollobligationsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool od_ststus = InsertIntoStypayod(objPayrollobligationsglobal[j], ref objTransaction);
                        if (!od_ststus)
                        {
                            ok_to_commit = false;
                        }
                    }
                    if (ok_to_commit)
                    {
                        //coomitwork
                        if (!statusObjTransaction && objTransaction != null)
                            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        objDVOPayrollProcess_PayEmployeeList.Add(objPayrollProcess_PayEmployee);
                    }
                    else
                    {
                        //rollback 
                        if (!statusObjTransaction && objTransaction != null)
                            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    }
                }

            }

            catch (Exception ex)
            {
                if (!statusObjTransaction && objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                objDVOPayrollProcess_PayEmployeeList.Clear();

                return objDVOPayrollProcess_PayEmployeeList;

            }
            finally
            {
                if (objTransaction != null)
                    objTransaction = null;
            }
            return objDVOPayrollProcess_PayEmployeeList;
        }

        /// <summary>
        /// objpayautosearchdata should have empl_code
        /// </summary>
        /// <param name="objTransaction"></param>
        /// <param name="objpayautosearchdata"></param>
        /// <returns></returns>
        public List<DVOPayrollProcess_PayEmployee> ReloadPayrollEntries(//ref object objTransaction,
            ref DVOPayrollautopay objpayautosearchdata,
            ref List<DVOPayrollstypayid> objPayrollIncomesglobal,
            ref List<DVOPayrollstypaydd> objPayrolldeductionsglobal,
            ref List<DVOPayrollStypayod> objPayrollobligationsglobal,
            int PayrollDocumentNo)
        {
            /*
             written by     Bharat Dhall 
             written Date   08/01/2009
             AIM :.
             this function loads the default income/deduction/obligation
             codes into the internal arrays p_ypayre, p_ypayid, p_ypaydd,
             and p_ypayod, calculates the corresponding amounts and inserts
             them into Process_PayEmployee,stypayid,stypaydd,stypayod etc... It also sets the remaining required
             data in Process_PayEmployee.
             */
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //bool statusObjTransaction = true;
            //if (objTransaction == null)
            //{
            //    objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //    statusObjTransaction = false;
            //}

            List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList = new List<DVOPayrollProcess_PayEmployee>();
            DVOPayrollautopay objpaydatasearch = new DVOPayrollautopay();
            //List<DVOPayrollstypayid> objPayrollIncomesglobal = new List<DVOPayrollstypayid>();
            //List<DVOPayrollstypaydd> objPayrolldeductionsglobal = new List<DVOPayrollstypaydd>();
            //List<DVOPayrollStypayod> objPayrollobligationsglobal = new List<DVOPayrollStypayod>();

            try
            {
                Int32 currentempnoid = 0;
                Int32 dup_ssn = 0;
                string empl_ssn = string.Empty;
                Int32 dup_ssn_pay = 0;
                bool same_person = false;
                decimal fica_wages = 0;
                decimal futa_wages = 0;
                string ErrMsg1 = string.Empty;
                string ErrMsg2 = string.Empty;
                string ErrMsg3 = string.Empty;

                bool prep_flag = false;
                bool year_is_current = true;
                List<DVOPayrollautopay> objListemplforprocess = GetEmplListforProcess(objpayautosearchdata, ref objpaydatasearch);
                DVOUpdatePayDefaults objPayDefaul = new DVOUpdatePayDefaults();
                List<DVOUpdatePayDefaults> objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);

                DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                //bool ok_to_commit = true;
                for (Int32 i = 0; i < objListemplforprocess.Count; i++)
                {
                    currentempnoid = i;
                    object[] parameters = new object[1];
                    DVOPayrollautopay objDvopayrollauto = new DVOPayrollautopay();
                    objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                    objDvopayrollauto = objListemplforprocess[i];
                    parameters[0] = objDvopayrollauto.SocSecNum.Trim();
                    //get from DB if it is a duplicate ssn code 
                    dup_ssn = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, objDvopayrollauto.DUP_SSN));
                    //
                    empl_ssn = objListemplforprocess[i].SocSecNum.Trim();
                    //ok_to_commit = true;
                    //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    if (dup_ssn > 1)
                    {
                        parameters = new object[2];
                        parameters[0] = objDvopayrollauto.EmplCode.Trim();
                        parameters[1] = objDvopayrollauto.SocSecNum.Trim();
                        dup_ssn_pay = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, objDvopayrollauto.DUP_SSN_PAY));
                    }
                    if (dup_ssn_pay > 0)
                    {
                        same_person = true;
                        // break;
                    }

                    objPayrollProcess_PayEmployee.EmplCode = objDvopayrollauto.EmplCode;

                    if (objpayautosearchdata.Payroll_Date == null)
                    {
                        objpayautosearchdata.Payroll_Date = DVOApplicationUserInfo.CurrentDate;
                    }
                    if (objpayautosearchdata.EOP_Date == null)
                    {
                        objpayautosearchdata.EOP_Date = objpayautosearchdata.Payroll_Date;
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.deposit = "Y";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.deposit = "N";
                    }
                    objPayrollProcess_PayEmployee.Doc_no = PayrollDocumentNo;
                    //objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_doc_no", ref objTransaction);
                    //if (objPayrollProcess_PayEmployee.Doc_no == 0)
                    //{
                    //    //rollback 
                    //    if (!statusObjTransaction && objTransaction != null)
                    //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //    return objDVOPayrollProcess_PayEmployeeList;
                    //}

                    objPayrollProcess_PayEmployee.doc_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.pay_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.eop_date = objpayautosearchdata.EOP_Date;
                    objPayrollProcess_PayEmployee.bonus = objpayautosearchdata.BonusCeck;
                    if (objPayrollProcess_PayEmployee.bonus == "Y")
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "N";
                        objPayrollProcess_PayEmployee.accrue_vac = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "Y";
                        objPayrollProcess_PayEmployee.accrue_vac = "Y";
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.print_check = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.print_check = "Y";
                    }
                    objPayrollProcess_PayEmployee.ok_to_post = "N";
                    objPayrollProcess_PayEmployee.StateTaxCode = objDvopayrollauto.StaTaxCode;
                    if (objDvopayrollauto.Flexdeptacctno == 0)
                    {
                        //set value from control table is there is no value for Employee cash account .
                        objDvopayrollauto.Flexdeptacctno = objstycntrcList[0].cash_acct;

                    }

                    objPayrollProcess_PayEmployee.Cash_acct_no = objDvopayrollauto.Flexdeptacctno;
                    objPayrollProcess_PayEmployee.Department = objDvopayrollauto.Department;
                    //objPayrollIncomesglobal = LoadIncomes(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                    //objPayrolldeductionsglobal = LoadDeductions(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue, ref objpaydatasearch);
                    //objPayrollobligationsglobal = LoadObligations(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);

                    objPayrollProcess_PayEmployee = CalculatePayrolls(ref objPayrollProcess_PayEmployee, ref objPayrollIncomesglobal, ref objPayrolldeductionsglobal, ref objPayrollobligationsglobal,
                        ref objstycntrcList,
                        ref objListemplforprocess,
                        ref objDVOPayrollProcess_PayEmployeeList,
                        ref objpaydatasearch,
                        ref dup_ssn,
                        ref empl_ssn,
                        ref same_person,
                        ref fica_wages,
                        ref futa_wages,
                        ref currentempnoid,
                        ref year_is_current,
                        ref ErrMsg1,
                        ref ErrMsg2);
                    //// post the header record
                    //bool re_status = InsertIntoProcess_PayEmployee(ref objPayrollProcess_PayEmployee, ref objTransaction);
                    //if (!re_status)
                    //{
                    //    ok_to_commit = false;
                    //}
                    //// post income detail                  
                    //for (int j = 0; j < objPayrollIncomesglobal.Count; j++)
                    //{
                    //    objPayrollIncomesglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                    //    bool id_ststus = InsertIntoStypayid(objPayrollIncomesglobal[j], ref objTransaction);
                    //    if (!id_ststus)
                    //    {
                    //        ok_to_commit = false;
                    //    }
                    //}
                    ////post deduction detail
                    //for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
                    //{
                    //    objPayrolldeductionsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                    //    bool dd_status = InsertIntoStypaydd(objPayrolldeductionsglobal[j], ref objTransaction);
                    //    if (!dd_status)
                    //    {
                    //        ok_to_commit = false;
                    //    }
                    //}
                    ////post obligation detail
                    //for (int j = 0; j < objPayrollobligationsglobal.Count; j++)
                    //{
                    //    objPayrollobligationsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                    //    bool od_ststus = InsertIntoStypayod(objPayrollobligationsglobal[j], ref objTransaction);
                    //    if (!od_ststus)
                    //    {
                    //        ok_to_commit = false;
                    //    }
                    //}
                    //if (ok_to_commit)
                    //{
                    //    //coomitwork
                    //    if (!statusObjTransaction && objTransaction != null)
                    //        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                    objDVOPayrollProcess_PayEmployeeList.Add(objPayrollProcess_PayEmployee);
                    //}
                    //else
                    //{
                    //    //rollback 
                    //    if (!statusObjTransaction && objTransaction != null)
                    //        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    //}
                }

            }

            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
                //if (!statusObjTransaction && objTransaction != null)
                //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                objDVOPayrollProcess_PayEmployeeList.Clear();

                return objDVOPayrollProcess_PayEmployeeList;

            }
            finally
            {
                //if (objTransaction != null)
                //    objTransaction = null;
            }
            return objDVOPayrollProcess_PayEmployeeList;
        }

        public DVOPayrollProcess_PayEmployee CalculatePayrolls(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee,
            ref List<DVOPayrollstypayid> objPayrollIncomesglobal,
            ref List<DVOPayrollstypaydd> objPayrolldeductionsglobal,
            ref List<DVOPayrollStypayod> objPayrollobligationsglobal,
            ref List<DVOUpdatePayDefaults> objstycntrcList,
            ref List<DVOPayrollautopay> objListemplforprocess,
            ref List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList,
            ref DVOPayrollautopay objpaydatasearch,
            ref Int32 dup_ssn,
            ref string empl_ssn,
            ref bool same_person,
            ref decimal fica_wages,
            ref decimal futa_wages,
            ref Int32 currentempnoid,
            ref bool year_is_current,
            ref string ErrMsg1,
            ref string ErrMsg2)
        {

            // bool dedflag = false;
            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objProcess_PayEmployee.cash_amount = 0.0M;
            objProcess_PayEmployee.inc_gross = 0.0M;
            objProcess_PayEmployee.inc_taxable = 0.0M;
            objProcess_PayEmployee.ded_fica = 0.0M;
            objProcess_PayEmployee.ded_medicare = 0.0M;
            objProcess_PayEmployee.ded_fedtax = 0.0M;
            objProcess_PayEmployee.ded_statax = 0.0M;
            objProcess_PayEmployee.ded_loctax = 0.0M;
            objProcess_PayEmployee.ded_other = 0.0M;
            objProcess_PayEmployee.obl_futa = 0.0M;
            objProcess_PayEmployee.obl_fica = 0.0M;
            objProcess_PayEmployee.obl_medicare = 0.0M;
            objProcess_PayEmployee.obl_other = 0.0M;
            objProcess_PayEmployee.obl_total = 0.0M;
            objProcess_PayEmployee.inc_net = 0.0M;
            objProcess_PayEmployee.inc_expense = 0.0M;
            objProcess_PayEmployee.total_hours = 0.0M;

            for (int i = 0; i < objPayrollIncomesglobal.Count; i++)
            {
                objProcess_PayEmployee = CalculateIncomes(ref objProcess_PayEmployee, i,
                    ref objPayrollIncomesglobal,
                    ref fica_wages,
                    ref futa_wages);
                if (objPayrollIncomesglobal[i].inc_type != "F")
                {
                    objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable + objPayrollIncomesglobal[i].amount;
                }
            }
            // set initial taxable & net income (gross cannot be less than zero)
            //
            //# Taxable income is derived from determining which income codes are to be taxed as
            //#  opposed to which deductions reduce the gross.  Only certain income codes are to
            //#  assessed SOC-SEC and LEVY taxes so we need to calculate thses separately.  These
            //#  are indicated with an "F" which indicates that the income code in question is exempt
            //#  these two taxes.
            //# let p_ypayre.inc_taxable = p_ypayre.inc_gross
            //##### ^^^^^ - - - -- - - - ^^^^^ ##########
            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross;


            // calculate deductions that reduce taxable income
            for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
            {
                if ((objPayrolldeductionsglobal[j].ded_taxred != "N") &&
                (objPayrolldeductionsglobal[j].ded_taxred != null))
                {
                    objPayrolldeductionsglobal[j].dedflag = true;
                    CalculateDeductions(ref objProcess_PayEmployee, j, ref objPayrolldeductionsglobal,
                        ref objstycntrcList,
                        ref objListemplforprocess,
                        ref objDVOPayrollProcess_PayEmployeeList,
                        ref objpaydatasearch,
                        ref dup_ssn,
                        ref empl_ssn,
                        ref same_person,
                        ref fica_wages,
                        ref futa_wages,
                        ref currentempnoid,
                        ref year_is_current,
                        ref ErrMsg1,
                        ref ErrMsg2);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[j].amount;
                    // figure the effect on wage bases
                    switch (objPayrolldeductionsglobal[j].ded_taxred)
                    {
                        case "A":
                            {
                                // deduction reduces all wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable -
                                objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "B":
                            {
                                // deduction reduces taxable and fica wage bases

                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "C":
                            {
                                //deduction reduces taxable and futa wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "D":
                            {
                                // deduction reduces futa and fica wage bases
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "F":
                            {
                                // deduction reduces fica wage base only
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "T":
                            {
                                // deduction reduces taxable wage base only
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                break;
                            }
                        case "U":
                            {
                                // deduction reduces futa wage base only
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }

                    }
                }
                else
                {
                    objPayrolldeductionsglobal[j].dedflag = false;
                }


            }
            // calculate deductions that do not reduce taxable income
            for (int k = 0; k < objPayrolldeductionsglobal.Count; k++)
            {
                if (objPayrolldeductionsglobal[k].dedflag == false)
                {
                    CalculateDeductions(ref objProcess_PayEmployee, k, ref objPayrolldeductionsglobal,
                        ref objstycntrcList,
                        ref objListemplforprocess,
                        ref objDVOPayrollProcess_PayEmployeeList,
                        ref objpaydatasearch,
                        ref dup_ssn,
                        ref empl_ssn,
                        ref same_person,
                        ref fica_wages,
                        ref futa_wages,
                        ref currentempnoid,
                        ref year_is_current,
                        ref ErrMsg1,
                        ref ErrMsg2);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[k].amount;
                }
            }

            // calculate obligations
            for (int k = 0; k < objPayrollobligationsglobal.Count; k++)
            {
                CalculateObligations(ref objProcess_PayEmployee, k,
                    ref objPayrollobligationsglobal,
                    ref objPayrolldeductionsglobal,
                    ref objstycntrcList,
                    ref dup_ssn,
                    ref empl_ssn,
                    ref same_person);
            }
            //add final totals
            //adjust totals using overall deduction accumulation instead
            //of running net to take care of possible rounding errors
            objProcess_PayEmployee.cash_amount = objProcess_PayEmployee.inc_gross +
           objProcess_PayEmployee.inc_expense - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_fedtax + objProcess_PayEmployee.ded_medicare +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax +
           objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_medicare + objProcess_PayEmployee.ded_fedtax +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax + objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.obl_total = objProcess_PayEmployee.obl_futa + objProcess_PayEmployee.obl_fica +
            objProcess_PayEmployee.obl_medicare + objProcess_PayEmployee.obl_other;

            return objProcess_PayEmployee;
        }

        public DVOPayrollProcess_PayEmployee CalculateIncomes(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i,
            ref List<DVOPayrollstypayid> objPayrollIncomesglobal,
            ref decimal fica_wages,
            ref decimal futa_wages)
        {
            //this function recalculates the income amount and resets the gross wages.

            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            //recalculate the amount
            objPayrollIncomesglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", (objPayrollIncomesglobal[i].inc_rate * objPayrollIncomesglobal[i].number)));

            // make sure amount is not null and non-negative
            if (objPayrollIncomesglobal[i].amount < 0)
            {
                objPayrollIncomesglobal[i].amount = 0.0M;
            }

            //add to the gross wages or expenses/advances
            switch (objPayrollIncomesglobal[i].inc_type)
            {
                case "H":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "E":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = objpayrollProcess_PayEmployee.inc_expense + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                case "A":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = objpayrollProcess_PayEmployee.inc_expense + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                case "F":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "U":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "B":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                default:
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
            }

            objpayrollProcess_PayEmployee.total_hours = objpayrollProcess_PayEmployee.total_hours + objPayrollIncomesglobal[i].hours;

            return objpayrollProcess_PayEmployee;
        }

        public static void CalculateIncomeChanges(string IncomeCodeType, decimal IncomeAmount, out decimal GrossIncomeChange, out decimal TaxableIncomeChange, out decimal FicaWagesChange, out decimal FutaWagesChange)
        {
            GrossIncomeChange = 0;
            FicaWagesChange = 0;
            FutaWagesChange = 0;
            TaxableIncomeChange = 0;

            switch (IncomeCodeType)
            {
                case "H":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "E":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "A":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "F":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        break;
                    }
                case "U":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "B":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                default:
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
            }
        }

        public DVOPayrollProcess_PayEmployee CalculateDeductions(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i,
            ref List<DVOPayrollstypaydd> objPayrolldeductionsglobal,
            ref List<DVOUpdatePayDefaults> objstycntrcList,
            ref List<DVOPayrollautopay> objListemplforprocess,
            ref List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList,
            ref DVOPayrollautopay objpaydatasearch,
            ref Int32 dup_ssn,
            ref string empl_ssn,
            ref bool same_person,
            ref decimal fica_wages,
            ref decimal futa_wages,
            ref Int32 currentempnoid,
            ref bool year_is_current,
            ref string ErrMsg1,
            ref string ErrMsg2)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_deductions = 0.0M;
            decimal currentdeductions = 0.0M;

            if (objPayrolldeductionsglobal[i].ded_type == null)
            {
                objPayrolldeductionsglobal[i].ded_type = "T";
            }
            //if (objPayrolldeductionsglobal[i].ded_type == null)
            //{
            //    objPayrolldeductionsglobal[i].ded_type = "T";
            //}
            //if (objPayrolldeductionsglobal[i].ded_type == null)
            //{
            //    objPayrolldeductionsglobal[i].ded_type = "T";
            //}
            // set the maximum deduction
            if (objPayrolldeductionsglobal[i].ded_limit == 0.0M)
            {
                Max_deductions = Convert.ToDecimal(999999999999.99);  // large decimal(12) value
            }
            else
            {
                objPayrolldeductionsglobal[i].ded_ytd = 0.0M;
                currentdeductions = 0;
                if (dup_ssn == 1)
                {
                    object[] parameter1 = new object[2];
                    parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    parameter1[1] = objPayrolldeductionsglobal[i].ded_code;
                    object ded_ytd = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrolldeductionsglobal[0].FIND_stypayddytd);
                    if (ded_ytd.ToString().Trim() != "")
                    {
                        objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd);
                    }

                }
                else
                {
                    //currentdeductions;
                    object[] parameter1 = new object[2];
                    parameter1[0] = empl_ssn;
                    parameter1[1] = objPayrolldeductionsglobal[i].ded_code;
                    object ded_ytd = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrolldeductionsglobal[0].FIND_stypayddytd1);
                    if (ded_ytd.ToString().Trim() != "")
                    {
                        objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd);
                    }

                    if (same_person)
                    {
                        object[] parameter2 = new object[3];
                        parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                        parameter1[1] = empl_ssn;
                        parameter1[2] = objPayrolldeductionsglobal[i].ded_code;
                        object ded_ytd1 = objDalBaseClass.ExecuteScalar(ref parameter2, objPayrolldeductionsglobal[0].FIND_stypayddytd2);
                        if (ded_ytd1.ToString().Trim() != "")
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd1);
                        }
                    }
                }


                if (currentdeductions != 0)
                {
                    objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + currentdeductions;
                }
                // get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrolldeductionsglobal[j].ded_code == objPayrolldeductionsglobal[i].ded_code)
                    {
                        if (objPayrolldeductionsglobal[j].amount != 0)
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + objPayrolldeductionsglobal[j].amount;
                        }
                    }
                }
                Max_deductions = (objPayrolldeductionsglobal[i].ded_limit - objPayrolldeductionsglobal[i].ded_ytd) ?? 0;//make nullable decimal By Rahul
            }
            if (Max_deductions < 0.0M)
            {
                Max_deductions = 0.0M;
            }
            //calc the amount of the deduction relative to type
            if (objPayrolldeductionsglobal[i].ded_code.Trim() == objpayrollProcess_PayEmployee.StateTaxCode.Trim())
            {
                //call the state tax calculation logic
                objPayrolldeductionsglobal[i].amount = state_calc(i, ref objPayrolldeductionsglobal, ref objDVOPayrollProcess_PayEmployeeList, ref objListemplforprocess,
                    ref objpaydatasearch,
                    ref fica_wages, ref futa_wages,
                    ref currentempnoid, ref ErrMsg1, ref ErrMsg2, ref year_is_current);
            }
            switch (objPayrolldeductionsglobal[i].ded_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != 0))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_gross));
                            }
                        }
                        else
                        {
                            //make nullable decimal By Rahul
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod,
                                ref objPayrolldeductionsglobal,
                                ref objListemplforprocess,
                                ref objpaydatasearch,
                                ref currentempnoid,
                                ref year_is_current,
                                ref ErrMsg1,
                                ref ErrMsg2);
                        }
                        break;
                    }

                case "T":
                    {
                        //calculate amount using taxable wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != Convert.ToDecimal(null)))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_taxable));
                            }
                        }
                        else
                        {
                            //make nullable decimal By Rahul
                            objPayrolldeductionsglobal[i].amount =
                                ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod,
                                    ref objPayrolldeductionsglobal,
                                    ref objListemplforprocess,
                                    ref objpaydatasearch,
                                    ref currentempnoid,
                                    ref year_is_current,
                                    ref ErrMsg1,
                                    ref ErrMsg2);
                            //ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable, objListemplforprocess[currentempnoid].PayPeriod);
                        }
                        break;
                    }
                case "U":
                    {
                        //calculate amount using futa wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * futa_wages));
                        }
                        break;
                    }
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * fica_wages));
                        }
                        break;
                    }
                case "H":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.total_hours));
                        break;
                    }
                case "N":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        break;
                    }
                default:
                    {
                        objPayrolldeductionsglobal[i].amount = 0.0M;
                        break;
                    }

            }


            //make sure deduction is not greater than net or zero if net < 0
            if (objPayrolldeductionsglobal[i].amount > 0)
            {
                if (objpayrollProcess_PayEmployee.inc_net < 0)
                {
                    objPayrolldeductionsglobal[i].amount = 0;
                }
                else
                {
                    if (objPayrolldeductionsglobal[i].amount > objpayrollProcess_PayEmployee.inc_net)
                    {
                        objPayrolldeductionsglobal[i].amount = objpayrollProcess_PayEmployee.inc_net;
                    }
                }
            }

            if (objPayrolldeductionsglobal[i].pay_limit != 0.0M)
            {
                if (objPayrolldeductionsglobal[i].amount > objPayrolldeductionsglobal[i].pay_limit)
                {
                    objPayrolldeductionsglobal[i].amount = objPayrolldeductionsglobal[i].pay_limit;
                }
            }
            // check for limit
            if (objPayrolldeductionsglobal[i].amount > Max_deductions)
            {
                objPayrolldeductionsglobal[i].amount = Max_deductions;
            }

            // post amount to correct total
            if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fedtax_code)
            {
                objpayrollProcess_PayEmployee.ded_fedtax = objpayrollProcess_PayEmployee.ded_fedtax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].statax_code)
            {
                objpayrollProcess_PayEmployee.ded_statax = objpayrollProcess_PayEmployee.ded_statax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].loctax_code)
            {
                objpayrollProcess_PayEmployee.ded_loctax = objpayrollProcess_PayEmployee.ded_loctax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.ded_fica = objpayrollProcess_PayEmployee.ded_fica + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].medicare_code)
            {
                objpayrollProcess_PayEmployee.ded_medicare = objpayrollProcess_PayEmployee.ded_medicare + objPayrolldeductionsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.ded_other = objpayrollProcess_PayEmployee.ded_other + objPayrolldeductionsglobal[i].amount;
            }



            return objpayrollProcess_PayEmployee;
        }

        public DVOPayrollProcess_PayEmployee CalculateObligations(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i,
            ref List<DVOPayrollStypayod> objPayrollobligationsglobal,
            ref List<DVOPayrollstypaydd> objPayrolldeductionsglobal,
            ref List<DVOUpdatePayDefaults> objstycntrcList,
            ref Int32 dup_ssn,
            ref string empl_ssn,
            ref bool same_person)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_Obligations = 0.0M;
            decimal Current_obligations = 0.0M;
            decimal obligationYTD = 0.0M;
            decimal Max_deductions = 0.0M;
            // this function calculates the obligation amount and updates
            //   cumulative totals for the control obligation codes

            // set obligation type
            if (objPayrollobligationsglobal[i].obl_type == null)
            {
                objPayrollobligationsglobal[i].obl_type = "T";
            }

            // get obligation limit and accrual

            // set the maximum obligation
            if (objPayrollobligationsglobal[i].obl_limit == 0.0M)
            {
                Max_deductions = 999999999.99M;  // large decimal(12) value
            }
            else
            {
                //check for current accrual
                //Current_obligations = 0.0M;
                if (dup_ssn == 1)
                {
                    //obligationYTD = 0.0M; //Set it with Year to date obligations
                    object[] parameter1 = new object[2];
                    parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    object Current_obl = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd);
                    if (Current_obl.ToString().Trim() != "")
                    {
                        objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obl);
                    }
                }
                else
                {
                    //obligationYTD = 0.0M; //Set it with Year to date obligations
                    object[] parameter1 = new object[2];
                    parameter1[0] = empl_ssn;
                    parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    object oblYTD = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd1);
                    if (oblYTD.ToString().Trim() != "")
                    {
                        objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(oblYTD);
                    }

                    if (same_person)
                    {
                        //Current_obligations = 0.0M; //set it with current obligations 
                        object[] parameter2 = new object[3];
                        parameter2[0] = objpayrollProcess_PayEmployee.EmplCode;
                        parameter2[1] = empl_ssn;
                        parameter2[2] = objPayrollobligationsglobal[i].obl_code;
                        object Current_obligations1 = objDalBaseClass.ExecuteScalar(ref parameter2, objPayrollobligationsglobal[0].FIND_stypayodytd2);
                        if (Current_obligations1.ToString().Trim() != "")
                        {
                            objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obligations1);
                        }

                    }
                }

                if (Current_obligations != 0)
                {
                    objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + Current_obligations;//objPayrollobligationsglobal[i].amount;
                }
                //get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrollobligationsglobal[j].obl_code == objPayrollobligationsglobal[i].obl_code)
                    {
                        if (objPayrollobligationsglobal[j].amount != 0.0M)
                        {
                            objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + objPayrollobligationsglobal[j].amount;
                        }
                    }

                }
                Max_Obligations = (objPayrollobligationsglobal[i].obl_limit - objPayrollobligationsglobal[i].obl_ytd) ?? 0;
            }
            if (Max_Obligations < 0)
            {
                Max_Obligations = 0.0M;
            }
            switch (objPayrollobligationsglobal[i].obl_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_gross));
                        }
                    }
                    break;
                case "T":
                    {
                        //calculate amount using taxable wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_taxable));
                        }
                    }
                    break;
                case "U":
                    //calculate amount using futa wage base
                    {
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_futa));
                        }
                    }
                    break;
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_fica));
                        }
                    }
                    break;
                case "E":
                    // Based on the employees deduction amount
                    {
                        for (int j = 0; j <= objPayrolldeductionsglobal.Count; j++)
                        {
                            if (objPayrollobligationsglobal[i].obl_code == objPayrolldeductionsglobal[j].ded_code)
                            {
                                objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objPayrolldeductionsglobal[j].amount));
                                break;
                            }
                        }
                    }
                    break;
                case "H":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.total_hours));
                    }
                    break;
                case "N":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                    }
                    break;
                    //default:
                    //    {
                    //        objPayrollobligationsglobal[i].amount = 0.0M;
                    //    }
            }

            //Modified by Sarvjeet On 13/08/2009
            if (objPayrollobligationsglobal[i].pay_limit > 0.0M)
            {

                if (objPayrollobligationsglobal[i].amount > objPayrollobligationsglobal[i].pay_limit)
                {

                    objPayrollobligationsglobal[i].amount = objPayrollobligationsglobal[i].pay_limit;

                }

            }
            //check for limit
            if (objPayrollobligationsglobal[i].amount > Max_Obligations)
            {
                objPayrollobligationsglobal[i].amount = Max_Obligations;
            }
            // post amount to correct total
            if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].futa_code)
            {
                objpayrollProcess_PayEmployee.obl_futa = objpayrollProcess_PayEmployee.obl_futa + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.obl_fica = objpayrollProcess_PayEmployee.obl_fica + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].medicare_ob_code)
            {
                objpayrollProcess_PayEmployee.obl_medicare = objpayrollProcess_PayEmployee.obl_medicare + objPayrollobligationsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.obl_other = objpayrollProcess_PayEmployee.obl_other + objPayrollobligationsglobal[i].amount;
            }

            //   DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            return objpayrollProcess_PayEmployee;
        }

        public static List<DVOPayrollstypayid> LoadIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOUpdateTimeCard> objlistTimecard = new List<DVOUpdateTimeCard>();
            objlistTimecard = LoadTimeCardIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            List<DVOPayrollstypayid> objlistpayrollincomes = new List<DVOPayrollstypayid>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            Int32 Maxlineno = 0;
            if (objlistTimecard.Count < 1)
            {
                objlistTimecard = LoadEmployeeIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            }
            for (int i = 0; i < objlistTimecard.Count; i++)
            {
                DVOPayrollstypayid objemployeeincome = new DVOPayrollstypayid();
                objemployeeincome.inc_code = objlistTimecard[i].inc_code_id;
                objemployeeincome.inc_rate = objlistTimecard[i].inc_rate_id;
                objemployeeincome.number = objlistTimecard[i].inc_number_id;
                objemployeeincome.hours = objlistTimecard[i].inc_hours_id;
                objemployeeincome.add_code = objlistTimecard[i].add_code_cr;
                objemployeeincome.inc_type = objlistTimecard[i].inc_type_cr;
                if ((objemployeeincome.add_code == "Y") || (objemployeeincome.add_code == "Z"))
                {
                    parameters[0] = EmployeeCode;

                    object Maxlineno1 = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOPayrollautopay()).EMPLOYEEMAXLINENOGET);
                    if (Maxlineno1 == null)
                    {
                        Maxlineno = 0;

                    }
                    Maxlineno = Maxlineno + i;
                }
                else
                {
                    objemployeeincome.add_code = "N";
                    objemployeeincome.line_no = objlistTimecard[i].line_no_id;
                }
                objemployeeincome.lo_inc_amt = objlistTimecard[i].lo_inc_amt_id;
                objemployeeincome.hi_inc_amt = objlistTimecard[i].hi_inc_amt_id;
                // Take the timecard account number first.  Comment out the following
                // line here, but use it as a default if the timecard account number is null.
                // let p_ypayid[n].acct_no = inc_ref[n].acct_no
                objemployeeincome.acct_no = objlistTimecard[i].timecd_acct_no;
                objemployeeincome.Department = objlistTimecard[i].department_id;

                //Assign Default Values as required ........
                if (objemployeeincome.lo_inc_amt == 0.0M)
                {
                    objemployeeincome.lo_inc_amt = objlistTimecard[i].dflt_lo_inc_amt_cr;
                }
                if (objemployeeincome.hi_inc_amt == 0.0M)
                {
                    objemployeeincome.hi_inc_amt = objlistTimecard[i].dflt_hi_inc_amt_cr;
                }
                if (objemployeeincome.inc_rate == 0.0M)
                {
                    objemployeeincome.inc_rate = objlistTimecard[i].dflt_rate_cr;
                }
                if (objemployeeincome.hours == 0.0M)
                {
                    objemployeeincome.hours = objlistTimecard[i].dflt_hours_cr;
                }
                if (objemployeeincome.number == 0.0M)
                {
                    objemployeeincome.number = objlistTimecard[i].dflt_num_cr;
                }
                // use defaults if necessary

                if (objemployeeincome.acct_no == 0)
                {
                    objemployeeincome.acct_no = objlistTimecard[i].acct_no_id;
                    if (objemployeeincome.acct_no == 0)
                    {
                        objemployeeincome.acct_no = objlistTimecard[i].dflt_acct_cr;
                    }
                }
                if ((objemployeeincome.Department == string.Empty) || (objemployeeincome.Department == null))
                {
                    objemployeeincome.Department = objlistTimecard[i].dflt_dept_cr;
                }
                objlistpayrollincomes.Add(objemployeeincome);
            }
            return objlistpayrollincomes;
        }

        public List<DVOPayrollstypayid> LoadIncomes(ref List<DVOPayrollstypayid> objlistpayrollincomes, string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            //List<DVOUpdateTimeCard> objlistTimecard = new List<DVOUpdateTimeCard>();
            //objlistTimecard = LoadTimeCardIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            //List<DVOPayrollstypayid> objlistpayrollincomes = new List<DVOPayrollstypayid>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            //Int32 Maxlineno = 0;
            //if (objlistTimecard.Count < 1)
            //{
            //    objlistTimecard = LoadEmployeeIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            //}
            //for (int i = 0; i < objlistTimecard.Count; i++)
            foreach (DVOPayrollstypayid objemployeeincome in objlistpayrollincomes)
            {
                //DVOUpdateTimeCard objTimeCard = objlistTimecard.Find(delegate(DVOUpdateTimeCard objparm)
                //{
                //    return objparm.inc_code_id.Trim() == objemployeeincome.inc_code.Trim();
                //});
                //if (objTimeCard != null)
                //{

                DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();
                objDVOUpdateIncCode.inc_code = objemployeeincome.inc_code.Trim();
                List<DVOUpdateIncCode> listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);
                if (listDVOUpdateIncCode != null && listDVOUpdateIncCode.Count > 0)
                {
                    if (objemployeeincome.inc_rate <= 0)
                        objemployeeincome.inc_rate = listDVOUpdateIncCode[0].dflt_rate ?? 0;//make nullable decimal By Rahul
                    if (objemployeeincome.number <= 0)
                        objemployeeincome.number = listDVOUpdateIncCode[0].dflt_num ?? 0;//make nullable decimal By Rahul
                    if (objemployeeincome.hours <= 0)
                        objemployeeincome.hours = listDVOUpdateIncCode[0].dflt_hours ?? 0;//make nullable decimal By Rahul

                    //objemployeeincome.add_code = listDVOUpdateIncCode[0].add_code_cr;
                    objemployeeincome.inc_type = listDVOUpdateIncCode[0].inc_type;

                    //Assign Default Values as required ........
                    if (objemployeeincome.lo_inc_amt == 0.0M)
                    {
                        objemployeeincome.lo_inc_amt = listDVOUpdateIncCode[0].dflt_lo_inc_amt ?? 0;//make nullable decimal By Rahul
                    }
                    if (objemployeeincome.hi_inc_amt == 0.0M)
                    {
                        objemployeeincome.hi_inc_amt = listDVOUpdateIncCode[0].dflt_hi_inc_amt ?? 0;//make nullable decimal By Rahul
                    }
                    if (objemployeeincome.inc_rate == 0.0M)
                    {
                        objemployeeincome.inc_rate = listDVOUpdateIncCode[0].dflt_rate ?? 0;//make nullable decimal By Rahul
                    }
                    if (objemployeeincome.hours == 0.0M)
                    {
                        objemployeeincome.hours = listDVOUpdateIncCode[0].dflt_hours ?? 0;//make nullable decimal By Rahul
                    }
                    if (objemployeeincome.number == 0.0M)
                    {
                        objemployeeincome.number = listDVOUpdateIncCode[0].dflt_num ?? 0;//make nullable decimal By Rahul
                    }

                    // use defaults if necessary
                    if (objemployeeincome.acct_no == 0)
                    {
                        objemployeeincome.acct_no = listDVOUpdateIncCode[0].acct_no ?? 0;//make nullable decimal By Rahul
                        if (objemployeeincome.acct_no == 0)
                        {
                            objemployeeincome.acct_no = listDVOUpdateIncCode[0].dflt_acct ?? 0;//make nullable decimal By Rahul
                        }
                    }
                    if ((objemployeeincome.Department == string.Empty) || (objemployeeincome.Department == null))
                    {
                        objemployeeincome.Department = listDVOUpdateIncCode[0].dflt_dept;
                    }
                    //}
                    //DVOPayrollstypayid objemployeeincome = new DVOPayrollstypayid();
                    //objemployeeincome.inc_code = objlistTimecard[i].inc_code_id;

                    if ((objemployeeincome.add_code == "Y") || (objemployeeincome.add_code == "Z"))
                    {
                        //    parameters[0] = EmployeeCode;

                        //    object Maxlineno1 = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOPayrollautopay()).EMPLOYEEMAXLINENOGET);
                        //    if (Maxlineno1 == null)
                        //        Maxlineno = 0;

                        //    Maxlineno = Maxlineno + i;
                    }
                    else
                    {
                        objemployeeincome.add_code = "N";
                        //objemployeeincome.line_no = objlistTimecard[i].line_no_id;
                    }
                }
                ////objemployeeincome.lo_inc_amt = objlistTimecard[i].lo_inc_amt_id;
                ////objemployeeincome.hi_inc_amt = objlistTimecard[i].hi_inc_amt_id;

                // Take the timecard account number first.  Comment out the following
                // line here, but use it as a default if the timecard account number is null.
                // let p_ypayid[n].acct_no = inc_ref[n].acct_no
                ////objemployeeincome.acct_no = objlistTimecard[i].timecd_acct_no;
                ////objemployeeincome.Department = objlistTimecard[i].department_id;
                //objlistpayrollincomes.Add(objemployeeincome);
            }
            return objlistpayrollincomes;
        }

        public List<DVOPayrollstypaydd> LoadDeductions(string EmployeeCode, DateTime EOPdate, string FlexacctType, string FlexDepartment,
            ref DVOPayrollautopay objpaydatasearch)
        {
            List<DVOPayrollstypaydd> objListpayrollstypaydd = new List<DVOPayrollstypaydd>();
            List<DVOMasterEmployeeDeductions> objListemployeeDeddefaultdata = new List<DVOMasterEmployeeDeductions>();
            DVOPayrollstypaydd objpayrollstypaydd = new DVOPayrollstypaydd();
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Dedreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeDeductions), (new DVOMasterEmployeeDeductions()).EmpDedanddefaults))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOMasterEmployeeDeductions objemployeedefaultdedrec = new DVOMasterEmployeeDeductions();

                    objemployeedefaultdedrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dfltkeyvalue = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);

                    objemployeedefaultdedrec.ded_code = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                    objemployeedefaultdedrec.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objemployeedefaultdedrec.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
                    objemployeedefaultdedrec.ded_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
                    objemployeedefaultdedrec.pay_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                    objemployeedefaultdedrec.balanceamt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                    objemployeedefaultdedrec.ded_apply = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]).Trim() : string.Empty);
                    objemployeedefaultdedrec.lo_ded_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);
                    objemployeedefaultdedrec.hi_ded_amt = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0);
                    objemployeedefaultdedrec.acct_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
                    objemployeedefaultdedrec.department = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]).Trim() : "000");
                    objemployeedefaultdedrec.ded_ytd = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0);
                    objemployeedefaultdedrec.ded_date = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);

                    objemployeedefaultdedrec.description = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : string.Empty);
                    objemployeedefaultdedrec.ded_type = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]).Trim() : string.Empty);
                    objemployeedefaultdedrec.ded_taxred = (dr[17] != DBNull.Value ? Convert.ToString(dr[17]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dflt_rate = (dr[18] != DBNull.Value ? Convert.ToDecimal(dr[18]) : 0.0M);
                    objemployeedefaultdedrec.dflt_limit = (dr[19] != DBNull.Value ? Convert.ToDecimal(dr[19]) : 0);
                    objemployeedefaultdedrec.dflt_pay_limit = (dr[20] != DBNull.Value ? Convert.ToDecimal(dr[20]) : 0);
                    objemployeedefaultdedrec.yearrollover = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dflt_lo_ded_amt = (dr[22] != DBNull.Value ? Convert.ToDecimal(dr[22]) : 0);
                    objemployeedefaultdedrec.dflt_hi_ded_amt = (dr[23] != DBNull.Value ? Convert.ToDecimal(dr[23]) : 0);
                    objemployeedefaultdedrec.dflt_acct = (dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0);
                    objemployeedefaultdedrec.dflt_dept = (dr[25] != DBNull.Value ? Convert.ToString(dr[25]).Trim() : "000");
                    objemployeedefaultdedrec.dflt_apply = (dr[26] != DBNull.Value ? Convert.ToString(dr[26]).Trim() : string.Empty);

                    parameters = new object[2];
                    parameters[0] = objemployeedefaultdedrec.ded_code;
                    parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    object taxcode = objDalBaseClass.ExecuteScalar(ref parameters, objpaydatasearch.DeductionTaxCodeGet);
                    if (taxcode != DBNull.Value)
                    {
                        objemployeedefaultdedrec.tax_code = taxcode.ToString().Trim();
                    }

                    parameters = new object[2];
                    parameters[0] = objemployeedefaultdedrec.ded_code;
                    parameters[1] = EOPdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                    object result = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeDeductions()).DeductioncodeTaxget);
                    if (result != null)
                        objemployeedefaultdedrec.tax_code = result.ToString().Trim();
                    if (objemployeedefaultdedrec.ded_apply == string.Empty)
                    {
                        objemployeedefaultdedrec.ded_apply = objemployeedefaultdedrec.dflt_apply;
                    }
                    // assign defaults as required
                    if (objemployeedefaultdedrec.lo_ded_amt == 0)
                    {
                        objemployeedefaultdedrec.lo_ded_amt = objemployeedefaultdedrec.dflt_lo_ded_amt;
                    }
                    if (objemployeedefaultdedrec.hi_ded_amt == 0)
                    {
                        objemployeedefaultdedrec.hi_ded_amt = objemployeedefaultdedrec.dflt_hi_ded_amt;
                    }
                    if (objemployeedefaultdedrec.ded_rate == 0)
                    {
                        objemployeedefaultdedrec.ded_rate = objemployeedefaultdedrec.dflt_rate;
                    }
                    if (objemployeedefaultdedrec.acct_no == 0)
                    {
                        objemployeedefaultdedrec.acct_no = objemployeedefaultdedrec.dflt_acct;
                    }
                    if (objemployeedefaultdedrec.department == null)
                    {
                        objemployeedefaultdedrec.department = objemployeedefaultdedrec.dflt_dept;
                    }
                    if (objemployeedefaultdedrec.ded_limit == 0)
                    {
                        objemployeedefaultdedrec.ded_limit = objemployeedefaultdedrec.dflt_limit;
                    }


                    objListemployeeDeddefaultdata.Add(objemployeedefaultdedrec);
                }
            }
            Dedreccount = objListemployeeDeddefaultdata.Count;
            for (int i = 0; i < Dedreccount; i++)
            {
                objpayrollstypaydd = new DVOPayrollstypaydd();
                // check to make sure deduction should be taken now

                //if (objListemployeeDeddefaultdata[i].ded_apply != string.Empty)
                // {
                DateTime ded_date = Convert.ToDateTime(null);
                BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                if (objListemployeeDeddefaultdata[i].ded_date != string.Empty)
                {
                    ded_date = Convert.ToDateTime(objListemployeeDeddefaultdata[i].ded_date);
                }
                else
                {
                    ded_date = Convert.ToDateTime(null);
                }
                if (objpayrollfunctions.Pay_Frequency(objListemployeeDeddefaultdata[i].ded_apply, EOPdate, ded_date))
                {
                    objpayrollstypaydd.ded_rate = objListemployeeDeddefaultdata[i].ded_rate.GetValueOrDefault(0.0M);
                }
                else
                {
                    objListemployeeDeddefaultdata[i].ded_rate = 0.0M;
                }
                //  }
                objpayrollstypaydd.ded_code = objListemployeeDeddefaultdata[i].ded_code;
                objpayrollstypaydd.amount = 0;
                objpayrollstypaydd.lo_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].lo_ded_amt);
                objpayrollstypaydd.hi_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].hi_ded_amt);
                objpayrollstypaydd.ded_taxred = objListemployeeDeddefaultdata[i].ded_taxred;
                objpayrollstypaydd.acct_no = objListemployeeDeddefaultdata[i].acct_no;
                objpayrollstypaydd.Department = objListemployeeDeddefaultdata[i].department;
                objpayrollstypaydd.line_no = objListemployeeDeddefaultdata[i].line_no;
                objpayrollstypaydd.add_code = "N";
                objpayrollstypaydd.ded_type = objListemployeeDeddefaultdata[i].ded_type;
                if (objListemployeeDeddefaultdata[i].pay_limit == 0)
                {
                    objpayrollstypaydd.pay_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_pay_limit);
                }

                if (objListemployeeDeddefaultdata[i].yearrollover == "Y")
                {
                    if (objpayrollstypaydd.amount > Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt))
                    {
                        objpayrollstypaydd.amount = Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt);
                    }
                }
                if (objListemployeeDeddefaultdata[i].ded_limit == 0)
                {
                    objpayrollstypaydd.ded_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_limit);
                }
                if (objpayrollstypaydd.ded_ytd == 0.0M)
                {
                    objpayrollstypaydd.ded_ytd = Convert.ToDecimal(objListemployeeDeddefaultdata[i].ded_ytd);
                }
                if (objpayrollstypaydd.tax_code == string.Empty)
                {
                    objpayrollstypaydd.tax_code = objListemployeeDeddefaultdata[i].tax_code;
                }
                objListpayrollstypaydd.Add(objpayrollstypaydd);
            }

            return objListpayrollstypaydd;
        }

        public List<DVOPayrollstypaydd> LoadDeductions(ref List<DVOMasterEmployeeDeductions> objListemployeeDeddefaultdata,
            DateTime EOPdate, string FlexacctType, string FlexDepartment, ref DVOPayrollautopay objpaydatasearch)
        {
            List<DVOPayrollstypaydd> objListpayrollstypaydd = new List<DVOPayrollstypaydd>();
            //List<DVOMasterEmployeeDeductions> objListemployeeDeddefaultdata = new List<DVOMasterEmployeeDeductions>();
            DVOPayrollstypaydd objpayrollstypaydd = new DVOPayrollstypaydd();
            //string Mixkeyvalue;
            //string MixAcctType;
            //Int32 MixaccountNo;
            Int32 Dedreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            //parameters[0] = EmployeeCode;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeDeductions), (new DVOMasterEmployeeDeductions()).EmpDedanddefaults))
            //{
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            foreach (DVOMasterEmployeeDeductions objemployeedefaultdedrec in objListemployeeDeddefaultdata)
            {
                //objemployeedefaultdedrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                //objemployeedefaultdedrec.dfltkeyvalue = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);

                ////objemployeedefaultdedrec.ded_code = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                //objemployeedefaultdedrec.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                ////objemployeedefaultdedrec.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
                ////objemployeedefaultdedrec.ded_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
                ////objemployeedefaultdedrec.pay_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                //objemployeedefaultdedrec.balanceamt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                //objemployeedefaultdedrec.ded_apply = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]).Trim() : string.Empty);
                ////objemployeedefaultdedrec.lo_ded_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);
                ////objemployeedefaultdedrec.hi_ded_amt = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0);
                ////objemployeedefaultdedrec.acct_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
                //objemployeedefaultdedrec.department = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]).Trim() : "000");
                //objemployeedefaultdedrec.ded_ytd = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0);
                //objemployeedefaultdedrec.ded_date = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);

                //objemployeedefaultdedrec.description = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : string.Empty);
                //objemployeedefaultdedrec.ded_type = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]).Trim() : string.Empty);
                //objemployeedefaultdedrec.ded_taxred = (dr[17] != DBNull.Value ? Convert.ToString(dr[17]).Trim() : string.Empty);
                //objemployeedefaultdedrec.dflt_rate = (dr[18] != DBNull.Value ? Convert.ToDecimal(dr[18]) : 0.0M);
                //objemployeedefaultdedrec.dflt_limit = (dr[19] != DBNull.Value ? Convert.ToDecimal(dr[19]) : 0);
                //objemployeedefaultdedrec.dflt_pay_limit = (dr[20] != DBNull.Value ? Convert.ToDecimal(dr[20]) : 0);
                //objemployeedefaultdedrec.yearrollover = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : string.Empty);
                //objemployeedefaultdedrec.dflt_lo_ded_amt = (dr[22] != DBNull.Value ? Convert.ToDecimal(dr[22]) : 0);
                //objemployeedefaultdedrec.dflt_hi_ded_amt = (dr[23] != DBNull.Value ? Convert.ToDecimal(dr[23]) : 0);
                //objemployeedefaultdedrec.dflt_acct = (dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0);
                //objemployeedefaultdedrec.dflt_dept = (dr[25] != DBNull.Value ? Convert.ToString(dr[25]).Trim() : "000");
                //objemployeedefaultdedrec.dflt_apply = (dr[26] != DBNull.Value ? Convert.ToString(dr[26]).Trim() : string.Empty);

                parameters = new object[2];
                parameters[0] = objemployeedefaultdedrec.ded_code;
                parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                object taxcode = objDalBaseClass.ExecuteScalar(ref parameters, objpaydatasearch.DeductionTaxCodeGet);
                if (taxcode != DBNull.Value)
                    objemployeedefaultdedrec.tax_code = taxcode.ToString().Trim();

                parameters = new object[2];
                parameters[0] = objemployeedefaultdedrec.ded_code;
                parameters[1] = EOPdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                object result = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeDeductions()).DeductioncodeTaxget);
                if (result != null)
                    objemployeedefaultdedrec.tax_code = result.ToString().Trim();

                if (objemployeedefaultdedrec.ded_apply == string.Empty)
                {
                    objemployeedefaultdedrec.ded_apply = objemployeedefaultdedrec.dflt_apply;
                }
                // assign defaults as required
                if (objemployeedefaultdedrec.lo_ded_amt == 0)
                {
                    objemployeedefaultdedrec.lo_ded_amt = objemployeedefaultdedrec.dflt_lo_ded_amt;
                }
                if (objemployeedefaultdedrec.hi_ded_amt == 0)
                {
                    objemployeedefaultdedrec.hi_ded_amt = objemployeedefaultdedrec.dflt_hi_ded_amt;
                }
                if (objemployeedefaultdedrec.ded_rate == 0)
                {
                    objemployeedefaultdedrec.ded_rate = objemployeedefaultdedrec.dflt_rate;
                }
                if (objemployeedefaultdedrec.acct_no == 0)
                {
                    objemployeedefaultdedrec.acct_no = objemployeedefaultdedrec.dflt_acct;
                }
                if (objemployeedefaultdedrec.department == null)
                {
                    objemployeedefaultdedrec.department = objemployeedefaultdedrec.dflt_dept;
                }
                if (objemployeedefaultdedrec.ded_limit == 0)
                {
                    objemployeedefaultdedrec.ded_limit = objemployeedefaultdedrec.dflt_limit;
                }


                //objListemployeeDeddefaultdata.Add(objemployeedefaultdedrec);
            }
            //}
            Dedreccount = objListemployeeDeddefaultdata.Count;
            for (int i = 0; i < Dedreccount; i++)
            {
                objpayrollstypaydd = new DVOPayrollstypaydd();
                // check to make sure deduction should be taken now

                //if (objListemployeeDeddefaultdata[i].ded_apply != string.Empty)
                // {
                DateTime ded_date = Convert.ToDateTime(null);
                BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                if (objListemployeeDeddefaultdata[i].ded_date != string.Empty)
                {
                    ded_date = DateTime.ParseExact(objListemployeeDeddefaultdata[i].ded_date, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                }
                else
                {
                    ded_date = Convert.ToDateTime(null);
                }
                if (objpayrollfunctions.Pay_Frequency(objListemployeeDeddefaultdata[i].ded_apply, EOPdate, ded_date))
                {
                    objpayrollstypaydd.ded_rate = objListemployeeDeddefaultdata[i].ded_rate.GetValueOrDefault(0.0M);
                }
                else
                {
                    objListemployeeDeddefaultdata[i].ded_rate = 0.0M;
                }
                //  }
                objpayrollstypaydd.ded_code = objListemployeeDeddefaultdata[i].ded_code;
                objpayrollstypaydd.amount = 0;
                objpayrollstypaydd.lo_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].lo_ded_amt);
                objpayrollstypaydd.hi_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].hi_ded_amt);
                objpayrollstypaydd.ded_taxred = objListemployeeDeddefaultdata[i].ded_taxred;
                objpayrollstypaydd.acct_no = objListemployeeDeddefaultdata[i].acct_no;
                objpayrollstypaydd.Department = objListemployeeDeddefaultdata[i].department;
                objpayrollstypaydd.line_no = objListemployeeDeddefaultdata[i].line_no;
                objpayrollstypaydd.add_code = "N";
                objpayrollstypaydd.ded_type = objListemployeeDeddefaultdata[i].ded_type;
                if (objListemployeeDeddefaultdata[i].pay_limit == 0)
                {
                    objpayrollstypaydd.pay_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_pay_limit);
                }

                if (objListemployeeDeddefaultdata[i].yearrollover == "Y")
                {
                    if (objpayrollstypaydd.amount > Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt))
                    {
                        objpayrollstypaydd.amount = Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt);
                    }
                }
                if (objListemployeeDeddefaultdata[i].ded_limit == 0)
                {
                    objpayrollstypaydd.ded_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_limit);
                }
                if (objpayrollstypaydd.ded_ytd == 0.0M)
                {
                    objpayrollstypaydd.ded_ytd = Convert.ToDecimal(objListemployeeDeddefaultdata[i].ded_ytd);
                }
                if (objpayrollstypaydd.tax_code == string.Empty)
                {
                    objpayrollstypaydd.tax_code = objListemployeeDeddefaultdata[i].tax_code;
                }
                objListpayrollstypaydd.Add(objpayrollstypaydd);
            }

            return objListpayrollstypaydd;
        }

        public static List<DVOPayrollStypayod> LoadObligations(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollStypayod> objListpayrollstypayod = new List<DVOPayrollStypayod>();
            List<DVOMasterEmployeeObligations> objListemployeeObldefaultdate = new List<DVOMasterEmployeeObligations>();

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Oblreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeObligations), (new DVOMasterEmployeeObligations()).Emplobldefaultsget))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //MasterOblCodes.dfltaccounttype,MasterOblCodes.dfltbaccounttype,MasterOblCodes.dfltbkeyvalue,
                    //MasterEmployeeObligations.obl_code,MasterEmployeeObligations.line_no,MasterEmployeeObligations.obl_rate,MasterEmployeeObligations.obl_limit,
                    //MasterEmployeeObligations.pay_limit,MasterEmployeeObligations.acct_no,MasterEmployeeObligations.department,MasterEmployeeObligations.bal_acct_no,
                    //MasterEmployeeObligations.bal_dept,MasterEmployeeObligations.obl_ytd,MasterOblCodes.description,
                    //MasterOblCodes.obl_type,MasterOblCodes.dflt_rate,MasterOblCodes.dflt_limit,
                    //MasterOblCodes.dflt_pay_limit,  MasterOblCodes.dflt_acct, MasterOblCodes.dflt_dept,
                    //MasterOblCodes.dflt_bacct,MasterOblCodes.dflt_bdept

                    DVOMasterEmployeeObligations objemployeedefaultoblrec = new DVOMasterEmployeeObligations();
                    objemployeedefaultoblrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dfltbaccounttype = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dfltbkeyvalue = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                    objemployeedefaultoblrec.obl_code = (dr[3] != DBNull.Value ? Convert.ToString(dr[3]).Trim() : string.Empty);
                    objemployeedefaultoblrec.line_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objemployeedefaultoblrec.obl_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                    objemployeedefaultoblrec.obl_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                    objemployeedefaultoblrec.pay_limit = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                    objemployeedefaultoblrec.acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                    objemployeedefaultoblrec.department = (dr[9] != DBNull.Value ? Convert.ToString(dr[9]).Trim() : "000");
                    objemployeedefaultoblrec.bal_acct_no = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                    objemployeedefaultoblrec.bal_dept = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                    objemployeedefaultoblrec.obl_ytd = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);
                    objemployeedefaultoblrec.description = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]).Trim() : string.Empty);
                    objemployeedefaultoblrec.obl_type = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dflt_rate = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objemployeedefaultoblrec.dflt_limit = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0);
                    objemployeedefaultoblrec.dflt_pay_limit = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0);
                    objemployeedefaultoblrec.dflt_acct = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                    objemployeedefaultoblrec.dflt_dept = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                    objemployeedefaultoblrec.dflt_bacct = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);
                    objemployeedefaultoblrec.dflt_bdept = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : "000");
                    DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                    objflexsegcommon.AccountType = objemployeedefaultoblrec.dfltaccounttype;
                    objflexsegcommon.EntityType = "MasterOblCodes";
                    objflexsegcommon.Code = objemployeedefaultoblrec.obl_code;
                    objemployeedefaultoblrec.dfltbkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    objpayrollfunctions.flxMix(objemployeedefaultoblrec.dfltaccounttype, objemployeedefaultoblrec.dfltbkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                    objemployeedefaultoblrec.acct_no = MixaccountNo;
                    // objemployeedefaultoblrec.dflt_acct
                    if ((objemployeedefaultoblrec.dflt_acct == 0) &&
                     (objemployeedefaultoblrec.acct_no == 0))
                    {
                        //Test if the Account Exists in the table or not with th ekeyvalue we got now .
                        // scratch will contain the description after testFlexAccountKey
                        //create PayrollGLAccounts Flex account Entry
                    }
                    objListemployeeObldefaultdate.Add(objemployeedefaultoblrec);
                }

            }
            Oblreccount = objListemployeeObldefaultdate.Count;
            for (Int32 i = 0; i < Oblreccount; i++)
            {
                DVOPayrollStypayod objpayrollstypayod = new DVOPayrollStypayod();
                objpayrollstypayod.obl_code = objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].obl_rate;
                objpayrollstypayod.amount = 0;//objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].acct_no;
                objpayrollstypayod.Department = objListemployeeObldefaultdate[i].department;
                objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].bal_acct_no;
                objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].bal_dept;
                objpayrollstypayod.line_no = objListemployeeObldefaultdate[i].line_no;
                objpayrollstypayod.add_code = "N";
                objpayrollstypayod.obl_type = objListemployeeObldefaultdate[i].obl_type;
                // assign defaults as required

                if (objpayrollstypayod.obl_rate == 0.0M)
                {
                    objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].dflt_rate;
                }
                if (objpayrollstypayod.acct_no == 0)
                {
                    objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].dflt_acct;
                }
                if ((objpayrollstypayod.Department == "000") || (objpayrollstypayod.Department == null))
                {
                    objpayrollstypayod.Department = objListemployeeObldefaultdate[i].dflt_dept;
                }
                if (objpayrollstypayod.bal_acct_no == 0)
                {
                    objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].dflt_bacct;
                }
                if ((objpayrollstypayod.bal_dept == "000") || (objpayrollstypayod.bal_dept == null))
                {
                    objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].dflt_bdept;
                }
                if (objListemployeeObldefaultdate[i].pay_limit == 0)
                {
                    objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_pay_limit);
                }
                else
                {
                    objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].pay_limit);
                }
                if (objListemployeeObldefaultdate[i].obl_limit == 0)
                {
                    objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_limit);
                }
                else
                {
                    objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].obl_limit);
                }
                objListpayrollstypayod.Add(objpayrollstypayod);

            }



            return objListpayrollstypayod;
        }

        public List<DVOPayrollStypayod> LoadObligations(ref List<DVOMasterEmployeeObligations> objListemployeeObldefaultdate,
            DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollStypayod> objListpayrollstypayod = new List<DVOPayrollStypayod>();
            //List<DVOMasterEmployeeObligations> objListemployeeObldefaultdate = new List<DVOMasterEmployeeObligations>();

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Oblreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            //parameters[0] = EmployeeCode;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeObligations), (new DVOMasterEmployeeObligations()).Emplobldefaultsget))
            //{
            //foreach (DataRow dr in ds.Tables[0].Rows)
            foreach (DVOMasterEmployeeObligations objemployeedefaultoblrec in objListemployeeObldefaultdate)
            {
                //objemployeedefaultoblrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                //objemployeedefaultoblrec.dfltbaccounttype = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                //objemployeedefaultoblrec.dfltbkeyvalue = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);

                ////objemployeedefaultoblrec.obl_code = (dr[3] != DBNull.Value ? Convert.ToString(dr[3]).Trim() : string.Empty);
                //objemployeedefaultoblrec.line_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                ////objemployeedefaultoblrec.obl_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                ////objemployeedefaultoblrec.obl_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                ////objemployeedefaultoblrec.pay_limit = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                ////objemployeedefaultoblrec.acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                ////objemployeedefaultoblrec.department = (dr[9] != DBNull.Value ? Convert.ToString(dr[9]).Trim() : "000");
                ////objemployeedefaultoblrec.bal_acct_no = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                ////objemployeedefaultoblrec.bal_dept = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                //objemployeedefaultoblrec.obl_ytd = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);

                //objemployeedefaultoblrec.description = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]).Trim() : string.Empty);
                //objemployeedefaultoblrec.obl_type = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                //objemployeedefaultoblrec.dflt_rate = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                //objemployeedefaultoblrec.dflt_limit = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0);
                //objemployeedefaultoblrec.dflt_pay_limit = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0);
                //objemployeedefaultoblrec.dflt_acct = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                //objemployeedefaultoblrec.dflt_dept = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                //objemployeedefaultoblrec.dflt_bacct = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);
                //objemployeedefaultoblrec.dflt_bdept = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : "000");

                DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                objflexsegcommon.AccountType = objemployeedefaultoblrec.dfltaccounttype;
                objflexsegcommon.EntityType = "MasterOblCodes";
                objflexsegcommon.Code = objemployeedefaultoblrec.obl_code;
                objemployeedefaultoblrec.dfltbkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                objpayrollfunctions.flxMix(objemployeedefaultoblrec.dfltaccounttype, objemployeedefaultoblrec.dfltbkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                objemployeedefaultoblrec.acct_no = MixaccountNo;
                // objemployeedefaultoblrec.dflt_acct
                if ((objemployeedefaultoblrec.dflt_acct == 0) &&
                 (objemployeedefaultoblrec.acct_no == 0))
                {
                    //Test if the Account Exists in the table or not with th ekeyvalue we got now .
                    // scratch will contain the description after testFlexAccountKey
                    //create PayrollGLAccounts Flex account Entry
                }
                //objListemployeeObldefaultdate.Add(objemployeedefaultoblrec);
            }
            //}
            Oblreccount = objListemployeeObldefaultdate.Count;
            for (Int32 i = 0; i < Oblreccount; i++)
            {
                DVOPayrollStypayod objpayrollstypayod = new DVOPayrollStypayod();
                objpayrollstypayod.obl_code = objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].obl_rate;
                objpayrollstypayod.amount = 0;//objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].acct_no;
                objpayrollstypayod.Department = objListemployeeObldefaultdate[i].department;
                objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].bal_acct_no;
                objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].bal_dept;
                objpayrollstypayod.line_no = objListemployeeObldefaultdate[i].line_no;
                objpayrollstypayod.add_code = "N";
                objpayrollstypayod.obl_type = objListemployeeObldefaultdate[i].obl_type;
                // assign defaults as required

                if (objpayrollstypayod.obl_rate == 0.0M)
                {
                    objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].dflt_rate;
                }
                if (objpayrollstypayod.acct_no == 0)
                {
                    objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].dflt_acct;
                }
                if ((objpayrollstypayod.Department == "000") || (objpayrollstypayod.Department == null))
                {
                    objpayrollstypayod.Department = objListemployeeObldefaultdate[i].dflt_dept;
                }
                if (objpayrollstypayod.bal_acct_no == 0)
                {
                    objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].dflt_bacct;
                }
                if ((objpayrollstypayod.bal_dept == "000") || (objpayrollstypayod.bal_dept == null))
                {
                    objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].dflt_bdept;
                }
                if (objListemployeeObldefaultdate[i].pay_limit == 0)
                {
                    objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_pay_limit);
                }
                else
                {
                    objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].pay_limit);
                }
                if (objListemployeeObldefaultdate[i].obl_limit == 0)
                {
                    objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_limit);
                }
                else
                {
                    objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].obl_limit);
                }
                objListpayrollstypayod.Add(objpayrollstypayod);

            }
            return objListpayrollstypayod;
        }

        public static List<DVOUpdateTimeCard> LoadTimeCardIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[2];
            parameters[0] = EmployeeCode;
            parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), (new DVOUpdateTimeCard()).FIND_DETAILS_FORAUTOPAY))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                    objUpdatetimecardincome.dfltkeyvalue = "";
                    objUpdatetimecardincome.acct_no_id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                    objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                    objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
                    objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
                    objUpdatetimecardincome.description_cr = dr[8].ToString().Trim();
                    objUpdatetimecardincome.dflt_num_cr = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
                    objUpdatetimecardincome.dflt_rate_cr = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0.0M);
                    objUpdatetimecardincome.dflt_hours_cr = (dr[11] != DBNull.Value ? Convert.ToDecimal(dr[11]) : 0.0M);
                    objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                    objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objUpdatetimecardincome.dflt_acct_cr = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
                    objUpdatetimecardincome.dflt_dept_cr = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : "000");
                    objUpdatetimecardincome.inc_type_cr = dr[16].ToString().Trim();

                    DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                    objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                    objflexsegcommon.EntityType = "MasterIncCodes";
                    objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                    objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                    objUpdatetimecardincome.dflt_acct_cr = MixaccountNo;
                    parameters = new object[3];
                    parameters[0] = EmployeeCode;
                    parameters[1] = objUpdatetimecardincome.inc_code_id;
                    parameters[2] = objUpdatetimecardincome.line_no_id;
                    DataSet dst = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeIncomes), (new DVOMasterEmployeeIncomes()).EMPLOYEEINCOME_AUTOPAY);
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drt in dst.Tables[0].Rows)
                        {
                            objUpdatetimecardincome.lo_inc_amt_id = (drt[0] != DBNull.Value ? Convert.ToDecimal(drt[0]) : 0.0M); ;
                            objUpdatetimecardincome.hi_inc_amt_id = (drt[1] != DBNull.Value ? Convert.ToDecimal(drt[1]) : 0.0M); ;
                            objUpdatetimecardincome.acct_no_id = (drt[2] != DBNull.Value ? Convert.ToInt32(drt[2]) : 0);
                            objUpdatetimecardincome.department_id = (drt[3] != DBNull.Value ? Convert.ToString(drt[3]).Trim() : "000"); ;
                        }

                        if (((objUpdatetimecardincome.timecd_acct_no == 0) || (objUpdatetimecardincome.timecd_acct_no == null)) &&
                        ((objUpdatetimecardincome.acct_no_id == 0) || (objUpdatetimecardincome.acct_no_id == null)) &&
                       ((objUpdatetimecardincome.dflt_acct_cr == 0) || (objUpdatetimecardincome.dflt_acct_cr == null)))
                        {
                            //test and create flex key records 

                        }
                        objUpdatetimecardincome.add_code_cr = "N";
                    }
                    else
                    {
                        // does the code already exist at the employee level?

                        parameters = new object[2];
                        parameters[0] = objUpdatetimecardincome.inc_code_id;
                        parameters[1] = EmployeeCode;
                        dupempinccount = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeIncomes()).EMPLINCOMECNT_AUTOPAY));
                        if (dupempinccount >= 1)
                        {
                            objUpdatetimecardincome.add_code_cr = "Z";
                        }
                        else
                        {
                            objUpdatetimecardincome.add_code_cr = "Y";
                        }
                    }
                    if (lastcard == objUpdatetimecardincome.card_no)
                    {

                    }
                    else
                    {
                        lastcard = objUpdatetimecardincome.card_no;
                        newRec = true;
                        //   objListtimecard.Add(objUpdatetimecardincome);
                    }

                    Timecarddetail = objListtimecard.Count;
                    for (int i = 0; i < Timecarddetail; i++)
                    {
                        if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                            (objUpdatetimecardincome.inc_rate_id == objListtimecard[i].inc_rate_id))
                        {
                            objListtimecard[i].inc_hours_id = objListtimecard[i].inc_hours_id + objUpdatetimecardincome.inc_hours_id;
                            objListtimecard[i].inc_number_id = objListtimecard[i].inc_number_id + objUpdatetimecardincome.inc_number_id;
                            newRec = false;
                            break;
                        }
                        else
                        {
                            // If the code is the same as the employee entry
                            // but the rate differs we do not want to append
                            if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                              (objUpdatetimecardincome.line_no_id == objListtimecard[i].line_no_id))
                            {
                                if ((objUpdatetimecardincome.inc_number_id == 0) && (objListtimecard[i].inc_number_id != 0))
                                {
                                    objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                    objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                    newRec = false;
                                    break;
                                }
                                else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id != 0))
                                {
                                    objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                    objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                    objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                    newRec = false;
                                    break;
                                }
                                else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id == 0))
                                {
                                    objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                    objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                    objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                    newRec = false;
                                    break;
                                }
                                else
                                {
                                    //This code is a duplicate but the rate differs
                                    // and the number is not zero.
                                    objUpdatetimecardincome.add_code_cr = "Z";
                                    // newRec = false;
                                }

                            }
                        }
                    }
                    if (newRec == true)
                    {
                        objListtimecard.Add(objUpdatetimecardincome);
                        objUpdatetimecardincome = new DVOUpdateTimeCard();
                    }

                }
            }
            // If duplicate codes were added on the fly to the timecard, we
            // only want to append one to the employee entry at posting time.
            Timecarddetail = objListtimecard.Count;
            for (int j = 0; j < Timecarddetail; j++)
            {
                if (objListtimecard[j].add_code_cr == "Y")
                {
                    for (int k = 0; k < Timecarddetail; k++)
                    {
                        if (j == k)
                        {
                        }
                        else
                        {
                            if (objListtimecard[j].inc_code_id == objListtimecard[k].inc_code_id)
                            {
                                objListtimecard[j].add_code_cr = "Z";
                            }
                        }
                    }
                }
            }
            return objListtimecard;
        }

        public static List<DVOUpdateTimeCard> LoadEmployeeIncomes(string EmployeeCode, DateTime Eopdate, string FlexacctType, string FlexDepartment)
        {
            //This function loads the default income data from the employee
            //reference tables into an income reference array and reset the array
            // count.

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixAcctNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollautopay), (new DVOPayrollautopay()).EmployeeIncomerefer))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
                    objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                    objUpdatetimecardincome.dfltkeyvalue = "";
                    objUpdatetimecardincome.timecd_acct_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                    objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                    objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
                    objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
                    objUpdatetimecardincome.lo_inc_amt_id = (dr[8] != DBNull.Value ? Convert.ToDecimal(dr[8]) : 0.0M);
                    objUpdatetimecardincome.hi_inc_amt_id = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
                    objUpdatetimecardincome.acct_no_id = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                    objUpdatetimecardincome.department_id = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                    objUpdatetimecardincome.description_cr = dr[12].ToString().Trim();
                    objUpdatetimecardincome.dflt_num_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objUpdatetimecardincome.dflt_rate_cr = (dr[14] != DBNull.Value ? Convert.ToDecimal(dr[14]) : 0.0M);
                    objUpdatetimecardincome.dflt_hours_cr = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                    objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0.0M);
                    objUpdatetimecardincome.dflt_acct_cr = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                    objUpdatetimecardincome.dflt_dept_cr = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                    objUpdatetimecardincome.inc_type_cr = dr[20].ToString().Trim();
                    DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                    objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                    objflexsegcommon.EntityType = "MasterIncCodes";
                    objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                    objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixAcctNo, out MixAcctType, out Mixkeyvalue);
                    objUpdatetimecardincome.dflt_acct_cr = MixAcctNo;
                    if (objUpdatetimecardincome.timecd_acct_no == 0 && objUpdatetimecardincome.acct_no_id == 0 && objUpdatetimecardincome.dflt_acct_cr == 0)
                    {
                        //test and create flex key records 

                    }
                    objListtimecard.Add(objUpdatetimecardincome);
                }
            }
            return objListtimecard;

        }

        public static bool InsertIntoProcess_PayEmployee(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee, ref object objTrx)
        {
            try
            {
                if (objProcess_PayEmployee.bonus.Trim().Length <= 0)
                    objProcess_PayEmployee.bonus = "N";

                object[] parameters = new object[31];
                parameters[0] = objProcess_PayEmployee.Doc_no;
                parameters[1] = objProcess_PayEmployee.EmplCode;
                parameters[2] = objProcess_PayEmployee.doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[3] = objProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[4] = objProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[5] = objProcess_PayEmployee.print_check;
                parameters[6] = objProcess_PayEmployee.Cash_acct_no;
                parameters[7] = objProcess_PayEmployee.Department;
                parameters[8] = objProcess_PayEmployee.cash_amount;
                parameters[9] = objProcess_PayEmployee.check_no;
                parameters[10] = objProcess_PayEmployee.inc_gross;
                parameters[11] = objProcess_PayEmployee.ded_fica;
                parameters[12] = objProcess_PayEmployee.inc_taxable;
                parameters[13] = objProcess_PayEmployee.ded_medicare;
                parameters[14] = objProcess_PayEmployee.ded_fedtax;
                parameters[15] = objProcess_PayEmployee.ded_statax;
                parameters[16] = objProcess_PayEmployee.ded_loctax;
                parameters[17] = objProcess_PayEmployee.ded_other;
                parameters[18] = objProcess_PayEmployee.obl_futa;
                parameters[19] = objProcess_PayEmployee.obl_fica;
                parameters[20] = objProcess_PayEmployee.obl_medicare;
                parameters[21] = objProcess_PayEmployee.obl_other;
                parameters[22] = objProcess_PayEmployee.obl_total;
                parameters[23] = objProcess_PayEmployee.inc_net;
                parameters[24] = objProcess_PayEmployee.inc_expense;
                parameters[25] = objProcess_PayEmployee.total_hours;
                parameters[26] = objProcess_PayEmployee.ok_to_post;
                parameters[27] = objProcess_PayEmployee.accrue_sick;
                parameters[28] = objProcess_PayEmployee.accrue_vac;
                parameters[29] = objProcess_PayEmployee.bonus;
                parameters[30] = objProcess_PayEmployee.deposit;

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objProcess_PayEmployee.FIND_INSERT_Process_PayEmployee, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool InsertIntoStypayid(DVOPayrollstypayid objDVOPayrollstypayid, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[13];
                parameters[0] = objDVOPayrollstypayid.Doc_no;
                parameters[1] = objDVOPayrollstypayid.line_no;
                parameters[2] = objDVOPayrollstypayid.inc_code.Trim();
                parameters[3] = objDVOPayrollstypayid.inc_rate;
                parameters[4] = objDVOPayrollstypayid.number;
                parameters[5] = objDVOPayrollstypayid.hours;
                parameters[6] = objDVOPayrollstypayid.amount;
                parameters[7] = objDVOPayrollstypayid.acct_no;
                parameters[8] = objDVOPayrollstypayid.Department.Trim();
                parameters[9] = objDVOPayrollstypayid.mod_flag;
                parameters[10] = objDVOPayrollstypayid.add_code.Trim();
                parameters[11] = objDVOPayrollstypayid.lo_inc_amt;
                parameters[12] = objDVOPayrollstypayid.hi_inc_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypayid.INSERT_STYPAYID, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool InsertIntoStypaydd(DVOPayrollstypaydd objDVOPayrollstypaydd, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollstypaydd.Doc_no;
                parameters[1] = objDVOPayrollstypaydd.line_no;
                parameters[2] = objDVOPayrollstypaydd.ded_code.Trim();
                parameters[3] = objDVOPayrollstypaydd.ded_rate;
                parameters[4] = objDVOPayrollstypaydd.amount;
                parameters[5] = objDVOPayrollstypaydd.acct_no;
                parameters[6] = objDVOPayrollstypaydd.Department.Trim();
                parameters[7] = objDVOPayrollstypaydd.mod_flag;
                parameters[8] = objDVOPayrollstypaydd.add_code.Trim();
                parameters[9] = objDVOPayrollstypaydd.lo_ded_amt;
                parameters[10] = objDVOPayrollstypaydd.hi_ded_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypaydd.INSERT_STYPARDD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool InsertIntoStypayod(DVOPayrollStypayod objDVOPayrollStypayod, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollStypayod.Doc_no;
                parameters[1] = objDVOPayrollStypayod.line_no;
                parameters[2] = objDVOPayrollStypayod.obl_code.Trim();
                parameters[3] = objDVOPayrollStypayod.obl_rate;
                parameters[4] = objDVOPayrollStypayod.amount;
                parameters[5] = objDVOPayrollStypayod.acct_no;
                parameters[6] = objDVOPayrollStypayod.Department.Trim();
                parameters[7] = objDVOPayrollStypayod.bal_acct_no;
                parameters[8] = objDVOPayrollStypayod.bal_dept.Trim();
                parameters[9] = objDVOPayrollStypayod.mod_flag;
                parameters[10] = objDVOPayrollStypayod.add_code.Trim();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollStypayod.INSERT_STYPAYOD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public decimal ded_taxcalc(int n, decimal tax_wages, string pay_period,
            ref List<DVOPayrollstypaydd> objPayrolldeductionsglobal,
            ref List<DVOPayrollautopay> objListemplforprocess,
            ref DVOPayrollautopay objpaydatasearch,
            ref Int32 currentempnoid,
            ref bool year_is_current,
            ref string ErrMsg1,
            ref string ErrMsg2)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // check for exempt status for state
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                if (objListemplforprocess[currentempnoid].StateAllow == 99)
                {
                    return 0;
                }
            }
            else
            {
                //if not state tax code then default to federal allowances
                if (objListemplforprocess[currentempnoid].Allowances == 99)
                {
                    return 0;
                }
            }
            // initialize flags
            bool check_year = false;
            year_is_current = true;
            decimal allow_amt = 0;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";

            // get allowance value
            object[] parameters = new object[2];
            parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            switch (pay_period)
            {

                case "W":
                    {
                        object week_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_week_allow);
                        if (week_allow == null || week_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(week_allow);

                        }
                        break;
                    }
                case "B":
                    {
                        object biweek_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_biweek_allow);
                        if (biweek_allow == null || biweek_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(biweek_allow);

                        }
                        break;
                    }
                case "S":
                    {
                        object smonth_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_smonth_allow);
                        if (smonth_allow == null || smonth_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(smonth_allow);

                        }
                        break;
                    }
                case "M":
                    {
                        object month_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_month_allow);
                        if (month_allow == null || month_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(month_allow);

                        }
                        break;
                    }
                case "Q":
                    {
                        object quarter_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_quarter_allow);
                        if (quarter_allow == null || quarter_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(quarter_allow);

                        }
                        break;
                    }
                case "H":
                    {
                        object syear_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_syear_allow);
                        if (syear_allow == null || syear_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(syear_allow);

                        }
                        break;
                    }
                case "A":
                    {
                        object year_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
                        if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(year_allow);

                        }
                        break;
                    }
                case "D":
                    {
                        object misc_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_misc_allow);
                        if (misc_allow == null || misc_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(misc_allow);

                        }
                        break;
                    }
            }
            //check_year is set to true when a table lookup returns nothing
            //check to see if the table does exist, but the Tax Year is not current
            if (check_year)
            {
                //call tbl_check
                tbl_check(n, ref objPayrolldeductionsglobal, ref objpaydatasearch, ref year_is_current, ref ErrMsg1, ref ErrMsg2);
            }
            // make sure required values exist
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[currentempnoid].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            // calculate taxable amount
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));

            }
            else
            {
                //if not state tax code then default to federal allowances
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            //get tax table values
            if (year_is_current)
            {
                object[] taxparameter = new object[5];
                taxparameter[0] = objPayrolldeductionsglobal[n].ded_code;
                taxparameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                taxparameter[2] = pay_period.Trim();
                taxparameter[3] = t_total;
                taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                DataSet ds = objDalBaseClass.GetData(ref taxparameter, typeof(DVOPayrollstypaydd), objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                        t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                        t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                        t_period = dr[3].ToString().Trim();
                        t_marital = dr[4].ToString().Trim();
                    }
                }
                //calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }

            return t_total;
        }

        public void tbl_check(int n,
            ref List<DVOPayrollstypaydd> objPayrolldeductionsglobal,
            ref DVOPayrollautopay objpaydatasearch,
            ref bool year_is_current,
            ref string ErrMsg1,
            ref string ErrMsg2)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // this function is called to verify that if a table exists, that
            // the Tax Year matches the payroll date year.
            object[] parameter = new object[2];
            parameter[0] = objPayrolldeductionsglobal[n].ded_code;
            parameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            DataSet ds = objDalBaseClass.GetData(ref parameter, typeof(DVOPayrollstypaydd), objPayrolldeductionsglobal[n].FIND_usp_tbl_check);
            if (ds.Tables[0].Rows[0][0] == DBNull.Value || ds.Tables[0].Rows[0][0].ToString().Trim() == string.Empty)
            {
                ErrMsg2 = "*** Error: Table(s) not current for employee's deduction codes.";
                ErrMsg1 = "**** End of report.  One or more deduction tables are not current.";
                year_is_current = false;
            }

        }

        public decimal state_calc(int n,
            ref List<DVOPayrollstypaydd> objPayrolldeductionsglobal,
            ref List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList,
            ref List<DVOPayrollautopay> objListemplforprocess,
            ref DVOPayrollautopay objpaydatasearch,
            ref decimal fica_wages,
            ref decimal futa_wages,
            ref Int32 currentempnoid,
            ref string ErrMsg1,
            ref string ErrMsg2,
            ref bool year_is_current)
        {
            // define
            decimal wage_amount = 0;
            decimal statax_amount = 0;
            // set wage_amount appropriately
            if (objPayrolldeductionsglobal[n].ded_type.Trim() == "G")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_gross ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "T")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_taxable ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "F")
            {
                wage_amount = fica_wages;

            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "U")
            {
                wage_amount = futa_wages;
            }
            else
            {
                wage_amount = 0;
            }
            if (objPayrolldeductionsglobal[n].tax_code == string.Empty)
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));

                }
                else
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate * wage_amount));
                }
            }
            else
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));
                }
                else
                {
                    statax_amount = ded_taxcalc(n, wage_amount, objListemplforprocess[n].PayPeriod,
                        ref objPayrolldeductionsglobal,
                        ref objListemplforprocess,
                        ref objpaydatasearch,
                        ref currentempnoid,
                        ref year_is_current,
                        ref ErrMsg1,
                        ref ErrMsg2);

                }

            }
            return statax_amount;
        }

        public decimal ded_fedgrs(int n, decimal tax_wages, string pay_period,
            ref List<DVOPayrollstypaydd> objPayrolldeductionsglobal,
            ref DVOPayrollautopay objpaydatasearch,
            ref List<DVOPayrollautopay> objListemplforprocess,
            ref List<DVOUpdatePayDefaults> objstycntrcList,
            ref List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList,
            ref Int32 currentempnoid,
            ref string ErrMsg1,
            ref string ErrMsg2)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal allow_amt = 0;
            bool check_year = false;
            bool year_is_current = true;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";
            object[] parameters = new object[2];
            parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            object year_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
            if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
            {
                check_year = true;
            }
            else
            {
                allow_amt = Convert.ToDecimal(year_allow);

            }
            if (check_year)
            {
                tbl_check(n, ref objPayrolldeductionsglobal, ref objpaydatasearch, ref year_is_current, ref ErrMsg1, ref ErrMsg2);
            }
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[n].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));
            }
            else
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            if (year_is_current)
            {
                object[] taxparameter = new object[5];
                taxparameter[0] = objstycntrcList[0].fedtax_code;
                taxparameter[1] = objDVOPayrollProcess_PayEmployeeList[n].pay_date;
                taxparameter[2] = "A";
                taxparameter[3] = t_total;
                taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                DataSet ds = objDalBaseClass.GetData(ref taxparameter, typeof(DVOPayrollstypaydd), objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                    t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                    t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                    t_period = dr[3].ToString().Trim();
                    t_marital = dr[4].ToString().Trim();
                }
                // calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
                if (t_total < 0)
                {
                    t_total = 0;
                }

            }
            return t_total;
        }
    }


    public class BLLPayrollAutopayDR
    {
        public List<DVOPayrollstypayid> objPayrollIncomesglobal = new List<DVOPayrollstypayid>();
        public List<DVOPayrollstypaydd> objPayrolldeductionsglobal = new List<DVOPayrollstypaydd>();
        public List<DVOPayrollStypayod> objPayrollobligationsglobal = new List<DVOPayrollStypayod>();
        public List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();

        public decimal fica_wages = 0.0M;
        public decimal futa_wages = 0.0M;
        public Int32 dup_ssn_pay = 0;
        public Int32 dup_ssn = 0;
        public DVOPayrollautopay objpaydatasearch = new DVOPayrollautopay();
        object objTransaction;
        BLLPayrollFunctions BPfunctions = new BLLPayrollFunctions();

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //DALBaseClass objDalBaseClass;
        public bool same_person = false;
        public string empl_ssn;
        public Int32 currentempnoid;
        public List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList = new List<DVOPayrollProcess_PayEmployee>();
        public List<DVOPayrollautopay> objListemplforprocess;
        bool year_is_current = true;
        public string ErrMsg1 = string.Empty;
        public string ErrMsg2 = string.Empty;
        public string ErrMsg3 = string.Empty;
        public List<DVOPayrollautopay> GetEmplListforProcess(DVOPayrollautopay objpayautosearchdata)
        {
            //this function will return the Employee
            //List for which the 
            //payroll is to be processed .
            objpaydatasearch = objpayautosearchdata;
            object[] parameters = new object[11];
            parameters[0] = objpayautosearchdata.RowID;
            parameters[1] = objpayautosearchdata.EmplCode;
            parameters[2] = objpayautosearchdata.SocSecNum;
            parameters[3] = objpayautosearchdata.FirstName;
            parameters[4] = objpayautosearchdata.LastName;
            parameters[5] = objpayautosearchdata.Employee_Type;
            parameters[6] = objpayautosearchdata.Job_Code;
            parameters[7] = "";
            parameters[8] = "";
            if (objpayautosearchdata.Process_TimeCard == "N")
            {
                parameters[9] = false;
            }
            else
            {
                parameters[9] = true;
            }

            parameters[10] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

            List<DVOPayrollautopay> objpayrollautopaylist = new List<DVOPayrollautopay>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            IDataReader dr = objDalBaseClass.GetDataByReader(ref parameters, typeof(DVOPayrollautopay));
            while (dr.Read())
            {
                using (DVOPayrollautopay objautopay = new DVOPayrollautopay())
                {
                    objautopay.EmplCode = dr[0].ToString().Trim();
                    objautopay.SocSecNum = dr[1].ToString().Trim();
                    objautopay.FirstName = dr[2].ToString().Trim();
                    objautopay.LastName = dr[3].ToString().Trim();
                    objautopay.CashAcct = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objautopay.Department = dr[5].ToString().Trim();
                    objautopay.Terminated = (dr[6] != DBNull.Value ? Convert.ToString(dr[6]).Trim() : string.Empty);
                    objautopay.PayPeriod = dr[7].ToString().Trim();
                    objautopay.Allowances = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                    objautopay.StateAllow = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
                    objautopay.MaritalStat = dr[10].ToString().Trim();
                    objautopay.VacCode = dr[11].ToString().Trim();
                    objautopay.VacAllowed = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                    objautopay.VacUsed = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objautopay.SickCode = dr[14].ToString().Trim();
                    objautopay.SickAllowed = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objautopay.SickUsed = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                    objautopay.LastPay = (dr[17] != DBNull.Value ? Convert.ToDateTime(dr[17]) : Convert.ToDateTime(null));
                    objautopay.HoldPayment = dr[18].ToString().Trim();
                    objautopay.StaTaxCode = dr[19].ToString().Trim();
                    objautopay.LocTaxCode = dr[20].ToString().Trim();
                    objautopay.DirDept = dr[21].ToString().Trim();
                    objautopay.FlexDeptAcctType = dr[22].ToString().Trim();
                    objautopay.LastIncDate = (dr[23] != DBNull.Value ? Convert.ToString(dr[23]).Trim() : string.Empty);
                    objautopay.RowID = (dr[24] != DBNull.Value && dr[24].ToString().Trim() != "" ? Convert.ToInt32(dr[24]) : 0);

                    DVOFlexSegCommon objflexsegloadtype = new DVOFlexSegCommon();
                    objflexsegloadtype.EntityType = objautopay.TABLE_NAME;
                    objflexsegloadtype.Code = objautopay.EmplCode;
                    objflexsegloadtype.AccountType = objautopay.FlexDeptAcctType;
                    objautopay.Flexdeptkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegloadtype);

                    if (BPfunctions.pay_time(objautopay.LastPay, objpayautosearchdata.EOP_Date, objautopay.PayPeriod, Convert.ToBoolean(parameters[9])))
                        objpayrollautopaylist.Add(objautopay);
                }
            }
            return objpayrollautopaylist;
        }

        public List<DVOPayrollProcess_PayEmployee> Autopay(ref DVOPayrollautopay objpayautosearchdata)
        {
            /*
             written by     Rohit Wadhwa 
             written Date   22/12/2008
             AIM :.
             this function loads the default income/deduction/obligation
             codes into the internal arrays p_ypayre, p_ypayid, p_ypaydd,
             and p_ypayod, calculates the corresponding amounts and inserts
             them into Process_PayEmployee,stypayid,stypaydd,stypayod etc... It also sets the remaining required
             data in Process_PayEmployee.
             */
            try
            {
                bool prep_flag = false;
                bool year_is_current = true;
                objListemplforprocess = GetEmplListforProcess(objpayautosearchdata);
                DVOUpdatePayDefaults objPayDefaul = new DVOUpdatePayDefaults();
                objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                bool ok_to_commit = true;

                for (Int32 i = 0; i < objListemplforprocess.Count; i++)
                {
                    currentempnoid = i;
                    object[] parameters = new object[1];
                    DVOPayrollautopay objDvopayrollauto = new DVOPayrollautopay();
                    objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                    objDvopayrollauto = objListemplforprocess[i];
                    parameters[0] = objDvopayrollauto.SocSecNum.Trim();
                    //get from DB if it is a duplicate ssn code 
                    dup_ssn = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, objDvopayrollauto.DUP_SSN));
                    //
                    empl_ssn = objListemplforprocess[i].SocSecNum.Trim();
                    ok_to_commit = true;
                    objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    if (dup_ssn > 1)
                    {
                        parameters = new object[2];
                        parameters[0] = objDvopayrollauto.EmplCode.Trim();
                        parameters[1] = objDvopayrollauto.SocSecNum.Trim();
                        dup_ssn_pay = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, objDvopayrollauto.DUP_SSN_PAY));
                    }
                    if (dup_ssn_pay > 0)
                    {
                        same_person = true;
                        // break;
                    }

                    objPayrollProcess_PayEmployee.EmplCode = objDvopayrollauto.EmplCode;

                    if (objpayautosearchdata.Payroll_Date == Convert.ToDateTime(null))
                    {
                        objpayautosearchdata.Payroll_Date = DVOApplicationUserInfo.CurrentDate;
                    }
                    if (objpayautosearchdata.EOP_Date == Convert.ToDateTime(null))
                    {
                        objpayautosearchdata.EOP_Date = objpayautosearchdata.Payroll_Date;
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.deposit = "Y";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.deposit = "N";
                    }
                    //objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_doc_no", ref objTransaction);
                    objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next_PYDocNo();
                    if (objPayrollProcess_PayEmployee.Doc_no == 0)
                    {
                        throw new Exception("Payroll Control table is locked or empty.");
                        //rollback 
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return objDVOPayrollProcess_PayEmployeeList;
                    }

                    objPayrollProcess_PayEmployee.doc_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.pay_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.eop_date = objpayautosearchdata.EOP_Date;
                    objPayrollProcess_PayEmployee.bonus = objpayautosearchdata.BonusCeck;
                    if (objPayrollProcess_PayEmployee.bonus == "Y")
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "N";
                        objPayrollProcess_PayEmployee.accrue_vac = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "Y";
                        objPayrollProcess_PayEmployee.accrue_vac = "Y";
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.print_check = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.print_check = "Y";
                    }
                    objPayrollProcess_PayEmployee.ok_to_post = "N";
                    objPayrollProcess_PayEmployee.StateTaxCode = objDvopayrollauto.StaTaxCode;
                    if (objDvopayrollauto.Flexdeptacctno == 0)
                    {
                        //set value from control table is there is no value for Employee cash account .
                        objDvopayrollauto.Flexdeptacctno = objstycntrcList[0].cash_acct;
                    }

                    objPayrollProcess_PayEmployee.Cash_acct_no = objDvopayrollauto.Flexdeptacctno;
                    objPayrollProcess_PayEmployee.Department = objDvopayrollauto.Department;
                    objPayrollIncomesglobal = LoadIncomes(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                    objPayrolldeductionsglobal = LoadDeductions(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                    objPayrollobligationsglobal = LoadObligations(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);

                    objPayrollProcess_PayEmployee = CalculatePayrolls(ref objPayrollProcess_PayEmployee);
                    // post the header record
                    bool re_status = InsertIntoProcess_PayEmployee(ref objPayrollProcess_PayEmployee, ref objTransaction);
                    if (!re_status)
                    {
                        ok_to_commit = false;
                    }
                    // post income detail                  
                    for (int j = 0; j < objPayrollIncomesglobal.Count; j++)
                    {
                        objPayrollIncomesglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool id_ststus = InsertIntoStypayid(objPayrollIncomesglobal[j], ref objTransaction);
                        if (!id_ststus)
                        {
                            ok_to_commit = false;
                        }
                    }
                    //post deduction detail
                    for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
                    {
                        objPayrolldeductionsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool dd_status = InsertIntoStypaydd(objPayrolldeductionsglobal[j], ref objTransaction);
                        if (!dd_status)
                        {
                            ok_to_commit = false;
                        }
                    }
                    //post obligation detail
                    for (int j = 0; j < objPayrollobligationsglobal.Count; j++)
                    {
                        objPayrollobligationsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool od_ststus = InsertIntoStypayod(objPayrollobligationsglobal[j], ref objTransaction);
                        if (!od_ststus)
                        {
                            ok_to_commit = false;
                        }
                    }
                    if (ok_to_commit)
                    {
                        //coomitwork
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        objDVOPayrollProcess_PayEmployeeList.Add(objPayrollProcess_PayEmployee);
                    }
                    else
                    {
                        //rollback 
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    }
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                objDVOPayrollProcess_PayEmployeeList.Clear();

                throw ex;
            }
            finally
            {
                if (objTransaction != null)
                    objTransaction = null;
            }
            return objDVOPayrollProcess_PayEmployeeList;
        }
        public DVOPayrollProcess_PayEmployee CalculatePayrolls(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee)
        {

            // bool dedflag = false;
            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objProcess_PayEmployee.cash_amount = 0.0M;
            objProcess_PayEmployee.inc_gross = 0.0M;
            objProcess_PayEmployee.inc_taxable = 0.0M;
            objProcess_PayEmployee.ded_fica = 0.0M;
            objProcess_PayEmployee.ded_medicare = 0.0M;
            objProcess_PayEmployee.ded_fedtax = 0.0M;
            objProcess_PayEmployee.ded_statax = 0.0M;
            objProcess_PayEmployee.ded_loctax = 0.0M;
            objProcess_PayEmployee.ded_other = 0.0M;
            objProcess_PayEmployee.obl_futa = 0.0M;
            objProcess_PayEmployee.obl_fica = 0.0M;
            objProcess_PayEmployee.obl_medicare = 0.0M;
            objProcess_PayEmployee.obl_other = 0.0M;
            objProcess_PayEmployee.obl_total = 0.0M;
            objProcess_PayEmployee.inc_net = 0.0M;
            objProcess_PayEmployee.inc_expense = 0.0M;
            objProcess_PayEmployee.total_hours = 0.0M;

            for (int i = 0; i < objPayrollIncomesglobal.Count; i++)
            {
                objProcess_PayEmployee = CalculateIncomes(ref objProcess_PayEmployee, i);
                if (objPayrollIncomesglobal[i].inc_type != "F")
                {
                    objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable + objPayrollIncomesglobal[i].amount;
                }
            }
            // set initial taxable & net income (gross cannot be less than zero)
            //
            //# Taxable income is derived from determining which income codes are to be taxed as
            //#  opposed to which deductions reduce the gross.  Only certain income codes are to
            //#  assessed SOC-SEC and LEVY taxes so we need to calculate thses separately.  These
            //#  are indicated with an "F" which indicates that the income code in question is exempt
            //#  these two taxes.
            //# let p_ypayre.inc_taxable = p_ypayre.inc_gross
            //##### ^^^^^ - - - -- - - - ^^^^^ ##########
            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross;


            // calculate deductions that reduce taxable income
            for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
            {
                if ((objPayrolldeductionsglobal[j].ded_taxred != "N") &&
                (objPayrolldeductionsglobal[j].ded_taxred != null))
                {
                    objPayrolldeductionsglobal[j].dedflag = true;
                    CalculateDeductions(ref objProcess_PayEmployee, j);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[j].amount;
                    // figure the effect on wage bases
                    switch (objPayrolldeductionsglobal[j].ded_taxred)
                    {
                        case "A":
                            {
                                // deduction reduces all wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable -
                                objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "B":
                            {
                                // deduction reduces taxable and fica wage bases

                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "C":
                            {
                                //deduction reduces taxable and futa wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "D":
                            {
                                // deduction reduces futa and fica wage bases
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "F":
                            {
                                // deduction reduces fica wage base only
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "T":
                            {
                                // deduction reduces taxable wage base only
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                break;
                            }
                        case "U":
                            {
                                // deduction reduces futa wage base only
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }

                    }
                }
                else
                {
                    objPayrolldeductionsglobal[j].dedflag = false;
                }


            }
            // calculate deductions that do not reduce taxable income
            for (int k = 0; k < objPayrolldeductionsglobal.Count; k++)
            {
                if (objPayrolldeductionsglobal[k].dedflag == false)
                {
                    CalculateDeductions(ref objProcess_PayEmployee, k);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[k].amount;
                }
            }

            // calculate obligations
            for (int k = 0; k < objPayrollobligationsglobal.Count; k++)
            {
                CalculateObligations(ref objProcess_PayEmployee, k);
            }
            //add final totals
            //adjust totals using overall deduction accumulation instead
            //of running net to take care of possible rounding errors
            objProcess_PayEmployee.cash_amount = objProcess_PayEmployee.inc_gross +
           objProcess_PayEmployee.inc_expense - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_fedtax + objProcess_PayEmployee.ded_medicare +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax +
           objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_medicare + objProcess_PayEmployee.ded_fedtax +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax + objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.obl_total = objProcess_PayEmployee.obl_futa + objProcess_PayEmployee.obl_fica +
            objProcess_PayEmployee.obl_medicare + objProcess_PayEmployee.obl_other;

            return objProcess_PayEmployee;
        }
        public DVOPayrollProcess_PayEmployee CalculateIncomes(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            //this function recalculates the income amount and resets the gross wages.

            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            //recalculate the amount
            objPayrollIncomesglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", (objPayrollIncomesglobal[i].inc_rate * objPayrollIncomesglobal[i].number)));

            // make sure amount is not null and non-negative
            if (objPayrollIncomesglobal[i].amount < 0)
            {
                objPayrollIncomesglobal[i].amount = 0.0M;
            }

            //add to the gross wages or expenses/advances
            switch (objPayrollIncomesglobal[i].inc_type)
            {
                case "H":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "E":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = objpayrollProcess_PayEmployee.inc_expense + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                case "A":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = objpayrollProcess_PayEmployee.inc_expense + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                case "F":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "U":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "B":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                default:
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
            }

            objpayrollProcess_PayEmployee.total_hours = objpayrollProcess_PayEmployee.total_hours + objPayrollIncomesglobal[i].hours;

            return objpayrollProcess_PayEmployee;
        }

        public static void CalculateIncomeChanges(string IncomeCodeType, decimal IncomeAmount, out decimal GrossIncomeChange, out decimal TaxableIncomeChange, out decimal FicaWagesChange, out decimal FutaWagesChange)
        {
            GrossIncomeChange = 0;
            FicaWagesChange = 0;
            FutaWagesChange = 0;
            TaxableIncomeChange = 0;

            switch (IncomeCodeType)
            {
                case "H":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "E":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "A":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "F":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        break;
                    }
                case "U":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "B":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                default:
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
            }
        }

        public DVOPayrollProcess_PayEmployee CalculateDeductions(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_deductions = 0.0M;
            decimal currentdeductions = 0.0M;

            if (objPayrolldeductionsglobal[i].ded_type == null)
            {
                objPayrolldeductionsglobal[i].ded_type = "T";
            }
            //if (objPayrolldeductionsglobal[i].ded_type == null)
            //{
            //    objPayrolldeductionsglobal[i].ded_type = "T";
            //}
            //if (objPayrolldeductionsglobal[i].ded_type == null)
            //{
            //    objPayrolldeductionsglobal[i].ded_type = "T";
            //}
            // set the maximum deduction
            if (objPayrolldeductionsglobal[i].ded_limit == 0.0M)
            {
                Max_deductions = Convert.ToDecimal(999999999999.99);  // large decimal(12) value
            }
            else
            {
                objPayrolldeductionsglobal[i].ded_ytd = 0.0M;
                currentdeductions = 0;
                if (dup_ssn == 1)
                {
                    object[] parameter1 = new object[2];
                    parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    parameter1[1] = objPayrolldeductionsglobal[i].ded_code;
                    object ded_ytd = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrolldeductionsglobal[0].FIND_stypayddytd);
                    if (ded_ytd.ToString().Trim() != "")
                    {
                        objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd);
                    }
                }
                else
                {
                    //currentdeductions;
                    object[] parameter1 = new object[2];
                    parameter1[0] = empl_ssn;
                    parameter1[1] = objPayrolldeductionsglobal[i].ded_code;
                    object ded_ytd = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrolldeductionsglobal[0].FIND_stypayddytd1);
                    if (ded_ytd.ToString().Trim() != "")
                    {
                        objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd);
                    }

                    if (same_person)
                    {
                        object[] parameter2 = new object[3];
                        parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                        parameter1[1] = empl_ssn;
                        parameter1[2] = objPayrolldeductionsglobal[i].ded_code;
                        object ded_ytd1 = objDalBaseClass.ExecuteScalar(ref parameter2, objPayrolldeductionsglobal[0].FIND_stypayddytd2);
                        if (ded_ytd1.ToString().Trim() != "")
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd1);
                        }
                    }
                }


                if (currentdeductions != 0)
                {
                    objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + currentdeductions;
                }
                // get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrolldeductionsglobal[j].ded_code == objPayrolldeductionsglobal[i].ded_code)
                    {
                        if (objPayrolldeductionsglobal[j].amount != 0)
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + objPayrolldeductionsglobal[j].amount;
                        }
                    }
                }
                Max_deductions = (objPayrolldeductionsglobal[i].ded_limit - objPayrolldeductionsglobal[i].ded_ytd) ?? 0;//make nullable decimal By Rahul
            }
            if (Max_deductions < 0.0M)
            {
                Max_deductions = 0.0M;
            }
            //calc the amount of the deduction relative to type
            if (objPayrolldeductionsglobal[i].ded_code.Trim() == objpayrollProcess_PayEmployee.StateTaxCode.Trim())
            {
                //call the state tax calculation logic
                objPayrolldeductionsglobal[i].amount = state_calc(i);
            }
            switch (objPayrolldeductionsglobal[i].ded_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != 0))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_gross));
                            }
                        }
                        else
                        {
                            //make nullable decimal By Rahul
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod);
                        }
                        break;
                    }

                case "T":
                    {
                        //calculate amount using taxable wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != Convert.ToDecimal(null)))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_taxable));
                            }
                        }
                        else
                        {
                            //make nullable decimal By Rahul
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod);
                        }
                        break;
                    }
                case "U":
                    {
                        //calculate amount using futa wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * futa_wages));
                        }
                        break;
                    }
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * fica_wages));
                        }
                        break;
                    }
                case "H":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.total_hours));
                        break;
                    }
                case "N":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        break;
                    }
                default:
                    {
                        objPayrolldeductionsglobal[i].amount = 0.0M;
                        break;
                    }

            }


            //make sure deduction is not greater than net or zero if net < 0
            if (objPayrolldeductionsglobal[i].amount > 0)
            {
                if (objpayrollProcess_PayEmployee.inc_net < 0)
                {
                    objPayrolldeductionsglobal[i].amount = 0;
                }
                else
                {
                    if (objPayrolldeductionsglobal[i].amount > objpayrollProcess_PayEmployee.inc_net)
                    {
                        objPayrolldeductionsglobal[i].amount = objpayrollProcess_PayEmployee.inc_net;
                    }
                }
            }

            if (objPayrolldeductionsglobal[i].pay_limit != 0.0M)
            {
                if (objPayrolldeductionsglobal[i].amount > objPayrolldeductionsglobal[i].pay_limit)
                {
                    objPayrolldeductionsglobal[i].amount = objPayrolldeductionsglobal[i].pay_limit;
                }
            }
            // check for limit
            if (objPayrolldeductionsglobal[i].amount > Max_deductions)
            {
                objPayrolldeductionsglobal[i].amount = Max_deductions;
            }

            // post amount to correct total
            if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fedtax_code)
            {
                objpayrollProcess_PayEmployee.ded_fedtax = objpayrollProcess_PayEmployee.ded_fedtax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].statax_code)
            {
                objpayrollProcess_PayEmployee.ded_statax = objpayrollProcess_PayEmployee.ded_statax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].loctax_code)
            {
                objpayrollProcess_PayEmployee.ded_loctax = objpayrollProcess_PayEmployee.ded_loctax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.ded_fica = objpayrollProcess_PayEmployee.ded_fica + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].medicare_code)
            {
                objpayrollProcess_PayEmployee.ded_medicare = objpayrollProcess_PayEmployee.ded_medicare + objPayrolldeductionsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.ded_other = objpayrollProcess_PayEmployee.ded_other + objPayrolldeductionsglobal[i].amount;
            }



            return objpayrollProcess_PayEmployee;
        }
        public DVOPayrollProcess_PayEmployee CalculateObligations(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_Obligations = 0.0M;
            decimal Current_obligations = 0.0M;
            decimal obligationYTD = 0.0M;
            decimal Max_deductions = 0.0M;
            // this function calculates the obligation amount and updates
            //   cumulative totals for the control obligation codes

            // set obligation type
            if (objPayrollobligationsglobal[i].obl_type == null)
            {
                objPayrollobligationsglobal[i].obl_type = "T";
            }

            // get obligation limit and accrual

            // set the maximum obligation
            if (objPayrollobligationsglobal[i].obl_limit == 0.0M)
            {
                Max_deductions = 999999999.99M;  // large decimal(12) value
            }
            else
            {
                //check for current accrual
                //Current_obligations = 0.0M;
                if (dup_ssn == 1)
                {
                    //obligationYTD = 0.0M; //Set it with Year to date obligations
                    object[] parameter1 = new object[2];
                    parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    object Current_obl = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd);
                    if (Current_obl.ToString().Trim() != "")
                    {
                        objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obl);
                    }
                }
                else
                {
                    //obligationYTD = 0.0M; //Set it with Year to date obligations
                    object[] parameter1 = new object[2];
                    parameter1[0] = empl_ssn;
                    parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    object oblYTD = objDalBaseClass.ExecuteScalar(ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd1);
                    if (oblYTD.ToString().Trim() != "")
                    {
                        objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(oblYTD);
                    }

                    if (same_person)
                    {
                        //Current_obligations = 0.0M; //set it with current obligations 
                        object[] parameter2 = new object[3];
                        parameter2[0] = objpayrollProcess_PayEmployee.EmplCode;
                        parameter2[1] = empl_ssn;
                        parameter2[2] = objPayrollobligationsglobal[i].obl_code;
                        object Current_obligations1 = objDalBaseClass.ExecuteScalar(ref parameter2, objPayrollobligationsglobal[0].FIND_stypayodytd2);
                        if (Current_obligations1.ToString().Trim() != "")
                        {
                            objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obligations1);
                        }

                    }
                }

                if (Current_obligations != 0)
                {
                    objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + Current_obligations;//objPayrollobligationsglobal[i].amount;
                }
                //get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrollobligationsglobal[j].obl_code == objPayrollobligationsglobal[i].obl_code)
                    {
                        if (objPayrollobligationsglobal[j].amount != 0.0M)
                        {
                            objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + objPayrollobligationsglobal[j].amount;
                        }
                    }

                }
                Max_Obligations = (objPayrollobligationsglobal[i].obl_limit - objPayrollobligationsglobal[i].obl_ytd) ?? 0;//make nullable decimal By Rahul
            }
            if (Max_Obligations < 0)
            {
                Max_Obligations = 0.0M;
            }
            switch (objPayrollobligationsglobal[i].obl_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_gross));
                        }
                    }
                    break;
                case "T":
                    {
                        //calculate amount using taxable wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_taxable));
                        }
                    }
                    break;
                case "U":
                    //calculate amount using futa wage base
                    {
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_futa));
                        }
                    }
                    break;
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_fica));
                        }
                    }
                    break;
                case "E":
                    // Based on the employees deduction amount
                    {
                        for (int j = 0; j <= objPayrolldeductionsglobal.Count; j++)
                        {
                            if (objPayrollobligationsglobal[i].obl_code == objPayrolldeductionsglobal[j].ded_code)
                            {
                                objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objPayrolldeductionsglobal[j].amount));
                                break;
                            }
                        }
                    }
                    break;
                case "H":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.total_hours));
                    }
                    break;
                case "N":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                    }
                    break;
                    //default:
                    //    {
                    //        objPayrollobligationsglobal[i].amount = 0.0M;
                    //    }
            }

            //Modified by Sarvjeet On 13/08/2009
            if (objPayrollobligationsglobal[i].pay_limit > 0.0M)
            {

                if (objPayrollobligationsglobal[i].amount > objPayrollobligationsglobal[i].pay_limit)
                {

                    objPayrollobligationsglobal[i].amount = objPayrollobligationsglobal[i].pay_limit;

                }

            }
            //check for limit
            if (objPayrollobligationsglobal[i].amount > Max_Obligations)
            {
                objPayrollobligationsglobal[i].amount = Max_Obligations;
            }
            // post amount to correct total
            if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].futa_code)
            {
                objpayrollProcess_PayEmployee.obl_futa = objpayrollProcess_PayEmployee.obl_futa + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.obl_fica = objpayrollProcess_PayEmployee.obl_fica + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].medicare_ob_code)
            {
                objpayrollProcess_PayEmployee.obl_medicare = objpayrollProcess_PayEmployee.obl_medicare + objPayrollobligationsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.obl_other = objpayrollProcess_PayEmployee.obl_other + objPayrollobligationsglobal[i].amount;
            }

            //   DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            return objpayrollProcess_PayEmployee;
        }
        public static List<DVOPayrollstypayid> LoadIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOUpdateTimeCard> objlistTimecard = new List<DVOUpdateTimeCard>();
            objlistTimecard = LoadTimeCardIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            List<DVOPayrollstypayid> objlistpayrollincomes = new List<DVOPayrollstypayid>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            Int32 Maxlineno = 0;
            if (objlistTimecard.Count < 1)
            {
                objlistTimecard = LoadEmployeeIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            }
            for (int i = 0; i < objlistTimecard.Count; i++)
            {
                DVOPayrollstypayid objemployeeincome = new DVOPayrollstypayid();
                objemployeeincome.inc_code = objlistTimecard[i].inc_code_id;
                objemployeeincome.inc_rate = objlistTimecard[i].inc_rate_id;
                objemployeeincome.number = objlistTimecard[i].inc_number_id;
                objemployeeincome.hours = objlistTimecard[i].inc_hours_id;
                objemployeeincome.add_code = objlistTimecard[i].add_code_cr;
                objemployeeincome.inc_type = objlistTimecard[i].inc_type_cr;
                if ((objemployeeincome.add_code == "Y") || (objemployeeincome.add_code == "Z"))
                {
                    parameters[0] = EmployeeCode;

                    object Maxlineno1 = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOPayrollautopay()).EMPLOYEEMAXLINENOGET);
                    if (Maxlineno1 == null)
                    {
                        Maxlineno = 0;

                    }
                    Maxlineno = Maxlineno + i;
                }
                else
                {
                    objemployeeincome.add_code = "N";
                    objemployeeincome.line_no = objlistTimecard[i].line_no_id;
                }
                objemployeeincome.lo_inc_amt = objlistTimecard[i].lo_inc_amt_id;
                objemployeeincome.hi_inc_amt = objlistTimecard[i].hi_inc_amt_id;
                // Take the timecard account number first.  Comment out the following
                // line here, but use it as a default if the timecard account number is null.
                // let p_ypayid[n].acct_no = inc_ref[n].acct_no
                objemployeeincome.acct_no = objlistTimecard[i].timecd_acct_no;
                objemployeeincome.Department = objlistTimecard[i].department_id;

                //Assign Default Values as required ........
                if (objemployeeincome.lo_inc_amt == 0.0M)
                {
                    objemployeeincome.lo_inc_amt = objlistTimecard[i].dflt_lo_inc_amt_cr;
                }
                if (objemployeeincome.hi_inc_amt == 0.0M)
                {
                    objemployeeincome.hi_inc_amt = objlistTimecard[i].dflt_hi_inc_amt_cr;
                }
                if (objemployeeincome.inc_rate == 0.0M)
                {
                    objemployeeincome.inc_rate = objlistTimecard[i].dflt_rate_cr;
                }
                if (objemployeeincome.hours == 0.0M)
                {
                    objemployeeincome.hours = objlistTimecard[i].dflt_hours_cr;
                }
                if (objemployeeincome.number == 0.0M)
                {
                    objemployeeincome.number = objlistTimecard[i].dflt_num_cr;
                }
                // use defaults if necessary

                if (objemployeeincome.acct_no == 0)
                {
                    objemployeeincome.acct_no = objlistTimecard[i].acct_no_id;
                    if (objemployeeincome.acct_no == 0)
                    {
                        objemployeeincome.acct_no = objlistTimecard[i].dflt_acct_cr;
                    }
                }
                if ((objemployeeincome.Department == string.Empty) || (objemployeeincome.Department == null))
                {
                    objemployeeincome.Department = objlistTimecard[i].dflt_dept_cr;
                }
                objlistpayrollincomes.Add(objemployeeincome);
            }
            return objlistpayrollincomes;
        }
        public List<DVOPayrollstypaydd> LoadDeductions(string EmployeeCode, DateTime EOPdate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollstypaydd> objListpayrollstypaydd = new List<DVOPayrollstypaydd>();
            List<DVOMasterEmployeeDeductions> objListemployeeDeddefaultdata = new List<DVOMasterEmployeeDeductions>();
            //DVOPayrollstypaydd objpayrollstypaydd = new DVOPayrollstypaydd();
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Dedreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;
            IDataReader dr = objDalBaseClass.GetDataByReader(ref parameters, (new DVOMasterEmployeeDeductions()).EmpDedanddefaults);

            while (dr.Read())
            {
                using (DVOMasterEmployeeDeductions objemployeedefaultdedrec = new DVOMasterEmployeeDeductions())
                {
                    objemployeedefaultdedrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dfltkeyvalue = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                    objemployeedefaultdedrec.ded_code = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                    objemployeedefaultdedrec.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objemployeedefaultdedrec.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
                    objemployeedefaultdedrec.ded_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
                    objemployeedefaultdedrec.pay_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                    objemployeedefaultdedrec.balanceamt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                    objemployeedefaultdedrec.ded_apply = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]).Trim() : string.Empty);
                    objemployeedefaultdedrec.lo_ded_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);
                    objemployeedefaultdedrec.hi_ded_amt = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0);
                    objemployeedefaultdedrec.acct_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
                    objemployeedefaultdedrec.department = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]).Trim() : "000");
                    objemployeedefaultdedrec.ded_ytd = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0);
                    objemployeedefaultdedrec.ded_date = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                    objemployeedefaultdedrec.description = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : string.Empty);
                    objemployeedefaultdedrec.ded_type = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]).Trim() : string.Empty);
                    objemployeedefaultdedrec.ded_taxred = (dr[17] != DBNull.Value ? Convert.ToString(dr[17]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dflt_rate = (dr[18] != DBNull.Value ? Convert.ToDecimal(dr[18]) : 0.0M);
                    objemployeedefaultdedrec.dflt_limit = (dr[19] != DBNull.Value ? Convert.ToDecimal(dr[19]) : 0);
                    objemployeedefaultdedrec.dflt_pay_limit = (dr[20] != DBNull.Value ? Convert.ToDecimal(dr[20]) : 0);
                    objemployeedefaultdedrec.yearrollover = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : string.Empty);
                    objemployeedefaultdedrec.dflt_lo_ded_amt = (dr[22] != DBNull.Value ? Convert.ToDecimal(dr[22]) : 0);
                    objemployeedefaultdedrec.dflt_hi_ded_amt = (dr[23] != DBNull.Value ? Convert.ToDecimal(dr[23]) : 0);
                    objemployeedefaultdedrec.dflt_acct = (dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0);
                    objemployeedefaultdedrec.dflt_dept = (dr[25] != DBNull.Value ? Convert.ToString(dr[25]).Trim() : "000");
                    objemployeedefaultdedrec.dflt_apply = (dr[26] != DBNull.Value ? Convert.ToString(dr[26]).Trim() : string.Empty);
                    parameters = new object[2];
                    parameters[0] = objemployeedefaultdedrec.ded_code;
                    parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                    object taxcode = objDalBaseClass.ExecuteScalar(ref parameters, objpaydatasearch.DeductionTaxCodeGet);
                    if (taxcode != DBNull.Value)
                    {
                        objemployeedefaultdedrec.tax_code = taxcode.ToString().Trim();
                    }

                    parameters = new object[2];
                    parameters[0] = objemployeedefaultdedrec.ded_code;
                    parameters[1] = EOPdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                    object result = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeDeductions()).DeductioncodeTaxget);
                    if (result != null)
                        objemployeedefaultdedrec.tax_code = result.ToString().Trim();
                    if (objemployeedefaultdedrec.ded_apply == string.Empty)
                    {
                        objemployeedefaultdedrec.ded_apply = objemployeedefaultdedrec.dflt_apply;
                    }
                    // assign defaults as required
                    if (objemployeedefaultdedrec.lo_ded_amt == 0)
                    {
                        objemployeedefaultdedrec.lo_ded_amt = objemployeedefaultdedrec.dflt_lo_ded_amt;
                    }
                    if (objemployeedefaultdedrec.hi_ded_amt == 0)
                    {
                        objemployeedefaultdedrec.hi_ded_amt = objemployeedefaultdedrec.dflt_hi_ded_amt;
                    }
                    if (objemployeedefaultdedrec.ded_rate == 0)
                    {
                        objemployeedefaultdedrec.ded_rate = objemployeedefaultdedrec.dflt_rate;
                    }
                    if (objemployeedefaultdedrec.acct_no == 0)
                    {
                        objemployeedefaultdedrec.acct_no = objemployeedefaultdedrec.dflt_acct;
                    }
                    if (objemployeedefaultdedrec.department == null)
                    {
                        objemployeedefaultdedrec.department = objemployeedefaultdedrec.dflt_dept;
                    }
                    if (objemployeedefaultdedrec.ded_limit == 0)
                    {
                        objemployeedefaultdedrec.ded_limit = objemployeedefaultdedrec.dflt_limit;
                    }
                    objListemployeeDeddefaultdata.Add(objemployeedefaultdedrec);
                }
            }

            Dedreccount = objListemployeeDeddefaultdata.Count;
            for (int i = 0; i < Dedreccount; i++)
            {
                using (DVOPayrollstypaydd objpayrollstypaydd = new DVOPayrollstypaydd())
                {
                    DateTime ded_date = Convert.ToDateTime(null);
                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    if (objListemployeeDeddefaultdata[i].ded_date != string.Empty)
                    {
                        ded_date = Convert.ToDateTime(objListemployeeDeddefaultdata[i].ded_date);
                    }
                    else
                    {
                        ded_date = Convert.ToDateTime(null);
                    }
                    if (objpayrollfunctions.Pay_Frequency(objListemployeeDeddefaultdata[i].ded_apply, EOPdate, ded_date))
                    {
                        objpayrollstypaydd.ded_rate = objListemployeeDeddefaultdata[i].ded_rate.GetValueOrDefault(0.0M);
                    }
                    else
                    {
                        objListemployeeDeddefaultdata[i].ded_rate = 0.0M;
                    }
                    objpayrollstypaydd.ded_code = objListemployeeDeddefaultdata[i].ded_code;
                    objpayrollstypaydd.amount = 0;
                    objpayrollstypaydd.lo_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].lo_ded_amt);
                    objpayrollstypaydd.hi_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].hi_ded_amt);
                    objpayrollstypaydd.ded_taxred = objListemployeeDeddefaultdata[i].ded_taxred;
                    objpayrollstypaydd.acct_no = objListemployeeDeddefaultdata[i].acct_no;
                    objpayrollstypaydd.Department = objListemployeeDeddefaultdata[i].department;
                    objpayrollstypaydd.line_no = objListemployeeDeddefaultdata[i].line_no;
                    objpayrollstypaydd.add_code = "N";
                    objpayrollstypaydd.ded_type = objListemployeeDeddefaultdata[i].ded_type;
                    if (objListemployeeDeddefaultdata[i].pay_limit == 0)
                    {
                        objpayrollstypaydd.pay_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_pay_limit);
                    }

                    if (objListemployeeDeddefaultdata[i].yearrollover == "Y")
                    {
                        if (objpayrollstypaydd.amount > Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt))
                        {
                            objpayrollstypaydd.amount = Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt);
                        }
                    }
                    if (objListemployeeDeddefaultdata[i].ded_limit == 0)
                    {
                        objpayrollstypaydd.ded_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_limit);
                    }
                    if (objpayrollstypaydd.ded_ytd == 0.0M)
                    {
                        objpayrollstypaydd.ded_ytd = Convert.ToDecimal(objListemployeeDeddefaultdata[i].ded_ytd);
                    }
                    if (objpayrollstypaydd.tax_code == string.Empty)
                    {
                        objpayrollstypaydd.tax_code = objListemployeeDeddefaultdata[i].tax_code;
                    }
                    objListpayrollstypaydd.Add(objpayrollstypaydd);
                }
            }
            return objListpayrollstypaydd;
        }
        public static List<DVOPayrollStypayod> LoadObligations(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollStypayod> objListpayrollstypayod = new List<DVOPayrollStypayod>();
            List<DVOMasterEmployeeObligations> objListemployeeObldefaultdate = new List<DVOMasterEmployeeObligations>();

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Oblreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;
            IDataReader dr = objDalBaseClass.GetDataByReader(ref parameters, (new DVOMasterEmployeeObligations()).Emplobldefaultsget);

            while (dr.Read())
            {
                //MasterOblCodes.dfltaccounttype,MasterOblCodes.dfltbaccounttype,MasterOblCodes.dfltbkeyvalue,
                //MasterEmployeeObligations.obl_code,MasterEmployeeObligations.line_no,MasterEmployeeObligations.obl_rate,MasterEmployeeObligations.obl_limit,
                //MasterEmployeeObligations.pay_limit,MasterEmployeeObligations.acct_no,MasterEmployeeObligations.department,MasterEmployeeObligations.bal_acct_no,
                //MasterEmployeeObligations.bal_dept,MasterEmployeeObligations.obl_ytd,MasterOblCodes.description,
                //MasterOblCodes.obl_type,MasterOblCodes.dflt_rate,MasterOblCodes.dflt_limit,
                //MasterOblCodes.dflt_pay_limit,  MasterOblCodes.dflt_acct, MasterOblCodes.dflt_dept,
                //MasterOblCodes.dflt_bacct,MasterOblCodes.dflt_bdept

                using (DVOMasterEmployeeObligations objemployeedefaultoblrec = new DVOMasterEmployeeObligations())
                {
                    objemployeedefaultoblrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dfltbaccounttype = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dfltbkeyvalue = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                    objemployeedefaultoblrec.obl_code = (dr[3] != DBNull.Value ? Convert.ToString(dr[3]).Trim() : string.Empty);
                    objemployeedefaultoblrec.line_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objemployeedefaultoblrec.obl_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                    objemployeedefaultoblrec.obl_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                    objemployeedefaultoblrec.pay_limit = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                    objemployeedefaultoblrec.acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                    objemployeedefaultoblrec.department = (dr[9] != DBNull.Value ? Convert.ToString(dr[9]).Trim() : "000");
                    objemployeedefaultoblrec.bal_acct_no = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                    objemployeedefaultoblrec.bal_dept = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                    objemployeedefaultoblrec.obl_ytd = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);
                    objemployeedefaultoblrec.description = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]).Trim() : string.Empty);
                    objemployeedefaultoblrec.obl_type = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                    objemployeedefaultoblrec.dflt_rate = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objemployeedefaultoblrec.dflt_limit = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0);
                    objemployeedefaultoblrec.dflt_pay_limit = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0);
                    objemployeedefaultoblrec.dflt_acct = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                    objemployeedefaultoblrec.dflt_dept = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                    objemployeedefaultoblrec.dflt_bacct = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);
                    objemployeedefaultoblrec.dflt_bdept = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : "000");
                    DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                    objflexsegcommon.AccountType = objemployeedefaultoblrec.dfltaccounttype;
                    objflexsegcommon.EntityType = "MasterOblCodes";
                    objflexsegcommon.Code = objemployeedefaultoblrec.obl_code;
                    objemployeedefaultoblrec.dfltbkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    objpayrollfunctions.flxMix(objemployeedefaultoblrec.dfltaccounttype, objemployeedefaultoblrec.dfltbkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                    objemployeedefaultoblrec.acct_no = MixaccountNo;
                    // objemployeedefaultoblrec.dflt_acct
                    if ((objemployeedefaultoblrec.dflt_acct == 0) &&
                     (objemployeedefaultoblrec.acct_no == 0))
                    {
                        //Test if the Account Exists in the table or not with th ekeyvalue we got now .
                        // scratch will contain the description after testFlexAccountKey
                        //create PayrollGLAccounts Flex account Entry
                    }
                    objListemployeeObldefaultdate.Add(objemployeedefaultoblrec);
                }
            }

            Oblreccount = objListemployeeObldefaultdate.Count;
            for (Int32 i = 0; i < Oblreccount; i++)
            {
                using (DVOPayrollStypayod objpayrollstypayod = new DVOPayrollStypayod())
                {
                    objpayrollstypayod.obl_code = objListemployeeObldefaultdate[i].obl_code;
                    objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].obl_rate;
                    objpayrollstypayod.amount = 0;//objListemployeeObldefaultdate[i].obl_code;
                    objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].acct_no;
                    objpayrollstypayod.Department = objListemployeeObldefaultdate[i].department;
                    objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].bal_acct_no;
                    objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].bal_dept;
                    objpayrollstypayod.line_no = objListemployeeObldefaultdate[i].line_no;
                    objpayrollstypayod.add_code = "N";
                    objpayrollstypayod.obl_type = objListemployeeObldefaultdate[i].obl_type;
                    // assign defaults as required

                    if (objpayrollstypayod.obl_rate == 0.0M)
                    {
                        objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].dflt_rate;
                    }
                    if (objpayrollstypayod.acct_no == 0)
                    {
                        objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].dflt_acct;
                    }
                    if ((objpayrollstypayod.Department == "000") || (objpayrollstypayod.Department == null))
                    {
                        objpayrollstypayod.Department = objListemployeeObldefaultdate[i].dflt_dept;
                    }
                    if (objpayrollstypayod.bal_acct_no == 0)
                    {
                        objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].dflt_bacct;
                    }
                    if ((objpayrollstypayod.bal_dept == "000") || (objpayrollstypayod.bal_dept == null))
                    {
                        objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].dflt_bdept;
                    }
                    if (objListemployeeObldefaultdate[i].pay_limit == 0)
                    {
                        objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_pay_limit);
                    }
                    else
                    {
                        objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].pay_limit);
                    }
                    if (objListemployeeObldefaultdate[i].obl_limit == 0)
                    {
                        objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_limit);
                    }
                    else
                    {
                        objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].obl_limit);
                    }
                    objListpayrollstypayod.Add(objpayrollstypayod);
                }
            }
            return objListpayrollstypayod;
        }

        public static List<DVOUpdateTimeCard> LoadTimeCardIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[2];
            parameters[0] = EmployeeCode;
            parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();

            IDataReader dr = objDalBaseClass.GetDataByReader(ref parameters, (new DVOUpdateTimeCard()).FIND_DETAILS_FORAUTOPAY);

            while (dr.Read())
            {
                objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                objUpdatetimecardincome.dfltkeyvalue = "";
                objUpdatetimecardincome.acct_no_id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
                objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
                objUpdatetimecardincome.description_cr = dr[8].ToString().Trim();
                objUpdatetimecardincome.dflt_num_cr = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
                objUpdatetimecardincome.dflt_rate_cr = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0.0M);
                objUpdatetimecardincome.dflt_hours_cr = (dr[11] != DBNull.Value ? Convert.ToDecimal(dr[11]) : 0.0M);
                objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                objUpdatetimecardincome.dflt_acct_cr = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
                objUpdatetimecardincome.dflt_dept_cr = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : "000");
                objUpdatetimecardincome.inc_type_cr = dr[16].ToString().Trim();

                DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                objflexsegcommon.EntityType = "MasterIncCodes";
                objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                objUpdatetimecardincome.dflt_acct_cr = MixaccountNo;
                parameters = new object[3];
                parameters[0] = EmployeeCode;
                parameters[1] = objUpdatetimecardincome.inc_code_id;
                parameters[2] = objUpdatetimecardincome.line_no_id;
                IDataReader drt = objDalBaseClass.GetDataByReader(ref parameters, (new DVOMasterEmployeeIncomes()).EMPLOYEEINCOME_AUTOPAY);
                bool _getdata = false;
                while (drt.Read())
                {
                    _getdata = true;
                    objUpdatetimecardincome.lo_inc_amt_id = (drt[0] != DBNull.Value ? Convert.ToDecimal(drt[0]) : 0.0M); ;
                    objUpdatetimecardincome.hi_inc_amt_id = (drt[1] != DBNull.Value ? Convert.ToDecimal(drt[1]) : 0.0M); ;
                    objUpdatetimecardincome.acct_no_id = (drt[2] != DBNull.Value ? Convert.ToInt32(drt[2]) : 0);
                    objUpdatetimecardincome.department_id = (drt[3] != DBNull.Value ? Convert.ToString(drt[3]).Trim() : "000"); ;
                }
                if (_getdata)
                {
                    if (((objUpdatetimecardincome.timecd_acct_no == 0) || (objUpdatetimecardincome.timecd_acct_no == null)) &&
                    ((objUpdatetimecardincome.acct_no_id == 0) || (objUpdatetimecardincome.acct_no_id == null)) &&
                   ((objUpdatetimecardincome.dflt_acct_cr == 0) || (objUpdatetimecardincome.dflt_acct_cr == null)))
                    {
                        //test and create flex key records 

                    }
                    objUpdatetimecardincome.add_code_cr = "N";
                }
                else
                {
                    // does the code already exist at the employee level?
                    parameters = new object[2];
                    parameters[0] = objUpdatetimecardincome.inc_code_id;
                    parameters[1] = EmployeeCode;
                    dupempinccount = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeIncomes()).EMPLINCOMECNT_AUTOPAY));
                    if (dupempinccount >= 1)
                    {
                        objUpdatetimecardincome.add_code_cr = "Z";
                    }
                    else
                    {
                        objUpdatetimecardincome.add_code_cr = "Y";
                    }
                }
                if (lastcard == objUpdatetimecardincome.card_no)
                {

                }
                else
                {
                    lastcard = objUpdatetimecardincome.card_no;
                    newRec = true;
                    //   objListtimecard.Add(objUpdatetimecardincome);
                }

                Timecarddetail = objListtimecard.Count;
                for (int i = 0; i < Timecarddetail; i++)
                {
                    if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                        (objUpdatetimecardincome.inc_rate_id == objListtimecard[i].inc_rate_id))
                    {
                        objListtimecard[i].inc_hours_id = objListtimecard[i].inc_hours_id + objUpdatetimecardincome.inc_hours_id;
                        objListtimecard[i].inc_number_id = objListtimecard[i].inc_number_id + objUpdatetimecardincome.inc_number_id;
                        newRec = false;
                        break;
                    }
                    else
                    {
                        // If the code is the same as the employee entry
                        // but the rate differs we do not want to append
                        if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                          (objUpdatetimecardincome.line_no_id == objListtimecard[i].line_no_id))
                        {
                            if ((objUpdatetimecardincome.inc_number_id == 0) && (objListtimecard[i].inc_number_id != 0))
                            {
                                objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                newRec = false;
                                break;
                            }
                            else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id != 0))
                            {
                                objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                newRec = false;
                                break;
                            }
                            else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id == 0))
                            {
                                objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                newRec = false;
                                break;
                            }
                            else
                            {
                                //This code is a duplicate but the rate differs
                                // and the number is not zero.
                                objUpdatetimecardincome.add_code_cr = "Z";
                                // newRec = false;
                            }

                        }
                    }
                }
                if (newRec == true)
                {
                    objListtimecard.Add(objUpdatetimecardincome);
                    objUpdatetimecardincome = new DVOUpdateTimeCard();
                }

            }

            // If duplicate codes were added on the fly to the timecard, we
            // only want to append one to the employee entry at posting time.
            Timecarddetail = objListtimecard.Count;
            for (int j = 0; j < Timecarddetail; j++)
            {
                if (objListtimecard[j].add_code_cr == "Y")
                {
                    for (int k = 0; k < Timecarddetail; k++)
                    {
                        if (j == k)
                        {
                        }
                        else
                        {
                            if (objListtimecard[j].inc_code_id == objListtimecard[k].inc_code_id)
                            {
                                objListtimecard[j].add_code_cr = "Z";
                            }
                        }
                    }
                }
            }
            return objListtimecard;
        }
        public static List<DVOUpdateTimeCard> LoadEmployeeIncomes(string EmployeeCode, DateTime Eopdate, string FlexacctType, string FlexDepartment)
        {
            //This function loads the default income data from the employee
            //reference tables into an income reference array and reset the array
            // count.

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            parameters[0] = EmployeeCode;

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixAcctNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();

            IDataReader dr = objDalBaseClass.GetDataByReader(ref parameters, (new DVOPayrollautopay()).EmployeeIncomerefer);

            while (dr.Read())
            {
                using (DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard())
                {
                    objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                    objUpdatetimecardincome.dfltkeyvalue = "";
                    objUpdatetimecardincome.timecd_acct_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                    objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                    objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                    objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                    objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
                    objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
                    objUpdatetimecardincome.lo_inc_amt_id = (dr[8] != DBNull.Value ? Convert.ToDecimal(dr[8]) : 0.0M);
                    objUpdatetimecardincome.hi_inc_amt_id = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
                    objUpdatetimecardincome.acct_no_id = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                    objUpdatetimecardincome.department_id = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                    objUpdatetimecardincome.description_cr = dr[12].ToString().Trim();
                    objUpdatetimecardincome.dflt_num_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objUpdatetimecardincome.dflt_rate_cr = (dr[14] != DBNull.Value ? Convert.ToDecimal(dr[14]) : 0.0M);
                    objUpdatetimecardincome.dflt_hours_cr = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                    objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0.0M);
                    objUpdatetimecardincome.dflt_acct_cr = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                    objUpdatetimecardincome.dflt_dept_cr = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                    objUpdatetimecardincome.inc_type_cr = dr[20].ToString().Trim();
                    DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                    objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                    objflexsegcommon.EntityType = "MasterIncCodes";
                    objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                    objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

                    BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                    objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixAcctNo, out MixAcctType, out Mixkeyvalue);
                    objUpdatetimecardincome.dflt_acct_cr = MixAcctNo;
                    if (objUpdatetimecardincome.timecd_acct_no == 0 && objUpdatetimecardincome.acct_no_id == 0 && objUpdatetimecardincome.dflt_acct_cr == 0)
                    {
                        //test and create flex key records 
                    }
                    objListtimecard.Add(objUpdatetimecardincome);
                }
            }
            return objListtimecard;
        }

        public static bool InsertIntoProcess_PayEmployee(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee, ref object objTrx)
        {
            try
            {
                if (objProcess_PayEmployee.bonus.Trim().Length <= 0)
                    objProcess_PayEmployee.bonus = "N";

                object[] parameters = new object[31];
                parameters[0] = objProcess_PayEmployee.Doc_no;
                parameters[1] = objProcess_PayEmployee.EmplCode;
                parameters[2] = objProcess_PayEmployee.doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[3] = objProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[4] = objProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[5] = objProcess_PayEmployee.print_check;
                parameters[6] = objProcess_PayEmployee.Cash_acct_no;
                parameters[7] = objProcess_PayEmployee.Department;
                parameters[8] = objProcess_PayEmployee.cash_amount;
                parameters[9] = objProcess_PayEmployee.check_no;
                parameters[10] = objProcess_PayEmployee.inc_gross;
                parameters[11] = objProcess_PayEmployee.ded_fica;
                parameters[12] = objProcess_PayEmployee.inc_taxable;
                parameters[13] = objProcess_PayEmployee.ded_medicare;
                parameters[14] = objProcess_PayEmployee.ded_fedtax;
                parameters[15] = objProcess_PayEmployee.ded_statax;
                parameters[16] = objProcess_PayEmployee.ded_loctax;
                parameters[17] = objProcess_PayEmployee.ded_other;
                parameters[18] = objProcess_PayEmployee.obl_futa;
                parameters[19] = objProcess_PayEmployee.obl_fica;
                parameters[20] = objProcess_PayEmployee.obl_medicare;
                parameters[21] = objProcess_PayEmployee.obl_other;
                parameters[22] = objProcess_PayEmployee.obl_total;
                parameters[23] = objProcess_PayEmployee.inc_net;
                parameters[24] = objProcess_PayEmployee.inc_expense;
                parameters[25] = objProcess_PayEmployee.total_hours;
                parameters[26] = objProcess_PayEmployee.ok_to_post;
                parameters[27] = objProcess_PayEmployee.accrue_sick;
                parameters[28] = objProcess_PayEmployee.accrue_vac;
                parameters[29] = objProcess_PayEmployee.bonus;
                parameters[30] = objProcess_PayEmployee.deposit;

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objProcess_PayEmployee.FIND_INSERT_Process_PayEmployee, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertIntoStypayid(DVOPayrollstypayid objDVOPayrollstypayid, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[13];
                parameters[0] = objDVOPayrollstypayid.Doc_no;
                parameters[1] = objDVOPayrollstypayid.line_no;
                parameters[2] = objDVOPayrollstypayid.inc_code.Trim();
                parameters[3] = objDVOPayrollstypayid.inc_rate;
                parameters[4] = objDVOPayrollstypayid.number;
                parameters[5] = objDVOPayrollstypayid.hours;
                parameters[6] = objDVOPayrollstypayid.amount;
                parameters[7] = objDVOPayrollstypayid.acct_no;
                parameters[8] = objDVOPayrollstypayid.Department.Trim();
                parameters[9] = objDVOPayrollstypayid.mod_flag;
                parameters[10] = objDVOPayrollstypayid.add_code.Trim();
                parameters[11] = objDVOPayrollstypayid.lo_inc_amt;
                parameters[12] = objDVOPayrollstypayid.hi_inc_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypayid.INSERT_STYPAYID, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool InsertIntoStypaydd(DVOPayrollstypaydd objDVOPayrollstypaydd, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollstypaydd.Doc_no;
                parameters[1] = objDVOPayrollstypaydd.line_no;
                parameters[2] = objDVOPayrollstypaydd.ded_code.Trim();
                parameters[3] = objDVOPayrollstypaydd.ded_rate;
                parameters[4] = objDVOPayrollstypaydd.amount;
                parameters[5] = objDVOPayrollstypaydd.acct_no;
                parameters[6] = objDVOPayrollstypaydd.Department.Trim();
                parameters[7] = objDVOPayrollstypaydd.mod_flag;
                parameters[8] = objDVOPayrollstypaydd.add_code.Trim();
                parameters[9] = objDVOPayrollstypaydd.lo_ded_amt;
                parameters[10] = objDVOPayrollstypaydd.hi_ded_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypaydd.INSERT_STYPARDD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertIntoStypayod(DVOPayrollStypayod objDVOPayrollStypayod, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollStypayod.Doc_no;
                parameters[1] = objDVOPayrollStypayod.line_no;
                parameters[2] = objDVOPayrollStypayod.obl_code.Trim();
                parameters[3] = objDVOPayrollStypayod.obl_rate;
                parameters[4] = objDVOPayrollStypayod.amount;
                parameters[5] = objDVOPayrollStypayod.acct_no;
                parameters[6] = objDVOPayrollStypayod.Department.Trim();
                parameters[7] = objDVOPayrollStypayod.bal_acct_no;
                parameters[8] = objDVOPayrollStypayod.bal_dept.Trim();
                parameters[9] = objDVOPayrollStypayod.mod_flag;
                parameters[10] = objDVOPayrollStypayod.add_code.Trim();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollStypayod.INSERT_STYPAYOD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public decimal ded_taxcalc(int n, decimal tax_wages, string pay_period)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // check for exempt status for state
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                if (objListemplforprocess[currentempnoid].StateAllow == 99)
                {
                    return 0;
                }
            }
            else
            {
                //if not state tax code then default to federal allowances
                if (objListemplforprocess[currentempnoid].Allowances == 99)
                {
                    return 0;
                }
            }
            // initialize flags
            bool check_year = false;
            year_is_current = true;
            decimal allow_amt = 0;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";

            // get allowance value
            object[] parameters = new object[2];
            parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            switch (pay_period)
            {

                case "W":
                    {
                        object week_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_week_allow);
                        if (week_allow == null || week_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(week_allow);

                        }
                        break;
                    }
                case "B":
                    {
                        object biweek_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_biweek_allow);
                        if (biweek_allow == null || biweek_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(biweek_allow);

                        }
                        break;
                    }
                case "S":
                    {
                        object smonth_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_smonth_allow);
                        if (smonth_allow == null || smonth_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(smonth_allow);

                        }
                        break;
                    }
                case "M":
                    {
                        object month_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_month_allow);
                        if (month_allow == null || month_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(month_allow);

                        }
                        break;
                    }
                case "Q":
                    {
                        object quarter_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_quarter_allow);
                        if (quarter_allow == null || quarter_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(quarter_allow);

                        }
                        break;
                    }
                case "H":
                    {
                        object syear_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_syear_allow);
                        if (syear_allow == null || syear_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(syear_allow);

                        }
                        break;
                    }
                case "A":
                    {
                        object year_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
                        if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(year_allow);

                        }
                        break;
                    }
                case "D":
                    {
                        object misc_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_misc_allow);
                        if (misc_allow == null || misc_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(misc_allow);

                        }
                        break;
                    }
            }
            //check_year is set to true when a table lookup returns nothing
            //check to see if the table does exist, but the Tax Year is not current
            if (check_year)
            {
                //call tbl_check
                tbl_check(n);
            }
            // make sure required values exist
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[currentempnoid].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            // calculate taxable amount
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));

            }
            else
            {
                //if not state tax code then default to federal allowances
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            //get tax table values
            if (year_is_current)
            {
                object[] taxparameter = new object[5];
                taxparameter[0] = objPayrolldeductionsglobal[n].ded_code;
                taxparameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                taxparameter[2] = pay_period.Trim();
                taxparameter[3] = t_total;
                taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();

                IDataReader dr = objDalBaseClass.GetDataByReader(ref taxparameter, objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                while (dr.Read())
                {
                    t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                    t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                    t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                    t_period = dr[3].ToString().Trim();
                    t_marital = dr[4].ToString().Trim();
                    break;
                }
                //calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }

            return t_total;
        }
        public void tbl_check(int n)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // this function is called to verify that if a table exists, that
            // the Tax Year matches the payroll date year.
            object[] parameter = new object[2];
            parameter[0] = objPayrolldeductionsglobal[n].ded_code;
            parameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            object obj = objDalBaseClass.ExecuteScalar(ref parameter, objPayrolldeductionsglobal[n].FIND_usp_tbl_check);
            if (obj == DBNull.Value || obj == null || obj.ToString().Trim().Length <= 0)
            {
                ErrMsg2 = "*** Error: Table(s) not current for employee's deduction codes.";
                ErrMsg1 = "**** End of report.  One or more deduction tables are not current.";
                year_is_current = false;
            }
        }
        public decimal state_calc(int n)
        {
            // define
            decimal wage_amount = 0;
            decimal statax_amount = 0;
            // set wage_amount appropriately
            if (objPayrolldeductionsglobal[n].ded_type.Trim() == "G")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_gross ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "T")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_taxable ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "F")
            {
                wage_amount = fica_wages;

            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "U")
            {
                wage_amount = futa_wages;
            }
            else
            {
                wage_amount = 0;
            }
            if (objPayrolldeductionsglobal[n].tax_code == string.Empty)
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));

                }
                else
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate * wage_amount));
                }
            }
            else
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));
                }
                else
                {
                    statax_amount = ded_taxcalc(n, wage_amount, objListemplforprocess[n].PayPeriod);

                }

            }
            return statax_amount;
        }
        public decimal ded_fedgrs(int n, decimal tax_wages, string pay_period)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal allow_amt = 0;
            bool check_year = false;
            bool year_is_current = true;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";
            object[] parameters = new object[2];
            parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            object year_allow = objDalBaseClass.ExecuteScalar(ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
            if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
                check_year = true;
            else
                allow_amt = Convert.ToDecimal(year_allow);

            if (check_year)
                tbl_check(n);

            if (objListemplforprocess[currentempnoid].StateAllow == 0)
                objListemplforprocess[n].StateAllow = objListemplforprocess[currentempnoid].Allowances;

            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));
            else
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));

            if (t_total < 0)
                t_total = 0;

            if (year_is_current)
            {
                object[] taxparameter = new object[5];
                taxparameter[0] = objstycntrcList[0].fedtax_code;
                taxparameter[1] = objDVOPayrollProcess_PayEmployeeList[n].pay_date;
                taxparameter[2] = "A";
                taxparameter[3] = t_total;
                taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                IDataReader dr = objDalBaseClass.GetDataByReader(ref taxparameter, objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                while (dr.Read())
                {
                    t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                    t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                    t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                    t_period = dr[3].ToString().Trim();
                    t_marital = dr[4].ToString().Trim();
                }
                // calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
                if (t_total < 0)
                    t_total = 0;
            }
            return t_total;
        }
    }


    public class BLLPayrollAutopayNewSingleTransaction
    {
        public List<DVOPayrollstypayid> objPayrollIncomesglobal = new List<DVOPayrollstypayid>();
        public List<DVOPayrollstypaydd> objPayrolldeductionsglobal = new List<DVOPayrollstypaydd>();
        public List<DVOPayrollStypayod> objPayrollobligationsglobal = new List<DVOPayrollStypayod>();
        public List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();

        public decimal fica_wages = 0.0M;
        public decimal futa_wages = 0.0M;
        public Int32 dup_ssn_pay = 0;
        public Int32 dup_ssn = 0;
        public DVOPayrollautopay objpaydatasearch = new DVOPayrollautopay();
        object objTransaction;
        BLLPayrollFunctions BPfunctions = new BLLPayrollFunctions();

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //DALBaseClass objDalBaseClass;
        public bool same_person = false;
        public string empl_ssn;
        public Int32 currentempnoid;
        public List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList = new List<DVOPayrollProcess_PayEmployee>();
        public List<DVOPayrollautopay> objListemplforprocess;
        bool year_is_current = true;
        public string ErrMsg1 = string.Empty;
        public string ErrMsg2 = string.Empty;
        public string ErrMsg3 = string.Empty;
        public List<DVOPayrollautopay> GetEmplListforProcess(ref object objTransaction, DVOPayrollautopay objpayautosearchdata)
        {
            //this function will return the Employee
            //List for which the 
            //payroll is to be processed .
            objpaydatasearch = objpayautosearchdata;
            object[] parameters = new object[11];
            parameters[0] = objpayautosearchdata.RowID;
            parameters[1] = objpayautosearchdata.EmplCode;
            parameters[2] = objpayautosearchdata.SocSecNum;
            parameters[3] = objpayautosearchdata.FirstName;
            parameters[4] = objpayautosearchdata.LastName;
            parameters[5] = objpayautosearchdata.Employee_Type;
            parameters[6] = objpayautosearchdata.Job_Code;
            parameters[7] = "";
            parameters[8] = "";
            if (objpayautosearchdata.Process_TimeCard == "N")
            {
                parameters[9] = false;
            }
            else
            {
                parameters[9] = true;
            }

            parameters[10] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

            List<DVOPayrollautopay> objpayrollautopaylist = new List<DVOPayrollautopay>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //DataSet ds;
            //ds.Tables[0].Select(

            using (DataSet ds = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref parameters, typeof(DVOPayrollautopay)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DVOPayrollautopay objautopay = new DVOPayrollautopay();
                    objautopay.EmplCode = dr[0].ToString().Trim();
                    objautopay.SocSecNum = dr[1].ToString().Trim();
                    objautopay.FirstName = dr[2].ToString().Trim();
                    objautopay.LastName = dr[3].ToString().Trim();
                    objautopay.CashAcct = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objautopay.Department = dr[5].ToString().Trim();
                    objautopay.Terminated = (dr[6] != DBNull.Value ? Convert.ToString(dr[6]).Trim() : string.Empty);
                    objautopay.PayPeriod = dr[7].ToString().Trim();
                    objautopay.Allowances = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                    objautopay.StateAllow = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
                    objautopay.MaritalStat = dr[10].ToString().Trim();
                    objautopay.VacCode = dr[11].ToString().Trim();
                    objautopay.VacAllowed = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                    objautopay.VacUsed = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objautopay.SickCode = dr[14].ToString().Trim();
                    objautopay.SickAllowed = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objautopay.SickUsed = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                    objautopay.LastPay = (dr[17] != DBNull.Value ? Convert.ToDateTime(dr[17]) : Convert.ToDateTime(null));
                    objautopay.HoldPayment = dr[18].ToString().Trim();
                    objautopay.StaTaxCode = dr[19].ToString().Trim();
                    objautopay.LocTaxCode = dr[20].ToString().Trim();
                    objautopay.DirDept = dr[21].ToString().Trim();
                    objautopay.FlexDeptAcctType = dr[22].ToString().Trim();
                    objautopay.LastIncDate = (dr[23] != DBNull.Value ? Convert.ToString(dr[23]).Trim() : string.Empty);
                    objautopay.RowID = (dr[24] != DBNull.Value && dr[24].ToString().Trim() != "" ? Convert.ToInt32(dr[24]) : 0);
                    DVOFlexSegCommon objflexsegloadtype = new DVOFlexSegCommon();
                    objflexsegloadtype.EntityType = objautopay.TABLE_NAME;
                    objflexsegloadtype.Code = objautopay.EmplCode;
                    objflexsegloadtype.AccountType = objautopay.FlexDeptAcctType;
                    objautopay.Flexdeptkeyvalue = BLLFlexSegCommon.Flexseg_Load_UsingTransaction(ref objTransaction, ref objflexsegloadtype);
                    if (BPfunctions.pay_time(objautopay.LastPay, objpayautosearchdata.EOP_Date, objautopay.PayPeriod, Convert.ToBoolean(parameters[9])))
                    {
                        objpayrollautopaylist.Add(objautopay);
                    }
                }

            }
            return objpayrollautopaylist;

        }
        public List<DVOPayrollProcess_PayEmployee> Autopay(ref DVOPayrollautopay objpayautosearchdata)
        {
            /*
             written by     Rohit Wadhwa 
             written Date   22/12/2008
             AIM :.
             this function loads the default income/deduction/obligation
             codes into the internal arrays p_ypayre, p_ypayid, p_ypaydd,
             and p_ypayod, calculates the corresponding amounts and inserts
             them into Process_PayEmployee,stypayid,stypaydd,stypayod etc... It also sets the remaining required
             data in Process_PayEmployee.
             */
            try
            {
                bool prep_flag = false;
                bool year_is_current = true;

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                objTransaction = objDALBaseClassHelper.GetTransactionObject();
                objListemplforprocess = GetEmplListforProcess(ref objTransaction, objpayautosearchdata);
                if (objListemplforprocess.Count > 0)
                {
                    LoadAllTimeCardIncomes(objpayautosearchdata);
                    LoadAllEmployeeIncomes(objpayautosearchdata);
                    LoadAllDeductions(objpayautosearchdata);
                    LoadAllObligations(objpayautosearchdata);
                }

                DVOUpdatePayDefaults objPayDefaul = new DVOUpdatePayDefaults();
                objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);
                DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                bool ok_to_commit = true;

                int _currentDocNo = 0;
                object nullTransacionObject = null;
                if (objListemplforprocess.Count > 0)
                    _currentDocNo = BLLAccountingLiberary.CurrentValue_With_LockAndSelect("stycntrc", "py_doc_no", objListemplforprocess.Count, ref nullTransacionObject);

                for (Int32 i = 0; i < objListemplforprocess.Count; i++)
                {
                    currentempnoid = i;
                    object[] parameters = new object[1];
                    DVOPayrollautopay objDvopayrollauto = new DVOPayrollautopay();
                    objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                    objDvopayrollauto = objListemplforprocess[i];
                    parameters[0] = objDvopayrollauto.SocSecNum.Trim();
                    //get from DB if it is a duplicate ssn code 
                    dup_ssn = Convert.ToInt32(objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDvopayrollauto.DUP_SSN));
                    //
                    empl_ssn = objListemplforprocess[i].SocSecNum.Trim();
                    ok_to_commit = true;
                    //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    if (dup_ssn > 1)
                    {
                        parameters = new object[2];
                        parameters[0] = objDvopayrollauto.EmplCode.Trim();
                        parameters[1] = objDvopayrollauto.SocSecNum.Trim();
                        dup_ssn_pay = Convert.ToInt32(objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDvopayrollauto.DUP_SSN_PAY));
                    }
                    if (dup_ssn_pay > 0)
                    {
                        same_person = true;
                        // break;
                    }

                    objPayrollProcess_PayEmployee.EmplCode = objDvopayrollauto.EmplCode;

                    if (objpayautosearchdata.Payroll_Date == Convert.ToDateTime(null))
                    {
                        objpayautosearchdata.Payroll_Date = DVOApplicationUserInfo.CurrentDate;
                    }
                    if (objpayautosearchdata.EOP_Date == Convert.ToDateTime(null))
                    {
                        objpayautosearchdata.EOP_Date = objpayautosearchdata.Payroll_Date;
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.deposit = "Y";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.deposit = "N";
                    }
                    //object nullTransacionObject = null;
                    //objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_doc_no", ref nullTransacionObject);
                    objPayrollProcess_PayEmployee.Doc_no = _currentDocNo + i + 1;

                    if (objPayrollProcess_PayEmployee.Doc_no == 0)
                    {
                        //rollback 
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                        return objDVOPayrollProcess_PayEmployeeList;
                    }

                    objPayrollProcess_PayEmployee.doc_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.pay_date = objpayautosearchdata.Payroll_Date;
                    objPayrollProcess_PayEmployee.eop_date = objpayautosearchdata.EOP_Date;
                    objPayrollProcess_PayEmployee.bonus = objpayautosearchdata.BonusCeck;
                    if (objPayrollProcess_PayEmployee.bonus == "Y")
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "N";
                        objPayrollProcess_PayEmployee.accrue_vac = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.accrue_sick = "Y";
                        objPayrollProcess_PayEmployee.accrue_vac = "Y";
                    }
                    if (objDvopayrollauto.DirDept == "Y")
                    {
                        objPayrollProcess_PayEmployee.print_check = "N";
                    }
                    else
                    {
                        objPayrollProcess_PayEmployee.print_check = "Y";
                    }
                    objPayrollProcess_PayEmployee.ok_to_post = "N";
                    objPayrollProcess_PayEmployee.StateTaxCode = objDvopayrollauto.StaTaxCode;
                    if (objDvopayrollauto.Flexdeptacctno == 0)
                    {
                        //set value from control table is there is no value for Employee cash account .
                        objDvopayrollauto.Flexdeptacctno = objstycntrcList[0].cash_acct;
                    }

                    objPayrollProcess_PayEmployee.Cash_acct_no = objDvopayrollauto.Flexdeptacctno;
                    objPayrollProcess_PayEmployee.Department = objDvopayrollauto.Department;
                    objPayrollIncomesglobal = LoadIncomes(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                    objPayrolldeductionsglobal = LoadDeductions(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                    objPayrollobligationsglobal = LoadObligations(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);

                    objPayrollProcess_PayEmployee = CalculatePayrolls(ref objPayrollProcess_PayEmployee);

                    // post the header record
                    bool re_status = InsertIntoProcess_PayEmployee(ref objPayrollProcess_PayEmployee, ref objTransaction);
                    if (!re_status)
                    {
                        ok_to_commit = false;
                    }
                    // post income detail                  
                    for (int j = 0; j < objPayrollIncomesglobal.Count; j++)
                    {
                        objPayrollIncomesglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool id_ststus = InsertIntoStypayid(objPayrollIncomesglobal[j], ref objTransaction);
                        if (!id_ststus)
                        {
                            ok_to_commit = false;
                        }
                    }
                    //post deduction detail
                    for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
                    {
                        objPayrolldeductionsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool dd_status = InsertIntoStypaydd(objPayrolldeductionsglobal[j], ref objTransaction);
                        if (!dd_status)
                        {
                            ok_to_commit = false;
                        }
                    }
                    //post obligation detail
                    for (int j = 0; j < objPayrollobligationsglobal.Count; j++)
                    {
                        objPayrollobligationsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                        bool od_ststus = InsertIntoStypayod(objPayrollobligationsglobal[j], ref objTransaction);
                        if (!od_ststus)
                        {
                            ok_to_commit = false;
                        }
                    }
                    if (ok_to_commit)
                    {
                        //coomitwork
                        objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                        objDVOPayrollProcess_PayEmployeeList.Add(objPayrollProcess_PayEmployee);
                    }
                    else
                    {
                        //rollback 
                        objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                    }

                    if (ok_to_commit)
                    {
                        objDVOPayrollProcess_PayEmployeeList.Add(objPayrollProcess_PayEmployee);
                    }
                }
                //coomitwork
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                ExceptionManagement.ExceptionManager.Publish(ex);
                objDVOPayrollProcess_PayEmployeeList.Clear();
                throw ex;
            }
            finally
            {
                if (objTransaction != null)
                    objTransaction = null;
            }
            return objDVOPayrollProcess_PayEmployeeList;
        }
        public DVOPayrollProcess_PayEmployee CalculatePayrolls(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee)
        {

            // bool dedflag = false;
            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objProcess_PayEmployee.cash_amount = 0.0M;
            objProcess_PayEmployee.inc_gross = 0.0M;
            objProcess_PayEmployee.inc_taxable = 0.0M;
            objProcess_PayEmployee.ded_fica = 0.0M;
            objProcess_PayEmployee.ded_medicare = 0.0M;
            objProcess_PayEmployee.ded_fedtax = 0.0M;
            objProcess_PayEmployee.ded_statax = 0.0M;
            objProcess_PayEmployee.ded_loctax = 0.0M;
            objProcess_PayEmployee.ded_other = 0.0M;
            objProcess_PayEmployee.obl_futa = 0.0M;
            objProcess_PayEmployee.obl_fica = 0.0M;
            objProcess_PayEmployee.obl_medicare = 0.0M;
            objProcess_PayEmployee.obl_other = 0.0M;
            objProcess_PayEmployee.obl_total = 0.0M;
            objProcess_PayEmployee.inc_net = 0.0M;
            objProcess_PayEmployee.inc_expense = 0.0M;
            objProcess_PayEmployee.total_hours = 0.0M;

            for (int i = 0; i < objPayrollIncomesglobal.Count; i++)
            {
                objProcess_PayEmployee = CalculateIncomes(ref objProcess_PayEmployee, i);
                if (objPayrollIncomesglobal[i].inc_type != "F")
                {
                    objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable + objPayrollIncomesglobal[i].amount;
                }
            }
            // set initial taxable & net income (gross cannot be less than zero)
            //
            //# Taxable income is derived from determining which income codes are to be taxed as
            //#  opposed to which deductions reduce the gross.  Only certain income codes are to
            //#  assessed SOC-SEC and LEVY taxes so we need to calculate thses separately.  These
            //#  are indicated with an "F" which indicates that the income code in question is exempt
            //#  these two taxes.
            //# let p_ypayre.inc_taxable = p_ypayre.inc_gross
            //##### ^^^^^ - - - -- - - - ^^^^^ ##########
            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross;


            // calculate deductions that reduce taxable income
            for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
            {
                if ((objPayrolldeductionsglobal[j].ded_taxred != "N") &&
                (objPayrolldeductionsglobal[j].ded_taxred != null))
                {
                    objPayrolldeductionsglobal[j].dedflag = true;
                    CalculateDeductions(ref objProcess_PayEmployee, j);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[j].amount;
                    // figure the effect on wage bases
                    switch (objPayrolldeductionsglobal[j].ded_taxred)
                    {
                        case "A":
                            {
                                // deduction reduces all wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable -
                                objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "B":
                            {
                                // deduction reduces taxable and fica wage bases

                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "C":
                            {
                                //deduction reduces taxable and futa wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "D":
                            {
                                // deduction reduces futa and fica wage bases
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "F":
                            {
                                // deduction reduces fica wage base only
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "T":
                            {
                                // deduction reduces taxable wage base only
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                break;
                            }
                        case "U":
                            {
                                // deduction reduces futa wage base only
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }

                    }
                }
                else
                {
                    objPayrolldeductionsglobal[j].dedflag = false;
                }


            }
            // calculate deductions that do not reduce taxable income
            for (int k = 0; k < objPayrolldeductionsglobal.Count; k++)
            {
                if (objPayrolldeductionsglobal[k].dedflag == false)
                {
                    CalculateDeductions(ref objProcess_PayEmployee, k);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[k].amount;
                }
            }

            // calculate obligations
            for (int k = 0; k < objPayrollobligationsglobal.Count; k++)
            {
                CalculateObligations(ref objProcess_PayEmployee, k);
            }
            //add final totals
            //adjust totals using overall deduction accumulation instead
            //of running net to take care of possible rounding errors
            objProcess_PayEmployee.cash_amount = objProcess_PayEmployee.inc_gross +
           objProcess_PayEmployee.inc_expense - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_fedtax + objProcess_PayEmployee.ded_medicare +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax +
           objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_medicare + objProcess_PayEmployee.ded_fedtax +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax + objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.obl_total = objProcess_PayEmployee.obl_futa + objProcess_PayEmployee.obl_fica +
            objProcess_PayEmployee.obl_medicare + objProcess_PayEmployee.obl_other;

            return objProcess_PayEmployee;
        }
        public DVOPayrollProcess_PayEmployee CalculateIncomes(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            //this function recalculates the income amount and resets the gross wages.

            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            //recalculate the amount
            objPayrollIncomesglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", (objPayrollIncomesglobal[i].inc_rate * objPayrollIncomesglobal[i].number)));

            // make sure amount is not null and non-negative
            if (objPayrollIncomesglobal[i].amount < 0)
            {
                objPayrollIncomesglobal[i].amount = 0.0M;
            }

            //add to the gross wages or expenses/advances
            switch (objPayrollIncomesglobal[i].inc_type)
            {
                case "H":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "E":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = objpayrollProcess_PayEmployee.inc_expense + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                case "A":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = objpayrollProcess_PayEmployee.inc_expense + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                case "F":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "U":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "B":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        break;
                    }
                default:
                    {
                        objpayrollProcess_PayEmployee.inc_gross = objpayrollProcess_PayEmployee.inc_gross + objPayrollIncomesglobal[i].amount;
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
            }

            objpayrollProcess_PayEmployee.total_hours = objpayrollProcess_PayEmployee.total_hours + objPayrollIncomesglobal[i].hours;

            return objpayrollProcess_PayEmployee;
        }

        public static void CalculateIncomeChanges(string IncomeCodeType, decimal IncomeAmount, out decimal GrossIncomeChange, out decimal TaxableIncomeChange, out decimal FicaWagesChange, out decimal FutaWagesChange)
        {
            GrossIncomeChange = 0;
            FicaWagesChange = 0;
            FutaWagesChange = 0;
            TaxableIncomeChange = 0;

            switch (IncomeCodeType)
            {
                case "H":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "E":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "A":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "F":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        break;
                    }
                case "U":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "B":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                default:
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
            }
        }

        public DVOPayrollProcess_PayEmployee CalculateDeductions(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_deductions = 0.0M;
            decimal currentdeductions = 0.0M;

            if (objPayrolldeductionsglobal[i].ded_type == null)
            {
                objPayrolldeductionsglobal[i].ded_type = "T";
            }
            //if (objPayrolldeductionsglobal[i].ded_type == null)
            //{
            //    objPayrolldeductionsglobal[i].ded_type = "T";
            //}
            //if (objPayrolldeductionsglobal[i].ded_type == null)
            //{
            //    objPayrolldeductionsglobal[i].ded_type = "T";
            //}
            // set the maximum deduction
            if (objPayrolldeductionsglobal[i].ded_limit == 0.0M)
            {
                Max_deductions = Convert.ToDecimal(999999999999.99);  // large decimal(12) value
            }
            else
            {
                objPayrolldeductionsglobal[i].ded_ytd = 0.0M;
                currentdeductions = 0;
                if (dup_ssn == 1)
                {
                    object[] parameter1 = new object[2];
                    parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    parameter1[1] = objPayrolldeductionsglobal[i].ded_code;
                    object ded_ytd = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter1, objPayrolldeductionsglobal[0].FIND_stypayddytd);
                    if (ded_ytd.ToString().Trim() != "")
                    {
                        objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd);
                    }
                }
                else
                {
                    //currentdeductions;
                    object[] parameter1 = new object[2];
                    parameter1[0] = empl_ssn;
                    parameter1[1] = objPayrolldeductionsglobal[i].ded_code;
                    object ded_ytd = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter1, objPayrolldeductionsglobal[0].FIND_stypayddytd1);
                    if (ded_ytd.ToString().Trim() != "")
                    {
                        objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd);
                    }

                    if (same_person)
                    {
                        object[] parameter2 = new object[3];
                        parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                        parameter1[1] = empl_ssn;
                        parameter1[2] = objPayrolldeductionsglobal[i].ded_code;
                        object ded_ytd1 = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter2, objPayrolldeductionsglobal[0].FIND_stypayddytd2);
                        if (ded_ytd1.ToString().Trim() != "")
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = Convert.ToDecimal(ded_ytd1);
                        }
                    }
                }


                if (currentdeductions != 0)
                {
                    objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + currentdeductions;
                }
                // get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrolldeductionsglobal[j].ded_code == objPayrolldeductionsglobal[i].ded_code)
                    {
                        if (objPayrolldeductionsglobal[j].amount != 0)
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + objPayrolldeductionsglobal[j].amount;
                        }
                    }
                }
                Max_deductions = (objPayrolldeductionsglobal[i].ded_limit - objPayrolldeductionsglobal[i].ded_ytd) ?? 0;//make nullable decimal By Rahul
            }
            if (Max_deductions < 0.0M)
            {
                Max_deductions = 0.0M;
            }
            //calc the amount of the deduction relative to type
            if (objPayrolldeductionsglobal[i].ded_code.Trim() == objpayrollProcess_PayEmployee.StateTaxCode.Trim())
            {
                //call the state tax calculation logic
                objPayrolldeductionsglobal[i].amount = state_calc(i);
            }
            switch (objPayrolldeductionsglobal[i].ded_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != 0))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_gross));
                            }
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod);//make nullable decimal By Rahul
                        }
                        break;
                    }

                case "T":
                    {
                        //calculate amount using taxable wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != Convert.ToDecimal(null)))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_taxable));
                            }
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod);//make nullable decimal By Rahul
                        }
                        break;
                    }
                case "U":
                    {
                        //calculate amount using futa wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * futa_wages));
                        }
                        break;
                    }
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * fica_wages));
                        }
                        break;
                    }
                case "H":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.total_hours));
                        break;
                    }
                case "N":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        break;
                    }
                default:
                    {
                        objPayrolldeductionsglobal[i].amount = 0.0M;
                        break;
                    }

            }


            //make sure deduction is not greater than net or zero if net < 0
            if (objPayrolldeductionsglobal[i].amount > 0)
            {
                if (objpayrollProcess_PayEmployee.inc_net < 0)
                {
                    objPayrolldeductionsglobal[i].amount = 0;
                }
                else
                {
                    if (objPayrolldeductionsglobal[i].amount > objpayrollProcess_PayEmployee.inc_net)
                    {
                        objPayrolldeductionsglobal[i].amount = objpayrollProcess_PayEmployee.inc_net;
                    }
                }
            }

            if (objPayrolldeductionsglobal[i].pay_limit != 0.0M)
            {
                if (objPayrolldeductionsglobal[i].amount > objPayrolldeductionsglobal[i].pay_limit)
                {
                    objPayrolldeductionsglobal[i].amount = objPayrolldeductionsglobal[i].pay_limit;
                }
            }
            // check for limit
            if (objPayrolldeductionsglobal[i].amount > Max_deductions)
            {
                objPayrolldeductionsglobal[i].amount = Max_deductions;
            }

            // post amount to correct total
            if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fedtax_code)
            {
                objpayrollProcess_PayEmployee.ded_fedtax = objpayrollProcess_PayEmployee.ded_fedtax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].statax_code)
            {
                objpayrollProcess_PayEmployee.ded_statax = objpayrollProcess_PayEmployee.ded_statax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].loctax_code)
            {
                objpayrollProcess_PayEmployee.ded_loctax = objpayrollProcess_PayEmployee.ded_loctax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.ded_fica = objpayrollProcess_PayEmployee.ded_fica + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].medicare_code)
            {
                objpayrollProcess_PayEmployee.ded_medicare = objpayrollProcess_PayEmployee.ded_medicare + objPayrolldeductionsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.ded_other = objpayrollProcess_PayEmployee.ded_other + objPayrolldeductionsglobal[i].amount;
            }



            return objpayrollProcess_PayEmployee;
        }
        public DVOPayrollProcess_PayEmployee CalculateObligations(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_Obligations = 0.0M;
            decimal Current_obligations = 0.0M;
            decimal obligationYTD = 0.0M;
            decimal Max_deductions = 0.0M;
            // this function calculates the obligation amount and updates
            //   cumulative totals for the control obligation codes

            // set obligation type
            if (objPayrollobligationsglobal[i].obl_type == null)
            {
                objPayrollobligationsglobal[i].obl_type = "T";
            }

            // get obligation limit and accrual

            // set the maximum obligation
            if (objPayrollobligationsglobal[i].obl_limit == 0.0M)
            {
                Max_deductions = 999999999.99M;  // large decimal(12) value
            }
            else
            {
                //check for current accrual
                //Current_obligations = 0.0M;
                if (dup_ssn == 1)
                {
                    //obligationYTD = 0.0M; //Set it with Year to date obligations
                    object[] parameter1 = new object[2];
                    parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    object Current_obl = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd);
                    if (Current_obl.ToString().Trim() != "")
                    {
                        objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obl);
                    }
                }
                else
                {
                    //obligationYTD = 0.0M; //Set it with Year to date obligations
                    object[] parameter1 = new object[2];
                    parameter1[0] = empl_ssn;
                    parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    object oblYTD = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd1);
                    if (oblYTD.ToString().Trim() != "")
                    {
                        objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(oblYTD);
                    }

                    if (same_person)
                    {
                        //Current_obligations = 0.0M; //set it with current obligations 
                        object[] parameter2 = new object[3];
                        parameter2[0] = objpayrollProcess_PayEmployee.EmplCode;
                        parameter2[1] = empl_ssn;
                        parameter2[2] = objPayrollobligationsglobal[i].obl_code;
                        object Current_obligations1 = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter2, objPayrollobligationsglobal[0].FIND_stypayodytd2);
                        if (Current_obligations1.ToString().Trim() != "")
                        {
                            objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obligations1);
                        }

                    }
                }

                if (Current_obligations != 0)
                {
                    objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + Current_obligations;//objPayrollobligationsglobal[i].amount;
                }
                //get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrollobligationsglobal[j].obl_code == objPayrollobligationsglobal[i].obl_code)
                    {
                        if (objPayrollobligationsglobal[j].amount != 0.0M)
                        {
                            objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + objPayrollobligationsglobal[j].amount;
                        }
                    }

                }
                Max_Obligations = (objPayrollobligationsglobal[i].obl_limit - objPayrollobligationsglobal[i].obl_ytd) ?? 0;//make nullable decimal By Rahul
            }
            if (Max_Obligations < 0)
            {
                Max_Obligations = 0.0M;
            }
            switch (objPayrollobligationsglobal[i].obl_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_gross));
                        }
                    }
                    break;
                case "T":
                    {
                        //calculate amount using taxable wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_taxable));
                        }
                    }
                    break;
                case "U":
                    //calculate amount using futa wage base
                    {
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_futa));
                        }
                    }
                    break;
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_fica));
                        }
                    }
                    break;
                case "E":
                    // Based on the employees deduction amount
                    {
                        for (int j = 0; j <= objPayrolldeductionsglobal.Count; j++)
                        {
                            if (objPayrollobligationsglobal[i].obl_code == objPayrolldeductionsglobal[j].ded_code)
                            {
                                objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objPayrolldeductionsglobal[j].amount));
                                break;
                            }
                        }
                    }
                    break;
                case "H":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.total_hours));
                    }
                    break;
                case "N":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                    }
                    break;
                    //default:
                    //    {
                    //        objPayrollobligationsglobal[i].amount = 0.0M;
                    //    }
            }

            //Modified by Sarvjeet On 13/08/2009
            if (objPayrollobligationsglobal[i].pay_limit > 0.0M)
            {

                if (objPayrollobligationsglobal[i].amount > objPayrollobligationsglobal[i].pay_limit)
                {

                    objPayrollobligationsglobal[i].amount = objPayrollobligationsglobal[i].pay_limit;

                }

            }
            //check for limit
            if (objPayrollobligationsglobal[i].amount > Max_Obligations)
            {
                objPayrollobligationsglobal[i].amount = Max_Obligations;
            }
            // post amount to correct total
            if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].futa_code)
            {
                objpayrollProcess_PayEmployee.obl_futa = objpayrollProcess_PayEmployee.obl_futa + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.obl_fica = objpayrollProcess_PayEmployee.obl_fica + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].medicare_ob_code)
            {
                objpayrollProcess_PayEmployee.obl_medicare = objpayrollProcess_PayEmployee.obl_medicare + objPayrollobligationsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.obl_other = objpayrollProcess_PayEmployee.obl_other + objPayrollobligationsglobal[i].amount;
            }

            //   DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            return objpayrollProcess_PayEmployee;
        }
        public List<DVOPayrollstypayid> LoadIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOUpdateTimeCard> objlistTimecard = new List<DVOUpdateTimeCard>();
            objlistTimecard = LoadTimeCardIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            List<DVOPayrollstypayid> objlistpayrollincomes = new List<DVOPayrollstypayid>();

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[1];
            Int32 Maxlineno = 0;
            if (objlistTimecard.Count < 1)
            {
                objlistTimecard = LoadEmployeeIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            }
            for (int i = 0; i < objlistTimecard.Count; i++)
            {
                DVOPayrollstypayid objemployeeincome = new DVOPayrollstypayid();
                objemployeeincome.inc_code = objlistTimecard[i].inc_code_id;
                objemployeeincome.inc_rate = objlistTimecard[i].inc_rate_id;
                objemployeeincome.number = objlistTimecard[i].inc_number_id;
                objemployeeincome.hours = objlistTimecard[i].inc_hours_id;
                objemployeeincome.add_code = objlistTimecard[i].add_code_cr;
                objemployeeincome.inc_type = objlistTimecard[i].inc_type_cr;
                if ((objemployeeincome.add_code == "Y") || (objemployeeincome.add_code == "Z"))
                {
                    parameters[0] = EmployeeCode;

                    object Maxlineno1 = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOPayrollautopay()).EMPLOYEEMAXLINENOGET);
                    if (Maxlineno1 == null)
                    {
                        Maxlineno = 0;

                    }
                    Maxlineno = Maxlineno + i;
                }
                else
                {
                    objemployeeincome.add_code = "N";
                    objemployeeincome.line_no = objlistTimecard[i].line_no_id;
                }
                objemployeeincome.lo_inc_amt = objlistTimecard[i].lo_inc_amt_id;
                objemployeeincome.hi_inc_amt = objlistTimecard[i].hi_inc_amt_id;
                // Take the timecard account number first.  Comment out the following
                // line here, but use it as a default if the timecard account number is null.
                // let p_ypayid[n].acct_no = inc_ref[n].acct_no
                objemployeeincome.acct_no = objlistTimecard[i].timecd_acct_no;
                objemployeeincome.Department = objlistTimecard[i].department_id;

                //Assign Default Values as required ........
                if (objemployeeincome.lo_inc_amt == 0.0M)
                {
                    objemployeeincome.lo_inc_amt = objlistTimecard[i].dflt_lo_inc_amt_cr;
                }
                if (objemployeeincome.hi_inc_amt == 0.0M)
                {
                    objemployeeincome.hi_inc_amt = objlistTimecard[i].dflt_hi_inc_amt_cr;
                }
                if (objemployeeincome.inc_rate == 0.0M)
                {
                    objemployeeincome.inc_rate = objlistTimecard[i].dflt_rate_cr;
                }
                if (objemployeeincome.hours == 0.0M)
                {
                    objemployeeincome.hours = objlistTimecard[i].dflt_hours_cr;
                }
                if (objemployeeincome.number == 0.0M)
                {
                    objemployeeincome.number = objlistTimecard[i].dflt_num_cr;
                }
                // use defaults if necessary

                if (objemployeeincome.acct_no == 0)
                {
                    objemployeeincome.acct_no = objlistTimecard[i].acct_no_id;
                    if (objemployeeincome.acct_no == 0)
                    {
                        objemployeeincome.acct_no = objlistTimecard[i].dflt_acct_cr;
                    }
                }
                if ((objemployeeincome.Department == string.Empty) || (objemployeeincome.Department == null))
                {
                    objemployeeincome.Department = objlistTimecard[i].dflt_dept_cr;
                }
                objlistpayrollincomes.Add(objemployeeincome);
            }
            return objlistpayrollincomes;
        }

        public List<DVOPayrollstypaydd> LoadDeductions(string EmployeeCode, DateTime EOPdate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollstypaydd> objListpayrollstypaydd = new List<DVOPayrollstypaydd>();
            List<DVOMasterEmployeeDeductions> objListemployeeDeddefaultdata = new List<DVOMasterEmployeeDeductions>();
            DVOPayrollstypaydd objpayrollstypaydd = new DVOPayrollstypaydd();
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Dedreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[0];

            if (dsAllDeductions != null && dsAllDeductions.Tables.Count > 0 && dsAllDeductions.Tables[0].Rows.Count > 0)
            {
                DataRow[] drdeductions = dsAllDeductions.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "'");
                if (drdeductions != null && drdeductions.Length > 0)
                    foreach (DataRow dr in drdeductions)
                    {
                        using (DVOMasterEmployeeDeductions objemployeedefaultdedrec = new DVOMasterEmployeeDeductions())
                        {
                            objemployeedefaultdedrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                            objemployeedefaultdedrec.dfltkeyvalue = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                            objemployeedefaultdedrec.ded_code = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                            objemployeedefaultdedrec.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                            objemployeedefaultdedrec.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
                            objemployeedefaultdedrec.ded_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
                            objemployeedefaultdedrec.pay_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                            objemployeedefaultdedrec.balanceamt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                            objemployeedefaultdedrec.ded_apply = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]).Trim() : string.Empty);
                            objemployeedefaultdedrec.lo_ded_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);
                            objemployeedefaultdedrec.hi_ded_amt = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0);
                            objemployeedefaultdedrec.acct_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
                            objemployeedefaultdedrec.department = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]).Trim() : "000");
                            objemployeedefaultdedrec.ded_ytd = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0);
                            objemployeedefaultdedrec.ded_date = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                            objemployeedefaultdedrec.description = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : string.Empty);
                            objemployeedefaultdedrec.ded_type = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]).Trim() : string.Empty);
                            objemployeedefaultdedrec.ded_taxred = (dr[17] != DBNull.Value ? Convert.ToString(dr[17]).Trim() : string.Empty);
                            objemployeedefaultdedrec.dflt_rate = (dr[18] != DBNull.Value ? Convert.ToDecimal(dr[18]) : 0.0M);
                            objemployeedefaultdedrec.dflt_limit = (dr[19] != DBNull.Value ? Convert.ToDecimal(dr[19]) : 0);
                            objemployeedefaultdedrec.dflt_pay_limit = (dr[20] != DBNull.Value ? Convert.ToDecimal(dr[20]) : 0);
                            objemployeedefaultdedrec.yearrollover = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : string.Empty);
                            objemployeedefaultdedrec.dflt_lo_ded_amt = (dr[22] != DBNull.Value ? Convert.ToDecimal(dr[22]) : 0);
                            objemployeedefaultdedrec.dflt_hi_ded_amt = (dr[23] != DBNull.Value ? Convert.ToDecimal(dr[23]) : 0);
                            objemployeedefaultdedrec.dflt_acct = (dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0);
                            objemployeedefaultdedrec.dflt_dept = (dr[25] != DBNull.Value ? Convert.ToString(dr[25]).Trim() : "000");
                            objemployeedefaultdedrec.dflt_apply = (dr[26] != DBNull.Value ? Convert.ToString(dr[26]).Trim() : string.Empty);
                            parameters = new object[2];
                            parameters[0] = objemployeedefaultdedrec.ded_code;
                            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            object taxcode = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objpaydatasearch.DeductionTaxCodeGet);
                            if (taxcode != DBNull.Value)
                            {
                                objemployeedefaultdedrec.tax_code = taxcode.ToString().Trim();
                            }

                            parameters = new object[2];
                            parameters[0] = objemployeedefaultdedrec.ded_code;
                            parameters[1] = EOPdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                            object result = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmployeeDeductions()).DeductioncodeTaxget);
                            if (result != null)
                                objemployeedefaultdedrec.tax_code = result.ToString().Trim();
                            if (objemployeedefaultdedrec.ded_apply == string.Empty)
                            {
                                objemployeedefaultdedrec.ded_apply = objemployeedefaultdedrec.dflt_apply;
                            }
                            // assign defaults as required
                            if (objemployeedefaultdedrec.lo_ded_amt == 0)
                            {
                                objemployeedefaultdedrec.lo_ded_amt = objemployeedefaultdedrec.dflt_lo_ded_amt;
                            }
                            if (objemployeedefaultdedrec.hi_ded_amt == 0)
                            {
                                objemployeedefaultdedrec.hi_ded_amt = objemployeedefaultdedrec.dflt_hi_ded_amt;
                            }
                            if (objemployeedefaultdedrec.ded_rate == 0)
                            {
                                objemployeedefaultdedrec.ded_rate = objemployeedefaultdedrec.dflt_rate;
                            }
                            if (objemployeedefaultdedrec.acct_no == 0)
                            {
                                objemployeedefaultdedrec.acct_no = objemployeedefaultdedrec.dflt_acct;
                            }
                            if (objemployeedefaultdedrec.department == null)
                            {
                                objemployeedefaultdedrec.department = objemployeedefaultdedrec.dflt_dept;
                            }
                            if (objemployeedefaultdedrec.ded_limit == 0)
                            {
                                objemployeedefaultdedrec.ded_limit = objemployeedefaultdedrec.dflt_limit;
                            }

                            objListemployeeDeddefaultdata.Add(objemployeedefaultdedrec);
                        }
                    }
            }



            //object[] parameters = new object[1];
            //parameters[0] = EmployeeCode;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeDeductions), (new DVOMasterEmployeeDeductions()).EmpDedanddefaults))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DVOMasterEmployeeDeductions objemployeedefaultdedrec = new DVOMasterEmployeeDeductions();

            //        objemployeedefaultdedrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.dfltkeyvalue = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.ded_code = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
            //        objemployeedefaultdedrec.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
            //        objemployeedefaultdedrec.ded_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
            //        objemployeedefaultdedrec.pay_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
            //        objemployeedefaultdedrec.balanceamt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
            //        objemployeedefaultdedrec.ded_apply = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.lo_ded_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);
            //        objemployeedefaultdedrec.hi_ded_amt = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0);
            //        objemployeedefaultdedrec.acct_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
            //        objemployeedefaultdedrec.department = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]).Trim() : "000");
            //        objemployeedefaultdedrec.ded_ytd = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0);
            //        objemployeedefaultdedrec.ded_date = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.description = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.ded_type = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.ded_taxred = (dr[17] != DBNull.Value ? Convert.ToString(dr[17]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.dflt_rate = (dr[18] != DBNull.Value ? Convert.ToDecimal(dr[18]) : 0.0M);
            //        objemployeedefaultdedrec.dflt_limit = (dr[19] != DBNull.Value ? Convert.ToDecimal(dr[19]) : 0);
            //        objemployeedefaultdedrec.dflt_pay_limit = (dr[20] != DBNull.Value ? Convert.ToDecimal(dr[20]) : 0);
            //        objemployeedefaultdedrec.yearrollover = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.dflt_lo_ded_amt = (dr[22] != DBNull.Value ? Convert.ToDecimal(dr[22]) : 0);
            //        objemployeedefaultdedrec.dflt_hi_ded_amt = (dr[23] != DBNull.Value ? Convert.ToDecimal(dr[23]) : 0);
            //        objemployeedefaultdedrec.dflt_acct = (dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0);
            //        objemployeedefaultdedrec.dflt_dept = (dr[25] != DBNull.Value ? Convert.ToString(dr[25]).Trim() : "000");
            //        objemployeedefaultdedrec.dflt_apply = (dr[26] != DBNull.Value ? Convert.ToString(dr[26]).Trim() : string.Empty);
            //        parameters = new object[2];
            //        parameters[0] = objemployeedefaultdedrec.ded_code;
            //        parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            //        object taxcode = objDalBaseClass.ExecuteScalar(ref parameters, objpaydatasearch.DeductionTaxCodeGet);
            //        if (taxcode != DBNull.Value)
            //        {
            //            objemployeedefaultdedrec.tax_code = taxcode.ToString().Trim();
            //        }

            //        parameters = new object[2];
            //        parameters[0] = objemployeedefaultdedrec.ded_code;
            //        parameters[1] = EOPdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

            //        object result = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeDeductions()).DeductioncodeTaxget);
            //        if (result != null)
            //            objemployeedefaultdedrec.tax_code = result.ToString().Trim();
            //        if (objemployeedefaultdedrec.ded_apply == string.Empty)
            //        {
            //            objemployeedefaultdedrec.ded_apply = objemployeedefaultdedrec.dflt_apply;
            //        }
            //        // assign defaults as required
            //        if (objemployeedefaultdedrec.lo_ded_amt == 0)
            //        {
            //            objemployeedefaultdedrec.lo_ded_amt = objemployeedefaultdedrec.dflt_lo_ded_amt;
            //        }
            //        if (objemployeedefaultdedrec.hi_ded_amt == 0)
            //        {
            //            objemployeedefaultdedrec.hi_ded_amt = objemployeedefaultdedrec.dflt_hi_ded_amt;
            //        }
            //        if (objemployeedefaultdedrec.ded_rate == 0)
            //        {
            //            objemployeedefaultdedrec.ded_rate = objemployeedefaultdedrec.dflt_rate;
            //        }
            //        if (objemployeedefaultdedrec.acct_no == 0)
            //        {
            //            objemployeedefaultdedrec.acct_no = objemployeedefaultdedrec.dflt_acct;
            //        }
            //        if (objemployeedefaultdedrec.department == null)
            //        {
            //            objemployeedefaultdedrec.department = objemployeedefaultdedrec.dflt_dept;
            //        }
            //        if (objemployeedefaultdedrec.ded_limit == 0)
            //        {
            //            objemployeedefaultdedrec.ded_limit = objemployeedefaultdedrec.dflt_limit;
            //        }


            //        objListemployeeDeddefaultdata.Add(objemployeedefaultdedrec);
            //    }
            //}

            Dedreccount = objListemployeeDeddefaultdata.Count;
            for (int i = 0; i < Dedreccount; i++)
            {
                objpayrollstypaydd = new DVOPayrollstypaydd();
                // check to make sure deduction should be taken now

                //if (objListemployeeDeddefaultdata[i].ded_apply != string.Empty)
                // {
                DateTime ded_date = Convert.ToDateTime(null);
                BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                if (objListemployeeDeddefaultdata[i].ded_date != string.Empty)
                {
                    ded_date = Convert.ToDateTime(objListemployeeDeddefaultdata[i].ded_date);
                }
                else
                {
                    ded_date = Convert.ToDateTime(null);
                }
                if (objpayrollfunctions.Pay_Frequency(objListemployeeDeddefaultdata[i].ded_apply, EOPdate, ded_date))
                {
                    objpayrollstypaydd.ded_rate = objListemployeeDeddefaultdata[i].ded_rate.GetValueOrDefault(0.0M);
                }
                else
                {
                    objListemployeeDeddefaultdata[i].ded_rate = 0.0M;
                }
                //  }
                objpayrollstypaydd.ded_code = objListemployeeDeddefaultdata[i].ded_code;
                objpayrollstypaydd.amount = 0;
                objpayrollstypaydd.lo_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].lo_ded_amt);
                objpayrollstypaydd.hi_ded_amt = Convert.ToDecimal(objListemployeeDeddefaultdata[i].hi_ded_amt);
                objpayrollstypaydd.ded_taxred = objListemployeeDeddefaultdata[i].ded_taxred;
                objpayrollstypaydd.acct_no = objListemployeeDeddefaultdata[i].acct_no;
                objpayrollstypaydd.Department = objListemployeeDeddefaultdata[i].department;
                objpayrollstypaydd.line_no = objListemployeeDeddefaultdata[i].line_no;
                objpayrollstypaydd.add_code = "N";
                objpayrollstypaydd.ded_type = objListemployeeDeddefaultdata[i].ded_type;
                if (objListemployeeDeddefaultdata[i].pay_limit == 0)
                {
                    objpayrollstypaydd.pay_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_pay_limit);
                }

                if (objListemployeeDeddefaultdata[i].yearrollover == "Y")
                {
                    if (objpayrollstypaydd.amount > Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt))
                    {
                        objpayrollstypaydd.amount = Convert.ToDecimal(objListemployeeDeddefaultdata[i].balanceamt);
                    }
                }
                if (objListemployeeDeddefaultdata[i].ded_limit == 0)
                {
                    objpayrollstypaydd.ded_limit = Convert.ToDecimal(objListemployeeDeddefaultdata[i].dflt_limit);
                }
                if (objpayrollstypaydd.ded_ytd == 0.0M)
                {
                    objpayrollstypaydd.ded_ytd = Convert.ToDecimal(objListemployeeDeddefaultdata[i].ded_ytd);
                }
                if (objpayrollstypaydd.tax_code == string.Empty)
                {
                    objpayrollstypaydd.tax_code = objListemployeeDeddefaultdata[i].tax_code;
                }
                objListpayrollstypaydd.Add(objpayrollstypaydd);
            }

            return objListpayrollstypaydd;
        }
        DataSet dsAllDeductions = new DataSet();
        public void LoadAllDeductions(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[9];
                parameters[0] = objpayautosearchdata.EmplCode;
                //parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[1] = objpayautosearchdata.SocSecNum;
                parameters[2] = objpayautosearchdata.FirstName;
                parameters[3] = objpayautosearchdata.LastName;
                parameters[4] = objpayautosearchdata.Employee_Type;
                parameters[5] = objpayautosearchdata.Job_Code;
                parameters[6] = "";
                parameters[7] = "";
                parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllDeductions = objDalBaseClass.GetData_ByTransaction(ref objTransaction, (new DVOMasterEmployeeDeductions()).FIND_EMPDEDANDDEFAULTS_QUERY(ref parameters));
            }
            catch (Exception ex) { }
        }

        public List<DVOPayrollStypayod> LoadObligations(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollStypayod> objListpayrollstypayod = new List<DVOPayrollStypayod>();
            List<DVOMasterEmployeeObligations> objListemployeeObldefaultdate = new List<DVOMasterEmployeeObligations>();

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Oblreccount;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            if (dsAllObligations != null && dsAllObligations.Tables.Count > 0 && dsAllObligations.Tables[0].Rows.Count > 0)
            {
                DataRow[] drobligations = dsAllObligations.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "'");
                if (drobligations != null && drobligations.Length > 0)
                    foreach (DataRow dr in drobligations)
                    {
                        //MasterOblCodes.dfltaccounttype,MasterOblCodes.dfltbaccounttype,MasterOblCodes.dfltbkeyvalue,
                        //MasterEmployeeObligations.obl_code,MasterEmployeeObligations.line_no,MasterEmployeeObligations.obl_rate,MasterEmployeeObligations.obl_limit,
                        //MasterEmployeeObligations.pay_limit,MasterEmployeeObligations.acct_no,MasterEmployeeObligations.department,MasterEmployeeObligations.bal_acct_no,
                        //MasterEmployeeObligations.bal_dept,MasterEmployeeObligations.obl_ytd,MasterOblCodes.description,
                        //MasterOblCodes.obl_type,MasterOblCodes.dflt_rate,MasterOblCodes.dflt_limit,
                        //MasterOblCodes.dflt_pay_limit,  MasterOblCodes.dflt_acct, MasterOblCodes.dflt_dept,
                        //MasterOblCodes.dflt_bacct,MasterOblCodes.dflt_bdept

                        using (DVOMasterEmployeeObligations objemployeedefaultoblrec = new DVOMasterEmployeeObligations())
                        {
                            objemployeedefaultoblrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                            objemployeedefaultoblrec.dfltbaccounttype = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                            objemployeedefaultoblrec.dfltbkeyvalue = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                            objemployeedefaultoblrec.obl_code = (dr[3] != DBNull.Value ? Convert.ToString(dr[3]).Trim() : string.Empty);
                            objemployeedefaultoblrec.line_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                            objemployeedefaultoblrec.obl_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                            objemployeedefaultoblrec.obl_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
                            objemployeedefaultoblrec.pay_limit = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
                            objemployeedefaultoblrec.acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                            objemployeedefaultoblrec.department = (dr[9] != DBNull.Value ? Convert.ToString(dr[9]).Trim() : "000");
                            objemployeedefaultoblrec.bal_acct_no = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                            objemployeedefaultoblrec.bal_dept = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                            objemployeedefaultoblrec.obl_ytd = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);
                            objemployeedefaultoblrec.description = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]).Trim() : string.Empty);
                            objemployeedefaultoblrec.obl_type = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                            objemployeedefaultoblrec.dflt_rate = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                            objemployeedefaultoblrec.dflt_limit = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0);
                            objemployeedefaultoblrec.dflt_pay_limit = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0);
                            objemployeedefaultoblrec.dflt_acct = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                            objemployeedefaultoblrec.dflt_dept = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                            objemployeedefaultoblrec.dflt_bacct = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);
                            objemployeedefaultoblrec.dflt_bdept = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : "000");
                            DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                            objflexsegcommon.AccountType = objemployeedefaultoblrec.dfltaccounttype;
                            objflexsegcommon.EntityType = "MasterOblCodes";
                            objflexsegcommon.Code = objemployeedefaultoblrec.obl_code;
                            objemployeedefaultoblrec.dfltbkeyvalue = BLLFlexSegCommon.Flexseg_Load_UsingTransaction(ref objTransaction, ref objflexsegcommon);

                            BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                            objpayrollfunctions.flxMix(objemployeedefaultoblrec.dfltaccounttype, objemployeedefaultoblrec.dfltbkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                            objemployeedefaultoblrec.acct_no = MixaccountNo;
                            // objemployeedefaultoblrec.dflt_acct
                            if ((objemployeedefaultoblrec.dflt_acct == 0) &&
                             (objemployeedefaultoblrec.acct_no == 0))
                            {
                                //Test if the Account Exists in the table or not with th ekeyvalue we got now .
                                // scratch will contain the description after testFlexAccountKey
                                //create PayrollGLAccounts Flex account Entry
                            }
                            objListemployeeObldefaultdate.Add(objemployeedefaultoblrec);
                        }
                    }
            }


            //object[] parameters = new object[1];
            //parameters[0] = EmployeeCode;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeObligations), (new DVOMasterEmployeeObligations()).Emplobldefaultsget))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        //MasterOblCodes.dfltaccounttype,MasterOblCodes.dfltbaccounttype,MasterOblCodes.dfltbkeyvalue,
            //        //MasterEmployeeObligations.obl_code,MasterEmployeeObligations.line_no,MasterEmployeeObligations.obl_rate,MasterEmployeeObligations.obl_limit,
            //        //MasterEmployeeObligations.pay_limit,MasterEmployeeObligations.acct_no,MasterEmployeeObligations.department,MasterEmployeeObligations.bal_acct_no,
            //        //MasterEmployeeObligations.bal_dept,MasterEmployeeObligations.obl_ytd,MasterOblCodes.description,
            //        //MasterOblCodes.obl_type,MasterOblCodes.dflt_rate,MasterOblCodes.dflt_limit,
            //        //MasterOblCodes.dflt_pay_limit,  MasterOblCodes.dflt_acct, MasterOblCodes.dflt_dept,
            //        //MasterOblCodes.dflt_bacct,MasterOblCodes.dflt_bdept

            //        DVOMasterEmployeeObligations objemployeedefaultoblrec = new DVOMasterEmployeeObligations();
            //        objemployeedefaultoblrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.dfltbaccounttype = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.dfltbkeyvalue = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.obl_code = (dr[3] != DBNull.Value ? Convert.ToString(dr[3]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.line_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
            //        objemployeedefaultoblrec.obl_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
            //        objemployeedefaultoblrec.obl_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
            //        objemployeedefaultoblrec.pay_limit = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
            //        objemployeedefaultoblrec.acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
            //        objemployeedefaultoblrec.department = (dr[9] != DBNull.Value ? Convert.ToString(dr[9]).Trim() : "000");
            //        objemployeedefaultoblrec.bal_acct_no = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
            //        objemployeedefaultoblrec.bal_dept = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
            //        objemployeedefaultoblrec.obl_ytd = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);
            //        objemployeedefaultoblrec.description = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.obl_type = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.dflt_rate = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
            //        objemployeedefaultoblrec.dflt_limit = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0);
            //        objemployeedefaultoblrec.dflt_pay_limit = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0);
            //        objemployeedefaultoblrec.dflt_acct = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
            //        objemployeedefaultoblrec.dflt_dept = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
            //        objemployeedefaultoblrec.dflt_bacct = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);
            //        objemployeedefaultoblrec.dflt_bdept = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : "000");
            //        DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
            //        objflexsegcommon.AccountType = objemployeedefaultoblrec.dfltaccounttype;
            //        objflexsegcommon.EntityType = "MasterOblCodes";
            //        objflexsegcommon.Code = objemployeedefaultoblrec.obl_code;
            //        objemployeedefaultoblrec.dfltbkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

            //        BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
            //        objpayrollfunctions.flxMix(objemployeedefaultoblrec.dfltaccounttype, objemployeedefaultoblrec.dfltbkeyvalue, FlexacctType, FlexDepartment, out  MixaccountNo, out MixAcctType, out Mixkeyvalue);
            //        objemployeedefaultoblrec.acct_no = MixaccountNo;
            //        // objemployeedefaultoblrec.dflt_acct
            //        if ((objemployeedefaultoblrec.dflt_acct == 0) &&
            //         (objemployeedefaultoblrec.acct_no == 0))
            //        {
            //            //Test if the Account Exists in the table or not with th ekeyvalue we got now .
            //            // scratch will contain the description after testFlexAccountKey
            //            //create PayrollGLAccounts Flex account Entry
            //        }
            //        objListemployeeObldefaultdate.Add(objemployeedefaultoblrec);
            //    }
            //}

            Oblreccount = objListemployeeObldefaultdate.Count;
            for (Int32 i = 0; i < Oblreccount; i++)
            {
                DVOPayrollStypayod objpayrollstypayod = new DVOPayrollStypayod();
                objpayrollstypayod.obl_code = objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].obl_rate;
                objpayrollstypayod.amount = 0;//objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].acct_no;
                objpayrollstypayod.Department = objListemployeeObldefaultdate[i].department;
                objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].bal_acct_no;
                objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].bal_dept;
                objpayrollstypayod.line_no = objListemployeeObldefaultdate[i].line_no;
                objpayrollstypayod.add_code = "N";
                objpayrollstypayod.obl_type = objListemployeeObldefaultdate[i].obl_type;
                // assign defaults as required

                if (objpayrollstypayod.obl_rate == 0.0M)
                {
                    objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].dflt_rate;
                }
                if (objpayrollstypayod.acct_no == 0)
                {
                    objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].dflt_acct;
                }
                if ((objpayrollstypayod.Department == "000") || (objpayrollstypayod.Department == null))
                {
                    objpayrollstypayod.Department = objListemployeeObldefaultdate[i].dflt_dept;
                }
                if (objpayrollstypayod.bal_acct_no == 0)
                {
                    objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].dflt_bacct;
                }
                if ((objpayrollstypayod.bal_dept == "000") || (objpayrollstypayod.bal_dept == null))
                {
                    objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].dflt_bdept;
                }
                if (objListemployeeObldefaultdate[i].pay_limit == 0)
                {
                    objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_pay_limit);
                }
                else
                {
                    objpayrollstypayod.pay_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].pay_limit);
                }
                if (objListemployeeObldefaultdate[i].obl_limit == 0)
                {
                    objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].dflt_limit);
                }
                else
                {
                    objpayrollstypayod.obl_limit = Convert.ToDecimal(objListemployeeObldefaultdate[i].obl_limit);
                }
                objListpayrollstypayod.Add(objpayrollstypayod);
            }
            return objListpayrollstypayod;
        }
        DataSet dsAllObligations = new DataSet();
        public void LoadAllObligations(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[9];
                parameters[0] = objpayautosearchdata.EmplCode;
                //parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[1] = objpayautosearchdata.SocSecNum;
                parameters[2] = objpayautosearchdata.FirstName;
                parameters[3] = objpayautosearchdata.LastName;
                parameters[4] = objpayautosearchdata.Employee_Type;
                parameters[5] = objpayautosearchdata.Job_Code;
                parameters[6] = "";
                parameters[7] = "";
                parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllObligations = objDalBaseClass.GetData_ByTransaction(ref objTransaction, (new DVOMasterEmployeeObligations()).FIND_EMPLOBLDEFAULTSGET_QUERY(ref parameters));
            }
            catch (Exception ex) { }
        }

        public List<DVOUpdateTimeCard> LoadTimeCardIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            //DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();
            object[] parameters = new object[0];

            if (dsAllTimeCardIncomes != null && dsAllTimeCardIncomes.Tables.Count > 0 && dsAllTimeCardIncomes.Tables[0].Rows.Count > 0)
            {
                DataRow[] drtimecardincomes = dsAllTimeCardIncomes.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "' and start_date <= '" + EopDate + "'");//.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) + "'");
                if (drtimecardincomes != null && drtimecardincomes.Length > 0)
                    foreach (DataRow dr in drtimecardincomes)
                    {
                        using (DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard())
                        {
                            objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                            objUpdatetimecardincome.dfltkeyvalue = "";
                            objUpdatetimecardincome.acct_no_id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                            objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                            objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                            objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                            objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                            objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
                            objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
                            objUpdatetimecardincome.description_cr = dr[8].ToString().Trim();
                            objUpdatetimecardincome.dflt_num_cr = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
                            objUpdatetimecardincome.dflt_rate_cr = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0.0M);
                            objUpdatetimecardincome.dflt_hours_cr = (dr[11] != DBNull.Value ? Convert.ToDecimal(dr[11]) : 0.0M);
                            objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                            objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                            objUpdatetimecardincome.dflt_acct_cr = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
                            objUpdatetimecardincome.dflt_dept_cr = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : "000");
                            objUpdatetimecardincome.inc_type_cr = dr[16].ToString().Trim();

                            DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                            objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                            objflexsegcommon.EntityType = "MasterIncCodes";
                            objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                            objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load_UsingTransaction(ref objTransaction, ref objflexsegcommon);

                            BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                            objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                            objUpdatetimecardincome.dflt_acct_cr = MixaccountNo;
                            parameters = new object[3];
                            parameters[0] = EmployeeCode;
                            parameters[1] = objUpdatetimecardincome.inc_code_id;
                            parameters[2] = objUpdatetimecardincome.line_no_id;
                            DataSet dst = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmployeeIncomes()).EMPLOYEEINCOME_AUTOPAY);
                            if (dst.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow drt in dst.Tables[0].Rows)
                                {
                                    objUpdatetimecardincome.lo_inc_amt_id = (drt[0] != DBNull.Value ? Convert.ToDecimal(drt[0]) : 0.0M); ;
                                    objUpdatetimecardincome.hi_inc_amt_id = (drt[1] != DBNull.Value ? Convert.ToDecimal(drt[1]) : 0.0M); ;
                                    objUpdatetimecardincome.acct_no_id = (drt[2] != DBNull.Value ? Convert.ToInt32(drt[2]) : 0);
                                    objUpdatetimecardincome.department_id = (drt[3] != DBNull.Value ? Convert.ToString(drt[3]).Trim() : "000"); ;
                                }

                                if (((objUpdatetimecardincome.timecd_acct_no == 0) || (objUpdatetimecardincome.timecd_acct_no == null)) &&
                                ((objUpdatetimecardincome.acct_no_id == 0) || (objUpdatetimecardincome.acct_no_id == null)) &&
                               ((objUpdatetimecardincome.dflt_acct_cr == 0) || (objUpdatetimecardincome.dflt_acct_cr == null)))
                                {
                                    //test and create flex key records 

                                }
                                objUpdatetimecardincome.add_code_cr = "N";
                            }
                            else
                            {
                                // does the code already exist at the employee level?

                                parameters = new object[2];
                                parameters[0] = objUpdatetimecardincome.inc_code_id;
                                parameters[1] = EmployeeCode;
                                dupempinccount = Convert.ToInt32(objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmployeeIncomes()).EMPLINCOMECNT_AUTOPAY));
                                if (dupempinccount >= 1)
                                {
                                    objUpdatetimecardincome.add_code_cr = "Z";
                                }
                                else
                                {
                                    objUpdatetimecardincome.add_code_cr = "Y";
                                }
                            }
                            if (lastcard == objUpdatetimecardincome.card_no)
                            {

                            }
                            else
                            {
                                lastcard = objUpdatetimecardincome.card_no;
                                newRec = true;
                                //   objListtimecard.Add(objUpdatetimecardincome);
                            }

                            Timecarddetail = objListtimecard.Count;
                            for (int i = 0; i < Timecarddetail; i++)
                            {
                                if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                                    (objUpdatetimecardincome.inc_rate_id == objListtimecard[i].inc_rate_id))
                                {
                                    objListtimecard[i].inc_hours_id = objListtimecard[i].inc_hours_id + objUpdatetimecardincome.inc_hours_id;
                                    objListtimecard[i].inc_number_id = objListtimecard[i].inc_number_id + objUpdatetimecardincome.inc_number_id;
                                    newRec = false;
                                    break;
                                }
                                else
                                {
                                    // If the code is the same as the employee entry
                                    // but the rate differs we do not want to append
                                    if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                                      (objUpdatetimecardincome.line_no_id == objListtimecard[i].line_no_id))
                                    {
                                        if ((objUpdatetimecardincome.inc_number_id == 0) && (objListtimecard[i].inc_number_id != 0))
                                        {
                                            objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                            objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                            newRec = false;
                                            break;
                                        }
                                        else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id != 0))
                                        {
                                            objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                            objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                            objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                            newRec = false;
                                            break;
                                        }
                                        else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id == 0))
                                        {
                                            objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                            objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                            objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                            newRec = false;
                                            break;
                                        }
                                        else
                                        {
                                            //This code is a duplicate but the rate differs
                                            // and the number is not zero.
                                            objUpdatetimecardincome.add_code_cr = "Z";
                                            // newRec = false;
                                        }

                                    }
                                }
                            }
                            if (newRec == true)
                            {
                                objListtimecard.Add(objUpdatetimecardincome);
                                //objUpdatetimecardincome = new DVOUpdateTimeCard();
                            }
                        }
                    }
            }


            //object[] parameters = new object[2];
            //parameters[0] = EmployeeCode;
            //parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), (new DVOUpdateTimeCard()).FIND_DETAILS_FORAUTOPAY))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
            //        objUpdatetimecardincome.dfltkeyvalue = "";
            //        objUpdatetimecardincome.acct_no_id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
            //        objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
            //        objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
            //        objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
            //        objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
            //        objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
            //        objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
            //        objUpdatetimecardincome.description_cr = dr[8].ToString().Trim();
            //        objUpdatetimecardincome.dflt_num_cr = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
            //        objUpdatetimecardincome.dflt_rate_cr = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0.0M);
            //        objUpdatetimecardincome.dflt_hours_cr = (dr[11] != DBNull.Value ? Convert.ToDecimal(dr[11]) : 0.0M);
            //        objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
            //        objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
            //        objUpdatetimecardincome.dflt_acct_cr = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
            //        objUpdatetimecardincome.dflt_dept_cr = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : "000");
            //        objUpdatetimecardincome.inc_type_cr = dr[16].ToString().Trim();

            //        DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
            //        objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
            //        objflexsegcommon.EntityType = "MasterIncCodes";
            //        objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
            //        objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

            //        BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
            //        objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
            //        objUpdatetimecardincome.dflt_acct_cr = MixaccountNo;
            //        parameters = new object[3];
            //        parameters[0] = EmployeeCode;
            //        parameters[1] = objUpdatetimecardincome.inc_code_id;
            //        parameters[2] = objUpdatetimecardincome.line_no_id;
            //        DataSet dst = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeIncomes), (new DVOMasterEmployeeIncomes()).EMPLOYEEINCOME_AUTOPAY);
            //        if (dst.Tables[0].Rows.Count > 0)
            //        {
            //            foreach (DataRow drt in dst.Tables[0].Rows)
            //            {
            //                objUpdatetimecardincome.lo_inc_amt_id = (drt[0] != DBNull.Value ? Convert.ToDecimal(drt[0]) : 0.0M); ;
            //                objUpdatetimecardincome.hi_inc_amt_id = (drt[1] != DBNull.Value ? Convert.ToDecimal(drt[1]) : 0.0M); ;
            //                objUpdatetimecardincome.acct_no_id = (drt[2] != DBNull.Value ? Convert.ToInt32(drt[2]) : 0);
            //                objUpdatetimecardincome.department_id = (drt[3] != DBNull.Value ? Convert.ToString(drt[3]).Trim() : "000"); ;
            //            }

            //            if (((objUpdatetimecardincome.timecd_acct_no == 0) || (objUpdatetimecardincome.timecd_acct_no == null)) &&
            //            ((objUpdatetimecardincome.acct_no_id == 0) || (objUpdatetimecardincome.acct_no_id == null)) &&
            //           ((objUpdatetimecardincome.dflt_acct_cr == 0) || (objUpdatetimecardincome.dflt_acct_cr == null)))
            //            {
            //                //test and create flex key records 

            //            }
            //            objUpdatetimecardincome.add_code_cr = "N";
            //        }
            //        else
            //        {
            //            // does the code already exist at the employee level?

            //            parameters = new object[2];
            //            parameters[0] = objUpdatetimecardincome.inc_code_id;
            //            parameters[1] = EmployeeCode;
            //            dupempinccount = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeIncomes()).EMPLINCOMECNT_AUTOPAY));
            //            if (dupempinccount >= 1)
            //            {
            //                objUpdatetimecardincome.add_code_cr = "Z";
            //            }
            //            else
            //            {
            //                objUpdatetimecardincome.add_code_cr = "Y";
            //            }
            //        }
            //        if (lastcard == objUpdatetimecardincome.card_no)
            //        {

            //        }
            //        else
            //        {
            //            lastcard = objUpdatetimecardincome.card_no;
            //            newRec = true;
            //            //   objListtimecard.Add(objUpdatetimecardincome);
            //        }

            //        Timecarddetail = objListtimecard.Count;
            //        for (int i = 0; i < Timecarddetail; i++)
            //        {
            //            if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
            //                (objUpdatetimecardincome.inc_rate_id == objListtimecard[i].inc_rate_id))
            //            {
            //                objListtimecard[i].inc_hours_id = objListtimecard[i].inc_hours_id + objUpdatetimecardincome.inc_hours_id;
            //                objListtimecard[i].inc_number_id = objListtimecard[i].inc_number_id + objUpdatetimecardincome.inc_number_id;
            //                newRec = false;
            //                break;
            //            }
            //            else
            //            {
            //                // If the code is the same as the employee entry
            //                // but the rate differs we do not want to append
            //                if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
            //                  (objUpdatetimecardincome.line_no_id == objListtimecard[i].line_no_id))
            //                {
            //                    if ((objUpdatetimecardincome.inc_number_id == 0) && (objListtimecard[i].inc_number_id != 0))
            //                    {
            //                        objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
            //                        objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
            //                        newRec = false;
            //                        break;
            //                    }
            //                    else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id != 0))
            //                    {
            //                        objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
            //                        objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
            //                        objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
            //                        newRec = false;
            //                        break;
            //                    }
            //                    else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id == 0))
            //                    {
            //                        objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
            //                        objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
            //                        objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
            //                        newRec = false;
            //                        break;
            //                    }
            //                    else
            //                    {
            //                        //This code is a duplicate but the rate differs
            //                        // and the number is not zero.
            //                        objUpdatetimecardincome.add_code_cr = "Z";
            //                        // newRec = false;
            //                    }

            //                }
            //            }
            //        }
            //        if (newRec == true)
            //        {
            //            objListtimecard.Add(objUpdatetimecardincome);
            //            objUpdatetimecardincome = new DVOUpdateTimeCard();
            //        }

            //    }
            //}


            // If duplicate codes were added on the fly to the timecard, we
            // only want to append one to the employee entry at posting time.
            Timecarddetail = objListtimecard.Count;
            for (int j = 0; j < Timecarddetail; j++)
            {
                if (objListtimecard[j].add_code_cr == "Y")
                {
                    for (int k = 0; k < Timecarddetail; k++)
                    {
                        if (j == k)
                        {
                        }
                        else
                        {
                            if (objListtimecard[j].inc_code_id == objListtimecard[k].inc_code_id)
                            {
                                objListtimecard[j].add_code_cr = "Z";
                            }
                        }
                    }
                }
            }
            return objListtimecard;
        }
        DataSet dsAllTimeCardIncomes = new DataSet();
        public void LoadAllTimeCardIncomes(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[9];
                parameters[0] = objpayautosearchdata.EmplCode;
                //parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[1] = objpayautosearchdata.SocSecNum;
                parameters[2] = objpayautosearchdata.FirstName;
                parameters[3] = objpayautosearchdata.LastName;
                parameters[4] = objpayautosearchdata.Employee_Type;
                parameters[5] = objpayautosearchdata.Job_Code;
                parameters[6] = "";
                parameters[7] = "";
                parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllTimeCardIncomes = objDalBaseClass.GetData_ByTransaction(ref objTransaction, (new DVOUpdateTimeCard()).FIND_DETAILS_FORAUTOPAY_QUERY(ref parameters));
            }
            catch (Exception ex) { }
        }

        public List<DVOUpdateTimeCard> LoadEmployeeIncomes(string EmployeeCode, DateTime Eopdate, string FlexacctType, string FlexDepartment)
        {
            //This function loads the default income data from the employee
            //reference tables into an income reference array and reset the array
            // count.
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixAcctNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();

            if (dsAllEmployeeIncomes != null && dsAllEmployeeIncomes.Tables.Count > 0 && dsAllEmployeeIncomes.Tables[0].Rows.Count > 0)
            {
                DataRow[] dremployeeincomes = dsAllEmployeeIncomes.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "'");
                if (dremployeeincomes != null && dremployeeincomes.Length > 0)
                    foreach (DataRow dr in dremployeeincomes)
                    {
                        using (DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard())
                        {
                            objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                            objUpdatetimecardincome.dfltkeyvalue = "";
                            objUpdatetimecardincome.timecd_acct_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                            objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                            objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                            objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                            objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
                            objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
                            objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
                            objUpdatetimecardincome.lo_inc_amt_id = (dr[8] != DBNull.Value ? Convert.ToDecimal(dr[8]) : 0.0M);
                            objUpdatetimecardincome.hi_inc_amt_id = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
                            objUpdatetimecardincome.acct_no_id = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                            objUpdatetimecardincome.department_id = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                            objUpdatetimecardincome.description_cr = dr[12].ToString().Trim();
                            objUpdatetimecardincome.dflt_num_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                            objUpdatetimecardincome.dflt_rate_cr = (dr[14] != DBNull.Value ? Convert.ToDecimal(dr[14]) : 0.0M);
                            objUpdatetimecardincome.dflt_hours_cr = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                            objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                            objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0.0M);
                            objUpdatetimecardincome.dflt_acct_cr = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                            objUpdatetimecardincome.dflt_dept_cr = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                            objUpdatetimecardincome.inc_type_cr = dr[20].ToString().Trim();
                            DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                            objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                            objflexsegcommon.EntityType = "MasterIncCodes";
                            objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                            objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load_UsingTransaction(ref objTransaction, ref objflexsegcommon);

                            BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                            objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixAcctNo, out MixAcctType, out Mixkeyvalue);
                            objUpdatetimecardincome.dflt_acct_cr = MixAcctNo;
                            if (objUpdatetimecardincome.timecd_acct_no == 0 && objUpdatetimecardincome.acct_no_id == 0 && objUpdatetimecardincome.dflt_acct_cr == 0)
                            {
                                //test and create flex key records 
                            }
                            objListtimecard.Add(objUpdatetimecardincome);
                        }
                    }
            }




            //object[] parameters = new object[1];
            //parameters[0] = EmployeeCode;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollautopay), (new DVOPayrollautopay()).EmployeeIncomerefer))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
            //        objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
            //        objUpdatetimecardincome.dfltkeyvalue = "";
            //        objUpdatetimecardincome.timecd_acct_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
            //        objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
            //        objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
            //        objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
            //        objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
            //        objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
            //        objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
            //        objUpdatetimecardincome.lo_inc_amt_id = (dr[8] != DBNull.Value ? Convert.ToDecimal(dr[8]) : 0.0M);
            //        objUpdatetimecardincome.hi_inc_amt_id = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
            //        objUpdatetimecardincome.acct_no_id = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
            //        objUpdatetimecardincome.department_id = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
            //        objUpdatetimecardincome.description_cr = dr[12].ToString().Trim();
            //        objUpdatetimecardincome.dflt_num_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
            //        objUpdatetimecardincome.dflt_rate_cr = (dr[14] != DBNull.Value ? Convert.ToDecimal(dr[14]) : 0.0M);
            //        objUpdatetimecardincome.dflt_hours_cr = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
            //        objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
            //        objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0.0M);
            //        objUpdatetimecardincome.dflt_acct_cr = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
            //        objUpdatetimecardincome.dflt_dept_cr = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
            //        objUpdatetimecardincome.inc_type_cr = dr[20].ToString().Trim();
            //        DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
            //        objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
            //        objflexsegcommon.EntityType = "MasterIncCodes";
            //        objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
            //        objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

            //        BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
            //        objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out  MixAcctNo, out MixAcctType, out Mixkeyvalue);
            //        objUpdatetimecardincome.dflt_acct_cr = MixAcctNo;
            //        if (objUpdatetimecardincome.timecd_acct_no == 0 && objUpdatetimecardincome.acct_no_id == 0 && objUpdatetimecardincome.dflt_acct_cr == 0)
            //        {
            //            //test and create flex key records 

            //        }
            //        objListtimecard.Add(objUpdatetimecardincome);
            //    }
            //}
            return objListtimecard;

        }
        DataSet dsAllEmployeeIncomes = new DataSet();
        public void LoadAllEmployeeIncomes(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[9];
                parameters[0] = objpayautosearchdata.EmplCode;
                //parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[1] = objpayautosearchdata.SocSecNum;
                parameters[2] = objpayautosearchdata.FirstName;
                parameters[3] = objpayautosearchdata.LastName;
                parameters[4] = objpayautosearchdata.Employee_Type;
                parameters[5] = objpayautosearchdata.Job_Code;
                parameters[6] = "";
                parameters[7] = "";
                parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllEmployeeIncomes = objDalBaseClass.GetData_ByTransaction(ref objTransaction, (new DVOPayrollautopay()).FIND_EMPLOYEEINCOMEREFER_QUERY(ref parameters));
            }
            catch (Exception ex) { }
        }

        public static bool InsertIntoProcess_PayEmployee(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee, ref object objTrx)
        {
            try
            {
                if (objProcess_PayEmployee.bonus.Trim().Length <= 0)
                    objProcess_PayEmployee.bonus = "N";

                object[] parameters = new object[31];
                parameters[0] = objProcess_PayEmployee.Doc_no;
                parameters[1] = objProcess_PayEmployee.EmplCode;
                parameters[2] = objProcess_PayEmployee.doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[3] = objProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[4] = objProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[5] = objProcess_PayEmployee.print_check;
                parameters[6] = objProcess_PayEmployee.Cash_acct_no;
                parameters[7] = objProcess_PayEmployee.Department;
                parameters[8] = objProcess_PayEmployee.cash_amount;
                parameters[9] = objProcess_PayEmployee.check_no;
                parameters[10] = objProcess_PayEmployee.inc_gross;
                parameters[11] = objProcess_PayEmployee.ded_fica;
                parameters[12] = objProcess_PayEmployee.inc_taxable;
                parameters[13] = objProcess_PayEmployee.ded_medicare;
                parameters[14] = objProcess_PayEmployee.ded_fedtax;
                parameters[15] = objProcess_PayEmployee.ded_statax;
                parameters[16] = objProcess_PayEmployee.ded_loctax;
                parameters[17] = objProcess_PayEmployee.ded_other;
                parameters[18] = objProcess_PayEmployee.obl_futa;
                parameters[19] = objProcess_PayEmployee.obl_fica;
                parameters[20] = objProcess_PayEmployee.obl_medicare;
                parameters[21] = objProcess_PayEmployee.obl_other;
                parameters[22] = objProcess_PayEmployee.obl_total;
                parameters[23] = objProcess_PayEmployee.inc_net;
                parameters[24] = objProcess_PayEmployee.inc_expense;
                parameters[25] = objProcess_PayEmployee.total_hours;
                parameters[26] = objProcess_PayEmployee.ok_to_post;
                parameters[27] = objProcess_PayEmployee.accrue_sick;
                parameters[28] = objProcess_PayEmployee.accrue_vac;
                parameters[29] = objProcess_PayEmployee.bonus;
                parameters[30] = objProcess_PayEmployee.deposit;

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objProcess_PayEmployee.FIND_INSERT_Process_PayEmployee, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertIntoStypayid(DVOPayrollstypayid objDVOPayrollstypayid, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[13];
                parameters[0] = objDVOPayrollstypayid.Doc_no;
                parameters[1] = objDVOPayrollstypayid.line_no;
                parameters[2] = objDVOPayrollstypayid.inc_code.Trim();
                parameters[3] = objDVOPayrollstypayid.inc_rate;
                parameters[4] = objDVOPayrollstypayid.number;
                parameters[5] = objDVOPayrollstypayid.hours;
                parameters[6] = objDVOPayrollstypayid.amount;
                parameters[7] = objDVOPayrollstypayid.acct_no;
                parameters[8] = objDVOPayrollstypayid.Department.Trim();
                parameters[9] = objDVOPayrollstypayid.mod_flag;
                parameters[10] = objDVOPayrollstypayid.add_code.Trim();
                parameters[11] = objDVOPayrollstypayid.lo_inc_amt;
                parameters[12] = objDVOPayrollstypayid.hi_inc_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypayid.INSERT_STYPAYID, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool InsertIntoStypaydd(DVOPayrollstypaydd objDVOPayrollstypaydd, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollstypaydd.Doc_no;
                parameters[1] = objDVOPayrollstypaydd.line_no;
                parameters[2] = objDVOPayrollstypaydd.ded_code.Trim();
                parameters[3] = objDVOPayrollstypaydd.ded_rate;
                parameters[4] = objDVOPayrollstypaydd.amount;
                parameters[5] = objDVOPayrollstypaydd.acct_no;
                parameters[6] = objDVOPayrollstypaydd.Department.Trim();
                parameters[7] = objDVOPayrollstypaydd.mod_flag;
                parameters[8] = objDVOPayrollstypaydd.add_code.Trim();
                parameters[9] = objDVOPayrollstypaydd.lo_ded_amt;
                parameters[10] = objDVOPayrollstypaydd.hi_ded_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypaydd.INSERT_STYPARDD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertIntoStypayod(DVOPayrollStypayod objDVOPayrollStypayod, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollStypayod.Doc_no;
                parameters[1] = objDVOPayrollStypayod.line_no;
                parameters[2] = objDVOPayrollStypayod.obl_code.Trim();
                parameters[3] = objDVOPayrollStypayod.obl_rate;
                parameters[4] = objDVOPayrollStypayod.amount;
                parameters[5] = objDVOPayrollStypayod.acct_no;
                parameters[6] = objDVOPayrollStypayod.Department.Trim();
                parameters[7] = objDVOPayrollStypayod.bal_acct_no;
                parameters[8] = objDVOPayrollStypayod.bal_dept.Trim();
                parameters[9] = objDVOPayrollStypayod.mod_flag;
                parameters[10] = objDVOPayrollStypayod.add_code.Trim();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollStypayod.INSERT_STYPAYOD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public decimal ded_taxcalc(int n, decimal tax_wages, string pay_period)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // check for exempt status for state
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                if (objListemplforprocess[currentempnoid].StateAllow == 99)
                {
                    return 0;
                }
            }
            else
            {
                //if not state tax code then default to federal allowances
                if (objListemplforprocess[currentempnoid].Allowances == 99)
                {
                    return 0;
                }
            }
            // initialize flags
            bool check_year = false;
            year_is_current = true;
            decimal allow_amt = 0;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";

            // get allowance value
            object[] parameters = new object[2];
            parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            switch (pay_period)
            {

                case "W":
                    {
                        object week_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_week_allow);
                        if (week_allow == null || week_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(week_allow);

                        }
                        break;
                    }
                case "B":
                    {
                        object biweek_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_biweek_allow);
                        if (biweek_allow == null || biweek_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(biweek_allow);

                        }
                        break;
                    }
                case "S":
                    {
                        object smonth_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_smonth_allow);
                        if (smonth_allow == null || smonth_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(smonth_allow);

                        }
                        break;
                    }
                case "M":
                    {
                        object month_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_month_allow);
                        if (month_allow == null || month_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(month_allow);

                        }
                        break;
                    }
                case "Q":
                    {
                        object quarter_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_quarter_allow);
                        if (quarter_allow == null || quarter_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(quarter_allow);

                        }
                        break;
                    }
                case "H":
                    {
                        object syear_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_syear_allow);
                        if (syear_allow == null || syear_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(syear_allow);

                        }
                        break;
                    }
                case "A":
                    {
                        object year_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
                        if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(year_allow);

                        }
                        break;
                    }
                case "D":
                    {
                        object misc_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_misc_allow);
                        if (misc_allow == null || misc_allow.ToString().Trim() == string.Empty)
                        {
                            check_year = true;
                        }
                        else
                        {
                            allow_amt = Convert.ToDecimal(misc_allow);

                        }
                        break;
                    }
            }
            //check_year is set to true when a table lookup returns nothing
            //check to see if the table does exist, but the Tax Year is not current
            if (check_year)
            {
                //call tbl_check
                tbl_check(n);
            }
            // make sure required values exist
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[currentempnoid].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            // calculate taxable amount
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));

            }
            else
            {
                //if not state tax code then default to federal allowances
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            //get tax table values
            if (year_is_current)
            {
                object[] taxparameter = new object[5];
                taxparameter[0] = objPayrolldeductionsglobal[n].ded_code;
                taxparameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                taxparameter[2] = pay_period.Trim();
                taxparameter[3] = t_total;
                taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                DataSet ds = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref taxparameter, objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                        t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                        t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                        t_period = dr[3].ToString().Trim();
                        t_marital = dr[4].ToString().Trim();
                    }
                }
                //calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }

            return t_total;
        }
        public void tbl_check(int n)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // this function is called to verify that if a table exists, that
            // the Tax Year matches the payroll date year.
            object[] parameter = new object[2];
            parameter[0] = objPayrolldeductionsglobal[n].ded_code;
            parameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            DataSet ds = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref parameter, objPayrolldeductionsglobal[n].FIND_usp_tbl_check);
            if (ds.Tables[0].Rows[0][0] == DBNull.Value || ds.Tables[0].Rows[0][0].ToString().Trim() == string.Empty)
            {
                ErrMsg2 = "*** Error: Table(s) not current for employee's deduction codes.";
                ErrMsg1 = "**** End of report.  One or more deduction tables are not current.";
                year_is_current = false;
            }

        }
        public decimal state_calc(int n)
        {
            // define
            decimal wage_amount = 0;
            decimal statax_amount = 0;
            // set wage_amount appropriately
            if (objPayrolldeductionsglobal[n].ded_type.Trim() == "G")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_gross ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "T")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_taxable ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "F")
            {
                wage_amount = fica_wages;

            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "U")
            {
                wage_amount = futa_wages;
            }
            else
            {
                wage_amount = 0;
            }
            if (objPayrolldeductionsglobal[n].tax_code == string.Empty)
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));

                }
                else
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate * wage_amount));
                }
            }
            else
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));
                }
                else
                {
                    statax_amount = ded_taxcalc(n, wage_amount, objListemplforprocess[n].PayPeriod);

                }

            }
            return statax_amount;
        }
        public decimal ded_fedgrs(int n, decimal tax_wages, string pay_period)
        {
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal allow_amt = 0;
            bool check_year = false;
            bool year_is_current = true;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";
            object[] parameters = new object[2];
            parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            object year_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
            if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
            {
                check_year = true;
            }
            else
            {
                allow_amt = Convert.ToDecimal(year_allow);

            }
            if (check_year)
            {
                tbl_check(n);
            }
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[n].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));
            }
            else
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            if (year_is_current)
            {
                object[] taxparameter = new object[5];
                taxparameter[0] = objstycntrcList[0].fedtax_code;
                taxparameter[1] = objDVOPayrollProcess_PayEmployeeList[n].pay_date;
                taxparameter[2] = "A";
                taxparameter[3] = t_total;
                taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                DataSet ds = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref taxparameter, objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                    t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                    t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                    t_period = dr[3].ToString().Trim();
                    t_marital = dr[4].ToString().Trim();
                }
                // calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
                if (t_total < 0)
                {
                    t_total = 0;
                }

            }
            return t_total;
        }
    }


    public class BLLPayrollAutopayNew
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        public List<DVOPayrollstypayid> objPayrollIncomesglobal = new List<DVOPayrollstypayid>();
        public List<DVOPayrollstypaydd> objPayrolldeductionsglobal = new List<DVOPayrollstypaydd>();
        public List<DVOPayrollStypayod> objPayrollobligationsglobal = new List<DVOPayrollStypayod>();
        public List<DVOUpdatePayDefaults> objstycntrcList = new List<DVOUpdatePayDefaults>();

        public decimal fica_wages = 0.0M;
        public decimal futa_wages = 0.0M;
        public Int32 dup_ssn_pay = 0;
        public Int32 dup_ssn = 0;
        //static bool TimeCardUsedForPayroll = false;
        //static int UsedTimeCardNo = 0;
        public DVOPayrollautopay objpaydatasearch = new DVOPayrollautopay();
        object objTransaction;
        BLLPayrollFunctions BPfunctions = new BLLPayrollFunctions();

        DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
        //DALBaseClass objDalBaseClass;
        public bool same_person = false;
        public string empl_ssn;
        public Int32 currentempnoid;
        public List<DVOPayrollProcess_PayEmployee> objDVOPayrollProcess_PayEmployeeList = new List<DVOPayrollProcess_PayEmployee>();
        public List<DVOPayrollautopay> objListemplforprocess;
        bool year_is_current = true;
        public string ErrMsg1 = string.Empty;
        public string ErrMsg2 = string.Empty;
        public string ErrMsg3 = string.Empty;
        public string SearchedEmployeeList = string.Empty;
        //*******************Added By Rohit **************
        public DataSet dsAllFlexKeyVal = new DataSet();
        public DataSet dsAllAcctType = new DataSet();
        public DataSet dsAllIncomesstyincr = new DataSet();
        public DataSet dsAllObligationsMasterOblCodes = new DataSet();
        public List<DVOUpdateIncCode> listDVOUpdateIncCode = new List<DVOUpdateIncCode>();
        public List<DVOMasterOblCodes> listDVOMasterOblCodes = new List<DVOMasterOblCodes>();
        //************************************************
        static bool TimeCardUsedForPayroll = false;
        public static bool isTimeCardUsedForPayroll
        {
            get { return TimeCardUsedForPayroll; }
        }

        static int UsedTimeCardNo = 0;
        public static int TimeCardNo
        {
            get { return UsedTimeCardNo; }
        }

        bool globalCallFromUpdatePayrollScreen = false;
        public bool isCallFromUpdatePayrollScreen
        {
            set { globalCallFromUpdatePayrollScreen = value; }
        }

        bool _canCreatePayroll = false;
        /// <summary>
        /// false - can't create payroll for selected employee
        /// true - can create payroll for selected employee
        /// </summary>
        public bool canCreatePayroll
        {
            get { return _canCreatePayroll; }
        }

        string _message = string.Empty;
        /// <summary>
        /// Get message if you can't create payroll for selected employee
        /// </summary>
        public string Message
        {
            get { return _message; }
        }

        public List<DVOPayrollautopay> GetEmplListforProcess(DVOPayrollautopay objpayautosearchdata)
        {
            //this function will return the Employee
            //List for which the 
            //payroll is to be processed .
            objpaydatasearch = objpayautosearchdata;
            object[] parameters = new object[12];
            parameters[0] = objpayautosearchdata.RowID;
            parameters[1] = objpayautosearchdata.EmplCode;
            parameters[2] = objpayautosearchdata.SocSecNum;
            parameters[3] = objpayautosearchdata.FirstName;
            parameters[4] = objpayautosearchdata.LastName;
            parameters[5] = objpayautosearchdata.Employee_Type;
            parameters[6] = objpayautosearchdata.Job_Code;
            parameters[7] = "";
            parameters[8] = "";
            if (objpayautosearchdata.Process_TimeCard == "N")
            {
                parameters[9] = false;
            }
            else
            {
                parameters[9] = true;
            }

            parameters[10] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            parameters[11] = objpayautosearchdata.District;

            List<DVOPayrollautopay> objpayrollautopaylist = new List<DVOPayrollautopay>();
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            SearchedEmployeeList = string.Empty;
            //object TransactionObject = objDALBaseClassHelper.GetUncommittedTransactionObject();
            //DataSet ds;
            //ds.Tables[0].Select(

            //using (DataSet ds = objDalBaseClass.GetData_ByTransaction(ref TransactionObject, ref parameters, typeof(DVOPayrollautopay)))
            using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollautopay)))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    DVOPayrollautopay objautopay = new DVOPayrollautopay();
                    objautopay.EmplCode = dr[0].ToString().Trim();
                    objautopay.SocSecNum = dr[1].ToString().Trim();
                    objautopay.FirstName = dr[2].ToString().Trim();
                    objautopay.LastName = dr[3].ToString().Trim();
                    objautopay.CashAcct = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                    objautopay.Department = dr[5].ToString().Trim();
                    objautopay.Terminated = (dr[6] != DBNull.Value ? Convert.ToString(dr[6]).Trim() : string.Empty);
                    objautopay.PayPeriod = dr[7].ToString().Trim();
                    objautopay.Allowances = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                    objautopay.StateAllow = (dr[9] != DBNull.Value ? Convert.ToInt32(dr[9]) : 0);
                    objautopay.MaritalStat = dr[10].ToString().Trim();
                    objautopay.VacCode = dr[11].ToString().Trim();
                    objautopay.VacAllowed = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
                    objautopay.VacUsed = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
                    objautopay.SickCode = dr[14].ToString().Trim();
                    objautopay.SickAllowed = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
                    objautopay.SickUsed = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
                    objautopay.LastPay = (dr[17] != DBNull.Value ? Convert.ToDateTime(dr[17]) : Convert.ToDateTime(null));
                    objautopay.HoldPayment = dr[18].ToString().Trim();
                    objautopay.StaTaxCode = dr[19].ToString().Trim();
                    objautopay.LocTaxCode = dr[20].ToString().Trim();
                    objautopay.DirDept = dr[21].ToString().Trim();
                    objautopay.FlexDeptAcctType = dr[22].ToString().Trim();
                    objautopay.LastIncDate = (dr[23] != DBNull.Value ? Convert.ToString(dr[23]).Trim() : string.Empty);
                    objautopay.RowID = (dr[24] != DBNull.Value && dr[24].ToString().Trim() != "" ? Convert.ToInt32(dr[24]) : 0);
                    DVOFlexSegCommon objflexsegloadtype = new DVOFlexSegCommon();
                    objflexsegloadtype.EntityType = objautopay.TABLE_NAME;
                    objflexsegloadtype.Code = objautopay.EmplCode;
                    objflexsegloadtype.AccountType = objautopay.FlexDeptAcctType;
                    objautopay.Flexdeptkeyvalue = PayrollFlexseg_Load(ref objflexsegloadtype);
                    if (BPfunctions.pay_time(objautopay.LastPay, objpayautosearchdata.EOP_Date, objautopay.PayPeriod, Convert.ToBoolean(parameters[9])))
                    {
                        if (SearchedEmployeeList.Trim().Length > 0)
                            SearchedEmployeeList += ",";
                        SearchedEmployeeList += "'" + objautopay.EmplCode + "'";

                        objpayrollautopaylist.Add(objautopay);
                    }
                }

            }
            return objpayrollautopaylist;

        }
        public void CalculateArrears()
        {
            try
            {
                //int result = db.Database.ExecuteSqlCommand("EXEC MasterEmployeeExcelExport @filePathWithName, @UserId, @ApplicantIFSCCode,@BankName", parameter1, parameter2, parameter3, parameter4);
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object[] parameters = new object[0];
                object InsResult = objDalBaseClass.ExecuteProcedure(ref parameters, "CalculateArrears");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<DVOPayrollProcess_PayEmployee> Autopay(ref DVOPayrollautopay objpayautosearchdata, ref DVOPYBatchProcessStybatchr pObjBatch, bool callSP = false)
        {
            DVOPYBatchProcessDetailStybatchd objProcessDtl = new DVOPYBatchProcessDetailStybatchd();
            if (objpayautosearchdata.Payroll_Date == DateTime.MinValue)
                objpayautosearchdata.Payroll_Date = DVOApplicationUserInfo.ParseDateConvertion("1900/01/01");
            if (objpayautosearchdata.EOP_Date == DateTime.MinValue)
                objpayautosearchdata.EOP_Date = DVOApplicationUserInfo.ParseDateConvertion("1900/01/01");

            if (callSP)
            {
                object[] parameters = new object[17];
                parameters[0] = pObjBatch.pybatchid;
                parameters[1] = "Create Auto Payroll";
                parameters[2] = pObjBatch.searchcriteria;
                parameters[3] = objProcessDtl.insertby;
                parameters[4] = objProcessDtl.insertmachineinfo;
                parameters[5] = objpayautosearchdata.RowID;
                parameters[6] = objpayautosearchdata.EmplCode;
                parameters[7] = objpayautosearchdata.SocSecNum;
                parameters[8] = objpayautosearchdata.FirstName;
                parameters[9] = objpayautosearchdata.LastName;
                parameters[10] = objpayautosearchdata.Employee_Type;
                parameters[11] = objpayautosearchdata.Job_Code;
                parameters[12] = objpayautosearchdata.Process_TimeCard;
                parameters[13] = objpayautosearchdata.EOP_Date;
                parameters[14] = objpayautosearchdata.District;
                parameters[15] = objpayautosearchdata.Payroll_Date;
                parameters[16] = "";
                (objDVOPayrollProcess_PayEmployeeList, objListemplforprocess) = BLLPYBatchProcessDetailStybatchd.ExecuteAutoPay(parameters);
                return objDVOPayrollProcess_PayEmployeeList;
            }
            /*
            objProcessDtl.pybatchid = pObjBatch.pybatchid;
            objProcessDtl.processname = "Create Auto Payroll";
            objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
            objpayautosearchdata.RowID;
            objpayautosearchdata.EmplCode;
            objpayautosearchdata.SocSecNum;
            objpayautosearchdata.FirstName;
            objpayautosearchdata.LastName;
            objpayautosearchdata.Employee_Type;
            objpayautosearchdata.Job_Code;
            objpayautosearchdata.EOP_Date;// .ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            objpayautosearchdata.District;
            objpayautosearchdata.Payroll_Date
            BonusCeck char(1) = ""
            */

            CalculateArrears();
            /*
              Added by Sarvjeet on 16/01/2010.
              To implemented Payroll batch process into Create Auto Payroll. Nedd to add
              A parameter 'ref DVOPYBatchProcessStybatchr pObjBatch' in Autopay function
              and remove comment from the code written for batch process logic.   


            /*
             written by     Rohit Wadhwa 
             written Date   22/12/2008
             AIM :.
             this function loads the default income/deduction/obligation
             codes into the internal arrays p_ypayre, p_ypayid, p_ypaydd,
             and p_ypayod, calculates the corresponding amounts and inserts
             them into Process_PayEmployee,stypayid,stypaydd,stypayod etc... It also sets the remaining required
             data in Process_PayEmployee.
             */
            object objLockTransaction = null;
            int _currentDocNo = 0;
            DVOUpdatePayDefaults objPayDefaul = new DVOUpdatePayDefaults();
            //Added by Sarvjeet on 16/01/2010 for batch detail..
            StringBuilder errorMassage = new StringBuilder();

            int recordsSearched = 0;
            int recordsProcessed = 0;
            bool IsProessIns = false;
            try
            {

                #region Insert Process Start Info..

                object objTrx = null;
                objProcessDtl.pybatchid = pObjBatch.pybatchid;
                objProcessDtl.processname = "Create Auto Payroll";
                objProcessDtl.searchcriteria = pObjBatch.searchcriteria;
                BLLPYBatchProcessDetailStybatchd.InsertProcessInfo(ref objTrx, ref objProcessDtl);
                IsProessIns = true;

                #endregion

                bool prep_flag = false;
                bool year_is_current = true;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                //*********************Added By Rohit *************************************
                LoadAllFlexKeyval(objpayautosearchdata);
                LoadAllAcctType();
                //*************************************************************************

                objListemplforprocess = GetEmplListforProcess(objpayautosearchdata);
                if (objListemplforprocess != null && objListemplforprocess.Count > 0)
                {
                    //objTransaction = objDALBaseClassHelper.GetTransactionObject();
                    //DVOUpdatePayDefaults objPayDefaul = new DVOUpdatePayDefaults();
                    //objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);

                    DVOPayrollProcess_PayEmployee objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                    bool ok_to_commit = true;
                    objLockTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();

                    objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);
                    if (objstycntrcList != null && objstycntrcList.Count > 0)
                    {
                        objPayDefaul = objstycntrcList[0];
                        int l = LockPayrollDefaultRecord(ref objLockTransaction, ref objPayDefaul);
                        if (l == 1)
                        {
                            objstycntrcList = BLLUpdPayDefault.GetPayrollDefaults(ref objPayDefaul);
                            if (objstycntrcList != null && objstycntrcList.Count > 0)
                            {
                                _currentDocNo = objstycntrcList[0].py_doc_no;
                                //object nullTransacionObject = null;
                                //if (objListemplforprocess.Count > 0)
                                //    _currentDocNo = BLLAccountingLiberary.CurrentValue_With_LockAndSelect("stycntrc", "py_doc_no", objListemplforprocess.Count, ref nullTransacionObject);

                                #region "Get All Data to Process"
                                //objListemplforprocess = GetEmplListforProcess(objpayautosearchdata);
                                if (objListemplforprocess.Count > 0)
                                {
                                    LoadAllTimeCardIncomes(objpayautosearchdata);
                                    LoadAllEmployeeIncomes(objpayautosearchdata);
                                    LoadAllDeductions(objpayautosearchdata);
                                    LoadAllded_taxcalc(objpayautosearchdata);
                                    LoadAllded_taxcalc_Detail(objpayautosearchdata);
                                    LoadAllObligations(objpayautosearchdata);
                                    LoadAllDupSSN();
                                    LoadAllDupSSNPay();
                                    LoadAllPayrollGLAccountsData();
                                    LoadallIncomestyincr();
                                    LoadAllobligationsMasterOblCodes();
                                    //DVOUpdateIncCode objDVOUpdateIncCode = new DVOUpdateIncCode();

                                    //listDVOUpdateIncCode = BLLUpdateIncomeCodes.GetDetailedInfo(ref objDVOUpdateIncCode);
                                    //DVOMasterOblCodes objDVOMasterOblCodes = new DVOMasterOblCodes();

                                    //listDVOMasterOblCodes = BLLPRObligationCodesMasterOblCodes.GetData(ref objDVOMasterOblCodes);
                                    #endregion "Get All Data to Process"

                                    DataTable dataTable = new DataTable("PPEmployeeType");
                                    dataTable.Columns.Add("doc_no", typeof(int));
                                    dataTable.Columns.Add("empl_code", typeof(string));
                                    dataTable.Columns.Add("doc_date", typeof(DateTime));
                                    dataTable.Columns.Add("pay_date", typeof(DateTime));
                                    dataTable.Columns.Add("eop_date", typeof(DateTime));
                                    dataTable.Columns.Add("print_check", typeof(string));
                                    dataTable.Columns.Add("cash_acct_no", typeof(int));
                                    dataTable.Columns.Add("department", typeof(string));
                                    dataTable.Columns.Add("cash_amount", typeof(decimal));
                                    dataTable.Columns.Add("check_no", typeof(int));
                                    dataTable.Columns.Add("inc_gross", typeof(decimal));
                                    dataTable.Columns.Add("ded_fica", typeof(decimal));
                                    dataTable.Columns.Add("inc_taxable", typeof(decimal));
                                    dataTable.Columns.Add("ded_medicare", typeof(decimal));
                                    dataTable.Columns.Add("ded_fedtax", typeof(decimal));
                                    dataTable.Columns.Add("ded_statax", typeof(decimal));
                                    dataTable.Columns.Add("ded_loctax", typeof(decimal));
                                    dataTable.Columns.Add("ded_other", typeof(decimal));
                                    dataTable.Columns.Add("obl_futa", typeof(decimal));
                                    dataTable.Columns.Add("obl_fica", typeof(decimal));
                                    dataTable.Columns.Add("obl_medicare", typeof(decimal));
                                    dataTable.Columns.Add("obl_other", typeof(decimal));
                                    dataTable.Columns.Add("obl_total", typeof(decimal));
                                    dataTable.Columns.Add("inc_net", typeof(decimal));
                                    dataTable.Columns.Add("inc_expense", typeof(decimal));
                                    dataTable.Columns.Add("total_hours", typeof(decimal));
                                    dataTable.Columns.Add("ok_to_post", typeof(string));
                                    dataTable.Columns.Add("accrue_sick", typeof(string));
                                    dataTable.Columns.Add("accrue_vac", typeof(string));
                                    dataTable.Columns.Add("bonus", typeof(string));
                                    dataTable.Columns.Add("deposit", typeof(string));
                                    dataTable.Columns.Add("timcrdusd", typeof(int));
                                    dataTable.Columns.Add("timcrdno", typeof(int));
                                    dataTable.Columns.Add("start_Date", typeof(DateTime));

                                    DataTable dataTableIncome = new DataTable("PPEmployeeIncomeType");
                                    dataTableIncome.Columns.Add("doc_no", typeof(int));
                                    dataTableIncome.Columns.Add("line_no", typeof(int));
                                    dataTableIncome.Columns.Add("inc_code", typeof(string));
                                    dataTableIncome.Columns.Add("inc_rate", typeof(decimal));
                                    dataTableIncome.Columns.Add("number", typeof(decimal));
                                    dataTableIncome.Columns.Add("hours", typeof(decimal));
                                    dataTableIncome.Columns.Add("amount", typeof(decimal));
                                    dataTableIncome.Columns.Add("acct_no", typeof(int));
                                    dataTableIncome.Columns.Add("department", typeof(string));
                                    dataTableIncome.Columns.Add("mod_flag", typeof(int));
                                    dataTableIncome.Columns.Add("add_code", typeof(string));
                                    dataTableIncome.Columns.Add("lo_inc_amt", typeof(decimal));
                                    dataTableIncome.Columns.Add("hi_inc_amt", typeof(decimal));

                                    #region "Payroll Process"
                                    //objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
                                    recordsSearched = objListemplforprocess.Count;
                                    for (Int32 i = 0; i < objListemplforprocess.Count; i++)
                                    {
                                        try
                                        {
                                            currentempnoid = i;
                                            //objTransaction = objDALBaseClassHelper.GetUncommittedTransactionObject();
                                            object[] parameters = new object[1];
                                            DVOPayrollautopay objDvopayrollauto = new DVOPayrollautopay();
                                            objPayrollProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
                                            objDvopayrollauto = objListemplforprocess[i];
                                            //parameters[0] = objDvopayrollauto.SocSecNum.Trim();
                                            //get from DB if it is a duplicate ssn code 
                                            if (dsDuplicateSSN != null && dsDuplicateSSN.Tables.Count > 0)
                                            {
                                                DataRow[] drs = dsDuplicateSSN.Tables[0].Select("SSN = '" + objDvopayrollauto.SocSecNum.Trim() + "'");
                                                if (drs.Length > 0)
                                                    dup_ssn = drs[0]["COUNT"] != DBNull.Value ? Convert.ToInt32(drs[0]["COUNT"]) : 0;
                                            }

                                            //dup_ssn = Convert.ToInt32(objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDvopayrollauto.DUP_SSN));
                                            //
                                            empl_ssn = objListemplforprocess[i].SocSecNum.Trim();
                                            ok_to_commit = true;
                                            if (dup_ssn > 1)
                                            {
                                                if (dsDuplicateSSNPay != null && dsDuplicateSSNPay.Tables.Count > 0)
                                                {
                                                    DataRow[] drs = dsDuplicateSSNPay.Tables[0].Select("SSN='" + objDvopayrollauto.SocSecNum.Trim() + "' AND EMPL_CODE <> '" + objDvopayrollauto.EmplCode.Trim() + "'");
                                                    if (drs.Length > 0)
                                                        dup_ssn_pay = drs[0]["COUNT"] != DBNull.Value ? Convert.ToInt32(drs[0]["COUNT"]) : 0;

                                                }

                                                //parameters = new object[2];
                                                //parameters[0] = objDvopayrollauto.EmplCode.Trim();
                                                //parameters[1] = objDvopayrollauto.SocSecNum.Trim();
                                                //dup_ssn_pay = Convert.ToInt32(objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objDvopayrollauto.DUP_SSN_PAY));
                                            }
                                            if (dup_ssn_pay > 0)
                                            {
                                                same_person = true;
                                                // break;
                                            }

                                            objPayrollProcess_PayEmployee.EmplCode = objDvopayrollauto.EmplCode;

                                            if (objpayautosearchdata.Payroll_Date == DateTime.MinValue)
                                            {
                                                objpayautosearchdata.Payroll_Date = DVOApplicationUserInfo.CurrentDate;
                                            }
                                            if (objpayautosearchdata.EOP_Date == DateTime.MinValue)
                                            {
                                                objpayautosearchdata.EOP_Date = objpayautosearchdata.Payroll_Date;
                                            }
                                            if (objpayautosearchdata.Start_date == DateTime.MinValue)
                                            {
                                                objpayautosearchdata.Start_date = objpayautosearchdata.EOP_Date.AddDays(-6);
                                            }
                                            if (objDvopayrollauto.DirDept == "Y")
                                            {
                                                objPayrollProcess_PayEmployee.deposit = "Y";
                                            }
                                            else
                                            {
                                                objPayrollProcess_PayEmployee.deposit = "N";
                                            }
                                            //object nullTransacionObject = null;
                                            //objPayrollProcess_PayEmployee.Doc_no = BLLAccountingLiberary.Auto_Next("stycntrc", "py_doc_no", ref nullTransacionObject);

                                            objPayrollProcess_PayEmployee.Doc_no = ++_currentDocNo;

                                            //if (objPayrollProcess_PayEmployee.Doc_no == 0)
                                            //{
                                            //    //rollback 
                                            //    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                            //    return objDVOPayrollProcess_PayEmployeeList;
                                            //}

                                            objPayrollProcess_PayEmployee.doc_date = objpayautosearchdata.Payroll_Date;
                                            objPayrollProcess_PayEmployee.pay_date = objpayautosearchdata.Payroll_Date;
                                            objPayrollProcess_PayEmployee.eop_date = objpayautosearchdata.EOP_Date;
                                            objPayrollProcess_PayEmployee.start_date = objpayautosearchdata.Start_date;
                                            objPayrollProcess_PayEmployee.bonus = objpayautosearchdata.BonusCeck;
                                            if (objPayrollProcess_PayEmployee.bonus == "Y")
                                            {
                                                objPayrollProcess_PayEmployee.accrue_sick = "N";
                                                objPayrollProcess_PayEmployee.accrue_vac = "N";
                                            }
                                            else
                                            {
                                                objPayrollProcess_PayEmployee.accrue_sick = "Y";
                                                objPayrollProcess_PayEmployee.accrue_vac = "Y";
                                            }
                                            if (objDvopayrollauto.DirDept == "Y")
                                            {
                                                objPayrollProcess_PayEmployee.print_check = "N";
                                            }
                                            else
                                            {
                                                objPayrollProcess_PayEmployee.print_check = "Y";
                                            }
                                            objPayrollProcess_PayEmployee.ok_to_post = "N";
                                            objPayrollProcess_PayEmployee.StateTaxCode = objDvopayrollauto.StaTaxCode;
                                            if (objDvopayrollauto.Flexdeptacctno == 0)
                                            {
                                                //set value from control table is there is no value for Employee cash account .
                                                objDvopayrollauto.Flexdeptacctno = objstycntrcList[0].cash_acct;
                                            }

                                            objPayrollProcess_PayEmployee.Cash_acct_no = objDvopayrollauto.Flexdeptacctno;
                                            objPayrollProcess_PayEmployee.Department = objDvopayrollauto.Department;
                                            objPayrollIncomesglobal = LoadIncomes(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                                            objPayrolldeductionsglobal = LoadDeductions(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);
                                            objPayrollobligationsglobal = LoadObligations(objPayrollProcess_PayEmployee.EmplCode, objPayrollProcess_PayEmployee.eop_date, objListemplforprocess[i].FlexDeptAcctType, objListemplforprocess[i].Flexdeptkeyvalue);

                                            objPayrollProcess_PayEmployee = CalculatePayrolls(ref objPayrollProcess_PayEmployee);

                                            DataRow processPayEmployeeDataRow = dataTable.NewRow();
                                            processPayEmployeeDataRow["doc_no"] = objPayrollProcess_PayEmployee.Doc_no;
                                            processPayEmployeeDataRow["empl_code"] = objPayrollProcess_PayEmployee.EmplCode;
                                            processPayEmployeeDataRow["doc_date"] = objPayrollProcess_PayEmployee.doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                            processPayEmployeeDataRow["pay_date"] = objPayrollProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                            processPayEmployeeDataRow["eop_date"] = objPayrollProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                                            processPayEmployeeDataRow["print_check"] = objPayrollProcess_PayEmployee.print_check;
                                            processPayEmployeeDataRow["cash_acct_no"] = objPayrollProcess_PayEmployee.Cash_acct_no;
                                            processPayEmployeeDataRow["department"] = "000";// objPayrollProcess_PayEmployee.Department;
                                            processPayEmployeeDataRow["cash_amount"] = objPayrollProcess_PayEmployee.cash_amount;
                                            processPayEmployeeDataRow["check_no"] = objPayrollProcess_PayEmployee.check_no;
                                            processPayEmployeeDataRow["inc_gross"] = objPayrollProcess_PayEmployee.inc_gross;
                                            processPayEmployeeDataRow["ded_fica"] = objPayrollProcess_PayEmployee.ded_fica;
                                            processPayEmployeeDataRow["inc_taxable"] = objPayrollProcess_PayEmployee.inc_taxable;
                                            processPayEmployeeDataRow["ded_medicare"] = objPayrollProcess_PayEmployee.ded_medicare;
                                            processPayEmployeeDataRow["ded_fedtax"] = objPayrollProcess_PayEmployee.ded_fedtax;
                                            processPayEmployeeDataRow["ded_statax"] = objPayrollProcess_PayEmployee.ded_statax;
                                            processPayEmployeeDataRow["ded_loctax"] = objPayrollProcess_PayEmployee.ded_loctax;
                                            processPayEmployeeDataRow["ded_other"] = objPayrollProcess_PayEmployee.ded_other;
                                            processPayEmployeeDataRow["obl_futa"] = objPayrollProcess_PayEmployee.obl_futa;
                                            processPayEmployeeDataRow["obl_fica"] = objPayrollProcess_PayEmployee.obl_fica;
                                            processPayEmployeeDataRow["obl_medicare"] = objPayrollProcess_PayEmployee.obl_medicare;
                                            processPayEmployeeDataRow["obl_other"] = objPayrollProcess_PayEmployee.obl_other;
                                            processPayEmployeeDataRow["obl_total"] = objPayrollProcess_PayEmployee.obl_total;
                                            processPayEmployeeDataRow["inc_net"] = objPayrollProcess_PayEmployee.inc_net;
                                            processPayEmployeeDataRow["inc_expense"] = objPayrollProcess_PayEmployee.inc_expense;
                                            processPayEmployeeDataRow["total_hours"] = objPayrollProcess_PayEmployee.total_hours;
                                            processPayEmployeeDataRow["ok_to_post"] = objPayrollProcess_PayEmployee.ok_to_post;
                                            processPayEmployeeDataRow["accrue_sick"] = objPayrollProcess_PayEmployee.accrue_sick;
                                            processPayEmployeeDataRow["accrue_vac"] = objPayrollProcess_PayEmployee.accrue_vac;
                                            processPayEmployeeDataRow["bonus"] = objPayrollProcess_PayEmployee.bonus;
                                            processPayEmployeeDataRow["deposit"] = objPayrollProcess_PayEmployee.deposit;
                                            processPayEmployeeDataRow["timcrdusd"] = TimeCardUsedForPayroll ? 1 : 0;
                                            processPayEmployeeDataRow["timcrdno"] = UsedTimeCardNo;
                                            processPayEmployeeDataRow["start_Date"] = objPayrollProcess_PayEmployee.start_date;
                                            dataTable.Rows.Add(processPayEmployeeDataRow);
                                            // post the header record
                                            // Comented on 31/10/2023 by sujeet
                                            //bool re_status = InsertIntoProcess_PayEmployee(ref objPayrollProcess_PayEmployee, ref objTransaction);
                                            //if (!re_status)
                                            //{
                                            //  ok_to_commit = false;
                                            //  throw new Exception("Error occured during insertion of Payroll in Automatic-Payroll");
                                            //}

                                            // post income detail                  
                                            for (int j = 0; j < objPayrollIncomesglobal.Count; j++)
                                            {
                                                objPayrollIncomesglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;

                                                DataRow processPayEmployeeIncomeDataRow = dataTableIncome.NewRow();
                                                processPayEmployeeIncomeDataRow["doc_no"] = objPayrollIncomesglobal[j].Doc_no;
                                                processPayEmployeeIncomeDataRow["line_no"] = objPayrollIncomesglobal[j].line_no;
                                                processPayEmployeeIncomeDataRow["inc_code"] = objPayrollIncomesglobal[j].inc_code.Trim();
                                                processPayEmployeeIncomeDataRow["inc_rate"] = objPayrollIncomesglobal[j].inc_rate;
                                                processPayEmployeeIncomeDataRow["number"] = objPayrollIncomesglobal[j].number;
                                                processPayEmployeeIncomeDataRow["hours"] = objPayrollIncomesglobal[j].hours;
                                                processPayEmployeeIncomeDataRow["amount"] = objPayrollIncomesglobal[j].amount;
                                                processPayEmployeeIncomeDataRow["acct_no"] = objPayrollIncomesglobal[j].acct_no;
                                                processPayEmployeeIncomeDataRow["department"] = "000";// objPayrollIncomesglobal[j].Department.Trim();
                                                processPayEmployeeIncomeDataRow["mod_flag"] = objPayrollIncomesglobal[j].mod_flag;
                                                processPayEmployeeIncomeDataRow["add_code"] = objPayrollIncomesglobal[j].add_code.Trim();
                                                processPayEmployeeIncomeDataRow["lo_inc_amt"] = objPayrollIncomesglobal[j].lo_inc_amt;
                                                processPayEmployeeIncomeDataRow["hi_inc_amt"] = objPayrollIncomesglobal[j].hi_inc_amt;
                                                dataTableIncome.Rows.Add(processPayEmployeeIncomeDataRow);

                                                //bool id_ststus = InsertIntoStypayid(objPayrollIncomesglobal[j], ref objTransaction);
                                                //if (!id_ststus)
                                                //{
                                                //  ok_to_commit = false;
                                                //  throw new Exception("Error occured during insertion of Payroll-Income in Automatic-Payroll");
                                                //}
                                            }
                                            ////post deduction detail
                                            //for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
                                            //{
                                            //  objPayrolldeductionsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                                            //  bool dd_status = InsertIntoStypaydd(objPayrolldeductionsglobal[j], ref objTransaction);
                                            //  if (!dd_status)
                                            //  {
                                            //    ok_to_commit = false;
                                            //    throw new Exception("Error occured during insertion of Payroll-Deduction in Automatic-Payroll");
                                            //  }
                                            //}
                                            ////post obligation detail
                                            //for (int j = 0; j < objPayrollobligationsglobal.Count; j++)
                                            //{
                                            //  objPayrollobligationsglobal[j].Doc_no = objPayrollProcess_PayEmployee.Doc_no;
                                            //  bool od_ststus = InsertIntoStypayod(objPayrollobligationsglobal[j], ref objTransaction);
                                            //  if (!od_ststus)
                                            //  {
                                            //    ok_to_commit = false;
                                            //    throw new Exception("Error occured during insertion of Payroll-Obligation in Automatic-Payroll");
                                            //  }
                                            //}
                                        }
                                        catch (Exception ex)
                                        {
                                            ok_to_commit = false;
                                            ExceptionManagement.ExceptionManager.Publish(ex);
                                            errorMassage.Append("[" + objPayrollProcess_PayEmployee.EmplCode + "-" + ex.Message + "]");
                                        }

                                        if (ok_to_commit)
                                        {
                                            //coomitwork
                                            //objDALBaseClassHelper.CommitTransaction(ref objTransaction);
                                            //_currentDocNo = _currentDocNo + i + 1;
                                            objDVOPayrollProcess_PayEmployeeList.Add(objPayrollProcess_PayEmployee);
                                            recordsProcessed++;
                                        }
                                        else
                                        {
                                            _currentDocNo--;
                                            //rollback 
                                            //objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                                        }
                                    }

                                    //string consString = ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;
                                    string consString = ConnectionStringProvider.GetConnectionString();
                                    using (SqlConnection con = new SqlConnection(consString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("GeneratePensionProcess"))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Connection = con;
                                            cmd.Parameters.AddWithValue("@PPEmployeeInfo", dataTable);
                                            cmd.Parameters.AddWithValue("@PPEmployeeIncome", dataTableIncome);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    errorMassage.Append("[No Record to Process]");
                                }
                                #endregion "Payroll Process"
                            }
                            ReleaseAndUpdatePayrollDefaultRecord(true, false, ref objLockTransaction, ref objPayDefaul, _currentDocNo);
                        }
                    }
                    //  canCreatePayroll = true;
                }
                else
                {
                    //errorMassage.Append("[No Record to Process]");
                }
            }
            catch (Exception ex)
            {
                if (objTransaction != null)
                    objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
                if (objLockTransaction != null)
                    ReleaseAndUpdatePayrollDefaultRecord(false, true, ref objLockTransaction, ref objPayDefaul, _currentDocNo);
                ExceptionManagement.ExceptionManager.Publish(ex);
                objDVOPayrollProcess_PayEmployeeList.Clear();
                errorMassage.Append("[" + ex.Message + "]");
                throw ex;
            }
            finally
            {
                #region Record process detail..
                if (IsProessIns)
                {
                    List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
                    object objTrx = null;
                    //objProcessDtl.pybatchid = pObjBatch.pybatchid;
                    //objProcessDtl.processname = "Create Auto Payroll";
                    //objProcessDtl.processstartedon = pObjBatch.startedon;
                    //objProcessDtl.processendedon = pObjBatch.endedon;
                    objProcessDtl.recordssearched = recordsSearched;
                    objProcessDtl.recordsprocessed = recordsProcessed;
                    objProcessDtl.status = 1;
                    objProcessDtl.errormessage = errorMassage.ToString();
                    objList.Add(objProcessDtl);
                    BLLPYBatchProcessDetailStybatchd.UpdateData(ref objTrx, ref objList);
                }
                else
                {
                    object objTrx = null;
                    List<DVOPYBatchProcessDetailStybatchd> objList = new List<DVOPYBatchProcessDetailStybatchd>();
                    DVOPYBatchProcessDetailStybatchd obj = new DVOPYBatchProcessDetailStybatchd();
                    obj.pybatchid = pObjBatch.pybatchid;
                    obj.processname = "Create Auto Payroll";
                    //obj.processstartedon = pObjBatch.startedon;
                    //obj.processendedon = pObjBatch.endedon;
                    obj.recordssearched = recordsSearched;
                    obj.recordsprocessed = recordsProcessed;
                    obj.status = 1;
                    obj.searchcriteria = pObjBatch.searchcriteria;
                    obj.errormessage = errorMassage.ToString();
                    objList.Add(obj);
                    BLLPYBatchProcessDetailStybatchd.InsertData(ref objTrx, ref objList);
                }
                #endregion
            }
            return objDVOPayrollProcess_PayEmployeeList;
        }

        DataSet dsDuplicateSSN = new DataSet();
        private void LoadAllDupSSN()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                dsDuplicateSSN = objDalBaseClass.GetData((new DVOPayrollautopay()).FIND_DUPSSN_QUERY());
                if (dsDuplicateSSN != null && dsDuplicateSSN.Tables.Count > 0)
                {
                    dsDuplicateSSN.Tables[0].Columns[0].ColumnName = "SSN";
                    dsDuplicateSSN.Tables[0].Columns[1].ColumnName = "COUNT";
                }
            }
            catch (Exception ex) { }
        }
        DataSet dsDuplicateSSNPay = new DataSet();
        private void LoadAllDupSSNPay()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                dsDuplicateSSNPay = objDalBaseClass.GetData((new DVOPayrollautopay()).FIND_DUPSSN_PAY_QUERY());
                if (dsDuplicateSSNPay != null && dsDuplicateSSNPay.Tables.Count > 0)
                {
                    dsDuplicateSSNPay.Tables[0].Columns[0].ColumnName = "SSN";
                    dsDuplicateSSNPay.Tables[0].Columns[1].ColumnName = "EMPL_CODE";
                    dsDuplicateSSNPay.Tables[0].Columns[2].ColumnName = "COUNT";
                }
            }
            catch (Exception ex) { }
        }
        DataSet dsallPayrollGLAccountsData = new DataSet();
        private void LoadAllPayrollGLAccountsData()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];

                dsallPayrollGLAccountsData = objDalBaseClass.GetData((new DVOGLPayrollGLAccounts()).GET_GL_ACCOUNTS(ref parameters));
                if (dsallPayrollGLAccountsData != null && dsallPayrollGLAccountsData.Tables.Count > 0)
                {
                    dsallPayrollGLAccountsData.Tables[0].Columns[0].ColumnName = "acct_no";
                    dsallPayrollGLAccountsData.Tables[0].Columns[1].ColumnName = "acct_type";
                    dsallPayrollGLAccountsData.Tables[0].Columns[2].ColumnName = "keyvalue";
                    dsallPayrollGLAccountsData.Tables[0].Columns[3].ColumnName = "acct_desc";
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadallIncomestyincr()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];

                dsAllIncomesstyincr = objDalBaseClass.GetData((new DVOUpdateIncCode()).GetIncomeDataOnly(ref parameters));
                if (dsAllIncomesstyincr != null && dsAllIncomesstyincr.Tables.Count > 0)
                {
                    dsAllIncomesstyincr.Tables[0].Columns[0].ColumnName = "inc_code";
                    dsAllIncomesstyincr.Tables[0].Columns[1].ColumnName = "dfltaccounttype";

                }
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadAllobligationsMasterOblCodes()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];

                dsAllObligationsMasterOblCodes = objDalBaseClass.GetData((new DVOMasterOblCodes1()).GetObligationData(ref parameters));
                if (dsAllObligationsMasterOblCodes != null && dsAllObligationsMasterOblCodes.Tables.Count > 0)
                {
                    dsAllObligationsMasterOblCodes.Tables[0].Columns[0].ColumnName = "obl_code";
                    dsAllObligationsMasterOblCodes.Tables[0].Columns[1].ColumnName = "dfltaccounttype";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DVOPayrollProcess_PayEmployee CalculatePayrolls(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee)
        {

            // bool dedflag = false;
            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();
            objProcess_PayEmployee.cash_amount = 0.0M;
            objProcess_PayEmployee.inc_gross = 0.0M;
            objProcess_PayEmployee.inc_taxable = 0.0M;
            objProcess_PayEmployee.ded_fica = 0.0M;
            objProcess_PayEmployee.ded_medicare = 0.0M;
            objProcess_PayEmployee.ded_fedtax = 0.0M;
            objProcess_PayEmployee.ded_statax = 0.0M;
            objProcess_PayEmployee.ded_loctax = 0.0M;
            objProcess_PayEmployee.ded_other = 0.0M;
            objProcess_PayEmployee.obl_futa = 0.0M;
            objProcess_PayEmployee.obl_fica = 0.0M;
            objProcess_PayEmployee.obl_medicare = 0.0M;
            objProcess_PayEmployee.obl_other = 0.0M;
            objProcess_PayEmployee.obl_total = 0.0M;
            objProcess_PayEmployee.inc_net = 0.0M;
            objProcess_PayEmployee.inc_expense = 0.0M;
            objProcess_PayEmployee.total_hours = 0.0M;

            for (int i = 0; i < objPayrollIncomesglobal.Count; i++)
            {
                objProcess_PayEmployee = CalculateIncomes(ref objProcess_PayEmployee, i);
                if (objPayrollIncomesglobal[i].inc_type != "F")
                {
                    objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable + objPayrollIncomesglobal[i].amount;
                }
            }
            // set initial taxable & net income (gross cannot be less than zero)
            //
            //# Taxable income is derived from determining which income codes are to be taxed as
            //#  opposed to which deductions reduce the gross.  Only certain income codes are to
            //#  assessed SOC-SEC and LEVY taxes so we need to calculate thses separately.  These
            //#  are indicated with an "F" which indicates that the income code in question is exempt
            //#  these two taxes.
            //# let p_ypayre.inc_taxable = p_ypayre.inc_gross
            //##### ^^^^^ - - - -- - - - ^^^^^ ##########
            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross;


            // calculate deductions that reduce taxable income
            for (int j = 0; j < objPayrolldeductionsglobal.Count; j++)
            {
                if ((objPayrolldeductionsglobal[j].ded_taxred != "N") &&
                (objPayrolldeductionsglobal[j].ded_taxred != null))
                {
                    objPayrolldeductionsglobal[j].dedflag = true;
                    CalculateDeductions(ref objProcess_PayEmployee, j);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[j].amount;
                    // figure the effect on wage bases
                    switch (objPayrolldeductionsglobal[j].ded_taxred)
                    {
                        case "A":
                            {
                                // deduction reduces all wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable -
                                objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "B":
                            {
                                // deduction reduces taxable and fica wage bases

                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "C":
                            {
                                //deduction reduces taxable and futa wage bases
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "D":
                            {
                                // deduction reduces futa and fica wage bases
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "F":
                            {
                                // deduction reduces fica wage base only
                                fica_wages = fica_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }
                        case "T":
                            {
                                // deduction reduces taxable wage base only
                                objProcess_PayEmployee.inc_taxable = objProcess_PayEmployee.inc_taxable - objPayrolldeductionsglobal[j].amount;
                                break;
                            }
                        case "U":
                            {
                                // deduction reduces futa wage base only
                                futa_wages = futa_wages - objPayrolldeductionsglobal[j].amount ?? 0;//make nullable decimal By Rahul
                                break;
                            }

                    }
                }
                else
                {
                    objPayrolldeductionsglobal[j].dedflag = false;
                }


            }
            // calculate deductions that do not reduce taxable income
            for (int k = 0; k < objPayrolldeductionsglobal.Count; k++)
            {
                if (objPayrolldeductionsglobal[k].dedflag == false)
                {
                    CalculateDeductions(ref objProcess_PayEmployee, k);
                    objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_net - objPayrolldeductionsglobal[k].amount;
                }
            }

            // calculate obligations
            for (int k = 0; k < objPayrollobligationsglobal.Count; k++)
            {
                CalculateObligations(ref objProcess_PayEmployee, k);
            }
            //add final totals
            //adjust totals using overall deduction accumulation instead
            //of running net to take care of possible rounding errors
            objProcess_PayEmployee.cash_amount = objProcess_PayEmployee.inc_gross +
           objProcess_PayEmployee.inc_expense - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_fedtax + objProcess_PayEmployee.ded_medicare +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax +
           objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.inc_net = objProcess_PayEmployee.inc_gross - (objProcess_PayEmployee.ded_fica +
           objProcess_PayEmployee.ded_medicare + objProcess_PayEmployee.ded_fedtax +
           objProcess_PayEmployee.ded_loctax + objProcess_PayEmployee.ded_statax + objProcess_PayEmployee.ded_other);

            objProcess_PayEmployee.obl_total = objProcess_PayEmployee.obl_futa + objProcess_PayEmployee.obl_fica +
            objProcess_PayEmployee.obl_medicare + objProcess_PayEmployee.obl_other;

            return objProcess_PayEmployee;
        }
        public DVOPayrollProcess_PayEmployee CalculateIncomes(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            //this function recalculates the income amount and resets the gross wages.

            // DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            //recalculate the amount
            objPayrollIncomesglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", (objPayrollIncomesglobal[i].inc_rate * objPayrollIncomesglobal[i].number)));

            // make sure amount is not null and non-negative
            if (objPayrollIncomesglobal[i].amount < 0)
            {
                objPayrollIncomesglobal[i].amount = 0.0M;
            }

            //add to the gross wages or expenses/advances
            switch (objPayrollIncomesglobal[i].inc_type)
            {
                case "H":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = (objpayrollProcess_PayEmployee.inc_gross ?? 0) + (objPayrollIncomesglobal[i].amount ?? 0);
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "E":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = (objpayrollProcess_PayEmployee.inc_expense ?? 0) + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "A":
                    {
                        objpayrollProcess_PayEmployee.inc_expense = (objpayrollProcess_PayEmployee.inc_expense ?? 0) + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "F":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = (objpayrollProcess_PayEmployee.inc_gross ?? 0) + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "U":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = (objpayrollProcess_PayEmployee.inc_gross ?? 0) + (objPayrollIncomesglobal[i].amount ?? 0);
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                case "B":
                    {
                        objpayrollProcess_PayEmployee.inc_gross = (objpayrollProcess_PayEmployee.inc_gross ?? 0) + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
                default:
                    {
                        objpayrollProcess_PayEmployee.inc_gross = (objpayrollProcess_PayEmployee.inc_gross ?? 0) + (objPayrollIncomesglobal[i].amount ?? 0);
                        fica_wages = fica_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        futa_wages = futa_wages + (objPayrollIncomesglobal[i].amount ?? 0);
                        break;
                    }
            }

            objpayrollProcess_PayEmployee.total_hours = (objpayrollProcess_PayEmployee.total_hours ?? 0) + (objPayrollIncomesglobal[i].hours ?? 0);

            return objpayrollProcess_PayEmployee;
        }

        public static void CalculateIncomeChanges(string IncomeCodeType, decimal IncomeAmount, out decimal GrossIncomeChange, out decimal TaxableIncomeChange, out decimal FicaWagesChange, out decimal FutaWagesChange)
        {
            GrossIncomeChange = 0;
            FicaWagesChange = 0;
            FutaWagesChange = 0;
            TaxableIncomeChange = 0;

            switch (IncomeCodeType)
            {
                case "H":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "E":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "A":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "F":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        break;
                    }
                case "U":
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                case "B":
                    {
                        GrossIncomeChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
                default:
                    {
                        GrossIncomeChange = IncomeAmount;
                        FicaWagesChange = IncomeAmount;
                        FutaWagesChange = IncomeAmount;
                        TaxableIncomeChange = IncomeAmount;
                        break;
                    }
            }
        }

        public DVOPayrollProcess_PayEmployee CalculateDeductions(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_deductions = 0.0M;
            decimal currentdeductions = 0.0M;
            //
            if (objPayrolldeductionsglobal[i].ded_type == null)
            {
                objPayrolldeductionsglobal[i].ded_type = "T";
            }
            //Set the maximum Deduction Limit IN case No limit Defined 
            if ((objPayrolldeductionsglobal[i].ded_limit == null))
            {
                Max_deductions = Convert.ToDecimal(999999999999.99);  // large decimal(12) value
            }
            else
            {
                objPayrolldeductionsglobal[i].ded_ytd = 0.0M;
                currentdeductions = 0;
                if (dup_ssn == 1)
                {
                    if (dsAllDeductions != null && dsAllDeductions.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDeductions.Tables[0].Select("empl_code='" + objpayrollProcess_PayEmployee.EmplCode.Trim() + "' AND ded_code='" + objPayrolldeductionsglobal[i].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                            objPayrolldeductionsglobal[i].ded_ytd = drs[0]["ded_ytd"] != DBNull.Value ? (decimal?)drs[0]["ded_ytd"] : null;
                    }

                }
                else
                {
                    //currentdeductions;
                    if (dsAllDeductions != null && dsAllDeductions.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDeductions.Tables[0].Select("soc_sec_num='" + empl_ssn.Trim() + "' AND ded_code='" + objPayrolldeductionsglobal[i].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                            objPayrolldeductionsglobal[i].ded_ytd = drs[0]["ded_ytd"] != DBNull.Value ? (decimal?)drs[0]["ded_ytd"] : null;
                    }


                    if (same_person)
                    {
                        if (dsAllDeductions != null && dsAllDeductions.Tables.Count > 0)
                        {
                            DataRow[] drs = dsAllDeductions.Tables[0].Select("empl_code='" + objpayrollProcess_PayEmployee.EmplCode.Trim() + "' AND soc_sec_num='" + empl_ssn.Trim() + "' AND ded_code='" + objPayrolldeductionsglobal[i].ded_code.Trim() + "'");
                            if (drs.Length > 0)
                                objPayrolldeductionsglobal[i].ded_ytd = drs[0]["ded_ytd"] != DBNull.Value ? (decimal?)drs[0]["ded_ytd"] : null;
                        }

                    }
                }


                if (currentdeductions != 0)
                {
                    objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + currentdeductions;
                }
                // get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrolldeductionsglobal[j].ded_code == objPayrolldeductionsglobal[i].ded_code)
                    {
                        if (objPayrolldeductionsglobal[j].amount != 0)
                        {
                            objPayrolldeductionsglobal[i].ded_ytd = objPayrolldeductionsglobal[i].ded_ytd + objPayrolldeductionsglobal[j].amount;
                        }
                    }
                }
                Max_deductions = (objPayrolldeductionsglobal[i].ded_limit ?? 0) - (objPayrolldeductionsglobal[i].ded_ytd) ?? 0; //make nullable decimal By Rahul
            }

            if (Max_deductions < 0.0M)
            {
                Max_deductions = 0.0M;
            }
            //calc the amount of the deduction relative to type
            if (objPayrolldeductionsglobal[i].ded_code.Trim() == objpayrollProcess_PayEmployee.StateTaxCode.Trim())
            {
                //call the state tax calculation logic
                objPayrolldeductionsglobal[i].amount = state_calc(i);
            }
            switch (objPayrolldeductionsglobal[i].ded_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != 0))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_gross));
                            }
                        }
                        else
                        {
                            //make nullable decimal By Rahul
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod);//make nullable decimal By Rahul
                        }
                        break;
                    }

                case "T":
                    {
                        //calculate amount using taxable wage base
                        // check for a tax table and retrieve the amount
                        if ((objPayrolldeductionsglobal[i].tax_code == string.Empty) || (objPayrolldeductionsglobal[i].ded_rate != null))
                        {
                            if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                            }
                            else
                            {
                                objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.inc_taxable));
                            }
                        }
                        else
                        {
                            //make nullable decimal By Rahul
                            objPayrolldeductionsglobal[i].amount = ded_taxcalc(i, objpayrollProcess_PayEmployee.inc_taxable ?? 0, objListemplforprocess[currentempnoid].PayPeriod);//make nullable decimal By Rahul
                        }
                        break;
                    }
                case "U":
                    {
                        //calculate amount using futa wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * futa_wages));
                        }
                        break;
                    }
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrolldeductionsglobal[i].ded_rate >= 1) || (objPayrolldeductionsglobal[i].ded_rate == 0))
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        }
                        else
                        {
                            objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * fica_wages));
                        }
                        break;
                    }
                case "H":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate * objpayrollProcess_PayEmployee.total_hours));
                        break;
                    }
                case "N":
                    {
                        objPayrolldeductionsglobal[i].amount = Convert.ToDecimal((new BLLPayrollFunctions()).Al_Round("a", objPayrolldeductionsglobal[i].ded_rate));
                        break;
                    }
                default:
                    {
                        objPayrolldeductionsglobal[i].amount = 0.0M;
                        break;
                    }

            }


            //make sure deduction is not greater than net or zero if net < 0
            if (objPayrolldeductionsglobal[i].amount > 0)
            {
                if (objpayrollProcess_PayEmployee.inc_net < 0)
                {
                    objPayrolldeductionsglobal[i].amount = 0;
                }
                else
                {
                    if (objPayrolldeductionsglobal[i].amount > objpayrollProcess_PayEmployee.inc_net)
                    {
                        objPayrolldeductionsglobal[i].amount = objpayrollProcess_PayEmployee.inc_net;
                    }
                }
            }
            //Checking if the Deduction Mentioned is More than Pay Limit Provided 

            if (objPayrolldeductionsglobal[i].pay_limit != null)
            {
                if (objPayrolldeductionsglobal[i].amount > objPayrolldeductionsglobal[i].pay_limit)
                {
                    objPayrolldeductionsglobal[i].amount = objPayrolldeductionsglobal[i].pay_limit;
                }
            }
            // check for limit with Maximum Deductions Allowed 
            if (objPayrolldeductionsglobal[i].amount > Max_deductions)
            {
                objPayrolldeductionsglobal[i].amount = Max_deductions;
            }

            // post amount to correct total
            if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fedtax_code)
            {
                objpayrollProcess_PayEmployee.ded_fedtax = objpayrollProcess_PayEmployee.ded_fedtax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].statax_code)
            {
                objpayrollProcess_PayEmployee.ded_statax = objpayrollProcess_PayEmployee.ded_statax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].loctax_code)
            {
                objpayrollProcess_PayEmployee.ded_loctax = objpayrollProcess_PayEmployee.ded_loctax + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.ded_fica = objpayrollProcess_PayEmployee.ded_fica + objPayrolldeductionsglobal[i].amount;
            }
            else if (objPayrolldeductionsglobal[i].ded_code == objstycntrcList[0].medicare_code)
            {
                objpayrollProcess_PayEmployee.ded_medicare = objpayrollProcess_PayEmployee.ded_medicare + objPayrolldeductionsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.ded_other = objpayrollProcess_PayEmployee.ded_other + objPayrolldeductionsglobal[i].amount;
            }




            return objpayrollProcess_PayEmployee;
        }
        public DVOPayrollProcess_PayEmployee CalculateObligations(ref DVOPayrollProcess_PayEmployee objpayrollProcess_PayEmployee, int i)
        {
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal Max_Obligations = 0.0M;
            decimal Current_obligations = 0.0M;
            //decimal obligationYTD = 0.0M;
            decimal Max_deductions = 0.0M;
            // this function calculates the obligation amount and updates
            //   cumulative totals for the control obligation codes

            // set obligation type
            if (objPayrollobligationsglobal[i].obl_type == null)
            {
                objPayrollobligationsglobal[i].obl_type = "T";
            }

            // get obligation limit and accrual

            // set the maximum obligation
            if (objPayrollobligationsglobal[i].obl_limit == null)
            {
                Max_deductions = 999999999.99M;  // large decimal(12) value
            }
            else
            {
                //check for current accrual
                //Current_obligations = 0.0M;
                if (dup_ssn == 1)
                {
                    if (dsAllObligations != null && dsAllObligations.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllObligations.Tables[0].Select("empl_code='" + objpayrollProcess_PayEmployee.EmplCode.Trim() + "' AND obl_code='" + objPayrollobligationsglobal[i].obl_code.Trim() + "'");
                        if (drs.Length > 0)
                            objPayrollobligationsglobal[i].obl_ytd = drs[0]["obl_ytd"] != DBNull.Value ? (decimal?)drs[0]["obl_ytd"] : null;
                    }

                    ////obligationYTD = 0.0M; //Set it with Year to date obligations
                    //object[] parameter1 = new object[2];
                    //parameter1[0] = objpayrollProcess_PayEmployee.EmplCode;
                    //parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    //object Current_obl = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd);
                    //if (Current_obl.ToString().Trim() != "")
                    //{
                    //    objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obl);
                    //}
                }
                else
                {
                    if (dsAllObligations != null && dsAllObligations.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllObligations.Tables[0].Select("soc_sec_num='" + empl_ssn.Trim() + "' AND obl_code='" + objPayrollobligationsglobal[i].obl_code.Trim() + "'");
                        if (drs.Length > 0)
                            objPayrollobligationsglobal[i].obl_ytd = drs[0]["obl_ytd"] != DBNull.Value ? (decimal?)drs[0]["obl_ytd"] : null;
                    }

                    ////obligationYTD = 0.0M; //Set it with Year to date obligations
                    //object[] parameter1 = new object[2];
                    //parameter1[0] = empl_ssn;
                    //parameter1[1] = objPayrollobligationsglobal[i].obl_code;
                    //object oblYTD = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter1, objPayrollobligationsglobal[0].FIND_stypayodytd1);
                    //if (oblYTD.ToString().Trim() != "")
                    //{
                    //    objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(oblYTD);
                    //}

                    if (same_person)
                    {
                        if (dsAllObligations != null && dsAllObligations.Tables.Count > 0)
                        {
                            DataRow[] drs = dsAllObligations.Tables[0].Select("empl_code='" + objpayrollProcess_PayEmployee.EmplCode.Trim() + "' AND soc_sec_num='" + empl_ssn.Trim() + "' AND obl_code='" + objPayrollobligationsglobal[i].obl_code.Trim() + "'");
                            if (drs.Length > 0)
                                objPayrollobligationsglobal[i].obl_ytd = drs[0]["obl_ytd"] != DBNull.Value ? (decimal?)drs[0]["obl_ytd"] : null;
                        }

                        ////Current_obligations = 0.0M; //set it with current obligations 
                        //object[] parameter2 = new object[3];
                        //parameter2[0] = objpayrollProcess_PayEmployee.EmplCode;
                        //parameter2[1] = empl_ssn;
                        //parameter2[2] = objPayrollobligationsglobal[i].obl_code;
                        //object Current_obligations1 = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameter2, objPayrollobligationsglobal[0].FIND_stypayodytd2);
                        //if (Current_obligations1.ToString().Trim() != "")
                        //{
                        //    objPayrollobligationsglobal[i].obl_ytd = Convert.ToDecimal(Current_obligations1);
                        //}

                    }
                }
                if (objPayrollobligationsglobal[i].obl_ytd == null)
                    objPayrollobligationsglobal[i].obl_ytd = 0;
                if (Current_obligations != 0)
                {
                    objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + Current_obligations;//objPayrollobligationsglobal[i].amount;
                }
                //get accrual for this payroll entry
                for (int j = 0; j < i - 1; j++)
                {
                    if (objPayrollobligationsglobal[j].obl_code == objPayrollobligationsglobal[i].obl_code)
                    {
                        if (objPayrollobligationsglobal[j].amount != null)
                        {
                            objPayrollobligationsglobal[i].obl_ytd = objPayrollobligationsglobal[i].obl_ytd + objPayrollobligationsglobal[j].amount;
                        }
                    }

                }
                Max_Obligations = (objPayrollobligationsglobal[i].obl_limit - objPayrollobligationsglobal[i].obl_ytd) ?? 0;//make nullable decimal By Rahul
            }
            if (Max_Obligations < 0)
            {
                Max_Obligations = 0.0M;
            }
            switch (objPayrollobligationsglobal[i].obl_type)
            {
                case "G":
                    {
                        //calculate amount using gross wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_gross));
                        }
                    }
                    break;
                case "T":
                    {
                        //calculate amount using taxable wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.inc_taxable));
                        }
                    }
                    break;
                case "U":
                    //calculate amount using futa wage base
                    {
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_futa));
                        }
                    }
                    break;
                case "F":
                    {
                        //calculate amount using fica wage base
                        if ((objPayrollobligationsglobal[i].obl_rate >= 1) || (objPayrollobligationsglobal[i].obl_rate == 0))
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                        }
                        else
                        {
                            objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.obl_fica));
                        }
                    }
                    break;
                case "E":
                    // Based on the employees deduction amount
                    {
                        for (int j = 0; j <= objPayrolldeductionsglobal.Count; j++)
                        {
                            if (objPayrollobligationsglobal[i].obl_code == objPayrolldeductionsglobal[j].ded_code)
                            {
                                objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objPayrolldeductionsglobal[j].amount));
                                break;
                            }
                        }
                    }
                    break;
                case "H":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate * objpayrollProcess_PayEmployee.total_hours));
                    }
                    break;
                case "N":
                    {
                        objPayrollobligationsglobal[i].amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrollobligationsglobal[i].obl_rate));
                    }
                    break;
                    //default:
                    //    {
                    //        objPayrollobligationsglobal[i].amount = 0.0M;
                    //    }
            }

            //Modified by Sarvjeet On 13/08/2009
            //Again Modified by Rohit to put Null 
            //Checkiing For Pay Limit
            if (objPayrollobligationsglobal[i].pay_limit != null)
            {

                if (objPayrollobligationsglobal[i].amount > objPayrollobligationsglobal[i].pay_limit)
                {

                    objPayrollobligationsglobal[i].amount = objPayrollobligationsglobal[i].pay_limit;

                }

            }
            //check for limit
            if (objPayrollobligationsglobal[i].amount > Max_Obligations)
            {
                objPayrollobligationsglobal[i].amount = Max_Obligations;
            }
            // post amount to correct total
            if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].futa_code)
            {
                objpayrollProcess_PayEmployee.obl_futa = objpayrollProcess_PayEmployee.obl_futa + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].fica_code)
            {
                objpayrollProcess_PayEmployee.obl_fica = objpayrollProcess_PayEmployee.obl_fica + objPayrollobligationsglobal[i].amount;
            }
            else if (objPayrollobligationsglobal[i].obl_code == objstycntrcList[0].medicare_ob_code)
            {
                objpayrollProcess_PayEmployee.obl_medicare = objpayrollProcess_PayEmployee.obl_medicare + objPayrollobligationsglobal[i].amount;
            }
            else
            {
                objpayrollProcess_PayEmployee.obl_other = objpayrollProcess_PayEmployee.obl_other + objPayrollobligationsglobal[i].amount;
            }

            //   DVOPayrollProcess_PayEmployee objProcess_PayEmployee = new DVOPayrollProcess_PayEmployee();

            return objpayrollProcess_PayEmployee;
        }
        public List<DVOPayrollstypayid> LoadIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOUpdateTimeCard> objlistTimecard = new List<DVOUpdateTimeCard>();
            objlistTimecard = LoadTimeCardIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            List<DVOPayrollstypayid> objlistpayrollincomes = new List<DVOPayrollstypayid>();

            //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //object[] parameters = new object[1];
            Int32 Maxlineno = 0;
            TimeCardUsedForPayroll = false;
            if (objlistTimecard.Count < 1)
            {
                objlistTimecard = LoadEmployeeIncomes(EmployeeCode, EopDate, FlexacctType, FlexDepartment);
            }
            else
            {
                TimeCardUsedForPayroll = true;
                UsedTimeCardNo = objlistTimecard[0].card_no;
            }
            for (int i = 0; i < objlistTimecard.Count; i++)
            {
                DVOPayrollstypayid objemployeeincome = new DVOPayrollstypayid();
                objemployeeincome.inc_code = objlistTimecard[i].inc_code_id;
                objemployeeincome.inc_rate = objlistTimecard[i].inc_rate_id;
                objemployeeincome.number = objlistTimecard[i].inc_number_id;
                objemployeeincome.hours = objlistTimecard[i].inc_hours_id;
                objemployeeincome.add_code = objlistTimecard[i].add_code_cr;
                objemployeeincome.inc_type = objlistTimecard[i].inc_type_cr;
                if ((objemployeeincome.add_code == "Y") || (objemployeeincome.add_code == "Z"))
                {
                    //parameters[0] = EmployeeCode;
                    //object Maxlineno1 = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOPayrollautopay()).EMPLOYEEMAXLINENOGET);
                    //if (Maxlineno1 == null)
                    //    Maxlineno = 0;
                    //Maxlineno = Maxlineno + i;
                }
                else
                {
                    objemployeeincome.add_code = "N";
                    objemployeeincome.line_no = objlistTimecard[i].line_no_id;
                }
                objemployeeincome.lo_inc_amt = objlistTimecard[i].lo_inc_amt_id;
                objemployeeincome.hi_inc_amt = objlistTimecard[i].hi_inc_amt_id;
                // Take the timecard account number first.  Comment out the following
                // line here, but use it as a default if the timecard account number is null.
                // let p_ypayid[n].acct_no = inc_ref[n].acct_no
                objemployeeincome.acct_no = objlistTimecard[i].timecd_acct_no;
                objemployeeincome.Department = objlistTimecard[i].department_id;

                //Assign Default Values as required ........
                if (objemployeeincome.lo_inc_amt == null)
                {
                    objemployeeincome.lo_inc_amt = objlistTimecard[i].dflt_lo_inc_amt_cr;
                }
                if (objemployeeincome.hi_inc_amt == null)
                {
                    objemployeeincome.hi_inc_amt = objlistTimecard[i].dflt_hi_inc_amt_cr;
                }
                if (objemployeeincome.inc_rate == null)
                {
                    objemployeeincome.inc_rate = objlistTimecard[i].dflt_rate_cr;
                }
                if (objemployeeincome.hours == null)
                {
                    objemployeeincome.hours = objlistTimecard[i].dflt_hours_cr;
                }
                if (objemployeeincome.number == null)
                {
                    objemployeeincome.number = objlistTimecard[i].dflt_num_cr;
                }
                // use defaults if necessary

                if (objemployeeincome.acct_no == 0)
                {
                    objemployeeincome.acct_no = objlistTimecard[i].acct_no_id;
                    if (objemployeeincome.acct_no == 0)
                    {
                        objemployeeincome.acct_no = objlistTimecard[i].dflt_acct_cr;
                    }
                }
                if ((objemployeeincome.Department == string.Empty) || (objemployeeincome.Department == null))
                {
                    objemployeeincome.Department = objlistTimecard[i].dflt_dept_cr;
                }
                objlistpayrollincomes.Add(objemployeeincome);
            }
            return objlistpayrollincomes;
        }

        public List<DVOPayrollstypaydd> LoadDeductions(string EmployeeCode, DateTime EOPdate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollstypaydd> objListpayrollstypaydd = new List<DVOPayrollstypaydd>();
            List<DVOMasterEmployeeDeductions> objListemployeeDeddefaultdata = new List<DVOMasterEmployeeDeductions>();
            DVOPayrollstypaydd objpayrollstypaydd = new DVOPayrollstypaydd();
            //string Mixkeyvalue;
            //string MixAcctType;
            //Int32 MixaccountNo;
            Int32 Dedreccount;
            //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //object[] parameters = new object[0];

            if (dsAllDeductions != null && dsAllDeductions.Tables.Count > 0 && dsAllDeductions.Tables[0].Rows.Count > 0)
            {
                DataRow[] drdeductions = dsAllDeductions.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "'");
                if (drdeductions != null && drdeductions.Length > 0)
                    foreach (DataRow dr in drdeductions)
                    {
                        using (DVOMasterEmployeeDeductions objemployeedefaultdedrec = new DVOMasterEmployeeDeductions())
                        {
                            objemployeedefaultdedrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                            objemployeedefaultdedrec.dfltkeyvalue = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                            objemployeedefaultdedrec.ded_code = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                            objemployeedefaultdedrec.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                            objemployeedefaultdedrec.ded_rate = (dr[4] != DBNull.Value ? (decimal?)dr[4] : null);
                            objemployeedefaultdedrec.ded_limit = (dr[5] != DBNull.Value ? (decimal?)dr[5] : null);
                            objemployeedefaultdedrec.pay_limit = (dr[6] != DBNull.Value ? (decimal?)dr[6] : null);
                            objemployeedefaultdedrec.balanceamt = (dr[7] != DBNull.Value ? (decimal?)dr[7] : null);
                            objemployeedefaultdedrec.ded_apply = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]).Trim() : string.Empty);
                            objemployeedefaultdedrec.lo_ded_amt = (dr[9] != DBNull.Value ? (decimal?)dr[9] : null);
                            objemployeedefaultdedrec.hi_ded_amt = (dr[10] != DBNull.Value ? (decimal?)dr[10] : null);
                            objemployeedefaultdedrec.acct_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
                            objemployeedefaultdedrec.department = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]).Trim() : "000");
                            objemployeedefaultdedrec.ded_ytd = (dr[13] != DBNull.Value ? (decimal?)dr[13] : null);
                            objemployeedefaultdedrec.ded_date = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                            objemployeedefaultdedrec.description = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : string.Empty);
                            objemployeedefaultdedrec.ded_type = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]).Trim() : string.Empty);
                            objemployeedefaultdedrec.ded_taxred = (dr[17] != DBNull.Value ? Convert.ToString(dr[17]).Trim() : string.Empty);
                            objemployeedefaultdedrec.dflt_rate = (dr[18] != DBNull.Value ? (decimal?)dr[18] : null);
                            objemployeedefaultdedrec.dflt_limit = (dr[19] != DBNull.Value ? (decimal?)dr[19] : null);
                            objemployeedefaultdedrec.dflt_pay_limit = (dr[20] != DBNull.Value ? (decimal?)dr[20] : null);
                            objemployeedefaultdedrec.yearrollover = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : string.Empty);
                            objemployeedefaultdedrec.dflt_lo_ded_amt = (dr[22] != DBNull.Value ? (decimal?)dr[22] : null);
                            objemployeedefaultdedrec.dflt_hi_ded_amt = (dr[23] != DBNull.Value ? (decimal?)dr[23] : null);
                            objemployeedefaultdedrec.dflt_acct = (dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0);
                            objemployeedefaultdedrec.dflt_dept = (dr[25] != DBNull.Value ? Convert.ToString(dr[25]).Trim() : "000");
                            objemployeedefaultdedrec.dflt_apply = (dr[26] != DBNull.Value ? Convert.ToString(dr[26]).Trim() : string.Empty);

                            //parameters = new object[2];
                            //parameters[0] = objemployeedefaultdedrec.ded_code;
                            //parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            //object taxcode = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objpaydatasearch.DeductionTaxCodeGet);
                            //if (taxcode != DBNull.Value)
                            //{
                            //    objemployeedefaultdedrec.tax_code = taxcode.ToString().Trim();
                            //}

                            //parameters = new object[2];
                            //parameters[0] = objemployeedefaultdedrec.ded_code;
                            //parameters[1] = EOPdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                            //object result = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmployeeDeductions()).DeductioncodeTaxget);
                            //if (result != null)
                            //    objemployeedefaultdedrec.tax_code = result.ToString().Trim();

                            if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                            {
                                DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code='" + objemployeedefaultdedrec.ded_code.Trim() + "'");
                                if (drs.Length > 0)
                                    objemployeedefaultdedrec.tax_code = objemployeedefaultdedrec.ded_code.Trim();
                            }


                            if (objemployeedefaultdedrec.ded_apply == string.Empty)
                            {
                                objemployeedefaultdedrec.ded_apply = objemployeedefaultdedrec.dflt_apply;
                            }
                            // assign defaults as required
                            if (objemployeedefaultdedrec.lo_ded_amt == null)
                            {
                                objemployeedefaultdedrec.lo_ded_amt = objemployeedefaultdedrec.dflt_lo_ded_amt;
                            }
                            if (objemployeedefaultdedrec.hi_ded_amt == null)
                            {
                                objemployeedefaultdedrec.hi_ded_amt = objemployeedefaultdedrec.dflt_hi_ded_amt;
                            }
                            if (objemployeedefaultdedrec.ded_rate == null)
                            {
                                objemployeedefaultdedrec.ded_rate = objemployeedefaultdedrec.dflt_rate;
                            }
                            if (objemployeedefaultdedrec.acct_no == 0)
                            {
                                objemployeedefaultdedrec.acct_no = objemployeedefaultdedrec.dflt_acct;
                            }
                            if (objemployeedefaultdedrec.department == null)
                            {
                                objemployeedefaultdedrec.department = objemployeedefaultdedrec.dflt_dept;
                            }
                            if (objemployeedefaultdedrec.ded_limit == null)
                            {
                                objemployeedefaultdedrec.ded_limit = objemployeedefaultdedrec.dflt_limit;
                            }

                            objListemployeeDeddefaultdata.Add(objemployeedefaultdedrec);
                        }
                    }
            }



            //object[] parameters = new object[1];
            //parameters[0] = EmployeeCode;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeDeductions), (new DVOMasterEmployeeDeductions()).EmpDedanddefaults))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DVOMasterEmployeeDeductions objemployeedefaultdedrec = new DVOMasterEmployeeDeductions();

            //        objemployeedefaultdedrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.dfltkeyvalue = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.ded_code = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.line_no = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
            //        objemployeedefaultdedrec.ded_rate = (dr[4] != DBNull.Value ? Convert.ToDecimal(dr[4]) : 0.0M);
            //        objemployeedefaultdedrec.ded_limit = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0);
            //        objemployeedefaultdedrec.pay_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
            //        objemployeedefaultdedrec.balanceamt = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
            //        objemployeedefaultdedrec.ded_apply = (dr[8] != DBNull.Value ? Convert.ToString(dr[8]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.lo_ded_amt = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0);
            //        objemployeedefaultdedrec.hi_ded_amt = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0);
            //        objemployeedefaultdedrec.acct_no = (dr[11] != DBNull.Value ? Convert.ToInt32(dr[11]) : 0);
            //        objemployeedefaultdedrec.department = (dr[12] != DBNull.Value ? Convert.ToString(dr[12]).Trim() : "000");
            //        objemployeedefaultdedrec.ded_ytd = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0);
            //        objemployeedefaultdedrec.ded_date = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.description = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.ded_type = (dr[16] != DBNull.Value ? Convert.ToString(dr[16]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.ded_taxred = (dr[17] != DBNull.Value ? Convert.ToString(dr[17]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.dflt_rate = (dr[18] != DBNull.Value ? Convert.ToDecimal(dr[18]) : 0.0M);
            //        objemployeedefaultdedrec.dflt_limit = (dr[19] != DBNull.Value ? Convert.ToDecimal(dr[19]) : 0);
            //        objemployeedefaultdedrec.dflt_pay_limit = (dr[20] != DBNull.Value ? Convert.ToDecimal(dr[20]) : 0);
            //        objemployeedefaultdedrec.yearrollover = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : string.Empty);
            //        objemployeedefaultdedrec.dflt_lo_ded_amt = (dr[22] != DBNull.Value ? Convert.ToDecimal(dr[22]) : 0);
            //        objemployeedefaultdedrec.dflt_hi_ded_amt = (dr[23] != DBNull.Value ? Convert.ToDecimal(dr[23]) : 0);
            //        objemployeedefaultdedrec.dflt_acct = (dr[24] != DBNull.Value ? Convert.ToInt32(dr[24]) : 0);
            //        objemployeedefaultdedrec.dflt_dept = (dr[25] != DBNull.Value ? Convert.ToString(dr[25]).Trim() : "000");
            //        objemployeedefaultdedrec.dflt_apply = (dr[26] != DBNull.Value ? Convert.ToString(dr[26]).Trim() : string.Empty);
            //        parameters = new object[2];
            //        parameters[0] = objemployeedefaultdedrec.ded_code;
            //        parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            //        object taxcode = objDalBaseClass.ExecuteScalar(ref parameters, objpaydatasearch.DeductionTaxCodeGet);
            //        if (taxcode != DBNull.Value)
            //        {
            //            objemployeedefaultdedrec.tax_code = taxcode.ToString().Trim();
            //        }

            //        parameters = new object[2];
            //        parameters[0] = objemployeedefaultdedrec.ded_code;
            //        parameters[1] = EOPdate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

            //        object result = objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeDeductions()).DeductioncodeTaxget);
            //        if (result != null)
            //            objemployeedefaultdedrec.tax_code = result.ToString().Trim();
            //        if (objemployeedefaultdedrec.ded_apply == string.Empty)
            //        {
            //            objemployeedefaultdedrec.ded_apply = objemployeedefaultdedrec.dflt_apply;
            //        }
            //        // assign defaults as required
            //        if (objemployeedefaultdedrec.lo_ded_amt == 0)
            //        {
            //            objemployeedefaultdedrec.lo_ded_amt = objemployeedefaultdedrec.dflt_lo_ded_amt;
            //        }
            //        if (objemployeedefaultdedrec.hi_ded_amt == 0)
            //        {
            //            objemployeedefaultdedrec.hi_ded_amt = objemployeedefaultdedrec.dflt_hi_ded_amt;
            //        }
            //        if (objemployeedefaultdedrec.ded_rate == 0)
            //        {
            //            objemployeedefaultdedrec.ded_rate = objemployeedefaultdedrec.dflt_rate;
            //        }
            //        if (objemployeedefaultdedrec.acct_no == 0)
            //        {
            //            objemployeedefaultdedrec.acct_no = objemployeedefaultdedrec.dflt_acct;
            //        }
            //        if (objemployeedefaultdedrec.department == null)
            //        {
            //            objemployeedefaultdedrec.department = objemployeedefaultdedrec.dflt_dept;
            //        }
            //        if (objemployeedefaultdedrec.ded_limit == 0)
            //        {
            //            objemployeedefaultdedrec.ded_limit = objemployeedefaultdedrec.dflt_limit;
            //        }


            //        objListemployeeDeddefaultdata.Add(objemployeedefaultdedrec);
            //    }
            //}

            Dedreccount = objListemployeeDeddefaultdata.Count;
            for (int i = 0; i < Dedreccount; i++)
            {
                objpayrollstypaydd = new DVOPayrollstypaydd();
                // check to make sure deduction should be taken now

                //if (objListemployeeDeddefaultdata[i].ded_apply != string.Empty)
                // {
                DateTime ded_date = Convert.ToDateTime(null);
                BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                if (objListemployeeDeddefaultdata[i].ded_date != string.Empty)
                {
                    ded_date = Convert.ToDateTime(objListemployeeDeddefaultdata[i].ded_date);
                }
                else
                {
                    ded_date = Convert.ToDateTime(null);
                }
                if (objpayrollfunctions.Pay_Frequency(objListemployeeDeddefaultdata[i].ded_apply, EOPdate, ded_date))
                {
                    objpayrollstypaydd.ded_rate = objListemployeeDeddefaultdata[i].ded_rate;
                }
                else
                {
                    objListemployeeDeddefaultdata[i].ded_rate = 0.0M;
                }
                //  }
                objpayrollstypaydd.ded_code = objListemployeeDeddefaultdata[i].ded_code;
                objpayrollstypaydd.amount = 0;
                objpayrollstypaydd.lo_ded_amt = (decimal?)objListemployeeDeddefaultdata[i].lo_ded_amt;
                objpayrollstypaydd.hi_ded_amt = (decimal?)objListemployeeDeddefaultdata[i].hi_ded_amt;
                objpayrollstypaydd.ded_taxred = objListemployeeDeddefaultdata[i].ded_taxred;
                objpayrollstypaydd.acct_no = objListemployeeDeddefaultdata[i].acct_no;
                objpayrollstypaydd.Department = objListemployeeDeddefaultdata[i].department;
                objpayrollstypaydd.line_no = objListemployeeDeddefaultdata[i].line_no;
                objpayrollstypaydd.add_code = "N";
                objpayrollstypaydd.ded_type = objListemployeeDeddefaultdata[i].ded_type;
                if (objListemployeeDeddefaultdata[i].pay_limit == null)
                {
                    objpayrollstypaydd.pay_limit = (decimal?)objListemployeeDeddefaultdata[i].dflt_pay_limit;
                }
                else
                {
                    objpayrollstypaydd.pay_limit = (decimal?)objListemployeeDeddefaultdata[i].pay_limit;
                }
                if (objListemployeeDeddefaultdata[i].yearrollover == "Y")
                {
                    if (objpayrollstypaydd.amount > (decimal?)objListemployeeDeddefaultdata[i].balanceamt)
                    {
                        objpayrollstypaydd.amount = (decimal?)objListemployeeDeddefaultdata[i].balanceamt;
                    }
                }
                if (objListemployeeDeddefaultdata[i].ded_limit == null)
                {
                    objpayrollstypaydd.ded_limit = (decimal?)objListemployeeDeddefaultdata[i].dflt_limit;
                }
                if (objpayrollstypaydd.ded_ytd == null)
                {
                    objpayrollstypaydd.ded_ytd = (decimal?)objListemployeeDeddefaultdata[i].ded_ytd;
                }
                if (objpayrollstypaydd.tax_code == string.Empty)
                {
                    objpayrollstypaydd.tax_code = objListemployeeDeddefaultdata[i].tax_code;
                }
                objListpayrollstypaydd.Add(objpayrollstypaydd);
            }

            return objListpayrollstypaydd;
        }
        DataSet dsAllDeductions = new DataSet();
        DataSet dsAllDedTaxCalc = new DataSet();
        DataSet dsAllDedTaxCalcDetail = new DataSet();
        public void LoadAllDeductions(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = SearchedEmployeeList.Trim();
                //parameters[0] = objpayautosearchdata.EmplCode;
                ////parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[1] = objpayautosearchdata.SocSecNum;
                //parameters[2] = objpayautosearchdata.FirstName;
                //parameters[3] = objpayautosearchdata.LastName;
                //parameters[4] = objpayautosearchdata.Employee_Type;
                //parameters[5] = objpayautosearchdata.Job_Code;
                //parameters[6] = "";
                //parameters[7] = "";
                //parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllDeductions = objDalBaseClass.GetData((new DVOMasterEmployeeDeductions()).FIND_EMPDEDANDDEFAULTS_QUERY(ref parameters));
                if (dsAllDeductions != null && dsAllDeductions.Tables.Count > 0)
                {
                    dsAllDeductions.Tables[0].Columns[0].ColumnName = "dfltaccounttype";
                    dsAllDeductions.Tables[0].Columns[1].ColumnName = "dfltkeyvalue";
                    dsAllDeductions.Tables[0].Columns[2].ColumnName = "ded_code";
                    dsAllDeductions.Tables[0].Columns[3].ColumnName = "line_no";
                    dsAllDeductions.Tables[0].Columns[4].ColumnName = "ded_rate";
                    dsAllDeductions.Tables[0].Columns[5].ColumnName = "ded_limit";
                    dsAllDeductions.Tables[0].Columns[6].ColumnName = "pay_limit";
                    dsAllDeductions.Tables[0].Columns[7].ColumnName = "balanceamt";
                    dsAllDeductions.Tables[0].Columns[8].ColumnName = "ded_apply";
                    dsAllDeductions.Tables[0].Columns[9].ColumnName = "lo_ded_amt";
                    dsAllDeductions.Tables[0].Columns[10].ColumnName = "hi_ded_amt";
                    dsAllDeductions.Tables[0].Columns[11].ColumnName = "acct_no";
                    dsAllDeductions.Tables[0].Columns[12].ColumnName = "department";
                    dsAllDeductions.Tables[0].Columns[13].ColumnName = "ded_ytd";
                    dsAllDeductions.Tables[0].Columns[14].ColumnName = "ded_date";
                    dsAllDeductions.Tables[0].Columns[15].ColumnName = "description";
                    dsAllDeductions.Tables[0].Columns[16].ColumnName = "ded_type";
                    dsAllDeductions.Tables[0].Columns[17].ColumnName = "ded_taxred";
                    dsAllDeductions.Tables[0].Columns[18].ColumnName = "dflt_rate";
                    dsAllDeductions.Tables[0].Columns[19].ColumnName = "dflt_limit";
                    dsAllDeductions.Tables[0].Columns[20].ColumnName = "dflt_pay_limit";
                    dsAllDeductions.Tables[0].Columns[21].ColumnName = "yearrollover";
                    dsAllDeductions.Tables[0].Columns[22].ColumnName = "dflt_lo_ded_amt";
                    dsAllDeductions.Tables[0].Columns[23].ColumnName = "dflt_hi_ded_amt";
                    dsAllDeductions.Tables[0].Columns[24].ColumnName = "dflt_acct";
                    dsAllDeductions.Tables[0].Columns[25].ColumnName = "dflt_dept";
                    dsAllDeductions.Tables[0].Columns[26].ColumnName = "dflt_apply";
                    dsAllDeductions.Tables[0].Columns[27].ColumnName = "empl_code";
                    dsAllDeductions.Tables[0].Columns[28].ColumnName = "soc_sec_num";
                }
            }
            catch (Exception ex) { }
        }
        public void LoadAllded_taxcalc(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = SearchedEmployeeList.Trim();
                parameters[1] = objpayautosearchdata.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[0] = objpayautosearchdata.EmplCode;
                ////parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[1] = objpayautosearchdata.SocSecNum;
                //parameters[2] = objpayautosearchdata.FirstName;
                //parameters[3] = objpayautosearchdata.LastName;
                //parameters[4] = objpayautosearchdata.Employee_Type;
                //parameters[5] = objpayautosearchdata.Job_Code;
                //parameters[6] = "";
                //parameters[7] = "";
                //parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[9] = objpayautosearchdata.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllDedTaxCalc = objDalBaseClass.GetData((new DVOMasterEmployeeDeductions()).FIND_EMPDED_TAXCALC_QUERY(ref parameters));
                if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                {
                    dsAllDedTaxCalc.Tables[0].Columns[0].ColumnName = "ded_code";
                    dsAllDedTaxCalc.Tables[0].Columns[1].ColumnName = "week_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[2].ColumnName = "biweek_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[3].ColumnName = "smonth_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[4].ColumnName = "month_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[5].ColumnName = "quarter_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[6].ColumnName = "year_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[7].ColumnName = "misc_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[8].ColumnName = "tax_year";
                    dsAllDedTaxCalc.Tables[0].Columns[9].ColumnName = "syear_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[10].ColumnName = "hrs_week_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[11].ColumnName = "hrs_biweek_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[12].ColumnName = "hrs_smonth_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[13].ColumnName = "hrs_month_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[14].ColumnName = "hrs_quarter_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[15].ColumnName = "hrs_syear_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[16].ColumnName = "hrs_year_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[17].ColumnName = "hrs_misc_allow";
                    dsAllDedTaxCalc.Tables[0].Columns[18].ColumnName = "allow_or_limit";
                }
            }
            catch (Exception ex) { }
        }
        public void LoadAllded_taxcalc_Detail(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[2];
                parameters[0] = SearchedEmployeeList.Trim();
                parameters[1] = objpayautosearchdata.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[0] = objpayautosearchdata.EmplCode;
                ////parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[1] = objpayautosearchdata.SocSecNum;
                //parameters[2] = objpayautosearchdata.FirstName;
                //parameters[3] = objpayautosearchdata.LastName;
                //parameters[4] = objpayautosearchdata.Employee_Type;
                //parameters[5] = objpayautosearchdata.Job_Code;
                //parameters[6] = "";
                //parameters[7] = "";
                //parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[9] = objpayautosearchdata.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllDedTaxCalcDetail = objDalBaseClass.GetData((new DVOMasterEmployeeDeductions()).FIND_EMPDED_TAXCALC_DETAIL_QUERY(ref parameters));
                if (dsAllDedTaxCalcDetail != null && dsAllDedTaxCalcDetail.Tables.Count > 0)
                {
                    dsAllDedTaxCalcDetail.Tables[0].Columns[0].ColumnName = "tax_year";
                    dsAllDedTaxCalcDetail.Tables[0].Columns[1].ColumnName = "ded_code";
                    dsAllDedTaxCalcDetail.Tables[0].Columns[2].ColumnName = "pay_period";
                    dsAllDedTaxCalcDetail.Tables[0].Columns[3].ColumnName = "marital_stat";
                    dsAllDedTaxCalcDetail.Tables[0].Columns[4].ColumnName = "over_amt";
                    dsAllDedTaxCalcDetail.Tables[0].Columns[5].ColumnName = "base_amt";
                    dsAllDedTaxCalcDetail.Tables[0].Columns[6].ColumnName = "tax_rate";
                    dsAllDedTaxCalcDetail.Tables[0].Columns[7].ColumnName = "order_no";
                }
            }
            catch (Exception ex) { }
        }
        public void LoadAllFlexKeyval(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[5];

                parameters[0] = objpayautosearchdata.EmplCode.Trim();
                parameters[1] = objpayautosearchdata.SocSecNum;
                parameters[2] = objpayautosearchdata.FirstName;
                parameters[3] = objpayautosearchdata.LastName;
                parameters[4] = objpayautosearchdata.Employee_Type;

                dsAllFlexKeyVal = objDalBaseClass.GetData((new DVOFlexSegCommon()).Find_allFlexVal(ref parameters));
                if (dsAllFlexKeyVal != null && dsAllFlexKeyVal.Tables.Count > 0)
                {
                    dsAllFlexKeyVal.Tables[0].Columns[0].ColumnName = "keyvalue";
                    dsAllFlexKeyVal.Tables[0].Columns[1].ColumnName = "position";
                    dsAllFlexKeyVal.Tables[0].Columns[2].ColumnName = "length";
                    dsAllFlexKeyVal.Tables[0].Columns[3].ColumnName = "abbreviation";
                    dsAllFlexKeyVal.Tables[0].Columns[4].ColumnName = "entity_type";
                    dsAllFlexKeyVal.Tables[0].Columns[5].ColumnName = "code";
                    dsAllFlexKeyVal.Tables[0].Columns[6].ColumnName = "accounttype";

                }
            }
            catch (Exception ex)
            {

            }

        }
        public void LoadAllAcctType()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                dsAllAcctType = objDalBaseClass.GetData((new DVOFlexSegCommon()).Find_allAcctTypelength(ref parameters));
                if (dsAllAcctType != null && dsAllAcctType.Tables.Count > 0)
                {
                    dsAllAcctType.Tables[0].Columns[0].ColumnName = "accounttype";
                    dsAllAcctType.Tables[0].Columns[1].ColumnName = "keylength";
                }
            }
            catch (Exception ex)
            {

            }

        }

        public List<DVOPayrollStypayod> LoadObligations(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            List<DVOPayrollStypayod> objListpayrollstypayod = new List<DVOPayrollStypayod>();
            List<DVOMasterEmployeeObligations> objListemployeeObldefaultdate = new List<DVOMasterEmployeeObligations>();

            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            Int32 Oblreccount;
            //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();

            if (dsAllObligations != null && dsAllObligations.Tables.Count > 0 && dsAllObligations.Tables[0].Rows.Count > 0)
            {
                DataRow[] drobligations = dsAllObligations.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "'");
                if (drobligations != null && drobligations.Length > 0)
                    foreach (DataRow dr in drobligations)
                    {
                        //MasterOblCodes.dfltaccounttype,MasterOblCodes.dfltbaccounttype,MasterOblCodes.dfltbkeyvalue,
                        //MasterEmployeeObligations.obl_code,MasterEmployeeObligations.line_no,MasterEmployeeObligations.obl_rate,MasterEmployeeObligations.obl_limit,
                        //MasterEmployeeObligations.pay_limit,MasterEmployeeObligations.acct_no,MasterEmployeeObligations.department,MasterEmployeeObligations.bal_acct_no,
                        //MasterEmployeeObligations.bal_dept,MasterEmployeeObligations.obl_ytd,MasterOblCodes.description,
                        //MasterOblCodes.obl_type,MasterOblCodes.dflt_rate,MasterOblCodes.dflt_limit,
                        //MasterOblCodes.dflt_pay_limit,  MasterOblCodes.dflt_acct, MasterOblCodes.dflt_dept,
                        //MasterOblCodes.dflt_bacct,MasterOblCodes.dflt_bdept

                        using (DVOMasterEmployeeObligations objemployeedefaultoblrec = new DVOMasterEmployeeObligations())
                        {
                            objemployeedefaultoblrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
                            objemployeedefaultoblrec.dfltbaccounttype = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
                            objemployeedefaultoblrec.dfltbkeyvalue = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
                            objemployeedefaultoblrec.obl_code = (dr[3] != DBNull.Value ? Convert.ToString(dr[3]).Trim() : string.Empty);
                            objemployeedefaultoblrec.line_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                            objemployeedefaultoblrec.obl_rate = (dr[5] != DBNull.Value ? (decimal?)dr[5] : null);
                            objemployeedefaultoblrec.obl_limit = (dr[6] != DBNull.Value ? (decimal?)dr[6] : null);
                            objemployeedefaultoblrec.pay_limit = (dr[7] != DBNull.Value ? (decimal?)dr[7] : null);
                            objemployeedefaultoblrec.acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
                            objemployeedefaultoblrec.department = (dr[9] != DBNull.Value ? Convert.ToString(dr[9]).Trim() : "000");
                            objemployeedefaultoblrec.bal_acct_no = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                            objemployeedefaultoblrec.bal_dept = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                            objemployeedefaultoblrec.obl_ytd = (dr[12] != DBNull.Value ? (decimal?)dr[12] : null);
                            objemployeedefaultoblrec.description = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]).Trim() : string.Empty);
                            objemployeedefaultoblrec.obl_type = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
                            objemployeedefaultoblrec.dflt_rate = (dr[15] != DBNull.Value ? (decimal?)dr[15] : null);
                            objemployeedefaultoblrec.dflt_limit = (dr[16] != DBNull.Value ? (decimal?)dr[16] : null);
                            objemployeedefaultoblrec.dflt_pay_limit = (dr[17] != DBNull.Value ? (decimal?)dr[17] : null);
                            objemployeedefaultoblrec.dflt_acct = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                            objemployeedefaultoblrec.dflt_dept = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                            objemployeedefaultoblrec.dflt_bacct = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);
                            objemployeedefaultoblrec.dflt_bdept = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : "000");
                            DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                            objflexsegcommon.AccountType = objemployeedefaultoblrec.dfltaccounttype;
                            objflexsegcommon.EntityType = "MasterOblCodes";
                            objflexsegcommon.Code = objemployeedefaultoblrec.obl_code;
                            objemployeedefaultoblrec.dfltbkeyvalue = PayrollFlexseg_Load(ref objflexsegcommon);
                            //objemployeedefaultoblrec.dfltbkeyvalue = BLLFlexSegCommon.Flexseg_Load_UsingTransaction(ref objTransaction, ref objflexsegcommon);

                            //BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                            PayrollflxMix(objemployeedefaultoblrec.dfltaccounttype, objemployeedefaultoblrec.dfltbkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                            objemployeedefaultoblrec.acct_no = MixaccountNo;
                            // objemployeedefaultoblrec.dflt_acct
                            if ((objemployeedefaultoblrec.dflt_acct == 0) &&
                             (objemployeedefaultoblrec.acct_no == 0))
                            {
                                //Test if the Account Exists in the table or not with th ekeyvalue we got now .
                                // scratch will contain the description after testFlexAccountKey
                                //create PayrollGLAccounts Flex account Entry
                            }
                            objListemployeeObldefaultdate.Add(objemployeedefaultoblrec);
                        }
                    }
            }


            //object[] parameters = new object[1];
            //parameters[0] = EmployeeCode;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeObligations), (new DVOMasterEmployeeObligations()).Emplobldefaultsget))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        //MasterOblCodes.dfltaccounttype,MasterOblCodes.dfltbaccounttype,MasterOblCodes.dfltbkeyvalue,
            //        //MasterEmployeeObligations.obl_code,MasterEmployeeObligations.line_no,MasterEmployeeObligations.obl_rate,MasterEmployeeObligations.obl_limit,
            //        //MasterEmployeeObligations.pay_limit,MasterEmployeeObligations.acct_no,MasterEmployeeObligations.department,MasterEmployeeObligations.bal_acct_no,
            //        //MasterEmployeeObligations.bal_dept,MasterEmployeeObligations.obl_ytd,MasterOblCodes.description,
            //        //MasterOblCodes.obl_type,MasterOblCodes.dflt_rate,MasterOblCodes.dflt_limit,
            //        //MasterOblCodes.dflt_pay_limit,  MasterOblCodes.dflt_acct, MasterOblCodes.dflt_dept,
            //        //MasterOblCodes.dflt_bacct,MasterOblCodes.dflt_bdept

            //        DVOMasterEmployeeObligations objemployeedefaultoblrec = new DVOMasterEmployeeObligations();
            //        objemployeedefaultoblrec.dfltaccounttype = (dr[0] != DBNull.Value ? Convert.ToString(dr[0]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.dfltbaccounttype = (dr[1] != DBNull.Value ? Convert.ToString(dr[1]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.dfltbkeyvalue = (dr[2] != DBNull.Value ? Convert.ToString(dr[2]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.obl_code = (dr[3] != DBNull.Value ? Convert.ToString(dr[3]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.line_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
            //        objemployeedefaultoblrec.obl_rate = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
            //        objemployeedefaultoblrec.obl_limit = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0);
            //        objemployeedefaultoblrec.pay_limit = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0);
            //        objemployeedefaultoblrec.acct_no = (dr[8] != DBNull.Value ? Convert.ToInt32(dr[8]) : 0);
            //        objemployeedefaultoblrec.department = (dr[9] != DBNull.Value ? Convert.ToString(dr[9]).Trim() : "000");
            //        objemployeedefaultoblrec.bal_acct_no = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
            //        objemployeedefaultoblrec.bal_dept = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
            //        objemployeedefaultoblrec.obl_ytd = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0);
            //        objemployeedefaultoblrec.description = (dr[13] != DBNull.Value ? Convert.ToString(dr[13]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.obl_type = (dr[14] != DBNull.Value ? Convert.ToString(dr[14]).Trim() : string.Empty);
            //        objemployeedefaultoblrec.dflt_rate = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
            //        objemployeedefaultoblrec.dflt_limit = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0);
            //        objemployeedefaultoblrec.dflt_pay_limit = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0);
            //        objemployeedefaultoblrec.dflt_acct = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
            //        objemployeedefaultoblrec.dflt_dept = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
            //        objemployeedefaultoblrec.dflt_bacct = (dr[20] != DBNull.Value ? Convert.ToInt32(dr[20]) : 0);
            //        objemployeedefaultoblrec.dflt_bdept = (dr[21] != DBNull.Value ? Convert.ToString(dr[21]).Trim() : "000");
            //        DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
            //        objflexsegcommon.AccountType = objemployeedefaultoblrec.dfltaccounttype;
            //        objflexsegcommon.EntityType = "MasterOblCodes";
            //        objflexsegcommon.Code = objemployeedefaultoblrec.obl_code;
            //        objemployeedefaultoblrec.dfltbkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

            //        BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
            //        objpayrollfunctions.flxMix(objemployeedefaultoblrec.dfltaccounttype, objemployeedefaultoblrec.dfltbkeyvalue, FlexacctType, FlexDepartment, out  MixaccountNo, out MixAcctType, out Mixkeyvalue);
            //        objemployeedefaultoblrec.acct_no = MixaccountNo;
            //        // objemployeedefaultoblrec.dflt_acct
            //        if ((objemployeedefaultoblrec.dflt_acct == 0) &&
            //         (objemployeedefaultoblrec.acct_no == 0))
            //        {
            //            //Test if the Account Exists in the table or not with th ekeyvalue we got now .
            //            // scratch will contain the description after testFlexAccountKey
            //            //create PayrollGLAccounts Flex account Entry
            //        }
            //        objListemployeeObldefaultdate.Add(objemployeedefaultoblrec);
            //    }
            //}

            Oblreccount = objListemployeeObldefaultdate.Count;
            for (Int32 i = 0; i < Oblreccount; i++)
            {
                DVOPayrollStypayod objpayrollstypayod = new DVOPayrollStypayod();
                objpayrollstypayod.obl_code = objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].obl_rate;
                objpayrollstypayod.amount = 0;//objListemployeeObldefaultdate[i].obl_code;
                objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].acct_no;
                objpayrollstypayod.Department = objListemployeeObldefaultdate[i].department;
                objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].bal_acct_no;
                objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].bal_dept;
                objpayrollstypayod.line_no = objListemployeeObldefaultdate[i].line_no;
                objpayrollstypayod.add_code = "N";
                objpayrollstypayod.obl_type = objListemployeeObldefaultdate[i].obl_type;
                // assign defaults as required

                if (objpayrollstypayod.obl_rate == null)
                {
                    objpayrollstypayod.obl_rate = objListemployeeObldefaultdate[i].dflt_rate;
                }
                if (objpayrollstypayod.acct_no == 0)
                {
                    objpayrollstypayod.acct_no = objListemployeeObldefaultdate[i].dflt_acct;
                }
                if ((objpayrollstypayod.Department == "000") || (objpayrollstypayod.Department == null))
                {
                    objpayrollstypayod.Department = objListemployeeObldefaultdate[i].dflt_dept;
                }
                if (objpayrollstypayod.bal_acct_no == 0)
                {
                    objpayrollstypayod.bal_acct_no = objListemployeeObldefaultdate[i].dflt_bacct;
                }
                if ((objpayrollstypayod.bal_dept == "000") || (objpayrollstypayod.bal_dept == null))
                {
                    objpayrollstypayod.bal_dept = objListemployeeObldefaultdate[i].dflt_bdept;
                }
                if (objListemployeeObldefaultdate[i].pay_limit == null)
                {
                    objpayrollstypayod.pay_limit = (decimal?)objListemployeeObldefaultdate[i].dflt_pay_limit;
                }
                else
                {
                    objpayrollstypayod.pay_limit = (decimal?)objListemployeeObldefaultdate[i].pay_limit;
                }
                if (objListemployeeObldefaultdate[i].obl_limit == null)
                {
                    objpayrollstypayod.obl_limit = (decimal?)objListemployeeObldefaultdate[i].dflt_limit;
                }
                else
                {
                    objpayrollstypayod.obl_limit = (decimal?)objListemployeeObldefaultdate[i].obl_limit;
                }
                objListpayrollstypayod.Add(objpayrollstypayod);
            }
            return objListpayrollstypayod;
        }
        DataSet dsAllObligations = new DataSet();
        public void LoadAllObligations(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = SearchedEmployeeList.Trim();
                //parameters[0] = objpayautosearchdata.EmplCode;
                ////parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[1] = objpayautosearchdata.SocSecNum;
                //parameters[2] = objpayautosearchdata.FirstName;
                //parameters[3] = objpayautosearchdata.LastName;
                //parameters[4] = objpayautosearchdata.Employee_Type;
                //parameters[5] = objpayautosearchdata.Job_Code;
                //parameters[6] = "";
                //parameters[7] = "";
                //parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllObligations = objDalBaseClass.GetData((new DVOMasterEmployeeObligations()).FIND_EMPLOBLDEFAULTSGET_QUERY(ref parameters));
                if (dsAllObligations != null && dsAllObligations.Tables.Count > 0)
                {
                    dsAllObligations.Tables[0].Columns[0].ColumnName = "dfltaccounttype";
                    dsAllObligations.Tables[0].Columns[1].ColumnName = "dfltbaccounttype";
                    dsAllObligations.Tables[0].Columns[2].ColumnName = "dfltbkeyvalue";
                    dsAllObligations.Tables[0].Columns[3].ColumnName = "obl_code";
                    dsAllObligations.Tables[0].Columns[4].ColumnName = "line_no";
                    dsAllObligations.Tables[0].Columns[5].ColumnName = "obl_rate";
                    dsAllObligations.Tables[0].Columns[6].ColumnName = "obl_limit";
                    dsAllObligations.Tables[0].Columns[7].ColumnName = "pay_limit";
                    dsAllObligations.Tables[0].Columns[8].ColumnName = "acct_no";
                    dsAllObligations.Tables[0].Columns[9].ColumnName = "department";
                    dsAllObligations.Tables[0].Columns[10].ColumnName = "bal_acct_no";
                    dsAllObligations.Tables[0].Columns[11].ColumnName = "bal_dept";
                    dsAllObligations.Tables[0].Columns[12].ColumnName = "obl_ytd";
                    dsAllObligations.Tables[0].Columns[13].ColumnName = "description";
                    dsAllObligations.Tables[0].Columns[14].ColumnName = "obl_type";
                    dsAllObligations.Tables[0].Columns[15].ColumnName = "dflt_rate";
                    dsAllObligations.Tables[0].Columns[16].ColumnName = "dflt_limit";
                    dsAllObligations.Tables[0].Columns[17].ColumnName = "dflt_pay_limit";
                    dsAllObligations.Tables[0].Columns[18].ColumnName = "dflt_acct";
                    dsAllObligations.Tables[0].Columns[19].ColumnName = "dflt_dept";
                    dsAllObligations.Tables[0].Columns[20].ColumnName = "dflt_bacct";
                    dsAllObligations.Tables[0].Columns[21].ColumnName = "dflt_bdept";
                    dsAllObligations.Tables[0].Columns[22].ColumnName = "empl_code";
                    dsAllObligations.Tables[0].Columns[23].ColumnName = "soc_sec_num";
                }
            }
            catch (Exception ex) { }
        }

        public List<DVOUpdateTimeCard> LoadTimeCardIncomes(string EmployeeCode, DateTime EopDate, string FlexacctType, string FlexDepartment)
        {
            //DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
            //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixaccountNo;
            int dupempinccount;
            int lastcard = 0;
            int Timecarddetail = 0;
            bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();
            //object[] parameters = new object[0];

            if (dsAllTimeCardIncomes != null && dsAllTimeCardIncomes.Tables.Count > 0 && dsAllTimeCardIncomes.Tables[0].Rows.Count > 0)
            {
                DataRow[] drtimecardincomes = dsAllTimeCardIncomes.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "' and start_date <= '" + EopDate + "'");//EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture) + 
                if (drtimecardincomes != null && drtimecardincomes.Length > 0)
                    foreach (DataRow dr in drtimecardincomes)
                    {
                        using (DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard())
                        {
                            objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                            objUpdatetimecardincome.dfltkeyvalue = "";
                            objUpdatetimecardincome.acct_no_id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                            objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                            objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                            objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                            objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? (decimal?)(dr[5]) : null);
                            objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? (decimal?)(dr[6]) : null);
                            objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? (decimal?)(dr[7]) : null);
                            objUpdatetimecardincome.description_cr = dr[8].ToString().Trim();
                            objUpdatetimecardincome.dflt_num_cr = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                            objUpdatetimecardincome.dflt_rate_cr = (dr[10] != DBNull.Value ? (decimal?)(dr[10]) : null);
                            objUpdatetimecardincome.dflt_hours_cr = (dr[11] != DBNull.Value ? (decimal?)(dr[11]) : null);
                            objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[12] != DBNull.Value ? (decimal?)(dr[12]) : null);
                            objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[13] != DBNull.Value ? (decimal?)(dr[13]) : null);
                            objUpdatetimecardincome.dflt_acct_cr = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
                            objUpdatetimecardincome.dflt_dept_cr = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : "000");
                            objUpdatetimecardincome.inc_type_cr = dr[16].ToString().Trim();

                            DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                            objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                            objflexsegcommon.EntityType = "MasterIncCodes";
                            objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                            //objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load_UsingTransaction(ref objTransaction, ref objflexsegcommon);
                            objUpdatetimecardincome.dfltkeyvalue = PayrollFlexseg_Load(ref objflexsegcommon);
                            BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
                            PayrollflxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
                            objUpdatetimecardincome.dflt_acct_cr = MixaccountNo;

                            if (dsAllEmployeeIncomes != null && dsAllEmployeeIncomes.Tables.Count > 0 && dsAllEmployeeIncomes.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] dremployeeincomes = dsAllEmployeeIncomes.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "' and inc_code='" + objUpdatetimecardincome.inc_code_id.Trim().Replace("'", "''") + "' and line_no=" + objUpdatetimecardincome.line_no_id.ToString());
                                if (dremployeeincomes != null && dremployeeincomes.Length > 0)
                                {
                                    foreach (DataRow drt in dremployeeincomes)
                                    {
                                        objUpdatetimecardincome.lo_inc_amt_id = (drt["lo_inc_amt"] != DBNull.Value ? (decimal?)(drt["lo_inc_amt"]) : null);
                                        objUpdatetimecardincome.hi_inc_amt_id = (drt["hi_inc_amt"] != DBNull.Value ? (decimal?)(drt["hi_inc_amt"]) : null);
                                        objUpdatetimecardincome.acct_no_id = (drt["acct_no"] != DBNull.Value ? Convert.ToInt32(drt["acct_no"]) : 0);
                                        objUpdatetimecardincome.department_id = (drt["department"] != DBNull.Value ? drt["department"].ToString().Trim() : "000"); ;
                                    }

                                    if (((objUpdatetimecardincome.timecd_acct_no == 0) || (objUpdatetimecardincome.timecd_acct_no == null)) &&
                                    ((objUpdatetimecardincome.acct_no_id == 0) || (objUpdatetimecardincome.acct_no_id == null)) &&
                                   ((objUpdatetimecardincome.dflt_acct_cr == 0) || (objUpdatetimecardincome.dflt_acct_cr == null)))
                                    {
                                        //test and create flex key records 
                                    }
                                    objUpdatetimecardincome.add_code_cr = "N";
                                }
                            }
                            else
                            {
                                // does the code already exist at the employee level?
                                DataRow[] dremployeeincomes = dsAllEmployeeIncomes.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "' and inc_code='" + objUpdatetimecardincome.inc_code_id.Trim().Replace("'", "''") + "'");
                                if (dremployeeincomes != null && dremployeeincomes.Length > 0)
                                    objUpdatetimecardincome.add_code_cr = "Z";
                                else
                                    objUpdatetimecardincome.add_code_cr = "Y";
                            }

                            //parameters = new object[3];
                            //parameters[0] = EmployeeCode;
                            //parameters[1] = objUpdatetimecardincome.inc_code_id;
                            //parameters[2] = objUpdatetimecardincome.line_no_id;
                            //DataSet dst = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmployeeIncomes()).EMPLOYEEINCOME_AUTOPAY);
                            //if (dst.Tables[0].Rows.Count > 0)
                            //{
                            //    foreach (DataRow drt in dst.Tables[0].Rows)
                            //    {
                            //        objUpdatetimecardincome.lo_inc_amt_id = (drt[0] != DBNull.Value ? (decimal?)(drt[0]) : null);
                            //        objUpdatetimecardincome.hi_inc_amt_id = (drt[1] != DBNull.Value ? (decimal?)(drt[1]) : null);
                            //        objUpdatetimecardincome.acct_no_id = (drt[2] != DBNull.Value ? Convert.ToInt32(drt[2]) : 0);
                            //        objUpdatetimecardincome.department_id = (drt[3] != DBNull.Value ? Convert.ToString(drt[3]).Trim() : "000"); ;
                            //    }

                            //    if (((objUpdatetimecardincome.timecd_acct_no == 0) || (objUpdatetimecardincome.timecd_acct_no == null)) &&
                            //    ((objUpdatetimecardincome.acct_no_id == 0) || (objUpdatetimecardincome.acct_no_id == null)) &&
                            //   ((objUpdatetimecardincome.dflt_acct_cr == 0) || (objUpdatetimecardincome.dflt_acct_cr == null)))
                            //    {
                            //        //test and create flex key records 

                            //    }
                            //    objUpdatetimecardincome.add_code_cr = "N";
                            //}
                            //else
                            //{
                            //    // does the code already exist at the employee level?

                            //    parameters = new object[2];
                            //    parameters[0] = objUpdatetimecardincome.inc_code_id;
                            //    parameters[1] = EmployeeCode;
                            //    dupempinccount = Convert.ToInt32(objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, (new DVOMasterEmployeeIncomes()).EMPLINCOMECNT_AUTOPAY));
                            //    if (dupempinccount >= 1)
                            //    {
                            //        objUpdatetimecardincome.add_code_cr = "Z";
                            //    }
                            //    else
                            //    {
                            //        objUpdatetimecardincome.add_code_cr = "Y";
                            //    }
                            //}
                            if (lastcard == objUpdatetimecardincome.card_no)
                            {

                            }
                            else
                            {
                                lastcard = objUpdatetimecardincome.card_no;
                                newRec = true;
                                //   objListtimecard.Add(objUpdatetimecardincome);
                            }

                            Timecarddetail = objListtimecard.Count;
                            for (int i = 0; i < Timecarddetail; i++)
                            {
                                if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                                    ((objUpdatetimecardincome.inc_rate_id ?? 0) == (objListtimecard[i].inc_rate_id ?? 0)))
                                {
                                    objListtimecard[i].inc_hours_id = (objListtimecard[i].inc_hours_id ?? 0) + (objUpdatetimecardincome.inc_hours_id ?? 0);
                                    objListtimecard[i].inc_number_id = (objListtimecard[i].inc_number_id ?? 0) + (objUpdatetimecardincome.inc_number_id ?? 0);
                                    newRec = false;
                                    break;
                                }
                                else
                                {
                                    // If the code is the same as the employee entry
                                    // but the rate differs we do not want to append
                                    if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
                                      (objUpdatetimecardincome.line_no_id == objListtimecard[i].line_no_id))
                                    {
                                        if ((objUpdatetimecardincome.inc_number_id == 0) && (objListtimecard[i].inc_number_id != 0))
                                        {
                                            objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                            objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                            newRec = false;
                                            break;
                                        }
                                        else if (((objListtimecard[i].inc_number_id ?? 0) == 0) && ((objUpdatetimecardincome.inc_number_id ?? 0) != 0))
                                        {
                                            objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                            objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                            objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                            newRec = false;
                                            break;
                                        }
                                        else if (((objListtimecard[i].inc_number_id ?? 0) == 0) && ((objUpdatetimecardincome.inc_number_id ?? 0) == 0))
                                        {
                                            objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
                                            objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
                                            objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
                                            newRec = false;
                                            break;
                                        }
                                        else
                                        {
                                            //This code is a duplicate but the rate differs
                                            // and the number is not zero.
                                            objUpdatetimecardincome.add_code_cr = "Z";
                                            // newRec = false;
                                        }

                                    }
                                }
                            }
                            if (newRec == true)
                            {
                                objListtimecard.Add(objUpdatetimecardincome);
                                //objUpdatetimecardincome = new DVOUpdateTimeCard();
                            }
                        }
                    }
            }


            //object[] parameters = new object[2];
            //parameters[0] = EmployeeCode;
            //parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOUpdateTimeCard), (new DVOUpdateTimeCard()).FIND_DETAILS_FORAUTOPAY))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
            //        objUpdatetimecardincome.dfltkeyvalue = "";
            //        objUpdatetimecardincome.acct_no_id = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
            //        objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
            //        objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
            //        objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
            //        objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
            //        objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
            //        objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
            //        objUpdatetimecardincome.description_cr = dr[8].ToString().Trim();
            //        objUpdatetimecardincome.dflt_num_cr = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
            //        objUpdatetimecardincome.dflt_rate_cr = (dr[10] != DBNull.Value ? Convert.ToDecimal(dr[10]) : 0.0M);
            //        objUpdatetimecardincome.dflt_hours_cr = (dr[11] != DBNull.Value ? Convert.ToDecimal(dr[11]) : 0.0M);
            //        objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[12] != DBNull.Value ? Convert.ToDecimal(dr[12]) : 0.0M);
            //        objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
            //        objUpdatetimecardincome.dflt_acct_cr = (dr[14] != DBNull.Value ? Convert.ToInt32(dr[14]) : 0);
            //        objUpdatetimecardincome.dflt_dept_cr = (dr[15] != DBNull.Value ? Convert.ToString(dr[15]).Trim() : "000");
            //        objUpdatetimecardincome.inc_type_cr = dr[16].ToString().Trim();

            //        DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
            //        objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
            //        objflexsegcommon.EntityType = "MasterIncCodes";
            //        objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
            //        objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

            //        BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
            //        objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixaccountNo, out MixAcctType, out Mixkeyvalue);
            //        objUpdatetimecardincome.dflt_acct_cr = MixaccountNo;
            //        parameters = new object[3];
            //        parameters[0] = EmployeeCode;
            //        parameters[1] = objUpdatetimecardincome.inc_code_id;
            //        parameters[2] = objUpdatetimecardincome.line_no_id;
            //        DataSet dst = objDalBaseClass.GetData(ref parameters, typeof(DVOMasterEmployeeIncomes), (new DVOMasterEmployeeIncomes()).EMPLOYEEINCOME_AUTOPAY);
            //        if (dst.Tables[0].Rows.Count > 0)
            //        {
            //            foreach (DataRow drt in dst.Tables[0].Rows)
            //            {
            //                objUpdatetimecardincome.lo_inc_amt_id = (drt[0] != DBNull.Value ? Convert.ToDecimal(drt[0]) : 0.0M); ;
            //                objUpdatetimecardincome.hi_inc_amt_id = (drt[1] != DBNull.Value ? Convert.ToDecimal(drt[1]) : 0.0M); ;
            //                objUpdatetimecardincome.acct_no_id = (drt[2] != DBNull.Value ? Convert.ToInt32(drt[2]) : 0);
            //                objUpdatetimecardincome.department_id = (drt[3] != DBNull.Value ? Convert.ToString(drt[3]).Trim() : "000"); ;
            //            }

            //            if (((objUpdatetimecardincome.timecd_acct_no == 0) || (objUpdatetimecardincome.timecd_acct_no == null)) &&
            //            ((objUpdatetimecardincome.acct_no_id == 0) || (objUpdatetimecardincome.acct_no_id == null)) &&
            //           ((objUpdatetimecardincome.dflt_acct_cr == 0) || (objUpdatetimecardincome.dflt_acct_cr == null)))
            //            {
            //                //test and create flex key records 

            //            }
            //            objUpdatetimecardincome.add_code_cr = "N";
            //        }
            //        else
            //        {
            //            // does the code already exist at the employee level?

            //            parameters = new object[2];
            //            parameters[0] = objUpdatetimecardincome.inc_code_id;
            //            parameters[1] = EmployeeCode;
            //            dupempinccount = Convert.ToInt32(objDalBaseClass.ExecuteScalar(ref parameters, (new DVOMasterEmployeeIncomes()).EMPLINCOMECNT_AUTOPAY));
            //            if (dupempinccount >= 1)
            //            {
            //                objUpdatetimecardincome.add_code_cr = "Z";
            //            }
            //            else
            //            {
            //                objUpdatetimecardincome.add_code_cr = "Y";
            //            }
            //        }
            //        if (lastcard == objUpdatetimecardincome.card_no)
            //        {

            //        }
            //        else
            //        {
            //            lastcard = objUpdatetimecardincome.card_no;
            //            newRec = true;
            //            //   objListtimecard.Add(objUpdatetimecardincome);
            //        }

            //        Timecarddetail = objListtimecard.Count;
            //        for (int i = 0; i < Timecarddetail; i++)
            //        {
            //            if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
            //                (objUpdatetimecardincome.inc_rate_id == objListtimecard[i].inc_rate_id))
            //            {
            //                objListtimecard[i].inc_hours_id = objListtimecard[i].inc_hours_id + objUpdatetimecardincome.inc_hours_id;
            //                objListtimecard[i].inc_number_id = objListtimecard[i].inc_number_id + objUpdatetimecardincome.inc_number_id;
            //                newRec = false;
            //                break;
            //            }
            //            else
            //            {
            //                // If the code is the same as the employee entry
            //                // but the rate differs we do not want to append
            //                if ((objUpdatetimecardincome.inc_code_id == objListtimecard[i].inc_code_id) &&
            //                  (objUpdatetimecardincome.line_no_id == objListtimecard[i].line_no_id))
            //                {
            //                    if ((objUpdatetimecardincome.inc_number_id == 0) && (objListtimecard[i].inc_number_id != 0))
            //                    {
            //                        objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
            //                        objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
            //                        newRec = false;
            //                        break;
            //                    }
            //                    else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id != 0))
            //                    {
            //                        objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
            //                        objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
            //                        objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
            //                        newRec = false;
            //                        break;
            //                    }
            //                    else if ((objListtimecard[i].inc_number_id == 0) && (objUpdatetimecardincome.inc_number_id == 0))
            //                    {
            //                        objListtimecard[i].inc_rate_id = objUpdatetimecardincome.inc_rate_id;
            //                        objListtimecard[i].inc_number_id = objUpdatetimecardincome.inc_number_id;
            //                        objListtimecard[i].inc_hours_id = objUpdatetimecardincome.inc_hours_id;
            //                        newRec = false;
            //                        break;
            //                    }
            //                    else
            //                    {
            //                        //This code is a duplicate but the rate differs
            //                        // and the number is not zero.
            //                        objUpdatetimecardincome.add_code_cr = "Z";
            //                        // newRec = false;
            //                    }

            //                }
            //            }
            //        }
            //        if (newRec == true)
            //        {
            //            objListtimecard.Add(objUpdatetimecardincome);
            //            objUpdatetimecardincome = new DVOUpdateTimeCard();
            //        }

            //    }
            //}


            // If duplicate codes were added on the fly to the timecard, we
            // only want to append one to the employee entry at posting time.
            Timecarddetail = objListtimecard.Count;
            for (int j = 0; j < Timecarddetail; j++)
            {
                if (objListtimecard[j].add_code_cr == "Y")
                {
                    for (int k = 0; k < Timecarddetail; k++)
                    {
                        if (j == k)
                        {
                        }
                        else
                        {
                            if (objListtimecard[j].inc_code_id == objListtimecard[k].inc_code_id)
                            {
                                objListtimecard[j].add_code_cr = "Z";
                            }
                        }
                    }
                }
            }
            return objListtimecard;
        }
        DataSet dsAllTimeCardIncomes = new DataSet();
        public void LoadAllTimeCardIncomes(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[9];
                parameters[0] = objpayautosearchdata.EmplCode;
                //parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[1] = objpayautosearchdata.SocSecNum;
                parameters[2] = objpayautosearchdata.FirstName;
                parameters[3] = objpayautosearchdata.LastName;
                parameters[4] = objpayautosearchdata.Employee_Type;
                parameters[5] = objpayautosearchdata.Job_Code;
                parameters[6] = "";
                parameters[7] = "";
                parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllTimeCardIncomes = objDalBaseClass.GetData((new DVOUpdateTimeCard()).FIND_DETAILS_FORAUTOPAY_QUERY(ref parameters));
                if (dsAllTimeCardIncomes != null && dsAllTimeCardIncomes.Tables.Count > 0)
                {
                    dsAllTimeCardIncomes.Tables[0].Columns[0].ColumnName = "dfltaccounttype";
                    dsAllTimeCardIncomes.Tables[0].Columns[1].ColumnName = "acct_no";
                    dsAllTimeCardIncomes.Tables[0].Columns[2].ColumnName = "inc_code";
                    dsAllTimeCardIncomes.Tables[0].Columns[3].ColumnName = "line_no";
                    dsAllTimeCardIncomes.Tables[0].Columns[4].ColumnName = "card_no";
                    dsAllTimeCardIncomes.Tables[0].Columns[5].ColumnName = "inc_rate";
                    dsAllTimeCardIncomes.Tables[0].Columns[6].ColumnName = "inc_number";
                    dsAllTimeCardIncomes.Tables[0].Columns[7].ColumnName = "inc_hours";
                    dsAllTimeCardIncomes.Tables[0].Columns[8].ColumnName = "description";
                    dsAllTimeCardIncomes.Tables[0].Columns[9].ColumnName = "dflt_num";
                    dsAllTimeCardIncomes.Tables[0].Columns[10].ColumnName = "dflt_rate";
                    dsAllTimeCardIncomes.Tables[0].Columns[11].ColumnName = "dflt_hours";
                    dsAllTimeCardIncomes.Tables[0].Columns[12].ColumnName = "dflt_lo_inc_amt";
                    dsAllTimeCardIncomes.Tables[0].Columns[13].ColumnName = "dflt_hi_inc_amt";
                    dsAllTimeCardIncomes.Tables[0].Columns[14].ColumnName = "dflt_acct";
                    dsAllTimeCardIncomes.Tables[0].Columns[15].ColumnName = "dflt_dept";
                    dsAllTimeCardIncomes.Tables[0].Columns[16].ColumnName = "inc_type";
                    dsAllTimeCardIncomes.Tables[0].Columns[17].ColumnName = "start_date";
                    dsAllTimeCardIncomes.Tables[0].Columns[18].ColumnName = "empl_code";
                }
            }
            catch (Exception ex) { }
        }

        public List<DVOUpdateTimeCard> LoadEmployeeIncomes(string EmployeeCode, DateTime Eopdate, string FlexacctType, string FlexDepartment)
        {
            //This function loads the default income data from the employee
            //reference tables into an income reference array and reset the array
            // count.
            //DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            string Mixkeyvalue;
            string MixAcctType;
            Int32 MixAcctNo;
            //int dupempinccount;
            //int lastcard = 0;
            //int Timecarddetail = 0;
            //bool newRec = false;
            List<DVOUpdateTimeCard> objListtimecard = new List<DVOUpdateTimeCard>();

            if (dsAllEmployeeIncomes != null && dsAllEmployeeIncomes.Tables.Count > 0 && dsAllEmployeeIncomes.Tables[0].Rows.Count > 0)
            {
                DataRow[] dremployeeincomes = dsAllEmployeeIncomes.Tables[0].Select(" empl_code = '" + EmployeeCode.Trim().Replace("'", "''") + "'");
                if (dremployeeincomes != null && dremployeeincomes.Length > 0)
                    foreach (DataRow dr in dremployeeincomes)
                    {
                        using (DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard())
                        {
                            objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
                            objUpdatetimecardincome.dfltkeyvalue = "";
                            objUpdatetimecardincome.timecd_acct_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
                            objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
                            objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
                            objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
                            objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? (decimal?)(dr[5]) : null);
                            objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? (decimal?)(dr[6]) : null);
                            objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? (decimal?)(dr[7]) : null);
                            objUpdatetimecardincome.lo_inc_amt_id = (dr[8] != DBNull.Value ? (decimal?)(dr[8]) : null);
                            objUpdatetimecardincome.hi_inc_amt_id = (dr[9] != DBNull.Value ? (decimal?)(dr[9]) : null);
                            objUpdatetimecardincome.acct_no_id = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
                            objUpdatetimecardincome.department_id = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
                            objUpdatetimecardincome.description_cr = dr[12].ToString().Trim();
                            objUpdatetimecardincome.dflt_num_cr = (dr[13] != DBNull.Value ? (decimal?)(dr[13]) : null);
                            objUpdatetimecardincome.dflt_rate_cr = (dr[14] != DBNull.Value ? (decimal?)(dr[14]) : null);
                            objUpdatetimecardincome.dflt_hours_cr = (dr[15] != DBNull.Value ? (decimal?)(dr[15]) : null);
                            objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[16] != DBNull.Value ? (decimal?)(dr[16]) : null);
                            objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[17] != DBNull.Value ? (decimal?)(dr[17]) : null);
                            objUpdatetimecardincome.dflt_acct_cr = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
                            objUpdatetimecardincome.dflt_dept_cr = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
                            objUpdatetimecardincome.inc_type_cr = dr[20].ToString().Trim();
                            DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
                            objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
                            objflexsegcommon.EntityType = "styinccr";
                            objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
                            // objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load_UsingTransaction(ref objTransaction, ref objflexsegcommon);
                            objUpdatetimecardincome.dfltkeyvalue = PayrollFlexseg_Load(ref objflexsegcommon);
                            BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();

                            PayrollflxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out MixAcctNo, out MixAcctType, out Mixkeyvalue);
                            objUpdatetimecardincome.dflt_acct_cr = MixAcctNo;
                            if (objUpdatetimecardincome.timecd_acct_no == 0 && objUpdatetimecardincome.acct_no_id == 0 && objUpdatetimecardincome.dflt_acct_cr == 0)
                            {
                                //test and create flex key records 
                            }
                            objListtimecard.Add(objUpdatetimecardincome);
                        }
                    }
            }




            //object[] parameters = new object[1];
            //parameters[0] = EmployeeCode;
            //using (DataSet ds = objDalBaseClass.GetData(ref parameters, typeof(DVOPayrollautopay), (new DVOPayrollautopay()).EmployeeIncomerefer))
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DVOUpdateTimeCard objUpdatetimecardincome = new DVOUpdateTimeCard();
            //        objUpdatetimecardincome.dftAccountType = dr[0].ToString().Trim();
            //        objUpdatetimecardincome.dfltkeyvalue = "";
            //        objUpdatetimecardincome.timecd_acct_no = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1]) : 0);
            //        objUpdatetimecardincome.inc_code_id = dr[2].ToString().Trim();
            //        objUpdatetimecardincome.line_no_id = (dr[3] != DBNull.Value ? Convert.ToInt32(dr[3]) : 0);
            //        objUpdatetimecardincome.card_no = (dr[4] != DBNull.Value ? Convert.ToInt32(dr[4]) : 0);
            //        objUpdatetimecardincome.inc_rate_id = (dr[5] != DBNull.Value ? Convert.ToDecimal(dr[5]) : 0.0M);
            //        objUpdatetimecardincome.inc_number_id = (dr[6] != DBNull.Value ? Convert.ToDecimal(dr[6]) : 0.0M);
            //        objUpdatetimecardincome.inc_hours_id = (dr[7] != DBNull.Value ? Convert.ToDecimal(dr[7]) : 0.0M);
            //        objUpdatetimecardincome.lo_inc_amt_id = (dr[8] != DBNull.Value ? Convert.ToDecimal(dr[8]) : 0.0M);
            //        objUpdatetimecardincome.hi_inc_amt_id = (dr[9] != DBNull.Value ? Convert.ToDecimal(dr[9]) : 0.0M);
            //        objUpdatetimecardincome.acct_no_id = (dr[10] != DBNull.Value ? Convert.ToInt32(dr[10]) : 0);
            //        objUpdatetimecardincome.department_id = (dr[11] != DBNull.Value ? Convert.ToString(dr[11]).Trim() : "000");
            //        objUpdatetimecardincome.description_cr = dr[12].ToString().Trim();
            //        objUpdatetimecardincome.dflt_num_cr = (dr[13] != DBNull.Value ? Convert.ToDecimal(dr[13]) : 0.0M);
            //        objUpdatetimecardincome.dflt_rate_cr = (dr[14] != DBNull.Value ? Convert.ToDecimal(dr[14]) : 0.0M);
            //        objUpdatetimecardincome.dflt_hours_cr = (dr[15] != DBNull.Value ? Convert.ToDecimal(dr[15]) : 0.0M);
            //        objUpdatetimecardincome.dflt_lo_inc_amt_cr = (dr[16] != DBNull.Value ? Convert.ToDecimal(dr[16]) : 0.0M);
            //        objUpdatetimecardincome.dflt_hi_inc_amt_cr = (dr[17] != DBNull.Value ? Convert.ToDecimal(dr[17]) : 0.0M);
            //        objUpdatetimecardincome.dflt_acct_cr = (dr[18] != DBNull.Value ? Convert.ToInt32(dr[18]) : 0);
            //        objUpdatetimecardincome.dflt_dept_cr = (dr[19] != DBNull.Value ? Convert.ToString(dr[19]).Trim() : "000");
            //        objUpdatetimecardincome.inc_type_cr = dr[20].ToString().Trim();
            //        DVOFlexSegCommon objflexsegcommon = new DVOFlexSegCommon();
            //        objflexsegcommon.AccountType = objUpdatetimecardincome.dftAccountType;
            //        objflexsegcommon.EntityType = "MasterIncCodes";
            //        objflexsegcommon.Code = objUpdatetimecardincome.inc_code_id;
            //        objUpdatetimecardincome.dfltkeyvalue = BLLFlexSegCommon.Flexseg_Load(ref objflexsegcommon);

            //        BLLPayrollFunctions objpayrollfunctions = new BLLPayrollFunctions();
            //        objpayrollfunctions.flxMix(objUpdatetimecardincome.dftAccountType, objUpdatetimecardincome.dfltkeyvalue, FlexacctType, FlexDepartment, out  MixAcctNo, out MixAcctType, out Mixkeyvalue);
            //        objUpdatetimecardincome.dflt_acct_cr = MixAcctNo;
            //        if (objUpdatetimecardincome.timecd_acct_no == 0 && objUpdatetimecardincome.acct_no_id == 0 && objUpdatetimecardincome.dflt_acct_cr == 0)
            //        {
            //            //test and create flex key records 

            //        }
            //        objListtimecard.Add(objUpdatetimecardincome);
            //    }
            //}
            return objListtimecard;

        }
        DataSet dsAllEmployeeIncomes = new DataSet();
        public void LoadAllEmployeeIncomes(DVOPayrollautopay objpayautosearchdata)
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = SearchedEmployeeList.Trim();
                //parameters[0] = objpayautosearchdata.EmplCode;
                ////parameters[1] = EopDate.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //parameters[1] = objpayautosearchdata.SocSecNum;
                //parameters[2] = objpayautosearchdata.FirstName;
                //parameters[3] = objpayautosearchdata.LastName;
                //parameters[4] = objpayautosearchdata.Employee_Type;
                //parameters[5] = objpayautosearchdata.Job_Code;
                //parameters[6] = "";
                //parameters[7] = "";
                //parameters[8] = objpayautosearchdata.EOP_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);

                dsAllEmployeeIncomes = objDalBaseClass.GetData((new DVOPayrollautopay()).FIND_EMPLOYEEINCOMEREFER_QUERY(ref parameters));
                if (dsAllEmployeeIncomes != null && dsAllEmployeeIncomes.Tables.Count > 0)
                {
                    dsAllEmployeeIncomes.Tables[0].Columns[0].ColumnName = "dfltaccounttype";
                    //dsAllEmployeeIncomes.Tables[0].Columns[1].ColumnName = "";
                    dsAllEmployeeIncomes.Tables[0].Columns[2].ColumnName = "inc_code";
                    dsAllEmployeeIncomes.Tables[0].Columns[3].ColumnName = "line_no";
                    dsAllEmployeeIncomes.Tables[0].Columns[4].ColumnName = "line_no1";
                    dsAllEmployeeIncomes.Tables[0].Columns[5].ColumnName = "inc_rate";
                    dsAllEmployeeIncomes.Tables[0].Columns[6].ColumnName = "inc_number";
                    dsAllEmployeeIncomes.Tables[0].Columns[7].ColumnName = "inc_hours";
                    dsAllEmployeeIncomes.Tables[0].Columns[8].ColumnName = "lo_inc_amt";
                    dsAllEmployeeIncomes.Tables[0].Columns[9].ColumnName = "hi_inc_amt";
                    dsAllEmployeeIncomes.Tables[0].Columns[10].ColumnName = "acct_no";
                    dsAllEmployeeIncomes.Tables[0].Columns[11].ColumnName = "department";
                    dsAllEmployeeIncomes.Tables[0].Columns[12].ColumnName = "description";
                    dsAllEmployeeIncomes.Tables[0].Columns[13].ColumnName = "dflt_num";
                    dsAllEmployeeIncomes.Tables[0].Columns[14].ColumnName = "dflt_rate";
                    dsAllEmployeeIncomes.Tables[0].Columns[15].ColumnName = "dflt_hours";
                    dsAllEmployeeIncomes.Tables[0].Columns[16].ColumnName = "dflt_lo_inc_amt";
                    dsAllEmployeeIncomes.Tables[0].Columns[17].ColumnName = "dflt_hi_inc_amt";
                    dsAllEmployeeIncomes.Tables[0].Columns[18].ColumnName = "dflt_acct";
                    dsAllEmployeeIncomes.Tables[0].Columns[19].ColumnName = "dflt_dept";
                    dsAllEmployeeIncomes.Tables[0].Columns[20].ColumnName = "inc_type";
                    dsAllEmployeeIncomes.Tables[0].Columns[21].ColumnName = "empl_code";
                }
            }
            catch (Exception ex) { }
        }

        public static bool InsertIntoProcess_PayEmployee(ref DVOPayrollProcess_PayEmployee objProcess_PayEmployee, ref object objTrx)
        {
            try
            {
                if (objProcess_PayEmployee.bonus.Trim().Length <= 0)
                    objProcess_PayEmployee.bonus = "N";

                object[] parameters = new object[34];
                parameters[0] = objProcess_PayEmployee.Doc_no;
                parameters[1] = objProcess_PayEmployee.EmplCode;
                parameters[2] = objProcess_PayEmployee.doc_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[3] = objProcess_PayEmployee.pay_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[4] = objProcess_PayEmployee.eop_date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                parameters[5] = objProcess_PayEmployee.print_check;
                parameters[6] = objProcess_PayEmployee.Cash_acct_no;
                parameters[7] = "000";// objProcess_PayEmployee.Department;
                parameters[8] = objProcess_PayEmployee.cash_amount;
                parameters[9] = objProcess_PayEmployee.check_no;
                parameters[10] = objProcess_PayEmployee.inc_gross;
                parameters[11] = objProcess_PayEmployee.ded_fica;
                parameters[12] = objProcess_PayEmployee.inc_taxable;
                parameters[13] = objProcess_PayEmployee.ded_medicare;
                parameters[14] = objProcess_PayEmployee.ded_fedtax;
                parameters[15] = objProcess_PayEmployee.ded_statax;
                parameters[16] = objProcess_PayEmployee.ded_loctax;
                parameters[17] = objProcess_PayEmployee.ded_other;
                parameters[18] = objProcess_PayEmployee.obl_futa;
                parameters[19] = objProcess_PayEmployee.obl_fica;
                parameters[20] = objProcess_PayEmployee.obl_medicare;
                parameters[21] = objProcess_PayEmployee.obl_other;
                parameters[22] = objProcess_PayEmployee.obl_total;
                parameters[23] = objProcess_PayEmployee.inc_net;
                parameters[24] = objProcess_PayEmployee.inc_expense;
                parameters[25] = objProcess_PayEmployee.total_hours;
                parameters[26] = objProcess_PayEmployee.ok_to_post;
                parameters[27] = objProcess_PayEmployee.accrue_sick;
                parameters[28] = objProcess_PayEmployee.accrue_vac;
                parameters[29] = objProcess_PayEmployee.bonus;
                parameters[30] = objProcess_PayEmployee.deposit;
                parameters[31] = TimeCardUsedForPayroll ? 1 : 0;
                parameters[32] = UsedTimeCardNo;
                parameters[33] = objProcess_PayEmployee.start_date;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objProcess_PayEmployee.FIND_INSERT_Process_PayEmployee, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertIntoStypayid(DVOPayrollstypayid objDVOPayrollstypayid, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[13];
                parameters[0] = objDVOPayrollstypayid.Doc_no;
                parameters[1] = objDVOPayrollstypayid.line_no;
                parameters[2] = objDVOPayrollstypayid.inc_code.Trim();
                parameters[3] = objDVOPayrollstypayid.inc_rate;
                parameters[4] = objDVOPayrollstypayid.number;
                parameters[5] = objDVOPayrollstypayid.hours;
                parameters[6] = objDVOPayrollstypayid.amount;
                parameters[7] = objDVOPayrollstypayid.acct_no;
                parameters[8] = "000";// objDVOPayrollstypayid.Department.Trim();
                parameters[9] = objDVOPayrollstypayid.mod_flag;
                parameters[10] = objDVOPayrollstypayid.add_code.Trim();
                parameters[11] = objDVOPayrollstypayid.lo_inc_amt;
                parameters[12] = objDVOPayrollstypayid.hi_inc_amt;

                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypayid.INSERT_STYPAYID, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool InsertIntoStypaydd(DVOPayrollstypaydd objDVOPayrollstypaydd, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollstypaydd.Doc_no;
                parameters[1] = objDVOPayrollstypaydd.line_no;
                parameters[2] = objDVOPayrollstypaydd.ded_code.Trim();
                parameters[3] = objDVOPayrollstypaydd.ded_rate;
                parameters[4] = objDVOPayrollstypaydd.amount;
                parameters[5] = objDVOPayrollstypaydd.acct_no;
                parameters[6] = "000";// objDVOPayrollstypaydd.Department.Trim();
                parameters[7] = objDVOPayrollstypaydd.mod_flag;
                parameters[8] = objDVOPayrollstypaydd.add_code.Trim();
                parameters[9] = objDVOPayrollstypaydd.lo_ded_amt;
                parameters[10] = objDVOPayrollstypaydd.hi_ded_amt;
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollstypaydd.INSERT_STYPARDD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertIntoStypayod(DVOPayrollStypayod objDVOPayrollStypayod, ref object objTrx)
        {
            try
            {
                object[] parameters = new object[11];
                parameters[0] = objDVOPayrollStypayod.Doc_no;
                parameters[1] = objDVOPayrollStypayod.line_no;
                parameters[2] = objDVOPayrollStypayod.obl_code.Trim();
                parameters[3] = objDVOPayrollStypayod.obl_rate;
                parameters[4] = objDVOPayrollStypayod.amount;
                parameters[5] = objDVOPayrollStypayod.acct_no;
                parameters[6] = "000";// objDVOPayrollStypayod.Department.Trim();
                parameters[7] = objDVOPayrollStypayod.bal_acct_no;
                parameters[8] = objDVOPayrollStypayod.bal_dept.Trim();
                parameters[9] = objDVOPayrollStypayod.mod_flag;
                parameters[10] = objDVOPayrollStypayod.add_code.Trim();
                DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
                DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
                object InsResult = objDalBaseClass.ExecuteProcedure_ByTransaction(ref objTrx, ref parameters, objDVOPayrollStypayod.INSERT_STYPAYOD, true);
                if (InsResult.ToString().Trim() != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public decimal ded_taxcalc(int n, decimal tax_wages, string pay_period)
        {
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            // check for exempt status for state
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                if (objListemplforprocess[currentempnoid].StateAllow == 99)
                {
                    return 0;
                }
            }
            else
            {
                //if not state tax code then default to federal allowances
                if (objListemplforprocess[currentempnoid].Allowances == 99)
                {
                    return 0;
                }
            }
            // initialize flags
            bool check_year = false;
            year_is_current = true;
            decimal allow_amt = 0;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";

            //// get allowance value
            //object[] parameters = new object[2];
            //parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            //parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            switch (pay_period)
            {

                case "W":
                    if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                        {
                            object week_allow = drs[0]["week_allow"];
                            if (week_allow != DBNull.Value && week_allow.ToString().Trim().Length > 0)
                                allow_amt = Convert.ToDecimal(week_allow);
                            else
                                check_year = true;
                        }
                    }

                    //object week_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_week_allow);
                    //if (week_allow == null || week_allow.ToString().Trim() == string.Empty)
                    //    check_year = true;
                    //else
                    //    allow_amt = Convert.ToDecimal(week_allow);
                    break;
                case "B":
                    if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                        {
                            object biweek_allow = drs[0]["biweek_allow"];
                            if (biweek_allow != DBNull.Value && biweek_allow.ToString().Trim().Length > 0)
                                allow_amt = Convert.ToDecimal(biweek_allow);
                            else
                                check_year = true;
                        }
                    }
                    //object biweek_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_biweek_allow);
                    //if (biweek_allow == null || biweek_allow.ToString().Trim() == string.Empty)
                    //    check_year = true;
                    //else
                    //    allow_amt = Convert.ToDecimal(biweek_allow);
                    break;
                case "S":
                    if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                        {
                            object smonth_allow = drs[0]["smonth_allow"];
                            if (smonth_allow != DBNull.Value && smonth_allow.ToString().Trim().Length > 0)
                                allow_amt = Convert.ToDecimal(smonth_allow);
                            else
                                check_year = true;
                        }
                    }
                    //object smonth_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_smonth_allow);
                    //if (smonth_allow == null || smonth_allow.ToString().Trim() == string.Empty)
                    //    check_year = true;
                    //else
                    //    allow_amt = Convert.ToDecimal(smonth_allow);
                    break;
                case "M":
                    if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                        {
                            object month_allow = drs[0]["month_allow"];
                            if (month_allow != DBNull.Value && month_allow.ToString().Trim().Length > 0)
                                allow_amt = Convert.ToDecimal(month_allow);
                            else
                                check_year = true;
                        }
                    }
                    //object month_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_month_allow);
                    //if (month_allow == null || month_allow.ToString().Trim() == string.Empty)
                    //    check_year = true;
                    //else
                    //    allow_amt = Convert.ToDecimal(month_allow);
                    break;
                case "Q":
                    if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                        {
                            object quarter_allow = drs[0]["quarter_allow"];
                            if (quarter_allow != DBNull.Value && quarter_allow.ToString().Trim().Length > 0)
                                allow_amt = Convert.ToDecimal(quarter_allow);
                            else
                                check_year = true;
                        }
                    }
                    //object quarter_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_quarter_allow);
                    //if (quarter_allow == null || quarter_allow.ToString().Trim() == string.Empty)
                    //    check_year = true;
                    //else
                    //    allow_amt = Convert.ToDecimal(quarter_allow);
                    break;
                case "H":
                    if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                        {
                            object syear_allow = drs[0]["syear_allow"];
                            if (syear_allow != DBNull.Value && syear_allow.ToString().Trim().Length > 0)
                                allow_amt = Convert.ToDecimal(syear_allow);
                            else
                                check_year = true;
                        }
                    }
                    //object syear_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_syear_allow);
                    //if (syear_allow == null || syear_allow.ToString().Trim() == string.Empty)
                    //    check_year = true;
                    //else
                    //    allow_amt = Convert.ToDecimal(syear_allow);
                    break;
                case "A":
                    if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                        {
                            object year_allow = drs[0]["year_allow"];
                            if (year_allow != DBNull.Value && year_allow.ToString().Trim().Length > 0)
                                allow_amt = Convert.ToDecimal(year_allow);
                            else
                                check_year = true;
                        }
                    }
                    //object year_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
                    //if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
                    //    check_year = true;
                    //else
                    //    allow_amt = Convert.ToDecimal(year_allow);
                    break;
                case "D":
                    if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
                    {
                        DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                        if (drs.Length > 0)
                        {
                            object misc_allow = drs[0]["misc_allow"];
                            if (misc_allow != DBNull.Value && misc_allow.ToString().Trim().Length > 0)
                                allow_amt = Convert.ToDecimal(misc_allow);
                            else
                                check_year = true;
                        }
                    }
                    //object misc_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_misc_allow);
                    //if (misc_allow == null || misc_allow.ToString().Trim() == string.Empty)
                    //    check_year = true;
                    //else
                    //    allow_amt = Convert.ToDecimal(misc_allow);
                    break;
            }
            //check_year is set to true when a table lookup returns nothing
            //check to see if the table does exist, but the Tax Year is not current
            if (check_year)
            {
                //call tbl_check
                tbl_check(n);
            }
            // make sure required values exist
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[currentempnoid].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            // calculate taxable amount
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));

            }
            else
            {
                //if not state tax code then default to federal allowances
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            //get tax table values
            if (year_is_current)
            {
                if (dsAllDedTaxCalcDetail != null && dsAllDedTaxCalcDetail.Tables.Count > 0)
                {
                    DataRow[] drs = dsAllDedTaxCalcDetail.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim()
                        + "' AND pay_period = '" + pay_period.Trim()
                        + "' AND over_amt < " + t_total.ToString()
                        + " AND marital_stat = '" + objListemplforprocess[currentempnoid].MaritalStat.Trim() + "'", "base_amt");
                    Int32 CorrectSlab = 0;
                    for (Int32 j = 0; j < drs.Length; j++)
                    {
                        if (drs[j]["base_amt"] != DBNull.Value && t_total != null)
                        {
                            if (Convert.ToDecimal(t_total) > Convert.ToDecimal(drs[j]["base_amt"]))
                            {
                                CorrectSlab = j;
                            }
                        }
                    }
                    if (drs.Length > 0)
                    {
                        t_base_amt = drs[CorrectSlab]["base_amt"] != DBNull.Value ? Convert.ToDecimal(drs[CorrectSlab]["base_amt"]) : 0.0M;
                        t_rate = drs[CorrectSlab]["tax_rate"] != DBNull.Value ? Convert.ToDecimal(drs[CorrectSlab]["tax_rate"]) : 0.0M;
                        t_over_amt = drs[CorrectSlab]["over_amt"] != DBNull.Value ? Convert.ToDecimal(drs[CorrectSlab]["over_amt"]) : 0.0M;
                        t_period = drs[CorrectSlab]["pay_period"] != DBNull.Value ? drs[CorrectSlab]["pay_period"].ToString().Trim() : string.Empty;
                        t_marital = drs[CorrectSlab]["marital_stat"] != DBNull.Value ? drs[CorrectSlab]["marital_stat"].ToString().Trim() : string.Empty;
                    }
                }
                //object[] taxparameter = new object[5];
                //taxparameter[0] = objPayrolldeductionsglobal[n].ded_code;
                //taxparameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
                //taxparameter[2] = pay_period.Trim();
                //taxparameter[3] = t_total;
                //taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                //DataSet ds = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref taxparameter, objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    if (i == 0)
                //    {
                //        DataRow dr = ds.Tables[0].Rows[i];
                //        t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                //        t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                //        t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                //        t_period = dr[3].ToString().Trim();
                //        t_marital = dr[4].ToString().Trim();
                //    }
                //}
                //calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }

            return t_total;
        }
        public void tbl_check(int n)
        {
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            //// this function is called to verify that if a table exists, that
            //// the Tax Year matches the payroll date year.
            //object[] parameter = new object[2];
            //parameter[0] = objPayrolldeductionsglobal[n].ded_code;
            //parameter[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            //DataSet ds = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref parameter, objPayrolldeductionsglobal[n].FIND_usp_tbl_check);
            //if (ds.Tables[0].Rows[0][0] == DBNull.Value || ds.Tables[0].Rows[0][0].ToString().Trim() == string.Empty)
            //{
            //    ErrMsg2 = "*** Error: Table(s) not current for employee's deduction codes.";
            //    ErrMsg1 = "**** End of report.  One or more deduction tables are not current.";
            //    year_is_current = false;
            //}

            if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
            {
                DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                if (drs.Length <= 0)
                {
                    ErrMsg2 = "*** Error: Table(s) not current for employee's deduction codes.";
                    ErrMsg1 = "**** End of report.  One or more deduction tables are not current.";
                    year_is_current = false;
                }
            }
        }
        public decimal state_calc(int n)
        {
            // define
            decimal wage_amount = 0;
            decimal statax_amount = 0;
            // set wage_amount appropriately
            if (objPayrolldeductionsglobal[n].ded_type.Trim() == "G")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_gross ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "T")
            {
                wage_amount = objDVOPayrollProcess_PayEmployeeList[n].inc_taxable ?? 0;//make nullable decimal By Rahul
            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "F")
            {
                wage_amount = fica_wages;

            }
            else if (objPayrolldeductionsglobal[n].ded_type.Trim() == "U")
            {
                wage_amount = futa_wages;
            }
            else
            {
                wage_amount = 0;
            }
            if (objPayrolldeductionsglobal[n].tax_code == string.Empty)
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));

                }
                else
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate * wage_amount));
                }
            }
            else
            {
                if (objPayrolldeductionsglobal[n].ded_rate >= 1 || objPayrolldeductionsglobal[n].ded_rate == 0)
                {
                    statax_amount = Convert.ToDecimal(BPfunctions.Al_Round("a", objPayrolldeductionsglobal[n].ded_rate));
                }
                else
                {
                    statax_amount = ded_taxcalc(n, wage_amount, objListemplforprocess[n].PayPeriod);

                }

            }
            return statax_amount;
        }
        public decimal ded_fedgrs(int n, decimal tax_wages, string pay_period)
        {
            //DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            decimal allow_amt = 0;
            bool check_year = false;
            bool year_is_current = true;
            decimal t_total = 0;
            decimal t_base_amt = 0;
            decimal t_rate = 0;
            decimal t_over_amt = 0;
            string t_period = "";
            string t_marital = "";
            //object[] parameters = new object[2];
            //parameters[0] = objPayrolldeductionsglobal[n].ded_code;
            //parameters[1] = objpaydatasearch.Payroll_Date.ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            //object year_allow = objDalBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, objPayrolldeductionsglobal[0].FIND_year_allow);
            //if (year_allow == null || year_allow.ToString().Trim() == string.Empty)
            //{
            //    check_year = true;
            //}
            //else
            //{
            //    allow_amt = Convert.ToDecimal(year_allow);
            //}

            if (dsAllDedTaxCalc != null && dsAllDedTaxCalc.Tables.Count > 0)
            {
                DataRow[] drs = dsAllDedTaxCalc.Tables[0].Select("ded_code = '" + objPayrolldeductionsglobal[n].ded_code.Trim() + "'");
                if (drs.Length > 0)
                {
                    object year_allow = drs[0]["year_allow"];
                    if (year_allow != DBNull.Value && year_allow.ToString().Trim().Length > 0)
                        allow_amt = Convert.ToDecimal(year_allow);
                    else
                        check_year = true;
                }
            }

            if (check_year)
            {
                tbl_check(n);
            }
            if (objListemplforprocess[currentempnoid].StateAllow == 0)
            {
                objListemplforprocess[n].StateAllow = objListemplforprocess[currentempnoid].Allowances;
            }
            if (objPayrolldeductionsglobal[n].ded_code == objListemplforprocess[currentempnoid].StaTaxCode)
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].StateAllow * allow_amt)));
            }
            else
            {
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", tax_wages - (objListemplforprocess[currentempnoid].Allowances * allow_amt)));
            }
            if (t_total < 0)
            {
                t_total = 0;
            }
            if (year_is_current)
            {
                if (dsAllDedTaxCalcDetail != null && dsAllDedTaxCalcDetail.Tables.Count > 0)
                {
                    DataRow[] drs = dsAllDedTaxCalcDetail.Tables[0].Select("ded_code = '" + objstycntrcList[0].fedtax_code.Trim()
                        + "' AND pay_period = 'A'"
                        + " AND over_amt < " + t_total.ToString()
                        + " AND marital_stat = '" + objListemplforprocess[currentempnoid].MaritalStat.Trim() + "'");
                    if (drs.Length > 0)
                    {
                        t_base_amt = drs[0]["base_amt"] != DBNull.Value ? Convert.ToDecimal(drs[0]["base_amt"]) : 0.0M;
                        t_rate = drs[0]["tax_rate"] != DBNull.Value ? Convert.ToDecimal(drs[0]["tax_rate"]) : 0.0M;
                        t_over_amt = drs[0]["over_amt"] != DBNull.Value ? Convert.ToDecimal(drs[0]["over_amt"]) : 0.0M;
                        t_period = drs[0]["pay_period"] != DBNull.Value ? drs[0]["pay_period"].ToString().Trim() : string.Empty;
                        t_marital = drs[0]["marital_stat"] != DBNull.Value ? drs[0]["marital_stat"].ToString().Trim() : string.Empty;
                    }
                }

                //object[] taxparameter = new object[5];
                //taxparameter[0] = objstycntrcList[0].fedtax_code;
                //taxparameter[1] = objDVOPayrollProcess_PayEmployeeList[n].pay_date;
                //taxparameter[2] = "A";
                //taxparameter[3] = t_total;
                //taxparameter[4] = objListemplforprocess[currentempnoid].MaritalStat.Trim();
                //DataSet ds = objDalBaseClass.GetData_ByTransaction(ref objTransaction, ref taxparameter, objPayrolldeductionsglobal[0].FIND_TaxValueGet);
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    t_base_amt = (dr[0] != DBNull.Value ? Convert.ToDecimal(dr[0]) : 0.0M);
                //    t_rate = (dr[1] != DBNull.Value ? Convert.ToDecimal(dr[1]) : 0.0M);
                //    t_over_amt = (dr[2] != DBNull.Value ? Convert.ToDecimal(dr[2]) : 0.0M);
                //    t_period = dr[3].ToString().Trim();
                //    t_marital = dr[4].ToString().Trim();
                //}
                // calculate taxes
                t_total = Convert.ToDecimal(BPfunctions.Al_Round("a", t_base_amt + (t_rate * (t_total - t_over_amt))));
                if (t_total < 0)
                {
                    t_total = 0;
                }

            }
            return t_total;
        }
        /// <summary>
        /// status of Lock, if current record is locked than 1, otherwise 0.
        /// </summary>
        int _ChangeLockStatus = 0;
        private int LockPayrollDefaultRecord(ref object TransactionObject, ref DVOUpdatePayDefaults objDVOUpdatePayDefaults)
        {
            int LockStatus = BLLCommonUtilities.LockCurrentRecord(ref TransactionObject, (iDVO)objDVOUpdatePayDefaults, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo);
            if (LockStatus != 1)
            {
                _ChangeLockStatus = 0;
                System.Windows.Forms.DialogResult d = System.Windows.Forms.MessageBox.Show("Want to wait to release the record?", "I.F.M.S.", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Threading.Thread.Sleep(10000);
                    LockPayrollDefaultRecord(ref TransactionObject, ref objDVOUpdatePayDefaults);
                }
            }
            else
            {
                _ChangeLockStatus = 1;
            }
            return _ChangeLockStatus;
        }

        private void ReleaseAndUpdatePayrollDefaultRecord(bool IsCommit, bool IsRollback, ref object TransactionObject, ref DVOUpdatePayDefaults objDVOUpdatePayDefaults, int NewPayrollDocumentNo)
        {
            if (TransactionObject != null)
            {
                objDVOUpdatePayDefaults.py_doc_no = NewPayrollDocumentNo;
                if (NewPayrollDocumentNo > 0)
                    BLLUpdPayDefault.UpdatePayDefaults_PyDocNo(ref TransactionObject, ref objDVOUpdatePayDefaults);
                BLLCommonUtilities.ReleaseLockCurrentRecord(ref TransactionObject, (iDVO)objDVOUpdatePayDefaults, DVOApplicationUserInfo.UserId, DVOApplicationUserInfo.MachineInfo, IsCommit);
                if ((!IsCommit) && IsRollback)
                    BLLCommonUtilities.LockTransaction_Rollback(ref TransactionObject);
                TransactionObject = null;
                _ChangeLockStatus = 0;
            }
        }


        ///<Development and modification Details>
        //Aim :- To make AutoPay Process faster, Found that this is pushing the Data base every times it need , where as 
        //just to load the data there should not be any need of Such type. It will just Include Loading Account Nos .
        //So just Copied the section FlexSegload
        //Written By : Copied from the BllflexSegCommon , Which is written By Rajeev
        // Written Date : 26 january 2010
        ///<summery>
        //DVOFlexSegCommon tempDvoFexprm_entity_code_accType = new DVOFlexSegCommon();

        //**********************************************************************************************************************
        // The Function in the file are used to manupulate the content of the table Flex_Segment_Reference. This table contain three column  *
        //        Flex_Segment_Reference.entity_type --> Determine which programe the row is releventto and is a qualified for Flex_Segment_Reference.code *
        //        Flex_Segment_Reference.code --> Identifies which entity in another table(Determine by the entity type) this row concern    * 
        //        Flex_Segment_Reference.segvd_id --> The Segment value id(Master_Segment.id),used to build a (partial or whole) keyvalue          *
        //                                                                                                                     *
        //**********************************************************************************************************************

        #region FunctionFlexSeg_Load
        //To Call this function 
        //Pass Parameter : EntityType , Code  And Account Type
        //Set EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
        //This Function calculate the keyvalue and return it as a string value     
        public string PayrollFlexseg_Load(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
        {
            int locPosition, locLength;
            //DVOFlexSegCommon objDvoFlexSegVal = new DVOFlexSegCommon();
            List<DVOFlexSegCommon> objDVOFlexSegCommonlist = new List<DVOFlexSegCommon>();
            int locKeyLength;
            string ret_keyvalue = string.Empty;
            string copyKeyval = string.Empty;
            //   DataSet loadFlexFeptPrepDS = BLLFlexSegPayroll.PayrollGetSegmentValInformation(ref tempDvoFexprm_entity_code_accType);

            int KeyLengthPrep = 0;// = PayrollGetKeyLengthInformation(ref tempDvoFexprm_entity_code_accType);
            if (dsAllAcctType != null && dsAllAcctType.Tables.Count > 0 && dsAllAcctType.Tables[0].Rows.Count > 0)
            {
                DataRow[] drAcctType = dsAllAcctType.Tables[0].Select(" accounttype='" + tempDvoFexprm_entity_code_accType.AccountType.Trim().Replace("'", "''") + "'");
                if (drAcctType != null && drAcctType.Length > 0)
                    foreach (DataRow dr in drAcctType)
                    {
                        try
                        {
                            if (dr[1] != null)
                            {
                                KeyLengthPrep = Convert.ToInt32(dr[1]);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
            }
            //Getting KeyLength value
            if (KeyLengthPrep > 0)
            {
                locKeyLength = Convert.ToInt32(KeyLengthPrep);
                if (locKeyLength > 0)
                {
                    //Concatination the ret_value with #, Uptill the length of locKeyvalue
                    for (int i = 1; i <= locKeyLength; i++)
                    {
                        ret_keyvalue = ret_keyvalue + "#";
                    }
                }
            }
            if (dsAllFlexKeyVal != null && dsAllFlexKeyVal.Tables.Count > 0 && dsAllFlexKeyVal.Tables[0].Rows.Count > 0)
            {

                DataRow[] drflexval = dsAllFlexKeyVal.Tables[0].Select(" entity_type = '" + tempDvoFexprm_entity_code_accType.EntityType.Trim().Replace("'", "''") + "' AND code='" + tempDvoFexprm_entity_code_accType.Code.Trim().Replace("'", "''") + "' AND accounttype='" + tempDvoFexprm_entity_code_accType.AccountType.Trim().Replace("'", "''") + "'");
                if (drflexval != null && drflexval.Length > 0)
                    foreach (DataRow dr in drflexval)
                    {
                        DVOFlexSegCommon tempobjDvoFlexSegVal = new DVOFlexSegCommon();
                        tempobjDvoFlexSegVal.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);  //locSegment
                        tempobjDvoFlexSegVal.position = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0); //locPosition
                        tempobjDvoFlexSegVal.length = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0); //locLength
                        tempobjDvoFlexSegVal.abbreviation = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty); //locAbbrev

                        //Logic to find the starting index and end index from the ret_keyvalue and replace with the new keyvalue
                        locPosition = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0);
                        locLength = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0);
                        copyKeyval = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);

                        StringBuilder retKeyValueBuilder = new StringBuilder(ret_keyvalue);

                        // Replace characters using a foreach loop
                        int startIndex = locPosition - 1; // Adjust for zero-based index
                        foreach (char c in copyKeyval)
                        {
                            if (startIndex < retKeyValueBuilder.Length) // Check if the index is within bounds
                            {
                                retKeyValueBuilder[startIndex] = c; // Replace character at the startIndex
                                startIndex++;
                            }
                        }

                        // Convert StringBuilder back to string
                        ret_keyvalue = retKeyValueBuilder.ToString();

                        //tempobjDvoFlexSegVal.Ret_Keyvalue = ret_keyvalue;
                        objDVOFlexSegCommonlist.Add(tempobjDvoFlexSegVal);
                    }

            }
            return ret_keyvalue;

        }

        //To Call this function 
        //Pass Parameter : EntityType , Code  And Account Type
        //Set EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
        //This Function calculate the keyvalue and return it as a string value     
        public static string PayrollFlexseg_Load_UsingDataReader(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
        {
            int locPosition, locLength;
            //DVOFlexSegCommon objDvoFlexSegVal = new DVOFlexSegCommon();
            List<DVOFlexSegCommon> objDVOFlexSegCommonlist = new List<DVOFlexSegCommon>();
            int locKeyLength;
            string ret_keyvalue = string.Empty;
            string copyKeyval = string.Empty;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();

            int KeyLengthPrep = 0;
            object[] parameters = new object[1];
            parameters[0] = tempDvoFexprm_entity_code_accType.AccountType;
            object obj = objDALBaseClass.ExecuteScalar(ref parameters, tempDvoFexprm_entity_code_accType.FIND_KEYLEN);
            if (obj != null && obj != DBNull.Value)
                KeyLengthPrep = Convert.ToInt32(obj);
            //Getting KeyLength value
            if (KeyLengthPrep > 0)
            {
                locKeyLength = KeyLengthPrep;
                if (locKeyLength > 0)
                    //Concatination the ret_value with #, Uptill the length of locKeyvalue
                    for (int i = 1; i <= locKeyLength; i++)
                        ret_keyvalue = ret_keyvalue + "#";
            }

            parameters = new object[3];
            parameters[0] = tempDvoFexprm_entity_code_accType.EntityType;
            parameters[1] = tempDvoFexprm_entity_code_accType.Code;
            parameters[2] = tempDvoFexprm_entity_code_accType.AccountType;
            IDataReader dr = objDALBaseClass.GetDataByReader(ref parameters, tempDvoFexprm_entity_code_accType.FIND_SPNAME);


            while (dr.Read())
            {
                using (DVOFlexSegCommon tempobjDvoFlexSegVal = new DVOFlexSegCommon())
                {
                    tempobjDvoFlexSegVal.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);  //locSegment
                    tempobjDvoFlexSegVal.position = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0); //locPosition
                    tempobjDvoFlexSegVal.length = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0); //locLength
                    tempobjDvoFlexSegVal.abbreviation = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty); //locAbbrev

                    //Logic to find the starting index and end index from the ret_keyvalue and replace with the new keyvalue
                    locPosition = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0);
                    locLength = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0);
                    copyKeyval = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);

                    StringBuilder retKeyValueBuilder = new StringBuilder(ret_keyvalue);

                    // Replace characters using a foreach loop
                    int startIndex = locPosition - 1; // Adjust for zero-based index
                    foreach (char c in copyKeyval)
                    {
                        if (startIndex < retKeyValueBuilder.Length) // Check if the index is within bounds
                        {
                            retKeyValueBuilder[startIndex] = c; // Replace character at the startIndex
                            startIndex++;
                        }
                    }

                    // Convert StringBuilder back to string
                    ret_keyvalue = retKeyValueBuilder.ToString();

                    //tempobjDvoFlexSegVal.Ret_Keyvalue = ret_keyvalue;
                    objDVOFlexSegCommonlist.Add(tempobjDvoFlexSegVal);
                }
            }
            dr.Close();
            return ret_keyvalue;
        }

        //To Call this function 
        //Pass Parameter : EntityType , Code  And Account Type
        //Set EntityType , Code and AccountType value in DVOFlexSegCommon and then call this function
        //This Function calculate the keyvalue and return it as a string value     
        public static string PayrollFlexseg_Load_UsingTransaction(ref Object objTransaction, ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
        {
            //    DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            //    DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            //    bool statusObjTransaction = true;
            //    if (objTransaction == null)
            //    {
            //        objTransaction = objDALBaseClassHelper.GetTransactionObject();
            //        statusObjTransaction = false;
            //    }
            string ret_keyvalue = string.Empty;

            //    try
            //    {
            //        int locPosition, locLength;
            //        //DVOFlexSegCommon objDvoFlexSegVal = new DVOFlexSegCommon();
            //        List<DVOFlexSegCommon> objDVOFlexSegCommonlist = new List<DVOFlexSegCommon>();
            //        int locKeyLength;
            //        string copyKeyval = string.Empty;

            //        int KeyLengthPrep = 0;
            //        object[] parameters = new object[1];
            //        parameters[0] = tempDvoFexprm_entity_code_accType.AccountType;
            //        object obj = objDALBaseClass.ExecuteScalar_ByTransaction(ref objTransaction, ref parameters, tempDvoFexprm_entity_code_accType.FIND_KEYLEN);
            //        if (obj != null && obj != DBNull.Value)
            //            KeyLengthPrep = Convert.ToInt32(obj);
            //        //Getting KeyLength value
            //        if (KeyLengthPrep > 0)
            //        {
            //            locKeyLength = KeyLengthPrep;
            //            if (locKeyLength > 0)
            //                //Concatination the ret_value with #, Uptill the length of locKeyvalue
            //                for (int i = 1; i <= locKeyLength; i++)
            //                    ret_keyvalue = ret_keyvalue + "#";
            //        }

            //        parameters = new object[3];
            //        parameters[0] = tempDvoFexprm_entity_code_accType.EntityType;
            //        parameters[1] = tempDvoFexprm_entity_code_accType.Code;
            //        parameters[2] = tempDvoFexprm_entity_code_accType.AccountType;
            //        using (DataSet ds = objDALBaseClass.GetData_ByTransaction(ref objTransaction, ref parameters, tempDvoFexprm_entity_code_accType.FIND_SPNAME))
            //        {
            //            if (ds != null && ds.Tables.Count > 0)
            //                foreach (DataRow dr in ds.Tables[0].Rows)
            //                {
            //                    using (DVOFlexSegCommon tempobjDvoFlexSegVal = new DVOFlexSegCommon())
            //                    {
            //                        tempobjDvoFlexSegVal.keyvalue = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);  //locSegment
            //                        tempobjDvoFlexSegVal.position = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0); //locPosition
            //                        tempobjDvoFlexSegVal.length = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0); //locLength
            //                        tempobjDvoFlexSegVal.abbreviation = (dr[3] != DBNull.Value ? dr[3].ToString().Trim() : string.Empty); //locAbbrev

            //                        //Logic to find the starting index and end index from the ret_keyvalue and replace with the new keyvalue
            //                        locPosition = (dr[1] != DBNull.Value ? Convert.ToInt32(dr[1].ToString().Trim()) : 0);
            //                        locLength = (dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString().Trim()) : 0);
            //                        copyKeyval = (dr[0] != DBNull.Value ? dr[0].ToString().Trim() : string.Empty);

            //                        char[] RetKeyValueArray = ret_keyvalue.ToCharArray();
            //                        copyKeyval.CopyTo(0, RetKeyValueArray, (locPosition - 1), copyKeyval.Length);
            //                        //Logic End here
            //                        String strRetkeyvalue = new String(RetKeyValueArray);
            //                        ret_keyvalue = strRetkeyvalue;
            //                        //tempobjDvoFlexSegVal.Ret_Keyvalue = strRetkeyvalue;
            //                        objDVOFlexSegCommonlist.Add(tempobjDvoFlexSegVal);
            //                    }
            //                }
            //        }
            //        if (!statusObjTransaction && objTransaction != null)
            //            objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            //    }
            //    catch (Exception ex)
            //    {
            //        if (!statusObjTransaction && objTransaction != null)
            //            objDALBaseClassHelper.RollbackTransaction(ref objTransaction);
            //        ExceptionManager.Publish(ex);
            //        throw ex;
            //    }
            return ret_keyvalue;
        }

        //Function used to get the information of segment value
        //Filter Criteria "Entity_Type,Account_Type and Code"
        // Value Return Keyvalue,Position,Length,Abbreviation
        private static DataSet PayrollGetSegmentValInformation(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
        {
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            object[] parameters = new object[3];
            try
            {

                parameters[0] = tempDvoFexprm_entity_code_accType.EntityType;
                parameters[1] = tempDvoFexprm_entity_code_accType.Code;
                parameters[2] = tempDvoFexprm_entity_code_accType.AccountType;

                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), tempDvoFexprm_entity_code_accType.FIND_SPNAME);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    parameters = null;
                    objDALBaseClass = null;
                    return ds;
                }
            }
            catch (Exception ex)
            {
                parameters = null;
                objDALBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return ds;
        }

        //Function used to get the length of the keyvalue, According to account type.
        //Table used ingflxkh
        private int PayrollGetKeyLengthInformation(ref DVOFlexSegCommon tempDvoFexprm_entity_code_accType)
        {
            int RetKeyval = 0;
            DataSet ds = null;
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDALBaseClass = objDALBaseClassHelper.GetDAL();
            try
            {
                object[] parameters = new object[1];
                parameters[0] = tempDvoFexprm_entity_code_accType.AccountType;

                ds = objDALBaseClass.GetData(ref parameters, typeof(DVOFlexSegCommon), tempDvoFexprm_entity_code_accType.FIND_KEYLEN);
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            RetKeyval = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                        }
                }
                parameters = null;
                objDALBaseClass = null;
            }
            catch (Exception ex)
            {

                objDALBaseClass = null;
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return RetKeyval;
        }

        #endregion FunctionFlexSeg_Load


        /// <summary>
        /// To Get AccountNumber for entered AccountType and keyvalue
        /// </summary>
        /// <param name="p_accountType"></param>
        /// <param name="p_Keyvalue"></param>
        /// <returns></returns>
        public int PayrollGetAccountNumber(string p_accountType, string p_Keyvalue)
        {
            int _AccountNumber = 0;
            String AccountDesc = "";
            try
            {
                if (dsallPayrollGLAccountsData != null && dsallPayrollGLAccountsData.Tables.Count > 0)
                {
                    DataRow[] drs = dsallPayrollGLAccountsData.Tables[0].Select("keyvalue = '" + p_Keyvalue + "' AND acct_type= '" + p_accountType + "'");

                    if (drs.Length > 0)
                    {
                        if (drs[0][0] != null)
                        {
                            _AccountNumber = Convert.ToInt32(drs[0][0]);
                        }
                    }
                    else
                    {
                        DVOGeneralLedger objdvogeneralledger = new DVOGeneralLedger();
                        objdvogeneralledger.acct_type = p_accountType;
                        objdvogeneralledger.keyvalue = p_Keyvalue;
                        BLLGeneralLedger.CreateLedgerAccounts(ref objdvogeneralledger, out _AccountNumber, out AccountDesc);
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManagement.ExceptionManager.Publish(ex);
            }
            return _AccountNumber;
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
        public void PayrollflxMix(string p_acct_type, string p_keyvalue, string p_dept_acct_type, string p_flex_dept, out int ret_acct_no, out string ret_acct_type, out string ret_keyvalue)
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

            ret_acct_no = PayrollGetAccountNumber(ret_acct_type, ret_keyvalue);
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
        public void PayrollflxMixInc(string p_empl_code, string p_inc_code, string p_empl_type, out int ret_acct_no, out string ret_acct_type, out string ret_keyvalue)
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

            if (dsAllIncomesstyincr != null && dsAllIncomesstyincr.Tables.Count > 0)
            {
                DataRow[] drs = dsAllIncomesstyincr.Tables[0].Select("inc_code = '" + p_inc_code + "'");

                if (drs.Length > 0)
                {
                    if (drs[0][0] != null)
                    {
                        loc_inc_accttype = drs[0][1].ToString();
                    }
                }

            }

            DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
            objDVOFlexSegCommon.EntityType = "MasterIncCodes";
            objDVOFlexSegCommon.Code = p_inc_code;
            objDVOFlexSegCommon.AccountType = loc_inc_accttype;
            string _keyvalue = PayrollFlexseg_Load(ref objDVOFlexSegCommon);

            objDVOFlexSegCommon.EntityType = "styemplr";
            objDVOFlexSegCommon.Code = p_empl_code;
            objDVOFlexSegCommon.AccountType = loc_emp_accttype;
            string _flexdepartment = PayrollFlexseg_Load(ref objDVOFlexSegCommon);

            //call flxMix function
            PayrollflxMix(loc_inc_accttype, _keyvalue, loc_emp_accttype, _flexdepartment,
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
        public void PayrollflxMixObl(string p_empl_code, string p_empl_type, string p_obl_code, out int ret_acct_no, out string ret_acct_type, out string ret_keyvalue)
        {
            // used to grab the acct_types for the employee and the income code
            string loc_emp_accttype = string.Empty; //char(6)  LIKE styemplr.flexdeptaccttype,
            string loc_obl_accttype = string.Empty; //char(6)  LIKE MasterIncCodes.dfltaccounttype

            //# the return values
            ret_acct_no = 0;  // LIKE PayrollGLAccounts.acct_no,
            ret_acct_type = string.Empty;  //char(6) LIKE PayrollGLAccounts.acct_type,
            ret_keyvalue = string.Empty;   //char(100)  LIKE PayrollGLAccounts.keyvalue

            // grab the account types
            DVOMasterEmployee objDVOMasterEmployee = new DVOMasterEmployee();
            objDVOMasterEmployee.EmplCode = p_empl_code;
            List<DVOMasterEmployee> listDVOMasterEmployee = BLLMasterEmployee.GetData(ref objDVOMasterEmployee);
            if (listDVOMasterEmployee.Count > 0)
                loc_emp_accttype = listDVOMasterEmployee[0].FlexDeptAcctType;
            objDVOMasterEmployee = null;
            listDVOMasterEmployee = null;


            if (dsAllObligationsMasterOblCodes != null && dsAllObligationsMasterOblCodes.Tables.Count > 0)
            {
                DataRow[] drs = dsAllObligationsMasterOblCodes.Tables[0].Select("obl_code = '" + p_obl_code + "'");

                if (drs.Length > 0)
                {
                    if (drs[0][0] != null)
                    {
                        loc_obl_accttype = drs[0][1].ToString();
                    }
                }

            }


            DVOFlexSegCommon objDVOFlexSegCommon = new DVOFlexSegCommon();
            objDVOFlexSegCommon.EntityType = "MasterOblCodes";
            objDVOFlexSegCommon.Code = p_obl_code;
            objDVOFlexSegCommon.AccountType = loc_obl_accttype;
            string _keyvalue = PayrollFlexseg_Load(ref objDVOFlexSegCommon);

            objDVOFlexSegCommon.EntityType = "styemplr";
            objDVOFlexSegCommon.Code = p_empl_code;
            objDVOFlexSegCommon.AccountType = loc_emp_accttype;
            string _flexdepartment = PayrollFlexseg_Load(ref objDVOFlexSegCommon);

            //call flxMix function
            PayrollflxMix(loc_obl_accttype, _keyvalue, loc_emp_accttype, _flexdepartment,
                    out ret_acct_no, out ret_acct_type, out ret_keyvalue);
        }
        public static DataSet updatePostedstatus(ref DVOPayrollProcess_PayEmployee testdvo)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DataSet I;

            I = objDalBaseClass.ExecuteQuery_ByTransaction(ref objTransaction, testdvo.Update_Postedstatus);

            if (objTransaction != null)
            {
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            return I;


        }

        public static DataSet Update_PostedstatusByDistrict(ref DVOPayrollProcess_PayEmployee testdvo, string District)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DataSet I;

            I = objDalBaseClass.ExecuteQuery_ByTransaction(ref objTransaction, testdvo.Update_PostedstatusByDistrict(District));

            if (objTransaction != null)
            {
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            return I;


        }

        public static DataSet updateCancelstatus1(ref DVOPayrollProcess_PayEmployee PayrollProcess_PayEmployee)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DataSet I;
            object[] parameters = new object[1];
            parameters[0] = PayrollProcess_PayEmployee.EmplCode;
            I = objDalBaseClass.ExecuteQuery_ByTransaction(ref objTransaction, PayrollProcess_PayEmployee.Update_Cancelstatus1(ref parameters));

            if (objTransaction != null)
            {
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            return I;


        }

        public static DataSet updateCancelstatus(ref DVOPayrollProcess_PayEmployee testdvo)
        {

            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            object objTransaction = objDALBaseClassHelper.GetTransactionObject();
            DataSet I;

            I = objDalBaseClass.ExecuteQuery_ByTransaction(ref objTransaction, testdvo.Update_Cancelstatus);

            if (objTransaction != null)
            {
                objDALBaseClassHelper.CommitTransaction(ref objTransaction);
            }
            return I;


        }

        public static DataSet LoadPayrollEmp()
        {
            DALBaseClassHelper objDALBaseClassHelper = new DALBaseClassHelper();
            DALBaseClass objDalBaseClass = objDALBaseClassHelper.GetDAL();
            DataSet PayrollEmp = null;
            try
            {
                object[] parameters = new object[1];

                PayrollEmp = objDalBaseClass.GetData((new DVOPayrollProcess_PayEmployee()).FINND_PayrollEmp(ref parameters));
                return PayrollEmp;
            }
            catch (Exception ex)
            {
                return PayrollEmp;
            }
        }
    }



}

