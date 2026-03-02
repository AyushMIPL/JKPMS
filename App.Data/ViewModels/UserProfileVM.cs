using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.ViewModels
{
    public class UserProfileVM
    {
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.MiddleName + " " + this.LastName;
            }
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
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser user { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Column(TypeName = "datetime")]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? DOB { get; set; }
        [Column(TypeName = "datetime")]
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? HireDate { get; set; }

        [StringLength(40)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Email Id First")]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string SocialSecurityNumber { get; set; }
        [StringLength(50)]
        public string ProfilePath { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char")]
        [UIHint("Gender")]
        public string Gender { get; set; }
    }
}