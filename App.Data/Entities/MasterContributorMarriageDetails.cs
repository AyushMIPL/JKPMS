using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterContributorMarriageDetails:BaseEntity
  {
    [Display(Name = "JKPS ID")]
    [StringLength(10)]
    public string PersonId { get; set; }

    [Required(ErrorMessage = "Enter First Name First")]
    [Display(Name = "First Name *")]
    [StringLength(50)]
    public string SpouseFirstName { get; set; }

    [Display(Name = "Mid Name")]
    [StringLength(50)]
    public string MidName { get; set; }
    [Required(ErrorMessage = "Enter Last Name First")]
    [Display(Name = "Last Name *")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Display(Name = "Birth Country *")]
    [Required(ErrorMessage = "Select Country")]
    public int? CountryID { get; set; }
    [ForeignKey("CountryID")]

    public virtual MasterCountry Country { get; set; }

    [Column(TypeName = "datetime")]
    //
    [Required(ErrorMessage = "Enter Date of Birth")]
    //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Enter Phone # First")]
    //[StringLength(13)]
    [Display(Name = "Phone *")]
    //[RegularExpression("^[0-9]\\d{2,4}-\\d{6,8}$", ErrorMessage = "Enter Correct Phone Number")]
    public string Phone { get; set; }
    //[Required(ErrorMessage = "Enter Office Phone Number")]
    //[StringLength(13)]
    [Display(Name = "Phone Office")]
    public string PhoneOffice { get; set; }
    [Required]
    //[StringLength(13)]
    [Display(Name = "Mobile *")]
    //[RegularExpression("^[789]\\d{9}$", ErrorMessage = "Enter Correct Mobile Number")]
    public string Mobile { get; set; }

    [StringLength(40)]
    [Display(Name = "Email")]
    [EmailAddress]
    //[Required]
    public string Email { get; set; }

    [Column(TypeName = "datetime")]
    //
    [Display(Name = "Marriage Begin Date *")]
    [Required(ErrorMessage = "Marriage Begin Date")]
    //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? MarriageBeginDate { get; set; }

    [Column(TypeName = "datetime")]
    //
    [Display(Name = "Marriage End Date")]
    //[Required(ErrorMessage = "Marriage End Date")]
    //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? MarriageEndDate { get; set; }

    [Display(Name = "Marital Status *")]
    [Required(ErrorMessage = "Select Marital Status")]
    public int? MaritalStatusId { get; set; }
    public virtual MasterMaritalStatus MaritalStatus { get; set; }
  }
}
