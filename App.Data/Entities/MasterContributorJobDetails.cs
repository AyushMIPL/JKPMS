using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    public class MasterContributorJobDetails : BaseEntity
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        public MasterContributor Contributor
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return db.MasterContributor.Where(x => x.PersonID == this.PersonID).FirstOrDefault();
            }
        }
        [Display(Name = "JKPS ID")]
        [StringLength(10)]
        public string PersonID { get; set; }
        //[ForeignKey("PersonID")]
        [Required(ErrorMessage = "Select Employer First")]
        [Display(Name = "Employer")]
        public int EmployerID { get; set; }
        [ForeignKey("EmployerID")]
        public virtual MasterEmployer Employer { get; set; }

        [Display(Name = "Employment Type")]

        public int? EmployerType { get; set; }

        [Display(Name = "Department *")]
        [Required(ErrorMessage = "Select Department")]
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual MasterDepartment Department { get; set; }
        [Required(ErrorMessage = "Enter Job Title First")]
        //[Display(Name = "Designation *")]
        public int? DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public virtual MasterDesignation Designation { get; set; }

        //[Required(ErrorMessage = "Enter Job Title First")]
        [Display(Name = "Job Title *")]
        public int? JobTitleID { get; set; }
        [ForeignKey("JobTitleID")]
        public virtual MasterJobTitle JobTitle { get; set; }

        [Display(Name = "Grade")]
        public int? GradeId { get; set; }
        [ForeignKey("GradeId")]
        public virtual MasterGrade Grade { get; set; }

        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Hire Date *")]

        [Required(ErrorMessage = "Select Hire Date")]
        public DateTime HireDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Joining Date *")]

        [Required(ErrorMessage = "Select Joining Date")]
        public DateTime JoiningDate { get; set; }


        [Display(Name = "Contribution Start Date ")]
        public DateTime? ContributionStartDate { get; set; }

        [Required(ErrorMessage = "Enter Present Salary First")]
        [Display(Name = "Present Salary (Annual) *")]
        [DataType("decimal(18 ,4")]
        public decimal PresentSalary { get; set; }

        [Display(Name = "Job Description")]
        [StringLength(250)]
        public string JobDescription { get; set; }
    }
}
