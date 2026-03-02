using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class MasterPensioner : BaseEntity
    {
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.MidName + " " + this.LastName;
            }
        }
        [StringLength(10)]
        [Display(Name = "JKPS ID")]
        public string PersonID { get; set; }

        [StringLength(10)]
        [Display(Name = "Pensioner Id")]
        public string PensionerID { get; set; }
        [Display(Name = "Employer")]
        public int? EmployerID { get; set; }
        [ForeignKey("EmployerID")]
        [Display(Name = "Employer")]
        public virtual MasterEmployer Employer { get; set; }

        [Required(ErrorMessage = "Enter S.S.# First")]
        [Display(Name = "S.S.#")]
        public string SocialSecurityNo { get; set; }

        [Display(Name = "Prefix")]
        [Required]
        public int? PrefixId { get; set; }
        public virtual MasterPrefix Prefix { get; set; }

        [Display(Name = "Suffix")]
        public int? SuffixId { get; set; }
        [ForeignKey("SuffixId")]
        public virtual MasterSuffix Suffix { get; set; }

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

        [Display(Name = "Maiden Name")]
        [StringLength(50)]
        public string MaidenName { get; set; }

        [Display(Name = "Gender")]
        [StringLength(1)]
        [Column(TypeName = "char")]
        [UIHint("Gender")]
        public string Gender { get; set; }

        [Column(TypeName = "datetime")]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? DateOfBirth { get; set; }

        [Column(TypeName = "datetime")]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expected Retirement Date")]

        public DateTime? ExpectedRetirementDate { get; set; }

        [Display(Name = "Pensioner Type ")]
        public int? PensionerTypeId { get; set; }
        [ForeignKey("PensionerTypeId")]
        public virtual MasterPensionerType PensionerType { get; set; }

        [Display(Name = "Country")]
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

        [Display(Name = "Permanent City")]
        public int? PermanentCityID { get; set; }
        public virtual MasterCity PermanentCity { get; set; }

        [Required(ErrorMessage = "Enter Postal Address First")]
        [Display(Name = "Postal Address")]
        [StringLength(150)]
        public string PostalAddress { get; set; }

        [Required(ErrorMessage = "Enter Phone # First")]
        [StringLength(13)]
        //[RegularExpression("^[0-9]\\d{2,4}-\\d{6,8}$", ErrorMessage = "Enter Correct Phone Number")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [StringLength(13)]
        [Display(Name = "Phone Office")]
        public string PhoneOffice { get; set; }

        [Required(ErrorMessage = "Enter Mobile First")]
        [StringLength(10)]
        [RegularExpression("^[789]\\d{9}$", ErrorMessage = "Enter Correct Mobile Number")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [StringLength(40)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter EmailId First")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Salary Amount First")]
        [DataType("decimal(18 ,2")]
        public decimal SalaryAmount { get; set; }

        [Required(ErrorMessage = "Enter PF For First")]
        public int? PFRateID { get; set; }
        [ForeignKey("PFRateID")]
        [Display(Name = "PF For")]
        public virtual MasterPFRate MasterPFRate { get; set; }

        [Required(ErrorMessage = "Enter PF(%) First")]
        [Display(Name = "PF(%)")]
        public decimal PFRate { get; set; }

        [Column(TypeName = "smalldatetime")]
        [ScaffoldColumn(false)]
        public DateTime? FirstAppointmentDate { get; set; }

        [Column(TypeName = "smalldatetime")]

        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        [ScaffoldColumn(false)]
        public DateTime? LastAppointmentDate { get; set; }

        [Required(ErrorMessage = "Enter Leave Due First")]
        [Display(Name = "Leave Due")]
        [ScaffoldColumn(false)]
        public decimal LeaveDue { get; set; }


        [Column(TypeName = "smalldatetime")]
        [ScaffoldColumn(true)]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RetirementOrResignationDate { get; set; }

        [ScaffoldColumn(false)]
        public int? LengthOfQualifyingServiceInMonthsTo31Dec2003 { get; set; }

        [ScaffoldColumn(false)]
        public int? LengthOfQualifyingServiceInMonthsFrom1Jan2014 { get; set; }

        [StringLength(150)]
        [Display(Name = "Reason")]
        [ScaffoldColumn(false)]
        public string Reason { get; set; }

        [DataType("decimal(18 ,2")]
        public decimal Balance { get; set; }

        [DataType("decimal(18 ,2")]
        public decimal QuarterlyAmount { get; set; }

        [DataType("decimal(18 ,2")]
        public decimal AnnualAmount { get; set; }

        [Display(Name = "Status")]
        public int? StatusID { get; set; }
        [ForeignKey("StatusID")]
        [Display(Name = "Job Status")]
        public virtual MasterStatus Status { get; set; }

        [NotMapped]
        [Display(Name = "Bank")]
        public int? BankID { get; set; }
        /*[ForeignKey("BankID")]*/

        //[NotMapped]
        //[Display(Name = "Bank")]
        //public virtual MasterBank Bank { get; set; }

        //[NotMapped]
        //[StringLength(20)]
        //[Display(Name = "Account Number")]
        //public string BankAccountNumber { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char")]
        public string HoldPymnt { get; set; }

        [Column(TypeName = "smalldatetime")]
        [ScaffoldColumn(true)]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PayPeriod { get; set; }

        [Column(TypeName = "smalldatetime")]
        [ScaffoldColumn(true)]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastPay { get; set; }

        [Column(TypeName = "smalldatetime")]
        [ScaffoldColumn(true)]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastIncDate { get; set; }

        [ScaffoldColumn(true)]
        [StringLength(6)]
        public string TypeCode { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [StringLength(6)]
        public string VacCode { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [DataType("decimal(18 ,2")]
        public decimal VacAllowed { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [DataType("decimal(18 ,2")]
        public decimal VacUsed { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [StringLength(6)]
        public string SickCode { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [DataType("decimal(18 ,2")]
        public decimal SickAllowed { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [DataType("decimal(18 ,2")]
        public decimal SickUsed { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [StringLength(6)]
        public string StataxCode { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [StringLength(6)]
        public string LoctaxCode { get; set; }
        [StringLength(6)]
        [NotMapped]
        [ScaffoldColumn(true)]
        public string SickAccrCode { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public int SickAccrCtr { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public DateTime SickLapseDate { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [StringLength(6)]
        public string VacAccrCode { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public int VacAccrCtr { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public DateTime VacLapseDate { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [StringLength(1)]
        [Column(TypeName = "char")]
        public string DirDept { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public int DfiDest { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public int ChkDigit { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public decimal StateUdf { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        [StringLength(6)]
        public string FlexDeptAcctType { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public int CashAcct { get; set; }
        [NotMapped]
        [ScaffoldColumn(true)]
        public int AgeInYears { get; set; }




    }
}
