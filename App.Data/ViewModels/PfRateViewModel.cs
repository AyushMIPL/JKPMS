using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Extentions;

namespace App.Data.ViewModels
{
  public class PfRateViewModel
  {
    public int Id { get; set; }
    public string Value { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime EffectiveEndDate { get; set; }
  }
}
