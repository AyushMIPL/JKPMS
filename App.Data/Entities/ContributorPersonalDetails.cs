using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class ContributorPersonalDetails : BaseEntity
  {
    [Display(Name = "PersonID")]
    [StringLength(10)]
    public string PSPFUniqueID { get; set; }

    [Required(ErrorMessage = "Enter S.S.# First")]
    [Display(Name = "S.S.#")]
    public int SocialSecurityNo { get; set; }

    [Display(Name = "Prefix")]
    public int? PrefixId { get; set; }
    [ForeignKey("PrefixId")]
    public virtual Prefix Prefix { get; set; }

    [Display(Name = "Suffix")]
    public int? SuffixId { get; set; }
    [ForeignKey("SuffixId")]
    public virtual Suffix Suffix { get; set; }

    [Required(ErrorMessage = "Enter First Name First")]
    [Display(Name = "First Name")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Display(Name = "Mid Name")]
    [StringLength(50)]
    public string MidName { get; set; }

    [Display(Name = "Last Name")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Display(Name = "Gender")]
    //public int? GenderId { get; set; }
    //[ForeignKey("GenderId")]

    [StringLength(1)]
    [Column(TypeName = "char")]
    [UIHint("Gender")]
    public string Gender { get; set; }

    [Column(TypeName = "datetime")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [UIHint("Date")]
    public DateTime? DateOfBirth { get; set; }

    [Display(Name = "Birth Country")]
    public int? CountryID { get; set; }
    [ForeignKey("CountryID")]

    public virtual Country Country { get; set; }

    [Display(Name = "Nationality")]
    public int? NationalityID { get; set; }
    [ForeignKey("NationalityID")]

    public virtual Nationality Nationality { get; set; }

    [Display(Name = "Employer")]
    public int? EmployerID { get; set; }
    public virtual Employer Employer { get; set; }

    [Required(ErrorMessage = "Enter Postal Address First")]
    [Display(Name = "Postal Address")]
    [StringLength(150)]
    public string PostalAddress { get; set; }

    /*[Required(ErrorMessage = "Enter Mailing Address First")]
    [Display(Name = "Mailing Address")]
    [StringLength(150)]
    public string MailingAddress { get; set; }

    [Required(ErrorMessage = "Select City First")]
    [Display(Name = "City")]
    public int CityID { get; set; }
    public virtual City City { get; set; }
    
    [Required(ErrorMessage = "Enter Zip Code First")]
    [StringLength(10)]
    [Display(Name = "Zip Code")]
    public string ZipCode { get; set; } 

    [Column(TypeName = "bit")]
    public bool SameAsMailingAddress { get; set; }*/

    [Display(Name = "Permanent Address")]
    //[StringLength(150)]
    public string PermanentAddress { get; set; }

    [Display(Name = "Permanent City")]
    public int? PermanentCityID { get; set; }
    public virtual City PermanentCity { get; set; }

    [StringLength(10)]
    [Display(Name = "Permanent Zip Code")]
    public string PermanentZipCode { get; set; }

    [Required(ErrorMessage = "Enter Phone # First")]
    [StringLength(13)]
    [Display(Name = "Phone")]
    public string Phone { get; set; }

    [StringLength(13)]
    [Display(Name = "Phone Office")]
    public string PhoneOffice { get; set; }

    [StringLength(13)]
    [Display(Name = "Mobile")]
    public string Mobile { get; set; }

    [StringLength(40)]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    public DateTime? FirstAppointmentDate { get; set; }

    [Column(TypeName = "smalldatetime")]
    [UIHint("Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [ScaffoldColumn(false)]
    public DateTime? LastAppointmentDate { get; set; }

    [StringLength(1)]
    [Column(TypeName = "char")]
    [ScaffoldColumn(false)]
    public string PostStatus { get; set; }

    [Required(ErrorMessage = "Enter Leave Due First")]
    [Display(Name = "Leave Due")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [ScaffoldColumn(false)]
    public decimal LeaveDue { get; set; }


    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? RetirementOrResignationDate { get; set; }

    [ScaffoldColumn(false)]
    public int? LengthOfQualifyingServiceInMonthsTo31Dec2003 { get; set; }

    [ScaffoldColumn(false)]
    public int? LengthOfQualifyingServiceInMonthsFrom1Jan2014 { get; set; }

    [StringLength(150)]
    [Display(Name = "Reason")]
    [ScaffoldColumn(false)]
    public string Reason { get; set; }
  }
}
