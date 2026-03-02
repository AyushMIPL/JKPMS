using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.ViewModels
{
  public class CountryVM
  {
    public CountryVM()
    {
      this.States = new List<MasterState>();
    }
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
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Country Abbreviation only contain Alphabets")]
    [Required(ErrorMessage = "Enter Country Abbreviation First")]
    [Display(Name = "Country Abbreviation *")]
    [StringLength(3)]
    public string CountryCode { get; set; }
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Country only contain Alphabets")]
    [Required(ErrorMessage = "Enter Country First")]
    [Display(Name = "Country *")]
    [StringLength(50)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Enter ISD Code First")]
    [StringLength(10)]
    [Display(Name = "ISD Code *")]
    public string ISDCode { get; set; }
    public virtual ICollection<MasterState> States { get; set; }
    public virtual ICollection<MasterContributor> ContributorPersonalDetails { get; set; }
  }

}
