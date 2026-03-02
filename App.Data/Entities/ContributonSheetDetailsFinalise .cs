using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ContributonSheetDetailsFinalise
  {
    //public MasterContributor Contributor
    //{
    //  get
    //  {
    //    var db = new AppDbContext();
    //    return db.MasterContributor.Where(x => x.Id == this.ContributorID).FirstOrDefault();
    //  }
    //}
    [Key]
    public int Id { get; set; }
    public int ContributonSheetHeaderFinaliseID { get; set; }//comes from details after finalized
    [ForeignKey("ContributonSheetHeaderFinaliseID")]
    [Display(Name = "ContributonSheetHeaderFinaliseID")]
    public virtual ContributonSheetHeaderFinalise ContributonSheetHeaderFinalise { get; set; }

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

    public DateTime EntryDate { get; set; }
    public int? SourceID { get; set; }
    [ForeignKey("SourceID")]
    public virtual MasterSource Source { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }

    public int? EmplPFRateId { get; set; }
    public decimal? EmplPfRate { get; set; }
    public DateTime? EmplEffectiveStartDate { get; set; }
    public DateTime? EmplEffectiveEndDate { get; set; }

    public int? EmplrPFRateId { get; set; }
    public decimal? EmplrPfRate { get; set; }
    public DateTime? EmplrEffectiveStartDate { get; set; }
    public DateTime? EmplrEffectiveEndDate { get; set; }
  }
}
