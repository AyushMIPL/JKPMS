using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.Entities
{
  public class MasterDiscountForGratuityDetails : BaseEntity
  {
    public int MainId { get; set; }
    public int YearsToNormalRetirement { get; set; }
    [DataType("decimal(18 ,9")]
    public decimal DiscountFactor { get; set; }

  }
}
