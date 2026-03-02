using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.ViewModels
{
    public class PensionProcessViewModelTest
    {
        [Display(Name = "Beneficiary Code")]
        [StringLength(10)]
        public string EmployeeCode { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Scheme Type")]
        public string EmpType { get; set; }

        public string RegionNames { get; set; }

    public int? pybatchid { get; set; }

        [Display(Name = "Last Pay Date")]
        public DateTime? LPayDate { get; set; }

        [Display(Name = "Payroll Date")]
        public DateTime? PayrollDate { get; set; }


        [Display(Name = "End Of Period")]
        public DateTime? EOPDate { get; set; }

         

        public int batchprocessid { get; set; }
        public string processname { get; set; }
        public DateTime? processstartedon { get; set; }
        public DateTime? processendedon { get; set; }
        public int recordssearched { get; set; }
        public int recordsprocessed { get; set; }
        [Display(Name = "Status")]
        public int status { get; set; }
        public string searchcriteria { get; set; }
        public string errormessage { get; set; }
        public int insertby { get; set; }
        public string insertbyName { get; set; }
        public int updateby { get; set; }
        public string updatebyName { get; set; }
        public string EmployerId { get; set; }


        public List<SecModuleVMTest> SecmoduleList { get; set; }
    }



    public class SecModuleVMTest
    {
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
        public string ModuleName { get; set; }
        public string ModuleDesc { get; set; }
        public int ParentId { get; set; }
        public string Url { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string ModuleClass { get; set; }
        public int? DisplayOrder { get; set; }

    }

}
