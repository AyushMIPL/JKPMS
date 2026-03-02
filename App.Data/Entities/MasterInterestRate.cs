using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterInterestRate : BaseEntity
  {
    //public MasterEmployerType employerType
    //{
    //  get
    //  {
    //    AppDbContext db = new AppDbContext();
    //    return db.MasterEmployerType.Where(x => x.Id == EmployerTypeID && x.IsActive == true).FirstOrDefault();
    //  }
    //}
    [Required(ErrorMessage = "Enter Interest Rate First")]
    [Display(Name = "Interest Rate")]
    public decimal InterestRate { get; set; }
    [Display(Name = "Date From")]
    [Required(ErrorMessage = "Enter Date From")]
    public DateTime? DateFrom { get; set; }
    [Display(Name = "Date To")]
    public DateTime? DateTo { get; set; }
    [Display(Name = "Employer Type")]
    [Required(ErrorMessage = "Select Employer Type")]
    public int? EmployerTypeID { get; set; }
    [ForeignKey("EmployerTypeID")]
    public virtual MasterEmployerType employerType { get; set; }
    //public ICollection<MasterContributorJobDetails> JobDetails { get; set; }
  }
}
