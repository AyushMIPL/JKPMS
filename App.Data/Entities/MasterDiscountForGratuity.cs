using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class MasterDiscountForGratuity : BaseEntity
  {
    public int YearsToNormalRetirement { get; set; }
    [DataType("decimal(16 ,2")]
    public decimal DiscountFactorPercent { get; set; }
    [DataType("decimal(8 ,4")]
    public decimal DiscountFactor { get; set; }
    public int? EmployerTypeID { get; set; }
  }
}
