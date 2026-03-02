using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ProcessDirectDepositHeader:BaseEntity
  {
    public int DocNo { get; set; }
    public string CompanyName { get; set; }
    public string EntryDescription { get; set; }
    public int DfiImmed { get; set; }
    public int SvcClass { get; set; }
    public int BatchNo { get; set; }
    public DateTime BatchDate { get; set; }
    public DateTime CreateDate { get; set; }
    public string FileId { get; set; }
    public string Used { get; set; }
    public string BankCode { get; set; }

  }
}
