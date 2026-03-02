using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterCountry : BaseEntity
  {
    public MasterCountry()
    {
      this.States = new List<MasterState>();
    }
    [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Country Abbreviation only contain Alphabets")]
    [Required(ErrorMessage = "Please Enter Country Abbreviation ")]
    [Display(Name = "Country Abbreviation *")]
    [StringLength(3)]
    public string CountryCode { get; set; }
    [RegularExpression("^[a-zA-Z ]*$",ErrorMessage = "Country only contain Alphabets")]
    [Required(ErrorMessage = "Please Enter Country ")]
    [Display(Name = "Country *")]
    [StringLength(50)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please Enter ISD Code ")]
    [StringLength(10)]
    [Display(Name = "ISD Code *")]
    public string ISDCode { get; set; }
    public virtual ICollection<MasterState> States { get; set; }
    public virtual ICollection<MasterContributor> ContributorPersonalDetails { get; set; }
  }
}
