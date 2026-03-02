using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterBeneficiariesDetails : BaseEntity
  {
    public Int32 MasterBeneficiariesId { get; set; }
    public Int32 SNo { get; set; }
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
    public Int64 AccountNoOfTheApplicant { get; set; }
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
    public string Last_pay_date { get; set; }
    public string Application_approve_on { get; set; }
    public string AADHAR { get; set; }
    public string ActionOnDate { get; set; }
    public string CreatedMachineInfo { get; set; }
    public string ModifiedMachineInfo { get; set; }



    }
    public class BeneficiariesRequest
    {
        public string Region { get; set; }
        public List<MasterBeneficiariesDetails> BeneficiariesDetails { get; set; }
        
    }
}
