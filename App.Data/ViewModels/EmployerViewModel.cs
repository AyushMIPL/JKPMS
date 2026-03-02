using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace App.Data.ViewModels
{
    public class EmployerViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Unique ID")]
        [StringLength(10)]
        public string UniqueID { get; set; }
        [Required]
        public EmployerType EmployerType { get; set; }

        [Required(ErrorMessage = "Enter Employer Name First")]
        [Display(Name = "Employer Name")]
        [StringLength(100)]
        public string EmployerName { get; set; }
        [Required(ErrorMessage = "Enter Employer Address First")]
        [Display(Name = "Employer Address")]
        [StringLength(150)]
        public string EmployerAddress { get; set; }
        [Required(ErrorMessage = "Select Country First")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Select City First")]
        [Display(Name = "City")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Enter PO.Box No. First")]
        [StringLength(50)]
        [Display(Name = " PO.Box No.")]
        public string POBoxNo { get; set; }

        [Required(ErrorMessage = "Enter Contact Person First")]
        [StringLength(50)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Enter Mobile First")]
        [StringLength(15)]
        [RegularExpression("^[789]\\d{9}$", ErrorMessage = "Enter Correct Mobile Number")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Enter PF For First")]
        [Display(Name = "PF For")]
        public int PFRateID { get; set; }

        [Required(ErrorMessage = "Enter PF(%) First")]
        [Display(Name = "PF(%)")]
        public decimal PFRate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPfApplied { get; set; }
        public string CityName { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public bool IsExpired { get; set; }

    }
    public class EmployerSummaryViewModel
    {
        public string EmployerName { get; set; }
        //{
        //  get
        //  {
        //    var db = new AppDbContext();
        //    return db.MasterEmployer.Where(x => x.Id == this.EmployerId).Select(x => x.EmployerName).FirstOrDefault();
        //  }
        //}
        public string EmployerId { get; set; }

        public int Count { get; set; }
    }
    public class TotalContributionSummaryViewModel
    {
        public string Region { get; set; }
        public string District { get; set; }
        public string EmployerName { get; set; }
        public string status { get; set; }
        public string NotUpdated { get; set; }
        //{
        //  get
        //  {
        //    var db = new AppDbContext();
        //    return db.MasterEmployer.Where(x => x.Id == this.EmployerId).Select(x => x.EmployerName).FirstOrDefault();
        //  }
        //}
        public string EmployerId { get; set; }
        public string TotalEmployerContribution { get; set; }

        public decimal? TotalContributorContribution { get; set; }
        public decimal? TotalMonthAmount { get; set; }
        public decimal? TotalArreaMonthAmount { get; set; }
        public int pybatchid { get; set; }
        public DateTime? pay_date { get; set; }
        public Int32 OAPCount { get; set; }
        public Int32 WIDCount { get; set; }
        public Int32 PCPCount { get; set; }
        public Int32 TGRCount { get; set; }
        public Int32 BeneficiariesCount { get; set; }
        public string PayMonthInYear { get; set; }
        public string LastPayMonthInYear { get; set; }
        public string IsReUploaded { get; set; }
        public bool isProcessed { get; set; }
    }
    public class BeneficiriesDetail
    {
        public string ApplicationReferenceNo { get; set; }
        public string PresentDistrict { get; set; }
        public string IFSCCode { get; set; }
        public string NameOfTheApplicant { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string CurrentStatus { get; set; }
        public string SelectPensionType { get; set; }
    }
    public class ReportParamModal
    {
        [JsonPropertyName("emplrId")]
        public string emplrId { get; set; }
        [JsonPropertyName("empl_code")]
        public string empl_code { get; set; }
        [JsonPropertyName("date1")]
        public string date1 { get; set; }
        [JsonPropertyName("date2")]
        public string date2 { get; set; }
        [JsonPropertyName("gender")]
        public string gender { get; set; }
        [JsonPropertyName("ageInYears")]
        public int? ageInYears { get; set; }
    }

    public class DistrictCount
    {
        public string District { get; set; }
        public List<empstatus> EmpStatusList { get; set; }

        public DistrictCount()
        {
            EmpStatusList = new List<empstatus>();
        }
    }
    public class empstatus
    {
        public string gender1 { get; set; }
        public string count1 { get; set; }
    }
}
