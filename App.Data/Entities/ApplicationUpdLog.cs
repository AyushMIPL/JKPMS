using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace App.Data.Entities
{
  public class ApplicationUpdLog
  {
    [Key]
    public int updlogid	{get;set;}
    [StringLength(1)]
    [Column(TypeName = "char")]
    public string Form_Type	{get;set;}
    public int App_Id	{get;set;}
    public string field_name	{get;set;}
    public string old_value	{get;set;}
    public string new_value	{get;set;}
    public int? update_by	{get;set;}
    public string update_machine	{get;set;}
    public DateTime? update_date	{get;set;}
          
  }
}
