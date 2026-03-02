using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class PensionProcessDetails:BaseEntity
  {
    public int pybatchid { get; set; }
    public int batchprocessid { get; set; }
    public string processname { get; set; }
    public DateTime processstartedon { get; set; }
    public DateTime processendedon { get; set; }
    public int recordssearched { get; set; }
    public int recordsprocessed { get; set; }
    public int status { get; set; }
    public string searchcriteria { get; set; }
    public string errormessage { get; set; }
     
  }
}
