using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.Entities
{
  public class ContributonSheetDetails : BaseEntity
  {
    //public string Source
    //{
    //  get
    //  {
    //    var db = new AppDbContext();
    //    return db.MasterSource.Where(x => x.Id == this.SourceID).Select(x=>x.Name).FirstOrDefault();
    //  }
    //}
    public int ContributonSheetHeaderID { get; set; }
    [ForeignKey("ContributonSheetHeaderID")]
    [Display(Name = "ContributonSheetHeaderID")]
    public virtual ContributonSheetHeader ContributonSheetHeader { get; set; }

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

    public int? Month { get; set; }
    public int? Year { get; set; }

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
