using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class ContributonSheetDetailsHistory : BaseEntity
    {
        public int ContributonSheetHeaderID { get; set; }
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
        //[DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? EntryDate { get; set; }
        [Display(Name = "Source")]
        public int? SourceID { get; set; }
        [ForeignKey("SourceID")]
        public virtual MasterSource Source { get; set; }
        public int DetailID { get; set; }
        public int ReplaceContributorID { get; set; }
        public int ReplaceHeaderID { get; set; }
        
    }
}
