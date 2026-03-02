using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterContributorUpdLog
  {
    [Key]    
    public int LogId { get; set; }
    public int MasterContributorId { get; set; }
    public string PersonID { get; set; }
    public string OldPersonID { get; set; }
    public Nullable<int> EmployerID { get; set; }
    public string SocialSecurityNo { get; set; }
    public Nullable<int> PrefixId { get; set; }
    public Nullable<int> SuffixId { get; set; }
    public string FirstName { get; set; }
    public string MidName { get; set; }
    public string LastName { get; set; }
    public string MaidenName { get; set; }
    public string Gender { get; set; }
    public Nullable<System.DateTime> DateOfBirth { get; set; }
    public Nullable<System.DateTime> ExpectedRetirementDate { get; set; }
    public Nullable<int> CountryID { get; set; }
    public Nullable<int> NationalityID { get; set; }
    public string PermanentAddress { get; set; }
    public Nullable<int> PermanentCityID { get; set; }
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
    public Nullable<System.DateTime> FirstAppointmentDate { get; set; }
    public Nullable<System.DateTime> LastAppointmentDate { get; set; }
    public Nullable<System.DateTime> RetirementOrResignationDate { get; set; }
    public string Reason { get; set; }
    public decimal Balance { get; set; }
    public decimal QuarterlyAmount { get; set; }
    public decimal AnnualAmount { get; set; }
    public Nullable<int> JobStatusID { get; set; }
    public int CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedOn { get; set; }
    public int ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedOn { get; set; }
    public bool IsActive { get; set; }
  }
}
