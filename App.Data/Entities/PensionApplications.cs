using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class PensionApplications : BaseEntity
  {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        public MasterContributor Contributor
    {
      get
      {
        var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
        return db.MasterContributor.Where(x => x.Id.ToString() == this.PersonID).FirstOrDefault();
      }
    }
    //public PensionApplications()
    //{
    //  this.ApplicationApprovalStatus = new HashSet<ApplicationApprovalStatus>();
    //}
    //public virtual ICollection<ApplicationApprovalStatus> ApplicationApprovalStatus { get; set; }

    [Required(ErrorMessage = "Select Employer")]
    public int EmployerID { get; set; }
    [ForeignKey("EmployerID")]
    [Display(Name = "Employer")]
    public virtual MasterEmployer Employer { get; set; }


    //[ForeignKey("EmployerID")]
    //public virtual MasterPensioner Pensioners { get; set; }


    [Required(ErrorMessage = "Select Officer")]
    [StringLength(10)]
    public string PersonID { get; set; }
    //public string PersonID { get; set; }
    //[ForeignKey("PersonID")]

    [NotMapped]
    public string FirstName { get; set; }
    [NotMapped]
    public string MiddleName { get; set; }
    [NotMapped]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Select Post")]
    [Display(Name = "Designation")]
    public int DesignationId { get; set; }
    [ForeignKey("DesignationId")]
    public virtual MasterDesignation Designation { get; set; }

    public bool PensionableStatus { get; set; }

		//[Display(Name = "Type of Benefits")]
		//public BenefitType BenefitType { get; set; }
    [Required(ErrorMessage = "Select Benefits")]
    public int BenefitTypes { get; set; }

    
    [DataType("decimal(18 ,4")]
    public decimal? LeaveDue { get; set; }

    [Required(ErrorMessage = "Enter Retirement or Resignation Date")]
    //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime RetirementOrResignationDate { get; set; }


    //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    [Required(ErrorMessage = "Enter Date of Birth")]
    public DateTime DOB { get; set; }

    [Required(ErrorMessage = "Enter First Date of Appointment")]
    public DateTime DateofAppointment1st { get; set; }

    //[Required(ErrorMessage = "Enter Last Date of Appointment")]
    public DateTime? DateofAppointmentLast { get; set; }

    //[Required(ErrorMessage = "Enter Length of Service(to Dec. 31, 2003)")]
    public int? QualifyingServiceRate1 { get; set; }

    //[Required(ErrorMessage = "Enter Length of Service(from Jan. 1, 2004)")]
    public int? QualifyingServiceRate2 { get; set; }

    //[Required(ErrorMessage = "Enter Reason")]
		[DataType(DataType.MultilineText)]
    public string LongerShorterReason { get; set; }

    [Required(ErrorMessage = "Enter Annual Salary")]
    public decimal RetirementAnnualSalary1 { get; set; }

    [Required(ErrorMessage = "Enter Average Annual Salary")]
    public decimal RetirementAnnualSalary2 { get; set; }

    //[Required(ErrorMessage = "Enter Submitted By")]
    public string ApplicationSubmittedBy { get; set; }

    //[Required(ErrorMessage = "Enter Noted By")]
    //public int NotedBy { get; set; }


    ////[ScaffoldColumn(false)]
    //public int HROfficer { get; set; }
    //public int Accountant { get; set; }
    //public int AuditorTreasury { get; set; }

    //[Required(ErrorMessage = "Enter Full Pension(to Dec. 31, 2003)")]
    public decimal? FullpensionAmount1 { get; set; }

    //[Required(ErrorMessage = "Enter Full Pension(from Jan. 1, 2004)")]
    public decimal? FullpensionAmount2 { get; set; }

    [Required(ErrorMessage = "Enter Total Pension")]
    public decimal TotalFullpension { get; set; }

    //[Required(ErrorMessage = "Enter Maximum Pension")]
    public decimal MaxPension { get; set; }
    public decimal? ReducedPension { get; set; }
    public decimal? Gratuity { get; set; }
    public decimal? GratuityReducedPension { get; set; }//this is discounted gratuity
    [ScaffoldColumn(false)]
    public decimal pensionPerAnnum { get; set; }


    //[ScaffoldColumn(false)]
    
    public bool IsActive { get; set; }
    [ScaffoldColumn(false)]
    public int? LengthOfQualifyingServiceInMonthsTo31Dec2003 { get; set; }

    [ScaffoldColumn(false)]
    public int? LengthOfQualifyingServiceInMonthsFrom1Jan2014 { get; set; }

    [NotMapped]
    public string ApprovalLevel { get; set; }
    
        
    [StringLength(1)]
    [Column(TypeName = "char")]
    [UIHint("DeathInjuryNormal")]
    public string DeathInjuryNormal { get; set; }

    public int? DiscountForGratuityMainId { get; set; }
    public decimal? GratuityRate { get; set; }
    
    //public virtual ApplicationApprovalStatus ApplicationApprovalStatus { get; set; }
  }
}
