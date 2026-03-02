using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.Entities
{
  public class ContributonTransactions:BaseEntity
  {
    public int CSDFID { get; set; }//ContributonSheetDetailsFinaliseID

    /*[Column(TypeName = "datetime")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    
    public DateTime? Transactiondate { get; set; }*/
    [Required]
    [DataType("decimal(18 ,4")]
    public decimal TransactionAmount { get; set; } 
    public string TransactionType { get; set; } 
    public string TransactionName { get; set; } 

  }
}
