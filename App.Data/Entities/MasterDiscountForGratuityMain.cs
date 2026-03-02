using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterDiscountForGratuityMain : BaseEntity
  {
    //public MasterEmployerType employerType
    //{
    //  get
    //  {
    //    AppDbContext db = new AppDbContext();
    //    return db.MasterEmployerType.Where(x => x.Id == EmployerTypeID&&x.IsActive==true).FirstOrDefault();
    //  }
    //}
    [Required(ErrorMessage = "Max Year")]
    [Display(Name = "Max Year")]
    public int MaxYearsToNormalRetirement { get; set; }
    [DataType("decimal(18 ,2")]
    [Required(ErrorMessage = "Enter Discount Factor (%)")]
    [Display(Name = "Discount Factor (%)")]
    public decimal DiscountFactorPercent { get; set; }
    [Required(ErrorMessage = "Enter Date From")]
    [Display(Name = "Date From")]
    public DateTime DateFrom { get; set; }
    [Display(Name = "Date To")]
    public DateTime? DateTo { get; set; }
    [Display(Name = "Employer Type")]
    [Required(ErrorMessage = "Select Employer Type")]
    public int? EmployerTypeID { get; set; }
    [ForeignKey("EmployerTypeID")]
    public virtual MasterEmployerType employerType { get; set; }
  }
}
