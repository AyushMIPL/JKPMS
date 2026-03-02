using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
  public class MasterContributor : BaseEntity
  {
    public MasterContributor()
    {
      //this.Pensioner = new List<MasterPensioner>();
    }
    public string FullName
    {
      get
      {
        return this.FirstName + " " + this.MidName + " " + this.LastName;
      }
    }
    [Display(Name = "JKPS ID")]
    [StringLength(10)]
    public string PersonID { get; set; }
    [Index(IsUnique = true)]
    [Display(Name = "Person ID")]
    [StringLength(10)]
    public string OldPersonID { get; set; }

    [Required(ErrorMessage = "Select Employer")]
    [Display(Name = "Employer ID")]
    public int? EmployerID { get; set; }
    [ForeignKey("EmployerID")]
    [Display(Name = "Employer")]
    public virtual MasterEmployer Employer { get; set; }
    [StringLength(11)]
    [Required(ErrorMessage = "Enter S.S.# First")]
    [Display(Name = "S.S.# *")]
    public string SocialSecurityNo { get; set; }

    [Display(Name = "Prefix")]
    //[Required]
    public int? PrefixId { get; set; }
    public virtual MasterPrefix Prefix { get; set; }

    [Display(Name = "Suffix")]
    public int? SuffixId { get; set; }
    [ForeignKey("SuffixId")]
    public virtual MasterSuffix Suffix { get; set; }

    [Required(ErrorMessage = "Enter First Name First")]
    [Display(Name = "First Name *")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Display(Name = "Mid Name")]
    [StringLength(50)]
    public string MidName { get; set; }
    [Required(ErrorMessage = "Enter Last Name")]
    [Display(Name = "Last Name *")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Display(Name = "Maiden Name")]
    [StringLength(50)]
    public string MaidenName { get; set; }

    [Display(Name = "Gender")]
    //public int? GenderId { get; set; }
    //[ForeignKey("GenderId")]

    [StringLength(1)]
    [Column(TypeName = "char")]
    [UIHint("Gender")]
    public string Gender { get; set; }
    //[Required(ErrorMessage = "Select Date Of Birth")]
    [Display(Name = "Date Of Birth *")]
    [Column(TypeName = "datetime")]
    //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
    //
    public DateTime? DateOfBirth { get; set; }
    //[Required(ErrorMessage = "Select Expected Retirement Date")]
    [Column(TypeName = "datetime")]
    //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
    [Display(Name = "Expected Retirement Date")]
    //
    public DateTime? ExpectedRetirementDate { get; set; }

    [Display(Name = "Country *")]
    [Required(ErrorMessage = "Select Country")]
    public int? CountryID { get; set; }
    [ForeignKey("CountryID")]

    public virtual MasterCountry Country { get; set; }

    [Display(Name = "Nationality")]
    public int? NationalityID { get; set; }
    [ForeignKey("NationalityID")]

    public virtual MasterNationality Nationality { get; set; }

    [Display(Name = "Permanent Address")]
    [StringLength(150)]
    public string PermanentAddress { get; set; }
    [Required(ErrorMessage = "Select City")]
    [Display(Name = "Permanent City")]
    public int? PermanentCityID { get; set; }
    public virtual MasterCity PermanentCity { get; set; }

    //[Required(ErrorMessage = "Enter Postal Address First")]
    [Display(Name = "Postal Address")]
    [StringLength(150)]
    public string PostalAddress { get; set; }

    //[Required(ErrorMessage = "Enter Phone # First")]
    //[StringLength(18)]
    //[RegularExpression("^[0-9]\\d{2,4}-\\d{6,8}$", ErrorMessage = "Enter Correct Phone Number")]
    [Display(Name = "Phone ")]
    public string Phone { get; set; }
    //[Required(ErrorMessage = "Enter Office Phone Number")]
    //[StringLength(18)]
    [Display(Name = "Office Phone ")]
    public string PhoneOffice { get; set; }
    //[StringLength(18)]
    [Display(Name = "Office Phone Extension")]
    public string PhoneOfficeExt { get; set; }



    //[StringLength(10)]
    //[RegularExpression("^[789]\\d{9}$", ErrorMessage = "Enter Correct Mobile Number")]
    [Display(Name = "Mobile")]
    public string Mobile { get; set; }

    //[StringLength(40)]
    [Display(Name = "Email")]
    //[Required(ErrorMessage = "Enter EmailId First")]
    //[EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Enter Salary Amount First")]
    [DataType("decimal(18 ,4")]
    [Display(Name = "Salary Amount (Annual) *")]
    public decimal SalaryAmount { get; set; }

    [DataType("decimal(18 ,4")]
    [Display(Name = "Monthly Salary")]
    public decimal MonthlySalary { get; set; }

    [Required(ErrorMessage = "Enter PF For First")]
    [Display(Name = "PF For *")]
    public int PFRateID { get; set; }
    [ForeignKey("PFRateID")]

    public virtual MasterPFRate MasterPFRate { get; set; }

    [Required(ErrorMessage = "Enter PF(%) First")]
    [Display(Name = "PF(%)")]
    public decimal PFRate { get; set; }

    //[NotMapped]
    //[Required(ErrorMessage = "Enter Employer PF(%) First")]
    //[Display(Name = "Employer PF(%)")]
    //public decimal EmplrPFRate { get; set; }

    [Display(Name = "First Appointment Date")]
    [Column(TypeName = "smalldatetime")]
    //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
    [ScaffoldColumn(false)]
    public DateTime? FirstAppointmentDate { get; set; }

    [Display(Name = "Last Appointment Date")]
    [Column(TypeName = "smalldatetime")]
    //
    //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
    [ScaffoldColumn(false)]
    public DateTime? LastAppointmentDate { get; set; }

    /*
    [StringLength(1)]
    [Column(TypeName = "char")]
    [ScaffoldColumn(false)]
    public string PostStatus { get; set; }

    [Required(ErrorMessage = "Enter Leave Due First")]
    [Display(Name = "Leave Due")]
    [ScaffoldColumn(false)]
    public decimal LeaveDue { get; set; }*/

    [Display(Name = "Retirement Or Resignation Date")]
    [Column(TypeName = "smalldatetime")]
    [ScaffoldColumn(false)]
    //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? RetirementOrResignationDate { get; set; }

    /*[ScaffoldColumn(false)]
    public int? LengthOfQualifyingServiceInMonthsTo31Dec2003 { get; set; }

    [ScaffoldColumn(false)]
    public int? LengthOfQualifyingServiceInMonthsFrom1Jan2014 { get; set; */

    [StringLength(150)]
    [Display(Name = "Reason")]
    [ScaffoldColumn(false)]
    public string Reason { get; set; }

    [DataType("decimal(18 ,2")]
    public decimal Balance { get; set; }
    [Display(Name = "Quarterly Amount")]
    [DataType("decimal(18 ,2")]
    public decimal QuarterlyAmount { get; set; }
    [Display(Name = "Annual Amount")]
    [DataType("decimal(18 ,2")]
    public decimal AnnualAmount { get; set; }

    public int? JobStatusID { get; set; }
    [ForeignKey("JobStatusID")]
    [Display(Name = "Job Status")]
    public virtual MasterStatus Status { get; set; }
    public string ProfilePath { get; set; }
    //public ICollection<MasterPensioner> Pensioner
    //{
    //  get;
    //  set;
    //}
    [NotMapped]
    public int OldPFRateID { get; set; }
    [NotMapped]
    public bool ShowTable { get; set; }

    [NotMapped]

    public int PermanentCityID1 { get; set; }
    //[NotMapped]
    //public int PFRateID1 { get; set; }
  }
}
