using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App.Data.Entities
{
  [Table("MasterEmployee")]
  public class MasterEmployee
  {
    [Key]
    public long EmployeeID { get; set; }
    public string Empl_Code { get; set; }
    public string Soc_Sec_Num { get; set; }
    public string Type_Code { get; set; }
    public DateTime? birthdate { get; set; }
    public string first_name { get; set; }
    public string middle_name { get; set; }
    public string last_name { get; set; }
    public string address1 { get; set; }
    public string address2 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zip { get; set; }
    public string phone { get; set; }
    public int? cash_acct { get; set; }
    public string department { get; set; }
    public string job_code { get; set; }
    public string job_title { get; set; }
    public DateTime? date_hired { get; set; }
    public DateTime? terminated { get; set; }
    public string empl_status { get; set; }
    public string pay_period { get; set; }
    public int? allowances { get; set; }
    public int? state_allow { get; set; }
    public string marital_stat { get; set; }
    public string vac_code { get; set; }
    public decimal? vac_allowed { get; set; }
    public decimal? vac_used { get; set; }
    public string sick_code { get; set; }
    public decimal? sick_allowed { get; set; }
    public decimal? sick_used { get; set; }
    public DateTime? last_pay { get; set; }
    public string hold_pymnt { get; set; }
    public string statax_code { get; set; }
    public string loctax_code { get; set; }
    public string sick_accr_code { get; set; }
    public int? sick_accr_ctr { get; set; }
    public DateTime? sick_lapse_date { get; set; }
    public string vac_accr_code { get; set; }
    public int? vac_accr_ctr { get; set; }
    public DateTime? vac_lapse_date { get; set; }
    public string dir_dept { get; set; }
    public int? dfi_dest { get; set; }
    public short? chk_digit { get; set; }
    public string bank_acct_no { get; set; }
    public decimal? state_udf { get; set; }
    public string flexdeptaccttype { get; set; }
    public DateTime? last_inc_date { get; set; }
    public DateTime? appoint_date { get; set; }
    public string gender { get; set; }
    public string mailid { get; set; }
    public string Prefix { get; set; }
    public string Suffix { get; set; }
    public string MaidenName { get; set; }
    public string Nationality { get; set; }
    public string PostalAddress { get; set; }
    public string PhoneOffice { get; set; }
    public string Mobile { get; set; }
    public int? EmployerID { get; set; }
    public string PensionerType { get; set; }
    public string PersonID { get; set; }
    public decimal? AnnualSalary { get; set; }
    public string OldPersonID { get; set; }
    public string ApplicationReferenceNo { get; set; }
    public string SubmissionLocation { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public string AppliedBy { get; set; }
    public string SelectTehsilSocialWelfareOffice_TSWO { get; set; }
    public string SelectDistrict { get; set; }
    public string Age_InYears { get; set; }
    public string DoYouHaveBPLcard { get; set; }
    public string FatherOrHusbandOrGuardianName { get; set; }
    public string EMail { get; set; }
    public string Category { get; set; }
    public string PercentageofDisability { get; set; }
    public string AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP { get; set; }
    public string ApplicationSanctionedunderSchemeName { get; set; }
    public string CurrentTask { get; set; }
    public string CurrentStatus { get; set; }
    public string LastTask { get; set; }
    public string VersionNo { get; set; }
    public string PresentDistrict { get; set; }
    public string PresentVillageName { get; set; }
    public string PresentHalqaPanchayatOrMunicipalityName { get; set; }
    public string PresentTehsil { get; set; }
    public string PresentAddress { get; set; }
    public string PermanentAddress { get; set; }
    public string PermanentDistrict { get; set; }
    public string PermanentTehsil { get; set; }
    public string PermanentHalqaPanchayatOrMunicipalityName { get; set; }
    public string PermanentVillageName { get; set; }
    public string CivilCondition { get; set; }
    public string ReasonForChange { get; set; }
    public string Last_pay_date { get; set; }
    public string Application_approve_on { get; set; }
    public string AADHAR { get; set; }

  }
}
