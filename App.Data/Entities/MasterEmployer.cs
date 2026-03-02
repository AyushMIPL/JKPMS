using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterEmployer : BaseEntity
  {
    public MasterEmployer()
    {
      this.MasterContributor = new List<MasterContributor>();
      this.Pensioner = new List<MasterPensioner>();
    }

    [Display(Name = "Unique ID")]
    [StringLength(10)]
    public string UniqueID { get; set; }
    //[Required]
    //public int EmployerType { get; set; }
    [Required(ErrorMessage = "Select Employer Type")]

    [Display(Name = "Employer Type *")]
    public int? EmployerTypeID { get; set; }
    [ForeignKey("EmployerTypeID")]
    public virtual MasterEmployerType EmployerType { get; set; }


    [Required(ErrorMessage = "Enter Employer Name First")]
    [Display(Name = "Employer Name *")]
    [StringLength(150)]
    public string EmployerName { get; set; }
    [Required(ErrorMessage = "Enter Employer Address First")]
    [Display(Name = "Employer Address *")]
    [StringLength(150)]
    public string EmployerAddress { get; set; }
    [Required(ErrorMessage = "Select Country First")]
    [Display(Name = "Country *")]
    public int CountryID { get; set; }

    [Required(ErrorMessage = "Select City First")]
    [Display(Name = "City *")]
    public int CityID { get; set; }
    //public int CityID1 { get; set; }
    public virtual MasterCity City { get; set; }
    /*
    [Required(ErrorMessage = "Enter Zip Code First")]
    [StringLength(10)]
    [Display(Name = "Zip Code")]
    public string ZipCode { get; set; }*/

    [Required(ErrorMessage = "Enter PO.Box No. First")]
    [StringLength(20)]
    [Display(Name = " PO.Box No. *")]
    public string POBoxNo { get; set; }

    [Required(ErrorMessage = "Enter Contact Person First")]
    [StringLength(50)]
    [Display(Name = "Contact Person *")]
    public string ContactPerson { get; set; }

    [Required(ErrorMessage = "Enter Mobile First")]
    [StringLength(15)]
    //[RegularExpression("^[789]\\d{9}$", ErrorMessage = "Enter Correct Mobile Number")]
    [Display(Name = "Mobile *")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "Enter PF For First")]
    [Display(Name = "PF For *")]
    //[ForeignKey("PFRateID")]
    public int PFRateID { get; set; }

    [Required(ErrorMessage = "Enter PF(%) First")]
    [Display(Name = "PF(%)")]
    public decimal PFRate { get; set; }

    [NotMapped]
    public int CityID1 { get; set; }

    //[NotMapped]
    //[Required(ErrorMessage = "Enter Employer PF(%) First")]
    //[Display(Name = "Employer PF(%)")]
    //public decimal EmplrPFRate { get; set; }

    [NotMapped]
    public int OldPFRateID { get; set; }
    [NotMapped]
    public bool ShowTable { get; set; }
    public virtual MasterPFRate MasterPFRate { get; set; }
    public ICollection<MasterContributor> MasterContributor { get; set; }
    public ICollection<MasterPensioner> Pensioner { get; set; }
    
  }
}
