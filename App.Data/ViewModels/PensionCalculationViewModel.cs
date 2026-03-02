using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.ViewModels
{
  public class PensionCalculationViewModel
  {
    public string PersonID { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? ExpectedRetirementDate { get; set; }
    public decimal SalaryAmount { get; set; }
    public decimal? SalaryAmount2 { get; set; }
    public DateTime? FirstAppointmentDate { get; set; }
    public DateTime? RetirementOrResignationDate { get; set; }
    public int? LengthOfQualifyingServiceInMonthsTo31Dec2003 { get; set; }
    public int? LengthOfQualifyingServiceInMonthsFrom1Jan2014 { get; set; }
    public int? noOfMonthsWithContribution { get; set; }
    public int? LengthOfQualifyingService { get; set; }
    public decimal FullPension { get; set; }
    public decimal FullPension1 { get; set; }
    public decimal FullPension2 { get; set; }
    public decimal MaxPension { get; set; }
    public decimal Gratuity { get; set; }
    public decimal DiscountedGratuity { get; set; }
    public decimal ReducedPension { get; set; }
    public string PensionType { get; set; }
    public string error { get; set; }
  }
  public class refundViewModel
  {
    public string ServiceLength { get; set; }
    public string TotalInterestPerYear { get; set; }
    public string TotalContributionPerYear { get; set; }
    public string TotalRefundAmountPerYear { get; set; }
    public string error { get; set; }
    public string interestRate { get; set; }
    
  }
}
