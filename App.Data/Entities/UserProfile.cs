using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class UserProfile : BaseEntity
  {
        [Display(Name = " Full Name")]
        public string FullName
    {
      get
      {
        return this.FirstName + " " + this.MiddleName+" "+this.LastName;
      }
    }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual AppUser user { get; set; }
    [StringLength(50)]
       
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }
    [StringLength(50)]
    public string MiddleName { get; set; }
    [StringLength(50)]
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
    [StringLength(50)]
        [Display(Name = "Employee Code")]
        [Required(ErrorMessage = "Please Enter Employee Code")]
        public string EmployeeCode { get; set; }
    [Column(TypeName = "datetime")]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Select Date of Birth")]
        public DateTime? DOB { get; set; }
    [Column(TypeName = "datetime")]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Select Hire Date")]
        [Display(Name = "Hire Date")]
        public DateTime? HireDate { get; set; }
    
    [StringLength(40)]
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Enter Email Id First")]
    [EmailAddress]
    [Index(IsUnique = true)]
    public string Email { get; set; }

        //[StringLength(12)]
        //[Index(IsUnique = true)]
        //    [Required(ErrorMessage = "Please Enter Aadhar Number")]
        [Index(IsUnique = true)]
    [Required]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "Social Security Number must be exactly 12 digits.")]
    [Display(Name = "Aadhar Number")]
    public string SocialSecurityNumber { get; set; }
    [StringLength(50)]
    public string ProfilePath { get; set; }    

    [StringLength(1)]
    [Column(TypeName = "char")]
    [UIHint("Gender")]
    public string Gender { get; set; }
  }
}
