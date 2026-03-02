using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class PayrollProcess_PayEmployee_Model
  {
    public string vac_accr_code { get; set; }
    public int vac_accr_ctr { get; set; }
    public DateTime vac_lapse_date { get; set; }
    public string sick_accr_code { get; set; }
    public int sick_accr_ctr { get; set; }
    public DateTime sick_lapse_date { get; set; }
    public int allowances { get; set; }

    public string accrue_sick { get; set; }
    public string sick_allowed { get; set; }
    public int sick_code { get; set; }
    public decimal? sick_used { get; set; }
    public string emp_department { get; set; }
    public int emp_cash_acct { get; set; }

    public string accrue_vac { get; set; }
    public decimal? vac_allowed { get; set; }
    public string terminated { get; set; }
    public string vac_code { get; set; }
    public decimal? vac_used { get; set; }
    public string ALL_SPNAME { get; set; }
    public string bonus { get; set; }
    public int Cash_acct_no { get; set; }
    public decimal? cash_amount { get; set; }
    public int check_no { get; set; }
    public string ded_code { get; set; }
    public decimal? ded_fedtax { get; set; }
    public decimal? ded_fica { get; set; }
    public decimal? ded_loctax { get; set; }
    public decimal? ded_medicare { get; set; }
    public decimal? ded_other { get; set; }
    public decimal? ded_statax { get; set; }
    public string DELETE_SPNAME { get; set; }
    public string Department { get; set; }
    public string deposit { get; set; }
    public DateTime doc_date { get; set; }
    public int Doc_no { get; set; }
    public string EMail { get; set; }
    public string empflexdeptaccttype { get; set; }
    public string EmplCode { get; set; }
    public string EmplSSN { get; set; }
    public DateTime eop_date { get; set; }
    public string FIND_CHECK_DOC_NO { get; set; }
    public string FIND_dup_flag { get; set; }
    public string FIND_dup_flag1 { get; set; }
    public string FIND_dup_ssn { get; set; }
    public string FIND_dup_ssn1 { get; set; }
    public string FIND_GEN_PAY_SLIP_DTL { get; set; }
    public string FIND_GEN_PAY_SLIP_DTL1 { get; set; }
    public string FIND_INSERT_Process_PayEmployee { get; set; }
    public string FIND_SPNAME { get; set; }
    public string FirstName { get; set; }
    public string GET_CASHKEY { get; set; }
    public string GET_EMP_PY_STATUS { get; set; }
    public string GET_EMPL_NAME { get; set; }
    public string GET_PAYSLIP_A4_DEDUCTIONS { get; set; }
    public string GET_PAYSLIP_A4_DEDUCTIONS_DUPLICATE { get; set; }
    public string GET_PAYSLIP_A4_DUPLICATE { get; set; }
    public string GET_PAYSLIP_A4_HEADER { get; set; }
    public string GET_PAYSLIP_A4_INCOMES { get; set; }
    public string GET_PAYSLIP_A4_INCOMES_DUPLICATE { get; set; }
    public string GET_STYPAYRE_DOC_NO { get; set; }
    public decimal? inc_expense { get; set; }
    public decimal? inc_gross { get; set; }
    public decimal? inc_net { get; set; }
    public decimal? inc_taxable { get; set; }
    public string INSERT_SPNAME { get; set; }
    public string INSERT_STYPAYRE_PY_SCREEN { get; set; }
    public int InsertBy { get; set; }
    public string InsertDate { get; set; }
    public string InsertMachineInfo { get; set; }
    public string keyvalue { get; set; }
    public string LastName { get; set; }
    public DateTime? LastPay { get; set; }
    public List<PayrollstypayddModel> ListDVOPayrollstypaydd { get; set; }
    public List<Payrollstypayid> ListDVOPayrollstypayid { get; set; }
    public List<PayrollStypayod> ListDVOPayrollStypayod { get; set; }
    public string MiddleName { get; set; }
    public  string NOTES_TABLE_RECORD_ID { get; set; }
    public decimal? obl_fica { get; set; }
    public decimal? obl_futa { get; set; }
    public decimal? obl_medicare { get; set; }
    public decimal? obl_other { get; set; }
    public decimal? obl_total { get; set; }
    public string ok_to_post { get; set; }
    public DateTime pay_date { get; set; }
    public string PayPeriod { get; set; }
    public string print_check { get; set; }
    public int RowID { get; set; }
    public DateTime start_date { get; set; }
    public string stataxcode { get; set; }
    public string StateTaxCode { get; set; }
    public string TABLE_NAME { get; set; }
    public decimal? total_hours { get; set; }
    public string TypeCode { get; set; }
    public int UNIQUE_ID { get; set; }
    public string Update_Cancelstatus { get; set; }
    public string UPDATE_CHECK_NO { get; set; }
    public string Update_Postedstatus { get; set; }
    public string update_Process_PayEmployee { get; set; }
    public string update_Process_PayEmployee1 { get;set;  }
    public string UPDATE_SPNAME { get; set; }
    public int UpdateBy { get; set; }
    public string UpdateDate { get; set; }
    public string UpdateMachineInfo { get; set; }

    //public string FIND_DUPLICATION_PayrollCheck(ref object[] parameters);
    //public override string FIND_QUERY(ref object[] parameters);
    //public string FIND_UPE(ref object[] parameters);
    //public string FINND_PayrollCheck(ref object[] parameters);
    //public string FINND_PayrollCheckWithDepartment(ref object[] parameters);
    //public string GetListofPersonsDeduction(ref object[] parameters);
    //public string GetListofPersonsIncome(ref object[] parameters);
    //public string GetListofPersonstoProcess(ref object[] parameters);
  }
}
