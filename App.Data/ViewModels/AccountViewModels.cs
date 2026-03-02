using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.ViewModels
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }

  public class ExternalLoginListViewModel
  {
    public string ReturnUrl { get; set; }
  }

  //public class SendCodeViewModel
  //{
  //    public string SelectedProvider { get; set; }
  //    public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
  //    public string ReturnUrl { get; set; }
  //    public bool RememberMe { get; set; }
  //}

  public class UserRegisterViewModel
  {
    public string FullName
    {
      get
      {
        return this.FirstName + " " + this.MiddleName + " " + this.LastName;
      }
    }
    public int Id { get; set; }
    public int UserId { get; set; }

    [StringLength(50)]
    [Required(ErrorMessage = "Please Enter First Name")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [StringLength(50)]
    [Display(Name = "Middle Name")]
    public string MiddleName { get; set; }
    [StringLength(50)]
    [Required(ErrorMessage = "Please Enter Last Name")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [StringLength(int.MaxValue)]
    [Required(ErrorMessage = "Please Select District")]
    [Display(Name = "District")]
    public string RegionNames { get; set; }
    [Display(Name = "User Name")]
    public string UserName { get; set; }
    [StringLength(50)]
    [Required(ErrorMessage = "Please Enter Employee Code")]
    [Display(Name = "Employee Code")]
    public string EmployeeCode { get; set; }
    [Required(ErrorMessage = "Please Select Date of Birth")]
    public DateTime? DOB { get; set; }
    [Required(ErrorMessage = "Please Select Hire Date")]
    public DateTime? HireDate { get; set; }
    [StringLength(50)]
    [Required(ErrorMessage = "Please Enter Email Id")]
    public string Email { get; set; }
    //[StringLength(50)]
    //[Required(ErrorMessage = "Please Enter Aadhar Number")]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhar Number must be exactly 12 digits.")]
    [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar Number must be exactly 12 digits.")]
    [Display(Name = "Aadhar Number")]
    public string SocialSecurityNumber { get; set; }
    [StringLength(50)]
    public string ProfilePath { get; set; }
    [Required(ErrorMessage = "Please Select Role")]
    public string Role { get; set; }
    [Required(ErrorMessage = "Please Select Gender")]
    [StringLength(1)]
    [Column(TypeName = "char")]
    [UIHint("Gender")]
    public string Gender { get; set; }
    [Required]
    [StringLength(100)]
    [RegularExpression("^.*(?=.{6,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password Must Contain atleast symbol,uppercase & lower case alphabet and have more than 6 characters...i.e:-Example@123")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    public bool IsActive { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

  }

  public class VerifyCodeViewModel
  {
    [Required]
    public string Provider { get; set; }

    [Required]
    [Display(Name = "Code")]
    public string Code { get; set; }
    public string ReturnUrl { get; set; }

    [Display(Name = "Remember this browser?")]
    public bool RememberBrowser { get; set; }

    public bool RememberMe { get; set; }
  }

  public class ForgotViewModel
  {
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }

  public class LoginViewModel
  {
    [Required]
    [Display(Name = "Username")]
    [StringLength(128)]
    public string UserName { get; set; }

    //[Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }


  public class ProfileViewModel
  {
    public int Id { get; set; }
    [Required]
    [Display(Name = "Username")]
    [StringLength(128)]
    public string UserName { get; set; }
    [Required]
    [Display(Name = "First Name")]
    [StringLength(256)]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    [StringLength(256)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    //[Required]
    //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //[DataType(DataType.Password)]
    //[Display(Name = "Password")]
    //public string Password { get; set; }

    //[DataType(DataType.Password)]
    //[Display(Name = "Confirm password")]
    //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //public string ConfirmPassword { get; set; }
    public bool IsActive { get; set; }
    public string RoleName { get; set; }
    public int UserID { get; set; }
    public DateTime? DOB { get; set; }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string EmergencyContact1 { get; set; }
    public string EmergencyContact2 { get; set; }
    public string DrivingLicenceNo { get; set; }
    public decimal? DrivingExperience { get; set; }
    public string PermanentAddress1 { get; set; }
    public string PermanentAddress2 { get; set; }
    public string PermanentCity { get; set; }
    public string PermanentState { get; set; }
    public string PermanentZip { get; set; }
    public string PermanentPhone { get; set; }
    public DateTime? HireDate { get; set; }
    public string Position { get; set; }
    public string Notes { get; set; }
    public DateTime? TerminationDate { get; set; }
    public string TerminationNotes { get; set; }
    public Gender Gender { get; set; }
  }
  /*---------------------------*/
  public class NewUserGeneralInfoViewModel
  {
    public int Id { get; set; }
    public int UserID { get; set; }
    [Required]
    [Display(Name = "Username")]
    [StringLength(128)]
    public string UserName { get; set; }
    [Required]
    [Display(Name = "First Name")]
    [StringLength(256)]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    [StringLength(256)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    public string RoleName { get; set; }
    public DateTime? DOB { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
  public class UserGeneralInfoViewModel
  {
    public int Id { get; set; }
    public int UserID { get; set; }
    [Required]
    [Display(Name = "Username")]
    [StringLength(128)]
    public string UserName { get; set; }
    [Required]
    [Display(Name = "First Name")]
    [StringLength(256)]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    [StringLength(256)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    public string RoleName { get; set; }
    public DateTime? DOB { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; }
  }
  public class UserAddressInfoViewModel
  {
    public int Id { get; set; }
    public int UserID { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string EmergencyContact1 { get; set; }
    public string EmergencyContact2 { get; set; }
    public string PermanentAddress1 { get; set; }
    public string PermanentAddress2 { get; set; }
    public string PermanentCity { get; set; }
    public string PermanentState { get; set; }
    public string PermanentZip { get; set; }
    public string PermanentPhone { get; set; }
  }
  public class UserOtherInfoViewModel
  {
    public int Id { get; set; }
    public int UserID { get; set; }
    public string DrivingLicenceNo { get; set; }
    public decimal? DrivingExperience { get; set; }
    public DateTime? HireDate { get; set; }
    public string Position { get; set; }
    public string Notes { get; set; }
    public DateTime? TerminationDate { get; set; }
    public string TerminationNotes { get; set; }
  }
  /*---------------------------*/
  public class ResetPasswordViewModel
  {
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string Code { get; set; }
  }

  public class ForgotPasswordViewModel
  {
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }
}
