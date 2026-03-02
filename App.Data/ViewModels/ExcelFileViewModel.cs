using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Entities;
namespace App.Data.ViewModels
{
  public class ExcelFileViewModel
  {
    public int Id { get; set; }
    public int? EmployerID { get; set; }
    [ForeignKey("EmployerID")]
    [Display(Name = "Employer")]
    public virtual MasterEmployer Employer { get; set; }
    public int? ContributorID { get; set; }
    [ForeignKey("ContributorID")]
    [Display(Name = "Contributor")]
    public virtual MasterContributor Contributor { get; set; }
    public string PersonId { get; set; }
    public string EmployerName { get; set; }
    [Required(ErrorMessage = "Select Month First")]
    public string Month { get; set; }
    [Required(ErrorMessage = "Select Year First")]
    public int Year { get; set; }
    public int MonthInDigit { get; set; }
    public string ContributorName { get; set; }
    public decimal? SystemSalaryAmount { get; set; }
    public decimal? SalaryAmount { get; set; }
    public decimal? SystemContributorContribution { get; set; }
    public decimal? ContributorContribution { get; set; }
    public decimal? SystemEmployerContribution { get; set; }
    public decimal? EmployerContribution { get; set; }
    public decimal? TempEmployerContribution { get; set; }
    public decimal? TempContributorContribution { get; set; }
    public string FilePath { get; set; }
    public string Final { get; set; }
    public string SourceName { get; set; }
    public int? SourceID { get; set; }
    [ForeignKey("SourceID")]
    public virtual MasterSource Source { get; set; }
    public bool IsActive { get; set; }
    public string error { get; set; }
    public string status { get; set; }
    public DateTime? EntryDate { get; set; }
    public int? contributorSheetHeaderId { get; set; }
    public int EmplPFRateId { get; set; }
    public decimal EmplPFRate { get; set; }
    public DateTime EmplEffectiveStartDate { get; set; }
    public DateTime EmplEffectiveEndDate { get; set; }

    public int EmplrPFRateId { get; set; }
    public decimal EmplrPFRate { get; set; }  
    public DateTime EmplrEffectiveStartDate { get; set; }
    public DateTime EmplrEffectiveEndDate { get; set; }

  }

  public class CsvFileViewModel
  {
    public Int32 SNo { get; set; }
    public bool IsValidate { get; set; }
    public string ApplicationReferenceNo { get; set; }
    public string SubmissionLocation { get; set; }
    public string SubmissionDate { get; set; }
    public string AppliedBy { get; set; }
    public string SelectTehsilSocialWelfareOffice_TSWO { get; set; }
    public string SelectDistrict { get; set; }
    public string NameOfTheApplicant { get; set; }
    public string DateOfBirth { get; set; }
    public string Age_InYears { get; set; }
    public string MobileNumber { get; set; }
    public string DoYouHaveBPLcard { get; set; }
    public string FatherOrHusbandOrGuardianName { get; set; }
    public string EMail { get; set; }
    public string Category { get; set; }
    public string Gender { get; set; }
    public string PresentAddress { get; set; }
    public string PresentDistrict { get; set; }
    public string PresentVillageName { get; set; }
    public string Pincode { get; set; }
    public string PresentHalqaPanchayatOrMunicipalityName { get; set; }
    public string PresentTehsil { get; set; }
    public string PermanentAddress { get; set; }
    public string PermanentDistrict { get; set; }
    public string PermanentTehsil { get; set; }
    public string PermanentHalqaPanchayatOrMunicipalityName { get; set; }
    public string PermanentVillageName { get; set; }
    public string BranchName { get; set; }
    public string IFSCCode { get; set; }
    public string AccountNoOfTheApplicant { get; set; }
    public string BankName { get; set; }
    public string SelectPensionType { get; set; }
    public string PercentageofDisability { get; set; }
    public string CivilCondition { get; set; }
    public string AreYouPreviouslyTakingPensionFromJK_ISSS_GOI_NSAP { get; set; }
    public string BankName1 { get; set; }
    public string BranchName1 { get; set; }
    public string IFSCCode1 { get; set; }
    public string AccountNumber { get; set; }
    public string ApplicationSanctionedunderSchemeName { get; set; }
    public string CurrentTask { get; set; }
    public string CurrentStatus { get; set; }
    public string LastTask { get; set; }
    public string VersionNo { get; set; }
    public string Last_pay_date  { get; set; }
    public string Application_approve_on { get; set; }
    public string ActionOnDate { get; set; }
    //public string AADHAR { get; set; }

  }

  public class DeathlistViewModel
  {
    public string Relationship { get; set; }
    public decimal PensionAmount { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }
  }
  public class ApprovalProcessAssignedUserViewModel
  {

    [Key]
    public int ApprovalProcessId { get; set; }
    public string LevelName { get; set; }
    public string Username { get; set; }
    public bool IsActive { get; set; }
    public string ApplicationType { get; set; }
    public int level { get; set; }

  }
  public class PensionOrRefundViewModel
  {
    public string Id { get; set; }
    public string contributorId { get; set; }
    public string Name { get; set; }
    public string ApplicationType { get; set; }
    public decimal? OldAmount { get; set; }
    public decimal? NewAmount { get; set; }
    public bool recalculated { get; set; }
  }


}
