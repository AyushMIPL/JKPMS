using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
  public class ErrorLogs
  {
    public int Id { get; set; }
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
    public DateTime DateOccurred { get; set; }
    public int UserId { get; set; }
    [NotMapped]
    public string UserName { get; set; }
    public string MachineName { get; set; }
    public string IpAddress { get; set; }
  }
}
