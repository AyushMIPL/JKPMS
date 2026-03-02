using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace App.Data.Entities
{
    public class RefundApplications : BaseEntity
    {
        private ConnectionStringProvider ConnectionStringProvider = new ConnectionStringProvider();
        //public RefundApplications()
        //{
        //  this.ApplicationApprovalStatus = new HashSet<ApplicationApprovalStatus>();
        //}
        //public virtual ICollection<ApplicationApprovalStatus> ApplicationApprovalStatus { get; set; }
        public MasterContributor Contributor
        {
            get
            {
                var db = new AppDbContext(ConnectionStringProvider.GetConnectionString());
                return db.MasterContributor.Where(x => x.Id.ToString() == PersonID).FirstOrDefault();
            }
        }
        public string PersonID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Designation")]
        public int? DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public virtual MasterDesignation Designation { get; set; }

        public int? EmployerID { get; set; }
        [ForeignKey("EmployerID")]
        [Display(Name = "Employer")]
        public virtual MasterEmployer Employer { get; set; }

        [DataType(DataType.Date)]
        //[Column(TypeName = "smalldatetime")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[ScaffoldColumn(false)]
        [Required(ErrorMessage = "First Appointment Date")]
        public DateTime FirstAppointmentDate { get; set; }

        //[DataType(DataType.Date)]
        //[Column(TypeName = "smalldatetime")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[ScaffoldColumn(false)]
        [Required(ErrorMessage = "Retirement/Resignation Date")]
        public DateTime RetirementOrResignationDate { get; set; }
        [Required(ErrorMessage = "Job Status")]
        public int JobStatus { get; set; }
        [ForeignKey("JobStatus")]
        public virtual MasterStatus JobStausName { get; set; }

        //[ScaffoldColumn(false)]
        public int? LengthOfServiceInMonths { get; set; }
        [Required(ErrorMessage = "Employee Contribution")]
        public decimal TotalEmployeeContribution { get; set; }
        [Required(ErrorMessage = "Annual Interest")]
        public decimal AnnualInterest { get; set; }
        [Required(ErrorMessage = "Contribution With Interest")]
        public decimal TotalEmployeeContributionWithInterest { get; set; }

        public int SubmittedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? SubmittedDate { get; set; }
        [Display(Name = "Interest Rate")]
        public int? InterestRateId { get; set; }
        public decimal? InterestRate { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? ApprovedDate { get; set; }

        //public int VerifiedBy  { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? VerifiedDate { get; set; }

        //public int CertifiedForPaymentBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? CertifiedForPaymentDate { get; set; }

        //public int AuditedBy { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? AuditedDate { get; set; }
    }
}
