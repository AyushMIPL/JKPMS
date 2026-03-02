using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Extentions;

namespace App.Data.ViewModels
{
  public class ContributorViewModel
  {
    public int Id { get; set; }
    public string PersonID { get; set; }
    public string OldPersonID { get; set; }
    public int? EmployerID { get; set; }
    public string SocialSecurityNo { get; set; }
    public int? PrefixId { get; set; }
    public string Prefix { get; set; }
    public int? SuffixId { get; set; }
    public string Suffix { get; set; }
    public string FirstName { get; set; }
    public string MidName { get; set; }
    public string LastName { get; set; }
    public string MaidenName { get; set; }
    public string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    //
    public DateTime? ExpectedRetirementDate { get; set; }
    public int? CountryID { get; set; }

    public string Country { get; set; }
    public int? NationalityID { get; set; }

    public string Nationality { get; set; }
    public string PermanentAddress { get; set; }
    public int? PermanentCityID { get; set; }
    public string PermanentCity { get; set; }
    public string PostalAddress { get; set; }
    public string Phone { get; set; }
    public string PhoneOffice { get; set; }
    public string PhoneOfficeExt { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public decimal SalaryAmount { get; set; }
    public decimal MonthlySalary { get; set; }
    public int PFRateID { get; set; }
    public decimal PFRate { get; set; }

    public DateTime? FirstAppointmentDate { get; set; }

    public DateTime? LastAppointmentDate { get; set; }

    public DateTime? RetirementOrResignationDate { get; set; }
    public string Reason { get; set; }
    public decimal Balance { get; set; }
    public decimal QuarterlyAmount { get; set; }
    public decimal AnnualAmount { get; set; }

    public int? JobStatusID { get; set; }
    public string Status { get; set; }
    public string ProfilePath { get; set; }
    public int OldPFRateID { get; set; }
    public bool ShowTable { get; set; }
    public string FullName { get; set; }
    public string EmployerName { get; set; }
    public bool IsActive { get; set; }
    public bool IsPfApplied { get; set; }
    public DateTime EffectiveStartDate { get; set; }
    public DateTime EffectiveEndDate { get; set; }
    public bool IsExpired { get; set; }
  }
}
