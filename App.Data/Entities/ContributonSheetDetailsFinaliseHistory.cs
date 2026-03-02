using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class ContributonSheetDetailsFinaliseHistory
    {
        [Key]
        public int Id { get; set; }
        public int ContributonSheetHeaderFinaliseID { get; set; }//comes from details after finalized

        public int? ContributorID { get; set; }
        [ForeignKey("ContributorID")]
        [Display(Name = "Contributor")]
        public virtual MasterContributor Contributor { get; set; }
        [DataType("decimal(18 ,4")]
        public decimal SalaryAmount { get; set; }
        [DataType("decimal(18 ,4")]
        public decimal ContributorContribution { get; set; }
        [DataType("decimal(18 ,4")]
        public decimal EmployerContribution { get; set; }
        [Column(TypeName = "datetime")]
        
        public DateTime EntryDate { get; set; }
        public int? SourceID { get; set; }
        [ForeignKey("SourceID")]
        public virtual MasterSource Source { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public int DetailFinalizeID { get; set; }
        public int ReplaceContributorID { get; set; }
        public int ReplaceHeaderFinalizeID { get; set; }
    }
}
