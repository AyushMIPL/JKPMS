using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.ReportForms
{
    public class ContributionsByContributors123
  {
    public string JKPSUniqueID { get; set; }

    public int? EmployerID { get; set; }

    public string SocialSecurityNo { get; set; }

    public int? PrefixId { get; set; }

    public int? SuffixId { get; set; }

    public string FirstName { get; set; }

    public string MidName { get; set; }

    public string LastName { get; set; }

    public string MaidenName { get; set; }

    public string Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? CountryID { get; set; }
    public string CountryName { get; set; }


    public int? NationalityID { get; set; }


    public string PermanentAddress { get; set; }

    public int? PermanentCityID { get; set; }

    public string PostalAddress { get; set; }

    public string Phone { get; set; }

    public string PhoneOffice { get; set; }

    public string Mobile { get; set; }

    public string Email { get; set; }

    public DateTime? FirstAppointmentDate { get; set; }

    public DateTime? LastAppointmentDate { get; set; }

    public string PostStatus { get; set; }

    public decimal LeaveDue { get; set; }

    public DateTime? RetirementOrResignationDate { get; set; }

    public int? LengthOfQualifyingServiceInMonthsTo31Dec2003 { get; set; }

    public int? LengthOfQualifyingServiceInMonthsFrom1Jan2014 { get; set; }

    public string Reason { get; set; }
  }
    public class ContributionsByContributorsModel
  {
    public int Id { get; set; }
    public string JKPSUniqueID { get; set; }
    public int EmployerID { get; set; }
    public string SocialSecurityNo { get; set; }
    public int PrefixId { get; set; }
    public int SuffixId { get; set; }
    public string FirstName { get; set; }
    public string MidName { get; set; }
    public string LastName { get; set; }
    public string MaidenName { get; set; }
    public string ContributerFullName { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int CountryID { get; set; }
    public string CountryName { get; set; }
    public int NationalityID { get; set; }

    public string Email { get; set; }
    public string Nationality { get; set; }
    public string PrefixName { get; set; }
    public string SufixName { get; set; }
    public string EmployerName { get; set; }
    public string EmployerAddress { get; set; }
    public string EmployerCity { get; set; }
    public string EmployerContractPerson { get; set; }
    public string EmployerMobile { get; set; }
    //public string PermanentAddress { get; set; }

    //public int? PermanentCityID { get; set; }

    //public string PostalAddress { get; set; }

    //public string Phone { get; set; }

    //public string PhoneOffice { get; set; }

    //public string Mobile { get; set; }


    //public DateTime? FirstAppointmentDate { get; set; }

    //public DateTime? LastAppointmentDate { get; set; }

    //public string PostStatus { get; set; }

    //public decimal LeaveDue { get; set; }

    //public DateTime? RetirementOrResignationDate { get; set; }

    //public int? LengthOfQualifyingServiceInMonthsTo31Dec2003 { get; set; }

    //public int? LengthOfQualifyingServiceInMonthsFrom1Jan2014 { get; set; }

    //public string Reason { get; set; }
  }
}