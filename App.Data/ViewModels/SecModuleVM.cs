using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.ViewModels
{
  public class SecModuleVM
  {
    public string Id { get; set; }

    [ScaffoldColumn(false)]
    public int CreatedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    public DateTime? CreatedOn { get; set; }

    [ScaffoldColumn(false)]
    public int ModifiedBy { get; set; }

    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    public DateTime? ModifiedOn { get; set; }
    [Display(Name = "Status")]
    public bool IsActive { get; set; }
    public string ModuleName { get; set; }
    public string ModuleDesc { get; set; }
    public int ParentId { get; set; }
    public string ParentIdString { get; set; }
    public string Url { get; set; }
    public string ActionName { get; set; }
    public string ControllerName { get; set; }
    public string ModuleClass { get; set; }
    public int? DisplayOrder { get; set; }
    //



    [Display(Name = "Beneficiary Code")]
    [StringLength(10)]
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
    public string RegionNames { get; set; }


    public int pybatchid { get; set; }

  }
}
