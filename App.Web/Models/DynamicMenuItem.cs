using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Models
{
  public class DynamicMenuItem
  {
    public string LinkText { get; set; }
    public string ActionName { get; set; }
    public string Area { get; set; }
    public string ControllerName { get; set; }
    public string Roles { get; set; }
    public string Class { get; set; }
    public int MenuId { get; set; }
    public int IsParent { get; set; }
    public int ParentMenuId { get; set; }
    public int? DisplayOrder { get; set; }
  }
}