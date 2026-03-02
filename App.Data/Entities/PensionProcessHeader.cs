using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class PensionProcessHeader:BaseEntity
  {
    public int pybatchid { get; set; }
    public DateTime startedon { get; set; }
    public DateTime endedon { get; set; }
    public string searchcriteria { get; set; }
    public int recordssearched { get; set; }
    public int recordcound { get; set; }
    public int status { get; set; }
    public string errormessage { get; set; }
    
  }
}
