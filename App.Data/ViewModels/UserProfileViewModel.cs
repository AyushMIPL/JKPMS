//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace App.Data.ViewModels
//{
//  public class NewUserProfileViewModel
//  {
//    public int Id { get; set; }
//    public int UserId { get; set; }
//    [Required(ErrorMessage = "Please User Name")]
//    [Display(Name = "Username")]
//    [StringLength(128)]
//    public string UserName { get; set; }
//    [Display(Name = "First Name")]
//    [StringLength(256)]
//    [Required(ErrorMessage = "Please enter First Name")]
//    public string FirstName { get; set; }
//    [Display(Name = "Last Name")]
//    [StringLength(256)]
//    public string LastName { get; set; }
//    [EmailAddress]
//    [Display(Name = "Email")]
//    public string Email { get; set; }
//    public string RoleName { get; set; }
//    [Required(ErrorMessage = "Please enter DOB")]
//    public DateTime? DOB { get; set; }
//    [Required(ErrorMessage = "Please Select Gender")]
//    public Gender Gender { get; set; }
//    public bool IsActive { get; set; }
//    public string Phone { get; set; }
//    public string Mobile { get; set; }
//    public string EmergencyContact1 { get; set; }
//    public string EmergencyContact2 { get; set; }
//    public string PermanentPhone { get; set; }
//    [Required(ErrorMessage = "Please Enter Password")]
//    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
//    [DataType(DataType.Password)]
//    [Display(Name = "Password")]
//    public string Password { get; set; }

//    [DataType(DataType.Password)]
//    [Display(Name = "Confirm password")]
//    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
//    public string ConfirmPassword { get; set; }
//    public string ProfilePath { get; set; }
//  }
//  public class ProfileGeneralViewModel
//  {
//    public int Id { get; set; }
//    public int UserId { get; set; }
//    [Display(Name = "Username")]
//    [StringLength(128)]
//    public string UserName { get; set; }
//    [Display(Name = "First Name")]
//    [StringLength(256)]
//    [Required(ErrorMessage = "Please enter First Name")]
//    public string FirstName { get; set; }
//    [Display(Name = "Last Name")]
//    [StringLength(256)]
//    public string LastName { get; set; }
//    [EmailAddress]
//    [Display(Name = "Email")]
//    public string Email { get; set; }
//    public string RoleName { get; set; }
//    [Required(ErrorMessage = "Please enter DOB")]
//    public DateTime? DOB { get; set; }
//    public Gender Gender { get; set; }
//    public bool IsActive { get; set; }
//    public string Phone { get; set; }
//    public string Mobile { get; set; }
//    public string EmergencyContact1 { get; set; }
//    public string EmergencyContact2 { get; set; }
//    public string PermanentPhone { get; set; }
//    public string ProfilePath { get; set; }

//    public string FullName
//    {
//      get
//      {
//        return this.FirstName + " " + this.LastName;
//      }
//    }
//  }

//  public class ProfileAddressViewModel
//  {
//    public int Id { get; set; }
//    public int UserId { get; set; }
//    public string Address1 { get; set; }
//    public string Address2 { get; set; }
//    public string City { get; set; }
//    public string State { get; set; }
//    public string Zip { get; set; }
//    public string PermanentAddress1 { get; set; }
//    public string PermanentAddress2 { get; set; }
//    public string PermanentCity { get; set; }
//    public string PermanentState { get; set; }
//    public string PermanentZip { get; set; }
//  }

//  public class ProfileOtherViewModel
//  {
//    public int Id { get; set; }
//    public int UserId { get; set; }
//    public DateTime? HireDate { get; set; }
//    public string Position { get; set; }
//    public string Notes { get; set; }
//    public DateTime? TerminationDate { get; set; }
//    public string TerminationNotes { get; set; }
//    public string DrivingLicenceNo { get; set; }
//    public decimal? DrivingExperience { get; set; }
//  }

//  public class ProfileChangePasswordViewModel
//  {
//    public int Id { get; set; }
//    public int UserId { get; set; }
//    [Required]
//    [DataType(DataType.Password)]
//    [Display(Name = "Current password")]
//    public string OldPassword { get; set; }

//    [Required]
//    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
//    [DataType(DataType.Password)]
//    [Display(Name = "New password")]
//    public string NewPassword { get; set; }

//    [DataType(DataType.Password)]
//    [Display(Name = "Confirm new password")]
//    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
//    public string ConfirmPassword { get; set; }
//  }

//  public class ProfileDocumentViewModel
//  {
//    public int Id { get; set; }
//    public int UserId { get; set; }
//    public string UserName { get; set; }
//    public int DocumentId { get; set; }
//    public string DocumentName { get; set; }
//    public string Path { get; set; }
//    public bool IsVerified { get; set; }
//    public bool IsActive { get; set; }
//  }
//}
