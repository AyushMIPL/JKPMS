using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.ViewModels
{
  public class PensionProcessViewModel
  {
    private int _AccountNumber = 0;

    [Display(Name = "Beneficiary Code")]
    [StringLength(50)]
    public string EmployeeCode { get; set; }
    //[Required(ErrorMessage = "Enter First Name First")]
    [Display(Name = "First Name")]
    [StringLength(50)]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Display(Name = "Scheme Type")]
    public string EmpType { get; set; }

    public string TxtEmpl_type { get; set; }
    public string JobCode { get; set; }
    public string PayPeriod { get; set; }
    public string Title { get; set; }

    [Display(Name = "Last Pay Date")]
    public DateTime? LPayDate { get; set; }
    public bool LPayDateChecked { get; set; }
    public string FullTime { get; set; }

    [Display(Name = "Payroll Date")]
    public DateTime? PayrollDate { get; set; }
    public bool PayrollDateChecked { get; set; }

    [Display(Name = "End Of Period")]
    public DateTime? EOPDate { get; set; }
    public bool EOPDateChecked { get; set; }

    public int pybatchid { get; set; } 
    public int batchprocessid { get; set; }
    public string processname { get; set; }
    public DateTime? processstartedon { get; set; }
    public string processstartedon1 { get; set; }
    public DateTime? processendedon { get; set; }
    public int recordssearched { get; set; }
    public int recordsprocessed { get; set; }
    [Display(Name = "Status")]
    public int status { get; set; }
    public string searchcriteria { get; set; }
    public string errormessage { get; set; }
    public int insertby { get; set; }
    public string insertbyName { get; set; }
    public int updateby { get; set; }
    public string updatebyName { get; set; }
    public string EmployerId { get; set; }
    public string RegionNames { get; set; }


  }
  public class RefundIndexViewModel
  {
    public string PersonID { get; set; }
    public string Name { get; set; }
    public string EmployerName { get; set; }
    public string Designation { get; set; }
    public string JobStatus { get; set; }
    public DateTime FirstAppointmentDate { get; set; }
    public DateTime RetirementOrResignationDate { get; set; }
    public int? LengthOfServiceInMonths { get; set; }
    public decimal TotalEmployeeContribution { get; set; }
    public decimal TotalEmployeeContributionWithInterest { get; set; }
    public decimal AnnualInterest { get; set; }
    public int Id { get; set; }
  }

}
