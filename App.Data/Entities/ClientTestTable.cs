using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ClientTestTable
  {
    public int Id { get; set; }
    public string ReferenceNo { get; set; }
    public string Name { get; set; }

    // Add property Rohit client Testing Perpose
    public string District { get; set; }
    public string PensionType { get; set; }
    public string Category { get; set; }

  }
}
