using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOPayrollProcess_PayEmployee : DVOBase
    {
        //Process_PayEmployee 
        private int _RowID;
        private int _Doc_no;
        private string _EmplCode;
        string _EmplSSN;
        private string _Type_Code;
        private DateTime _doc_date;  //date
        private DateTime _pay_date;   //date
        private DateTime _eop_date;   //date
        private string _print_check;
        private int _Cash_acct_no;
        private string _Department;
        private decimal? _cash_amount;
        private int _check_no;
        private decimal? _inc_gross;
        private decimal? _ded_fica;
        private decimal? _inc_taxable;
        private decimal? _ded_medicare;
        private decimal? _ded_fedtax;
        private decimal? _ded_statax;
        private decimal? _ded_loctax;
        private decimal? _ded_other;
        private decimal? _obl_futa;
        private decimal? _obl_fica;
        private decimal? _obl_medicare;
        private decimal? _obl_other;
        private decimal? _obl_total;
        private decimal? _inc_net;
        private decimal? _inc_expense;
        private decimal? _total_hours;
        private string _ok_to_post;
        private string _accrue_sick;//date
        private string _accrue_vac;
        private string _bonus;
        private string _deposit;
        private string _StateTaxCode;
        private string _InsertMachineInfo;
        private string _InsertDate;//date
        private int _InsertBy;
        private string _UpdateMachineInfo;
        private string _UpdateDate;//date
        private int _UpdateBy;
        //stypaydd
        private string _ded_code;
        //MasterEmployee
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _PayPeriod;
        private string _LastPay;
        private string _email;
        private string _District;
        string _stataxcode;
        //PayrollGLAccounts
        private string _kayvalue;
        string _empflexdeptaccttype;
        private DateTime _start_date;   //date
        #region UsingInAutoPayroll
        List<DVOPayrollstypayid> listDVOPayrollstypayid;
        List<DVOPayrollstypaydd> listDVOPayrollstypaydd;
        List<DVOPayrollStypayod> listDVOPayrollStypayod;
        #endregion UsingInAutoPayroll
        #region Constructor

        public DVOPayrollProcess_PayEmployee()
        {
            _RowID = 0;
            _Doc_no = 0;
            _doc_date = Convert.ToDateTime(null);
            _pay_date = Convert.ToDateTime(null);
            _eop_date = Convert.ToDateTime(null);
            _print_check = string.Empty;
            _EmplCode = string.Empty;
            _EmplSSN = string.Empty;
            _Type_Code = string.Empty;
            _Cash_acct_no = 0;
            _Department = string.Empty;
            _cash_amount = null;
            _check_no = 0;
            _inc_gross = null;
            _ded_fica = null;
            _inc_taxable = null;
            _ded_medicare = null;
            _ded_fedtax = null;
            _ded_statax = null;
            _ded_loctax = null;
            _ded_other = null;
            _obl_futa = null;
            _obl_fica = null;
            _obl_medicare = null;
            _obl_other = null;
            _obl_total = null;
            _inc_net = null;
            _inc_expense = null;
            _total_hours = null;
            _ok_to_post = string.Empty;
            _accrue_sick = string.Empty;
            _accrue_vac = string.Empty;
            _bonus = string.Empty;
            _deposit = string.Empty;
            _InsertMachineInfo = string.Empty;
            _InsertDate = "01/01/1900";
            _InsertBy = 0;
            _UpdateMachineInfo = string.Empty;
            _UpdateDate = "01/01/1900";
            _UpdateBy = 0;
            _StateTaxCode = string.Empty;
            _ded_code = string.Empty;
            _FirstName = string.Empty;
            _MiddleName = string.Empty;
            _LastName = string.Empty;
            _stataxcode = string.Empty;
            _PayPeriod = string.Empty;
            _LastPay = "01/01/1900";
            _kayvalue = string.Empty;
            _email = string.Empty;
            _empflexdeptaccttype = string.Empty;
            _start_date = Convert.ToDateTime(null);
            #region UsingInAutoPayroll
            listDVOPayrollstypayid = new List<DVOPayrollstypayid>();
            listDVOPayrollstypaydd = new List<DVOPayrollstypaydd>();
            listDVOPayrollStypayod = new List<DVOPayrollStypayod>();
            #endregion UsingInAutoPayroll
        }

        #endregion Constructor

        #region Properties

        public int RowID
        {
            get { return _RowID; }
            set { _RowID = value; }
        }
        public int Doc_no
        {
            get { return _Doc_no; }
            set { _Doc_no = value; }
        }
        public string EmplCode
        {
            get { return _EmplCode; }
            set { _EmplCode = value; }
        }
        public string EmplSSN
        {
            get { return _EmplSSN; }
            set { _EmplSSN = value; }
        }
        public string TypeCode
        {
            get { return _Type_Code; }
            set { _Type_Code = value; }
        }
        public DateTime doc_date
        {
            get { return _doc_date; }
            set { _doc_date = value; }
        }
        public string StateTaxCode
        {
            get { return _StateTaxCode; }
            set { _StateTaxCode = value; }
        }

        public DateTime pay_date
        {
            get { return _pay_date; }
            set { _pay_date = value; }
        }

        public DateTime eop_date
        {
            get { return _eop_date; }
            set { _eop_date = value; }
        }
        public string print_check
        {
            get { return _print_check; }
            set { _print_check = value; }
        }
        public int Cash_acct_no
        {
            get { return _Cash_acct_no; }
            set { _Cash_acct_no = value; }
        }
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }


        public decimal? cash_amount
        {
            get { return _cash_amount; }
            set { _cash_amount = value; }
        }

        public int check_no
        {
            get { return _check_no; }
            set { _check_no = value; }
        }
        public decimal? inc_gross
        {
            get { return _inc_gross; }
            set { _inc_gross = value; }
        }
        public decimal? ded_fica
        {
            get { return _ded_fica; }
            set { _ded_fica = value; }
        }
        public decimal? inc_taxable
        {
            get { return _inc_taxable; }
            set { _inc_taxable = value; }
        }
        public decimal? ded_medicare
        {
            get { return _ded_medicare; }
            set { _ded_medicare = value; }
        }
        public decimal? ded_fedtax
        {
            get { return _ded_fedtax; }
            set { _ded_fedtax = value; }
        }
        public decimal? ded_statax
        {
            get { return _ded_statax; }
            set { _ded_statax = value; }
        }
        public decimal? ded_loctax
        {
            get { return _ded_loctax; }
            set { _ded_loctax = value; }
        }
        public decimal? ded_other
        {
            get { return _ded_other; }
            set { _ded_other = value; }
        }
        public decimal? obl_futa
        {
            get { return _obl_futa; }
            set { _obl_futa = value; }
        }
        public decimal? obl_fica
        {
            get { return _obl_fica; }
            set { _obl_fica = value; }
        }
        public decimal? obl_medicare
        {
            get { return _obl_medicare; }
            set { _obl_medicare = value; }
        }
        public decimal? obl_other
        {
            get { return _obl_other; }
            set { _obl_other = value; }
        }
        public decimal? obl_total
        {
            get { return _obl_total; }
            set { _obl_total = value; }
        }

        public decimal? inc_net
        {
            get { return _inc_net; }
            set { _inc_net = value; }
        }

        public decimal? inc_expense
        {
            get { return _inc_expense; }
            set { _inc_expense = value; }
        }
        public decimal? total_hours
        {
            get { return _total_hours; }
            set { _total_hours = value; }
        }
        public string ok_to_post
        {
            get { return _ok_to_post; }
            set { _ok_to_post = value; }
        }
        public string accrue_sick
        {
            get { return _accrue_sick; }
            set { _accrue_sick = value; }
        }
        public string accrue_vac
        {
            get { return _accrue_vac; }
            set { _accrue_vac = value; }
        }
        public string bonus
        {
            get { return _bonus; }
            set { _bonus = value; }
        }
        public string deposit
        {
            get { return _deposit; }
            set { _deposit = value; }
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


        public string ded_code
        {
            get { return _ded_code; }
            set { _ded_code = value; }
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
        public string PayPeriod
        {
            get { return _PayPeriod; }
            set { _PayPeriod = value; }
        }
        public string LastPay
        {
            get { return _LastPay; }
            set { _LastPay = value; }
        }
        public string stataxcode
        {
            get { return _stataxcode; }
            set { _stataxcode = value; }
        }
        public string keyvalue
        {
            get { return _kayvalue; }
            set { _kayvalue = value; }
        }
        public string empflexdeptaccttype
        {
            get { return _empflexdeptaccttype; }
            set { _empflexdeptaccttype = value; }
        }
        public DateTime start_date
        {
            get { return _start_date; }
            set { _start_date = value; }
        }
        public string EMail
        {
            get { return _email; }
            set { _email = value; }
        }
        public string District
        {
            get { return _District; }
            set { _District = value; }
        }
        #region UsingInAutoPayroll
        public List<DVOPayrollstypayid> ListDVOPayrollstypayid
        {
            get { return listDVOPayrollstypayid; }
            set { listDVOPayrollstypayid = value; }
        }
        public List<DVOPayrollstypaydd> ListDVOPayrollstypaydd
        {
            get { return listDVOPayrollstypaydd; }
            set { listDVOPayrollstypaydd = value; }
        }
        public List<DVOPayrollStypayod> ListDVOPayrollStypayod
        {
            get { return listDVOPayrollStypayod; }
            set { listDVOPayrollStypayod = value; }
        }
        #endregion UsingInAutoPayroll
        #endregion Properties

        #region Stored-Procedures

        public override string INSERT_SPNAME
        {
            get { return "USP_PPEmployeeIns"; }
        }

        public override string UPDATE_SPNAME
        {
            get { return "usppayreupd"; }
        }


        public override string FIND_SPNAME
        {
            get { return " "; }//uspDedComGet
        }

        public override string ALL_SPNAME
        {
            get { return ""; }
        }
        public override string TABLE_NAME
        {
            get { return "Process_PayEmployee"; }
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
        //Added by Sunil Pahwa
        public string FIND_CHECK_DOC_NO
        {
            get { return "uspvoidpayreget"; }
        }

        //****************************
        public string FIND_dup_ssn
        {
            get { return "usp_dup_ssn"; }
        }
        public string GET_STYPAYRE_DOC_NO
        {
            get { return "uspmanpaychkdocget"; }
        }
        public string GET_EMP_PY_STATUS
        {
            get { return "uspemppystts"; }
        }
        public string INSERT_STYPAYRE_PY_SCREEN
        {
            get { return "usppyins"; }
        }
        public string FIND_dup_flag
        {
            get { return "usp_dup_flag"; }
        }
        public string update_Process_PayEmployee
        {
            get { return "USP_PPEmployee_UPD"; }
        }
        public string update_Process_PayEmployee1
        {
            get { return "USP_PPEmployeeUpd1"; }
        }
        public string FIND_GEN_PAY_SLIP_DTL
        {
            get { return "uspgenpayslipdetl"; }
        }
        public string FIND_GEN_PAY_SLIP_DTL1
        {
            get { return "uspgenpayslipdetl1"; }
        }
        public string FIND_INSERT_Process_PayEmployee
        {
            get { return "USP_PPEmployeeIns"; }
        }
        public string FIND_INSERT_GeneratePensionProcess
        {
            get { return "GeneratePensionProcess"; }
        }
        public string GET_EMPL_NAME
        {
            get { return "uspempl_name"; }
        }


        public string GET_PAYSLIP_A4_HEADER
        {
            get { return "USP_PromptPaySlpA4"; }
        }
        public string GET_PAYSLIP_A4_DUPLICATE
        {
            get { return "USP_DuplPaySlpA4"; }//uspporptpayslpa4q  
        }
        public string GET_PAYSLIP_A4_INCOMES
        {
            get { return "USP_PrompPaySlpA4Inc"; }//uspporptpyslpa4inq  
        }
        public string GET_PAYSLIP_A4_INCOMES_DUPLICATE
        {
            get { return "USP_DuplPaySlpA4INC"; }//uspporptpyslpa4inq  
        }
        public string GET_PAYSLIP_A4_DEDUCTIONS
        {
            get { return "USP_PromptPaySlipA4Ded"; }//uspporptpyslpa4deq  
        }
        public string GET_PAYSLIP_A4_DEDUCTIONS_DUPLICATE
        {
            get { return "USP_DuplPaySlipA4Ded"; }//uspporptpyslpa4deq  
        }
        //*****************************************************************
        public string UPDATE_CHECK_NO
        {
            get { return "uspcheck_noupd"; }
        }
        public override string DELETE_SPNAME
        {
            get { return "USP_PayReDel"; }
        }
        public string GET_CASHKEY
        {
            get { return "uspcashacctget"; }
        }

        public string FIND_dup_ssn1
        {
            get { return "USP_DUP_SSN1"; }
        }
        public string FIND_dup_flag1
        {
            get { return "USP__Dup_Flag1"; }
        }

        public override string FIND_QUERY(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT empl_code,soc_sec_num,first_name,last_name,");
            sql.Append(" cash_acct,department,terminated,pay_period,allowances,");
            sql.Append(" state_allow,marital_stat,vac_code,vac_allowed,vac_used,sick_code,sick_allowed,sick_used,last_pay,hold_pymnt,");
            sql.Append(" statax_code,loctax_code,");
            sql.Append(" dir_dept,flexdeptaccttype,last_inc_date,");
            sql.Append(" RowID");
            sql.Append(" FROM MasterEmployee");
            if (Convert.ToBoolean(parameters[9]) == true)
            {
                sql.Append(", EmployeeTCard_Header");
            }

            sql.Append(" WHERE 1=1");

            if (Convert.ToInt32(parameters[0]) > 0)
                sql.Append(" AND RowID = " + parameters[0].ToString());
            if (parameters[1] != null)
                if (parameters[1].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(empl_code) = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[2] != null)
                if (parameters[2].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(soc_sec_num) = '" + parameters[2].ToString().Trim().Replace("'", "''") + "'");
            if (parameters[3] != null)
                if (parameters[5].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(first_name) LIKE  '" + parameters[3].ToString().Trim().Replace("'", "''") + "%'");

            if (parameters[4].ToString() != string.Empty)
                sql.Append(" AND Rtrim(last_name) LIKE  '" + parameters[4].ToString().Trim().Replace("'", "''") + "%'");
            if (Convert.ToInt32(parameters[5]) > 0)
                sql.Append(" AND Rtrim(type_code) = " + parameters[5].ToString());
            if (parameters[6] != null)
                if (parameters[6].ToString() != string.Empty && Convert.ToDateTime(parameters[6].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND Rtrim(job_code) = '" + parameters[6].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[7] != null)
                if (parameters[7].ToString() != string.Empty)
                    sql.Append(" AND Rtrim(job_title) = '" + parameters[7].ToString().Replace("'", "''") + "'");
            if (parameters[8] != null)
                if (parameters[8].ToString() != string.Empty && Convert.ToDateTime(parameters[8].ToString()) != Convert.ToDateTime("01/01/1900"))
                    sql.Append(" AND pay_period = '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");

            return sql.ToString();
        }


        public string FIND_UPE(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select MasterEmployee.first_name,MasterEmployee.middle_name, MasterEmployee.last_name,");
            sql.Append(" MasterEmployee.last_pay,MasterEmployee.pay_period,Process_PayEmployee.empl_code,");
            sql.Append(" Process_PayEmployee.pay_date, Process_PayEmployee.eop_date,PayrollGLAccounts.keyvalue,");
            sql.Append(" Process_PayEmployee.print_check, Process_PayEmployee.check_no,Process_PayEmployee.ok_to_post,");
            sql.Append(" Process_PayEmployee.doc_no, Process_PayEmployee.cash_amount, Process_PayEmployee.deposit,");
            sql.Append(" Process_PayEmployee.inc_gross, Process_PayEmployee.inc_taxable, Process_PayEmployee.ded_fica,");
            sql.Append(" Process_PayEmployee.ded_medicare, Process_PayEmployee.ded_fedtax, Process_PayEmployee.ded_statax,");
            sql.Append(" Process_PayEmployee.ded_loctax, Process_PayEmployee.ded_other, Process_PayEmployee.obl_futa,");
            sql.Append(" Process_PayEmployee.obl_fica, Process_PayEmployee.obl_medicare, Process_PayEmployee.obl_other,");
            sql.Append(" Process_PayEmployee.obl_total, Process_PayEmployee.inc_net, Process_PayEmployee.inc_expense,Process_PayEmployee.total_hours,");
            sql.Append(" Process_PayEmployee.accrue_sick,Process_PayEmployee.accrue_vac,Process_PayEmployee.bonus,Process_PayEmployee.PayProcess_ID,doc_date,MasterEmployee.soc_sec_num,MasterEmployee.statax_code");
            sql.Append(" ,MasterEmployee.flexdeptaccttype");
            sql.Append(" from MasterEmployee,Process_PayEmployee LEFT outer JOIN  PayrollGLAccounts ON Process_PayEmployee.cash_acct_no=PayrollGLAccounts.acct_no");
            sql.Append(" where MasterEmployee.empl_code=Process_PayEmployee.empl_code");


            if (parameters[0] != null)
                if (parameters[0].ToString().Trim() != string.Empty)
                    sql.Append(" and Process_PayEmployee.empl_code= '" + parameters[0].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[1] != null && parameters[1].ToString().Trim().Length > 0)
                if (parameters[1].ToString().Trim() != Convert.ToDateTime(null).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture))
                    sql.Append(" and Process_PayEmployee.pay_date= '" + parameters[1].ToString().Trim() + "'");

            if (parameters[2] != null && parameters[1].ToString().Trim().Length > 0)
                if (parameters[2].ToString().Trim() != Convert.ToDateTime(null).ToString(DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture))
                    sql.Append(" and Process_PayEmployee.eop_date='" + parameters[2].ToString().Trim() + "'");

            if (parameters[3] != null)
                if (parameters[3].ToString().Trim() != string.Empty)
                    sql.Append(" and Process_PayEmployee.print_check= '" + parameters[3].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[4] != null)
                if (Convert.ToInt32(parameters[4]) > 0)
                    sql.Append(" and Process_PayEmployee.check_no= " + Convert.ToInt32(parameters[4]));

            if (parameters[5] != null)
                if (parameters[5].ToString().Trim() != string.Empty)
                    sql.Append(" and Process_PayEmployee.ok_to_post= '" + parameters[5].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[6] != null)
                if (Convert.ToInt32(parameters[6]) > 0)
                    sql.Append(" and Process_PayEmployee.doc_no= " + Convert.ToInt32(parameters[6]));

            if (parameters[7] != null)
                if (Convert.ToDecimal(parameters[7]) > 0)
                    sql.Append(" and Process_PayEmployee.cash_amount= " + Convert.ToDecimal(parameters[7]));

            if (parameters[8] != null)
                if (parameters[8].ToString().Trim() != string.Empty)
                    sql.Append(" and Process_PayEmployee.deposit= '" + parameters[8].ToString().Trim().Replace("'", "''") + "'");

            if (parameters[9] != null)
                if (Convert.ToDecimal(parameters[9]) > 0)
                    sql.Append(" and Process_PayEmployee.inc_gross= " + Convert.ToDecimal(parameters[9]));

            if (parameters[10] != null)
                if (Convert.ToDecimal(parameters[10]) > 0)
                    sql.Append(" and Process_PayEmployee.inc_taxable= " + Convert.ToDecimal(parameters[10]));

            if (parameters[11] != null)
                if (Convert.ToDecimal(parameters[11]) > 0)
                    sql.Append(" and Process_PayEmployee.ded_fica= " + Convert.ToDecimal(parameters[11]));

            if (parameters[12] != null)
                if (Convert.ToDecimal(parameters[12]) > 0)
                    sql.Append(" and Process_PayEmployee.obl_futa=" + Convert.ToDecimal(parameters[12]));

            if (parameters[13] != null)
                if (Convert.ToDecimal(parameters[13]) > 0)
                    sql.Append(" and Process_PayEmployee.ded_medicare=" + Convert.ToDecimal(parameters[13]));

            if (parameters[14] != null)
                if (Convert.ToDecimal(parameters[14]) > 0)
                    sql.Append(" and Process_PayEmployee.obl_fica=" + Convert.ToDecimal(parameters[14]));

            if (parameters[15] != null)
                if (Convert.ToDecimal(parameters[15]) > 0)
                    sql.Append(" and Process_PayEmployee.ded_fedtax= " + Convert.ToDecimal(parameters[15]));

            if (parameters[16] != null)
                if (Convert.ToDecimal(parameters[16]) > 0)
                    sql.Append(" and Process_PayEmployee.obl_medicare=" + Convert.ToDecimal(parameters[16]));

            if (parameters[17] != null)
                if (Convert.ToDecimal(parameters[17]) > 0)
                    sql.Append(" and Process_PayEmployee.ded_statax=" + Convert.ToDecimal(parameters[17]));

            if (parameters[18] != null)
                if (Convert.ToDecimal(parameters[18]) > 0)
                    sql.Append(" and Process_PayEmployee.obl_other= " + Convert.ToDecimal(parameters[18]));

            if (parameters[19] != null)
                if (Convert.ToDecimal(parameters[19]) > 0)
                    sql.Append(" and Process_PayEmployee.ded_loctax= " + Convert.ToDecimal(parameters[19]));

            if (parameters[20] != null)
                if (Convert.ToDecimal(parameters[20]) > 0)
                    sql.Append(" and Process_PayEmployee.ded_other= " + Convert.ToDecimal(parameters[20]));

            if (parameters[21] != null)
                if (Convert.ToDecimal(parameters[21]) > 0)
                    sql.Append(" and Process_PayEmployee.obl_total= " + Convert.ToDecimal(parameters[21]));

            if (parameters[22] != null)
                if (Convert.ToDecimal(parameters[22]) > 0)
                    sql.Append(" and Process_PayEmployee.inc_net= " + Convert.ToDecimal(parameters[22]));

            if (parameters[23] != null)
                if (Convert.ToDecimal(parameters[23]) > 0)
                    sql.Append(" and Process_PayEmployee.inc_expense= " + Convert.ToDecimal(parameters[23]));

            if (parameters[24] != null)
                if (Convert.ToDecimal(parameters[24]) > 0)
                    sql.Append(" and Process_PayEmployee.total_hours= " + Convert.ToDecimal(parameters[24]));

            return sql.ToString();
        }
        public string GetListofPersonstoProcess(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select MasterEmployee.address1,MasterEmployee.address2,   MasterEmployee.city,");
            sql.Append(" MasterEmployee.first_name, MasterEmployee.last_name,MasterEmployee.middle_name,");
            sql.Append(" MasterEmployee.soc_sec_num,MasterEmployee.state,MasterEmployee.zip,Process_PayEmployee.cash_amount,");
            sql.Append(" Process_PayEmployee.check_no,Process_PayEmployee.department,Process_PayEmployee.doc_date,");
            sql.Append(" Process_PayEmployee.doc_no,Process_PayEmployee.empl_code,Process_PayEmployee.eop_date,");
            sql.Append(" Process_PayEmployee.pay_date,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,MasterEmployee.mailid from MasterEmployee, Process_PayEmployee,PayrollGLAccounts ");
            sql.Append(" where MasterEmployee.empl_code = Process_PayEmployee.empl_code  ");
            sql.Append(" and PayrollGLAccounts.acct_no =Process_PayEmployee.cash_acct_no ");
            if (parameters[1].ToString().Trim() != "0")
            {
                sql.Append(" and Process_PayEmployee.cash_acct_no = " + parameters[1].ToString());
            }
            if (parameters[2].ToString().Trim() != string.Empty)
            {
                sql.Append(" and MasterEmployee.type_code LIKE  '%" + parameters[2].ToString() + "%'");
            }
            sql.Append(" and Process_PayEmployee.ok_to_post = 'Y' ");
            if (parameters[0].ToString().Trim() == "Y")
            {
                sql.Append(" AND deposit = 'Y'");
                sql.Append(" and Process_PayEmployee.print_check ='N' ");
            }
            else
            {
                sql.Append(" AND deposit = 'N'");
                sql.Append(" and Process_PayEmployee.print_check ='Y' ");
            }

            if (parameters[3] != null)
                if (parameters[3].ToString().Trim() != string.Empty)
                    sql.Append(" AND MasterEmployee.SelectDistrict IN (" + parameters[3].ToString() + ")");

            return sql.ToString();
        }
        //Added By Rahul jain on 16/Jan/2010 Getting All Employee Incomes
        public string GetListofPersonsIncome(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stypayid.line_no,stypayid.inc_code, ");
            sql.Append(" stypayid.number,stypayid.hours, ");
            sql.Append(" stypayid.amount,stypayid.amount,stypayid.doc_no,Process_PayEmployee.empl_code ");
            sql.Append(" FROM stypayid ,Process_PayEmployee,MasterEmployee ");
            sql.Append(" WHERE stypayid.doc_no = Process_PayEmployee.doc_no ");
            sql.Append(" AND Process_PayEmployee.empl_code=Process_PayEmployee.empl_code ");
            if (parameters[1].ToString().Trim() != "0")
            {
                sql.Append(" and Process_PayEmployee.cash_acct_no = " + parameters[1].ToString());
            }
            if (parameters[2].ToString().Trim() != string.Empty)
            {
                sql.Append(" and MasterEmployee.type_code LIKE  '%" + parameters[2].ToString() + "%'");
            }
            sql.Append(" and Process_PayEmployee.ok_to_post = 'Y' ");
            if (parameters[0].ToString().Trim() == "Y")
            {
                sql.Append(" AND deposit = 'Y'");
                sql.Append(" and Process_PayEmployee.print_check ='N' ");
            }
            else
            {
                sql.Append(" AND deposit = 'N'");
                sql.Append(" and Process_PayEmployee.print_check ='Y' ");
            }
            sql.Append(" UNION ");
            sql.Append(" SELECT MasterEmployeeIncomes.line_no,MasterEmployeeIncomes.inc_code,	0 number,0 hours,0 amount,MasterEmployeeIncomes.inc_ytd,0 doc_no,MasterEmployeeIncomes.empl_code FROM MasterEmployeeIncomes WHERE MasterEmployeeIncomes.empl_code IN(");
            sql.Append(" select Process_PayEmployee.empl_code ");
            sql.Append("  from MasterEmployee, Process_PayEmployee ");
            sql.Append(" where MasterEmployee.empl_code = Process_PayEmployee.empl_code  ");
            if (parameters[1].ToString().Trim() != "0")
            {
                sql.Append(" and Process_PayEmployee.cash_acct_no = " + parameters[1].ToString());
            }
            if (parameters[2].ToString().Trim() != string.Empty)
            {
                sql.Append(" and MasterEmployee.type_code LIKE  '%" + parameters[2].ToString() + "%'");
            }
            sql.Append(" and Process_PayEmployee.ok_to_post = 'Y' ");
            if (parameters[0].ToString().Trim() == "Y")
            {
                sql.Append(" AND deposit = 'Y'");
                sql.Append(" and Process_PayEmployee.print_check ='N' ");
            }
            else
            {
                sql.Append(" AND deposit = 'N'");
                sql.Append(" and Process_PayEmployee.print_check ='Y' ");
            }
            sql.Append(")");
            sql.Append(" UNION ");
            sql.Append(" SELECT stypayid.line_no,stypayid.inc_code,	stypayid.number,stypayid.hours,	stypayid.amount,0 inc_ytd,stypayid.doc_no, '' empl_code FROM stypayid WHERE rowid=0 ");

            return sql.ToString();
        }

        //Added By Rahul jain on 16/Jan/2010 Getting All Employee Deductions
        public string GetListofPersonsDeduction(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT stypaydd.line_no,stypaydd.ded_code,stypaydd.amount,stypaydd.amount ");
            sql.Append(" stypayid.number,stypayid.hours, ");
            sql.Append(" stypayid.amount,stypayid.amount,stypayid.doc_no,Process_PayEmployee.empl_code ");
            sql.Append(" FROM stypayid ,Process_PayEmployee,MasterEmployee ");
            sql.Append(" WHERE stypayid.doc_no = Process_PayEmployee.doc_no ");
            sql.Append(" AND Process_PayEmployee.empl_code=Process_PayEmployee.empl_code ");
            if (parameters[1].ToString().Trim() != "0")
            {
                sql.Append(" and Process_PayEmployee.cash_acct_no = " + parameters[1].ToString());
            }
            if (parameters[2].ToString().Trim() != string.Empty)
            {
                sql.Append(" and MasterEmployee.type_code LIKE  '%" + parameters[2].ToString() + "%'");
            }
            sql.Append(" and Process_PayEmployee.ok_to_post = 'Y' ");
            if (parameters[0].ToString().Trim() == "Y")
            {
                sql.Append(" AND deposit = 'Y'");
                sql.Append(" and Process_PayEmployee.print_check ='N' ");
            }
            else
            {
                sql.Append(" AND deposit = 'N'");
                sql.Append(" and Process_PayEmployee.print_check ='Y' ");
            }
            sql.Append(" UNION ");
            sql.Append(" SELECT MasterEmployeeIncomes.line_no,MasterEmployeeIncomes.inc_code,	0 number,0 hours,0 amount,MasterEmployeeIncomes.inc_ytd,0 doc_no,MasterEmployeeIncomes.empl_code FROM MasterEmployeeIncomes WHERE MasterEmployeeIncomes.empl_code IN(");
            sql.Append(" select Process_PayEmployee.empl_code ");
            sql.Append("  from MasterEmployee, Process_PayEmployee ");
            sql.Append(" where MasterEmployee.empl_code = Process_PayEmployee.empl_code  ");
            if (parameters[1].ToString().Trim() != "0")
            {
                sql.Append(" and Process_PayEmployee.cash_acct_no = " + parameters[1].ToString());
            }
            if (parameters[2].ToString().Trim() != string.Empty)
            {
                sql.Append(" and MasterEmployee.type_code LIKE  '%" + parameters[2].ToString() + "%'");
            }
            sql.Append(" and Process_PayEmployee.ok_to_post = 'Y' ");
            if (parameters[0].ToString().Trim() == "Y")
            {
                sql.Append(" AND deposit = 'Y'");
                sql.Append(" and Process_PayEmployee.print_check ='N' ");
            }
            else
            {
                sql.Append(" AND deposit = 'N'");
                sql.Append(" and Process_PayEmployee.print_check ='Y' ");
            }
            sql.Append(")");
            sql.Append(" UNION ");
            sql.Append(" SELECT stypayid.line_no,stypayid.inc_code,	stypayid.number,stypayid.hours,	stypayid.amount,0 inc_ytd,stypayid.doc_no, '' empl_code FROM stypayid WHERE rowid=0 ");

            return sql.ToString();
        }

        public string FINND_PayrollCheck(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select MasterEmployee.address1,MasterEmployee.address2,   MasterEmployee.city,");
            sql.Append(" MasterEmployee.first_name, MasterEmployee.last_name,MasterEmployee.middle_name,");
            sql.Append(" MasterEmployee.soc_sec_num,MasterEmployee.state,MasterEmployee.zip,Process_PayEmployee.cash_amount,");
            sql.Append(" Process_PayEmployee.check_no,Process_PayEmployee.department,Process_PayEmployee.doc_date,");
            sql.Append(" Process_PayEmployee.doc_no,Process_PayEmployee.empl_code,Process_PayEmployee.eop_date,");
            sql.Append(" Process_PayEmployee.pay_date,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc,MasterEmployee.ApplicationReferenceNo from MasterEmployee,Process_PayEmployee,PayrollGLAccounts ");
            sql.Append(" where MasterEmployee.empl_code = Process_PayEmployee.empl_code  ");
            sql.Append(" and PayrollGLAccounts.acct_no =Process_PayEmployee.cash_acct_no ");
            sql.Append(" and Process_PayEmployee.doc_no not in (select  Paydocno  from PayrollChecks) ");
            if (parameters[1].ToString().Trim() != "0")
            {
                sql.Append(" and Process_PayEmployee.cash_acct_no = " + parameters[1].ToString());
            }
            if (parameters[2].ToString().Trim() != string.Empty)
            {
                sql.Append(" and MasterEmployee.type_code LIKE  '%" + parameters[2].ToString() + "%'");
            }
            sql.Append(" and Process_PayEmployee.ok_to_post = 'Y' ");
            if (parameters[0].ToString().Trim() == "Y")
            {
                sql.Append(" AND deposit = 'Y'");
                sql.Append(" and Process_PayEmployee.print_check ='N' ");
            }
            else
            {
                sql.Append(" AND deposit = 'N'");
                sql.Append(" and Process_PayEmployee.print_check ='Y' ");
            }
            return sql.ToString();
        }

        public string FINND_PayrollCheckWithDepartment(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select MasterEmployee.address1,MasterEmployee.address2,   MasterEmployee.city,");
            sql.Append(" MasterEmployee.first_name, MasterEmployee.last_name,MasterEmployee.middle_name,");
            sql.Append(" MasterEmployee.soc_sec_num,MasterEmployee.state,MasterEmployee.zip,Process_PayEmployee.cash_amount,");
            sql.Append(" Process_PayEmployee.check_no,Process_PayEmployee.department,Process_PayEmployee.doc_date,");
            sql.Append(" Process_PayEmployee.doc_no,Process_PayEmployee.empl_code,Process_PayEmployee.eop_date,");
            sql.Append(" Process_PayEmployee.pay_date,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc from MasterEmployee,Process_PayEmployee,PayrollGLAccounts ");
            sql.Append(" where MasterEmployee.empl_code = Process_PayEmployee.empl_code  ");
            sql.Append(" and PayrollGLAccounts.acct_no =Process_PayEmployee.cash_acct_no ");
            sql.Append(" and Process_PayEmployee.doc_no not in (select  Paydocno  from PayrollChecks) ");
            if (parameters[1].ToString().Trim() != "0")
            {
                sql.Append(" and Process_PayEmployee.cash_acct_no = " + parameters[1].ToString());
            }
            if (parameters[2].ToString().Trim() != string.Empty)
            {
                sql.Append(" and MasterEmployee.type_code LIKE  '%" + parameters[2].ToString() + "%'");
            }
            sql.Append(" and Process_PayEmployee.ok_to_post = 'Y' ");
            if (parameters[0].ToString().Trim() == "Y")
            {
                sql.Append(" AND deposit = 'Y'");
                sql.Append(" and Process_PayEmployee.print_check ='N' ");
            }
            else
            {
                sql.Append(" AND deposit = 'N'");
                sql.Append(" and Process_PayEmployee.print_check ='Y' ");
            }
            return sql.ToString();
        }

        public string FIND_DUPLICATION_PayrollCheck(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select MasterEmployee.address1,MasterEmployee.address2,   MasterEmployee.city,");
            sql.Append(" MasterEmployee.first_name, MasterEmployee.last_name,MasterEmployee.middle_name,");
            sql.Append(" MasterEmployee.soc_sec_num,MasterEmployee.state,MasterEmployee.zip,Process_PayEmployee.cash_amount,");
            sql.Append(" Process_PayEmployee.check_no,Process_PayEmployee.department,Process_PayEmployee.doc_date,");
            sql.Append(" Process_PayEmployee.doc_no,Process_PayEmployee.empl_code,Process_PayEmployee.eop_date,");
            sql.Append(" Process_PayEmployee.pay_date ,PayrollGLAccounts.keyvalue,PayrollGLAccounts.acct_desc from MasterEmployee, Process_PayEmployee,PayrollGLAccounts ");
            sql.Append(" where MasterEmployee.empl_code = Process_PayEmployee.empl_code  ");
            sql.Append(" and PayrollGLAccounts.acct_no =Process_PayEmployee.cash_acct_no ");
            sql.Append(" and Process_PayEmployee.doc_no in (select apdocno from apchecksrecord) ");
            sql.Append(" and Process_PayEmployee.ok_to_post IN ('Y','P') ");
            sql.Append(" AND deposit = 'N'");
            sql.Append(" and Process_PayEmployee.print_check ='N' ");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0 && !parameters[0].ToString().Trim().Contains("1900") && !parameters[0].ToString().Trim().Contains("0001"))
                {
                    sql.Append(" and Process_PayEmployee.pay_date = '" + parameters[0].ToString().Trim() + "'");
                }
            if (parameters[1] != null)
                if (parameters[1].ToString().Trim().Length > 0)
                {
                    sql.Append(" and MasterEmployee.empl_code = '" + parameters[1].ToString().Trim().Replace("'", "''") + "'");
                }
            return sql.ToString();
        }

        public string Update_Postedstatus
        {
            get { return "update Process_payemployee set ok_to_post ='P' where ok_to_post ='Y' DELETE FROM [dbo].[MasterEmployeeIncomes] WHERE inc_code = (SELECT inc_code FROM [dbo].[MasterIncCodes] WHERE [description] = 'Arrear')"; }
        }

        public string Update_PostedstatusByDistrict(string District)
        {
            string param = string.Empty;
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(District))
            {
                sql.Append(" update Process_payemployee set ok_to_post ='P' where ok_to_post ='Y'");
                sql.Append(" and empl_code in (select me.Empl_Code from masteremployee me");
                sql.Append(" join MasterEmpBankDetails ebd on me.Empl_Code=ebd.empl_code");
                sql.Append(" where me.selectdistrict='" + District.Trim() + "' ");
                sql.Append(" and ebd.ACCOUNT_STATUS='ACTIVE') ");

                sql.Append(" DELETE FROM [dbo].[MasterEmployeeIncomes]");
                sql.Append(" WHERE inc_code = (SELECT inc_code FROM [dbo].[MasterIncCodes] WHERE [description] = 'Arrear')");
                sql.Append(" and empl_code in (select me.Empl_Code from masteremployee me");
                sql.Append(" join MasterEmpBankDetails ebd on me.Empl_Code=ebd.empl_code");
                sql.Append(" where me.selectdistrict='" + District.Trim() + "' ");
                sql.Append(" and ebd.ACCOUNT_STATUS='ACTIVE') ");
            }
            else
            {
                sql.Append("update Process_payemployee set ok_to_post ='P' where ok_to_post ='Y' DELETE FROM [dbo].[MasterEmployeeIncomes] WHERE inc_code = (SELECT inc_code FROM [dbo].[MasterIncCodes] WHERE [description] = 'Arrear')");
            }

            return sql.ToString();
        }


        public string Update_Cancelstatus
        {

            //get { return "DELETE from Process_DirectDeposit_Header where doc_no IN ( select doc_no from Process_DirectDeposit_Details where pay_doc_no in( select doc_no from Process_PayEmployee where ok_to_post='Y')); DELETE from Process_DirectDeposit_Details where doc_no IN (select doc_no from Process_DirectDeposit_Details where pay_doc_no in( select doc_no from Process_PayEmployee where ok_to_post='Y'));update Process_payemployee set ok_to_post ='C' where ok_to_post IN('Y','N'); "; }
            // Added by sujeet 30/10/2023. Check with window from. In the window form only update the table.
            get { return "update Process_payemployee set ok_to_post ='C' where ok_to_post IN('Y','N'); DELETE FROM [dbo].[MasterEmployeeIncomes] WHERE inc_code = (SELECT inc_code FROM [dbo].[MasterIncCodes] WHERE [description] = 'Arrear')"; }
        }

        public string Update_Cancelstatus1(ref Object[] parameters)
        {
            //get { return "update Process_payemployee set ok_to_post ='C' where ok_to_post IN('Y','N') AND "; }
            string param = string.Empty;
            StringBuilder sql = new StringBuilder();
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                {
                    param = " and Process_payemployee.empl_code IN (" + parameters[0].ToString().Trim() + ") ));";
                }
                else
                    param = "  ));";
            sql.Append(" DELETE from Process_DirectDeposit_Header where doc_no IN ( select doc_no from Process_DirectDeposit_Details where pay_doc_no in( select doc_no from Process_PayEmployee where ok_to_post='Y' " + param);
            sql.Append(" DELETE from Process_DirectDeposit_Details where doc_no IN (select doc_no from Process_DirectDeposit_Details where pay_doc_no in( select doc_no from Process_PayEmployee where ok_to_post='Y' " + param);
            sql.Append(" UPDATE Process_payemployee set ok_to_post ='C' where ok_to_post IN('Y','N') ");
            if (parameters[0] != null)
                if (parameters[0].ToString().Trim().Length > 0)
                {
                    sql.Append(" and Process_payemployee.empl_code IN (" + parameters[0].ToString().Trim() + ")");
                }

            return sql.ToString();
        }
        public string FINND_PayrollEmp(ref Object[] parameters)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT DISTINCT Process_PayEmployee.empl_code,ISNULL(MasterEmployee.first_name,'')+' '+ISNULL(MasterEmployee.middle_name,'')+' '+ISNULL(MasterEmployee.last_name,'') +' - '+ISNULL(MasterEmployee.PresentDistrict,'')+' - '+ISNULL(MasterEmployee.PresentTehsil,'') Pensioner ");
            sql.Append(" FROM MasterEmployee INNER JOIN Process_PayEmployee ON MasterEmployee.Empl_Code =Process_PayEmployee.empl_code ");
            sql.Append(" WHERE Process_PayEmployee.ok_to_post IN('Y','P') ");
            return sql.ToString();
        }
        #endregion Stored-Procedures
    }
}
